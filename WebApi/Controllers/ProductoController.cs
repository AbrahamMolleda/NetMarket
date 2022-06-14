using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dtos;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly IGenericRepository<Producto> _productoRepository;
        private readonly IMapper _mapper;
        public ProductoController(IGenericRepository<Producto> productoRepository, IMapper mapper)
        {
            _productoRepository = productoRepository;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductoDto>> GetProductoById(int id)
        {
            // spec = Logica de la condicion de la consulta y tambien las relaciones entres las entidades
            // La relacion entre Producto, Marca y Categoria
            var spec = new ProductoWithCategoriaAndMarcaSpecification(id);
            //var producto = await _productoRepository.GetByIdAsync(id);
            var producto = await _productoRepository.GetByIdWithSpec(spec);

            return _mapper.Map<Producto, ProductoDto>(producto);
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductoDto>>> GetAllProductos()
        {
            var spec = new ProductoWithCategoriaAndMarcaSpecification();
            var productos = await _productoRepository.GetAllWithSpec(spec);
            return Ok(_mapper.Map<IReadOnlyList<Producto>, IReadOnlyList<ProductoDto>>(productos));
        }

    }
}
