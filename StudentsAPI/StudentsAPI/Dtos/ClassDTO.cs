using StudentsAPI.Model;

namespace StudentsAPI.Dtos
{
    public class ClassDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Students> Students { get; set; } = new List<Students>();
    }
}
