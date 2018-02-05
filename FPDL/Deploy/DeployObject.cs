using FPDL.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace FPDL.Deploy
{
    /// <summary>
    /// FPDL Deploy
    /// </summary>
    public class DeployObject : IFpdlObject
    {
        /// <summary>
        /// ConfigMgmt for Deploy document
        /// </summary>
        public ConfigMgmt ConfigMgmt;
        /// <summary>
        /// Systems within Deploy document
        /// </summary>
        public List<System> Systems = new List<System>();
        /// <summary>
        /// Construct a DeployObject
        /// </summary>
        public DeployObject() { }

        /// <summary>
        /// Construct DeployObject from FPDL
        /// </summary>
        /// <param name="fpdl"></param>
        public DeployObject(XElement fpdl)
        {
            FromFPDL(fpdl);
        }
        /// <summary>
        /// Deserialise DeployObject from FPDL
        /// </summary>
        /// <param name="fpdl"></param>
        public void FromFPDL(XElement fpdl)
        {
            if (fpdl.Name != "Deploy")
                throw new ApplicationException("Cannot parse: Not an FPDL Deploy file");

            ConfigMgmt = new ConfigMgmt(fpdl.Descendants("configMgmt").FirstOrDefault());

            IEnumerable<XElement> systems = fpdl.Descendants("system");
            foreach (XElement system in systems)
            {
                Systems.Add(new System(system));
            }
        }
        /// <summary>
        /// Serialise DeployObject to FPDL
        /// </summary>
        /// <returns></returns>
        public XElement ToFPDL()
        {
            XElement fpdl = new XElement("Deploy");

            foreach(System system in Systems)
            {
                fpdl.Add(system.ToFPDL());
            }
            return fpdl;
        }
        /// <summary>
        /// String representation of DeployObject
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder str = new StringBuilder("DEPLOY =>\n");
            str.AppendFormat("{0}", ConfigMgmt);
            foreach (System sys in Systems)
                str.AppendFormat("{0}\n", sys);
            return str.ToString();
        }
    }
}
