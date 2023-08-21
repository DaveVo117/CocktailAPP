
using CocktailAPP.BLL.Interfaces;
using CocktailAPP.Entity;
using Microsoft.AspNetCore.Mvc;
using CocktailAPP.DAL.Interfaces;


namespace CocktailAPP.AplicacionWeb.Utilidades.Extensiones
{
    public static class FavoritosExtension
    {

        public static async Task<bool> Eliminar(this IFavoritosService favoritosService, IGenericRepository<Favoritos> repository, int id)
        {
            try
            {
                Favoritos favorito_encontrado = await repository.Obtener(u => u.idDrink == id);

                if (favorito_encontrado == null)
                    throw new TaskCanceledException("El usuario no existe");

                bool respuesta = await repository.Eliminar(favorito_encontrado);

                return true;
            }
            catch
            {
                throw;
            }
        }


    }
}
