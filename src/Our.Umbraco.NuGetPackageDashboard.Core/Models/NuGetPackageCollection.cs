using System;
using System.Xml.Serialization;

namespace Our.Umbraco.NuGetPackageDashboard.Core.Models
{
    [Serializable]
    [XmlRoot(PackageConstants.XmlConfigSettings.RootElementName)]
    internal class NuGetPackageCollection
    {
        [XmlElement(PackageConstants.XmlConfigSettings.PackageElementName, typeof(NuGetPackage))]
        public NuGetPackage[] NuGetPackages { get; set; }
    }
}