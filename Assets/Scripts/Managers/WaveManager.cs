using EscapeTheLava.Utilities;

namespace EscapeTheLava.Managers
{
    
    /// Controls the diamond wave progression.
    
    public class WaveManager
    {
        public int TotalDiamonds { get; }

        public int DiamondsPerWave { get; }

        public int CurrentWave { get; private set; }

        public int TotalWaves { get; }

        public int DiamondsCollected { get; private set; }

        public int DiamondsCollectedThisWave { get; private set; }

        public bool IsLastWave
        {
            get
            {
                return CurrentWave >= TotalWaves;
            }
        }

        public bool CurrentWaveCompleted
        {
            get
            {
                return DiamondsCollectedThisWave >= DiamondsPerWave;
            }
        }

        public bool AllDiamondsCollected
        {
            get
            {
                return DiamondsCollected >= TotalDiamonds;
            }
        }

        public WaveManager()
        {
            TotalDiamonds = GameConstants.TotalDiamonds;

            DiamondsPerWave = GameConstants.DiamondsPerWave;

            TotalWaves =(TotalDiamonds + DiamondsPerWave - 1) / DiamondsPerWave;

            Reset();
        }

        
        /// Starts the first wave.
        
        public void Reset()
        {
            CurrentWave = 1;

            DiamondsCollected = 0;

            DiamondsCollectedThisWave = 0;
        }

       
        /// Called whenever the player collects a diamond.
        
        public void CollectDiamond()
        {
            DiamondsCollected++;

            DiamondsCollectedThisWave++;
        }

        /// Starts the next wave.
       
        public bool StartNextWave()
        {
            if (AllDiamondsCollected)
                return false;

            CurrentWave++;

            DiamondsCollectedThisWave = 0;

            return true;
        }

        
        /// Returns how many diamonds are still required.
        
        public int RemainingDiamonds
        {
            get
            {
                return TotalDiamonds - DiamondsCollected;
            }
        }

        public int DiamondsInCurrentWave
        {
            get
            {
                int remaining =
                    TotalDiamonds - DiamondsCollected;

                return remaining < DiamondsPerWave
                    ? remaining
                    : DiamondsPerWave;
            }
        }
    }
}