﻿using AdventOfCodeAPI.Models;
using System.Data;
using System.Text.RegularExpressions;

namespace AdventOfCodeAPI.repository
{
    public class AdventOfCode2023Logic
    {
        #region Day One
        public int DayOnePartOneLogic(List<string> calibrationTexts)
        {
            var calibrationTotal = 0;
            foreach (var calibration in calibrationTexts)
            {
                var numbersOnly = Regex.Replace(calibration, "[^0-9]", "");
                var firstNumber = numbersOnly.Substring(0, 1);
                var lastNumber = string.Empty;
                if (numbersOnly.Length > 1)
                {
                    lastNumber = numbersOnly.Substring(numbersOnly.Length - 1);
                }
                else
                {
                    lastNumber = firstNumber;
                }
                calibrationTotal += Convert.ToInt32(firstNumber + lastNumber);
            }
            return calibrationTotal;
        }

        public int DayOnePartTwoLogic(List<string> calibrationTexts)
        {
            var calibrationTotal = 0;
            foreach (var calibration in calibrationTexts)
            {
                var regexString = "one|two|three|four|five|six|seven|eight|nine|\\d";
                var groupsOfLettersOrFirstDigit = Regex.Matches(calibration, regexString);
                var reverseGroupsOfLettersOrFirstDigit = Regex.Matches(calibration, regexString, RegexOptions.RightToLeft);
                var firstNumberString = groupsOfLettersOrFirstDigit.First().ToString();
                var lastNumberString = reverseGroupsOfLettersOrFirstDigit.First().ToString();
                if (firstNumberString.Length > 1)
                {
                    firstNumberString = WordToNumberConverter(firstNumberString);
                }
                if (lastNumberString.Length > 1)
                {
                    lastNumberString = WordToNumberConverter(lastNumberString);
                }
                calibrationTotal += Convert.ToInt32(firstNumberString + lastNumberString);
            }
            return calibrationTotal;
        }
        #endregion

        #region Day Two
        public int DayTwoPartOneLogic(List<string> gameTexts, int blueThreshold, int greenThreshold, int redThreshold)
        {
            var gameTotal = 0;
            foreach (var game in gameTexts)
            {
                var gameSplit = game.Split(':');
                var gameNumberString = gameSplit[0].Replace("Game ", "");
                var cubeHandfulls = gameSplit[1].Split(";");
                var amountOverThreshold = false;
                foreach (var cubeHandfull in cubeHandfulls)
                {
                    var cubeColorAmounts = cubeHandfull.Split(",");
                    foreach (var cubeColor in cubeColorAmounts)
                    {
                        if (cubeColor.Contains("green"))
                        {
                            if (CheckCubeColorThreshold(cubeColor, greenThreshold)) { amountOverThreshold = true; }
                        }
                        if (cubeColor.Contains("blue"))
                        {
                            if (CheckCubeColorThreshold(cubeColor, blueThreshold)) { amountOverThreshold = true; }
                        }
                        if (cubeColor.Contains("red"))
                        {
                            if (CheckCubeColorThreshold(cubeColor, redThreshold)) { amountOverThreshold = true; }
                        }
                    }
                }
                if (!amountOverThreshold)
                {
                    gameTotal += Convert.ToInt32(gameNumberString);
                }
            }
            return gameTotal;
        }

        public int DayTwoPartTwoLogic(List<string> gameTexts)
        {
            var gameTotal = 0;
            foreach (var game in gameTexts)
            {
                var gameSplit = game.Split(':');
                var cubeHandfulls = gameSplit[1].Split(";");
                var blueCubeMax = 0;
                var greenCubeMax = 0; 
                var redCubeMax = 0;
                foreach (var cubeHandfull in cubeHandfulls)
                {
                    var cubeColorAmounts = cubeHandfull.Split(",");
                    foreach (var cubeColor in cubeColorAmounts)
                    {
                        if (cubeColor.Contains("green"))
                        {
                            greenCubeMax = CheckMaxCubeColor(cubeColor, greenCubeMax);
                        }
                        if (cubeColor.Contains("blue"))
                        {
                            blueCubeMax = CheckMaxCubeColor(cubeColor, blueCubeMax);
                        }
                        if (cubeColor.Contains("red"))
                        {
                            redCubeMax = CheckMaxCubeColor(cubeColor, redCubeMax);
                        }
                    }
                }
                gameTotal += greenCubeMax * blueCubeMax * redCubeMax;
            }
            return gameTotal;
        }
        #endregion

