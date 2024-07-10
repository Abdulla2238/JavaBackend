using LoginApiTest.Services.Contract;
using LoginApiTest.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace LoginApiTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ITestLog _testLog;

        public ValuesController(ITestLog testLog)
        {
            _testLog = testLog;
        }

        [HttpPost]
        public IActionResult postCustomerDetails(UserResponseAPI loginapi)
        {
            ResponseAPI<UserResponseAPI> responce = new ResponseAPI<UserResponseAPI>();

            try
            {
                UserResponseAPI newdeatil = _testLog.AddUser(loginapi);
                responce = new ResponseAPI<UserResponseAPI>
                {
                    status = true,
                    msg = "Added",
                    Value = newdeatil
                };
                return StatusCode(StatusCodes.Status200OK, responce);
            }
            catch (Exception ex)
            {
                responce = new ResponseAPI<UserResponseAPI> { msg = ex.Message };
                return StatusCode(StatusCodes.Status500InternalServerError, responce);
            }
        }

        [HttpPost("verify")]
        public ActionResult<UserResponseAPI> CheckUser([FromBody] UserResponseAPI log)
        {
            try
            {
                var response = _testLog.Verify(log);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new UserResponseAPI { Log = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult Get()
        {
            ResponseAPI<List<UserResponseAPI>> response = new ResponseAPI<List<UserResponseAPI>>();
            try
            {
                List<UserResponseAPI> pricelist = _testLog.GetList();
                response = new ResponseAPI<List<UserResponseAPI>> { msg = "price generated", Value = pricelist };
                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                response = new ResponseAPI<List<UserResponseAPI>> { msg = ex.Message };
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

       
    }
}
