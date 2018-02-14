using FPDL.Common;
using System;
using System.Text;
using System.Xml.Linq;
using static FPDL.Common.Enums;

namespace FPDL.Deploy
{
    /// <summary>
    /// OSP module
    /// </summary>
    public class ModuleOsp : IModule
    {
        /// <summary>
        /// OspProtocol types
        /// </summary>
        public enum OspProtocol
        {
            /// <summary>
            /// HPSD over ZMQ
            /// </summary>
            HPSD_ZMQ,
            /// <summary>
            /// HPSD over TCP
            /// </summary>
            HPSD_TCP,
            /// <summary>
            /// WebLVC over ZMQ
            /// </summary>
            WebLVC_ZMQ,
            /// <summary>
            /// WebLVC over TCP
            /// </summary>
            WebLVC_TCP
        }
        /// <summary>
        /// Direction of information flow
        /// </summary>
        [DeployIf("path", "Direction of this OSP configuration")]
        public string Path { get; set; }
        /// <summary>
        /// OSP protocol
        /// </summary>
        [DeployIf("protocol", "Messaging protocol in use")]
        public OspProtocol Protocol { get; set; }
        /// <summary>
        /// Input port (optional)
        /// </summary>
        [DeployIf("inputPort", "Address:Port for input", true)]
        public string InputPort { get; set; }
        /// <summary>
        /// Output port (optional)
        /// </summary>
        [DeployIf("outputPort", "Address:Port for output", true)]
        public string OutputPort { get; set; }

        /// <summary>
        /// Get the module identity
        /// </summary>
        public ModuleType GetModuleType()
        {
            return ModuleType.osp;
        }

        /// <summary>
        /// Construct OSP module
        /// </summary>
        public ModuleOsp() { }
        /// <summary>
        /// Construct OSP module from FPDL
        /// </summary>
        /// <param name="fpdl"></param>
        public ModuleOsp(XElement fpdl)
        {
            FromFPDL(fpdl);
        }
        /// <summary>
        /// Deserialise OSP module from FPDL
        /// </summary>
        /// <param name="fpdl"></param>
        public void FromFPDL(XElement fpdl)
        {
            if (fpdl.Name != "osp")
                throw new ApplicationException("Cannot parse: Not an FPDL OSP description");
            try
            {
                Path = fpdl.Element("path").Value;
                Protocol = (OspProtocol)Enum.Parse(typeof(OspProtocol), fpdl.Element("protocol").Value);
                if (fpdl.Element("inputPort") != null)
                    InputPort = fpdl.Element("inputPort").Value;
                if (fpdl.Element("outputPort") != null)
                    OutputPort = fpdl.Element("outputPort").Value;
            }
            catch (ArgumentException e)
            {
                throw new ApplicationException("Invalid OSP protocol: " + e.Message);
            }
            catch (NullReferenceException e)
            {
                throw new ApplicationException("Cannot parse: " + e.Message);
            }
        }
        /// <summary>
        /// Serialise OSP module to FPDL
        /// </summary>
        /// <returns></returns>
        public XElement ToFPDL()
        {
            XElement fpdl = new XElement("osp",
                new XElement("path", Path),
                new XElement("protocol", Protocol.ToString())
            );
            if (InputPort != "")
                fpdl.Add(new XElement("inputPort", InputPort));
            if (OutputPort != "")
                fpdl.Add(new XElement("outputPort", OutputPort));
            return fpdl;
        }
        /// <summary>
        /// String representation of OSP module
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder str = new StringBuilder("  OSP:\n");
            str.AppendFormat("\tPath: {0}\n", Path);
            str.AppendFormat("\tProtocol: {0}\n", Protocol);
            if (InputPort != "")
                str.AppendFormat("\tInput: {0}\n", InputPort);
            if (OutputPort != "")
                str.AppendFormat("\tOutput: {0}\n", OutputPort);
            return str.ToString();
        }
    }
}

