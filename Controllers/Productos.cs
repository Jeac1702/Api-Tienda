using apiTienda.Models;
using apiTienda.Recursos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;

namespace apiTienda.Controllers
{
    [ApiController]
    [Route("Productos")]
    public class Productos
    {
        [HttpGet]
        [Route("listar")]
        public dynamic ListarProductos()
        {
            DBDatos conObj = new DBDatos();
            string query = "SELECT * FROM producto";
            DataTable tprod = conObj.queryAdapter(query);
            string jsonLista = JsonConvert.SerializeObject(tprod);
            return new
            {
                succes = true,
                message = "Exito",
                result = new
                {
                    categorias = JsonConvert.DeserializeObject<List<ProductosModel>>(jsonLista)
                }
            };
        }
        [HttpPost]
        [Route("Agregar")]
        public dynamic AgregarProducto(ProductosAdd producto)
        {
            DBDatos conObj = new DBDatos();
            string queryid = "SELECT NombreCategoria FROM CategoriaProducto WHERE idCategoria = " + producto.idCategoria;
            string query = "INSERT INTO Producto (NombreProducto, idCategoria, NombreCategoria, DescripcionProducto) values ('" + producto.NombreProducto + "', '" + producto.idCategoria + "', (SELECT NombreCategoria FROM CategoriaProducto WHERE idCategoria = '" + producto.idCategoria + "'), '" + producto.DescripcionProducto + "')";
            conObj.queryAdapter(query);
            string queryr = "SELECT * FROM Producto";
            DataTable tprod = conObj.queryAdapter(queryr);
            string jsonLista = JsonConvert.SerializeObject(tprod);
            return new
            {
                success = true,
                message = "Exito",
                result = new
                {
                    categorias = JsonConvert.DeserializeObject<List<ProductosModel>>(jsonLista)
                }
            };
        }
        [HttpPut]
        [Route("Modificar")]
        public dynamic ModificarProducto(ProductosModel producto)
        {
            DBDatos conObj = new DBDatos();
            string query = "UPDATE Producto SET NombreProducto = '" + producto.NombreProducto + "', idCategoria = '" + producto.idCategoria + "', NombreCategoria = (SELECT NombreCategoria FROM CategoriaProducto WHERE idCategoria = '"+ producto.idCategoria+"'), DescripcionProducto = '"+producto.DescripcionProducto+"' WHERE idProducto = " + producto.idProducto;
            conObj.queryAdapter(query);
            string queryr = "SELECT * FROM Producto";
            DataTable tprod = conObj.queryAdapter(queryr);
            string jsonLista = JsonConvert.SerializeObject(tprod);
            return new
            {
                success = true,
                message = "Exito",
                result = new
                {
                    categorias = JsonConvert.DeserializeObject<List<ProductosModel>>(jsonLista)
                }
            };
        }
        [HttpDelete]
        [Route("Eliminar")]
        public dynamic EliminarProducto(ProductosId producto)
        {
            DBDatos conObj = new DBDatos();
            string query = "delete from producto where idProducto = '" + producto.idProducto + "'";
            conObj.queryAdapter(query);
            string queryr = "SELECT * FROM Producto";
            DataTable tprod = conObj.queryAdapter(queryr);
            string jsonLista = JsonConvert.SerializeObject(tprod);
            return new
            {
                success = true,
                message = "Exito",
                result = new
                {
                    categorias = JsonConvert.DeserializeObject<List<ProductosModel>>(jsonLista)
                }
            };
        }
    }
}
