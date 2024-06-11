using System;
using System.Collections.Generic;

namespace Notebook.Models;

public partial class Task
{
    public int Taskid { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime? Duedate { get; set; }

    public int? Userid { get; set; }

    public virtual User? User { get; set; }
}
