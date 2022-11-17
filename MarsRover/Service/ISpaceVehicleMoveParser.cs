using MarsRover.Models;

namespace MarsRover.Service
{
    public interface ISpaceVehicleMoveParser
    {
        Dictionary<int, Position> ParseMoves(IEnumerable<Position> moves);
    }
}
