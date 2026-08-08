namespace EscapeTheLava.Utilities
{
    /// <summary>
    /// Stores the fixed values used by the game.
    /// Keeping them here makes the game easier to balance.
    /// </summary>
    public static class GameConstants
    {
        public const int GridColumns = 16;

        public const int GridRows = 8;

        public const float RoundDuration = 30f;

        public const int StartingLives = 5;

        // Total number of diamonds required to win.
        public const int TotalDiamonds = 25;

        // Number of diamonds visible in one wave.
        public const int DiamondsPerWave = 5;

        // Number of lava tiles on the board.
        public const int LavaCount = 55;
    }
}