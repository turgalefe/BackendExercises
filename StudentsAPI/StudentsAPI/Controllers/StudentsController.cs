using Microsoft.AspNetCore.Mvc;
using StudentsAPI.Model;
using StudentsAPI.Validators;
using StudentsAPI.ViewModels;
using System.ComponentModel.DataAnnotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StudentsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        //BİZE VERİLERİ TO LİST İLE BU 3 ÖZELLİĞİ KULLANIP GERİ DÖNÜCEK
        [HttpGet]
        public ActionResult<List<PostStudentViewModel>> Get()
        {
            using (var context = new DgAkademiContext())
            {
                var classes = context.Students.Select(data => new PostStudentViewModel()
                {
                    Email = data.Email,
                    Id = data.Id,
                    Name = data.Name
                }).ToList();

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


        // GET api/<StudentsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<StudentsController>
        [HttpPost]
        public ActionResult<PostStudentViewModel> Post(Students student)
        {
            using (var context = new DgAkademiContext())
            {

                StudentValidator validate = new StudentValidator();

                if (validate.Validate(student).IsValid)
                {
                    context.Add(student);
                    context.SaveChanges();

                    PostStudentViewModel viewModel = new PostStudentViewModel()
                    {
                        Id = student.Id,
                        Email = student.Email,
                        Name = student.Name,
                    };
                    return viewModel;
                }
                else
                {
                    return BadRequest();
                }
            }
        }

        // PUT api/<StudentsController>/5
        [HttpPut("{id}")]
        public void Put(int id, Students student)
        {
        }

        // DELETE api/<StudentsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
