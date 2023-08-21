using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using CocktailAPP.BLL.Interfaces;
using CocktailAPP.DAL.Interfaces;
using CocktailAPP.Entity;

namespace CocktailAPP.BLL.Implementacion
{
    public class FavoritosService : IFavoritosService
    {
        #region ATRIBUTOS

        private readonly IGenericRepository<Favoritos> _repository;

        //constructor
        public FavoritosService(IGenericRepository<Favoritos> repositorio)
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


        public async Task<Favoritos> Obtener(int id)
        {
            IQueryable<Favoritos> query = await _repository.Consultar(u => u.id == id);
            Favoritos favorito = query.First();

            return favorito;
        }


        public async Task<Favoritos> Guardar(Favoritos entidad)
        {
            Favoritos favorito_existe= await _repository.Obtener(u => u.idDrink == entidad.idDrink);
            if (favorito_existe != null)
                throw new TaskCanceledException("El cocktel ya existe en la lista de favoritos");

            try
            {

                Favoritos favorito_guardado= await _repository.Crear(entidad);

                if (favorito_guardado.id == 0)
                    throw new TaskCanceledException("No se pudo crear el usuario");

                IQueryable<Favoritos> query = await _repository.Consultar(u => u.id == favorito_guardado.id);
                favorito_guardado = query.First();

                return favorito_guardado;

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        #endregion




    }
}
