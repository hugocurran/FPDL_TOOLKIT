using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FPDL.Policy
{
    public class PolicyObject : IFpdlObject
    {

        public PolicyObject(XElement fpdl)
        {
            FromFPDL(fpdl);
        }

        public void FromFPDL(XElement fpdl)
        { }

        public XElement ToFPDL()
        {
            return new XElement("Policy");
        }
    }
}
