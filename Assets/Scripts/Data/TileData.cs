namespace EscapeTheLava.Data
{
    /// <summary>
    /// Stores information about one cell on the board.
    /// </summary>
    public class TileData
    {
        public int X { get; }

        public int Y { get; }

        public TileType Type { get; private set; }

        public TileData(int x, int y, TileType type)
        {
            X = x;
            Y = y;
            Type = type;
        }

        public void SetType(TileType type)
        {
            Type = type;
        }
    }
}