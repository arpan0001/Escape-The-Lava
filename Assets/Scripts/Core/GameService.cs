using EscapeTheLava.Data;
using EscapeTheLava.Managers;
using EscapeTheLava.Utilities;

namespace EscapeTheLava.Core
{
    /// <summary>
    /// Main gameplay coordinator.
    /// 
    /// It does not draw the board.
    /// It coordinates the gameplay systems.
    /// </summary>
    public class GameService
    {
        private readonly GridManager _gridManager;
        private readonly WaveManager _waveManager;
        private readonly LifeManager _lifeManager;
        private readonly ScoreManager _scoreManager;
        private readonly TimerManager _timerManager;

        public int TotalDiamonds => 25;
        public GridData Grid =>
            _gridManager.Grid;

        public int CurrentWave =>
            _waveManager.CurrentWave;

        public int TotalWaves =>
            _waveManager.TotalWaves;

        public int Score =>
            _scoreManager.Score;

        public int RemainingLives =>
            _lifeManager.RemainingLives;

        public int DiamondsCollected =>
            _waveManager.DiamondsCollected;

        public float RemainingTime =>
            _timerManager.RemainingTime;

        public GameState State
        {
            get;
            private set;
        }

        public GameService()
        {
            _gridManager =
                new GridManager();

            _waveManager =
                new WaveManager();

            _lifeManager =
                new LifeManager();

            _scoreManager =
                new ScoreManager();
            _timerManager =
                 new TimerManager();
        }

        /// <summary>
        /// Starts a completely new game.
        /// </summary>
        public void Initialize()
        {
            _waveManager.Reset();

            _lifeManager.Reset();

            _scoreManager.Reset();

            State = GameState.Playing;

            _gridManager.Initialize();

            GenerateCurrentWave();

            _timerManager.Reset();
        }

        /// <summary>
        /// Creates the board for the current wave.
        /// </summary>
        private void GenerateCurrentWave()
        {
            _gridManager.GenerateWave(
                _waveManager.DiamondsInCurrentWave);
        }

        /// <summary>
        /// Handles a player click on a grid cell.
        /// </summary>
        public bool ClickTile(int x, int y)
        {
            if (State != GameState.Playing)
                return false;

            TileData tile =
                Grid.GetTile(x, y);

            switch (tile.Type)
            {
                case TileType.Diamond:

                    CollectDiamond(x, y);

                    return true;

                case TileType.Lava:

                    HitLava();

                    return true;

                case TileType.Island:

                    return false;
            }

            return false;
        }

        /// <summary>
        /// Handles diamond collection.
        /// </summary>
        private void CollectDiamond(
                          int x,
                            int y)
        {
            _gridManager.SetTile(
                x,
                y,
                TileType.Island);

            _scoreManager.AddDiamondScore();

            _waveManager.CollectDiamond();

            if (_waveManager.AllDiamondsCollected)
            {
                State = GameState.Won;
                return;
            }

            if (_waveManager.CurrentWaveCompleted)
            {
                StartNextWave();
            }
        }

        /// <summary>
        /// Handles a player clicking lava.
        /// </summary>
        private void HitLava()
        {
            _lifeManager.LoseLife();

            if (_lifeManager.IsDead)
            {
                State = GameState.GameOver;
            }
        }

        /// <summary>
        /// Creates the next board.
        /// </summary>
        private void StartNextWave()
        {
            bool started =
                _waveManager.StartNextWave();

            if (!started)
                return;

            GenerateCurrentWave();
        }
        public void Update(float deltaTime)
        {
            if (State != GameState.Playing)
                return;

            _timerManager.Update(deltaTime);

            if (_timerManager.IsTimeUp)
            {
                State = GameState.GameOver;

                GameEvents.GameOver?.Invoke();
            }
        }

    }
}