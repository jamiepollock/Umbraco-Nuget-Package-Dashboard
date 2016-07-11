using System;
using System.Xml.Serialization;

namespace Our.Umbraco.NuGetPackageDashboard.Core.Models
{
    [Serializable]
    [XmlType]
    internal class NuGetPackage
    {
        [XmlAttribute("id")]
        public string Id { get; set; }
        [XmlAttribute("version")]
        public string Version { get; set; }
        [XmlAttribute("targetFramework")]
        public string TargetFramework { get; set; }
    }
}
