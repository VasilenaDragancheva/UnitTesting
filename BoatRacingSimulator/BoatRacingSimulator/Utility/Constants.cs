namespace BoatRacingSimulator.Utility
{
    public static class Constants
    {
        public const string DuplicateModelMessage = "An entry for the given model already exists.";

        public const string IncorrectBoatTypeMessage = "The specified boat does not meet the race constraints.";

        public const string IncorrectEngineTypeMessage = "Incorrect engine type.";

        public const string IncorrectModelLenghtMessage = "Model's name must be at least {0} symbols long.";

        public const string IncorrectPropertyValueMessage = "{0} must be a positive integer.";

        public const string IncorrectSailEfficiencyMessage = "Sail Effectiveness must be between [1...100].";

        public const string InsufficientContestantsMessage = "Not enough contestants for the race.";

        public const int MinBoatEngineModelLength = 3;

        public const int MinBoatModelLength = 5;

        public const string NonExistantModelMessage = "The specified model does not exist.";

        public const string NoSetRaceMessage = "There is currently no race set.";

        public const string RaceAlreadyExistsMessage = "The current race has already been set.";
    }
}