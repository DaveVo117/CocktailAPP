using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CocktailAPP.Entity;

namespace CocktailAPP.DAL.DBContext

{
    public class DBContext : DbContext
    {

        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
            
        }

        public DbSet<Favoritos> Favoritos { get; set; }

    }
}
