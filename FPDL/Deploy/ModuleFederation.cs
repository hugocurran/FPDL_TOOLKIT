using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FPDL.Deploy
{
    /// <summary>
    /// Federation Module
    /// </summary>
    public class ModuleFederation : IModule
    {
        /// <summary>
        /// Federation Name
        /// </summary>
        public string FederationName;
        /// <summary>
        /// Federate Name
        /// </summary>
        public string FederateName;
        /// <summary>
        /// Interface Binding
        /// </summary>
        public string InterfaceName;
        /// <summary>
        /// RTI specification
        /// </summary>
        public Rti RTI;

        /// <summary>
        /// Construct Federation Module
        /// </summary>
        public ModuleFederation() { }
        /// <summary>
        /// Construct Federation Module from FPDL
        /// </summary>
        /// <param name="fpdl"></param>
        public ModuleFederation(XElement fpdl)
        {
            FromFPDL(fpdl);
        }
        /// <summary>
        /// Deserialise Module from FPDL
        /// </summary>
        /// <param name="fpdl"></param>
        public void FromFPDL(XElement fpdl)
        {
            if (fpdl.Name != "federation")
                throw new ApplicationException("Cannot parse: Not an FPDL federation description");
            try
            {
                FederationName = fpdl.Element("federationName").Value;
                FederateName = fpdl.Element("federateName").Value;
                InterfaceName = fpdl.Element("interfaceName").Value;
                RTI = new Rti
                {
                    CrcAddress = fpdl.Element("rti").Element("crcAddress").Value,
                    AddressType = fpdl.Element("rti").Element("crcAddress").Attribute("addressType").Value,
                    CrcPortNumber = fpdl.Element("rti").Element("crcPortNumber").Value,
                    HlaSpec = fpdl.Element("rti").Element("hlaSpec").Value,
                };
                XElement foo = fpdl.Element("rti").Element("fom");
                if (fpdl.Element("rti").Element("fom").Element("uri") != null)
                {
                    foreach (XElement fom in fpdl.Element("rti").Element("fom").Elements("uri"))
                        RTI.FomUri.Add(fom.Value);
                }
                if (fpdl.Element("rti").Element("fom").Element("fileName") != null)
                {
                    foreach (XElement fom in fpdl.Element("rti").Element("fom").Elements("fileName"))
                        RTI.FomFile.Add(fom.Value);
                }
            }
            catch (NullReferenceException e)
            {
                throw new ApplicationException("Cannot parse: " + e.Message);
            }
        }
        /// <summary>
        /// Serialise Module to FPDL
        /// </summary>
        /// <returns></returns>
        public XElement ToFPDL()
        {
            XElement fpdl = new XElement("federation",
               new XElement("federationName", FederationName),
               new XElement("federateName", FederateName),
               new XElement("interfaceName", InterfaceName)
            );

            XElement rti = new XElement("rti",
                new XElement("crcAddress", RTI.CrcAddress,
                    new XAttribute("addressType", RTI.AddressType)),
                new XElement("crcPortNumber", RTI.CrcPortNumber),
                new XElement("hlaSpec", RTI.HlaSpec)
                );
            foreach (string fom in RTI.FomUri)
                rti.Add(new XElement("uri", fom));
            foreach (string fom in RTI.FomFile)
                rti.Add(new XElement("uri", fom));
            return fpdl;
        }

        /// <summary>
        /// String representation of Federation module
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder str = new StringBuilder("  Federation:\n");
            str.AppendFormat("\tFederation Name: {0}\n", FederationName);
            str.AppendFormat("\tFederate Name: {0}\n", FederateName);
            str.AppendFormat("\tInterface Name: {0}\n", InterfaceName);
            str.AppendFormat("\tRTI: CRC Address: {0}\n", RTI.CrcAddress);
            str.AppendFormat("\tRTI: CRC Port: {0}\n", RTI.CrcPortNumber);
            str.AppendFormat("\tRTI HLA Spec: {0}\n", RTI.HlaSpec);
            foreach (string fom in RTI.FomUri)
                str.AppendFormat("\tFOM: {0}\n", fom);
            foreach (string fom in RTI.FomFile)
                str.AppendFormat("\tFOM: {0}\n", fom);
            return str.ToString();
        }

    }

    /// <summary>
    /// RTI class
    /// </summary>
    public class Rti
    {
        /// <summary>
        /// CRC Address
        /// </summary>
        public string CrcAddress;
        /// <summary>
        /// Address Type
        /// </summary>
        public string AddressType;
        /// <summary>
        /// CRC Port number
        /// </summary>
        public string CrcPortNumber;
        /// <summary>
        /// HLA Specification
        /// </summary>
        public string HlaSpec;
        /// <summary>
        /// URI of FOM modules
        /// </summary>
        public List<string> FomUri = new List<string>();
        /// <summary>
        /// Filenames of FOM modules
        /// </summary>
        public List<string> FomFile = new List<string>();
    }
}
