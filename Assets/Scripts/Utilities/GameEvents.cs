using System;

namespace EscapeTheLava.Utilities
{
    /// <summary>
    /// Central events used to notify the UI and effects
    /// about important gameplay actions.
    /// </summary>
    public static class GameEvents
    {
        public static Action<int, int> DiamondCollected;

        public static Action<int, int> LavaHit;

        public static Action<int> ScoreChanged;

        public static Action<int> LivesChanged;

        public static Action<int> WaveStarted;

        public static Action GameWon;

        public static Action GameOver;
    }
}