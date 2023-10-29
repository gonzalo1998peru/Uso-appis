using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using Uso_appis.Data;
using Uso_appis.Models;
using Uso_appis.Service;


namespace Uso_appis.Controllers.Rest
{
    [ApiController]
    [Route("api/producto")]
    public class ProductoApiController : ControllerBase
    {
        private readonly ProductoService _productoService;

        public ProductoApiController(ProductoService productoService)
        {
            _productoService = productoService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<Producto>>> List()
        {
            var productos = await _productoService.GetAll();
            if(productos == null)
                return NotFound();
            return Ok(productos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Producto>> GetProducto(int? id)
        {
            var producto = await _productoService.Get(id);
            if(producto == null)
                return NotFound();
            return Ok(producto);
        }

        [HttpPost]
        public async Task<ActionResult<Producto>> CreateProducto(Producto producto){
            if (producto == null)
            {
                return BadRequest();
            }
            await _productoService.CreateOrUpdate(producto);
            return Ok(producto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProducto(int? id)
        {
            await _productoService.Delete(id);
            return Ok();
        }
    }
}