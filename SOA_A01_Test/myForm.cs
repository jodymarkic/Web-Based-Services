/*
 *  FILENAME        : myForm.cs
 *  PROJECT         : SOA_A01
 *  PROGRAMMER      : Jody Markic
 *  FIRST VERSION   : 10/1/2017
 *  DESCRIPTION     : This file hold the UI and API's for SOA_A01's GUI.
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;


namespace SOA_A01_Test
{
    //
    //  CLASS: myForm : Form
    //  DESCRIPTION: This Class holds all event handles for UI based interactions in SOA_A01.
    //
    //
    public partial class myForm : Form
    {
        //local variables
        private enum xmlTagFlags { webCount = 0, serCount, funcCount, paramCount, website, webservice, parameter }
        public DataTable resultTable;

        ServiceRecord serviceRecord = new ServiceRecord();
        List<ServiceRecord> serviceRecordList = new List<ServiceRecord>();
        Interpreter interpreter = new Interpreter();

        DataTable myDataTable = new DataTable();

        //
        //  METHOD      : myForm
        //  DESCRIPTION : Consructor
        //  PARAMETERS  : na
        //  RETURNS     : na
        //
        public myForm()
        {
            InitializeComponent();
            interpreter.ReadConfig();
            interpreter.WriteToConfig();

            //connects the gridview with table data.
            ParametersDataGridView.DataSource = myDataTable;

            SeedUI(); //seed UI
        }

        //
        //  METHOD      : SeedUI
        //  DESCRIPTION : seeds the service combobox and reads the config file for all records
        //                stores a copy of each record in a list of ServiceRecord objects.
        //  PARAMETERS  : na
        //  RETURNS     : void
        //
        public void SeedUI()
        {
            int duplicateIndicator = 0;
            string[] elements;
            string[] parameters;

            //read the Service config file
            using (StreamReader reader = new StreamReader(@"..\..\ServiceRecordConfig.txt"))
            {
                string currentRecord;
                //read each line
                while ((currentRecord = reader.ReadLine()) != null)
                {
                    ServiceRecord serviceRecord = new ServiceRecord();
                    //split each line into individual values
                    //and store them into a object's property
                    elements = currentRecord.Split(',');
                    serviceRecord.HostHeader = elements[0];
                    serviceRecord.MethodAddress = elements[1];
                    serviceRecord.MethodName = elements[2];
                    serviceRecord.ServiceName = elements[3];
                    Dictionary<string, string> myDictionary = new Dictionary<string, string>();
                    for (int i = 4; i < elements.Length; i++)
                    {
                        //Dictionary<string, string> myDictionary = new Dictionary<string, string>();
                        parameters = elements[i].Split('-');

                        myDictionary.Add(parameters[0], parameters[1]);
                    }
                    //store parameters
                    serviceRecord.SetParameters(myDictionary);

                    //add record to a list
                    serviceRecordList.Add(serviceRecord);
                }
            }

            foreach (ServiceRecord record in serviceRecordList)
            {
                if (!ServicesCombobox.Items.Contains(record.ServiceName))
                {
                    //and service names to combobox
                    ServicesCombobox.Items.Add(record.ServiceName);
                }
            }
        }

        //
        //  METHOD      : ServicesCombobox_SelectedIndexChanged
        //  DESCRIPTION : event handle or for service combobox selected index changing
        //  PARAMETERS  : object sender, EventArgs e
        //  RETURNS     : void
        //
        private void ServicesCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            MethodCombobox.Items.Clear();
            if (ParametersDataGridView.Columns.Count > 0)
            {
                ParametersDataGridView.Columns.Clear();
            }
            if (ParametersDataGridView.Rows.Count > 0)
            {
                ParametersDataGridView.Rows.Clear();
            }

            // int selectionMade = 0;
            // selectionMade = ServicesCombobox.SelectedIndex;
            foreach (ServiceRecord record in serviceRecordList)
            {
                if (record.ServiceName.Equals(ServicesCombobox.SelectedItem.ToString()))
                {
                    if (!MethodCombobox.Items.Contains(record.MethodName))
                    {
                        MethodCombobox.Items.Add(record.MethodName);
                    }
                }
            }
        }

        //
        //  METHOD      : MethodCombobox_SelectedIndexChanged
        //  DESCRIPTION : event handle for method combobox selected index changing
        //  PARAMETERS  : object sender, EventArgs 
        //  RETURNS     : void
        //
        private void MethodCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            myDataTable.Columns.Clear();
            myDataTable.Clear();

            //create and add columns to gridview
            DataColumn dataColumnName = new DataColumn("Name");
            DataColumn dataColumnType = new DataColumn("Type");
            DataColumn dataColumnValue = new DataColumn("Value");

            myDataTable.Columns.Add(dataColumnName);
            myDataTable.Columns.Add(dataColumnType);
            myDataTable.Columns.Add(dataColumnValue);


            int parameterCount = 0;

            int selectedIndex;
            selectedIndex = MethodCombobox.SelectedIndex;
            Dictionary<string, string> myDictionary = new Dictionary<string, string>();

            //used to evaluate parameters of the selected method,
            //if any exist seed the values into the gridview
            foreach (ServiceRecord record in serviceRecordList)
            {
                if (record.MethodName.Equals(MethodCombobox.Items[selectedIndex].ToString()))
                {
                    myDictionary = record.GetParameters();

                    parameterCount = myDictionary.Count();

                    myDataTable.Columns[0].ReadOnly = true;
                    myDataTable.Columns[1].ReadOnly = true;

                    foreach (KeyValuePair<string, string> dictionaryRecord in myDictionary)
                    {
                        string typeBuffer = dictionaryRecord.Value;
                        int index = typeBuffer.IndexOf(';');
                        if (index != -1)
                        {
                            typeBuffer = typeBuffer.Remove(index, 1);
                        }
                        //typeBuffer = typeBuffer.Remove(typeBuffer.Length - 1, 1);
                        if (!(dictionaryRecord.Key.Contains("null")))
                        {
                             myDataTable.Rows.Add(dictionaryRecord.Key, typeBuffer);
                        }
                        else
                        {
                             myDataTable.Columns.Clear();
                        }
                    }
                }
            }
        }

        //
        //  METHOD      : requestButton_Click
        //  DESCRIPTION : event handle for the requestbutton widget.
        //  PARAMETERS  : object sender, EventArgs e
        //  RETURNS     : void
        //
        private void requestButton_Click(object sender, EventArgs e)
        {
            bool serviceSelected = false;
            bool methodSelected = false;
            bool parametersValid = false;

            if (ServicesCombobox.SelectedIndex != -1) //check a service has been selected
            {
                serviceSelected = true;

                if ((serviceSelected) && (MethodCombobox.SelectedIndex != -1)) //check a method has been selected
                {
                    methodSelected = true;
                    if (ParametersDataGridView.Rows.Count > 0) //check if this methods has parameters
                    {
                        for (int i = 0; i < ParametersDataGridView.Rows.Count; i++) //if it does validate those parameters
                        {
                            if (validateParameters(ParametersDataGridView.Rows[i].Cells[2].Value.ToString(), ParametersDataGridView.Rows[i].Cells[1].Value.ToString()))
                            {
                                parametersValid = true;
                            }
                            else
                            {
                                parametersValid = false;
                                break;
                            }
                        }
                    }
                    else
                    {
                        parametersValid = true;
                    }
                    // validateParameters
                }
                else
                {
                    MessageBox.Show("Please Select a Method!");// error message
                }
            }
            else
            {
                MessageBox.Show("Please Select a Service!"); //error message
            }

            if ((!parametersValid) && (serviceSelected) && (methodSelected))
            {
                MessageBox.Show("Please provided values in the parameters that correspond with the requested type!"); //erro message
            }

            if (parametersValid && serviceSelected && methodSelected)
            {
                ServiceHandle();//if everything is good generate the SOAP request.
            }
        }

        //
        //  METHOD      : ServiceHandle
        //  DESCRIPTION : Create's, sends, and recieves values to a Service via the SOAP messaging protocol.
        //  PARAMETERS  : n/a
        //  RETURNS     : void
        //
        public void ServiceHandle()
        {
            //retrieve user selections
            int serviceIndex = ServicesCombobox.SelectedIndex;
            int methodIndex = MethodCombobox.SelectedIndex;
            string serviceName = ServicesCombobox.Items[serviceIndex].ToString();
            string methodName = MethodCombobox.Items[methodIndex].ToString();

            string soapBodyXML;

            //iterate through exiting records.
            foreach (ServiceRecord record in serviceRecordList)
            {
                //match user request with a service record
                if ((record.ServiceName.Equals(serviceName)) && (record.MethodName.Equals(methodName)))
                {
                    //create the SOAP header
                    HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(record.HostHeader);
                    webRequest.Headers.Add(@"SOAP:Action");
                    webRequest.ContentType = "text/xml;charset=\"utf-8\"";
                    webRequest.Accept = "text/xml";
                    webRequest.Method = "POST";

                    string parameters = "";

                    //create the SOAP body
                    for (int i = 0; i < ParametersDataGridView.Rows.Count; i++)
                    {
                        parameters = parameters + String.Format(@"<{0}>{1}</{0}>", ParametersDataGridView.Rows[i].Cells[0].Value.ToString(), ParametersDataGridView.Rows[i].Cells[2].Value.ToString());
                    }

                    //doesn't handle no parameter methods
                    soapBodyXML = String.Format(@"<?xml version=""1.0"" encoding=""utf-8""?>
                    <soap:Envelope xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
                    <soap:Body>
                    <{0} xmlns=""{1}"">
                    {2}
                    </{0}>
                    </soap:Body>
                    </soap:Envelope>", methodName, record.MethodAddress, parameters);

                    //load the SOAP string into a XML file
                    XmlDocument soapEnvelopeXml = new XmlDocument();
                    soapEnvelopeXml.LoadXml(soapBodyXML);

                    using (Stream stream = webRequest.GetRequestStream()) //request a stream
                    {
                        soapEnvelopeXml.Save(stream);//send the soap request
                    }

                    using (WebResponse response = webRequest.GetResponse()) //get the service response
                    {

                        using (StreamReader rd = new StreamReader(response.GetResponseStream())) //use the response stream
                        {
                            string soapResult = rd.ReadToEnd();//read in the response to a string

                            XmlDocument result = new XmlDocument(); //create an xml file as a buffer for the response
                            soapResult = WebUtility.HtmlDecode(soapResult);//decode the html in the payload back into XML
                            result.LoadXml(soapResult);//load the reponse into XML

                           
                            //check for an empty response
                            XmlNodeList checkEmpty = result.GetElementsByTagName("NewDataSet");
                            if (checkEmpty[0].HasChildNodes)
                            {
                                //if the response isn't empty
                                //read the response into a dataset
                                XmlNodeReader xmlReader = new XmlNodeReader(result);
                                DataSet dataSet = new DataSet();
                                dataSet.ReadXml(xmlReader);


                                int whichTable = dataSet.Tables.Count;
                                //use the dataset to filter out the dupliate table in the response
                                resultTable = dataSet.Tables[whichTable - 1].DefaultView.ToTable(true);

                                //pass the respond now formated into a table into the resultsForm  form object.
                                resultsForm showResults = new resultsForm(resultTable);
                                //display the results
                                showResults.ShowDialog();
                                resultTable.Clear();
                            }
                            else
                            {
                                //if the response is empty show this
                                MessageBox.Show("Returned an Empty Response. Parameter values are likely incorrect. Please try again!");
                            }
                        }
                    }
                }
            }
        }

        //
        //  METHOD      : validateParameters
        //  DESCRIPTION : validate parameters based on their types
        //  PARAMETERS  : string value, string typeExpected
        //  RETURNS     : bool allGood
        //
        private bool validateParameters(string value, string typeExpected)
        {
            bool allGood = false;

            if (typeExpected.Contains("string"))
            {
                if (value.Length > 0)
                {
                    allGood = true;
                }
            }
            else
            {
                allGood = false;
                MessageBox.Show("I havn't handled this yet!");
            }

            return allGood;        
        }
    }
}
