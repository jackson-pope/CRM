using System;
using System.Collections.Generic;

namespace Backend.Models
{
    public partial class Email
    {
        public int Id { get; set; }
        public int Customer { get; set; }
        public string EmailAddress { get; set; } = null!;

        public virtual Customer CustomerNavigation { get; set; } = null!;
    }
}
