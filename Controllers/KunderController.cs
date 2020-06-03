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
using KundService1._2;

namespace KundService1._2.Controllers
{
    public class KunderController : ApiController
    {
        private KundModell db = new KundModell();
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        // GET: api/Kunder
        public IQueryable<Kund> GetKund()
        {
            return db.Kund;
        }

        // GET: api/Kunder/5
        [ResponseType(typeof(Kund))]
        public IHttpActionResult GetKund(int id)
        {
            Kund kund = db.Kund.Find(id);
            if (kund == null)
            {
                Logger.Error("Gick inte att hitta kunden");
                return NotFound();
            }

            return Ok(kund);
        }

        // PUT: api/Kunder/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutKund(int id, Kund kund)
        {
            if (!ModelState.IsValid)
            {
                Logger.Error("Fel format på angivna data");
                return BadRequest(ModelState);
            }

            if (id != kund.InloggningsId)
            {
                Logger.Error("Gick inte att hitta kund med id:t");
                return BadRequest();
            }

            db.Entry(kund).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KundExists(id))
                {
                    Logger.Error("Kunden finns inte");
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Kunder
        [ResponseType(typeof(Kund))]
        public IHttpActionResult PostKund(Kund kund)
        {
            if (!ModelState.IsValid)
            {
                Logger.Error("Fel format på angivna data");
                return BadRequest(ModelState);
            }

            db.Kund.Add(kund);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (KundExists(kund.InloggningsId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = kund.InloggningsId }, kund);
        }

        // DELETE: api/Kunder/5
        [ResponseType(typeof(Kund))]
        public IHttpActionResult DeleteKund(int id)
        {
            Kund kund = db.Kund.Find(id);
            if (kund == null)
            {
                Logger.Error("Gick inte att hitta kunden");
                return NotFound();
            }

            db.Kund.Remove(kund);
            db.SaveChanges();

            return Ok(kund);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool KundExists(int id)
        {
            return db.Kund.Count(e => e.InloggningsId == id) > 0;
        }
    }
}