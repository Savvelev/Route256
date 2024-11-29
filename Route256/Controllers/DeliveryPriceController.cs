using Microsoft.AspNetCore.Mvc;
using Route256.Models.DeliveryPrice;

namespace Route256.Controllers;

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
    
    
}