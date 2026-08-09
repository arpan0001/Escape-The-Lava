using EscapeTheLava.Utilities;

namespace EscapeTheLava.Managers
{
    
    /// Keeps track of the player's remaining lives.
  
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

      
        /// Removes one life.
       
        public void LoseLife()
        {
            if (RemainingLives <= 0)
                return;

            RemainingLives--;
        }
    }
}