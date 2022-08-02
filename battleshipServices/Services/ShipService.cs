using battleshipServices.Model;
using Microsoft.Extensions.Primitives;

namespace battleshipServices.Services
{
    /// <summary>
    /// Service: exposes public methods to support AddShip and Attack.
    /// </summary>
    public class ShipService: IShipService
    {
        private readonly BattleshipSingleton _battleship;
        private readonly HttpContext? _httpContext;
        
        /// <summary>
        /// Contructor
        /// </summary>
        /// <param name="battleship">State managing Singleton</param>
        /// <param name="httpContextAccessor">Provides httpContext</param>
        public ShipService(BattleshipSingleton battleship, IHttpContextAccessor httpContextAccessor)
        {
            _battleship = battleship;
            _httpContext = httpContextAccessor?.HttpContext;
        }

        /// <summary>
        /// Adds a ship to board.
        /// </summary>
        /// <param name="start">Start Co-ordinates</param>
        /// <param name="end">End Co-ordinates</param>
        /// <returns>Result whether ship was successfully added.</returns>
        public AddShipResult AddShip(CoOrdinates start, CoOrdinates end)
        {
            var result = false;
            var gameId = GetGameId();

            if(gameId == int.MinValue)
                return new AddShipResult { success = result };

            if (start.X == end.X)
            {
                if(start.Y > end.Y)
                {
                    for(int i = end.Y; i <= start.Y; i++)
                    {
                        if (_battleship.Boards[gameId][start.X, i] == 1)
                            return new AddShipResult { success = result };
                    }

                    for (int i = end.Y; i <= start.Y; i++)
                    {
                        _battleship.Boards[gameId][start.X, i] = 1;
                    }
                    result = true;
                }
                else
                {
                    for (int i = start.Y; i <= end.Y; i++)
                    {
                        if (_battleship.Boards[gameId][start.X, i] == 1)
                            return new AddShipResult { success = result };
                    }

                    for (int i = start.Y; i <= end.Y; i++)
                    {
                        _battleship.Boards[gameId][start.X, i] = 1;
                    }
                    result = true;
                }
            }
            else if (start.Y == end.Y)
            {
                if (start.X > end.X)
                {
                    for (int i = end.X; i <= start.X; i++)
                    {
                        if (_battleship.Boards[gameId][start.Y, i] == 1)
                            return new AddShipResult { success = result };
                    }

                    for (int i = end.X; i <= start.X; i++)
                    {
                        _battleship.Boards[gameId][start.Y, i] = 1;
                    }
                    result = true;
                }
                else
                {
                    for (int i = start.X; i <= end.X; i++)
                    {
                        if (_battleship.Boards[gameId][start.Y, i] == 1)
                            return new AddShipResult { success = result };
                    }

                    for (int i = start.X; i <= end.X; i++)
                    {
                        _battleship.Boards[gameId][start.Y, i] = 1;
                    }
                    result = true;
                }
            }

            return new AddShipResult { success = result };
        }

        /// <summary>
        /// carries out an Attack
        /// </summary>
        /// <param name="x">x co-ordinate</param>
        /// <param name="y">y co-ordinate</param>
        /// <returns>True/false: Whether attack was a hit or a miss.</returns>
        public AttackResult Attack(int x, int y)
        {
            var result = false;

            var gameId = GetGameId();
            if(gameId == int.MinValue)
            {
                return new AttackResult { success = result };
            }

            if (_battleship.Boards[gameId][x, y] == 1)
            {
                _battleship.Boards[gameId][x, y] = 0;
                result = true;
            }

            return new AttackResult { success = result };
        }

        private int GetGameId()
        {
            StringValues sv;
            if (_httpContext.Request.Headers.TryGetValue("gameId", out sv))
            {
                var gameId = sv.FirstOrDefault() ?? string.Empty;
                if (!string.IsNullOrWhiteSpace(gameId))
                {
                    int id = int.MinValue;
                    if (int.TryParse(gameId, out id))
                    {
                        return id;
                    }
                }
            }

            return int.MinValue;
        }

    }
}
