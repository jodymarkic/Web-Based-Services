/*
 *  FILENAME        : Program.cs
 *  PROJECT         : SOA_A01
 *  PROGRAMMER      : Jody Markic
 *  FIRST VERSION   : 10/1/2017
 *  DESCRIPTION     : This file holds the Main entry point into the program SOA_A01_Test.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SOA_A01_Test
{
    //
    //  CLASS: Program
    //  DESCRIPTION: This class holds the main entry point into the SOA_A01_Test solution.
    //
    //
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new myForm());
        }
    }
}
