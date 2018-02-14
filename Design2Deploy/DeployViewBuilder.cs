
using FPDL.Common;
using FPDL.Deploy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace FPDL.Tools.DeployEditor
{
    internal static class DeployViewBuilder
    {
        internal static TreeNode[] getSystems(List<DeploySystem> systems)
        {
            TreeNode[] sys = new TreeNode[systems.Count];
            for (int i = 0; i < systems.Count; i++)
            {
                string parent =
                    systems[i].SystemType.ToString().ToUpperFirst() + " (" +
                    systems[i].FederateName + ")";

                TreeNode t = new TreeNode(parent, getComponents(systems[i].Components));
                //t.ToolTipText = a.ToolTip;
                t.Tag = systems[i];
                sys[i] = t;
            }
            return sys;
        }

        internal static TreeNode[] getComponents(List<Component> components)
        {
            TreeNode[] comps = new TreeNode[components.Count];
            for (int i = 0; i < components.Count; i++)
            {
                string parent = 
                    components[i].ComponentType.ToString().ToUpperFirst() + " (" +
                    components[i].ComponentID.ToString() + ")";

                TreeNode t = new TreeNode(parent, getModules(components[i].Modules));
                //t.ToolTipText = a.ToolTip;
                t.Tag = components[i];
                comps[i] = t;
            }
            return comps;
        }

        internal static TreeNode[] getModules(List<IModule> modules)
        {
            TreeNode[] mods = new TreeNode[modules.Count];
            for (int i=0; i < modules.Count; i++)
            {
                string parent = modules[i].GetModuleType().ToString().ToUpperFirst();

                TreeNode t = new TreeNode(parent, getParameters(modules[i]));
                //t.ToolTipText = a.ToolTip;
                t.Tag = modules[i];
                mods[i] = t;
            }
            return mods;
        }


        internal static TreeNode[] getParameters(IModule module)
        {
            TreeNode[] parameters =  null;

            switch (module.GetModuleType())
            {
                case Enums.ModuleType.export:
                    parameters = GetParams(typeof(ModuleExport), module);
                    break;
                case Enums.ModuleType.extension:
                    parameters = GetParams(typeof(ModuleExtension), module);
                    break;
                case Enums.ModuleType.federation:
                    parameters = GetParams(typeof(ModuleFederation), module);
                    break;
                //case Enums.ModuleType.filter:
                //    parameters = GetParams(typeof(ModuleFilter));
                //    break;
                case Enums.ModuleType.host:
                    parameters = GetParams(typeof(ModuleHost), module);
                    break;
                case Enums.ModuleType.import:
                    parameters = GetParams(typeof(ModuleImport), module);
                    break;
                case Enums.ModuleType.@interface:
                    parameters = GetParams(typeof(ModuleInterface), module);
                    break;
                case Enums.ModuleType.osp:
                    parameters = GetParams(typeof (ModuleOsp), module);
                    break;
            }
            return parameters;
        }

        // Figure out the parameters in a module
        internal static TreeNode[] GetParams(Type module, IModule moduleObj)
        {
            PropertyInfo[] prop = module.GetProperties();
            TreeNode[] param = new TreeNode[prop.Count()];

            for (int i = 0; i < prop.Count(); i++)
            {
                PropertyInfo p = prop[i];
                DeployIfAttribute a = (DeployIfAttribute)p.GetCustomAttribute(typeof(DeployIfAttribute));

                TreeNode t;
                switch (a.FpdlName)     // handle special cases
                {
                    case "RTI":
                        t = new TreeNode("RTI", GetRti(typeof(Rti), ((ModuleFederation)moduleObj).RTI));
                        break;
                    case "OBJECTS":
                        t = new TreeNode("Objects", GetObjects(p, ((ModuleImport)moduleObj).Objects));
                        break;
                    case "INTERACTIONS":
                        t = new TreeNode("Interactions", GetObjects(p, ((ModuleImport)moduleObj).Objects));
                        break;
                    default:
                        t = new TreeNode(a.FpdlName + " = " + p.GetValue(moduleObj))
                        {
                            ToolTipText = a.ToolTip,
                            Tag = p.GetSetMethod()
                        };
                        break;
                }
                param[i] = t;
            }
            return param;
        }

        internal static TreeNode[] GetObjects(PropertyInfo p, IReadOnlyList<HlaObject> list)
        {
            DeployIfAttribute a = (DeployIfAttribute)p.GetCustomAttribute(typeof(DeployIfAttribute));
            TreeNode[] param = new TreeNode[list.Count];

            for (int i = 0; i < list.Count; i++)
            {
                TreeNode t;
                if (a.List)
                {
                    t = new TreeNode("Attributes", GetSimpleList(p, list[i].Attributes));
                }
                else
                {
                    t = new TreeNode(a.FpdlName + " = " + list[i].ObjectClassName)
                    {
                        ToolTipText = a.ToolTip,
                    };
                }
                param[i] = t;
            }
            return param;
        }

        internal static TreeNode[] GetInteractions(PropertyInfo p, IReadOnlyList<HlaInteraction> list)
        {
            DeployIfAttribute a = (DeployIfAttribute)p.GetCustomAttribute(typeof(DeployIfAttribute));
            TreeNode[] param = new TreeNode[list.Count];

            for (int i = 0; i < list.Count; i++)
            {
                TreeNode t;
                if (a.List)
                {
                    t = new TreeNode("Parameters", GetSimpleList(p, list[i].Parameters));
                }
                else
                {
                    t = new TreeNode(a.FpdlName + " = " + list[i].InteractionClassName)
                    {
                        ToolTipText = a.ToolTip,
                    };
                }
                param[i] = t;
            }
            return param;
        }

        internal static TreeNode[] GetSimpleList<T>(PropertyInfo p, List<T> list)
        {
            DeployIfAttribute a = (DeployIfAttribute)p.GetCustomAttribute(typeof(DeployIfAttribute));
            //MethodInfo setter = p.GetSetMethod();   // should be the setter for the list
            TreeNode[] param = new TreeNode[list.Count];

            for (int i = 0; i < list.Count; i++)
            {
                TreeNode t = new TreeNode(a.FpdlName + " = " + p.GetValue(list[i]))
                {
                    ToolTipText = a.ToolTip,
                    //Tag = setter
                };
                param[i] = t;
            }
            return param;
        }



        #region special cases

        internal static TreeNode[] GetRti(Type module, Rti rti)
        {
            PropertyInfo[] prop = module.GetProperties();
            TreeNode[] param = new TreeNode[prop.Count()];

            for (int i = 0; i < prop.Count(); i++)
            {
                PropertyInfo p = prop[i];
                DeployIfAttribute a = (DeployIfAttribute)p.GetCustomAttribute(typeof(DeployIfAttribute));

                TreeNode t;
                if (a.List)
                {
                    t = new TreeNode(a.FpdlName + " = " + p.GetValue(rti))
                    {
                        ToolTipText = a.ToolTip,
                        Tag = p.GetSetMethod()
                    };
                }
                else
                {
                    if (a.FpdlName.Contains("file"))
                        t = new TreeNode("FOM module", GetSimpleList(p, rti.FomFile));
                    else
                        t = new TreeNode("FOM module", GetSimpleList(p, rti.FomUri));
                }
                param[i] = t;
            }
            return param;
        }

        #endregion

    }
}