        #region Day Three
        public int DayThreePartOneLogic(List<string> partTexts)
        {
            var partNumberTotal = 0;
            var partNumberPositionCollections = new List<PartNumberPositionCollection>();
            var symbolPositionCollections = new List<SymbolPositionCollection>();
            for (int i = 0; i < partTexts.Count; i++)
            {
                var numbersInString = Regex.Matches(partTexts[i], "[0-9][0-9]{0,4}");
                var nextIndex = 0;
                foreach (var number in numbersInString)
                {
                    var startingIndexOfPartNumber = partTexts[i].IndexOf(number.ToString(), nextIndex);
                    var endingIndex = startingIndexOfPartNumber + number.ToString().Length - 1;
                    nextIndex = endingIndex + 1;
                    var partNumberPositionCollection = new PartNumberPositionCollection
                    {
                        PartNumber = Convert.ToInt32(number.ToString()),
                        PartTextRowIndex = i,
                        PartNumberStartingIndex = startingIndexOfPartNumber,
                        PartNumberEndingIndex = endingIndex
                    };
                    partNumberPositionCollections.Add(partNumberPositionCollection);
                }
                for (int j = 0; j < partTexts[i].Length; j++)
                {
                    var isSymbol = Regex.IsMatch(partTexts[i].Substring(j, 1), "[^a-zA-Z\\d\\s.]");
                    if (isSymbol)
                    {
                        var symbolPositionCollection = new SymbolPositionCollection
                        {
                            PartTextRowIndex = i,
                            SymbolPositionIndex = j
                        };
                        symbolPositionCollections.Add(symbolPositionCollection);
                    }
                }
            }
            foreach (var partNumberPositionCollection in partNumberPositionCollections)
            {
                var isSymbolAdjacent = false;
                var rowAbove = partNumberPositionCollection.PartTextRowIndex - 1;
                var currentRow = partNumberPositionCollection.PartTextRowIndex;
                var rowBelow = partNumberPositionCollection.PartTextRowIndex + 1;
                var startingPosition = partNumberPositionCollection.PartNumberStartingIndex;
                var endingPosition = partNumberPositionCollection.PartNumberEndingIndex;
                var adjacentPositions = new List<int>();
                for (int i = startingPosition - 1; i <= endingPosition + 1; i++)
                {
                    adjacentPositions.Add(i);
                }
                var rowIndexes = new List<int>
                {
                    rowAbove, currentRow, rowBelow
                };
                foreach (var adjacentPosition in adjacentPositions)
                {
                    if(symbolPositionCollections.Where(x => x.PartTextRowIndex == rowAbove)
                        .Any(y => y.SymbolPositionIndex == adjacentPosition))
                    {
                        isSymbolAdjacent = true;
                    }
                    if (symbolPositionCollections.Where(x => x.PartTextRowIndex == currentRow)
                        .Any(y => y.SymbolPositionIndex == adjacentPosition))
                    {
                        isSymbolAdjacent = true;
                    }
                    if (symbolPositionCollections.Where(x => x.PartTextRowIndex == rowBelow)
                        .Any(y => y.SymbolPositionIndex == adjacentPosition))
                    {
                        isSymbolAdjacent = true;
                    }
                }
                if (isSymbolAdjacent)
                {
                    partNumberTotal += partNumberPositionCollection.PartNumber;
                }
            }
            return partNumberTotal;
        }

