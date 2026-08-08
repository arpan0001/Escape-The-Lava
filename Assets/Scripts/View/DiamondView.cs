using UnityEngine;

namespace EscapeTheLava.View
{
    /// <summary>
    /// Controls the idle visual animation of a diamond.
    ///
    /// This script only handles visual effects.
    /// It does not handle score or gameplay logic.
    /// </summary>
    public class DiamondView : MonoBehaviour
    {
        [Header("Floating")]
        [SerializeField]
        private float floatHeight = 5f;

        [SerializeField]
        private float floatSpeed = 2f;


        [Header("Tilt")]
        [SerializeField]
        private float tiltAngle = 8f;

        [SerializeField]
        private float tiltSpeed = 2f;


        [Header("Scale Pulse")]
        [SerializeField]
        private float pulseAmount = 0.03f;

        [SerializeField]
        private float pulseSpeed = 3f;


        [Header("Shine")]
        [SerializeField]
        private CanvasGroup shine;

        [SerializeField]
        private float shineSpeed = 2f;

        [SerializeField]
        private float shineMinAlpha = 0.1f;

        [SerializeField]
        private float shineMaxAlpha = 0.8f;


        // Original position of the diamond.
        private Vector3 _startPosition;

        // Original scale of the diamond.
        private Vector3 _startScale;

        // Original rotation of the diamond.
        private Quaternion _startRotation;

        // Gives every diamond a different animation timing.
        private float _randomOffset;


        private void Awake()
        {
            _startPosition = transform.localPosition;
            _startScale = transform.localScale;
            _startRotation = transform.localRotation;

            // Give each diamond a different animation starting point.
            _randomOffset = Random.Range(0f, 10f);
        }


        private void Update()
        {
            AnimateFloat();
            AnimateTilt();
            AnimatePulse();
            AnimateShine();
        }


        /// <summary>
        /// Makes the diamond smoothly move up and down.
        /// </summary>
        private void AnimateFloat()
        {
            float time =
                (Time.time + _randomOffset) *
                floatSpeed;

            float offset =
                Mathf.Sin(time) *
                floatHeight;

            transform.localPosition =
                _startPosition +
                Vector3.up * offset;
        }


        /// <summary>
        /// Makes the diamond gently tilt
        /// from left to right.
        /// </summary>
        private void AnimateTilt()
        {
            float time =
                (Time.time + _randomOffset) *
                tiltSpeed;

            float angle =
                Mathf.Sin(time) *
                tiltAngle;

            transform.localRotation =
                _startRotation *
                Quaternion.Euler(0f, 0f, angle);
        }


        /// <summary>
        /// Makes the diamond slightly grow and shrink.
        /// </summary>
        private void AnimatePulse()
        {
            float time =
                (Time.time + _randomOffset) *
                pulseSpeed;

            float scale =
                1f +
                Mathf.Sin(time) *
                pulseAmount;

            transform.localScale =
                _startScale * scale;
        }


        /// <summary>
        /// Creates a soft pulsing shine.
        /// </summary>
        private void AnimateShine()
        {
            if (shine == null)
                return;

            float time =
                (Time.time + _randomOffset) *
                shineSpeed;

            float value =
                (Mathf.Sin(time) + 1f) * 0.5f;

            shine.alpha =
                Mathf.Lerp(
                    shineMinAlpha,
                    shineMaxAlpha,
                    value);
        }
    }
}