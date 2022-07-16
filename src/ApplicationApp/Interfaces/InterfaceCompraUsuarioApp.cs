﻿using Entities.Entities;

namespace ApplicationApp.Interfaces
{
    public interface InterfaceCompraUsuarioApp : InterfaceGenericaApp<CompraUsuario>
    {
        public Task<int> QuantidadeProdutoCarrinhoUsuario(string userId);

        public Task<CompraUsuario> CarrinhoCompras(string userId);

        public Task<CompraUsuario> ProdutosComprados(string userId);

        public Task<bool> ConfirmaCompraCarrinhoUsuario(string userId);

        //public Task<List<CompraUsuario>> MinhasCompras(string userId);

        //public Task AdicionaProdutoCarrinho(string userId, CompraUsuario compraUsuario);

    }
}
