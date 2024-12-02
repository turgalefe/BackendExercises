using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using Students2API.Model;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StudentsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassesController : ControllerBase
    {
        // GET: api/<StudentsController>
        [HttpGet]
        public IEnumerable<Class> Get()
        {
            using (var context = new DgAkademiContext())
            {
                return context.Classes.ToList();
            }
        }


        // GET api/<StudentsController>/5
        [HttpGet("{id}")]
        public ActionResult<Class> Get(int id)
        {
            using (var context = new DgAkademiContext())
            {
                var classes = context.Classes.Where(data => data.Id == id).FirstOrDefault();

                if (classes == null)
                {
                    return NotFound();
                }
                else
                {
                    return classes;
                }
            }
        }

        // POST api/<StudentsController>
        [HttpPost]
        public bool Post(Class classes)
        {
            using (var context = new DgAkademiContext())
            {
                if (classes != null && classes.Name != null)
                {
                    context.Add(classes);
                    context.SaveChanges();

                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        // PUT api/<StudentsController>/5
        [HttpPut("{id}")]
        public ActionResult<Class> Put(int id, Class classes)
        {
            using (var context = new DgAkademiContext())
            {
                var data = context.Classes.Where(data => data.Id == id).FirstOrDefault();

                if (data == null)
                {
                    return NotFound();
                }
                else
                {
                    classes.Id = data.Id;
                    context.Classes.Update(classes);
                    context.SaveChanges();
                    return classes;

                }
            }
        }

        // DELETE api/<StudentsController>/5
        [HttpDelete("{id}")]
        public ActionResult<Class> Delete(int id)
        {
            using (var context = new DgAkademiContext())
            {
                var data = context.Classes.Where(data => data.Id == id).FirstOrDefault();

                if (data == null)
                {
                    return NotFound();
                }
                else
                {
                    context.Classes.Remove(data);
                    context.SaveChanges();
                    return data;

                }
            }
        }
    }
}
