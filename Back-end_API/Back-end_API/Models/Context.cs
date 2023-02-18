using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back_end_API.Models
{
    public class Context: DbContext
    {
        public Context(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Client> Clients { get; set; }
    }
}
