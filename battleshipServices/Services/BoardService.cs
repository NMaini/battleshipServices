using battleshipServices.Model;


namespace battleshipServices.Services;

public class BoardService: IBoardService
{
    public readonly BattleshipSingleton _battleship;

    public BoardService(BattleshipSingleton battleship)
    {
        _battleship = battleship;
    }

    public AddBoardServiceResult AddBoard()
    {
        var _gameId = _battleship.AddBoard();
        return new AddBoardServiceResult { success = true, gameId = _gameId };
    }
}
