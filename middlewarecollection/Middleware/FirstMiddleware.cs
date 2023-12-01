using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using MiddlewareCollections.Controllers;
using System;
using System.Diagnostics;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace MiddlewareCollections.Middleware
{
    public class FirstMiddleware(RequestDelegate next,Guid guid)
    {
        private readonly RequestDelegate _next = next;

        public async Task Invoke(HttpContext context)
        {
            Debug.WriteLine(" FirstMiddleware ");
            Debug.WriteLine("*****************");

            Debug.WriteLine($"Entry. ID-> {guid:n} {DateTime.UtcNow:dd/MM/yyyy hh:mm:ss.fff}");
            
            Thread.Sleep(1000);

            // Call the next middleware in the pipeline
            await _next(context);
            
            Debug.WriteLine($"Exit. ID-> {guid:n} {DateTime.UtcNow:dd/MM/yyyy hh:mm:ss.fff}");
        }
    }
    public class SecondMiddleware(RequestDelegate next, Guid guid)
    {
        private readonly RequestDelegate _next = next;

        public async Task Invoke(HttpContext context)
        {
            Debug.WriteLine("\t|-----------------");
            Debug.WriteLine("\t|SecondMiddleware ");
            Debug.WriteLine("\t|-----------------");

            Debug.WriteLine($"\t|Entry. ID-> {guid:n} {DateTime.UtcNow:dd/MM/yyyy hh:mm:ss.fff}");

            Thread.Sleep(1000);

            // Call the next middleware in the pipeline
            await _next(context);

            Debug.WriteLine($"\t|Exit. ID-> {guid:n} {DateTime.UtcNow:dd/MM/yyyy hh:mm:ss.fff}");
            Debug.WriteLine("\t|-----------------");
        }
    }

    public class LastMiddleware(RequestDelegate next, Guid guid)
    {
        private readonly RequestDelegate _next = next;

        public async Task Invoke(HttpContext context)
        {
            Debug.WriteLine("\t\tLastMiddleware ");
            Debug.WriteLine("\t\t******************");

            Debug.WriteLine($"\t\tEntry. ID-> {guid} {DateTime.UtcNow:dd/MM/yyyy hh:mm:ss.fff}");

            Thread.Sleep(1000);

            // Call the next middleware in the pipeline
            await _next(context);

            Debug.WriteLine($"\t\tExit. ID-> {guid} {DateTime.UtcNow:dd/MM/yyyy hh:mm:ss.fff}");
        }
    }

    public class ShortCircuitValidateRequestPathMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly IActionDescriptorCollectionProvider _actionDescriptorCollectionProvider;

        private readonly List<string?> path ;

        public ShortCircuitValidateRequestPathMiddleware(RequestDelegate next, IActionDescriptorCollectionProvider actionDescriptorCollectionProvider)
        {
            _next= next;
            _actionDescriptorCollectionProvider= actionDescriptorCollectionProvider;

            /// 
            /// !!! For Example only, We need to lookup for better way.
            /// 
            path = [];
            path = _actionDescriptorCollectionProvider.ActionDescriptors.Items.Where(
              ad => ad.AttributeRouteInfo != null).Select(
                  ad => ad.AttributeRouteInfo?.Template).ToList();

            Debug.WriteLine("ShortCircuitValidateRequestPathMiddleware CTOR Called .....");
        }
       
        public async Task Invoke(HttpContext context)
        {
            Debug.WriteLine("ShorCircuit ");  
            
            if (context.Request.Path.HasValue && path.Any(x => !string.IsNullOrWhiteSpace(x) &&
                            x.ToLower().Equals(context.Request.Path.Value.ToLower().Replace("/", ""))))
            {
                // Call the next middleware in the pipeline
                await _next(context);

                Debug.WriteLine($"\t Next Middleware Executed");
            }
            else {
                Debug.WriteLine($"\t Bye pass for unknow call.");
            } 
             
        }
    }
}
