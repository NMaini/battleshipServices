using battleshipServices.Model;

namespace battleshipServices.Services
{
    public interface IShipService
    {
        AddShipResult AddShip(CoOrdinates start, CoOrdinates end);

        AttackResult Attack(int x, int y);
    }
}
