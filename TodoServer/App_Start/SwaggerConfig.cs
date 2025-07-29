using System.Web.Http;
using WebActivatorEx;
using Swashbuckle.Application;
using System.IO;

[assembly: PreApplicationStartMethod(typeof(TodoServer.App_Start.SwaggerConfig), "Register")]

namespace TodoServer.App_Start
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            GlobalConfiguration.Configuration.EnableSwagger(c =>
                {
                    c.SingleApiVersion("v1", "TodoServer API");

                    var xmlPath = System.Web.Hosting.HostingEnvironment.MapPath("~/bin/TodoServer.xml");
                    if (File.Exists(xmlPath))
                        c.IncludeXmlComments(xmlPath);
                })
                .EnableSwaggerUi();
        }
    }
}
