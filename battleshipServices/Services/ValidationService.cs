using battleshipServices.Model;

namespace battleshipServices.Services
{
    public class ValidationService: IValidationService
    {
        private readonly BattleshipSingleton _battleship;

        public ValidationService(BattleshipSingleton battleship)
        {
            _battleship = battleship;
        }
        public bool IsGameIdValid(int gameId)
        {
            return _battleship.Boards.ContainsKey(gameId);
        }
    }

    public interface IValidationService
    {
        public bool IsGameIdValid(int gameId);
    }
}