        public int DayThreePartTwoLogic(List<string> partTexts)
        {
            var gearRatioTotal = 0;
            var partNumberPositionCollections = new List<PartNumberPositionCollection>();
            var symbolPositionCollections = new List<SymbolPositionCollection>();
            for (int i = 0; i < partTexts.Count; i++)
            {
                var numbersInString = Regex.Matches(partTexts[i], "[0-9][0-9]{0,4}");
                var nextIndex = 0;
                foreach (var number in numbersInString)
                {
                    var startingIndexOfPartNumber = partTexts[i].IndexOf(number.ToString(), nextIndex);
                    var endingIndex = startingIndexOfPartNumber + number.ToString().Length - 1;
                    nextIndex = endingIndex + 1;
                    var partNumberPositionCollection = new PartNumberPositionCollection
                    {
                        PartNumber = Convert.ToInt32(number.ToString()),
                        PartTextRowIndex = i,
                        PartNumberStartingIndex = startingIndexOfPartNumber,
                        PartNumberEndingIndex = endingIndex
                    };
                    partNumberPositionCollections.Add(partNumberPositionCollection);
                }
                for (int j = 0; j < partTexts[i].Length; j++)
                {
                    var isSymbol = Regex.IsMatch(partTexts[i].Substring(j, 1), "[*]");
                    if (isSymbol)
                    {
                        var symbolPositionCollection = new SymbolPositionCollection
                        {
                            PartTextRowIndex = i,
                            SymbolPositionIndex = j
                        };
                        symbolPositionCollections.Add(symbolPositionCollection);
                    }
                }
            }
            foreach (var symbolPositionCollection in symbolPositionCollections)
            {
                var adjacentPartNumbers = new List<int>();
                var rowAbove = symbolPositionCollection.PartTextRowIndex - 1;
                var currentRow = symbolPositionCollection.PartTextRowIndex;
                var rowBelow = symbolPositionCollection.PartTextRowIndex + 1;
                var startingPosition = symbolPositionCollection.SymbolPositionIndex;
                var endingPosition = symbolPositionCollection.SymbolPositionIndex;
                var adjacentPositions = new List<int>();
                for (int i = startingPosition - 1; i <= endingPosition + 1; i++)
                {
                    adjacentPositions.Add(i);
                }
                var rowIndexes = new List<int>
                {
                    rowAbove, currentRow, rowBelow
                };
                var topAdjacentPartNumbers = partNumberPositionCollections.Where(x => x.PartTextRowIndex == rowAbove
                    && ((x.PartNumberEndingIndex >= adjacentPositions.First() && x.PartNumberEndingIndex <= adjacentPositions.Last())
                    || (x.PartNumberStartingIndex >= adjacentPositions.First() && x.PartNumberStartingIndex <= adjacentPositions.Last())))
                    .ToList();
                if (topAdjacentPartNumbers.Any())
                {
                    adjacentPartNumbers.AddRange(topAdjacentPartNumbers.Select(x => x.PartNumber));
                }
                var leftAdjacentPartNumber = partNumberPositionCollections.FirstOrDefault(x => x.PartTextRowIndex == currentRow
                    && x.PartNumberEndingIndex == adjacentPositions.First());
                if (leftAdjacentPartNumber != null)
                {
                    adjacentPartNumbers.Add(leftAdjacentPartNumber.PartNumber);
                }
                var rightAdjacentPartNumber = partNumberPositionCollections.FirstOrDefault(x => x.PartTextRowIndex == currentRow
                    && x.PartNumberStartingIndex == adjacentPositions.Last());
                if (rightAdjacentPartNumber != null)
                {
                    adjacentPartNumbers.Add(rightAdjacentPartNumber.PartNumber);
                }
                var bottomAdjacentPartNumbers = partNumberPositionCollections.Where(x => x.PartTextRowIndex == rowBelow
                    && ((x.PartNumberEndingIndex >= adjacentPositions.First() && x.PartNumberEndingIndex <= adjacentPositions.Last())
                    || (x.PartNumberStartingIndex >= adjacentPositions.First() && x.PartNumberStartingIndex <= adjacentPositions.Last())))
                    .ToList();
                if (bottomAdjacentPartNumbers.Any())
                {
                    adjacentPartNumbers.AddRange(bottomAdjacentPartNumbers.Select(x => x.PartNumber));
                }

                if (adjacentPartNumbers.Count == 2)
                {
                    int gearRatio = adjacentPartNumbers[0] * adjacentPartNumbers[1];
                    gearRatioTotal += gearRatio;
                }
            }
            return gearRatioTotal;
        }
        #endregion

