/*
 *  FILENAME        : resultForm.cs
 *  PROJECT         : SOA_A01
 *  PROGRAMMER      : Jody Markic
 *  FIRST VERSION   : 10/1/2017
 *  DESCRIPTION     : This file hold the code behind the resultForm used to display a SOAP response data.
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SOA_A01_Test
{
    //
    //  CLASS: resultForm : Form
    //  DESCRIPTION: This Class just act as an outlet to display a successful SOAP response's data.
    //
    //
    public partial class resultsForm : Form
    {
        //
        //  METHOD      :resultsForm
        //  DESCRIPTION : Constructor
        //  PARAMETERS  : DataTable resultsTable
        //  RETURNS     : n/a
        //
        public resultsForm(DataTable resultsTable)
        {
            InitializeComponent();

            //connect the table provided to the UI's datagridview.
            resultsDataGridView.DataSource = resultsTable;
        }
    }
}
