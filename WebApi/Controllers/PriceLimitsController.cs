using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class PriceLimitsController : ApiController
    {
        private CarPoolDBContext db = new CarPoolDBContext();

        // GET: api/PriceLimits
        public IQueryable<PriceLimit> GetPriceLimits()
        {
            return db.PriceLimits;
        }

        // GET: api/PriceLimits/5
        [ResponseType(typeof(PriceLimit))]
        public IHttpActionResult GetPriceLimit(int id)
        {
            PriceLimit priceLimit = db.PriceLimits.Find(id);
            if (priceLimit == null)
            {
                return NotFound();
            }

            return Ok(priceLimit);
        }

        // PUT: api/PriceLimits/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPriceLimit(int id, PriceLimit priceLimit)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != priceLimit.CarType)
            {
                return BadRequest();
            }

            db.Entry(priceLimit).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PriceLimitExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/PriceLimits
        [ResponseType(typeof(PriceLimit))]
        public IHttpActionResult PostPriceLimit(PriceLimit priceLimit)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PriceLimits.Add(priceLimit);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (PriceLimitExists(priceLimit.CarType))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = priceLimit.CarType }, priceLimit);
        }

        // DELETE: api/PriceLimits/5
        [ResponseType(typeof(PriceLimit))]
        public IHttpActionResult DeletePriceLimit(int id)
        {
            PriceLimit priceLimit = db.PriceLimits.Find(id);
            if (priceLimit == null)
            {
                return NotFound();
            }

            db.PriceLimits.Remove(priceLimit);
            db.SaveChanges();

            return Ok(priceLimit);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PriceLimitExists(int id)
        {
            return db.PriceLimits.Count(e => e.CarType == id) > 0;
        }
    }
}