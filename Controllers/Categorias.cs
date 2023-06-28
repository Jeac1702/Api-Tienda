using apiTienda.Models;
using apiTienda.Recursos;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Text.Json.Serialization;
using Xceed.Wpf.Toolkit;

namespace apiTienda.Controllers
{
    [ApiController]
    [Microsoft.AspNetCore.Mvc.Route("Categorias")]
    public class Categorias
    {
        [HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("listar")]
        public dynamic ListarDetallesCategorias()
        {
            DBDatos conObj = new DBDatos();
            string query = "SELECT * FROM CategoriaProducto";
            DataTable tdetallesprod = conObj.queryAdapter(query);
            string jsonLista = JsonConvert.SerializeObject(tdetallesprod);
            return new
            {
                succes = true,
                message = "Exito",
                result = new
                {
                    categorias = JsonConvert.DeserializeObject<List<CategoriasModel>>(jsonLista)
                }
            };
        }
        [HttpPost]
        [Microsoft.AspNetCore.Mvc.Route("Agregar")]
        public dynamic AgregarCategoria(CategoriasAdd categoria)
        {
            DBDatos conObj = new DBDatos();
            string query = "INSERT INTO CategoriaProducto(NombreCategoria, DescripcionCategoria) VALUES('" + categoria.NombreCategoria + "', '" + categoria.DescripcionCategoria + "')";
            conObj.queryAdapter(query);
            string queryr = "SELECT * FROM CategoriaProducto";
            DataTable tdetallesprod = conObj.queryAdapter(queryr);
            string jsonLista = JsonConvert.SerializeObject(tdetallesprod);
            return new
            {
                success = true,
                message = "Exito",
                result = new
                {
                    categorias = JsonConvert.DeserializeObject<List<CategoriasModel>>(jsonLista)
                }
            };
        }

        [HttpPut]
        [Microsoft.AspNetCore.Mvc.Route("Modificar")]
        public dynamic ModificarCategoria(CategoriasModel categoria)
        {
            DBDatos conObj = new DBDatos();
            string query = "UPDATE CategoriaProducto SET NombreCategoria = '" + categoria.NombreCategoria + "', DescripcionCategoria = '"+categoria.DescripcionCategoria+ "' WHERE idCategoria = "+categoria.idCategoria;
            int regs = 0;
            conObj.nonQueryAdapter(query, out regs);
            string queryr = "SELECT * FROM CategoriaProducto";
            DataTable tdetallesprod = conObj.queryAdapter(queryr);
            string jsonLista = JsonConvert.SerializeObject(tdetallesprod);
            return new
            {
                success = true,
                message = "Exito",
                result = new
                {
                    categorias = JsonConvert.DeserializeObject<List<CategoriasModel>>(jsonLista)
                }
            };
        }
        [HttpDelete]
        [Microsoft.AspNetCore.Mvc.Route("Eliminar")]
        public dynamic EliminarCategoria(CategoriasId categoria)
        {
            DBDatos conObj = new DBDatos();
            try
            {
                string query = "delete from CategoriaProducto where idCategoria = '" + categoria.idCategoria + "'";
                conObj.queryAdapter(query);
            }
            catch
            {

            }
            string queryr = "SELECT * FROM CategoriaProducto";
            DataTable tprod = conObj.queryAdapter(queryr);
            string jsonLista = JsonConvert.SerializeObject(tprod);
            return new
            {
                success = true,
                message = "Exito",
                result = new
                {
                    categorias = JsonConvert.DeserializeObject<List<CategoriasModel>>(jsonLista)
                }
            };
        }
    }
}
