using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Web.Hosting;
using System.Web.Http;
using Our.Umbraco.NuGetPackageDashboard.Core.Services;
using Umbraco.Core;
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
                    throw new FileNotFoundException("Unable to find Packages.config file.", filePath);
                }

                var service = new XmlConfigFileNuGetPackageService();
                var packages = service.GetPackages(filePath);

                return Request.CreateResponse(packages);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }
    }
}
