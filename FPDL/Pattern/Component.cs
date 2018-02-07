using FPDL.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace FPDL.Pattern
{
    /// <summary>
    /// Component
    /// </summary>
    public class Component
    {
        /// <summary>
        /// Component type
        /// </summary>
        public Enums.ComponentType ComponentType;
        /// <summary>
        /// Component ID
        /// </summary>
        public Guid ComponentID;
        /// <summary>
        /// Modules
        /// </summary>
        public List<Module> Modules = new List<Module>();
        /// <summary>
        /// Construct Component object from FPDL
        /// </summary>
        /// <param name="fpdl"></param>
        public Component(XElement fpdl)
        {
            FromFPDL(fpdl);
        }
        /// <summary>
        /// Deserialise Component object from FPDL
        /// </summary>
        /// <param name="fpdl"></param>
        public void FromFPDL(XElement fpdl)
        {
            if (fpdl.Name != "component")
                throw new ApplicationException("Cannot parse: Not an FPDL component description");
            try
            {
                ComponentType = (Enums.ComponentType)Enum.Parse(typeof(Enums.ComponentType), fpdl.Element("componentType").Value);
                ComponentID = Guid.Parse(fpdl.Element("componentID").Value);
                foreach (XElement module in fpdl.Descendants("module"))
                    Modules.Add(new Module(module));
            }
            catch (ArgumentException e)
            {
                throw new ApplicationException("Invalid component type: " + e.Message);
            }
            catch (NullReferenceException e)
            {
                throw new ApplicationException("Component parse error: " + e.Message);
            }
        }
        /// <summary>
        /// Serialise Component object to FPDL
        /// </summary>
        /// <returns></returns>
        public XElement ToFPDL()
        {
            XElement fpdl = new XElement("component",
                new XElement("componentType", ComponentType.ToString()),
                new XElement("componentID", ComponentID.ToString())
                );
            foreach (Module module in Modules)
                fpdl.Add(module.ToFPDL());
            return fpdl;
        }
        /// <summary>
        /// String representation of Component object
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder str = new StringBuilder("component\n");
            str.AppendFormat("  Type: {0} ({1})\n", ComponentType, ComponentID);
            foreach (Module module in Modules)
                str.Append(module.ToString());
            return str.ToString();
        }
    }
}
