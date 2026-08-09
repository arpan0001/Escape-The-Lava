using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

using EscapeTheLava.Data;

namespace EscapeTheLava.View
{
    /// <summary>
    /// Controls the visual appearance of one grid tile
    /// and detects player clicks.
    ///
    /// This class handles visual effects only.
    /// Gameplay logic is handled elsewhere.
    /// </summary>
    public class TileView : MonoBehaviour, IPointerClickHandler
    {
        [Header("Tile Visuals")]

        [SerializeField]
        private Image islandView;

        [SerializeField]
        private Image diamondView;

        [SerializeField]
        private Image lavaView;
        [Header("Score Popup")]

        [SerializeField]
        private ScorePopup scorePopupPrefab;

        [Header("Diamond Collection Animation")]

        [SerializeField]
        private float collectScale = 1.35f;

        [SerializeField]
        private float collectDuration = 0.15f;


        private int _x;
        private int _y;

        private TileType _currentType;

        private bool _isCollecting;


        /// <summary>
        /// Gives this tile its grid position.
        /// </summary>
        public void SetPosition(int x, int y)
        {
            _x = x;
            _y = y;
        }


        /// <summary>
        /// Displays the correct visual for this tile.
        /// </summary>
        public void SetTile(TileType type)
        {
            _currentType = type;

            // Hide everything first.
            islandView.gameObject.SetActive(false);
            diamondView.gameObject.SetActive(false);
            lavaView.gameObject.SetActive(false);


            switch (type)
            {
                case TileType.Empty:

                    // Nothing is displayed.
                    break;


                case TileType.Island:

                    islandView.gameObject.SetActive(true);
                    break;


                case TileType.Diamond:

                    diamondView.gameObject.SetActive(true);

                    // Reset diamond scale.
                    diamondView.transform.localScale =
                        Vector3.one;

                    break;


                case TileType.Lava:

                    lavaView.gameObject.SetActive(true);
                    break;
            }
        }


        /// <summary>
        /// Called by Unity when the player
        /// clicks or taps this tile.
        /// </summary>
        public void OnPointerClick(
            PointerEventData eventData)
        {
            // Prevent multiple clicks while
            // the collection animation is running.
            if (_isCollecting)
                return;


            // Only diamonds have the
            // special collection animation.
            if (_currentType == TileType.Diamond)
            {
                StartCoroutine(CollectDiamond());
                return;
            }


            // For lava/island, send the click immediately.
            TileClicked?.Invoke(_x, _y);
        }


        /// <summary>
        /// Plays the diamond collection animation.
        /// The diamond first grows and then disappears.
        /// </summary>
        private IEnumerator CollectDiamond()
        {
            _isCollecting = true;


            Transform diamond =
                diamondView.transform;


            Vector3 startScale =
                diamond.localScale;


            Vector3 targetScale =
                startScale * collectScale;


            float elapsed = 0f;


            // -------------------------------
            // 1. Scale the diamond up
            // -------------------------------

            while (elapsed < collectDuration)
            {
                elapsed += Time.deltaTime;


                float t =
                    elapsed / collectDuration;


                diamond.localScale =
                    Vector3.Lerp(
                        startScale,
                        targetScale,
                        t);


                yield return null;
            }


            diamond.localScale = targetScale;


            // -------------------------------
            // 2. Disable the diamond
            // -------------------------------

            diamondView.gameObject.SetActive(false);


            // -------------------------------
            // 3. Show +1 popup
            // -------------------------------

            ShowScorePopup();


            _isCollecting = false;


            // -------------------------------
            // 4. Tell the game logic
            //    that the diamond was collected
            // -------------------------------

            TileClicked?.Invoke(_x, _y);
        }

        private void ShowScorePopup()
        {
            if (scorePopupPrefab == null)
                return;


            // Create the popup under the same parent
            // as the TileView.
            ScorePopup popup =
                Instantiate(
                    scorePopupPrefab,
                    transform.parent);


            RectTransform popupRect =
                popup.GetComponent<RectTransform>();


            RectTransform tileRect =
                GetComponent<RectTransform>();


            // Put the popup exactly where
            // this tile is located.
            popupRect.anchoredPosition =
                tileRect.anchoredPosition;


            // Start the animation.
            popup.Play(1);
        }


        /// <summary>
        /// Sends the clicked tile position
        /// to GridRenderer/GameManager.
        /// </summary>
        public event Action<int, int> TileClicked;
    }
}