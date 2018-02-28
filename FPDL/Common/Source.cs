using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace FPDL.Common
{
    /// <summary>
    /// FPDL sourceType
    /// </summary>
    public class Source
    {
        // Note that we handle things differently to the FPDL schema....
        /// <summary>
        /// Source type
        /// </summary>
        public enum Type
        {
            /// <summary>
            /// Federate source
            /// </summary>
            Federate,
            /// <summary>
            /// Entity source
            /// </summary>
            Entity
        }
        /// <summary>
        /// Source type
        /// </summary>
        [DeployIf("sourceType", "Federate or Entity")]
        public Type SourceType { get; private set; }
        ///// <summary>
        ///// ID of source
        ///// </summary>
        //[DeployIf("sourceID", "Federate name or Entity ID")]
        //public string SourceId { get; private set; }

        /// <summary>
        /// Identity of the entity
        /// </summary>
        [DeployIf("entitySource", "Entity", true)]
        public string EntityId { get; private set; }
        /// <summary>
        /// Name of the federate
        /// </summary>
        [DeployIf("federateSource", "Federate", true)]
        public string FederateName { get; private set; }

        /// <summary>
        /// True if this Source refers to a filter
        /// </summary>
        public bool FilterSource;
        /// <summary>
        /// HLA objects (read only)
        /// </summary>
        [DeployIf("OBJECTS", "Objects", true, true)]
        public IReadOnlyList<HlaObject> Objects { get { return _objects; } }
        /// <summary>
        /// HLA interactions (read only)
        /// </summary>
        [DeployIf("INTERACTIONS", "Interactions", true, true)]
        public IReadOnlyList<HlaInteraction> Interactions { get { return _interactions; } }

        private List<HlaObject> _objects = new List<HlaObject>();
        private List<HlaInteraction> _interactions = new List<HlaInteraction>();

        /// <summary>
        /// Construct Source object
        /// </summary>
        public Source()
        {
            _objects = new List<HlaObject>();
            _interactions = new List<HlaInteraction>();
        }
        /// <summary>
        /// Construct a Source object from FPDL
        /// </summary>
        /// <param name="fpdl"></param>
        public Source(XElement fpdl) : this ()
        {
            FromFPDL(fpdl);
        }

        /// <summary>
        /// Set the identity of the subscription source
        /// </summary>
        /// <param name="source">Federate or Entity</param>
        /// <param name="id">Identity</param>
        /// <exception cref="ApplicationException">If source is Entity and name is not a GUID</exception>
        public void SetSubscribeSource(XElement id)
        {
            try
            {
                Guid _g = Guid.Parse(id.Value);
            }
            catch (FormatException e)
            {
                throw new ApplicationException("Deploy.Entities.Subscribe.SetSubscribeSource: Invalid EntityID - ", e);
            }
            EntityId = id.Value;
            FederateName = id.Attribute("federateName").Value;
        }
        /// <summary>
        /// Deserialise Source object from FPDL
        /// </summary>
        /// <param name="fpdl"></param>
        public void FromFPDL(XElement fpdl)
        {
            if (fpdl.Name != "source")
                throw new ApplicationException("Cannot parse: Not an FPDL source description");
            try
            {
                if (fpdl.Attribute("filterSource") != null)
                    FilterSource = Convert.ToBoolean(fpdl.Attribute("filterSource").Value);
                else
                    FilterSource = false;

                if (fpdl.Element("federateSource") != null)
                    FederateName = fpdl.Element("federateSource").Value;
                else if (fpdl.Element("entitySource") != null)
                    SetSubscribeSource(fpdl.Element("entitySource"));

                if (fpdl.Element("object") != null)
                {
                    foreach (XElement hlaObject in fpdl.Elements("object"))
                        _objects.Add(new HlaObject(hlaObject));
                }
                if (fpdl.Element("interaction") != null)
                {
                    foreach (XElement hlaInteraction in fpdl.Elements("interaction"))
                    {
                        _interactions.Add(new HlaInteraction(hlaInteraction));
                    }
                }
            }
            catch (NullReferenceException e)
            {
                throw new ApplicationException("Cannot parse: " + e.Message);
            }
        }

        /// <summary>
        /// Serialise Source to FPDL
        /// </summary>
        /// <returns></returns>
        public XElement ToFPDL()
        {
            XElement fpdlType = new XElement("source", new XAttribute("filterSource", FilterSource.ToString()));

            if (SourceType == Type.Federate)
                fpdlType.Add(new XElement("federateSource", FederateName));
            else
                fpdlType.Add(new XElement("entitySource", EntityId, new XAttribute("federateName", FederateName)));

            foreach (HlaObject obj in _objects)
            {
                fpdlType.Add(obj.ToFPDL());
            }

            foreach (HlaInteraction inter in _interactions)
            {
                fpdlType.Add(inter.ToFPDL());
            }
            return fpdlType;
        }

        /// <summary>
        /// String representation of source
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder str = new StringBuilder("Source: ");
            if (SourceType == Type.Federate)
                str.AppendFormat("(Federate) {0}\n", FederateName);
            else
                str.AppendFormat("(Entity) {0}\n", EntityId);
            foreach (HlaObject obj in _objects)
            {
                str.AppendFormat("\t{0}\n", obj.ToString());
            }
            foreach (HlaInteraction inter in _interactions)
            {
                str.AppendFormat("\t{0}\n", inter.ToString());
            }
            return str.ToString();
        }
        /// <summary>
        /// Get a TreeNode
        /// </summary>
        /// <returns></returns>
        public TreeNode GetNode()
        {
            TreeNode[] t1 = new TreeNode[Objects.Count];
            for (int i = 0; i < Objects.Count; i++)
                t1[i] = Objects[i].GetNode();

            TreeNode[] t2 = new TreeNode[Interactions.Count];
            for (int i = 0; i < Interactions.Count; i++)
                t2[i] = Interactions[i].GetNode();

            TreeNode a;
            if (SourceType == Type.Federate)
                a = new TreeNode(FederateName);
            else
                a = new TreeNode(EntityId + " (" + FederateName + ")");
            a.Nodes.AddRange(t1);
            a.Nodes.AddRange(t2);
            a.ToolTipText = "Source federate/entity";
            a.Tag = this;
            return a;
        }
    }
}

