using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Our.Umbraco.NuGetPackageDashboard.Core.Models;

namespace Our.Umbraco.NuGetPackageDashboard.Core.Services
{
    internal class XmlConfigFileNuGetPackageService : INuGetPackageService
    {
        public IEnumerable<NuGetPackage> GetPackages(string filePath)
        {
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                return GetPackages(stream);
            }
        }

        public IEnumerable<NuGetPackage> GetPackages(Stream stream)
        {
            var serializer = new XmlSerializer(typeof(NuGetPackageCollection), new XmlRootAttribute(PackageConstants.XmlConfigSettings.RootElementName));
            var reader = new StreamReader(stream);
            var packages = (NuGetPackageCollection)serializer.Deserialize(reader);

            return packages != null ? packages.NuGetPackages : Enumerable.Empty<NuGetPackage>();
        }
    }
}
