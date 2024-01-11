using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoriasController(AppDbContext context)
        {
            _context = context;
        }

        #region GET

        [HttpGet("produtos")]  //Rota diferente, definida para não dar conflito de métodos GET
        public ActionResult<IEnumerable<Categoria>> GetCategoriasProdutos()
        {
            // Retorna as Categorias com seus produtos relacionados, pelo "Método Include" 
            return _context.Categorias.Include(p => p.Produtos).ToList();
        }

        [HttpGet]
        public ActionResult<IEnumerable<Categoria>> Get()
        {
            var categorias = _context.Categorias.ToList();
            if (categorias == null)
            {
                return NotFound("Categorias não encontradas...");  // Retorna o erro quando os dados forem nulos
            }
            return categorias;
        }

        [HttpGet("{id:int}", Name = "ObterCategoria")]
        public ActionResult<Categoria> Get(int id)
        {
            var categorias = _context.Categorias.FirstOrDefault(p => p.CategoriaId == id);
            if (categorias is null)
            {
                return NotFound();
            }
            return categorias;
        }

        #endregion

        #region POST

        [HttpPost]
        public ActionResult Post(Categoria categoria)
        {
            if (categoria is null)
            {
                return BadRequest();
            }
            _context.Categorias.Add(categoria);
            _context.SaveChanges(); //Salva contexto no banco 

            //Retorna o 201 created no header location
            return new CreatedAtRouteResult("ObterCategoria", new { id = categoria.CategoriaId }, categoria);

        }



        #endregion

        #region PUT

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Categoria categoria)
        {
            if (id != categoria.CategoriaId) //Garanti alterar o produto correto
            {
                return BadRequest();
            }
            _context.Entry(categoria).State = EntityState.Modified; //Entidade precisa ser persistida
            _context.SaveChanges();

            return Ok(categoria); // retorna 200 e retorna também os dados do produto. 
        }


        #endregion

        #region DELETE
        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var categorias = _context.Categorias.FirstOrDefault(p => p.CategoriaId == id);

            if (categorias is null)
            {
                return NotFound("Categoria não encontrada...");
            }

            _context.Categorias.Remove(categorias);
            _context.SaveChanges(); //Salvando as alterações do contexto. 

            return Ok(categorias);
        }
        #endregion
    }
}
