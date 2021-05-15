namespace Periotris.Common
{
    public static class TetrisConst
    {
        public const int PlayAreaWidth = 18;

        public const int PlayAreaHeight = 11;

        public const double OriginalGameUpdateIntervalSeconds = 1.5;

        public const double TimeDisplayUpdateIntervalSeconds = 0.1;

        public const double TimeDecreaseDeltaSeconds = 0.025;

        /// <summary>
        ///     Current update interval for the game timer.
        /// </summary>
        public static double GameUpdateIntervalSeconds = OriginalGameUpdateIntervalSeconds;

        public const string PeriodicTableJsonFileName = "pack://application:,,,/Assets/PeriodicTable.json";
    }
}