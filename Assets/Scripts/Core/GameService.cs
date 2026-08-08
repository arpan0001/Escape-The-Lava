using EscapeTheLava.Data;
using EscapeTheLava.Managers;
using EscapeTheLava.Utilities;

namespace EscapeTheLava.Core
{
    /// <summary>
    /// Coordinates the main game systems.
    /// </summary>
    public class GameService
    {
        private readonly GridManager _gridManager;

        private readonly WaveManager _waveManager;

        public GridData Grid =>
            _gridManager.Grid;

        public int CurrentWave =>
            _waveManager.CurrentWave;

        public int TotalWaves =>
            _waveManager.TotalWaves;

        public int DiamondsCollected =>
            _waveManager.DiamondsCollected;

        public GameService()
        {
            _gridManager = new GridManager();

            _waveManager = new WaveManager();
        }

        /// <summary>
        /// Starts a new game.
        /// </summary>
        public void Initialize()
        {
            _waveManager.Reset();

            _gridManager.Initialize();

            GenerateCurrentWave();
        }

        /// <summary>
        /// Generates the board for the current wave.
        /// </summary>
        private void GenerateCurrentWave()
        {
            _gridManager.GenerateWave(
                _waveManager.DiamondsInCurrentWave);
        }
    }
}