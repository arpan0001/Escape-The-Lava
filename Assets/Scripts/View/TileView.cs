using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

using EscapeTheLava.Data;

namespace EscapeTheLava.View
{
    /// <summary>
    /// Represents one cell of the game grid.
    ///
    /// TileView controls which visual object is visible:
    /// Island, Diamond, or Lava.
    ///
    /// It also detects player clicks/taps.
    ///
    /// It does not contain gameplay logic.
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


        private int _x;
        private int _y;


        /// <summary>
        /// Gives this tile its position in the grid.
        /// </summary>
        public void SetPosition(int x, int y)
        {
            _x = x;
            _y = y;
        }


        /// <summary>
        /// Changes which visual object is visible.
        /// </summary>
        public void SetTile(TileType type)
        {
            // First hide all visuals.
            islandView.gameObject.SetActive(false);
            diamondView.gameObject.SetActive(false);
            lavaView.gameObject.SetActive(false);


            // Then show the correct visual.
            switch (type)
            {
                case TileType.Island:

                    islandView.gameObject.SetActive(true);

                    break;


                case TileType.Diamond:

                    diamondView.gameObject.SetActive(true);

                    break;


                case TileType.Lava:

                    lavaView.gameObject.SetActive(true);

                    break;
            }
        }


        /// <summary>
        /// Called automatically by Unity
        /// when the player clicks or taps this tile.
        /// </summary>
        public void OnPointerClick(
            PointerEventData eventData)
        {
            TileClicked?.Invoke(_x, _y);
        }


        /// <summary>
        /// Sends the clicked grid position
        /// to the system that is listening.
        /// </summary>
        public System.Action<int, int> TileClicked;
    }
}