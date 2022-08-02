using battleshipServices.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace battleshipServices.Controllers;

/// <summary>
/// Endpoints to Manage Boards.
/// </summary>
[Route("api/[controller]")]
public class BoardController : ControllerBase
{
    private readonly IBoardService _boardService;

    /// <summary>
    /// Contructor
    /// </summary>
    /// <param name="boardService">Dependent service</param>
    public BoardController(IBoardService boardService)
    {
        _boardService = boardService;
    }

    /// <summary>
    /// Adds a Board OR Starts a new game of Battleship. *gameId need not be passed as header for this call.*
    /// </summary>
    /// <returns>A GameId.</returns>
    /// <remarks>
    /// Sample Request:
    /// 
    ///     POST api/board
    ///     
    /// </remarks>
    /// <response code="201">Returns the newly created GameId, which should be included in Header for calls to Add Ship/Attack</response>
    /// <response code="417">Failed to create a board.</response>
    [HttpPost]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status417ExpectationFailed)]
    public IActionResult Post()
    {
        var result = _boardService.AddBoard();
        if(result.success != true)
        {
            return Problem(statusCode: StatusCodes.Status417ExpectationFailed);
        }

        return Created(this.HttpContext.Request.Path.ToUriComponent(), result.gameId);
    }
}