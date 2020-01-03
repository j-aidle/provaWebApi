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
    public class dadasController : ApiController
    {
        private instiEntities1 db = new instiEntities1();

        // GET: api/dadas
        public IQueryable<DadaDTO> Getdada()
        {
            var dades = from c in db.dada
                        select new DadaDTO()
                        {
                            id=c.id,
                            alumne = c.alumne == null ? null : new AlumneDTO()
                            {
                                id = c.alumne.id,
                                nom = c.alumne.nom,
                                cognom = c.alumne.cognom,
                                edat = c.alumne.edat
                            },
                            assignatura = c.assignatura == null ? null : new AssignaturaDTO()
                            {
                                id = c.assignatura.id,
                                nom = c.assignatura.nom,
                            },
                            professor = c.profe == null ? null : new ProfessorDTO()
                            {
                                id = c.profe.id,
                                nom = c.profe.nom,
                                cognom = c.profe.cognom,
                                edat = c.profe.edat
                            },
                        };
            return dades;
        }

        // GET: api/dadas/5
        [ResponseType(typeof(DadaDTO))]
        public async Task<IHttpActionResult> Getdada(int id)
        {
            //dada dada = await db.dada.FindAsync(id);
            var dades = await db.dada.Select(c=>
            new DadaDTO() {
                id = c.id,
                alumne = c.alumne == null ? null : new AlumneDTO()
                {
                    id = c.alumne.id,
                    nom = c.alumne.nom,
                    cognom = c.alumne.cognom,
                    edat = c.alumne.edat
                },
                assignatura = c.assignatura == null ? null : new AssignaturaDTO()
                {
                    id = c.assignatura.id,
                    nom = c.assignatura.nom,
                },
                professor = c.profe == null ? null : new ProfessorDTO()
                {
                    id = c.profe.id,
                    nom = c.profe.nom,
                    cognom = c.profe.cognom,
                    edat = c.profe.edat
                }
            }).SingleOrDefaultAsync(b => b.id == id);

            if (dades == null)
            {
                return NotFound();
            }

            return Ok(dades);
        }

        // PUT: api/dadas/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putdada(int id, dada dada)
        {
            var dades = await db.dada.Where(f => f.id == id).FirstOrDefaultAsync();
            if (dades != null)
            {
                dades.alumneid = dada.alumneid;
                dades.professorid = dada.professorid;
                dades.assignaturaid = dada.assignaturaid;
                await db.SaveChangesAsync();
                return Ok();
            }
            return NotFound();

            ////if (!ModelState.IsValid)
            ////{
            ////    return BadRequest(ModelState);
            ////}

            ////if (id != dada.alumneid)
            ////{
            ////    return BadRequest();
            ////}

            ////db.Entry(dada).State = EntityState.Modified;

            ////try
            ////{
            ////    await db.SaveChangesAsync();
            ////}
            ////catch (DbUpdateConcurrencyException)
            ////{
            ////    if (!dadaExists(id))
            ////    {
            ////        return NotFound();
            ////    }
            ////    else
            ////    {
            ////        throw;
            ////    }
            ////}

            ////return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/dadas
        [ResponseType(typeof(dada))]
        public async Task<IHttpActionResult> Postdada(dada dada)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.dada.Add(dada);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (dadaExists(dada.alumneid) && dadaExists(dada.professorid) && dadaExists(dada.assignaturaid))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtRoute("DefaultApi", new { id = dada.alumneid }, dada);
        }

        // DELETE: api/dadas/5
        [ResponseType(typeof(dada))]
        public async Task<IHttpActionResult> Deletedada(int id)
        {
            dada dada = await db.dada.FindAsync(id);
            if (dada == null)
            {
                return NotFound();
            }

            db.dada.Remove(dada);
            await db.SaveChangesAsync();

            return Ok(dada);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool dadaExists(int id)
        {
            return db.dada.Count(e => e.alumneid == id) > 0;
        }
    }
}