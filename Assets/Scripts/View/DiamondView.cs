using UnityEngine;

namespace EscapeTheLava.View
{
    /// <summary>
    /// Controls the idle animation of a diamond.
    /// This script only handles visual animation.
    /// It does not contain gameplay logic.
    /// </summary>
    public class DiamondView : MonoBehaviour
    {
        [Header("Floating")]
        [SerializeField]
        private float floatHeight = 8f;

        [SerializeField]
        private float floatSpeed = 2f;

        [Header("Rotation")]
        [SerializeField]
        private float rotationSpeed = 30f;

        [Header("Scale Pulse")]
        [SerializeField]
        private float pulseAmount = 0.04f;

        [SerializeField]
        private float pulseSpeed = 3f;

        // Original position of the diamond.
        private Vector3 _startPosition;

        // Original scale of the diamond.
        private Vector3 _startScale;

        private void Start()
        {
            // Remember the position where the diamond was created.
            _startPosition = transform.localPosition;

            // Remember the original scale.
            _startScale = transform.localScale;
        }

        private void Update()
        {
            AnimateFloat();
            AnimateRotation();
            AnimatePulse();
        }

        /// <summary>
        /// Makes the diamond smoothly move up and down.
        /// </summary>
        private void AnimateFloat()
        {
            float offset =
                Mathf.Sin(Time.time * floatSpeed) *
                floatHeight;

            transform.localPosition =
                _startPosition +
                Vector3.up * offset;
        }

        /// <summary>
        /// Slowly rotates the diamond around its Y axis.
        /// </summary>
        private void AnimateRotation()
        {
            transform.Rotate(
                Vector3.up,
                rotationSpeed * Time.deltaTime,
                Space.Self);
        }

        /// <summary>
        /// Makes the diamond slightly grow and shrink.
        /// </summary>
        private void AnimatePulse()
        {
            float scale =
                1f +
                Mathf.Sin(Time.time * pulseSpeed) *
                pulseAmount;

            transform.localScale =
                _startScale * scale;
        }
    }
}