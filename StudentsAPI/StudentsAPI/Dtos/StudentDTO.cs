using StudentsAPI.Model;

namespace StudentsAPI.Dtos
{
    public class StudentDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Classid { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public virtual Class Class { get; set; } = null!;
    }
}
