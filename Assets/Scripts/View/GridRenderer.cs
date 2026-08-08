using UnityEngine;
using EscapeTheLava.Data;

namespace EscapeTheLava.View
{
    /// <summary>
    /// Converts the logical GridData into visible Unity tiles.
    ///
    /// This class does not decide what the tile means.
    /// It only displays what GridData tells it.
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
        private float cellSize = 90f;

        [SerializeField]
        private float spacing = 6f;

        private TileView[,] _tileViews;

        /// <summary>
        /// Creates all visual tiles.
        /// </summary>
        public void Initialize(GridData grid)
        {
            CreateTiles(grid);

            Render(grid);
        }

        private void CreateTiles(GridData grid)
        {
            _tileViews =
                new TileView[
                    grid.Columns,
                    grid.Rows];

            float totalWidth =
                grid.Columns * cellSize +
                (grid.Columns - 1) * spacing;

            float totalHeight =
                grid.Rows * cellSize +
                (grid.Rows - 1) * spacing;

            float startX =
                -totalWidth * 0.5f +
                cellSize * 0.5f;

            float startY =
                totalHeight * 0.5f -
                cellSize * 0.5f;

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

                    rect.anchoredPosition =
                        new Vector2(
                            startX +
                            x * (cellSize + spacing),

                            startY -
                            y * (cellSize + spacing));

                    rect.sizeDelta =
                        new Vector2(
                            cellSize,
                            cellSize);

                    _tileViews[x, y] = tile;
                }
            }
        }

        /// <summary>
        /// Updates the visual tiles
        /// according to the logical grid.
        /// </summary>
        public void Render(GridData grid)
        {
            for (int y = 0; y < grid.Rows; y++)
            {
                for (int x = 0; x < grid.Columns; x++)
                {
                    TileData tile =
                        grid.GetTile(x, y);

                    _tileViews[x, y]
                        .SetTile(tile.Type);
                }
            }
        }
    }
}