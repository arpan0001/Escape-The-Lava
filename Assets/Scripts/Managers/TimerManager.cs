using EscapeTheLava.Utilities;

namespace EscapeTheLava.Managers
{
    /// <summary>
    /// Controls the round countdown.
    /// </summary>
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

        /// <summary>
        /// Reduces the remaining time.
        /// </summary>
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