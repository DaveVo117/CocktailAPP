using CocktailAPP.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CocktailAPP.BLL.Interfaces
{
    public interface IFavoritosService
    {
        Task<List<Favoritos>> Lista();
        Task<Favoritos> Obtener(int id);
        Task<Favoritos> Guardar(Favoritos entidad);
    
    }
}
