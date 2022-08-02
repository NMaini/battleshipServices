namespace battleshipServices.Services;

/// <summary>
/// Singleton service created to manage Application state.
/// </summary>
public class BattleshipSingleton
{
    private Dictionary<int, int[,]> _boards = new Dictionary<int, int[,]>();
    private readonly object _boardsLock = new object();

    /// <summary>
    /// represents dictionary of (Id, Board)
    /// </summary>
    public Dictionary<int, int[,]> Boards
    {
        get
        {
            lock (_boardsLock)
            {
                return _boards;
            }
        }
    }

    /// <summary>
    /// Game Id
    /// </summary>
    public int gameId;

    /// <summary>
    /// Contructor.
    /// </summary>
    public BattleshipSingleton()
    {
        gameId = 0;
    }

    /// <summary>
    /// Adds a new Board.
    /// </summary>
    /// <returns>gameId</returns>
    public int AddBoard()
    {
        Boards.Add(++gameId, new int[10, 10]);
        return gameId;
    }
}
