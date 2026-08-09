namespace EscapeTheLava.Managers
{
    
    /// Keeps track of the player's score.
    
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