using MarsRover.Models;

namespace MarsRover.Entities
{
    public class MarsRover : SpaceVehicle
    {
        public Position? StartingPosition { get; set; }
        public IEnumerable<Position>? Moves { get; set; }
    }
}
