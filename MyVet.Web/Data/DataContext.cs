using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyVet.Web.Data.Entities;

namespace MyVet.Web.Data
{
  public class DataContext: DbContext
  {
    public DataContext(DbContextOptions<DataContext> options) :base(options)
    {

    }

    public DbSet<Owner> Owners { get; set; }
  }
}