        #region Day Four
        public int DayFourPartOneLogic(List<string> scratchOffTexts)
        {
            var winningsTotal = 0;
            foreach (var scratchOffText in scratchOffTexts)
            {
                var gameSection = scratchOffText.Split(":").ToList();
                var numbersSection = gameSection[1].Split("|").ToList();
                var winnableNumbers = numbersSection[0].Split(" ").ToList();
                winnableNumbers = winnableNumbers.Where(x => !string.IsNullOrWhiteSpace(x)).ToList();
                var playedNumbers = numbersSection[1].Split(" ").ToList();
                playedNumbers = playedNumbers.Where(x => !string.IsNullOrWhiteSpace(x)).ToList();
                var winningNumbers = winnableNumbers.Intersect(playedNumbers).ToList();
                winningsTotal += Convert.ToInt32(Math.Pow(2, winningNumbers.Count - 1));
            }
            return winningsTotal;
        }

        public int DayFourPartTwoLogic(List<string> scratchOffTexts)
        {
            var scratchOffsTotal = 0;
            var winningNumberAmounts = Enumerable.Repeat(1, scratchOffTexts.Count).ToList();
            for (int i = 0; i < scratchOffTexts.Count; i++)
            {
                var gameSection = scratchOffTexts[i].Split(":").ToList();
                var numbersSection = gameSection[1].Split("|").ToList();
                var winnableNumbers = numbersSection[0].Split(" ").ToList();
                winnableNumbers = winnableNumbers.Where(x => !string.IsNullOrWhiteSpace(x)).ToList();
                var playedNumbers = numbersSection[1].Split(" ").ToList();
                playedNumbers = playedNumbers.Where(x => !string.IsNullOrWhiteSpace(x)).ToList();
                var winningNumbers = winnableNumbers.Intersect(playedNumbers).ToList();
                for (int j = 0; j < winningNumberAmounts[i]; j++)
                {
                    for (int k = i; k < winningNumbers.Count + i; k++)
                    {
                        winningNumberAmounts[k + 1] += 1;
                    }
                }
            }
            scratchOffsTotal = winningNumberAmounts.Sum();
            return scratchOffsTotal;
        }
        #endregion

        #region Day Five
        public async Task<long> DayFivePartOneLogicAsync(List<string> dataRows)
        {
            var seedMaps = new SeedMaps();
            foreach (var dataRow in dataRows)
            {
                if (dataRow.Contains("seeds:"))
                {
                    var seedsSplits = dataRow.Split(':').ToList();
                    var seeds = seedsSplits[1].Split(" ").ToList();
                    seeds = seeds.Where(x => !string.IsNullOrWhiteSpace(x)).ToList();
                    seedMaps.SeedList = seeds.Select(long.Parse).ToList();
                }
                else
                {
                    seedMaps = CreateSeedMaps(dataRow, seedMaps);
                }
            }
            return await SeedToMinLocationMapTraversalAsync(seedMaps);
        }

        public async Task<long> DayFivePartTwoLogicAsync(List<string> dataRows)
        {
            var seedMaps = new SeedMaps
            {
                SeedList = new List<long>()
            };
            foreach (var dataRow in dataRows)
            {
                if (dataRow.Contains("seeds:"))
                {
                    var seedsSplits = dataRow.Split(':').ToList();
                    var seeds = seedsSplits[1].Split(" ").ToList();
                    seeds = seeds.Where(x => !string.IsNullOrWhiteSpace(x)).ToList();
                    var seedMapList = seeds.Select(long.Parse).ToList();
                    for (int i = 0; i < seedMapList.Count; i += 2)
                    {
                        for (int j = 0; j < seedMapList[i+1]; j++)
                        {
                            seedMaps.SeedList.Add(seedMapList[i] + j);
                        }
                    }
                }
                else
                {
                    seedMaps = CreateSeedMaps(dataRow, seedMaps);
                }
            }
            return await SeedToMinLocationMapTraversalAsync(seedMaps);
        }
        #endregion

