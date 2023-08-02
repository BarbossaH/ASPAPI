using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPAPI.Data
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options):base(options)
        {            
        }
    //in this way, there will be a warning about null value
    // public DbSet<Character> Characters{ get; set; }
    public DbSet<Character> Characters => Set<Character>();

    public virtual DbSet<Customer>? Customers { get; set; }
    // public DbSet<Customer> Customers => Set<Customer>();
  }
}