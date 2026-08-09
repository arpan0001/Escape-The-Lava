using UnityEngine;
using UnityEngine.UI;

namespace EscapeTheLava.View
{
    public class LavaView : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Image lavaImage;

        [Header("Animation Frames")]
        [SerializeField] private Sprite[] lavaFrames;

        [Header("Animation Settings")]
        [SerializeField] private float framesPerSecond = 12f;

        private int _currentFrame;
        private int _direction = 1;
        private float _timer;

        private void Awake()
        {
            _currentFrame = 0;
            _direction = 1;
            _timer = 0f;

            if (lavaFrames != null && lavaFrames.Length > 0)
            {
                lavaImage.sprite = lavaFrames[0];
            }
        }

        private void Update()
        {
            if (lavaFrames == null || lavaFrames.Length <= 1)
                return;

            _timer += Time.deltaTime;

            float frameDuration = 1f / framesPerSecond;

            if (_timer >= frameDuration)
            {
                _timer -= frameDuration;

                PlayNextFrame();
            }
        }

        private void PlayNextFrame()
        {
            _currentFrame += _direction;

            // Reached the last frame.
            if (_currentFrame >= lavaFrames.Length - 1)
            {
                _currentFrame = lavaFrames.Length - 1;
                _direction = -1;
            }

            // Reached the first frame.
            else if (_currentFrame <= 0)
            {
                _currentFrame = 0;
                _direction = 1;
            }

            lavaImage.sprite = lavaFrames[_currentFrame];
        }
    }
}