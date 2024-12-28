<<<<<<< HEAD
namespace Lyzer.Common.DTO
{
    public class ResultsDTO
    {
        public required string Season { get; set; }
        public required string Round { get; set; }
        public required string Url { get; set; }

        public required string RaceName { get; set; }
        public required CircuitDTO Circuit { get; set; }
        public required string Date { get; set; }
        public required string Time { get; set; }
        public required List<ResultDTO> Results { get; set; }
    }

    public class ResultDTO
    {
        public required string Number { get; set; }
        public required string Position { get; set; }
        public required string PositionText { get; set; }
        public required string Points { get; set; }
        public required DriverDTO Driver { get; set; }
        public ConstructorDTO? Constructor { get; set; }
        public string? Grid { get; set; }
        public string? Laps { get; set; }
        public string? Status { get; set; }
        public ResultTimeDTO? Time { get; set; }
        public ResultFastestLapDTO? FastestLap { get; set; }
    }

    public class ResultTimeDTO
    {
        public string? Millis { get; set; }
        public required string Time { get; set; }
    }

    public class ResultFastestLapDTO
    {
        public required string rank { get; set; }
        public required string lap { get; set; }
        public required ResultTimeDTO Time { get; set; }
        public required ResultAverageSpeedDTO AverageSpeed { get; set; }
    }

    public class ResultAverageSpeedDTO
    {
        public required string Units { get; set; }
        public required string Speed { get; set; }
    }
=======
﻿namespace Lyzer.Common.DTO
{
    public class ResultsDTO
    {
        public required string Season { get; set; }
        public required string Round { get; set; }
        public required string Url { get; set; }

        public required string RaceName { get; set; }
        public required CircuitDTO Circuit { get; set; }
        public required string Date { get; set; }
        public required string Time { get; set; }
        public required List<ResultDTO> Results { get; set; }
    }
    public class ResultDTO
    {
        public required string Number { get; set; }
        public required string Position { get; set; }
        public required string PositionText { get; set; }
        public required string Points { get; set; }
        public required DriverDTO Driver { get; set; }
        public required ConstructorDTO Constructor { get; set; }
        public required string Grid { get; set; }
        public required string Laps { get; set; }
        public required string Status { get; set; }
        public required ResultTimeDTO Time { get; set; }
        public required ResultFastestLapDTO FastestLap { get; set; }
    }
    public class ResultTimeDTO
    {
        public string? Millis { get; set; }
        public required string Time { get; set; }
    }
    public class ResultFastestLapDTO
    {
        public required string rank { get; set; }
        public required string lap { get; set; }
        public required ResultTimeDTO Time { get; set; }
        public required ResultAverageSpeedDTO AverageSpeed { get; set; }
    }
    public class ResultAverageSpeedDTO
    {
        public required string Units { get; set; }
        public required string Speed { get; set; }
    }
>>>>>>> 3df3072b4e162e52ef0823467df0939ff4a72985
}