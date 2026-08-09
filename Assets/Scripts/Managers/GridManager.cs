using System;
using EscapeTheLava.Data;
using EscapeTheLava.Utilities;

namespace EscapeTheLava.Managers
{
    
    /// Creates and manages the logical board.
    /// This class does not create Unity GameObjects.
    
    public class GridManager
    {
        private readonly Random _random;

        public GridData Grid { get; private set; }

        public GridManager()
        {
            _random = new Random();
        }

       
        /// Creates the basic board.
        
        public void Initialize()
        {
            Grid = new GridData( GameConstants.GridColumns,GameConstants.GridRows);

            GenerateBaseBoard();
        }

        public void SetTile(int x, int y, TileType type)
        {
            Grid.SetTile(x, y, type);
        }

        
        /// Creates a board containing islands,
        /// lava and the diamonds for the current wave.
        
        public void GenerateWave(int diamondCount)
        {
            Grid.Clear();

            FillWithIslands();

           
            PlaceRandomTiles( TileType.Lava, GameConstants.LavaCount);

            
            PlaceRandomTiles( TileType.Diamond,diamondCount);
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
                    Grid.SetTile( x,y,TileType.Island);
                }
            }
        }

        private void PlaceRandomTiles(TileType type, int amount)
        {
            int placed = 0;

            while (placed < amount)
            {
                int x = _random.Next( 0, Grid.Columns);

                int y = _random.Next( 0, Grid.Rows);

                TileData currentTile = Grid.GetTile(x, y);

                
                if (currentTile.Type != TileType.Island)
                    continue;

                Grid.SetTile(  x, y,type);

                placed++;
            }
        }

        public void Restart()
        {
            Initialize();
        }
    }
}