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
        [DeployIf("name", "Attribute name")]
        public string Name { get; set; }
        /// <summary>
        /// Attribute data type (optional)
        /// </summary>
        [DeployIf("dataType", "Data type",true)]
        public string DataType { get; set; }
        /// <summary>
        /// Attribute default value (optional)
        /// </summary>
        [DeployIf("defaultValue", "Default value", true)]
        public object DefaultValue { get; set; }

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
