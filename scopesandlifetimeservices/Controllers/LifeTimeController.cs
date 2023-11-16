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

        [HttpGet("process")]
        public ActionResult Process()
        { 
            var message =
                $"<div class='single'> GetSingletonID <ul><li><b>Sample Service </b>{_sample.GetSingletonID()}<li><b>ISingletonService Service</b> {_singletonService.SingletonId}</ul></div>" +
                $"<div class='scope'>GetScopedID <ul><li><b>Sample Service </b>{_sample.GetScopedID()}<li><b>IScopedService Service </b>{_scopedService.ScopeId}</ul></div>" +
                $"<div class='transient'>GetTransientID <ul><li><b>Sample Service </b>{_sample.GetTransientID()}<li><b>ITransientService Service</b> {_transientService.TransientId}</ul></div>";

            return new ContentResult
            {
                Content = message,
                ContentType = "text/html"
            };
        }

        [HttpGet("request")]
        public async Task<ActionResult> RequestMade()
        {

            using HttpClient client = new HttpClient();            
            var responseMessage = await client.GetAsync("https://localhost:44365/api/LifeTime/process");

            using HttpClient client1 = new HttpClient();
            var responseMessage1 = await client1.GetAsync("https://localhost:44365/api/LifeTime/process");
            using HttpClient client2 = new HttpClient();

            var responseMessage2 = await client2.GetAsync("https://localhost:44365/api/LifeTime/process");
            return new ContentResult
            {
                Content = "<!DOCTYPE html><html lang=en><meta charset=UTF-8><meta content=\"width=device-width,initial-scale=1\"name=viewport><title>Scopes and Lifetime Management</title><center><h1>Scopes and Lifetime Management</h1></center> <style>.single{border: 3px red solid;margin: 5px;padding: 5px;}.scope{border: 3px green solid;margin: 5px;padding: 5px;}.transient{border: 3px blue solid;margin: 5px;padding: 5px;}</style>" +
                $"<table><tr><td>{responseMessage.Content.ReadAsStringAsync().Result}</td><td> {responseMessage1.Content.ReadAsStringAsync().Result}</td><td> {responseMessage2.Content.ReadAsStringAsync().Result}</td></tr></table>",
                ContentType = "text/html"
            };
        }
    }
}
