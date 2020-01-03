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
    public class assignaturasController : ApiController
    {
        private instiEntities1 db = new instiEntities1();

        // GET: api/assignaturas
        public IQueryable<AssignaturaDTO> Getassignatura()
        {
            var asg = from b in db.assignatura
                      select new AssignaturaDTO()
                      {
                          id = b.id,
                          nom = b.nom
                      };

            return asg;
        }

        // GET: api/assignaturas/5
        [ResponseType(typeof(AssignaturaDTO))]
        public async Task<IHttpActionResult> Getassignatura(int id)
        {
            var asig = await db.assignatura.Select(b =>
                new AssignaturaDTO()
                {
                    id = b.id,
                    nom = b.nom
                }).SingleOrDefaultAsync(b => b.id == id);

            if (asig == null)
            {
                return NotFound();
            }

            return Ok(asig);
        }

        // PUT: api/assignaturas/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putassignatura(int id, assignatura assignatura)
        {
            var asig = await db.assignatura.Where(f => f.id == id).FirstOrDefaultAsync();
            if (asig != null)
            {
                asig.nom = assignatura.nom;
                await db.SaveChangesAsync();
                return Ok();
            }
            return NotFound();
        }

        // POST: api/assignaturas
        [ResponseType(typeof(assignatura))]
        public async Task<IHttpActionResult> Postassignatura(assignatura assignatura)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.assignatura.Add(assignatura);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = assignatura.id }, assignatura);
        }

        // DELETE: api/assignaturas/5
        [ResponseType(typeof(assignatura))]
        public async Task<IHttpActionResult> Deleteassignatura(int id)
        {
            assignatura assignatura = await db.assignatura.FindAsync(id);
            if (assignatura == null)
            {
                return NotFound();
            }

            db.assignatura.Remove(assignatura);
            await db.SaveChangesAsync();

            return Ok(assignatura);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool assignaturaExists(int id)
        {
            return db.assignatura.Count(e => e.id == id) > 0;
        }
    }
}