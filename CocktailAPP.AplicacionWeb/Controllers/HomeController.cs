using CocktailAPP.AplicacionWeb.Models;
using CocktailAPP.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Newtonsoft.Json;

using CocktailAPP.Entity;
using CocktailAPP.AplicacionWeb.Utilidades.Response;
using CocktailAPP.AplicacionWeb.Utilidades.Extensiones;
using CocktailAPP.DAL.Interfaces;
using CocktailAPP.AplicacionWeb.Utilidades.Abstraciones;

namespace CocktailAPP.AplicacionWeb.Controllers
{
    public class HomeController : Controller
    {

        #region Atributos
        private readonly IFavoritosService _favoritosService;
        private readonly IGenericRepository<Favoritos> _repository;
        public HomeController(IFavoritosService favoritosService, IGenericRepository<Favoritos> repository)
        {
            _favoritosService = favoritosService;
            _repository = repository;
        }
        #endregion


        #region Metodos
        [HttpGet]
        public async Task<IActionResult> Lista() //lista de usuarios
        {
            List<Favoritos> favsList= await _favoritosService.Lista();
            return StatusCode(StatusCodes.Status200OK, new { data = favsList });//se regresa el formato data ya que es necesario para trabajar con DataTable de Jquery
        }





        [HttpPost]
        public async Task<IActionResult> Crear([FromForm] string model)
        {
            GenericResponse<Favoritos> genericResponse = new GenericResponse<Favoritos>();

            try
            {
                Favoritos vmFav = JsonConvert.DeserializeObject<Favoritos>(model);

                Favoritos favorito_guardado = await _favoritosService.Guardar(vmFav);

                genericResponse.Estado = true;
                genericResponse.Objeto = favorito_guardado;
                genericResponse.Mensaje = "Ok";
            }
            catch (Exception ex)
            {
                genericResponse.Estado = false;
                genericResponse.Mensaje = ex.Message;
            }

            return StatusCode(StatusCodes.Status200OK, genericResponse);

        }





        [HttpDelete]
        public async Task<IActionResult> Eliminar(int id)
        {
            GenericResponse<string> genericResponse = new GenericResponse<string>();
            try
            {
                //Metodo de extension de la clase FavoritosService
                await _favoritosService.Eliminar(_repository, id);
            }
            catch (Exception ex)
            {
                genericResponse.Estado = false;
                genericResponse.Mensaje = ex.Message;
            }

            return StatusCode(StatusCodes.Status200OK, genericResponse);
        }

        #endregion







        public IActionResult Index()
        {
            return View();
        }




    }
}