using System.Collections.Generic;
using System.IO;
using Our.Umbraco.NuGetPackageDashboard.Core.Models;

namespace Our.Umbraco.NuGetPackageDashboard.Core.Services
{
    internal interface INuGetPackageService
    {
        IEnumerable<NuGetPackage> GetPackages(string filePath);
        IEnumerable<NuGetPackage> GetPackages(Stream stream);
    }
}
