using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProyectUMG.Models;

namespace ProyectUMG.Models
{
    public class ProyectContext : DbContext
    {
        public ProyectContext (DbContextOptions<ProyectContext> options) : base(options) 
        {
        }

        public DbSet<User> User { get; set; }
        public DbSet<Faculty> Faculty { get; set; }
        public DbSet<ProyectUMG.Models.Event> Event { get; set; }
        public DbSet<ProyectUMG.Models.Course> Course { get; set; }
        public DbSet<ProyectUMG.Models.Calendar> Calendar { get; set; }
    }
}
