

let tableData;

const FAVORITOS_MODEL = {
    id: 0,
    idDrink: 0,
    strDrink: ""
}

$(document).ready(function () {

    $("#divResultados").hide();
    $(".card .card-body-custom").removeClass("active").slideUp();
    getFavs();

})



// EVENTOS
$("#txtIngrediente").on("input", limpiarNombre);
$("#txtNombre").on("input", limpiarIngrediente);

$("#btnLimpiar").click(async function () {
    limpiarBusqueda();
    $("#divResultados").hide();
    $("#divFavoritos").hide();
});

$("#btnBuscar").click(async function () {
    var nombre = $("#txtNombre").val();
    var ingrediente = $("#txtIngrediente").val();

    $('#dtSearch').DataTable().clear().destroy();

    if (nombre != '') {
        busquedaNombre(nombre);
    } else if (ingrediente != '') {
        busquedaIngrediente(ingrediente);
    } else {
        swal("Lo sentimos","Introduce un parámetro de búsqueda");
    }

});



$("#btnGuardar").click(function () {
   $(".card .card-body-custom").addClass("active").slideDown();
    createFavs(() => {
        getFavs(() => {
        });
    });
});



$("#btnOk").click(async function () {
    getFavs();
});

$("#dtSearch tbody").on("click", ".btn-primary", function () {

    var tableData = $("#dtSearch").DataTable();

    if ($(this).closest("tr").hasClass("child")) {
        filaSeleccionada = $(this).closest("tr").prev();
    } else {
        filaSeleccionada = $(this).closest("tr");
    }

    var modalFooter = document.querySelector('.modal-footer');
    modalFooter.hidden = false;

    const data = tableData.row(filaSeleccionada).data();

    geByiId(data.idDrink,0);

})


