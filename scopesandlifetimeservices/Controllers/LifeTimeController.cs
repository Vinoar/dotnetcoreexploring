using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using scopesandlifetimeservices.Services;

namespace scopesandlifetimeservices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LifeTimeController : ControllerBase
    {
        readonly ISingletonService _singletonService;
        readonly IScopedService _scopedService;
        readonly ITransientService _transientService;
        readonly ISample _sample;

        public LifeTimeController(
            ISingletonService singletonService,
            IScopedService scopedService,
            ITransientService transientService,
            ISample sample)
        {
            _singletonService = singletonService;
            _scopedService = scopedService;
            _transientService = transientService;
            _sample = sample;
        }

        [HttpGet("request")]
        public ActionResult RequestMade()
        { 
            var message = $"<!DOCTYPE html><html lang=en><meta charset=UTF-8><meta content=\"width=device-width,initial-scale=1\"name=viewport><title>Scopes and Lifetime Management</title><h1>Scopes and Lifetime Management.</h1>" +
                $"GetSingletonID<ul><li><b>Sample Service </b>{_sample.GetSingletonID()}<li><b>ISingletonService Service</b> {_singletonService.SingletonId}</ul>" +
                $"GetScopedID<ul><li><b>Sample Service </b>{_sample.GetScopedID()}<li><b>IScopedService Service </b>{_scopedService.ScopeId}</ul>" +
                $"GetTransientID<ul><li><b>Sample Service </b>{_sample.GetTransientID()}<li><b>ITransientService Service</b> {_transientService.TransientId}</ul>";

            return new ContentResult
            {
                Content = message,
                ContentType = "text/html"
            };
        }
    }
}
