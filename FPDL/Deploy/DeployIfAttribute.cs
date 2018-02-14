using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPDL.Common
{
    /// <summary>
    /// Class defining the custome DeployIf Attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class DeployIfAttribute : Attribute
    {
        private string fpdlName;
        private string toolTip;
        private bool optional;
        private bool list;

        /// <summary>
        /// The name of this value in FPDL
        /// </summary>
        public string FpdlName
        {
            get { return fpdlName; }
        }
        /// <summary>
        /// Text for use in a tooltip
        /// </summary>
        public string ToolTip
        {
            get { return toolTip; }
        }
        /// <summary>
        /// Defines if this field is optional or not
        /// </summary>
        public bool Optional
        {
            get { return optional; }
        }
        /// <summary>
        /// Defines if this field is a List type or not
        /// </summary>
        public bool List
        {
            get { return list; }
        }
        /// <summary>
        /// Construct a DeployIf Attribute object
        /// </summary>
        /// <param name="fpdlName"></param>
        /// <param name="toolTip"></param>
        /// <param name="optional"></param>
        /// <param name="list"></param>
        public DeployIfAttribute(string fpdlName, string toolTip, bool optional=false, bool list=false)
        {
            this.fpdlName = fpdlName;
            this.toolTip = toolTip;
            this.optional = optional;
            this.list = list;
        }

    }
}
