/*
 *  FILENAME        : ServiceRecord.cs
 *  PROJECT         : SOA_A01
 *  PROGRAMMER      : Jody Markic
 *  FIRST VERSION   : 10/1/2017
 *  DESCRIPTION     : This file holds the Class Service Record. A in memory representation of the data
 *                    required to build a soap request
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOA_A01_Test
{
    //
    //  CLASS: ServiceRecord
    //  DESCRIPTION: This Class represents a container object to host all required data to build a soap request.
    //               it used a intermediary transport between reading the wsdl and writing to the config file.
    //               also reused when reading the config file and seeding UI/ writing soap request.
    //
    //
    class ServiceRecord
    {
        //local variables
        private string asmx;
        private string hostHeader;
        private string methodAddress;
        private string methodName;
        private string serviceName;
        private string hostBody;
        private Dictionary<string, string> parameters;

        //properties to local variables
        public string Asmx { get { return asmx; } set {asmx = value; } }
        public string HostHeader { get { return hostHeader; } set { hostHeader = value; } }
        public string MethodAddress { get { return methodAddress; } set { methodAddress = value; } }
        public string MethodName { get { return methodName; } set { methodName = value; } }
        public string ServiceName { get { return serviceName; } set { serviceName = value; } }
        public string HostBody { get { return hostBody; } set { hostBody = value; } }

        //
        //  METHOD      : GetParameters
        //  DESCRIPTION : Accessor to the dictionary parameters
        //  PARAMETERS  : n/a
        //  RETURNS     : Dictionary<string,string> parameters
        //
        public Dictionary<string, string> GetParameters()
        {
            return parameters;
        }

        //
        //  METHOD      : SetParameters
        //  DESCRIPTION : Mutator to the dictionary parameters
        //  PARAMETERS  : Dictionary<string, string> newDictionary
        //  RETURNS     : void
        //
        public void SetParameters(Dictionary<string, string> newDictionary)
        {
            parameters = new Dictionary<string, string>(newDictionary);
        }
    }
}
