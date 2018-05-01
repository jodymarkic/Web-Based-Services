/*
 *  FILENAME        : Interpreter.cs
 *  PROJECT         : SOA_A01
 *  PROGRAMMER      : Jody Markic
 *  FIRST VERSION   : 10/1/2017
 *  DESCRIPTION     : This file holds the Class Interpreter that is used to parse wsdl's and read and write config files
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.Windows.Forms;

namespace SOA_A01_Test
{
    //
    //  CLASS: Interpreter
    //  DESCRIPTION: This Class represents a interpreter that parses wsdl's for data required
    //               to build a soap request that accesses a serivce. It also handles all
    //               read and writes to the config files
    //
    //
    class Interpreter
    {
        //local parameter
        List<ServiceRecord> configRecords = new List<ServiceRecord>();

        //
        //  METHOD      : WriteToConfig
        //  DESCRIPTION : Write's the data gleamed out of ParseWSDL to a config file
        //  PARAMETERS  : n/a
        //  RETURNS     : Dictionary<string,string> parameters
        //
        public void WriteToConfig()
        {
            string recordString = "";
            foreach (ServiceRecord record in configRecords) //for each service method stored 
            {
                //format the data into a string
                recordString = recordString + String.Format("{0},{1},{2},{3}", record.HostHeader, record.MethodAddress, record.MethodName, record.ServiceName);
                foreach (KeyValuePair<string, string> parameters in record.GetParameters())
                {
                    recordString = recordString + "," + parameters.Key + "-" + parameters.Value;
                }
                recordString = recordString + ";\n";
            }
            //remove the final newline
            recordString = recordString.Remove(recordString.Length - 1, 1);
            //write all entries to the ServiceRecordConfig file
            File.WriteAllText(@"..\..\ServiceRecordConfig.txt", recordString);
        }

        //
        //  METHOD      : ParseWSDL
        //  DESCRIPTION : Read the WSDL's contents, parse out data used in building a SOAP request to the service.
        //  PARAMETERS  : n/a
        //  RETURNS     : Dictionary<string,string> parameters
        //
        public void ParseWSDL(string filepath)
        {
            string parameterName = "";
            string parameterType= "";

            bool methodResponse = false;

            Dictionary<string, string> parameterListBuffer = new Dictionary<string, string>();

            //load a wsdl
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filepath);    

            //find a parent node to all methods of the service
            XmlNodeList mySchema = xmlDoc.GetElementsByTagName("s:schema");
            XmlNodeList schemaChildNodes = mySchema[0].ChildNodes; //get childnodes of the schema tag
            foreach (XmlNode childnode in schemaChildNodes) // iterate through each childnode
            {
                if (childnode.HasChildNodes) //check if this tag has children
                {
                    ServiceRecord serviceRecord = new ServiceRecord(); //create a storage object

                    
                    XmlAttributeCollection myMethodAttributes = childnode.Attributes; //create a attribute collection so we can check attribute names of tags

                    foreach (XmlAttribute attribute in myMethodAttributes) //iterate through each attribute
                    {
                        if (attribute.Name == "name") //look for these specific tags and retrieve their values.
                        {
                            if (!attribute.Value.ToString().Contains("Response"))
                            {
                                methodResponse = false;
                                serviceRecord.MethodName = attribute.Value.ToString();
                            }//find the value of the targetNamespace
                            else
                            {
                                methodResponse = true;
                            }
                        }
                    }
                    if (!methodResponse) //if we haven't got a response method.
                    {
                        //originally outside the record loop
                        XmlAttributeCollection targetNamespace = mySchema[0].Attributes; //get attributes of schema
                       // THIS GETS OUR TARGET NAMESPACE
                        foreach (XmlAttribute attribute in targetNamespace) //iterate through schema attributes
                        {
                            if (attribute.Name == "targetNamespace") //find the attribute targetNamespace
                            {
                                serviceRecord.MethodAddress = attribute.Value.ToString();//find the value of the targetNamespace
                            }
                        }

                        //get our service
                        XmlNodeList myService = xmlDoc.GetElementsByTagName("wsdl:service");
                        XmlAttributeCollection myServiceName = myService[0].Attributes;
                        foreach (XmlAttribute serviceAttribute in myServiceName)
                        {
                            if (serviceAttribute.Name == "name")
                            {
                                serviceRecord.ServiceName = serviceAttribute.Value.ToString();
                            }
                        }
                        //get our soad address
                        XmlNodeList myAddress = xmlDoc.GetElementsByTagName("soap:address");
                        XmlAttributeCollection myAddressAttributes = myAddress[0].Attributes;
                        foreach (XmlAttribute attribute in myAddressAttributes)
                        {
                            if (attribute.Name == "location")
                            {
                                serviceRecord.HostHeader = attribute.Value.ToString();
                            }
                        }//end of outside

                        //this is for parameters
                        if (childnode.HasChildNodes)
                        {
                            if (childnode.ChildNodes[0].HasChildNodes)
                            {
                                XmlNodeList parameterNodes = childnode.ChildNodes[0].ChildNodes[0].ChildNodes;

                                foreach (XmlNode parameters in parameterNodes)
                                {
                                    XmlAttributeCollection myParameterAttributes = parameters.Attributes;
                                    foreach (XmlNode parameterAttributes in myParameterAttributes)
                                    {
                                        if (parameterAttributes.Name == "name")
                                        {
                                            parameterName = parameterAttributes.Value.ToString();
                                        }
                                        if (parameterAttributes.Name == "type")
                                        {
                                            parameterType = parameterAttributes.Value.ToString();
                                        }
                                        if ((parameterName.Length > 0) && (parameterType.Length > 0))
                                        {
                                            parameterListBuffer.Add(parameterName, parameterType);
                                            parameterName = "";
                                            parameterType = "";
                                            //add to service record and remove values from these variables

                                        }
                                    }
                                    //add the parameters to a dictionary
                                    serviceRecord.SetParameters(parameterListBuffer);
                                }
                                parameterListBuffer.Clear();
                            }
                            else
                            {
                                parameterListBuffer.Add("null", "null");
                                //add parameters to a record
                                serviceRecord.SetParameters(parameterListBuffer);
                                parameterListBuffer.Clear();
                            }
                        }
                        configRecords.Add(serviceRecord); //add record to a list
                    }
                }             
            }
        }


        //
        //  METHOD      : ReadConfig
        //  DESCRIPTION : read the WSDL links stored in the WSDLConfigList and runs ParseWSDL on them
        //  PARAMETERS  : n/a
        //  RETURNS     : Dictionary<string,string> parameters
        //
        public void ReadConfig()
        {
            //read the wsdl config file
            using (StreamReader reader = new StreamReader(@"..\..\WSDLConfigList.txt"))
            {
                string servicehost;
                //read all wsdl files
                while ((servicehost = reader.ReadLine()) != null)
                {
                    ParseWSDL(servicehost);
                }
            }
        }
    }
}
