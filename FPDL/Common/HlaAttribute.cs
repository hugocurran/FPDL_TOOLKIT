using System.Text;

namespace FPDL.Common
{
    /// <summary>
    /// HLA Attribute
    /// </summary>
    public class HlaAttribute
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
}
