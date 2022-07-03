using ApplicationApp.Interfaces;
using Entities.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Web_ECommerce.Controllers
{
    [Authorize]
    public class ProdutosController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly InterfaceProductApp _interfaceProductApp;
        private readonly InterfaceCompraUsuarioApp _InterfaceCompraUsuarioApp;
        public ProdutosController(InterfaceProductApp InterfaceProductApp, 
            InterfaceCompraUsuarioApp InterfaceCompraUsuarioApp, 
            UserManager<ApplicationUser> userManager)
        {
            _interfaceProductApp = InterfaceProductApp;
            _InterfaceCompraUsuarioApp = InterfaceCompraUsuarioApp;
            _userManager = userManager;
        }
        // GET: ProdutosController
        public async Task<IActionResult> Index()
        {
            var UserId = await RetornarIdUsuarioLogado();
           
            return View(await _interfaceProductApp.ListarProdutosUsuario(UserId));
        }

        // GET: ProdutosController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            return View(await _interfaceProductApp.GetEntityById(id));
        }

        // GET: ProdutosController/Create
        public async Task<IActionResult> Create()
        {
            return View();
        }

        // POST: ProdutosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Produto produto)
        {
            try
            {
                var UserId = await RetornarIdUsuarioLogado();
                produto.UserId = UserId;
                await _interfaceProductApp.AddProduct(produto);
                if (produto.Notitycoes.Any())
                {
                    foreach (var item in produto.Notitycoes)
                    {
                        ModelState.AddModelError(item.NomePropriedade, item.mensagem);
                    }
                    return View("Create",produto);
                }                
            }
            catch
            {
                return View("Create", produto);
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: ProdutosController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            return View(await _interfaceProductApp.GetEntityById(id));
        }

        // POST: ProdutosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Produto produto)
        {
            try
            {
                await _interfaceProductApp.UpdateProduct(produto);
                if (produto.Notitycoes.Any())
                {
                    foreach (var item in produto.Notitycoes)
                    {
                        ModelState.AddModelError(item.NomePropriedade, item.mensagem);
                    }

                    ViewBag.Alerta = true;
                    ViewBag.Mensagem = "Verifique, ocorreu algum erro!";

                    return View("Edit", produto);
                }
            }
            catch
            {
                return View("Edit", produto);
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: ProdutosController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            return View(await _interfaceProductApp.GetEntityById(id));
        }

        // POST: ProdutosController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, Produto produto)
        {
            try
            {
                var produtoDeletar = await _interfaceProductApp.GetEntityById(id);
                await _interfaceProductApp.Delete(produtoDeletar);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private async Task<string> RetornarIdUsuarioLogado()
        {
            var UserLogged = await _userManager.GetUserAsync(User);
            return UserLogged.Id;
        }

        //[AllowAnonymous]
        //[HttpGet("/api/ListarProdutosComEstoque")]
        //public async Task<JsonResult> ListarProdutosComEstoque(string descricao)
        //{
        //    return Json(await _interfaceProductApp.ListarProdutosComEstoque(descricao));
        //}

        [AllowAnonymous]
        [HttpGet("/api/ListarProdutosComEstoque")]
        public async Task<JsonResult> ListarProdutosComEstoque()
        {
            return Json(await _interfaceProductApp.ListarProdutosComEstoque(""));
        }

        public async Task<IActionResult> ListarProdutosCarrinhoUsuario()
        {
            var UserId = await RetornarIdUsuarioLogado();
            return View(await _interfaceProductApp.ListarProdutosCarrinhoUsuario(UserId));
        }


      
        public async Task<IActionResult> RemoverCarrinho(int id)
        {
            return View(await _interfaceProductApp.ObterProdutoCarrinho(id));
        }

        // POST: ProdutosController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoverCarrinho(int id, Produto produto)
        {
            try
            {
                var produtoDeletar = await _InterfaceCompraUsuarioApp.GetEntityById(id);
                await _InterfaceCompraUsuarioApp.Delete(produtoDeletar);
                return RedirectToAction(nameof(ListarProdutosCarrinhoUsuario));
            }
            catch
            {
                return View();
            }
        }



    }
}
