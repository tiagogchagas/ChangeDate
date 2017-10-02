using ChangeDate.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Http;

namespace ChangeDate.Controllers
{
    public class ChangeDateController : ApiController
    {

        private ChangeDateModel ChangeDateModel = new ChangeDateModel();
        public static string GlobalDate = "01/01/1900 00:00";
        // GET api/<controller>
        [HttpGet]
        public string Get()
        {
            return GlobalDate;
        }

        // PUT api/<controller>
        [HttpPut]
        public IHttpActionResult Put([FromBody]DateTimeRequest date)
        {
            try
            {
                char operation = date.op[0];
                string _ret = ChangeDateModel.ChangeDate(date.date, operation, date.value);
                if (_ret == "500")
                {
                    return InternalServerError();
                }
                else
                {
                    GlobalDate = _ret;
                    return Ok(GlobalDate);
                }
            }
            catch(Exception ex)
            {
                return InternalServerError();
            }
        }

        public class DateTimeRequest
        {
            public string date;
            public string op;
            public long value;
        }
    }
}