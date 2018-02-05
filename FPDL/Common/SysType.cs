using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPDL.Common
{
    /// <summary>
    /// DOTC(A) system type
    /// </summary>
    public class Sys
    {
        /// <summary>
        /// System types
        /// </summary>
        public enum Type
        {
            /// <summary>
            /// Gateway
            /// </summary>
            Gateway,
            /// <summary>
            /// Filter
            /// </summary>
            Filter,
            /// <summary>
            /// Service
            /// </summary>
            Service
        }
    }
}
