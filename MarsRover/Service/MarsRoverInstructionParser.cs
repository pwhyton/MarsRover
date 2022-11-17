using MarsRover.Entities;
using MarsRover.Models;

namespace MarsRover.Service
{
    public class MarsRoverInstructionParser : ISpaceVehicleInstructionParser
    {
        private const string MovesDelimiter = "|";
        private const string StartPositionDelimiter = " ";
        const int StartPositionParts = 3;
        private List<Direction> _directions = new List<Direction>
        {
            new North(),
            new South(),
            new East(),
            new West()
        };

        private readonly Dictionary<string, Func<Position, Position>> _moveFunctions;

        public MarsRoverInstructionParser()
        {
            _moveFunctions = new Dictionary<string, Func<Position, Position>>
            {
                {"L", (p) =>
                    {
                        return new Position(p.X,p.Y,p.Direction.MoveLeft());
                    }
                },
                {"R", (p) =>
                    {
                        return new Position(p.X,p.Y,p.Direction.MoveRight());
                    }
                },
                {"M", (p) =>
                    {
                        var newCoordinates = p.Direction.MoveOneSpace(p.X, p.Y);
                        return new Position(newCoordinates.Item1, newCoordinates.Item2, p.Direction);
                    }
                }
            };
        }
        public IEnumerable<SpaceVehicleInstruction> ParseInstructions(IEnumerable<string> instructionLines)
        {
            if (!ValidateInstructions(instructionLines))
            {
                throw new Exception("Instructions are not valid");
            }

            var instructions = new List<SpaceVehicleInstruction>();

            foreach (var instructionLine in instructionLines)
            {
                var startingPosition = GetStartingPosition(instructionLine);
                instructions.Add(new SpaceVehicleInstruction
                {                    
                    Moves = GetMoves(instructionLine, startingPosition)
                });
            }

            return instructions;
        }

        private IEnumerable<Position> GetMoves(string instructionLine, Position startingPosition)
        {
            var positions = new List<Position> { startingPosition };
            var movesInstruction = instructionLine.Split(MovesDelimiter)[1];
            if (movesInstruction.Length == 0)
            {
                return positions;
            }

            var moves = movesInstruction.ToArray();
            var currentPosition = startingPosition;
            
            foreach (var move in moves)
            {
                if (!_moveFunctions.ContainsKey(move.ToString()))
                {
                    throw new ArgumentOutOfRangeException($"Move {move} is not valid");
                }
                var moveFunction = _moveFunctions[move.ToString()];
                var newPosition = moveFunction(currentPosition);
                positions.Add(newPosition);
                currentPosition = newPosition;
            }

            return positions;
        }

        private Position GetStartingPosition(string instructionLine)
        {
            var positionInstruction = instructionLine.Split(MovesDelimiter)[0];
            if (string.IsNullOrWhiteSpace(positionInstruction))
            {
                throw new ArgumentOutOfRangeException(nameof(positionInstruction));
            }

            var startPositionParts = positionInstruction.Split(StartPositionDelimiter);

            var direction = _directions.SingleOrDefault(d => d.DirectionSymbol.Equals(startPositionParts[2], StringComparison.OrdinalIgnoreCase));
            if (direction == null)
            {
                throw new Exception("Direction not valid");
            }

            return new Position(int.Parse(startPositionParts[0]), int.Parse(startPositionParts[1]), direction);
        }

        private bool ValidateInstructions(IEnumerable<string> instructionLines)
        {            

            if (!instructionLines.All(x => x.Contains("|"))){
                return false;
            }

            if (!instructionLines.All(x => x.Split("|")[0].Split(" ").Length == StartPositionParts))
            {
                return false;
            }

            if(!instructionLines.All(x=>x.Split("|")[1].Length>0))
            {
                return false;
            }

            return true;
        }


    }
}
