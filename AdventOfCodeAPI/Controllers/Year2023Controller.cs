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
        private readonly AdventOfCode2023FourModel adventOfCode2023FourModel = new();
        private readonly AdventOfCode2023FiveModel adventOfCode2023FiveModel = new();
        private readonly AdventOfCode2023SixModel adventOfCode2023SixModel = new();

        public Year2023Controller(ILogger<Year2023Controller> logger)
        {
            _logger = logger;
        }

        [HttpPost()]
        [Route("Day1")]
        public ActionResult<int> PostDay1(bool sampleData = true, bool isPart1 = true, bool isPart2 = false)
        {
            if (isPart1 && isPart2 || (!isPart1 && !isPart2))
            {
                return 0;
            }
            string? data;
            if (isPart1)
            {
                data = sampleData ? adventOfCode2023OneModel.OneASampleData : adventOfCode2023OneModel.OneAData;
            }
            else
            {
                data = sampleData ? adventOfCode2023OneModel.OneBSampleData : adventOfCode2023OneModel.OneBData;
            }
            var dataRows = data.Split('\r').ToList();
            var total = isPart1 ? adventOfCode2023Logic.DayOnePartOneLogic(dataRows) :
                adventOfCode2023Logic.DayOnePartTwoLogic(dataRows);
            return total;
        }

        [HttpPost()]
        [Route("Day2")]
        public ActionResult<int> PostDay2(bool sampleData = true, bool isPart1 = true, bool isPart2 = false)
        {
            if (isPart1 && isPart2 || (!isPart1 && !isPart2))
            {
                return 0;
            }
            string? data;
            if (isPart1)
            {
                data = sampleData ? adventOfCode2023TwoModel.TwoASampleData : adventOfCode2023TwoModel.TwoAData;
            }
            else
            {
                data = sampleData ? adventOfCode2023TwoModel.TwoBSampleData : adventOfCode2023TwoModel.TwoBData;
            }
            var dataRows = data.Split('\r').ToList();
            var total = isPart1 ? adventOfCode2023Logic.DayTwoPartOneLogic(dataRows, 
                adventOfCode2023TwoModel.TwoADataBlueCubeMax,
                adventOfCode2023TwoModel.TwoADataGreenCubeMax, 
                adventOfCode2023TwoModel.TwoADataRedCubeMax) :
                adventOfCode2023Logic.DayTwoPartTwoLogic(dataRows);
            return total;
        }

        [HttpPost()]
        [Route("Day3")]
        public ActionResult<int> PostDay3(bool sampleData = true, bool isPart1 = true, bool isPart2 = false)
        {
            if (isPart1 && isPart2 || (!isPart1 && !isPart2))
            {
                return 0;
            }
            string? data;
            if (isPart1)
            {
                data = sampleData ? adventOfCode2023ThreeModel.ThreeASampleData : adventOfCode2023ThreeModel.ThreeAData;
            }
            else
            {
                data = sampleData ? adventOfCode2023ThreeModel.ThreeBSampleData : adventOfCode2023ThreeModel.ThreeBData;
            }
            var dataRows = data.Split("\r\n").ToList();
            var total = isPart1 ? adventOfCode2023Logic.DayThreePartOneLogic(dataRows) :
                adventOfCode2023Logic.DayThreePartTwoLogic(dataRows);
            return total;
        }

        [HttpPost()]
        [Route("Day4")]
        public ActionResult<int> PostDay4(bool sampleData = true, bool isPart1 = true, bool isPart2 = false)
        {
            if (isPart1 && isPart2 || (!isPart1 && !isPart2))
            {
                return 0;
            }
            string? data;
            if (isPart1)
            {
                data = sampleData ? adventOfCode2023FourModel.FourASampleData : adventOfCode2023FourModel.FourAData;
            }
            else
            {
                data = sampleData ? adventOfCode2023FourModel.FourBSampleData : adventOfCode2023FourModel.FourBData;
            }
            var dataRows = data.Split("\r\n").ToList();
            var total = isPart1 ? adventOfCode2023Logic.DayFourPartOneLogic(dataRows) :
                adventOfCode2023Logic.DayFourPartTwoLogic(dataRows);
            return total;
        }

        [HttpPost()]
        [Route("Day5")]
        public async Task<ActionResult<long>> PostDay5Async(bool sampleData = true, bool isPart1 = true, bool isPart2 = false)
        {
            if (isPart1 && isPart2 || (!isPart1 && !isPart2))
            {
                return 0;
            }
            string? data;
            if (isPart1)
            {
                data = sampleData ? adventOfCode2023FiveModel.FiveASampleData : adventOfCode2023FiveModel.FiveAData;
            }
            else
            {
                data = sampleData ? adventOfCode2023FiveModel.FiveBSampleData : adventOfCode2023FiveModel.FiveBData;
            }
            var dataRows = data.Split("\r\n\r\n").ToList();
            var total = isPart1 ? await adventOfCode2023Logic.DayFivePartOneLogicAsync(dataRows) :
                await adventOfCode2023Logic.DayFivePartTwoLogicAsync(dataRows);
            return total;
        }

        [HttpPost()]
        [Route("Day6")]
        public ActionResult<long> PostDay6(bool sampleData = true, bool isPart1 = true, bool isPart2 = false)
        {
            if (isPart1 && isPart2 || (!isPart1 && !isPart2))
            {
                return 0;
            }
            string? data;
            if (isPart1)
            {
                data = sampleData ? adventOfCode2023SixModel.SixASampleData : adventOfCode2023SixModel.SixAData;
            }
            else
            {
                data = sampleData ? adventOfCode2023SixModel.SixBSampleData : adventOfCode2023SixModel.SixBData;
            }
            var dataRows = data.Split("\r\n").ToList();
            var total = isPart1 ? adventOfCode2023Logic.DaySixPartOneLogic(dataRows) :
                adventOfCode2023Logic.DaySixPartTwoLogic(dataRows);
            return total;
        }
    }
}
