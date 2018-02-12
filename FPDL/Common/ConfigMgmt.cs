using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FPDL.Common
{
    /// <summary>
    /// FPDL ConfigMgmt type
    /// </summary>
    public class ConfigMgmt
    {
        /// <summary>
        /// National owner
        /// </summary>
        public string SecurityOwner { get; private set; }
        /// <summary>
        /// Classification
        /// </summary>
        public string SecurityClassification { get; private set; }
        /// <summary>
        /// Current version
        /// </summary>
        public Tuple<int, int> CurrentVersion { get; private set; }
        /// <summary>
        /// Document description
        /// </summary>
        public string Description;
        /// <summary>
        /// Creation data
        /// </summary>
        public created Created { get; private set; }
        /// <summary>
        /// Document reference
        /// </summary>
        public Guid DocReference { get; private set; }
        /// <summary>
        /// Changes
        /// </summary>
        public IReadOnlyList<changed> Changed { get { return _changed; } }

        private List<changed> _changed = new List<changed>();
        private bool initialised = false;

        /// <summary>
        /// Construct ConfigMgmt object
        /// </summary>
        public ConfigMgmt()
        {
            initialised = false;
        }
        /// <summary>
        /// Construct ConfigMgmt object from FPDL
        /// </summary>
        /// <param name="fpdl"></param>
        public ConfigMgmt(XElement fpdl)
        {
            FromFPDL(fpdl);
        }
        /// <summary>
        /// Deserialise ConfigMgmt object from FPDL
        /// </summary>
        /// <param name="fpdl"></param>
        public void FromFPDL(XElement fpdl)
        {
            if (fpdl == null)
                throw new ApplicationException("Null configMgmt description");
            if (fpdl.Name != "configMgmt")
                throw new ApplicationException("Cannot parse: Not an FPDL configMgmt description");
            try
            {
                SecurityOwner = fpdl.Element("securityOwner").Value;
                SecurityClassification = fpdl.Element("securityClassification").Value;
                CurrentVersion = VersionFromString(fpdl.Element("currentVersion").Value);
                Description = fpdl.Element("description").Value;
                Created = new created
                {
                    date = DateTime.Parse(fpdl.Element("created").Element("date").Value),
                    author = fpdl.Element("created").Element("author").Value,
                    initialVersion = VersionFromString(fpdl.Element("created").Element("initialVersion").Value)
                };
                DocReference = Guid.Parse(fpdl.Element("docReference").Value);
                foreach (XElement change in fpdl.Elements("changed"))
                {
                    changed _ch = new changed
                    {
                        date = DateTime.Parse(change.Element("date").Value),
                        author = change.Element("author").Value,
                        newVersion = VersionFromString(change.Element("newVersion").Value),
                        prevDocReference = Guid.Parse(change.Element("prevDocReference").Value),
                        changeNotes = change.Element("changeNotes").Value
                    };
                    _changed.Add(_ch);
                }
            }
            catch (NullReferenceException e)
            {
                throw new ApplicationException("Cannot parse: " + e.Message);
            }
            initialised = true;
        }
        /// <summary>
        /// Serialise ConfigMgmt object to FPDL
        /// </summary>
        /// <returns></returns>
        public XElement ToFPDL()
        {
            XElement fpdl = new XElement("configMgmt",
                new XElement("SecurityOwner", SecurityOwner),
                new XElement("securityClassification", SecurityClassification),
                new XElement("currentVersion", VersionToString(CurrentVersion)),
                new XElement("description", Description),
                new XElement("created",
                    new XElement("date", Created.date.ToUniversalTime()),
                    new XElement("author", Created.author),
                    new XElement("initialVersion", VersionToString(Created.initialVersion))
                    ),
                new XElement("docReference", DocReference.ToString())
                );
            foreach (changed change in _changed)
            {
                XElement _c = new XElement("changed",
                    new XElement("date", change.date.ToUniversalTime()),
                    new XElement("author", change.author),
                    new XElement("newVersion", VersionToString(change.newVersion)),
                    new XElement("prevDocReference", change.prevDocReference.ToString()),
                    new XElement("changeNotes", change.changeNotes)
                );
                fpdl.Add(_c);
            }
            return fpdl;
        }
        /// <summary>
        /// Initialise a ConfigMgmt object
        /// </summary>
        /// <param name="author">Athor name</param>
        /// <param name="Major">Major version number</param>
        /// <param name="Minor">Minor version number</param>
        /// <param name="changeNotes">Change notes</param>
        /// <param name="owner">National owner</param>
        /// <param name="classification">Classification</param>
        /// <exception cref="ApplicationException">Attempt to initialise an initialised configMgmt entry</exception>
        public void Initialise(string author, int Major, int Minor, string changeNotes, string owner="UK", string classification="OFFICIAL")
        {
            if (initialised)
                throw new ApplicationException("Attempt to initialise an initialised configMgmt entry");

            SecurityOwner = owner;
            SecurityClassification = classification;
            CurrentVersion = Tuple.Create(Major, Minor);
            Description = changeNotes;
            DocReference = new Guid();

            Created = new created
            {
                date = DateTime.UtcNow,
                author = author,
                initialVersion = Tuple.Create(Major, Minor)
            };

            changed _ch = new changed
            {
                date = Created.date,
                author = Created.author,
                newVersion = Created.initialVersion,
                prevDocReference = DocReference,
                changeNotes = changeNotes
            };
            _changed.Insert(0, _ch);
            initialised = true;
        }
        /// <summary>
        /// Issue a new version of a document
        /// </summary>
        /// <param name="author">Author name</param>
        /// <param name="Major">Major version number</param>
        /// <param name="Minor">Minor version number</param>
        /// <param name="changeNotes">Change notes</param>
        public void NewVersion(string author, int Major, int Minor, string changeNotes)
        {
            // should check new version num > old version num
            changed _c = new changed
            {
                date = DateTime.UtcNow,
                author = author,
                newVersion = Tuple.Create(Major, Minor),
                prevDocReference = DocReference,
                changeNotes = changeNotes
            };
            DocReference = new Guid();
            CurrentVersion = Tuple.Create(Major, Minor);
            _changed.Insert(0, _c);
        }
        /// <summary>
        /// String representation of a ConfigMgmt object
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder str = new StringBuilder("Configuration Management: \n");
            str.AppendFormat("  Classification: {0} {1}\n", SecurityOwner, SecurityClassification);
            str.AppendFormat("  Version: {0} Reference: {1}\n", CurrentVersion, DocReference);
            str.AppendFormat("  Description: {0}\n", Description);
            str.AppendFormat("  {0}", Created);
            foreach (changed ch in Changed)
                str.AppendFormat("  {0}", ch);
            return str.ToString();
        }
        /// <summary>
        /// Created type
        /// </summary>
        public struct created
        {
            /// <summary>
            /// Creation data
            /// </summary>
            public DateTime date;
            /// <summary>
            /// Author
            /// </summary>
            public string author;
            /// <summary>
            /// First version number
            /// </summary>
            public Tuple<int, int> initialVersion;
            /// <summary>
            /// String representation of Created type
            /// </summary>
            /// <returns></returns>
            public override string ToString()
            {
                StringBuilder str = new StringBuilder("  Created: ");
                str.AppendFormat("{0} By: {1}; Version: {2}\n", date.ToShortDateString(), author, initialVersion);
                return str.ToString();
            }
        }
        /// <summary>
        /// Changed type
        /// </summary>
        public struct changed
        {
            /// <summary>
            /// Date of change
            /// </summary>
            public DateTime date;
            /// <summary>
            /// Author
            /// </summary>
            public string author;
            /// <summary>
            /// New version number
            /// </summary>
            public Tuple<int, int> newVersion;
            /// <summary>
            /// Reference of the last version
            /// </summary>
            public Guid prevDocReference;
            /// <summary>
            /// Change notes
            /// </summary>
            public string changeNotes;
            /// <summary>
            /// String representation of a Changed type
            /// </summary>
            /// <returns></returns>
            public override string ToString()
            {
                StringBuilder str = new StringBuilder("  Changed: ");
                str.AppendFormat("{0} By: {1}; Version: {2}\n", date.ToShortDateString(), author, newVersion);
                return str.ToString();
            }
        }
        /// <summary>
        /// Create version from string
        /// </summary>
        /// <param name="value">Version string</param>
        /// <returns>Version tuple</returns>
        public static Tuple<int, int> VersionFromString(string value)
        {
            string[] bits = value.Split('.');
            int major = Convert.ToInt32(bits[0]);
            int minor = Convert.ToInt32(bits[1]);
            return Tuple.Create(major, minor);
        }
        /// <summary>
        /// Create string from version tuple
        /// </summary>
        /// <param name="version">Version tuple</param>
        /// <returns>Version string</returns>
        public static string VersionToString(Tuple<int,int> version)
        {
            string major = version.Item1.ToString();
            string minor = version.Item2.ToString();
            return major + "." + minor;
        }
    }
}
    