        #region Day Six
        public int DaySixPartOneLogic(List<string> dataRows)
        {
            var winningButtonPressesList = new List<int>();
            var timesList = ConvertDataStringToIntString(dataRows[0]);
            var distancesList = ConvertDataStringToIntString(dataRows[1]);
            for (int i = 0; i < timesList.Count; i++)
            {
                var winningButtonPresses = 0;
                for (int j = 0; j < timesList[i] + 1; j++)
                {
                    if (j == 0) { continue; }
                    var minDistance = distancesList[i];
                    var maxTime = timesList[i];
                    var buttonHeld = j;

                    var distanceMoved = (maxTime - buttonHeld) * buttonHeld;
                    if (distanceMoved > minDistance)
                    {
                        winningButtonPresses += 1;
                    }
                }
                winningButtonPressesList.Add(winningButtonPresses);
            }
            return winningButtonPressesList.Aggregate((a, x) => a * x);
        }

        public int DaySixPartTwoLogic(List<string> dataRows)
        {
            var timesList = dataRows[0].Split(":").ToList();
            var maxTime = long.Parse(timesList[1].Trim().Replace(" ", ""));
            var distancesList = dataRows[1].Split(":").ToList();
            var minDistance = long.Parse(distancesList[1].Trim().Replace(" ", ""));
            var winningButtonPresses = 0;
            for (int i = 0; i < maxTime + 1; i++)
            {
                if (i == 0) { continue; }
                var buttonHeld = i;
                var distanceMoved = (maxTime - buttonHeld) * buttonHeld;
                if (distanceMoved > minDistance)
                {
                    winningButtonPresses += 1;
                }
            }
            return winningButtonPresses;
        }
        #endregion

        #region Day
        public int DayPartOneLogic(List<string> dataRows)
        {
            var Total = 0;
            foreach (var dataRow in dataRows)
            {

            }
            for (int i = 0; i < dataRows.Count; i++)
            {

            }
            return Total;
        }

        public int DayPartTwoLogic(List<string> dataRows)
        {
            var Total = 0;
            foreach (var dataRow in dataRows)
            {

            }
            for (int i = 0; i < dataRows.Count; i++)
            {

            }
            return Total;
        }
        #endregion

        #region private methods
        private static string? WordToNumberConverter(string word)
        {
            switch (word)
            {
                case "one":
                    return "1";
                case "two":
                    return "2";
                case "three":
                    return "3";
                case "four":
                    return "4";
                case "five":
                    return "5";
                case "six":
                    return "6";
                case "seven":
                    return "7";
                case "eight":
                    return "8";
                case "nine":
                    return "9";
                default:
                    return null;
            }
        }

        private static bool CheckCubeColorThreshold(string cubeColor, int colorThreshold)
        {
            var numberOfCubes = Regex.Replace(cubeColor, "[a-z]|\\s", "");
            if (Convert.ToInt32(numberOfCubes) > colorThreshold)
            {
                return true;
            }
            return false;
        }

        private static int CheckMaxCubeColor(string cubeColor, int colorCubeMax)
        {
            var numberOfCubes = Convert.ToInt32(Regex.Replace(cubeColor, "[a-z]|\\s", ""));
            if (numberOfCubes > colorCubeMax) 
            { 
                return numberOfCubes; 
            }
            return colorCubeMax;
        }

        private static SeedMaps CreateSeedMaps(string dataRow, SeedMaps seedMaps)
        {
            if (dataRow.Contains("seed-to-soil map:"))
            {
                seedMaps.SeedToSoilMaps = ReturnIntList(dataRow);
            }
            if (dataRow.Contains("soil-to-fertilizer map:"))
            {
                seedMaps.SoilToFertilizerMaps = ReturnIntList(dataRow);
            }
            if (dataRow.Contains("fertilizer-to-water map:"))
            {
                seedMaps.FertilizerToWaterMaps = ReturnIntList(dataRow);
            }
            if (dataRow.Contains("water-to-light map:"))
            {
                seedMaps.WaterToLightMaps = ReturnIntList(dataRow);
            }
            if (dataRow.Contains("light-to-temperature map:"))
            {
                seedMaps.LightToTemperatureMaps = ReturnIntList(dataRow);
            }
            if (dataRow.Contains("temperature-to-humidity map:"))
            {
                seedMaps.TemperatureToHumidityMaps = ReturnIntList(dataRow);
            }
            if (dataRow.Contains("humidity-to-location map:"))
            {
                seedMaps.HumidityToLocationMaps = ReturnIntList(dataRow);
            }
            return seedMaps;
        }

