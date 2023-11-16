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
            var message = $"Sample Service GetSingletonID {_sample.GetSingletonID()}, ISingletonService Service {_singletonService.SingletonId} " +
                $"Sample Service GetScopedID {_sample.GetScopedID()}, IScopedService Service {_scopedService.ScopeId} " +
                $"Sample Service GetTransientID {_sample.GetTransientID()}, ITransientService Service {_transientService.TransientId}";
            return Ok(message);
        }
    }
}
