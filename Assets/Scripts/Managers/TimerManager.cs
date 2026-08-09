using EscapeTheLava.Utilities;

namespace EscapeTheLava.Managers
{
    
    /// Controls the round countdown.
    
    public class TimerManager
    {
        public float RemainingTime { get; private set; }

        public bool IsTimeUp =>
            RemainingTime <= 0f;

        public void Reset()
        {
            RemainingTime =
                GameConstants.RoundDuration;
        }

        
        /// Reduces the remaining time.
        
        public void Update(float deltaTime)
        {
            if (RemainingTime <= 0f)
                return;

            RemainingTime -= deltaTime;

            if (RemainingTime < 0f)
                RemainingTime = 0f;
        }
    }
}