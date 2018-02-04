using System.Text;

namespace FPDL.Common
{
    /// <summary>
    /// HLA Attribute value type
    /// </summary>
    public struct HlaAttribute
    {
        /// <summary>
        /// Attribute Name
        /// </summary>
        public string Name;
        /// <summary>
        /// Attribute data type (optional)
        /// </summary>
        public string DataType;
        /// <summary>
        /// Attribute default value (optional)
        /// </summary>
        public object DefaultValue;

        /// <summary>
        /// String representation of HLA Attribute
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            str.AppendFormat("{0} ", Name);
            if (DataType != "")
            {
                str.AppendFormat("[{0}", DataType);
                if (DefaultValue != null)
                    str.AppendFormat(" ({0})]", DefaultValue);
                else
                    str.Append("]");
            }
            return str.ToString();
        }
    }
    /// <summary>
    /// HLA Parameter value type
    /// </summary>
    public struct HlaParameter
    {
        /// <summary>
        /// Parameter name
        /// </summary>
        public string ParameterName;
        /// <summary>
        /// Parameter data type (optional)
        /// </summary>
        public string DataType;
        /// <summary>
        /// Parameter default value (optional)
        /// </summary>
        public object DefaultValue;

        /// <summary>
        /// String representation of HLA Parameter
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            str.AppendFormat("{0} ", ParameterName);
            if (DataType != "")
            {
                str.AppendFormat("[{0}", DataType);
                if (DefaultValue != null)
                    str.AppendFormat(" ({0})]", DefaultValue);
                else
                    str.Append("]");
            }
            return str.ToString();
        }
    }
}
