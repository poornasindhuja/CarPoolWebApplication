using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using CarPool.Services;
using CarPool.Models;
using System.Web.Http;

namespace CarPool.Api.Controllers
{
    public class UsersController : ApiController
    {
        IUserServices userService = new UserServices();

        // GET: api/Users/5
        public User Get(int id)
        {
            return userService.GetUser(id);
        }

        // POST: api/Users
        public void Post([FromBody]string value)
        {

        }

        // PUT: api/Users/5
        public void Put(int id, [FromBody]string value)
        {

        }

        // DELETE: api/Users/5
        public void Delete(int id)
        {

        }
    }
}
