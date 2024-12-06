using Microsoft.AspNetCore.Mvc;
using Route256.ApiModels.V3.DeliveryPrice;
using Route256.ApiModels.V3.GetHistory;
using Route256.BLL.DeliveryPrice;
using Route256.BLL.DeliveryPrice.Models;

namespace Route256.Controllers.V3;

[ApiController]
[Route("/v3/[controller]")]
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
            Wight = g.Width,
            Weight = g.Weight,
            
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
        
        return Ok(new GetHistoryResponse(result.Select(r => new CargoHistoryResponse(r.Volume, r.Weight, r.Price)).ToArray()));
    }
    
    [HttpPost]
    [Route("[action]")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult DeleteHistory()
    {
        _deliveryPriceService.DeleteHistoryCargos();
        return Ok();
    }
}