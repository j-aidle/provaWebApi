using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using insti;
using insti.Models;

namespace insti.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class alumnesController : ApiController
    {
        private instiEntities1 db = new instiEntities1();

        // GET: api/alumnes
        public IQueryable<AlumneDTO> Getalumne()
        {
            var alu = from a in db.alumne
                      select new AlumneDTO()
                      {
                          id = a.id,
                          nom = a.nom,
                          cognom = a.cognom,
                          edat = a.edat
                      };

            return alu;
        }

        // GET: api/alumnes/5
        [ResponseType(typeof(AlumneDTO))]
        public async Task<IHttpActionResult> Getalumne(int id)
        {
            var alum = await db.alumne.Select(a =>
                      new AlumneDTO()
                      {
                          id = a.id,
                          nom = a.nom,
                          cognom = a.cognom,
                          edat = a.edat
                      }).SingleOrDefaultAsync(b => b.id == id);
            if (alum == null)
            {
                return NotFound();
            }

            return Ok(alum);
        }

        // PUT: api/alumnes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putalumne(int id, alumne alumne)
        {
            var alum = await db.alumne.Where(f => f.id == id).FirstOrDefaultAsync();
            if (alum != null)
            {
                alum.nom = alumne.nom;
                alum.cognom = alumne.cognom;
                alum.edat = alumne.edat;
                await db.SaveChangesAsync();
                return Ok();
            }
            return NotFound();

        }

        // POST: api/alumnes
        [ResponseType(typeof(alumne))]
        public async Task<IHttpActionResult> Postalumne(alumne alumne)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.alumne.Add(alumne);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (alumneExists(alumne.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = alumne.id }, alumne);
        }

        // DELETE: api/alumnes/5
        [ResponseType(typeof(alumne))]
        public async Task<IHttpActionResult> Deletealumne(int id)
        {
            alumne alumne = await db.alumne.FindAsync(id);
            if (alumne == null)
            {
                return NotFound();
            }

            db.alumne.Remove(alumne);
            await db.SaveChangesAsync();

            return Ok(alumne);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool alumneExists(int id)
        {
            return db.alumne.Count(e => e.id == id) > 0;
        }
    }
}