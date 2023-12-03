using AdventOfCodeAPI.Models;
using AdventOfCodeAPI.repository;
using Microsoft.AspNetCore.Mvc;

namespace AdventOfCodeAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Year2023Controller : ControllerBase
    {
        private readonly ILogger<Year2023Controller> _logger;
        private readonly AdventOfCode2023Logic adventOfCode2023Logic = new();

        private readonly AdventOfCode2023OneModel adventOfCode2023OneModel = new();
        private readonly AdventOfCode2023TwoModel adventOfCode2023TwoModel = new();
        private readonly AdventOfCode2023ThreeModel adventOfCode2023ThreeModel = new();

        public Year2023Controller(ILogger<Year2023Controller> logger)
        {
            _logger = logger;
        }

        [HttpPost()]
        [Route("1a")]
        public ActionResult<int> PostCalibrationSumA(bool sampleData)
        {
            string? data = sampleData ? adventOfCode2023OneModel.OneASampleData : adventOfCode2023OneModel.OneAData;
            var calibrationTexts = data.Split('\r').ToList();
            return adventOfCode2023Logic.OneALogic(calibrationTexts);
        }

        [HttpPost()]
        [Route("1b")]
        public ActionResult<int> PostCalibrationSumB(bool sampleData)
        {
            string? data = sampleData ? adventOfCode2023OneModel.OneBSampleData : adventOfCode2023OneModel.OneBData;
            var calibrationTexts = data.Split('\r').ToList();
            return adventOfCode2023Logic.OneBLogic(calibrationTexts);
        }

        [HttpPost()]
        [Route("2a")]
        public ActionResult<int> PostCalibrationSum2A(bool sampleData)
        {
            string? data = sampleData ? adventOfCode2023TwoModel.TwoASampleData : adventOfCode2023TwoModel.TwoAData;
            var gameTexts = data.Split('\r').ToList();
            return adventOfCode2023Logic.TwoALogic(gameTexts, adventOfCode2023TwoModel.TwoADataBlueCubeMax,
                adventOfCode2023TwoModel.TwoADataGreenCubeMax, adventOfCode2023TwoModel.TwoADataRedCubeMax);
        }

        [HttpPost()]
        [Route("2b")]
        public ActionResult<int> PostCalibrationSum2B(bool sampleData)
        {
            string? data = sampleData ? adventOfCode2023TwoModel.TwoBSampleData : adventOfCode2023TwoModel.TwoBData;
            var gameTexts = data.Split('\r').ToList();
            return adventOfCode2023Logic.TwoBLogic(gameTexts);
        }

        [HttpPost()]
        [Route("3a")]
        public ActionResult<int> PostCalibrationSum3A(bool sampleData)
        {
            string? data = sampleData ? adventOfCode2023ThreeModel.ThreeASampleData : adventOfCode2023ThreeModel.ThreeAData;
            var partTexts = data.Split("\r\n").ToList();
            return adventOfCode2023Logic.ThreeALogic(partTexts);
        }

        [HttpPost()]
        [Route("3b")]
        public ActionResult<int> PostCalibrationSum3B(bool sampleData)
        {
            string? data = sampleData ? adventOfCode2023ThreeModel.ThreeBSampleData : adventOfCode2023ThreeModel.ThreeBData;
            var partTexts = data.Split('\r').ToList();
            return adventOfCode2023Logic.ThreeBLogic(partTexts);
        }
    }
}
