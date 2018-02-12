using FPDL.Common;
using FPDL.Deploy;
using FPDL.Design;
using FPDL.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeployEditor
{
    /// <summary>
    /// Make a deploy skeleton by combining Design and Pattern
    /// </summary>
    internal class BuildIt
    {
        private DesignObject design;
        internal DeployObject deploy;

        internal BuildIt()
        {
            deploy = new DeployObject();
        }

        internal void Initialise(DesignObject design, string owner, string classification)
        {
            this.design = design;

            // we need to build a skeleton of the Deploy doc
            deploy.ConfigMgmt = new ConfigMgmt();
            string author = Environment.UserName;
            deploy.ConfigMgmt.Initialise(author, 1, 0, "Initial Version", owner, classification);

            deploy.DesignReference = design.ConfigMgmt.DocReference;

            foreach(Federate federate in design.Federation.Federates)
            {
                if ((federate.FederateType == Enums.FederateType.gateway) || (federate.FederateType == Enums.FederateType.filter))
                {
                    FPDL.Deploy.System system = new FPDL.Deploy.System
                    {
                        SystemType = federate.FederateType
                    };
                }
            }
        }

        internal void AddPattern(FPDL.Deploy.System system, PatternObject pattern)
        {
            system.Pattern = pattern.PatternName;
            system.PatternRef = pattern.ConfigMgmt.DocReference;
            foreach (var pcomponent in pattern.Components)
            {
                FPDL.Deploy.Component component = new FPDL.Deploy.Component();
                component.ComponentType = pcomponent.ComponentType;
                component.ComponentID = pcomponent.ComponentID;
                foreach (var pmodule in pcomponent.Modules)
                {
                    ModuleFactory.Create(pmodule.ModuleType, component);
                }
            }
        }

        

    }
}
