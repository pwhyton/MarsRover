using MarsRover.Entities;
using MarsRover.Models;
using MarsRover.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace MarsRover.Controllers
{
    public class MarsRoverController : Controller
    {
        private readonly IOptions<PlateauOptions> _plateauOptions;
        private readonly ISpaceVehicleInstructionParser _spaceVehicleInstructionParser;
        private readonly IFormFileReader _formFileReader;
        public MarsRoverController(
            IOptions<PlateauOptions> plateauOptions,
            IFormFileReader formFileReader,
            ISpaceVehicleInstructionParser spaceVehicleInstructionParser)
        {
            _plateauOptions = plateauOptions ?? throw new ArgumentNullException(nameof(plateauOptions));
            if(_plateauOptions.Value == null)
            {
                throw new ArgumentNullException(nameof(plateauOptions.Value));
            }

            _formFileReader = formFileReader ?? throw new ArgumentNullException(nameof(formFileReader));
            _spaceVehicleInstructionParser = spaceVehicleInstructionParser ?? throw new ArgumentNullException(nameof(spaceVehicleInstructionParser));   
        }
      
        public IActionResult Index()
        {
            PlateauModel model = GetPlateauModel();

            return View(model);
        }

        

        [HttpPost("UploadFile")]
        public IActionResult UploadInstructions(IFormFile instructions)
        {
            var instructionLines = _formFileReader.ReadFormFile(instructions);
            var spaceVehicleInstructions = _spaceVehicleInstructionParser.ParseInstructions(instructionLines);

            return Json(spaceVehicleInstructions);
        }
       
        private PlateauModel GetPlateauModel()
        {
            return new PlateauModel
            {
                Height = _plateauOptions.Value.Height,
                Width = _plateauOptions.Value.Width
            };
        }
    }
}
