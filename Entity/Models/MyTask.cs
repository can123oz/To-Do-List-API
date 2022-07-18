using System;
using System.Collections.Generic;

#nullable disable

namespace Entity.Models
{
    public partial class MyTask
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool? Status { get; set; }
        public string Description { get; set; }
    }
}
