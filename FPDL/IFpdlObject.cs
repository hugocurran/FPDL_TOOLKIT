using System.Xml.Linq;

namespace FPDL
{
    /// <summary>
    /// Interface for FPDL *Object classes
    /// </summary>
    public interface IFpdlObject
    {
        /// <summary>
        /// Deserialise object from FPDL document
        /// </summary>
        /// <param name="fpdl"></param>
        void FromFPDL(XElement fpdl);

        /// <summary>
        /// Serialise object to FPDL document
        /// </summary>
        /// <returns></returns>
        XElement ToFPDL();
    }
}
