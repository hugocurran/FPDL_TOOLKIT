using FPDL.Pattern;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml.Linq;
using static FPDL.Common.Enums;

namespace FPDL.Deploy
{
    /// <summary>
    /// Common interface for Module types
    /// </summary>
    public interface IModule
    {
        /// <summary>
        /// Get the module identity
        /// </summary>
        ModuleType GetModuleType();
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
        /// <summary>
        /// Get a TreeNode representation
        /// </summary>
        /// <returns></returns>
        TreeNode GetNode();
        /// <summary>
        /// Apply specifications from a Pattern to this module
        /// </summary>
        /// <param name="specifications"></param>
        void ApplyPattern(List<Specification> specifications);
    }
}
