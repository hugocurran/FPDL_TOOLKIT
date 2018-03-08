using FPDL.Common;
using FPDL.Deploy;
using FPDL.Design;
using FPDL.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPDL.Tools.DeployEditor
{
    /// <summary>
    /// Make a deploy skeleton by combining Design and Pattern
    /// </summary>
    internal static class BuildIt
    {
        internal static DeployObject Initialise(DesignObject design, string owner, string classification, DeployEditor editor)
        {
            // we need to build a skeleton of the Deploy doc
            DeployObject deploy = new DeployObject();

            deploy.ConfigMgmt = new ConfigMgmt();
            string author = Environment.UserName;
            deploy.ConfigMgmt.Initialise(author, 1, 0, "Initial Version", owner, classification);

            deploy.DesignReference = design.ConfigMgmt.DocReference;

            foreach(Federate federate in design.Federation.Federates)
            {
                DeploySystem system = null;
                if (federate.FederateType != Enums.FederateType.service) // it is a gateway or filter
                {
                    system = new DeploySystem
                    {
                        SystemType = federate.FederateType.ToPattern(federate.GatewayType),
                        FederateName = federate.FederateName
                        //PatternType = federate.FederateType.ToPattern(federate.GatewayType)
                    };
                    deploy.Systems.Add(system);
                    editor.checkList[federate.FederateName] = true;
                }
            }
            return deploy;
        }

        
        // Complete the system spec using a pattern
        internal static void AddPattern(Federate federate, DeploySystem system, PatternObject pattern)
        {
            system.Pattern = pattern.PatternName;
            system.PatternRef = pattern.ConfigMgmt.DocReference;
            foreach (var pcomponent in pattern.Components)
            {
                Deploy.Component component = new Deploy.Component();
                component.ComponentType = pcomponent.ComponentType;
                component.ComponentID = pcomponent.ComponentID;
                component.ComponentName = pcomponent.ComponentName ?? "";
                foreach (var pmodule in pcomponent.Modules)
                {
                    IModule module = ModuleFactory.Create(pmodule.ModuleType);
                    component.Modules.Add(module);
                    switch (pmodule.ModuleType)
                    {
                        case Enums.ModuleType.federation:
                        case Enums.ModuleType.extension:
                        case Enums.ModuleType.host:
                        case Enums.ModuleType.@interface:
                        case Enums.ModuleType.osp:
                            module.ApplyPattern(pmodule.Specifications);
                            break;
                        case Enums.ModuleType.import:
                            module.ApplyPattern(pmodule.Specifications);
                            if (federate.Publish != null)
                                ((ModuleImport)module).ApplyPattern(federate.Publish);
                            break;
                        case Enums.ModuleType.export:
                            module.ApplyPattern(pmodule.Specifications);
                            if (federate.Sources.Count > 0)
                                ((ModuleExport)module).ApplyPattern(federate.Sources);
                            break;
                        //case Enums.ModuleType.filter:
                        //    module.ApplyPattern(pmodule.Specifications);
                        //    ((ModuleFilter)module).ApplyPattern(federate.Filter);
                        //    break;
                    }
                }
                system.Components.Add(component);
            }
        }
    }
}
