using System;
using EscapeTheLava.Data;
using EscapeTheLava.Utilities;

namespace EscapeTheLava.Managers
{
    /// <summary>
    /// Creates and manages the logical board.
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
        /// Creates the basic board.
        /// </summary>
        public void Initialize()
        {
            Grid = new GridData(
                GameConstants.GridColumns,
                GameConstants.GridRows);

            GenerateBaseBoard();
        }

        public void SetTile(int x, int y, TileType type)
        {
            Grid.SetTile(x, y, type);
        }

        /// <summary>
        /// Creates a board containing islands,
        /// lava and the diamonds for the current wave.
        /// </summary>
        public void GenerateWave(int diamondCount)
        {
            Grid.Clear();

            // Start with safe islands.
            FillWithIslands();

            // Add lava obstacles.
            PlaceRandomTiles(
                TileType.Lava,
                GameConstants.LavaCount);

            // Add diamonds for this wave.
            PlaceRandomTiles(
                TileType.Diamond,
                diamondCount);
        }

        private void GenerateBaseBoard()
        {
            FillWithIslands();
        }

        private void FillWithIslands()
        {
            for (int x = 0; x < Grid.Columns; x++)
            {
                for (int y = 0; y < Grid.Rows; y++)
                {
                    Grid.SetTile(
                        x,
                        y,
                        TileType.Island);
                }
            }
        }

        private void PlaceRandomTiles(
            TileType type,
            int amount)
        {
            int placed = 0;

            while (placed < amount)
            {
                int x =
                    _random.Next(
                        0,
                        Grid.Columns);

                int y =
                    _random.Next(
                        0,
                        Grid.Rows);

                TileData currentTile =
                    Grid.GetTile(x, y);

                // Only replace safe islands.
                if (currentTile.Type != TileType.Island)
                    continue;

                Grid.SetTile(
                    x,
                    y,
                    type);

                placed++;
            }
        }

        public void Restart()
        {
            Initialize();
        }
    }
}