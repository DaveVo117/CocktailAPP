using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.IO;
using CocktailAPP.AplicacionWeb.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CocktailAPP.AplicacionWeb.Controllers
{
    public abstract class AbstractController : Controller
    {
        protected IConfiguration _configuration;

        public AbstractController(IConfiguration configuration)
        {
            _configuration = configuration;
        }



        protected async Task<IActionResult> ConsultaGenerica(string baseUrl, string endpoint, string queryParam)
        {
            List<VMDrink> lista = new List<VMDrink>();

            try
            {
                var cliente = new HttpClient();
                cliente.BaseAddress = new Uri(baseUrl);
                var response = await cliente.GetAsync($"{endpoint}{Uri.EscapeDataString(queryParam)}");

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





    }
}

