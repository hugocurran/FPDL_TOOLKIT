using System;
using System.Xml.Linq;

namespace FPDL.Policy
{
    /// <summary>
    /// Factory method for Release Statements
    /// </summary>
    public static class ReleaseStatement
    {
        /// <summary>
        /// Create a ReleaseStatement from FPDL
        /// </summary>
        /// <param name="fpdl"></param>
        /// <returns></returns>
        public static IReleaseStatement FromFPDL(XElement fpdl)
        {
            switch (fpdl.Name.ToString())
            {
                case "releaseToGeneric":
                case "notReleaseToGeneric":
                    return new ReleaseToGeneric(fpdl);
                case "releaseToSpecific":
                case "notReleaseToSpecific":
                    return new ReleaseToSpecific(fpdl);
                default:
                    throw new ApplicationException("Not a valid release statement");
            }
        }
    }

   


}