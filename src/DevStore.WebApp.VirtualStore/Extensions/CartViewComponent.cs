using DevStore.Sales.Application.Queries;
using Microsoft.AspNetCore.Mvc;

namespace DevStore.WebApp.VirtualStore.Extensions;

public class CartViewComponent(IOrderQueries pedidoQueries) : ViewComponent
{
    private readonly IOrderQueries _orderQueries = pedidoQueries;

    // TODO: get logged client
    protected Guid ClientId = Guid.Parse("4885e451-b0e4-4490-b959-04fabc806d32");

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var carrinho = await _orderQueries.GetShoppingCartClient(ClientId);
        var itens = carrinho?.Items.Count ?? 0;

        return View(itens);
    }
}
