using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FPDL.Common
{
    /// <summary>
    /// FPDL sourceType
    /// </summary>
    public class Source
    {
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
        public Type SourceType { get; private set; }
        /// <summary>
        /// ID of source
        /// </summary>
        public string SourceId { get; private set; }
        /// <summary>
        /// HLA objects (read only)
        /// </summary>
        public IReadOnlyList<HlaObject> Objects { get { return _objects; } }
        /// <summary>
        /// HLA interactions (read only)
        /// </summary>
        public IReadOnlyList<HlaInteraction> Interactions { get { return _interactions; } }

        private List<HlaObject> _objects = new List<HlaObject>();
        private List<HlaInteraction> _interactions = new List<HlaInteraction>();

        /// <summary>
        /// Construct Source object
        /// </summary>
        public Source() { }
        /// <summary>
        /// Construct a Source object from FPDL
        /// </summary>
        /// <param name="fpdl"></param>
        public Source(XElement fpdl)
        {
            FromFPDL(fpdl);
        }

        /// <summary>
        /// Set the identity of the subscription source
        /// </summary>
        /// <param name="source">Federate or Entity</param>
        /// <param name="id">Identity</param>
        /// <exception cref="ApplicationException">If source is Entity and name is not a GUID</exception>
        public void SetSubscribeSource(Type source, string id)
        {
            SourceType = source;
            if (SourceType == Type.Entity)
            {
                try
                {
                    Guid _g = Guid.Parse(id);
                }
                catch (FormatException e)
                {
                    throw new ApplicationException("Deploy.Entities.Subscribe.SetSubscribeSource: Invalid EntityID - ", e);
                }
            }
            SourceId = id;
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
                if (fpdl.Element("federateSource") != null)
                    SetSubscribeSource(Type.Federate, fpdl.Element("federateSource").Value);
                else if (fpdl.Element("entitySource") != null)
                    SetSubscribeSource(Type.Entity, fpdl.Element("entitySource").Value);

                if (fpdl.Element("object") != null)
                {
                    foreach (XElement hlaObject in fpdl.Elements("object"))
                        _objects.Add(HlaObject.FromFPDL(hlaObject));
                }
                if (fpdl.Element("interaction") != null)
                {
                    foreach (XElement hlaInteraction in fpdl.Elements("interaction"))
                    {
                        _interactions.Add(HlaInteraction.FromFPDL(hlaInteraction));
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
            XElement fpdlType = new XElement("source");

            if (SourceType == Type.Federate)
                fpdlType.Add(new XElement("federateSource", SourceId));
            else
                fpdlType.Add(new XElement("entitySource", SourceId));

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
                str.AppendFormat("(Federate) {0}\n", SourceId);
            else
                str.AppendFormat("(Entity) {0}\n", SourceId);
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
    }
}

