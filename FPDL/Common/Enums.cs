using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPDL.Common
{
    /// <summary>
    /// FPDL Schema Enum mappings\n
    /// NB: The FPDL Schema comvention is for lower case
    /// </summary>
    public class Enums
    {
        /// <summary>
        /// System types
        /// </summary>
        public enum FederateType
        {
            /// <summary>
            /// Gateway
            /// </summary>
            gateway,
            /// <summary>
            /// Filter
            /// </summary>
            filter,
            /// <summary>
            /// Service
            /// </summary>
            service
        }
        /// <summary>
        /// Gateway Type
        /// </summary>
        public enum GatewayType
        {
            /// <summary>
            /// Not Applicable
            /// </summary>
            NotApplicable,  // avoids selecting HTG by defaul
            /// <summary>
            /// HTG
            /// </summary>
            htg,
            /// <summary>
            /// MTG
            /// </summary>
            mtg,
            /// <summary>
            /// LTG
            /// </summary>
            ltg
        }

        /// <summary>
        /// Component type
        /// </summary>
        public enum ComponentType
        {
            /// <summary>
            /// Proxy
            /// </summary>
            proxy,
            /// <summary>
            /// Guard
            /// </summary>
            guard,
            /// <summary>
            /// Filter
            /// </summary>
            filter
        }
        /// <summary>
        /// Module type
        /// </summary>
        public enum ModuleType
        {
            /// <summary>
            /// Interface
            /// </summary>
            @interface, // character literal to protocol c# reserved word
            /// <summary>
            /// OSP
            /// </summary>
            osp,
            /// <summary>
            /// Export Policy
            /// </summary>
            export,
            /// <summary>
            /// Import Policy
            /// </summary>
            import,
            /// <summary>
            /// Filter
            /// </summary>
            filter,
            /// <summary>
            /// Federation
            /// </summary>
            federation,
            /// <summary>
            /// Vendor extension
            /// </summary>
            extension,
            /// <summary>
            /// Host
            /// </summary>
            host
        }
    }
}
