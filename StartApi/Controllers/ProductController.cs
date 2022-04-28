using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StartProject.Entities;
using StartProject.Interfaces;

namespace StartApi.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IProduct IProduto;
        public ProductController(IProduct IProduto)
        {
            this.IProduto = IProduto;
        }


        [HttpGet("/api/ListaProdutos")]
        public async Task<JsonResult> ListaProdutos()
        {
            return Json(await this.IProduto.GetAll());
        }


        [HttpPost("/api/AdicionarProduto")]
        public async Task AdicionarProduto([FromBody] Product product)
        {
            await Task.FromResult(this.IProduto.Add(product));
        }

    }
}
