using Entities.Entities;

namespace ApplicationApp.Interfaces
{
    public interface InterfaceProductApp : InterfaceGenericaApp<Produto>
    {
        Task AddProduct(Produto produto);
        Task UpdateProduct(Produto produto);

        //Task<List<Produto>> ListarProdutosUsuario(string userId);
        //Task<List<Produto>> ListarProdutosComEstoque(string descricao);
        //Task<List<Produto>> ListarProdutosCarrinhoUsuario(string userId);
        //Task<Produto> ObterProdutoCarrinho(int idProdutoCarrinho);
        //Task<List<Produto>> ListarProdutosVendidos(string userId, string filtro);
    }
}
