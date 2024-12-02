using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project01.Model;
using Project01.Validators;
using Project01.ViewModels;
using System.ComponentModel.DataAnnotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Project01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserLikedController : ControllerBase
    {

        [HttpGet]
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


        [HttpGet("{id}")]
        public ActionResult<PostUserLikedViewModel> GetUserLiked(int id)
        {

            using (var context = new Project01Context())
            {
                //userlikeds db ye göre işlem yapmaya yarar.
                var userLiked = context.UserLikeds.Where(data => data.UserId == id).FirstOrDefault();


                if (userLiked == null)
                {
                    return NotFound();
                }
                else
                {
                    PostUserLikedViewModel viewModel = new PostUserLikedViewModel()
                    {
                        IsLiked = userLiked.IsLiked,
                        Ratings = userLiked.Ratings,
                        Comments = userLiked.Comments,
                    };
                    return viewModel;
                }
            }
        }

        [HttpPost]
        public ActionResult <PostUserLikedViewModel> Post(UserLiked userLiked)
        {
            using (var context = new Project01Context())
            {
                UserLikedValidator validationRules = new UserLikedValidator();

                if (validationRules.Validate(userLiked).IsValid)
                {
                    context.Add(userLiked);
                    context.SaveChanges();
                    PostUserLikedViewModel viewModel = new PostUserLikedViewModel()
                    {
                        IsLiked = userLiked.IsLiked,
                        Ratings = userLiked.Ratings,
                        Comments = userLiked.Comments,
                        //PostComments = userLiked.PostComments
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
        public ActionResult <UserLiked> Put(int id,UserLiked userLiked)
        {
            using (var context = new Project01Context())
            {
                if (id != userLiked.UserId)
                {
                    return BadRequest("ID mismatch.");
                }

                // Kullanıcı varlığını ekleyin ve değiştirilmiş olarak işaretleyin
                context.Entry(userLiked).State = EntityState.Modified;

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

                return Ok(userLiked);
            }
        }
        //Bu yardımcı metod, verilen id'ye sahip bir kullanıcının veritabanında
        //mevcut olup olmadığını kontrol eder:
        private bool UserExists(int id, Project01Context context)
        {
            return context.UserLikeds.Any(e => e.UserId == id);
        }


        [HttpDelete("{id}")]
        public ActionResult <UserLiked> Delete(int id)
        {
            using (var context = new Project01Context())
            {
                var data = context.UserLikeds.Where(data => data.UserId == id).FirstOrDefault();

                if (data == null)
                {
                    return NotFound();
                }
                else
                {
                    context.UserLikeds.Remove(data);
                    context.SaveChanges();
                    return data;

                }
            }
        }
    }
}