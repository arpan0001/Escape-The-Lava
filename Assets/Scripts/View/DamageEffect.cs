using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace EscapeTheLava.View
{
    /// <summary>
    /// Controls the visual damage effect
    /// shown when the player clicks lava.
    ///
    /// This class contains visual logic only.
    /// It does not reduce player lives.
    /// </summary>
    public class DamageEffect : MonoBehaviour
    {
        [Header("Animation")]

        [SerializeField]
        private float duration = 0.4f;

        [SerializeField]
        private float startScale = 0.5f;

        [SerializeField]
        private float endScale = 1.4f;


        private Image _image;
        private Color _startColor;


        private void Awake()
        {
            _image = GetComponent<Image>();

            _startColor = _image.color;
        }


        public void Play()
        {
            StartCoroutine(PlayAnimation());
        }


        private IEnumerator PlayAnimation()
        {
            RectTransform rect =
                GetComponent<RectTransform>();


            float elapsed = 0f;


            rect.localScale =
                Vector3.one * startScale;


            Color color =
                _startColor;

            color.a = 1f;

            _image.color = color;


            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;


                float t =
                    elapsed / duration;


                // Expand the effect.
                float scale =
                    Mathf.Lerp(
                        startScale,
                        endScale,
                        t);


                rect.localScale =
                    Vector3.one * scale;


                // Fade out.
                color.a =
                    1f - t;


                _image.color =
                    color;


                yield return null;
            }


            Destroy(gameObject);
        }
    }
}