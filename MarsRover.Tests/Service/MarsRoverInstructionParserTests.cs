using MarsRover.Service;
using Xunit;
using System.Collections.Generic;
using System.Linq;
using System;

namespace MarsRover.Tests.Service
{
    public class MarsRoverInstructionParserTests
    {
        private readonly MarsRoverInstructionParser _sut;
        private List<string> _instructionLines = new List<string>();

        public MarsRoverInstructionParserTests()
        {
            _sut = new MarsRoverInstructionParser();
        }

        public class GivenARequestToParseInstructions : MarsRoverInstructionParserTests
        {
            [Fact]
            public void WhenTheInstructionsAreEmpty_ThenIExpectAnEmptyResult()
            {
                var instructions = _sut.ParseInstructions(new List<string>());

                Assert.Empty(instructions);
            }

            [Fact]
            public void WhenInstructionsAreInvalid_ThenIExpectAnException()
            {
                const string InstructionLine = "0 0 N";
                _instructionLines.Add(InstructionLine);

                Assert.Throws<Exception>(()=>_sut.ParseInstructions(_instructionLines));
            }

            [Fact]
            public void WhenStartingPositionIsNotFormattedCorrectly_ThenIExpectAnException()
            {
                const string InstructionLine = "0 0|M";
                _instructionLines.Add(InstructionLine);

                Assert.Throws<Exception>(() => _sut.ParseInstructions(_instructionLines));
            }

            [Fact]
            public void WhenThereAreNoMoves_ThenIExpectAnException()
            {
                const string InstructionLine = "0 0 N|";

                _instructionLines.Add(InstructionLine);

                Assert.Throws<Exception>(() => _sut.ParseInstructions(_instructionLines));
            }

            [Fact]
            public void WhenThereAreMultipleLines_ThenMultipleInstructionsAreReturned()
            {
                const string InstructionLine = "0 0 N|MMLLLL";

                _instructionLines.Add(InstructionLine);
                _instructionLines.Add(InstructionLine);

                var instructions = _sut.ParseInstructions(_instructionLines);

                Assert.Equal(2, instructions.Count());    
            }

            [Fact]
            public void WhenTheInstructionIsLeft_ThenOnlyDirectionChanges()
            {
                const string InstructionLine = "0 0 N|LLLL";
                _instructionLines.Add(InstructionLine);

                var instructions = _sut.ParseInstructions(_instructionLines).ToList();

                Assert.True(instructions[0].Moves.All(m => m.X == 0)); 
                Assert.True(instructions[0].Moves.All(m => m.Y == 0));
                Assert.True(instructions[0].Moves.First().Direction.DirectionSymbol == "N");
                Assert.True(instructions[0].Moves.Skip(1).First().Direction.DirectionSymbol == "W");
                Assert.True(instructions[0].Moves.Skip(2).First().Direction.DirectionSymbol == "S");
                Assert.True(instructions[0].Moves.Skip(3).First().Direction.DirectionSymbol == "E");
                Assert.True(instructions[0].Moves.Skip(4).First().Direction.DirectionSymbol == "N");

            }

            [Fact]
            public void WhenTheInstructionIsRight_ThenOnlyDirectionChanges()
            {
                const string InstructionLine = "0 0 N|RRRR";
                _instructionLines.Add(InstructionLine);

                var instructions = _sut.ParseInstructions(_instructionLines).ToList();

                Assert.True(instructions[0].Moves.All(m => m.X == 0));
                Assert.True(instructions[0].Moves.All(m => m.Y == 0));
                Assert.True(instructions[0].Moves.First().Direction.DirectionSymbol == "N");
                Assert.True(instructions[0].Moves.Skip(1).First().Direction.DirectionSymbol == "E");
                Assert.True(instructions[0].Moves.Skip(2).First().Direction.DirectionSymbol == "S");
                Assert.True(instructions[0].Moves.Skip(3).First().Direction.DirectionSymbol == "W");
                Assert.True(instructions[0].Moves.Skip(4).First().Direction.DirectionSymbol == "N");

            }

            [Fact]
            public void WhenTheInstructionIsMove_ThenPositionChangesAndDirectionRemainsSame()
            {
                const string InstructionLine = "0 0 N|MMMM";
                _instructionLines.Add(InstructionLine);

                var instructions = _sut.ParseInstructions(_instructionLines).ToList();

                Assert.True(instructions[0].Moves.All(m => m.Direction.DirectionSymbol == "N"));
                Assert.True(instructions[0].Moves.All(m => m.X == 0));
                Assert.True(instructions[0].Moves.First().Y == 0);
                Assert.True(instructions[0].Moves.Skip(1).First().Y == 1);
                Assert.True(instructions[0].Moves.Skip(2).First().Y == 2);
                Assert.True(instructions[0].Moves.Skip(3).First().Y == 3);
                Assert.True(instructions[0].Moves.Skip(4).First().Y == 4);

            }

            [Fact]
            public void WhenThereIsAStartingPosition_ThenFirstMoveIsStartingPosition()
            {
                const string InstructionLine = "0 0 N|MMM";
                _instructionLines.Add(InstructionLine);

                var instructions = _sut.ParseInstructions(_instructionLines).ToList();

                Assert.Single(instructions);
                var firstMove = instructions[0].Moves.First();
                Assert.Equal(0, firstMove.X);
                Assert.Equal(0, firstMove.Y);
                Assert.Equal("N", firstMove.Direction.DirectionSymbol);
            }
        }

        

    }
}
