using Domain.Interfaces.Generics;
using Entities.Entities;

namespace Domain.Interfaces.InterfaceCompraUsuario
{
    public interface ICompraUsuario: IGeneric<CompraUsuario>
    {
        public Task<int> QuantidadeProdutoCarrinhoUsuario(string userId);
    }
}
