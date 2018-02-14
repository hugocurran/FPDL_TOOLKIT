using FPDL.Common;
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
        /// Modules
        /// </summary>
        public List<IModule> Modules;
        /// <summary>
        /// Component ID
        /// </summary>
        public Guid ComponentID;
        /// <summary>
        /// Component Type
        /// </summary>
        public Enums.ComponentType ComponentType;
        /// <summary>
        /// Construct Component object
        /// </summary>
        public Component()
        {
            Modules = new List<IModule>();
        }
        /// <summary>
        /// Construct Component object from FPDL
        /// </summary>
        /// <param name="fpdl"></param>
        public Component(XElement fpdl) : this()
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
                ComponentType = (Enums.ComponentType)Enum.Parse(typeof(Enums.ComponentType), fpdl.Attribute("componentType").Value);
                ModuleFactory.Create(fpdl, this);
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
        /// Serialise Component to FPDL
        /// </summary>
        /// <returns></returns>
        public XElement ToFPDL()
        {
            XElement fpdl = new XElement("component",
                    new XElement("componentID", ComponentID.ToString()),
                    new XElement("componentType", ComponentType.ToString())
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

