using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPDL.Tools.PatternEditor
{
    [DefaultProperty("SaveOnClose")]
    public class ModuleView
    {
        private bool saveOnClose = true;

        private bool settingsChanged = false;

        [Category("Module Settings"),
            DefaultValue(true)]
        public bool SaveOnClose
        {
            get { return saveOnClose; }
            set { saveOnClose = value; }
        }



    }
}
