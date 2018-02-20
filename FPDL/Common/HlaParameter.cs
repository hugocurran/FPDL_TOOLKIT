using System.Text;
using System.Windows.Forms;

namespace FPDL.Common
{
    /// <summary>
    /// HLA Parameter
    /// </summary>
    public class HlaParameter
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
                return new TreeNode(ParameterName, t);
            }
            return new TreeNode(ParameterName);
        }
    }
}
