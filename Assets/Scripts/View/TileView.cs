using UnityEngine;
using UnityEngine.UI;
using EscapeTheLava.Data;

namespace EscapeTheLava.View
{
    /// <summary>
    /// Controls the visual appearance of one board cell.
    /// </summary>
    public class TileView : MonoBehaviour
    {
        [SerializeField]
        private Image background;

        [SerializeField]
        private Image icon;

        [Header("Tile Sprites")]
        [SerializeField]
        private Sprite islandSprite;

        [SerializeField]
        private Sprite diamondSprite;

        [SerializeField]
        private Sprite lavaSprite;

        /// <summary>
        /// Changes the visual appearance
        /// according to the tile type.
        /// </summary>
        public void SetTile(TileType type)
        {
            switch (type)
            {
                case TileType.Island:

                    icon.sprite = islandSprite;

                    break;

                case TileType.Diamond:

                    icon.sprite = diamondSprite;

                    break;

                case TileType.Lava:

                    icon.sprite = lavaSprite;

                    break;
            }
        }
    }
}   