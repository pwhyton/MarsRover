using MarsRover.Entities;

namespace MarsRover.Models
{
    public class MarsRoverMovingModel
    {
        public PlateauModel Plateau { get; set; }
        public IEnumerable<SpaceVehicleInstruction> Instructions { get; set; }
    }
}
