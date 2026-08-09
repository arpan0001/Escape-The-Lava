using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace EscapeTheLava.UI
{
    /// <summary>
    /// Controls all information shown to the player.
    ///
    /// UIManager does not contain gameplay logic.
    /// It only displays values given to it.
    /// </summary>
    public class UIManager : MonoBehaviour
    {
        [Header("HUD")]
        [SerializeField]
        private TMP_Text scoreText;

        [SerializeField]
        private TMP_Text waveText;

        [SerializeField]
        private TMP_Text timerText;

        [SerializeField]
        private Transform livesContainer;

        [SerializeField]
        private Image[] lifeIcons;

        [Header("Panels")]
        [SerializeField]
        private GameObject winPanel;

        [SerializeField]
        private GameObject gameOverPanel;



        /// <summary>
        /// Updates the score displayed on screen.
        /// </summary>
        public void UpdateScore(
            int collected,
            int total)
        {
            scoreText.text =
                $" {collected}/{total}";
        }
        public void HideWin()
        {
            winPanel.SetActive(false);
        }
        /// <summary>
        /// Updates the current wave.
        /// </summary>
        public void UpdateWave(
            int currentWave,
            int totalWaves)
        {
            waveText.text =
                $"WAVE {currentWave}/{totalWaves}";
        }

        /// <summary>
        /// Updates the number of visible hearts.
        /// </summary>
        public void UpdateLives(
            int remainingLives)
        {
            for (int i = 0;
                 i < lifeIcons.Length;
                 i++)
            {
                lifeIcons[i].gameObject.SetActive(
                    i < remainingLives);
            }
        }

        /// <summary>
        /// Updates the countdown timer.
        /// </summary>
        public void UpdateTimer(
            float remainingTime)
        {
            int seconds =
                Mathf.CeilToInt(
                    remainingTime);

            timerText.text =
                seconds.ToString();
        }

        /// <summary>
        /// Shows the win screen.
        /// </summary>
        public void ShowWin()
        {
            winPanel.SetActive(true);
        }

        /// <summary>
        /// Shows the game over screen.
        /// </summary>
        public void ShowGameOver()
        {
            gameOverPanel.SetActive(true);
        }

        /// <summary>
        /// Hides both result panels.
        /// </summary>
        public void HideResultPanels()
        {
            winPanel.SetActive(false);

            gameOverPanel.SetActive(false);
        }
    }
}