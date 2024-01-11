using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProdutosController(AppDbContext context)
        {
            _context = context;
        }

        #region GET
        [HttpGet]
        public ActionResult<IEnumerable<Produto>> Get()
        {
            var produtos = _context.Produtos.ToList();
            if(produtos is null)
            {
                return NotFound("Produtos não encontrados...");  // Retorna o erro quando os dados forem nulos
            }
            return produtos;
        }

        [HttpGet("{id:int}", Name="ObterProduto")] 
        public ActionResult<Produto> Get(int id)
        {
            var produtos = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);
            if(produtos is null)
            {
                return NotFound();
            }
            return produtos;
        }
        #endregion

        #region POST

        [HttpPost]
        public ActionResult Post(Produto produto)
        {
            if(produto is null)
            {
                return BadRequest();
            }
            _context.Produtos.Add(produto);
            _context.SaveChanges(); //Salva contexto no banco 

            //Retorna o 201 created no header location
            return new CreatedAtRouteResult("ObterProduto", new { id = produto.ProdutoId }, produto);
            
        }



        #endregion


        #region PUT

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Produto produto)
        {
            if(id != produto.ProdutoId) //Garanti alterar o produto correto
            {
                return BadRequest();
            }
            _context.Entry(produto).State = EntityState.Modified; //Entidade precisa ser persistida
            _context.SaveChanges();

            return Ok(produto); // retorna 200 e retorna também os dados do produto. 
        }


        #endregion


        #region DELETE
        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);

            if(produto is null)
            {
                return NotFound("Produto não encontrado...");
            }

            _context.Produtos.Remove(produto);
            _context.SaveChanges(); //Salvando as alterações do contexto. 

            return Ok(produto);
        }
        #endregion





    }
}
