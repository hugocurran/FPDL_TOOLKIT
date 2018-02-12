using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace FPDL.Design
{
    /// <summary>
    /// Federation
    /// </summary>
    public class Federation
    {
        /// <summary>
        /// Federation name
        /// </summary>
        public string Name;
        /// <summary>
        /// Federation summary
        /// </summary>
        public string Summary;
        /// <summary>
        /// URI of MSDL Scenario (optional)
        /// </summary>
        public string Scenario;
        /// <summary>
        /// Modules
        /// </summary>
        public List<Federate> Federates = new List<Federate>();


        /// <summary>
        /// Construct Federation object from FPDL
        /// </summary>
        /// <param name="fpdl"></param>
        public Federation(XElement fpdl)
        {
            FromFPDL(fpdl);
        }
        /// <summary>
        /// Deserialise Federation object from FPDL
        /// </summary>
        /// <param name="fpdl"></param>
        public void FromFPDL(XElement fpdl)
        {
            if (fpdl.Name != "federation")
                throw new ApplicationException("Cannot parse: Not an FPDL federation description");
            try
            {
                Name = fpdl.Element("description").Element("name").Value;
                Summary = fpdl.Element("description").Element("summary").Value;
                if (fpdl.Element("description").Element("scenario") != null)
                    Scenario = fpdl.Element("description").Element("scenario").Value;
                foreach (XElement federate in fpdl.Descendants("federate"))
                    Federates.Add(new Federate(federate));
            }
            catch (NullReferenceException e)
            {
                throw new ApplicationException("Federation parse error: " + e.Message);
            }
        }
        /// <summary>
        /// Serialise Federation object to FPDL
        /// </summary>
        /// <returns></returns>
        public XElement ToFPDL()
        {
            XElement fpdl = new XElement("federation",
                new XElement("description", 
                    new XElement("name", Name),
                    new XElement("summary", Summary))
                );
            if (Scenario != null)
                fpdl.Element("description").Add(new XElement("scenario", Scenario));
            foreach (Federate federate in Federates)
                fpdl.Add(federate.ToFPDL());
            return fpdl;
        }
        /// <summary>
        /// String representation of Component object
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            str.AppendFormat("  Name: {0}\n", Name);
            str.AppendFormat("  Summary: {0}\n", Summary);
            if (Scenario != null)
                str.AppendFormat("  Scenario: {0}\n", Scenario);
            foreach (Federate federate in Federates)
                str.Append(federate.ToString());
            return str.ToString();
        }
    }
}