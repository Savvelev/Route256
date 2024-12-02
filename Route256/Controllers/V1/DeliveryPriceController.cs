using Microsoft.AspNetCore.Mvc;
using Route256.Models.V1.DeliveryPrice;
using Route256.Models.V1.GetHistory;

namespace Route256.Controllers.V1;

[ApiController]
[Route("/v1/[controller]")]
public class DeliveryPriceController : ControllerBase
{

    [HttpPost]
    [Route("[action]")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult Calculate(DeliveryPriceRequest request)
    {
        
        
        
        return Ok(new DeliveryPriceResponse(0));
    }


    [HttpPost]
    [Route("[action]")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetHistory(GetHistoryRequest request)
    {
        return Ok(new GetHistoryResponse(null));
    }
}