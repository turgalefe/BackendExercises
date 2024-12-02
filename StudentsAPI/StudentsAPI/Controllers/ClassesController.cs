using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using StudentsAPI.Model;
using StudentsAPI.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StudentsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassesController : ControllerBase
    {

        [HttpGet]
        public IEnumerable<Class> Get()
        {
            try
            {
                throw new Exception("Random hata döndürüldü.");
            }
            catch 
            {
                using (var context = new DgAkademiContext())
                {
                    return context.Classes.ToList();
                }
            }
        }


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
        //dto ile yapmak istersek postviewmodeli dto ile değiştiriyoruz
        //return new dto şeklşinde
        public ActionResult<PostClassViewModel> Post(Class classes)
        {
            using (var context = new DgAkademiContext())
            {
                if (classes != null && classes.Name != null)
                {
                    context.Add(classes);
                    context.SaveChanges();

                    PostClassViewModel ClassviewModel = new PostClassViewModel()
                    {
                        Id = classes.Id,
                        Name = classes.Name,
                    };
                    return ClassviewModel;
                }
                else
                {
                    return BadRequest();
                }
            }
        }

        [HttpPut("{id}")]
        public ActionResult <Class> Put(int id,Class classes)
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
                    classes.Id=data.Id;
                    context.Classes.Update(classes);
                    context.SaveChanges() ;
                    return classes;
                    
                }
            }
        }

        [HttpDelete("{id}")]
        public ActionResult <Class> Delete(int id)
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
