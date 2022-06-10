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
    public class AvailableBooksController : ApiController
    {
        private LibraryContext db = new LibraryContext();

        // GET: api/AvailableBooks
        public IQueryable<AvailableBook> GetAvailableBooks()
        {
            return db.AvailableBooks;
        }

        // GET: api/AvailableBooks/5
        [ResponseType(typeof(AvailableBook))]
        public async Task<IHttpActionResult> GetAvailableBook(int id)
        {
            AvailableBook availableBook = await db.AvailableBooks.FindAsync(id);
            if (availableBook == null)
            {
                return NotFound();
            }

            return Ok(availableBook);
        }
        [ResponseType(typeof(AvailableBook))]
        public async Task<IHttpActionResult> GetAvailableBooks(string name)
        {
            AvailableBook availableBook = await db.AvailableBooks.FindAsync(name);
            if (availableBook == null)
            {
                return NotFound();
            }

            return Ok(availableBook);
        }

        // PUT: api/AvailableBooks/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutAvailableBook(int id, AvailableBook availableBook)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != availableBook.BookId)
            {
                return BadRequest();
            }

            db.Entry(availableBook).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AvailableBookExists(id))
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

        // POST: api/AvailableBooks
        [ResponseType(typeof(AvailableBook))]
        public async Task<IHttpActionResult> PostAvailableBook(AvailableBook availableBook)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AvailableBooks.Add(availableBook);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = availableBook.BookId }, availableBook);
        }

        // DELETE: api/AvailableBooks/5
        [ResponseType(typeof(AvailableBook))]
        public async Task<IHttpActionResult> DeleteAvailableBook(int id)
        {
            AvailableBook availableBook = await db.AvailableBooks.FindAsync(id);
            if (availableBook == null)
            {
                return NotFound();
            }

            db.AvailableBooks.Remove(availableBook);
            await db.SaveChangesAsync();

            return Ok(availableBook);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AvailableBookExists(int id)
        {
            return db.AvailableBooks.Count(e => e.BookId == id) > 0;
        }
    }
}