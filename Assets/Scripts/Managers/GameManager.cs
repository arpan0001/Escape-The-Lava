using UnityEngine;
using EscapeTheLava.Core;
using EscapeTheLava.View;

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

        private void Awake()
        {
            // Create the backend game system.
            _gameService =
                new GameService();

            // Start the first wave.
            _gameService.Initialize();

            // Display the generated board.
            gridRenderer.Initialize(
                _gameService.Grid);
        }
    }
}