using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace EscapeTheLava.UI
{
    
    /// Controls all information shown to the player.
   
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



        
        /// Updates the score displayed on screen.
        
        public void UpdateScore(int collected,int total)
        {
            scoreText.text = $" {collected}/{total}";
        }

        
        /// Updates the current wave.
        
        public void UpdateWave(int currentWave, int totalWaves)
        {
            waveText.text =$"WAVE {currentWave}/{totalWaves}";
        }

        /// Updates the number of visible hearts.
       
        public void UpdateLives( int remainingLives)
        {
            for (int i = 0;
                 i < lifeIcons.Length;
                 i++)
            {
                lifeIcons[i].gameObject.SetActive(
                    i < remainingLives);
            }
        }

        /// Updates the countdown timer.
        
        public void UpdateTimer( float remainingTime)
        {
            int seconds =Mathf.CeilToInt(remainingTime);

            timerText.text = seconds.ToString();
        }

      
        /// Shows the win screen.
       
        public void ShowWin()
        {
            winPanel.SetActive(true);
        }

     
        /// Shows the game over screen.
       
        public void ShowGameOver()
        {
            gameOverPanel.SetActive(true);
        }

        
        /// Hides both result panels.
        
        public void HideResultPanels()
        {
            winPanel.SetActive(false);

            gameOverPanel.SetActive(false);
        }
    }
}