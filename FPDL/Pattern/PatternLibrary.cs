using FPDL.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace FPDL.Pattern
{
    /// <summary>
    /// Pattern Library
    /// </summary>
    public class PatternLibrary : IFpdlObject
    {
        /// <summary>
        /// ConfigMgmt object for Library document
        /// </summary>
        public ConfigMgmt ConfigMgmt { get; private set; }
        /// <summary>
        /// Library
        /// </summary>
        public IReadOnlyList<Entry> Library { get { return _library; } }

        private List<Entry> _library = new List<Entry>();
        private bool initialised = false;

        /// <summary>
        /// Construct a Library object
        /// </summary>
        public PatternLibrary()
        {
            initialised = false;
        }
        /// <summary>
        /// Construct a Library object from FPDL
        /// </summary>
        /// <param name="fpdl"></param>
        public PatternLibrary(XElement fpdl)
        {
            FromFPDL(fpdl);
        }
        /// <summary>
        /// Deserialise Library object from FPDL
        /// </summary>
        /// <param name="fpdl"></param>
        public void FromFPDL(XElement fpdl)
        {
            if (fpdl.Name != "PatternLibrary")
                throw new ApplicationException("Cannot parse: Not an FPDL pattern library");
            try
            {
                ConfigMgmt = new ConfigMgmt(fpdl.Element("configMgmt"));
                foreach (XElement _entry in fpdl.Elements("entry"))
                {
                    // Entry data is either extracted directly from the PatternLibrary doc or constructed
                    // from embedded Pattern entries
                    Entry entry;
                    if (Convert.ToBoolean(_entry.Attribute("embedded").Value))
                    {
                        entry = new Entry
                        {
                            Embedded = Convert.ToBoolean(_entry.Attribute("embedded").Value),
                            Pattern = new PatternObject(_entry.Element("Pattern"))
                        };
                        entry.Type = entry.Pattern.PatternType;
                        entry.Reference = entry.Pattern.ConfigMgmt.DocReference;
                        entry.Name = entry.Pattern.PatternName;
                        entry.Version = entry.Pattern.ConfigMgmt.CurrentVersion.ToString();
                    }
                    else
                    {
                        entry = new Entry
                        {
                            Embedded = Convert.ToBoolean(_entry.Attribute("embedded").Value),
                            Type = (Enums.PatternType)Enum.Parse(typeof(Enums.PatternType), _entry.Element("patternGenericType").Value),
                            Reference = Guid.Parse(_entry.Element("patternReference").Value),
                            Name = _entry.Element("patternName").Value,
                            Version = _entry.Element("patternVersion").Value,
                            Filename = _entry.Element("fileName").Value
                        };
                    }
                    _library.Add(entry);
                }
            }
            catch (ArgumentException e)
            {
                throw new ApplicationException("Invalid pattern type: " + e.Message);
            }
            catch (NullReferenceException e)
            {
                throw new ApplicationException("Library parse error: " + e.Message);
            }
            initialised = true;
        }

        /// <summary>
        /// Serialise Library to FPDL
        /// </summary>
        /// <returns></returns>
        public XElement ToFPDL()
        {
            XElement fpdl = new XElement("PatternLibrary",
                new XElement(ConfigMgmt.ToFPDL())
                );
            // The library entry either has embedded Pattern or a summery of a Pattern contained in a file
            foreach (Entry entry in _library)
            {
                fpdl.Add(new XElement("entry", new XAttribute("embedded", entry.Embedded.ToString())));
                if (entry.Embedded)
                {
                    fpdl.Add(entry.Pattern.ToFPDL());
                }
                else
                {
                    fpdl.Add(new XElement("patternGenericType", entry.Type.ToString()));
                    fpdl.Add(new XElement("patternName", entry.Name));
                    fpdl.Add(new XElement("patternVersion", entry.Version));
                    fpdl.Add(new XElement("patternReference", entry.Reference));
                    fpdl.Add(new XElement("fileName", entry.Filename));
                }
            }
            return fpdl;
        }

        /// <summary>
        /// Initialise a Library object
        /// </summary>
        /// <param name="author"></param>
        public void Initialise(string author)
        {
            if (initialised)
                throw new ApplicationException("Attempt to initialise an initialised pattern library");

            ConfigMgmt.Initialise(author, 1, 0, "Initial Version");
            initialised = true;
        }
        /// <summary>
        /// Add a Pattern to the Library
        /// </summary>
        /// <remarks>Store the Pattern in the library</remarks>
        /// <param name="pattern"></param>
        internal void Add(PatternObject pattern)
        {
            if (initialised)
            {
                var entry = new Entry
                {
                    Embedded = true,
                    Type = pattern.PatternType,
                    Reference = pattern.ConfigMgmt.DocReference,
                    Name = pattern.PatternName,
                    Version = ConfigMgmt.VersionToString(pattern.ConfigMgmt.CurrentVersion),
                    Pattern = pattern
                };
                _library.Add(entry);
            }
            else
                throw new ApplicationException("Attempt to update unitialised pattern library");
        }
        /// <summary>
        /// Add a Pattern to the Library
        /// </summary>
        /// <remarks>Store the pattern in a file</remarks>
        /// <param name="pattern"></param>
        /// <param name="filename"></param>
        internal void Add(PatternObject pattern, string filename)
        {
            if (initialised)
            {
                if (filename == "")
                    filename = @"Patterns\" + pattern.ConfigMgmt.DocReference.ToString() + ".xml";
                var entry = new Entry
                {
                    Embedded = false,
                    Type = pattern.PatternType,
                    Reference = pattern.ConfigMgmt.DocReference,
                    Name = pattern.PatternName,
                    Version = ConfigMgmt.VersionToString(pattern.ConfigMgmt.CurrentVersion),
                    Filename = filename
                };
                _library.Add(entry);
            }
            else
                throw new ApplicationException("Attempt to update unitialised pattern library");
        }
        /// <summary>
        /// Add a Pattern to the Library
        /// </summary>
        /// <param name="pattern"></param>
        /// <param name="author"></param>
        /// <param name="changeNotes"></param>
        /// <param name="updateMajor"></param>
        /// <param name="updateMinor"></param>
        public void Add(PatternObject pattern, string author, string changeNotes, bool updateMajor = false, bool updateMinor = true)
        {
            Add(pattern);

            int major = ConfigMgmt.CurrentVersion.Item1;
            int minor = ConfigMgmt.CurrentVersion.Item2;
            if (updateMajor)
            {
                major++;
                minor = 0;
            }
            if (updateMinor && !updateMajor)
            {
                minor++;
            }
            ConfigMgmt.NewVersion(author, major, minor, changeNotes);
        }

        /// <summary>
        /// Remove a Pattern from the Library
        /// </summary>
        /// <param name="pattern"></param>
        internal void Remove(PatternObject pattern)
        {
            Remove(pattern.ConfigMgmt.DocReference);

            int major = ConfigMgmt.CurrentVersion.Item1;
            int minor = ConfigMgmt.CurrentVersion.Item2;
                minor++;

            ConfigMgmt.NewVersion("Editor", major, minor, "Removed pattern: " + pattern.ConfigMgmt.DocReference.ToString());
        }
        /// <summary>
        /// Remove a Pattern from the Library
        /// </summary>
        /// <param name="patternRef"></param>
        public void Remove(Guid patternRef)
        {
            if (initialised)
            {
                foreach (Entry entry in _library)
                {
                    if (entry.Reference == patternRef)
                    {
                        _library.Remove(entry);
                        return;
                    }
                }
                throw new ApplicationException("Remove failed: Not in library: " + patternRef.ToString());
            }
            else
                throw new ApplicationException("Attempt to update unitialised pattern library");
        }

    }
}
