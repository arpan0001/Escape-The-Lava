using System;
using EscapeTheLava.Data;
using EscapeTheLava.Utilities;

namespace EscapeTheLava.Managers
{
    /// <summary>
    /// Creates and manages the logical game board.
    /// This class does not create Unity GameObjects.
    /// </summary>
    public class GridManager
    {
        private readonly Random _random;

        public GridData Grid { get; private set; }

        public GridManager()
        {
            _random = new Random();
        }

        /// <summary>
        /// Creates a new game board.
        /// </summary>
        public void Initialize()
        {
            Grid = new GridData(
                GameConstants.GridColumns,
                GameConstants.GridRows);

            GenerateLevel();
        }

        /// <summary>
        /// Creates the tile layout.
        /// </summary>
        private void GenerateLevel()
        {
            Grid.Clear();

            // First fill every cell with a safe island.
            for (int x = 0; x < Grid.Columns; x++)
            {
                for (int y = 0; y < Grid.Rows; y++)
                {
                    Grid.SetTile(x, y, TileType.Island);
                }
            }

            PlaceRandomTiles(
                TileType.Diamond,
                15);

            PlaceRandomTiles(
                TileType.Lava,
                35);
        }

        /// <summary>
        /// Places a specific number of tiles randomly.
        /// </summary>
        private void PlaceRandomTiles(
            TileType type,
            int amount)
        {
            int placed = 0;

            while (placed < amount)
            {
                int x = _random.Next(0, Grid.Columns);

                int y = _random.Next(0, Grid.Rows);

                TileData currentTile =
                    Grid.GetTile(x, y);

                if (currentTile.Type != TileType.Island)
                    continue;

                Grid.SetTile(x, y, type);

                placed++;
            }
        }

        /// <summary>
        /// Creates a completely new level.
        /// </summary>
        public void Restart()
        {
            Initialize();
        }
    }
}