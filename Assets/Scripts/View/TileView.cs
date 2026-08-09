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
    /// TileView handles visual feedback only.
    /// Gameplay logic is handled by GameManager/GameService.
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


        [Header("Lava Damage Effect")]

        [SerializeField]
        private BurnEffect burnEffect;

        [SerializeField]
        private float burnScale = 1.2f;

        [SerializeField]
        private float burnDuration = 0.15f;

        [SerializeField]
        private float burnJiggleAmount = 8f;

        [SerializeField]
        private float burnJiggleDuration = 0.2f;


        [Header("Diamond Collection Animation")]

        [SerializeField]
        private float collectScale = 1.35f;

        [SerializeField]
        private float collectDuration = 0.15f;


        // Logical grid position.
        private int _x;
        private int _y;


        // Current type of this tile.
        private TileType _currentType;


        // Prevents multiple clicks while
        // an animation is running.
        private bool _isCollecting;


        /// <summary>
        /// Event sent when this tile is clicked.
        /// Sends the tile's X and Y grid position.
        /// </summary>
        public event Action<int, int> TileClicked;


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
            // Remember the current tile type.
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
        /// Called automatically by Unity when
        /// the player clicks/taps this tile.
        /// </summary>
        public void OnPointerClick(
            PointerEventData eventData)
        {
            // Ignore clicks while an animation
            // is already running.
            if (_isCollecting)
                return;


            // ================================
            // DIAMOND
            // ================================

            if (_currentType == TileType.Diamond)
            {
                StartCoroutine(
                    CollectDiamond());

                return;
            }


            // ================================
            // LAVA
            // ================================

            if (_currentType == TileType.Lava)
            {
                StartCoroutine(
                    PlayLavaDamage());

                return;
            }


            // ================================
            // ISLAND / EMPTY
            // ================================

            TileClicked?.Invoke(
                _x,
                _y);
        }


        /// <summary>
        /// Plays the diamond collection animation.
        ///
        /// Diamond:
        /// 1. Scales up.
        /// 2. Disappears.
        /// 3. Shows +1 popup.
        /// 4. Sends click to game logic.
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


            // ================================
            // 1. SCALE DIAMOND UP
            // ================================

            while (elapsed < collectDuration)
            {
                elapsed += Time.deltaTime;


                float t =
                    elapsed / collectDuration;


                t = Mathf.SmoothStep(
                    0f,
                    1f,
                    t);


                diamond.localScale =
                    Vector3.Lerp(
                        startScale,
                        targetScale,
                        t);


                yield return null;
            }


            // Make sure final scale is correct.
            diamond.localScale =
                targetScale;


            // ================================
            // 2. DISABLE DIAMOND
            // ================================

            diamondView.gameObject.SetActive(false);


            // ================================
            // 3. SHOW +1 POPUP
            // ================================

            ShowScorePopup();


            // ================================
            // 4. ALLOW CLICK AGAIN
            // ================================

            _isCollecting = false;


            // ================================
            // 5. TELL GAME LOGIC
            // ================================

            TileClicked?.Invoke(
                _x,
                _y);
        }


        /// <summary>
        /// Displays the +1 popup exactly
        /// at the clicked tile's position.
        /// </summary>
        private void ShowScorePopup()
        {
            if (scorePopupPrefab == null)
                return;


            // Create the popup under the
            // same parent as the TileView.
            ScorePopup popup =
                Instantiate(
                    scorePopupPrefab,
                    transform.parent);


            RectTransform popupRect =
                popup.GetComponent<RectTransform>();


            RectTransform tileRect =
                GetComponent<RectTransform>();


            // Put popup at this tile's position.
            popupRect.anchoredPosition =
                tileRect.anchoredPosition;


            // Start popup animation.
            popup.Play(1);
        }


        /// <summary>
        /// Plays the lava damage effect.
        ///
        /// Lava:
        /// 1. Shows burn effect.
        /// 2. Burn effect pops up.
        /// 3. Burn effect jiggles.
        /// 4. Burn effect disappears.
        /// 5. Sends click to game logic.
        /// </summary>
        private IEnumerator PlayLavaDamage()
        {
            _isCollecting = true;


            // Play burn effect.
            if (burnEffect != null)
            {
                burnEffect.Play(
                    burnScale,
                    burnDuration,
                    burnJiggleAmount,
                    burnJiggleDuration);
            }


            // Wait until the visual effect
            // has finished.
            yield return new WaitForSeconds(
                burnDuration +
                burnJiggleDuration);


            _isCollecting = false;


            // Tell GameManager/GameService
            // that lava was clicked.
            TileClicked?.Invoke(
                _x,
                _y);
        }
    }
}