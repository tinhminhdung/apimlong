using System.Web.Http;
using OAuthTokenBasedRestService;
using Swashbuckle.Application;
using System;
using System.Web;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace OAuthTokenBasedRestService
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                {
                    c.SingleApiVersion("v1", "API News");

                    c.IncludeXmlComments(GetXmlCommentsPath(thisAssembly.GetName().Name));
                    c.IncludeXmlComments(GetXmlCommentsPath("WebAPI"));

                })
                .EnableSwaggerUi(c =>
                {
                    // c.EnableApiKeySupport("apiKey", "header");
                    c.EnableApiKeySupport("Authorization", "header");
                });
        }
        protected static string GetXmlCommentsPath(string name)
        {
            return string.Format(@"{0}\bin\{1}.XML", AppDomain.CurrentDomain.BaseDirectory, name);
        }
    }


}