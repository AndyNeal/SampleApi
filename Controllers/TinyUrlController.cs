using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace SampleApi.Controllers
{
    [Route("/")]
    public class TinyUrlController : Controller
    {
        ITinyUrlStore _store;
        public TinyUrlController(ITinyUrlStore store)
        {
            _store = store;
        }

        [HttpPost]
        public ActionResult Create([FromBody] TinyUrl input)
        {
            // still need to hand key generation
            var result = _store.Create(input);
            //PrependShemeAndHostname(result);
            return Created(Request.Scheme + "://" + Request.Host + "/" + result.Record.Key, result);
        }

        [HttpGet]
        [Route("/{key}")]
        [Route("/")]
        public ActionResult Read(string key)
        {
            var result = _store.Read(key);
            if (result.Success)
            {
                return Redirect(result.Record.Url);
            }
            else
            {
                return new NotFoundObjectResult(result);
            }
        }

        [HttpPut]
        public ActionResult Update([FromBody]TinyUrl input)
        {
            var result = _store.Update(input);
            //PrependShemeAndHostname(result);
            return Ok(result);
        }

        [HttpDelete]
        [Route("/{key}")]
        public ActionResult Delete(string key)
        {
            _store.Delete(key);
            return Ok();
        }


        private void PrependShemeAndHostname(TinyUrlResponse resp)
        {
            resp.Record.Key = Request.Scheme + "://" + Request.Host + "/" + resp.Record.Key;
        }
    }
}