//METODOS
function mostrarModal(data) {

    $("#txtId").val(data.idDrink)
    $("#modalTttle").text(data.strDrink)
    $("#txtTags").text(data.strTags)
    $("#txtIngrediente1").text(data.strIngredient1)
    $("#txtIngrediente2").text(data.strIngredient2)
    $("#txtIngrediente3").text(data.strIngredient3)
    $("#txtIngrediente4").text(data.strIngredient4)
    $("#txtInstrucciones").text(data.strInstructions)
    $("#imgCocktail").attr("src", data.strDrinkThumb);

    $("#modalData").modal("show")
}
function dataModelFavs() {

    const modelo = structuredClone(FAVORITOS_MODEL);

    modelo["idDrink"] = parseInt($("#txtId").val())
    modelo["strDrink"] = $("#modalTttle").text().trim();
    
    return modelo;
}
function limpiarBusqueda() {
    $("#txtNombre").val("");
    $("#txtIngrediente").val("")
}
function limpiarNombre() {
    $("#txtNombre").val("");
}
function limpiarIngrediente() {
    $("#txtIngrediente").val("");
}
function busquedaNombre(nombre) {

    const url = `https://www.thecocktaildb.com/api/json/v1/1/search.php?s=${nombre}`;

    fetch(url)
        .then(response => {
            if (!response.ok) {
                throw new Error(`Error en la solicitud: ${response.status} ${response.statusText}`);
            }
            return response.json();
        })
        .then(data => {
 
            const cocktails = data.drinks;

            $('#dtSearch').DataTable({
                data: cocktails,
                columns: [
                    { "data": "idDrink", "visible": false },
                    {
                        "data": "strDrinkThumb", render: function (data) {
                           return `<img style="height:60px" src=${data} class="rounded mx-auto d-block"/ >`
                           
                        }
                    },
                    { "data": "strDrink" },
                    {
                        "data": "strCategory", render: function (data) {
                            return `<span class="badge badge-success">${data} </span>`;
                        }
                    },
                    { "data": "strIngredient1" },
                    {
                        "data": null,
                        "defaultContent": '<button class="btn btn-primary btn-info btn-sm mr-2" id="btnVer"><i class="fas fa-eye"></i></button>' +
                            '<button class="btn btn-danger btn-sm mr-2" id="btnEliminar"><i class="fas fa-trash"></i></button>',
                        "orderable": false,
                        "searchable": false,
                        "width": "120px"
                    }
                ],
                order: [[0, "desc"]],
                dom: "Bfrtip",
                buttons: [

                ],
                language: {
                    url: "https://cdn.datatables.net/plug-ins/1.11.5/i18n/es-ES.json"
                }
            });
        })
        .catch(error => {
            console.error('Error:', error);
        });

    $("#divResultados").show();
}
function busquedaIngrediente(ingrediente) {

    const url = `https://www.thecocktaildb.com/api/json/v1/1/filter.php?i=${ingrediente}`;

    fetch(url)
        .then(response => {
            if (!response.ok) {
                throw new Error(`Error en la solicitud: ${response.status} ${response.statusText}`);
            }
            return response.json();
        })
        .then(data => {

            const cocktails = data.drinks;

            $('#dtSearch').DataTable({
                data: cocktails,
                columns: [
                    { "data": "idDrink", "visible": false },
                    {
                        "data": "strDrinkThumb", render: function (data) {
                            return `<img style="height:60px" src=${data} class="rounded mx-auto d-block"/ >`

                        }
                    },
                    { "data": "strDrink" },
                    {"data": null, "visible":false},
                    {
                        "data": null,
                        "defaultContent": `<span class="badge badge-success">${ingrediente} </span>`
                    
                    },
                    {
                        "data": null,
                        "defaultContent": '<button class="btn btn-primary btn-info btn-sm mr-2" id="btnVer"><i class="fas fa-eye"></i></button>' +
                            '<button class="btn btn-danger btn-sm mr-2" id="btnEliminar"><i class="fas fa-trash"></i></button>',
                        "orderable": false,
                        "searchable": false,
                        "width": "120px"
                    }
                ],
                order: [[0, "desc"]],
                dom: "Bfrtip",
                buttons: [

                ],
                language: {
                    url: "https://cdn.datatables.net/plug-ins/1.11.5/i18n/es-ES.json"
                }
            });
        })
        .catch(error => {
            console.error('Error:', error);
        });

    $("#divResultados").show();
}
function geByiId(id,type) {

    const url = `https://www.thecocktaildb.com/api/json/v1/1/lookup.php?i=${id}`;

    fetch(url)
        .then(response => {
            if (!response.ok) {
                throw new Error(`Error en la solicitud: ${response.status} ${response.statusText}`);
            }
            return response.json();
        })
        .then(data => {
            const cocktails = data.drinks[0];

            mostrarModal(cocktails);
            if (type == 1) {
                $("#btnGuardar").hide();
            } else {
                $("#btnGuardar").show();
            }

        })
        .catch(error => {
            console.error('Error:', error);
        });

}
async function getFavs() {
    try {
        const response = await fetch('/Home/Lista'); // Asegúrate de usar la ruta correcta
        const data = await response.json();

        const ulFavoritos = document.getElementById('ulFavoritos');

        ulFavoritos.innerHTML = ''; // Limpiamos la lista

        data.data.forEach(fav => {
            const li = document.createElement('li');
            li.textContent = fav.strDrink;

            const listItem = document.createElement('div');
            listItem.className = 'd-flex justify-content-between align-items-center';

            const buttonsContainer = document.createElement('div');
            buttonsContainer.className = 'd-flex';

            const viewButton = document.createElement('button');
            viewButton.className = 'btn btn-primary-custom btn-info btn-sm mr-2';
            viewButton.innerHTML = '<i class="fas fa-eye"></i>';
            viewButton.addEventListener('click', () => {
                geByiId(fav.idDrink, 1); // Llamamos a la función para eliminar el favorito
            });

            const deleteButton = document.createElement('button');
            deleteButton.className = 'btn btn-danger-custom btn-sm';
            deleteButton.innerHTML = '<i class="fas fa-trash"></i>';
            deleteButton.dataset.id = fav.idDrink; // Almacenamos el id como atributo data-id
            deleteButton.addEventListener('click', () => {
                deleteFav(fav.idDrink); // Pasamos el id como parámetro a la función deleteFav
                li.remove(); // Removemos el elemento <li> correspondiente al botón eliminado
            });

            buttonsContainer.appendChild(viewButton);
            buttonsContainer.appendChild(deleteButton);

            listItem.appendChild(li);
            listItem.appendChild(buttonsContainer);

            ulFavoritos.appendChild(listItem);
        });

        $("#divFavoritos").show();
    } catch (error) {
        console.error('Error fetching data:', error);
    }
    callback();
}
async function deleteFav(id) {
    try {
        const response = await fetch(`/Home/Eliminar/${id}`, {
            method: 'DELETE'
        });

        if (response.ok) {
            const deleteButton = document.querySelector(`[data-id="${id}"]`);
            if (deleteButton) {
                const buttonsContainer = deleteButton.parentElement;
                const listItem = buttonsContainer.parentElement;
                listItem.remove();
            } else {
                console.warn(`No se encontró el botón con data-id ${id}`);
            }
        } else {
            console.error('Error al eliminar el favorito');
        }
    } catch (error) {
        console.error('Error al eliminar el favorito:', error);
    }
}
async function createFavs(callback) {
    const url = "/Home/Crear";
    const model = dataModelFavs();

    const formData = new FormData();
    formData.append("model", JSON.stringify(model));

    try {
        const response = await fetch(url, {
            method: "POST",
            body: formData
        });

        if (!response.ok) {
            throw new Error(`Error en la solicitud: ${response.status} ${response.statusText}`);
        }

        const data = await response.json();

        if (data.mensaje == "Ok") {
            swal("Listo!", "El cocktel se agregó a los favoritos", "success");
        } else {
            swal("Lo sentimos!", data.mensaje, "warning");
        }

        $("#btnGuardar").hide();
    } catch (error) {
        console.error("Error:", error);
    }

    callback();
}
