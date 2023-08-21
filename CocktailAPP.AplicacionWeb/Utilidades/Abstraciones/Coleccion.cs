using CocktailAPP.DAL.Interfaces;
using CocktailAPP.Entity;

namespace CocktailAPP.AplicacionWeb.Utilidades.Abstraciones
{
    public abstract class Coleccion
    {
        #region ATRIBUTOS

        private readonly IGenericRepository<Favoritos> _repository;

        //constructor
        public Coleccion(IGenericRepository<Favoritos> repositorio)
        {
            _repository = repositorio;
        }

        #endregion

        #region METODOS
        public async Task<List<Favoritos>> Lista()
        {
            IQueryable<Favoritos> query = await _repository.Consultar();
            return query.ToList();
        }
        #endregion
    }
}
