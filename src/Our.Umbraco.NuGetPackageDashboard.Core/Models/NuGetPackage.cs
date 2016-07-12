using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Our.Umbraco.NuGetPackageDashboard.Core.Models
{
    [XmlType]
    public class NuGetPackage
    {
        [XmlAttribute("id")]
        public string Id { get; set; }
        [XmlAttribute("version")]
        public string Version { get; set; }
        [XmlAttribute("targetFramework")]
        public string TargetFramework { get; set; }
    }
}
