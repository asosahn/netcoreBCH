using System;
using Microsoft.AspNetCore.Mvc;
using test2.Entities;
using test2.Services;

namespace test2.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class QueueController : ControllerBase
    {

        private readonly ISendQueue sendQueue;
        public QueueController(ISendQueue sendQueue)
        {
            this.sendQueue = sendQueue;

        }

        [HttpPost]
        public ActionResult<Queue> Post([FromBody] Queue queue) {
            try {
            var queue_ = sendQueue.sendToQueue(queue);

            return Ok(queue_);

            } catch (Exception e) {
                return BadRequest(e.Message);
            }
        }

    }
}