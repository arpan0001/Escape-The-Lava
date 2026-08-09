using System.Collections;
using TMPro;
using UnityEngine;

namespace EscapeTheLava.View
{
   
    /// Controls the +1 score popup animation.
    
    public class ScorePopup : MonoBehaviour
    {
        [Header("Animation")]

        [SerializeField]
        private float moveDistance = 40f;

        [SerializeField]
        private float duration = 0.6f;


        [Header("Text")]

        [SerializeField]
        private TextMeshProUGUI scoreText;


        private Vector2 _startPosition;


        
        /// Starts the popup animation.
        
        public void Play(int score)
        {
            scoreText.text = "+" + score;

            _startPosition = GetComponent<RectTransform>().anchoredPosition;

            StartCoroutine(AnimatePopup());
        }


        
        /// Moves the text upward and fades it out.
        
        private IEnumerator AnimatePopup()
        {
            RectTransform rect = GetComponent<RectTransform>();

            CanvasGroup canvasGroup =  GetComponent<CanvasGroup>();


            if (canvasGroup == null)
            {
                canvasGroup =  gameObject.AddComponent<CanvasGroup>();
            }


            float elapsed = 0f;


            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;

                float t =  elapsed / duration;


                // Move upward.
                rect.anchoredPosition =_startPosition + Vector2.up * (moveDistance * t);


                // Fade out.
                canvasGroup.alpha = 1f - t;


                yield return null;
            }


            Destroy(gameObject);
        }
    }
}