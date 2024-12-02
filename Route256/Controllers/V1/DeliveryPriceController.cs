using Microsoft.AspNetCore.Mvc;
using Route256.BLL.DeliveryPrice;
using Route256.BLL.DeliveryPrice.Models;
using Route256.Models.V1.DeliveryPrice;
using Route256.Models.V1.GetHistory;

namespace Route256.Controllers.V1;

[ApiController]
[Route("/v1/[controller]")]
public class DeliveryPriceController : ControllerBase
{
    private readonly IDeliveryPriceService _deliveryPriceService;

    public DeliveryPriceController(IDeliveryPriceService deliveryPriceService)
    {
        _deliveryPriceService = deliveryPriceService;
    }
    [HttpPost]
    [Route("[action]")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult Calculate(DeliveryPriceRequest request)
    {
        var goods = request?.Goods?.Select(g => new GoodsModel()
        {
            Height = g.Height,
            Lenght = g.Length,
            Wight = g.Width
        }).ToArray();
        
        var deliveryPrice = _deliveryPriceService.CalculateDeliveryPrice(goods);
        
        return Ok(new DeliveryPriceResponse(deliveryPrice));
    }


    [HttpPost]
    [Route("[action]")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetHistory(GetHistoryRequest request)
    {
        
        var result = _deliveryPriceService.GetHistoryCargos(request.Take);
        
        return Ok(new GetHistoryResponse(result.Select(r => new CargoHistoryResponse(r.Volume, r.Price)).ToArray()));
    }
}