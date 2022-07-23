using Domain.Interfaces.InterfaceProduct;
using Domain.Interfaces.InterfaceServices;
using Domain.Services;
using Entities.Entities;
using Infrastructure.Repository.Repositories;

namespace UnitTestEcommerceDDD
{
    [TestClass]
    public class UnitTestEcommerce
    {
        [TestMethod]
        public async Task AddProductWithSuccess()
        {
            try
            {
                IProduct _IProduct = new RepositoryProduct();
                IServiceProduct _IServiceProduct = new ServiceProduct(_IProduct);
                var product = new Produto
                {
                    Descricao = String.Concat("Descrição Test TDD", DateTime.Now.ToString()),
                    QtdEstoque = 10,
                    Nome = String.Concat("Nome Test TDD", DateTime.Now.ToString()),
                    Valor = 20,
                    UserId = "1da20eff-b83c-42b9-a473-ac71f4971df9"
                };

                await _IServiceProduct.AddProduct(product);

                Assert.IsFalse(product.Notitycoes.Any());
            }
            catch (Exception)
            {

                Assert.Fail();
            }
        }

        [TestMethod]
        public async Task AddProductWithMandatoryFieldsWithValidation()
        {
            try
            {
                IProduct _IProduct = new RepositoryProduct();
                IServiceProduct _IServiceProduct = new ServiceProduct(_IProduct);
                var product = new Produto
                {
                };

                await _IServiceProduct.AddProduct(product);

                Assert.IsTrue(product.Notitycoes.Any());
            }
            catch (Exception)
            {

                Assert.Fail();
            }

        }
        [TestMethod]
        public async Task ListUserProducts()
        {
            try
            {
                IProduct _IProduct = new RepositoryProduct();
                var listProducts = await _IProduct.ListarProdutosUsuario("1da20eff-b83c-42b9-a473-ac71f4971df9");
                Assert.IsTrue(listProducts.Any());
            }
            catch (Exception)
            {

                Assert.Fail();
            }
        }
        [TestMethod]
        public async Task GetEntityById()
        {
            try
            {
                IProduct _IProduct = new RepositoryProduct();
                var listProducts = await _IProduct.ListarProdutosUsuario("1da20eff-b83c-42b9-a473-ac71f4971df9");

                var product = await _IProduct.GetEntityById(listProducts.LastOrDefault().Id) ;
                Assert.IsNotNull(product);
                Assert.IsTrue(product != null);
            }
            catch (Exception)
            {

                Assert.Fail();
            }

        }
        [TestMethod]
        public async Task Delete()
        {
            try
            {
                IProduct _IProduct = new RepositoryProduct();
                var listProducts = await _IProduct.ListarProdutosUsuario("1da20eff-b83c-42b9-a473-ac71f4971df9");

                var lastProduct = listProducts.LastOrDefault();

                await _IProduct.Delete(lastProduct);
          
                Assert.IsTrue(true);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

    }
}