using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project01.Model;
using Project01.Validators;
using Project01.ViewModels;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Project01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }


        [HttpGet("{id}")]
        public ActionResult<User> Get(int id)
        {
            using (var context = new Project01Context())
            {
                var users = context.Users.Where(data => data.Id == id).FirstOrDefault();

                if (users == null)
                {
                    return NotFound();
                }
                else
                {
                    return users;
                }
            }
        }


        [HttpPost]
        public ActionResult<PostUserViewModel> Post(User user)
        {
            using (var context = new Project01Context())
            {
                UserValidator validate = new UserValidator();

                if (validate.Validate(user).IsValid)
                {
                    context.Add(user);
                    context.SaveChanges();
                    PostUserViewModel viewModel = new PostUserViewModel()
                    {
                        Name = user.Name,
                        Surname = user.Surname,
                        Email = user.Email,
                        Age = user.Age
                    };
                    return viewModel;
                }
                else
                {
                    return BadRequest();
                }
            }
        }

        [HttpPut("{id}")]
        public ActionResult<User> Put(int id, User user)
        {
            using (var context = new Project01Context())
            {
                if (id != user.Id)
                {
                    return BadRequest("ID mismatch.");
                }

                // Kullanıcı varlığını ekleyin ve değiştirilmiş olarak işaretleyin
                context.Entry(user).State = EntityState.Modified;

                //Veritabanı Güncellemeleri ve Hata Yönetimi:
                try
                {
                    context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(id, context))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return Ok(user);
            }
        }
        //Bu yardımcı metod, verilen id'ye sahip bir kullanıcının veritabanında
        //mevcut olup olmadığını kontrol eder:
        private bool UserExists(int id, Project01Context context)
        {
            return context.Users.Any(e => e.Id == id);
        }



        [HttpDelete("{id}")]
        public ActionResult<User> Delete(int id)
        {
            using (var context = new Project01Context())
            {
                var data = context.Users.Where(data => data.Id == id).FirstOrDefault();

                if (data == null)
                {
                    return NotFound();
                }
                else
                {
                    context.Users.Remove(data);
                    context.SaveChanges();
                    return data;

                }
            }
        }
    }
}
