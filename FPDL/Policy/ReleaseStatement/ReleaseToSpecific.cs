using System;
using System.Text;
using System.Xml.Linq;

namespace FPDL.Policy
{
    /// <summary>
    /// ReleaseToSpecific
    /// </summary>
    public class ReleaseToSpecific : IReleaseStatement
    {
        /// <summary>
        /// True if this is a NotReleasableToSpecific statement
        /// </summary>
        public bool NotReleasable = false;

        /// <summary>
        /// Federate Name
        /// </summary>
        public string FederateName;

        /// <summary>
        /// Construct ReleaseToSpecific object
        /// </summary>
        /// <param name="notReleasable"></param>
        public ReleaseToSpecific(bool notReleasable = false)
        {
            NotReleasable = notReleasable;
        }
        /// <summary>
        /// Construct ReleaseToSpecific object from FPDL
        /// </summary>
        /// <param name="fpdl"></param>
        public ReleaseToSpecific(XElement fpdl)
        {
            FromFPDL(fpdl);
        }
        /// <summary>
        /// Deserialise ReleaseToSpecific object from FPDL
        /// </summary>
        /// <param name="fpdl"></param>
        public void FromFPDL(XElement fpdl)
        {
            try
            {
                if (fpdl.Name.ToString() == "notReleaseToSpecific")
                    NotReleasable = true;
                else
                    NotReleasable = false;

                FederateName = fpdl.Element("federateName").Value;
            }
            catch (NullReferenceException e)
            {
                throw new ApplicationException("ReleaseToSpecific parse error: " + e.Message);
            }
        }
        /// <summary>
        /// Serialise ReleaseToSpecific to FPDL
        /// </summary>
        /// <returns></returns>
        public XElement ToFPDL()
        {
            XElement fpdl;
            if (NotReleasable)
            {
                fpdl = new XElement("notReleasableToSpecific",
                    new XElement("federateName", FederateName));
            }
            else
            {
                fpdl = new XElement("ReleasableToSpecific",
                    new XElement("federateName", FederateName));
            }
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
                str.Append("NotReleasableToSpecific: ");
            else
                str.Append("ReleasableToSpecific: ");
            str.AppendFormat("{0}", FederateName);
            return str.ToString();
        }
    }
}
