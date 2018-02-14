using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace FPDL.Deploy
{
    /// <summary>
    /// Factory for System objects
    /// </summary>
    public class SystemFactory
    {
        /// <summary>
        /// Construct a System Factory
        /// </summary>
        public SystemFactory() { }

        /// <summary>
        /// Populate a List of systems
        /// </summary>
        /// <param name="systems">List to populate</param>
        /// <param name="fpdl">FPDL Deploy document</param>
        public SystemFactory(List<DeploySystem> systems, XElement fpdl)
        {
            if (fpdl.Name != "Deploy")
                throw new ApplicationException("Cannot parse: Not an FPDL Deploy file");
            try
            {
                foreach (XElement system in fpdl.Elements("system"))
                    systems.Add(new DeploySystem(system));
            }
            catch (Exception e)
            {
                throw new ApplicationException("System parse error: " + e.Message);
            }
        }

        //public XElement ToXML()
        //{
        //    XElement fpdl = new XElement("Deploy",
        //        new XElement("configMgmt", Pattern,
        //            new XAttribute("patternReference", PatternRef.ToString())
        //        )
        //    );
        //    foreach (Component component in Components)
        //    {
        //        fpdl.Add(component.ToFPDL());
        //    }
        //    return fpdl;
        //}
    }
}
