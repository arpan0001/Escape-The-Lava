namespace EscapeTheLava.Managers
{
    /// <summary>
    /// Keeps track of the player's score.
    /// </summary>
    public class ScoreManager
    {
        public int Score { get; private set; }

        public void Reset()
        {
            Score = 0;
        }

        public void AddDiamondScore()
        {
            Score++;
        }
    }
}