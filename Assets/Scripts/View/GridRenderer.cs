using System;
using UnityEngine;
using EscapeTheLava.Data;

namespace EscapeTheLava.View
{
    /// <summary>
    /// Creates and displays the visual grid.
    /// 
    /// GridRenderer does not contain gameplay logic.
    /// It only reads GridData and updates the TileViews.
    /// </summary>
    public class GridRenderer : MonoBehaviour
    {
        [Header("Grid References")]

        [SerializeField]
        private TileView tilePrefab;

        [SerializeField]
        private Transform gridRoot;


        [Header("Layout")]

        [SerializeField]
        private float cellSize = 55f;

        [SerializeField]
        private float spacing = 4f;


        // Stores the visual TileView for every grid position.
        private TileView[,] _tileViews;


        // Sends the clicked grid position to GameManager.
        public event Action<int, int> TileClicked;


        /// <summary>
        /// Creates the visual grid and displays the initial state.
        /// </summary>
        public void Initialize(GridData grid)
        {
            CreateTiles(grid);
            Render(grid);
        }


        /// <summary>
        /// Creates one TileView for every logical grid cell.
        /// </summary>
        private void CreateTiles(GridData grid)
        {
            _tileViews = new TileView[
                grid.Columns,
                grid.Rows];


            // Calculate the complete size of the grid.
            float totalWidth =
                grid.Columns * cellSize +
                (grid.Columns - 1) * spacing;

            float totalHeight =
                grid.Rows * cellSize +
                (grid.Rows - 1) * spacing;


            // Calculate the starting position
            // so the complete grid stays centered.
            float startX =
                -totalWidth * 0.5f +
                cellSize * 0.5f;

            float startY =
                totalHeight * 0.5f -
                cellSize * 0.5f;


            // Create every visual tile.
            for (int y = 0; y < grid.Rows; y++)
            {
                for (int x = 0; x < grid.Columns; x++)
                {
                    TileView tile =
                        Instantiate(
                            tilePrefab,
                            gridRoot);


                    RectTransform rect =
                        tile.GetComponent<RectTransform>();


                    // Position the tile.
                    rect.anchoredPosition =
                        new Vector2(
                            startX +
                            x * (cellSize + spacing),

                            startY -
                            y * (cellSize + spacing));


                    // Set the visual size.
                    rect.sizeDelta =
                        new Vector2(
                            cellSize,
                            cellSize);


                    // Store the TileView.
                    _tileViews[x, y] = tile;


                    // Tell the TileView its logical grid position.
                    tile.SetPosition(x, y);


                    // Listen for clicks.
                    tile.TileClicked += OnTileClicked;
                }
            }
        }


        /// <summary>
        /// Called when a TileView is clicked.
        /// 
        /// We forward the position to whoever is listening,
        /// usually GameManager.
        /// </summary>
        private void OnTileClicked(int x, int y)
        {
            TileClicked?.Invoke(x, y);
        }


        /// <summary>
        /// Updates the visual grid using the current GridData.
        /// 
        /// This method does not change the logical data.
        /// It only displays it.
        /// </summary>
        public void Render(GridData grid)
        {
            for (int y = 0; y < grid.Rows; y++)
            {
                for (int x = 0; x < grid.Columns; x++)
                {
                    _tileViews[x, y]
                        .SetTile(
                            grid.GetTile(x, y).Type);
                }
            }
        }
    }
}