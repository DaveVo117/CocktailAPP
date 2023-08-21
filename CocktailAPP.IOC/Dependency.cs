using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

using CocktailAPP.DAL.DBContext;
using CocktailAPP.DAL.Interfaces;
using CocktailAPP.DAL.Implementacion;
using CocktailAPP.BLL.Implementacion;
using CocktailAPP.BLL.Interfaces;

namespace CocktailAPP.IOC
{
    public static class Dependency
    {
        public static void InjectDependency(this IServiceCollection Services, IConfiguration Configuration)
        {
            Services.AddDbContext<DBContext>(optionsAction =>
            {
                optionsAction.UseSqlServer(Configuration.GetConnectionString("CocktailConnection"));
            });

            Services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            Services.AddScoped<IFavoritosService, FavoritosService>();

        }

    }
}
