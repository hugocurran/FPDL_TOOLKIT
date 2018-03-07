using FPDL.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPDL
{
    /// <summary>
    /// Extension methods
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Return a string with the first character capitalised
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ToUpperFirst(this string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            char[] a = s.ToCharArray();
            a[0] = char.ToUpper(a[0]);
            return new string(a);
        }
        /// <summary>
        /// Derive PatternType from FederateType and GatewayType
        /// </summary>
        /// <param name="type"></param>
        /// <param name="gw"></param>
        /// <returns></returns>
        public static Enums.PatternType ToPattern(this Enums.FederateType type, Enums.GatewayType gw)
        {
            switch (type)
            {
                case Enums.FederateType.gateway:
                    return (Enums.PatternType)Enum.Parse(typeof(Enums.PatternType), gw.ToString());
                case Enums.FederateType.filter:
                    return Enums.PatternType.filter;
                default:
                    return Enums.PatternType.NotApplicable;
            }
        }

        public static string VersionToString(this Tuple<int,int> version)
        {
            string major = version.Item1.ToString();
            string minor = version.Item2.ToString();
            return major + "." + minor;
        }
    }
}
