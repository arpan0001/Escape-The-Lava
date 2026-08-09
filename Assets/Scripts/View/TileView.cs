using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

using EscapeTheLava.Data;

namespace EscapeTheLava.View
{

    /// Controls the visual appearance of one grid tile and detects player clicks.
    /// TileView handles visual feedback only.
    /// Gameplay logic is handled by GameManager/GameService.
    
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


        
        private int _x;
        private int _y;


        
        private TileType _currentType;

        private bool _isCollecting;


        
        /// Event sent when this tile is clicked.
        /// Sends the tile's X and Y grid position.
     
        public event Action<int, int> TileClicked;


        
        /// Gives this tile its grid position.
      
        public void SetPosition(int x, int y)
        {
            _x = x;
            _y = y;
        }


       
        /// Displays the correct visual for this tile.
      
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
                    diamondView.transform.localScale = Vector3.one;

                    break;


                case TileType.Lava:

                    lavaView.gameObject.SetActive(true);

                    break;
            }
        }



        /// Called automatically by Unity when the player clicks/taps this tile.
        
        public void OnPointerClick(PointerEventData eventData)
        {
            // Ignore clicks while an animation
            // is already running.
            if (_isCollecting)
                return;


            

            if (_currentType == TileType.Diamond)
            {
                StartCoroutine(CollectDiamond());

                return;
            }


           

            if (_currentType == TileType.Lava)
            {
                StartCoroutine(PlayLavaDamage());

                return;
            }


           
            TileClicked?.Invoke(_x,_y);
        }


   
        private IEnumerator CollectDiamond()
        {
            _isCollecting = true;


            Transform diamond = diamondView.transform;


            Vector3 startScale = diamond.localScale;


            Vector3 targetScale =startScale * collectScale;


            float elapsed = 0f;


           

            while (elapsed < collectDuration)
            {
                elapsed += Time.deltaTime;


                float t =  elapsed / collectDuration;


                t = Mathf.SmoothStep(0f,1f,t);


                diamond.localScale =  Vector3.Lerp(startScale,targetScale, t);


                yield return null;
            }


            // Make sure final scale is correct.
            diamond.localScale =  targetScale;
            diamondView.gameObject.SetActive(false);
            ShowScorePopup();
            _isCollecting = false;
            TileClicked?.Invoke(_x, _y);
        }


       
        /// Displays the +1 popup exactly
        /// at the clicked tile's position.
        
        private void ShowScorePopup()
        {
            if (scorePopupPrefab == null)
                return;


            // Create the popup under the
            // same parent as the TileView.
            ScorePopup popup = Instantiate(scorePopupPrefab,transform.parent);


            RectTransform popupRect =popup.GetComponent<RectTransform>();


            RectTransform tileRect =GetComponent<RectTransform>();


            // Put popup at this tile's position.
            popupRect.anchoredPosition = tileRect.anchoredPosition;


            // Start popup animation.
            popup.Play(1);
        }


        
        private IEnumerator PlayLavaDamage()
        {
            _isCollecting = true;


            // Play burn effect.
            if (burnEffect != null)
            {
                burnEffect.Play(burnScale,burnDuration,burnJiggleAmount, burnJiggleDuration);
            }


            // Wait until the visual effect
            // has finished.
            yield return new WaitForSeconds(burnDuration + burnJiggleDuration);


            _isCollecting = false;


            // Tell GameManager/GameService
            // that lava was clicked.
            TileClicked?.Invoke(_x,_y);
        }
    }
}