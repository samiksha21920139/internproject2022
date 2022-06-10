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
using CLMS.Models;

namespace CLMS.Controllers
{
    public class StudentsController : ApiController
    {
        private LibraryContext db = new LibraryContext();

        // GET: api/Students
        public IQueryable<RequestBook> GetRequestBooks()
        {
            return db.RequestBooks;
        }

        // GET: api/Students/5
        [ResponseType(typeof(RequestBook))]
        public async Task<IHttpActionResult> GetRequestBook(int id)
        {
                var requestBook = from r in db.RequestBooks
                                  orderby r.RequestId
                                  where r.UserID == id
                                  select r;

                if (requestBook == null)
                {
                    return NotFound();
                }

                return Ok(requestBook);
            
        }

        // PUT: api/Students/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutRequestBook(int id, RequestBook requestBook)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != requestBook.RequestId)
            {
                return BadRequest();
            }

            db.Entry(requestBook).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequestBookExists(id))
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

        // POST: api/Students
        [ResponseType(typeof(RequestBook))]
        public async Task<IHttpActionResult> PostRequestBook(RequestBook requestBook)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.RequestBooks.Add(requestBook);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = requestBook.RequestId }, requestBook);
        }

        // DELETE: api/Students/5
        [ResponseType(typeof(RequestBook))]
        public async Task<IHttpActionResult> DeleteRequestBook(int id)
        {
            RequestBook requestBook = await db.RequestBooks.FindAsync(id);
            if (requestBook == null)
            {
                return NotFound();
            }

            db.RequestBooks.Remove(requestBook);
            await db.SaveChangesAsync();

            return Ok(requestBook);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RequestBookExists(int id)
        {
            return db.RequestBooks.Count(e => e.RequestId == id) > 0;
        }
    }
}