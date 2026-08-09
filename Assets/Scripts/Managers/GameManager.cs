using UnityEngine;
using EscapeTheLava.Core;
using EscapeTheLava.View;
using EscapeTheLava.UI;
using EscapeTheLava.Utilities;
using UnityEngine.SceneManagement;

namespace EscapeTheLava.Managers
{
    
    /// Unity entry point for the game.
    /// Connects Unity scene objects with GameService.
    
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
           
            _gameService = new GameService();

            
            _gameService.Initialize();

            gridRenderer.Initialize( _gameService.Grid);

           
            UpdateHUD();
        }

        private void Update()
        {
           
            _gameService.Update(
                Time.deltaTime);

           
            uiManager.UpdateTimer( _gameService.RemainingTime);

            
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
            bool handled =_gameService.ClickTile(x, y);

            // Nothing happened.
            if (!handled)
                return;

            // Update the board visuals.
            gridRenderer.Render( _gameService.Grid);

            // Update score, lives and wave.
            UpdateHUD();

            // Check for win or game over.
            CheckGameState();
        }

      
        /// Updates all gameplay information shown on screen.
        
        private void UpdateHUD()
        {
            uiManager.UpdateScore( _gameService.DiamondsCollected, _gameService.TotalDiamonds);

            uiManager.UpdateLives(_gameService.RemainingLives);

            uiManager.UpdateWave(_gameService.CurrentWave, _gameService.TotalWaves);

            uiManager.UpdateTimer( _gameService.RemainingTime);
        }

        
        /// Checks whether the player has won or lost.
        
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
            SceneManager.LoadScene( SceneManager.GetActiveScene().buildIndex);
        }
    }
}