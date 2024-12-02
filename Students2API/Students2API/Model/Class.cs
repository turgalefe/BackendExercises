using System;
using System.Collections.Generic;

namespace Students2API.Model;

public partial class Class
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
