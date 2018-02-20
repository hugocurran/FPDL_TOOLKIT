using FPDL.Common;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace FPDL.Deploy
{
    /// <summary>
    /// Create a concrete Module instance
    /// </summary>
    public class ModuleFactory
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
                    case "host":
                        component.Modules.Add(new ModuleHost(module));
                        break;
                    case "federation":
                        component.Modules.Add(new ModuleFederation(module));
                        break;
                    case "extension":
                        component.Modules.Add(new ModuleExtension(module));
                        break;
                    default:
                        // Should throw an exception
                        continue;
                }
            }
        }
        /// <summary>
        /// Create a concrete Module instance
        /// </summary>
        /// <param name="moduleName"></param>
        /// <param name="component"></param>
        public static void Create(Enums.ModuleType moduleName, Component component)
        {
            switch (moduleName.ToString())
            {
                case "export":
                    component.Modules.Add(new ModuleExport());
                    break;
                case "import":
                    component.Modules.Add(new ModuleImport());
                    break;
                case "osp":
                    component.Modules.Add(new ModuleOsp());
                    break;
                case "interface":
                    component.Modules.Add(new ModuleInterface());
                    break;
                case "host":
                    component.Modules.Add(new ModuleHost());
                    break;
                case "federation":
                    component.Modules.Add(new ModuleFederation());
                    break;
                case "extension":
                    component.Modules.Add(new ModuleExtension());
                    break;
                default:
                    // Should throw an exception
                    break;
            }
        }

        /// <summary>
        /// Create a concrete Module instance
        /// </summary>
        /// <param name="moduleName"></param>
        public static IModule Create(Enums.ModuleType moduleName)
        {
            switch (moduleName.ToString())
            {
                case "export":
                    return new ModuleExport();
                case "import":
                    return new ModuleImport();
                case "osp":
                    return new ModuleOsp();
                case "interface":
                    return new ModuleInterface();
                case "host":
                    return new ModuleHost();
                case "federation":
                    return new ModuleFederation();
                case "extension":
                    return new ModuleExtension();
                default:
                    return null;
            }
        }
    }
}


