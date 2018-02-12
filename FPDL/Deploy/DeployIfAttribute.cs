using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPDL.Deploy
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class DeployIfAttribute : Attribute
    {
        private string fpdlName;
        private string toolTip;
        private bool optional;

        public string FpdlName
        {
            get { return fpdlName; }
        }
        public string ToolTip
        {
            get { return toolTip; }
        }
        public bool Optional
        {
            get { return optional; }
        }
        public DeployIfAttribute(string fpdlName, string toolTip, bool optional=false)
        {
            this.fpdlName = fpdlName;
            this.toolTip = toolTip;
            this.optional = optional;
        }

    }
}
