﻿namespace ServiceCompany.DbStuff.Models
{
    public class Alert : BaseModelAbstract
    {
        public string Message { get; set; }

        public DateTime ExpireDate { get; set; }

        public bool IsRead { get; set; }

        public virtual User? Author { get; set; }

        public virtual List<User>? NotifiedUsers { get; set; }

        public Alert() : base() { }
    }
}
