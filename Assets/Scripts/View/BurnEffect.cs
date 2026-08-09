using System.Collections;
using UnityEngine;

namespace EscapeTheLava.View
{
    public class BurnEffect : MonoBehaviour
    {
        private RectTransform _rectTransform;

        private Vector2 _originalPosition;


        private void Awake()
        {
            _rectTransform =
                GetComponent<RectTransform>();


            _originalPosition =
                _rectTransform.anchoredPosition;


            // Hidden when the game starts.
            gameObject.SetActive(false);
        }


        public void Play(
            float targetScale,
            float scaleDuration,
            float jiggleAmount,
            float jiggleDuration)
        {
            StopAllCoroutines();


            // Show effect.
            gameObject.SetActive(true);


            // Reset position.
            _rectTransform.anchoredPosition =
                _originalPosition;


            // Start from zero scale.
            _rectTransform.localScale =
                Vector3.zero;


            StartCoroutine(
                PlayAnimation(
                    targetScale,
                    scaleDuration,
                    jiggleAmount,
                    jiggleDuration));
        }


        private IEnumerator PlayAnimation(
            float targetScale,
            float scaleDuration,
            float jiggleAmount,
            float jiggleDuration)
        {
            // ================================
            // POP UP
            // ================================

            float elapsed = 0f;


            while (elapsed < scaleDuration)
            {
                elapsed += Time.deltaTime;


                float t =
                    elapsed / scaleDuration;


                t = Mathf.SmoothStep(
                    0f,
                    1f,
                    t);


                _rectTransform.localScale =
                    Vector3.Lerp(
                        Vector3.zero,
                        Vector3.one * targetScale,
                        t);


                yield return null;
            }


            _rectTransform.localScale =
                Vector3.one * targetScale;


            // ================================
            // JIGGLE
            // ================================

            elapsed = 0f;


            while (elapsed < jiggleDuration)
            {
                elapsed += Time.deltaTime;


                float t =
                    elapsed / jiggleDuration;


                float strength =
                    1f - t;


                float x =
                    Random.Range(
                        -jiggleAmount,
                        jiggleAmount) *
                    strength;


                float y =
                    Random.Range(
                        -jiggleAmount,
                        jiggleAmount) *
                    strength;


                _rectTransform.anchoredPosition =
                    _originalPosition +
                    new Vector2(x, y);


                yield return null;
            }


            // Return to original position.
            _rectTransform.anchoredPosition =
                _originalPosition;


            // Reset scale.
            _rectTransform.localScale =
                Vector3.one;


            // Hide effect.
            gameObject.SetActive(false);
        }
    }
}