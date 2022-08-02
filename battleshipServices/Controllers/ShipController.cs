using battleshipServices.Model;
using battleshipServices.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace battleshipServices.Controllers;

/// <summary>
/// Endpoints to manage Ships, Attacks
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ShipController : Controller
{
    private readonly IShipService _shipService;

    /// <summary>
    /// Contructor
    /// </summary>
    /// <param name="shipService">Dependent service</param>
    public ShipController(IShipService shipService)
    {
        _shipService = shipService;
    }

    /// <summary>
    /// Adds a Ship to an already available game board. *Add gameId returned from AddBoard call here as a header*
    /// </summary>
    /// <returns>String: describes either ship was added or not.</returns>
    /// <remarks>
    /// Sample Request:
    /// 
    ///     POST api/ship
    ///     header: 2   // This should the gameId returned from POST api/board
    ///     body:       // Represents the Start and End Co-Ordinates, where the ship should be placed.
    ///         [{
    ///             "X": 1,
    ///             "Y": 2
    ///         },
    ///         {
    ///             "X": 1,
    ///             "Y": 5
    ///         }]
    ///     
    /// </remarks>
    /// <response code="200">Ship Added</response>
    /// <response code="400">Unable to add ship to board. Invalid Arguments</response>
    [HttpPost]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult AddShip([FromBody]List<CoOrdinates> coOrdinates)
    {
        if(coOrdinates == null || coOrdinates.Count != 2)
        {
            return BadRequest("Invalid Arguments. Ship was not added.");
        }

        var result = _shipService.AddShip(coOrdinates[0], coOrdinates[1]);
        if (result.success == true)
        {
            return Ok("Ship Added");
        }

        return BadRequest("Invalid Arguments. Ship was not added.");
    }

    /// <summary>
    /// Attacks a specific co-ordinate occupied by a ship. *Add gameId returned from AddBoard call here as a header*
    /// </summary>
    /// <returns>TRUE/FALSE i.e. whether it was a hit OR miss</returns>
    /// <remarks>
    /// Sample Request:
    /// 
    ///     DELETE api/ship
    ///     header: 2   // This should the gameId returned from POST api/board
    ///     body:       // Represents the cell co-ordinates, to attack.
    ///         {
    ///             "X": 1,
    ///             "Y": 2
    ///         }
    ///     
    /// </remarks>
    /// <response code="200">True/False: Signifies a hit OR a miss.</response>
    [HttpDelete]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult Attack([FromBody]CoOrdinates location)
    {
        var result = _shipService.Attack(location.X, location.Y);
        if (result.success == true)
            return Ok(true);
        else
            return Ok(false);
    }
}

