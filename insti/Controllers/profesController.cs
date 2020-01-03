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
    public class profesController : ApiController
    {
        private instiEntities1 db = new instiEntities1();

        // GET: api/profes
        public IQueryable<ProfessorDTO> Getprofe()
        {
            var prof = from c in db.profe
                       select new ProfessorDTO()
                       {
                           id = c.id,
                           nom = c.nom,
                           cognom = c.cognom,
                           edat=c.edat
                       };

            return prof;
        }

        // GET: api/profes/5
        [ResponseType(typeof(ProfessorDTO))]
        public async Task<IHttpActionResult> Getprofe(int id)
        {
            var profe = await db.profe.Select(b =>
                         new ProfessorDTO()
                         {
                             id = b.id,
                             nom = b.nom,
                             cognom = b.cognom,
                             edat = b.edat
                            
                         }).SingleOrDefaultAsync(b => b.id == id);
            if (profe == null)
            {
                return NotFound();
            }

            return Ok(profe);
        }

        // PUT: api/profes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putprofe(int id, profe profe)
        {
            var prof = await db.profe.Where(f => f.id == id).FirstOrDefaultAsync();
            if (prof != null)
            {
                prof.nom = profe.nom;
                prof.cognom = profe.cognom;
                prof.edat = profe.edat;
                await db.SaveChangesAsync();
                return Ok();
            }
            return NotFound();
        }

        // POST: api/profes
        [ResponseType(typeof(profe))]
        public async Task<IHttpActionResult> Postprofe(profe profe)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.profe.Add(profe);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = profe.id }, profe);
        }

        // DELETE: api/profes/5
        [ResponseType(typeof(profe))]
        public async Task<IHttpActionResult> Deleteprofe(int id)
        {
            profe profe = await db.profe.FindAsync(id);
            if (profe == null)
            {
                return NotFound();
            }

            db.profe.Remove(profe);
            await db.SaveChangesAsync();

            return Ok(profe);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool profeExists(int id)
        {
            return db.profe.Count(e => e.id == id) > 0;
        }
    }
}