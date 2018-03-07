using FPDL.Common;
using FPDL.Pattern;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
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
            /// Invalid (null value)
            /// </summary>
            INVALID,
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
        public ModuleOsp()
        {
            Path = "";
            Protocol = OspProtocol.INVALID;
            InputPort = "";
            OutputPort = "";
        }
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
                InputPort = (String)fpdl.Element("inputPort") ?? "";
                OutputPort = (String)fpdl.Element("outputPort") ?? "";
                //if (fpdl.Element("inputPort") != null)
                //    InputPort = fpdl.Element("inputPort").Value;
                //if (fpdl.Element("outputPort") != null)
                //    OutputPort = fpdl.Element("outputPort").Value;
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
        public XElement ToFPDL(XNamespace ns)
        {
            XElement fpdl = new XElement(ns + "osp",
                new XElement(ns + "path", Path),
                new XElement(ns + "protocol", Protocol.ToString())
            );
            if (InputPort != "")
                fpdl.Add(new XElement(ns + "inputPort", InputPort));
            if (OutputPort != "")
                fpdl.Add(new XElement(ns + "outputPort", OutputPort));
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
        /// <summary>
        /// Get a TreeNode
        /// </summary>
        /// <returns></returns>
        public TreeNode GetNode()
        {
            TreeNode[] t = new TreeNode[4];
            t[0] = new TreeNode("Path = " + Path);
            t[0].Tag = new Specification { ParamName = "path", Value = Path };
            t[0].ToolTipText = "Path (export/import)";
            t[1] = new TreeNode("Protocol = " + Protocol.ToString());
            t[1].Tag = new Specification { ParamName = "protocol", Value = Protocol.ToString() };
            t[1].ToolTipText = "OSP protocol";
            t[2] = new TreeNode("Input = " + InputPort);
            t[2].Tag = new Specification { ParamName = "inputPort", Value = InputPort };
            t[2].ToolTipText = "Input Address:Port (optional)";
            t[3] = new TreeNode("Output = " + OutputPort);
            t[3].Tag = new Specification { ParamName = "outputPort", Value = OutputPort };
            t[3].ToolTipText = "Output Address:Port (optional)";

            TreeNode a = new TreeNode("OSP", t);
            a.ToolTipText = "OSP module";
            a.Tag = this;
            return a;
        }
        /// <summary>
        /// Apply specifications from a Pattern to this module
        /// </summary>
        /// <param name="specifications"></param>
        public void ApplyPattern(List<Specification> specifications)
        {
            foreach (Specification spec in specifications)
                ApplyPattern(spec);
        }
        /// <summary>
        /// Apply specifications from a Pattern to this module
        /// </summary>
        /// <param name="specification"></param>
        public void ApplyPattern(Specification specification)
        {
            switch (specification.ParamName)
            {
                case "path":
                    Path = specification.Value;
                    break;
                case "protocol":
                    Protocol = (OspProtocol)Enum.Parse(typeof(OspProtocol), specification.Value);
                    break;
                case "inputPort":
                    InputPort = specification.Value;
                    break;
                case "outputPort":
                    OutputPort = specification.Value;
                    break;
            }
        }
    }
}

