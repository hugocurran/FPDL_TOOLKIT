using FPDL.Common;
using FPDL.Deploy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FPDL.Pattern
{
    /// <summary>
    /// Pattern
    /// </summary>
    public class PatternObject : IFpdlObject
    {

        /// <summary>
        /// ConfigMgmt for the Pattern document
        /// </summary>
        public ConfigMgmt ConfigMgmt;
        /// <summary>
        /// Pattern type
        /// </summary>
        public Enums.PatternType PatternType;
        /// <summary>
        /// Pattern name
        /// </summary>
        public string PatternName;
        /// <summary>
        /// Description
        /// </summary>
        public string Description;
        /// <summary>
        /// Components
        /// </summary>
        public List<Component> Components = new List<Component>();
        /// <summary>
        /// Construct PatternObject
        /// </summary>
        public PatternObject() { }

        /// <summary>
        /// Construct Pattern object from FPDL
        /// </summary>
        /// <param name="fpdl"></param>
        public PatternObject(XElement fpdl)
        {
            FromFPDL(fpdl);
        }
        /// <summary>
        /// Deserialise PatternObject from FPDL
        /// </summary>
        /// <param name="fpdl"></param>
        public void FromFPDL(XElement fpdl)
        {
            if (fpdl.Name != "Pattern")
                throw new ApplicationException("Cannot parse: Not an FPDL Pattern file");
            try
            {
                ConfigMgmt = new ConfigMgmt(fpdl.Descendants("configMgmt").FirstOrDefault());
                PatternType = (Enums.PatternType)Enum.Parse(typeof(Enums.PatternType), fpdl.Element("patternGenericType").Value);
                PatternName = fpdl.Element("patternName").Value;
                Description = fpdl.Element("description").Value;
                foreach (XElement component in fpdl.Descendants("component"))
                    Components.Add(new Component(component));
            }
            catch (ArgumentException e)
            {
                throw new ApplicationException("Invalid generic pattern type: " + e.Message);
            }
            catch (NullReferenceException e)
            {
                throw new ApplicationException("PatternObject parse error: " + e.Message);
            }
        }
        /// <summary>
        /// Serialise PatternObject to FPDL
        /// </summary>
        /// <returns></returns>
        public XElement ToFPDL()
        {
            XElement pattern = new XElement("Pattern",
                ConfigMgmt.ToFPDL(),
                new XElement("patternGenericType", PatternType.ToString()),
                new XElement("patternName", PatternName),
                new XElement("description", Description)
            );
            foreach (Component component in Components)
            {
                pattern.Add(component.ToFPDL());
            }
            return pattern;
        }
        /// <summary>
        /// String representation of PatternObject
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder str = new StringBuilder("PATTERN =>\n");
            str.AppendFormat("{0}", ConfigMgmt);
            foreach (Component component in Components)
                str.AppendFormat("\n{0}\n", component);
            return str.ToString();
        }
    }
}
