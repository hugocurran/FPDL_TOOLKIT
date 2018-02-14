using FPDL.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using static FPDL.Common.Enums;

namespace FPDL.Deploy
{
    /// <summary>
    /// Host Module
    /// </summary>
    public class ModuleHost : IModule
    {
        /// <summary>
        /// Host name
        /// </summary>
        [DeployIf("hostName", "Name for the host")]
        public string HostName { get; set; }
        /// <summary>
        /// Logging servers
        /// </summary>
        [DeployIf("LOGGING", "Logging servers", true, true )]
        public List<Server> Logging { get; set; }
        /// <summary>
        /// Time servers
        /// </summary>
        [DeployIf("TIME", "Time servers", true, true)]
        public List<Server> Time { get; set; }

        /// <summary>
        /// Get the module identity
        /// </summary>
        public ModuleType GetModuleType()
        {
            return ModuleType.host;
        }

        /// <summary>
        /// Construct a Host Module
        /// </summary>
        public ModuleHost()
        {
            Logging = new List<Server>();
            Time = new List<Server>();
        }
        /// <summary>
        /// Construct an Interface module from FPDL
        /// </summary>
        /// <param name="fpdl"></param>
        public ModuleHost(XElement fpdl) : this()
        {
            FromFPDL(fpdl);
        }
        /// <summary>
        /// Deserialise Interface module from FPDL
        /// </summary>
        /// <param name="fpdl"></param>
        public void FromFPDL(XElement fpdl)
        {
            if (fpdl.Name != "host")
                throw new ApplicationException("Cannot parse: Not an FPDL host description");
            try
            {
                HostName = fpdl.Element("hostName").Value;
                if (fpdl.Element("logging") != null)
                {
                    foreach (XElement server in fpdl.Elements("server"))
                    {
                        Server serv = new Server
                        {
                            Name = server.Value,
                            Protocol = server.Attribute("protocol").Value,
                            Transport = server.Attribute("transport").Value,
                            Port = server.Attribute("port").Value
                        };
                        Logging.Add(serv);
                    }
                }
                if (fpdl.Element("time") != null)
                {
                    foreach (XElement server in fpdl.Elements("server"))
                    {
                        Server serv = new Server
                        {
                            Name = server.Value,
                            Protocol = server.Attribute("protocol").Value,
                            Transport = server.Attribute("transport").Value,
                            Port = server.Attribute("port").Value
                        };
                        Logging.Add(serv);
                    }
                }
            }
            catch (NullReferenceException e)
            {
                throw new ApplicationException("Cannot parse: " + e.Message);
            }
        }
        /// <summary>
        /// Serialise Host module to FPDL
        /// </summary>
        /// <returns></returns>
        public XElement ToFPDL()
        {
            XElement fpdl = new XElement("host",
                new XElement("hostName", HostName)
            );
            if (Logging.Count > 0)
            {
                foreach (Server serv in Logging)
                    fpdl.Add(new XElement("server", serv.Name,
                        new XAttribute("protocol", serv.Protocol),
                        new XAttribute("transport", serv.Transport),
                        new XAttribute("port", serv.Port))
                        );
            }
            if (Time.Count > 0)
            {
                foreach (Server serv in Time)
                    fpdl.Add(new XElement("server", serv.Name,
                        new XAttribute("protocol", serv.Protocol),
                        new XAttribute("transport", serv.Transport),
                        new XAttribute("port", serv.Port))
                        );
            }
            return fpdl;
        }
        /// <summary>
        /// String representation of Host module
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder str = new StringBuilder("  Host:\n");
            str.AppendFormat("\tHost Name: {0}\n", HostName);
            foreach (Server serv in Logging)
                str.AppendFormat("\tLog: {0} - {1} - {2} Port {3}\n", serv.Name, serv.Protocol, serv.Transport, serv.Port);
            foreach (Server serv in Time)
                str.AppendFormat("\tTime: {0} - {1} - {2} Port {3}\n", serv.Name, serv.Protocol, serv.Transport, serv.Port);
            return str.ToString();
        }
    }
    /// <summary>
    /// Generic Server description
    /// </summary>
    public class Server
    {
        /// <summary>
        /// FQDN or IP address of server
        /// </summary>
        public string Name;
        /// <summary>
        /// Application protocol (eg syslog, NTP, etc
        /// </summary>
        public string Protocol;
        /// <summary>
        /// Transport protocol (TCP, UDP, etc)
        /// </summary>
        public string Transport;
        /// <summary>
        /// Port number
        /// </summary>
        public string Port;
    }
}
