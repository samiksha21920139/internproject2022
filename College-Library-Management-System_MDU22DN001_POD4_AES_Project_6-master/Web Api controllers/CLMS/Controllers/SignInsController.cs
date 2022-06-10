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
using System.Web.Http.Description;
using CLMS.Database;

namespace CLMS.Controllers
{
    public class SignInsController : ApiController
    {
        private LibraryContext db = new LibraryContext();

        // GET: api/SignIns
        public IQueryable<SignIn> GetSignIns()
        {
            return db.SignIns;
        }

        // GET: api/SignIns/5
        [HttpGet]
        [Route("api/SignIns/GetSignIn")]
        [ResponseType(typeof(SignIn))]
        public async Task<IHttpActionResult> GetSignIn(string username,string password)
        {
            var signIn = from r in db.SignIns
                         where r.UserName == username && r.Password == password
                         select new { r.UserRole, r.UserId };
            SignIn obj = new SignIn();
            foreach(var x in signIn)
            {
                obj.UserRole = x.UserRole;
                obj.UserId = x.UserId;
                obj.UserName = "";
                obj.Password = "";
            }

            if (signIn == null)
            {
                return NotFound();
            }

            return Ok(obj);
        }

        // PUT: api/SignIns/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutSignIn(string id, SignIn signIn)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != signIn.Password)
            {
                return BadRequest();
            }

            db.Entry(signIn).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SignInExists(id))
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

        // POST: api/SignIns
        [ResponseType(typeof(SignIn))]
        public async Task<IHttpActionResult> PostSignIn(SignIn signIn)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SignIns.Add(signIn);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SignInExists(signIn.Password))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = signIn.Password }, signIn);
        }

        // DELETE: api/SignIns/5
        [ResponseType(typeof(SignIn))]
        public async Task<IHttpActionResult> DeleteSignIn(string id)
        {
            SignIn signIn = await db.SignIns.FindAsync(id);
            if (signIn == null)
            {
                return NotFound();
            }

            db.SignIns.Remove(signIn);
            await db.SaveChangesAsync();

            return Ok(signIn);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SignInExists(string id)
        {
            return db.SignIns.Count(e => e.Password == id) > 0;
        }
    }
}