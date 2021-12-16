using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;

namespace FunctionAppFramework
{
    public static class Function1
    {

        //az functionapp deployment source config-zip  -g SharperLending -n SLTest2 --src c:\temp\SLTest2\FunctionUpload.zip
        [FunctionName("Function1")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", 
            Route = null)]HttpRequestMessage req, TraceWriter log, ExecutionContext context)
        {
            log.Info("C# HTTP trigger function processed a request.");

            // parse query parameter
            string name = req.GetQueryNameValuePairs()
                .FirstOrDefault(q => string.Compare(q.Key, "name", true) == 0)
                .Value;

            if (name == null)
            {
                // Get request body
                dynamic data = await req.Content.ReadAsAsync<object>();
                name = data?.name;
            }


            try
            {
                COMExample.COMClass c = new COMExample.COMClassClass();
                var st = c.Version();

                //int step = 0;
                //Guid clsid = Guid.Parse("AB08EAB9-210D-4EEE-9C21-64E5CCBE37EE");
                //var assemblies = new NRegFreeCom.AssemblySystem();
                //step++;

                //step++;
                ////var createdDirectly = NRegFreeCom.ActivationContext.CreateInstanceWithManifest(clsid, Path.Combine(contentPath, @"bin\COMExample.dll.manifest")) as COMExample.COMClass;
                //var createdDirectly = NRegFreeCom.ActivationContext.CreateInstanceWithManifest(clsid, Path.Combine(context.FunctionAppDirectory, @"bin\COMExample.dll.manifest")) as COMExample.COMClass;
                //step++;
                //st = createdDirectly.Version() as string;
                //step++;

                return req.CreateResponse(HttpStatusCode.OK, st);
            }
            catch (Exception ex)
            {
                return req.CreateResponse(HttpStatusCode.OK, ex.Message);
            }

            return name == null
                ? req.CreateResponse(HttpStatusCode.BadRequest, "Please pass a name on the query string or in the request body")
                : req.CreateResponse(HttpStatusCode.OK, "Hello " + name);
        }
    }
}
