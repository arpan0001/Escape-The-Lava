using EscapeTheLava.Utilities;

namespace EscapeTheLava.Managers
{
    /// <summary>
    /// Keeps track of the player's remaining lives.
    /// </summary>
    public class LifeManager
    {
        public int RemainingLives { get; private set; }

        public bool IsDead
        {
            get
            {
                return RemainingLives <= 0;
            }
        }

        public void Reset()
        {
            RemainingLives =
                GameConstants.StartingLives;
        }

        /// <summary>
        /// Removes one life.
        /// </summary>
        public void LoseLife()
        {
            if (RemainingLives <= 0)
                return;

            RemainingLives--;
        }
    }
}