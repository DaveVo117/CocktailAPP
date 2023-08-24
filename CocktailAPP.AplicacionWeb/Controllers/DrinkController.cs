using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using CocktailAPP.AplicacionWeb.Models.ViewModels;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Threading.Tasks;

namespace CocktailAPP.AplicacionWeb.Controllers
{
    public class DrinkController : AbstractController //Se implementa clase abstracta
    {
        #region Atributos

        private static string _baseUrl;

        public DrinkController(IConfiguration configuration) : base(configuration)
        {
            _baseUrl = _configuration.GetSection("ApiSettings:baseUrl").Value;
        }

        #endregion


        #region MetodosAbstractos

        [HttpGet]
        public async Task<IActionResult> BusquedaNombre(string nombre)
        {
            if (nombre is null)
                nombre = "";

            return await ConsultaGenerica(_baseUrl, "api/json/v1/1/search.php?s=", nombre);
        }

        [HttpGet]
        public async Task<IActionResult> BusquedaIngrediente(string ingrediente)
        {
            if (ingrediente is null)
                ingrediente = "";

            return await ConsultaGenerica(_baseUrl, "api/json/v1/1/filter.php?i=", ingrediente);
        }

        #endregion


        #region Metodos

        [HttpGet]
        public async Task<IActionResult> ObtenerPorId(int? id)
        {

            List<VMDrink> lista = new List<VMDrink>();

            if (id == null)
                id = 0;

            try
            {
                var cliente = new HttpClient();
                cliente.BaseAddress = new Uri(_baseUrl);
                var response = await cliente.GetAsync($"api/json/v1/1/lookup.php?i={id}");

                if (response.IsSuccessStatusCode)
                {
                    var json_respuesta = await response.Content.ReadAsStringAsync();
                    var resultado = JsonConvert.DeserializeObject<DrinksResponse>(json_respuesta);
                    lista = resultado.Drinks;


                    return StatusCode(StatusCodes.Status200OK, new { data = lista });
                }
                else
                {
                    return StatusCode((int) response.StatusCode, new { error = "Error en la solicitud" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = ex.Message });
            }
        }

        #endregion

        public IActionResult Index()
        {
            return View();
        }
    }
}

