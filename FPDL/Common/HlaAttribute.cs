using System.Text;
using System.Windows.Forms;

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
        public string AttributeName { get; set; }
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
            str.AppendFormat("{0} ", AttributeName);
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

        /// <summary>
        /// Get a TreeNode
        /// </summary>
        /// <returns></returns>
        public TreeNode GetNode()
        {
            if (DataType != null)
            {
                TreeNode[] t = new TreeNode[1];
                string dt = string.Format("{0} [{1}]", DefaultValue, DataType);
                t[0] = new TreeNode(dt);
                return new TreeNode(AttributeName, t);
            }
            return new TreeNode(AttributeName);
        }
    }
}
