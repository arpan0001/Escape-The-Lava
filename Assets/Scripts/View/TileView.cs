using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

using EscapeTheLava.Data;

namespace EscapeTheLava.View
{
    /// <summary>
    /// Controls the visual appearance of one grid tile
    /// and detects when the player clicks/taps it.
    /// 
    /// TileView does NOT contain gameplay logic.
    /// It only reports which tile was clicked.
    /// </summary>
    public class TileView : MonoBehaviour, IPointerClickHandler
    {
        [Header("References")]
        [SerializeField]
        private Image background;

        [SerializeField]
        private Image icon;

        [Header("Sprites")]
        [SerializeField]
        private Sprite islandSprite;

        [SerializeField]
        private Sprite diamondSprite;

        [SerializeField]
        private Sprite lavaSprite;

        private int _x;
        private int _y;

        /// <summary>
        /// Gives this visual tile its grid position.
        /// </summary>
        public void SetPosition(int x, int y)
        {
            _x = x;
            _y = y;
        }

        /// <summary>
        /// Changes the visual appearance of the tile.
        /// </summary>
        public void SetTile(TileType type)
        {
            switch (type)
            {
                case TileType.Island:

                    icon.sprite = islandSprite;
                    icon.enabled = true;

                    break;

                case TileType.Diamond:

                    icon.sprite = diamondSprite;
                    icon.enabled = true;

                    break;

                case TileType.Lava:

                    icon.sprite = lavaSprite;
                    icon.enabled = true;

                    break;
            }
        }

        /// <summary>
        /// Called automatically by Unity when
        /// the player clicks/taps this UI tile.
        /// </summary>
        public void OnPointerClick(
            PointerEventData eventData)
        {
            TileClicked?.Invoke(_x, _y);
        }

        /// <summary>
        /// Sends the clicked grid position.
        /// </summary>
        public System.Action<int, int> TileClicked;
    }
}