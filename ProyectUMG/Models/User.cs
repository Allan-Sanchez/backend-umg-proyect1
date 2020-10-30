using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectUMG.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        //[DataType(DataType.Date)]
        public DateTime? Created_at = null;
        [Required]
        public string UserRole { get; set; }
        [JsonIgnore]
        public string Password { get; set; }

        public ICollection<Event> Event { get; set; }
        public ICollection<Course> Course { get; set; }
        public ICollection<Calendar> Calendar { get; set; }
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
