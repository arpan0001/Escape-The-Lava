using UnityEngine;
using EscapeTheLava.Core;
using EscapeTheLava.View;
using EscapeTheLava.UI;
using EscapeTheLava.Utilities;
using UnityEngine.SceneManagement;

namespace EscapeTheLava.Managers
{
    /// <summary>
    /// Unity entry point for the game.
    /// Connects Unity scene objects with GameService.
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        [Header("References")]
        [SerializeField]
        private GridRenderer gridRenderer;

        [SerializeField]
        private UIManager uiManager;

        private GameService _gameService;

        private void Awake()
        {
            // Create the backend game service.
            _gameService = new GameService();

            // Start a fresh game.
            _gameService.Initialize();

            // Create and display the board.
            gridRenderer.Initialize(
                _gameService.Grid);

            // Show the starting UI values.
            UpdateHUD();
        }

        private void Update()
        {
            // Update the game timer.
            _gameService.Update(
                Time.deltaTime);

            // Update the timer shown on screen.
            uiManager.UpdateTimer(
                _gameService.RemainingTime);

            // Check if the game has ended.
            CheckGameState();
        }

        private void OnEnable()
        {
            if (gridRenderer != null)
            {
                // Listen for tile clicks.
                gridRenderer.TileClicked += OnTileClicked;
            }
        }

        private void OnDisable()
        {
            if (gridRenderer != null)
            {
                // Stop listening for tile clicks.
                gridRenderer.TileClicked -= OnTileClicked;
            }
        }

        /// <summary>
        /// Called when the player clicks a tile.
        /// </summary>
        private void OnTileClicked(int x, int y)
        {
            // Ask GameService to process the click.
            bool handled =
                _gameService.ClickTile(x, y);

            // Nothing happened.
            if (!handled)
                return;

            // Update the board visuals.
            gridRenderer.Render(
                _gameService.Grid);

            // Update score, lives and wave.
            UpdateHUD();

            // Check for win or game over.
            CheckGameState();
        }

        /// <summary>
        /// Updates all gameplay information shown on screen.
        /// </summary>
        private void UpdateHUD()
        {
            uiManager.UpdateScore(
                _gameService.DiamondsCollected,
                _gameService.TotalDiamonds);

            uiManager.UpdateLives(
                _gameService.RemainingLives);

            uiManager.UpdateWave(
                _gameService.CurrentWave,
                _gameService.TotalWaves);

            uiManager.UpdateTimer(
                _gameService.RemainingTime);
        }

        /// <summary>
        /// Checks whether the player has won or lost.
        /// </summary>
        private void CheckGameState()
        {
            if (_gameService.State == GameState.Won)
            {
                uiManager.ShowWin();
            }
            else if (_gameService.State == GameState.GameOver)
            {
                uiManager.ShowGameOver();
            }
        }

        public void RestartScene()
        {
            SceneManager.LoadScene(
                SceneManager.GetActiveScene().buildIndex);
        }
    }
}