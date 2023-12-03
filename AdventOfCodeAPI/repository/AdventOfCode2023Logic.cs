using AdventOfCodeAPI.Models;
using System.Text.RegularExpressions;

namespace AdventOfCodeAPI.repository
{
    public class AdventOfCode2023Logic
    {
        public int OneALogic(List<string> calibrationTexts)
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

        public int OneBLogic(List<string> calibrationTexts)
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

        public int TwoALogic(List<string> gameTexts, int blueThreshold, int greenThreshold, int redThreshold)
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

        public int TwoBLogic(List<string> gameTexts)
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

        public int ThreeALogic(List<string> partTexts)
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

        public int ThreeBLogic(List<string> partTexts)
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
        #endregion
    }
}
