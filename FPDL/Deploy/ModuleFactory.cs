using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace FPDL.Deploy
{
    /// <summary>
    /// Create a concrete Module instance
    /// </summary>
    class ModuleFactory
    {
        /// <summary>
        /// Create a concrete Module instance
        /// </summary>
        /// <param name="fpdl"></param>
        /// <param name="component"></param>
        public static void Create(XElement fpdl, Component component)
        {
            // Peek inside the fpdl to determine which Module classes to instantiate
            if (fpdl.Name != "component")
                throw new ApplicationException("Cannot parse: Not an FPDL component description");

            IEnumerable<XElement> foo = fpdl.Elements();

            foreach (XElement module in fpdl.Elements())
            {
                switch (module.Name.LocalName)
                {
                    case "export":
                        component.Modules.Add(new ModuleExport(module));
                        break;
                    case "import":
                        component.Modules.Add(new ModuleImport(module));
                        break;
                    case "osp":
                        component.Modules.Add(new ModuleOsp(module));
                        break;
                    case "interface":
                        component.Modules.Add(new ModuleInterface(module));
                        break;
                    default:
                        // Should throw an exception
                        continue;
                }
            }
        }
    }
}
