using System;
using System.Collections.Generic;

namespace Notebook.Models;

public partial class User
{
    public int Userid { get; set; }

    public string Name { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
}
