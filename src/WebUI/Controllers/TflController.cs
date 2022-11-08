using Microsoft.AspNetCore.Mvc;
using TransformDeveloperTest.Application.Tfl.Queries.GetStationArrivals;

namespace WebUI.Controllers;

public class TflController : ApiControllerBase
{

    [HttpGet]
    [Route("GetStationArrivals")]
    public async Task<ArrivalsResultDto> GetStationArrivals()
    {
        return await Mediator.Send(new GetStationArrivalsQuery());
    }
}
