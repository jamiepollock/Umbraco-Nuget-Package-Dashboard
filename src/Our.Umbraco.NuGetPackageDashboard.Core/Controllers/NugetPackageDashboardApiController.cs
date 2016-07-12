using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Web.Hosting;
using System.Web.Http;
using Our.Umbraco.NuGetPackageDashboard.Core.Models;
using Our.Umbraco.NuGetPackageDashboard.Core.Services;
using Umbraco.Core;
using Umbraco.Core.Cache;
using Umbraco.Core.Logging;
using Umbraco.Web.WebApi;
using Umbraco.Web.WebApi.Filters;

namespace Our.Umbraco.NuGetPackageDashboard.Core.Controllers
{
    [UmbracoApplicationAuthorize(Constants.Applications.Developer)]
    public class NuGetPackageDashboardApiController : UmbracoAuthorizedApiController
    {
        [HttpGet]
        public HttpResponseMessage Get()
        {
            try
            {
                var filePath = HostingEnvironment.MapPath("~/Packages.config");
                if (File.Exists(filePath) == false)
                {
                    throw new FileNotFoundException("Unable to find Packages file.", filePath);
                }
                var service = new XmlConfigFileNuGetPackageService();
                var packages = ApplicationContext.ApplicationCache.RuntimeCache.GetCacheItem<IEnumerable<NuGetPackage>>(
                    PackageConstants.CacheKey, () => service.GetPackages(filePath));

                LogHelper.Info(typeof(NuGetPackageDashboardApiController), "Added serialized Packages file (path: \"{0}\") into ApplicationCache.RuntimeCache (key: \"{1}\").", () => new[] { filePath, PackageConstants.CacheKey });
                return Request.CreateResponse(packages);
            }
            catch (Exception ex)
            {
                LogHelper.Error(typeof(NuGetPackageDashboardApiController), "Unable to process Get API call.", ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }
    }
}
