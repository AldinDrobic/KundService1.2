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
    public class NotiserController : ApiController
    {
        private KundModell db = new KundModell();
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        // GET: api/Notiser
        public IQueryable<Notis> GetNotis()
        {
            return db.Notis;
        }

        // GET: api/Notiser/5
        [ResponseType(typeof(Notis))]
        public IHttpActionResult GetNotis(int id)
        {
            Notis notis = db.Notis.Find(id);
            if (notis == null)
            {
                Logger.Error("Gick inte att hitta notiser");
                return NotFound();
            }

            return Ok(notis);
        }

        // PUT: api/Notiser/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutNotis(int id, Notis notis)
        {
            if (!ModelState.IsValid)
            {
                Logger.Error("Fel format på angivna data");
                return BadRequest(ModelState);
            }

            if (id != notis.Id)
            {
                Logger.Error("Gick inte att hitta notis med id:t");
                return BadRequest();
            }

            db.Entry(notis).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NotisExists(id))
                {
                    Logger.Error("Notisen finns inte");
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Notiser
        [ResponseType(typeof(Notis))]
        public IHttpActionResult PostNotis(Notis notis)
        {
            if (!ModelState.IsValid)
            {
                Logger.Error("Fel format på angivna data");
                return BadRequest(ModelState);
            }

            db.Notis.Add(notis);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = notis.Id }, notis);
        }

        // DELETE: api/Notiser/5
        [ResponseType(typeof(Notis))]
        public IHttpActionResult DeleteNotis(int id)
        {
            Notis notis = db.Notis.Find(id);
            if (notis == null)
            {
                Logger.Error("Gick inte att hitta notisen");
                return NotFound();
            }

            db.Notis.Remove(notis);
            db.SaveChanges();

            return Ok(notis);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool NotisExists(int id)
        {
            return db.Notis.Count(e => e.Id == id) > 0;
        }
    }
}