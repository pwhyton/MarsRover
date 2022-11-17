using MarsRover.Entities;

namespace MarsRover.Service
{
    public interface ISpaceVehicleInstructionParser
    {
        IEnumerable<SpaceVehicleInstruction> ParseInstructions(IEnumerable<string> instructionLines);
    }
}