        private static List<SeedMapParts> ReturnIntList(string dataRow)
        {
            var seedMapPartsList = new List<SeedMapParts>();
            var stringSplits = dataRow.Split("\r\n").ToList();
            stringSplits.RemoveAt(0);
            foreach (var stringSplit in stringSplits)
            {
                var numberSplits = stringSplit.Split(" ").ToList();
                numberSplits = numberSplits.Where(x => !string.IsNullOrWhiteSpace(x)).ToList();
                seedMapPartsList.Add(new SeedMapParts
                {
                    SourceRangeStart = long.Parse(numberSplits[1]),
                    DestinationRangeStart = long.Parse(numberSplits[0]),
                    RangeLength = long.Parse(numberSplits[2]),
                });
            }
            return seedMapPartsList;
        }

        private static async Task<long> SeedToMinLocationMapTraversalAsync(SeedMaps seedMaps)
        {
            var minLocation = long.MaxValue;
            foreach (var seed in seedMaps.SeedList)
            {
                var nextMapNumber = seed;
                nextMapNumber = await GetNextMapNumberAsync(seedMaps.SeedToSoilMaps, nextMapNumber);
                nextMapNumber = await GetNextMapNumberAsync(seedMaps.SoilToFertilizerMaps, nextMapNumber);
                nextMapNumber = await GetNextMapNumberAsync(seedMaps.FertilizerToWaterMaps, nextMapNumber);
                nextMapNumber = await GetNextMapNumberAsync(seedMaps.WaterToLightMaps, nextMapNumber);
                nextMapNumber = await GetNextMapNumberAsync(seedMaps.LightToTemperatureMaps, nextMapNumber);
                nextMapNumber = await GetNextMapNumberAsync(seedMaps.TemperatureToHumidityMaps, nextMapNumber);
                nextMapNumber = await GetNextMapNumberAsync(seedMaps.HumidityToLocationMaps, nextMapNumber);
                minLocation = nextMapNumber < minLocation ? nextMapNumber : minLocation;
            }
            return minLocation;
        }

        private static Task<long> GetNextMapNumberAsync(List<SeedMapParts> seedTraversalMaps, long nextMapNumber)
        {
            //foreach (var seedTraversalMap in seedTraversalMaps)
            //{
            //    if (nextMapNumber >= seedTraversalMap.SourceRangeStart
            //        && nextMapNumber <= seedTraversalMap.SourceRangeStart + seedTraversalMap.RangeLength - 1)
            //    {
            //        return seedTraversalMap.DestinationRangeStart + nextMapNumber - seedTraversalMap.SourceRangeStart;
            //    }
            //}
            //return nextMapNumber;
            var seedTraversalMap = seedTraversalMaps.FirstOrDefault(x => nextMapNumber >= x.SourceRangeStart
                    && nextMapNumber <= x.SourceRangeStart + x.RangeLength - 1);
            if(seedTraversalMap == null)
            {
                return Task.FromResult(nextMapNumber);
            }
            else
            {
                return Task.FromResult(seedTraversalMap.DestinationRangeStart + nextMapNumber - seedTraversalMap.SourceRangeStart);
            }
        }

        #region private Day Six
        private List<int> ConvertDataStringToIntString(string dataString)
        {
            var dataParts = dataString.Split(":").ToList();
            var individualDataList = dataParts[1].Split(" ").ToList();
            individualDataList = individualDataList.Where(x => !string.IsNullOrWhiteSpace(x)).ToList();
            return individualDataList.Select(int.Parse).ToList();
        }
        #endregion
        #endregion
    }
}
