using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FPDL.Policy
{
    /// <summary>
    /// ReleaseStatement Interface
    /// </summary>
    public interface IReleaseStatement
    {
        /// <summary>
        /// Deserialise from FPDL
        /// </summary>
        /// <param name="fpdl"></param>
        void FromFPDL(XElement fpdl);
        /// <summary>
        /// Serialise to FPDL
        /// </summary>
        /// <returns></returns>
        XElement ToFPDL();
        /// <summary>
        /// String representation
        /// </summary>
        /// <returns></returns>
        string ToString();
    }
}
