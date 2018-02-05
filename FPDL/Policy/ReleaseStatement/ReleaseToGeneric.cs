using System;
using System.Text;
using System.Xml.Linq;

namespace FPDL.Policy
{
    /// <summary>
    /// ReleaseToGeneric
    /// </summary>
    public class ReleaseToGeneric : IReleaseStatement
    {
        /// <summary>
        /// Caveat type
        /// </summary>
        public enum CaveatType
        {
            /// <summary>
            /// Permissive
            /// </summary>
            permissive,
            /// <summary>
            /// Restrictive
            /// </summary>
            restrictive,
            /// <summary>
            /// NOT Releaseable
            /// </summary>
            NOT_RELEASABLE
        }
        /// <summary>
        /// True if this is a NotReleasableToGeneric statement
        /// </summary>
        public bool NotReleasable = false;
        /// <summary>
        /// Owner
        /// </summary>
        public string SecurityOwner;
        /// <summary>
        /// Classification
        /// </summary>
        public string SecurityClassification;
        /// <summary>
        /// Caveat (optional)
        /// </summary>
        public string SecurityCaveat;
        /// <summary>
        /// Caveat Type
        /// </summary>
        public CaveatType Caveat;
        /// <summary>
        /// Notes
        /// </summary>
        public string SecurityNote;

        /// <summary>
        /// Construct ReleaseToGeneric object
        /// </summary>
        public ReleaseToGeneric(bool notReleasable = false)
        {
            this.NotReleasable = notReleasable;
        }
        /// <summary>
        /// Construct ReleaseToGeneric from FPDL
        /// </summary>
        /// <param name="fpdl"></param>
        public ReleaseToGeneric(XElement fpdl)
        {
            FromFPDL(fpdl);
        }
        /// <summary>
        /// Deserialise ReleaseToGeneric from FPDL
        /// </summary>
        /// <param name="fpdl"></param>
        public void FromFPDL(XElement fpdl)
        {
            try
            {
                if (fpdl.Name.ToString() == "notReleaseToGeneric")
                    NotReleasable = true;
                else
                    NotReleasable = false;

                SecurityOwner = fpdl.Element("securityOwner").Value;
                SecurityClassification = fpdl.Element("securityClassification").Value;
                if (fpdl.Element("securityCaveat") != null)
                {
                    SecurityCaveat = fpdl.Element("securityCaveat").Value;
                    Caveat = (CaveatType)Enum.Parse(typeof(CaveatType), fpdl.Element("securityCaveat").Attribute("type").Value);
                }
                if (fpdl.Element("securityNote") != null)
                    SecurityNote = fpdl.Element("securityNote").Value;
            }
            catch (ArgumentException e)
            {
                throw new ApplicationException("Invalid caveat type: " + e.Message);
            }
            catch (NullReferenceException e)
            {
                throw new ApplicationException("ReleaseToGeneric parse error: " + e.Message);
            }
        }
        /// <summary>
        /// Serialise ReleaseToGeneric to FPDL
        /// </summary>
        /// <returns></returns>
        public XElement ToFPDL()
        {
            XElement fpdl;
            if (NotReleasable)
                fpdl = new XElement("notReleasableToGeneric",
                    new XElement("securityOwner", SecurityOwner),
                    new XElement("securityClassification", SecurityClassification)
                    );
            else
                fpdl = new XElement("releasableToGeneric",
                    new XElement("securityOwner", SecurityOwner),
                    new XElement("securityClassification", SecurityClassification)
                    );

            if (SecurityCaveat != null)
                fpdl.Add(new XElement("securityCaveat", SecurityCaveat, new XAttribute("type", Caveat.ToString())));
            if (SecurityNote != null)
                fpdl.Add(new XElement("securityNote", SecurityNote));
            return fpdl;
        }

        /// <summary>
        /// String representation of ReleaseToGeneric
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            if (NotReleasable)
                str.Append("NotReleasableToGeneric: ");
            else
                str.Append("ReleasableToGeneric: ");
            str.AppendFormat("{0} {1} ", SecurityOwner, SecurityClassification);
            if (SecurityCaveat != null)
                str.AppendFormat("[{0} - {1}] ", SecurityCaveat, Caveat);
            if (SecurityNote != null)
                str.AppendFormat("{0}", SecurityNote);
            return str.ToString();
        }
    }
}
