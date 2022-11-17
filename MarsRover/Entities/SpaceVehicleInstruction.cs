using MarsRover.Models;

namespace MarsRover.Entities
{
    public class SpaceVehicleInstruction
    {
        public IEnumerable<Position> Moves { get; set; }
    }
}
