using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentsAPI.Model;

public partial class Students
{
    public int Id { get; set; }

    public string Name { get; set; }

    public int Classid { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public int ClassId { get; set; }

}
