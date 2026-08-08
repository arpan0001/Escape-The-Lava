using UnityEngine;

namespace EscapeTheLava.View
{
    /// <summary>
    /// Controls the idle visual animation of a lava tile.
    /// </summary>
    public class LavaView : MonoBehaviour
    {
        [SerializeField]
        private float pulseSpeed = 3f;

        [SerializeField]
        private float pulseAmount = 0.04f;

        private Vector3 _baseScale;
        private float _animationOffset;

        private void Start()
        {
            _baseScale =
                transform.localScale;
            _animationOffset =
                  Random.Range(0f, 10f);

        }

        private void Update()
        {
            float scale =
                1f +
               Mathf.Sin(
               (Time.time + _animationOffset) *
                  pulseSpeed) *
                  pulseAmount;
        }
    }
}