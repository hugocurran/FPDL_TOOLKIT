using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace FPDL.Deploy
{

    /// <summary>
    /// Deploy Component
    /// </summary>
    public class Component
    {
        /// <summary>
        /// Component type
        /// </summary>
        public enum Type
        {
            /// <summary>
            /// Proxy
            /// </summary>
            Proxy,
            /// <summary>
            /// Guard
            /// </summary>
            Guard,
            /// <summary>
            /// Filter
            /// </summary>
            Filter
        }
        /// <summary>
        /// Modules
        /// </summary>
        public List<IModule> Modules = new List<IModule>();
        /// <summary>
        /// Component ID
        /// </summary>
        public Guid ComponentID;
        /// <summary>
        /// Component Type
        /// </summary>
        public Type ComponentType;
        /// <summary>
        /// Construct Component object
        /// </summary>
        public Component() { }
        /// <summary>
        /// Construct Component object from FPDL
        /// </summary>
        /// <param name="fpdl"></param>
        public Component(XElement fpdl)
        {
            FromFPDL(fpdl);
        }
        /// <summary>
        /// Deserialise Component from FPDL
        /// </summary>
        /// <param name="fpdl"></param>
        public void FromFPDL(XElement fpdl)
        {
            if (fpdl.Name != "component")
                throw new ApplicationException("Cannot parse: Not an FPDL component description");
            try
            {
                ComponentID = Guid.Parse(fpdl.Attribute("componentID").Value);
                switch (fpdl.Attribute("componentType").Value)
                {
                    case "Proxy":
                        ComponentType = Type.Proxy;
                        break;
                    case "Guard":
                        ComponentType = Type.Guard;
                        break;
                    case "Filter":
                        ComponentType = Type.Filter;
                        break;
                    default:
                        throw new ApplicationException("Component parse error: Invalid componentType");
                }
                ModuleFactory.Create(fpdl, this);
            }
            catch (NullReferenceException e)
            {
                throw new ApplicationException("Component parse error: " + e.Message);
            }
        }
        /// <summary>
        /// Serialise Component to FPDL
        /// </summary>
        /// <returns></returns>
        public XElement ToFPDL()
        {
            XElement fpdl = new XElement("component",
                    new XAttribute("componentID", ComponentID.ToString()),
                    new XAttribute("componentType", ComponentType.ToString())
                    );

            foreach (IModule module in Modules)
            {
                fpdl.Add(module.ToFPDL());
            }
            return fpdl;
        }
        /// <summary>
        /// String representation of Component
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder str = new StringBuilder("=======\nComponent ");
            str.AppendFormat("({0}):\n", ComponentID);
            str.AppendFormat("  Type: {0}\n", ComponentType);
            foreach (IModule mod in Modules)
                str.AppendFormat("  {0}\n", mod.ToString());
            return str.ToString();
        }
    }
}

