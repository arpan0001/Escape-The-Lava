using System;

namespace EscapeTheLava.Data
{
    /// <summary>
    /// Stores the complete logical state of the game board.
    /// This class does not use Unity APIs.
    /// </summary>
    public class GridData
    {
        private readonly TileData[,] _tiles;

        public int Columns { get; }

        public int Rows { get; }

        public GridData(int columns, int rows)
        {
            if (columns <= 0)
                throw new ArgumentException("Columns must be greater than zero.");

            if (rows <= 0)
                throw new ArgumentException("Rows must be greater than zero.");

            Columns = columns;
            Rows = rows;

            _tiles = new TileData[columns, rows];
        }

        /// <summary>
        /// Gets the tile at the given position.
        /// </summary>
        public TileData GetTile(int x, int y)
        {
            if (!IsInside(x, y))
                throw new IndexOutOfRangeException(
                    $"Grid position ({x}, {y}) is outside the board.");

            return _tiles[x, y];
        }

        /// <summary>
        /// Places a tile at the given position.
        /// </summary>
        public void SetTile(int x, int y, TileType type)
        {
            if (!IsInside(x, y))
                throw new IndexOutOfRangeException(
                    $"Grid position ({x}, {y}) is outside the board.");

            _tiles[x, y] = new TileData(x, y, type);
        }

        /// <summary>
        /// Checks whether a position exists inside the board.
        /// </summary>
        public bool IsInside(int x, int y)
        {
            return x >= 0 &&
                   x < Columns &&
                   y >= 0 &&
                   y < Rows;
        }

        /// <summary>
        /// Returns the total number of cells.
        /// </summary>
        public int CellCount
        {
            get
            {
                return Columns * Rows;
            }
        }

        /// <summary>
        /// Clears the entire board.
        /// </summary>
        public void Clear()
        {
            for (int x = 0; x < Columns; x++)
            {
                for (int y = 0; y < Rows; y++)
                {
                    _tiles[x, y] = null;
                }
            }
        }

        /// <summary>
        /// Counts how many tiles of a specific type exist.
        /// </summary>
        public int Count(TileType type)
        {
            int count = 0;

            for (int x = 0; x < Columns; x++)
            {
                for (int y = 0; y < Rows; y++)
                {
                    TileData tile = _tiles[x, y];

                    if (tile != null && tile.Type == type)
                    {
                        count++;
                    }
                }
            }

            return count;
        }
    }
}   