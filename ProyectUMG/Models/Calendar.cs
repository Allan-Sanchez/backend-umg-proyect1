using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectUMG.Models
{
    public class Calendar
    {
        [Key]
        public int CalendarId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }

        public User User { get; set; }
        public Course Course { get; set; }
    }
}
