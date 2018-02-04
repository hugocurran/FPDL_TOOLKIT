using System.Xml.Linq;

namespace FPDL.Deploy
{
    /// <summary>
    /// Common interface for Module types
    /// </summary>
    public interface IModule
    {
        /// <summary>
        /// Deserialise module from FPDL
        /// </summary>
        /// <param name="fpdl"></param>
        void FromFPDL(XElement fpdl);
        /// <summary>
        /// Serialise module to FPDL
        /// </summary>
        /// <returns></returns>
        XElement ToFPDL();
    }
}
