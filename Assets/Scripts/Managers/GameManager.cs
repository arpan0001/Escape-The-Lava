using UnityEngine;
using EscapeTheLava.Core;
using EscapeTheLava.View;
using EscapeTheLava.UI;

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

        private GameService _gameService;
        [SerializeField]
        private UIManager uiManager;

        private void Awake()
        {
            _gameService =
                new GameService();

            _gameService.Initialize();

            gridRenderer.Initialize(
                _gameService.Grid);

            UpdateHUD();
        }

        private void Update()
        {
            _gameService.Update(
            Time.deltaTime);

            uiManager.UpdateTimer(
                _gameService.RemainingTime);
        }


        private void OnEnable()
        {
            if (gridRenderer != null)
            {
                gridRenderer.TileClicked += OnTileClicked;
            }
        }

        private void OnDisable()
        {
            if (gridRenderer != null)
            {
                gridRenderer.TileClicked -= OnTileClicked;
            }
        }

        private void OnTileClicked(int x, int y)
        {
            bool handled =
                _gameService.ClickTile(x, y);

            if (!handled)
                return;

            gridRenderer.Render(
                _gameService.Grid);

            UpdateUI();
        }

        private void UpdateUI()
        {
        }

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
    }
}