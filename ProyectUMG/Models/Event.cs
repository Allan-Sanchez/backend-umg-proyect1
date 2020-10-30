using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectUMG.Models
{
    public class Event
    {
        [Key]
        public int EventID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }

        public DateTime? Created_at = null;

        public User User { get; set; }      

        public DateTime DateCreated
        {
            get
            {
                return this.Created_at.HasValue ? this.Created_at.Value : DateTime.Now;
            }

            set { this.Created_at = value; }

        }
    }
}
