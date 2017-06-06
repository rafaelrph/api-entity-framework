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
using ApiEntityFramework.Models;

namespace ApiEntityFramework.Controllers
{
    public class peopleController : ApiController
    {
        private apiEntities1 db = new apiEntities1();

        // GET: api/people
        public IQueryable<people> Getpeople()
        {
            return db.people;
        }

        // GET: api/people/5
        [ResponseType(typeof(people))]
        public IHttpActionResult Getpeople(long id)
        {
            people people = db.people.Find(id);
            if (people == null)
            {
                return NotFound();
            }

            return Ok(people);
        }

        // PUT: api/people/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putpeople(long id, people people)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != people.id)
            {
                return BadRequest();
            }

            db.Entry(people).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!peopleExists(id))
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

        // POST: api/people/5
        [ResponseType(typeof(people))]
        public IHttpActionResult Postpeople(people people)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.people.Add(people);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = people.id }, people);
            
        }

        /*
         * INSERT MULTIPLE
         * 
         * people people = new people();
            for (int i = 0; i < 10; i++)
            {
                people = new people()
                {
                    name = "People Multiple " + i,
                    email = "peoplemultiple" + i + "@mail.com"
                };
                db.people.Add(people);
                db.SaveChanges();
            }

            return Ok(people);
         **/



        // DELETE: api/people/5
        [ResponseType(typeof(people))]
        public IHttpActionResult Deletepeople(long id)
        {
            people people = db.people.Find(id);
            if (people == null)
            {
                return NotFound();
            }

            db.people.Remove(people);
            db.SaveChanges();

            return Ok(people);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool peopleExists(long id)
        {
            return db.people.Count(e => e.id == id) > 0;
        }
    }
}