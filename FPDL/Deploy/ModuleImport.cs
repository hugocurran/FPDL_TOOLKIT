using FPDL.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using static FPDL.Common.Enums;

namespace FPDL.Deploy
{
    /// <summary>
    /// Import policy module.
    /// </summary>
    public class ModuleImport : IModule
    {
        // See the FPDL definition for importDeployType
        // import
        //   interfaceName
        //   object
        //   interaction

        /// <summary>
        /// Interface binding for the policy
        /// </summary>
        [DeployIf("interfaceName", "Interface binding for import policy")]
        public string InterfaceName { get; set; }
        /// <summary>
        /// Objects
        /// </summary>
        [DeployIf("OBJECTS", "Objects", true, true)]
        public IReadOnlyList<HlaObject> Objects { get { return _objects; } }
        /// <summary>
        /// Interactions
        /// </summary>
        [DeployIf("INTERACTIONS", "Interactions", true, true)]
        public IReadOnlyList<HlaInteraction> Interactions { get { return _interactions; } }

        private List<HlaObject> _objects = new List<HlaObject>();
        private List<HlaInteraction> _interactions = new List<HlaInteraction>();

        /// <summary>
        /// Get the module identity
        /// </summary>
        public ModuleType GetModuleType()
        {
            return ModuleType.import;
        }


        /// <summary>
        /// Construct an Import module
        /// </summary>
        public ModuleImport() {}
        /// <summary>
        /// Construct an Import module from FPDL
        /// </summary>
        /// <param name="fpdl"></param>
        public ModuleImport(XElement fpdl)
        {
            FromFPDL(fpdl);
        }
        /// <summary>
        /// Deserialise Import module from FPDL
        /// </summary>
        /// <param name="fpdl"></param>
        public void FromFPDL(XElement fpdl)
        {
            if (fpdl.Name != "import")
                throw new ApplicationException("Cannot parse: Not an FPDL import description");
            try
            {
                if (fpdl.Element("interfaceName") != null)
                    InterfaceName = fpdl.Element("interfaceName").Value;

                foreach (XElement hlaObject in fpdl.Elements("object"))
                    _objects.Add(new HlaObject(hlaObject));

                foreach (XElement hlaInteraction in fpdl.Elements("interaction"))
                    _interactions.Add(new HlaInteraction(hlaInteraction));
            }
            catch (NullReferenceException e)
            {
                throw new ApplicationException("Cannot parse: " + e.Message);
            }
        }
        /// <summary>
        /// Serialise Import module to FPDL
        /// </summary>
        /// <returns></returns>
        public XElement ToFPDL()
        {
            XElement fpdl = new XElement("import");
            fpdl.Add(new XElement("interfaceName", InterfaceName));

            foreach (HlaObject hlaObject in _objects)
                fpdl.Add(hlaObject.ToFPDL());
            foreach (HlaInteraction hlaInteraction in _interactions)
                fpdl.Add(hlaInteraction.ToFPDL());
            return fpdl;
        }
        /// <summary>
        /// String representation of Import module
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder str = new StringBuilder("  Import Policy:\n");
            str.AppendFormat("\tInterfaceName: {0}\n", InterfaceName);
            foreach (HlaObject obj in _objects)
                str.AppendFormat("\t{0}\n", obj.ToString());
            foreach (HlaInteraction inter in _interactions)
                str.AppendFormat("\t{0}\n", inter.ToString());
            return str.ToString();
        }
    }
}
