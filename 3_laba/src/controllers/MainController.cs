using _3_laba.src.services;
using _3_laba.src.TextClassificationTF.classes;
using Microsoft.AspNetCore.Mvc;

namespace _3_laba.src.controllers
{
    public class MainController : ControllerBase
    {
        private readonly MainService _service;

        public MainController(MainService service)
        {
            _service = service;
        }

        [HttpGet("get-result")]
        public IActionResult GetResult()
        {
            var result = _service.GetResult();
            return Ok(result);
        }

        [HttpPost("start")]
        public IActionResult MakeStart()
        {
           _service.StartTraining();
            return Ok();
        }

        [HttpGet("status")]
        public IActionResult GetModelStatus()
        {
            return Ok(_service.GetModelStatus());
        }

        [HttpPost("predict")]
        public IActionResult Predict([FromBody] MovieReview review)
        {
            return Ok(_service.Predict(review));
        }
    }
}
