namespace Maze.Core.Utils
{
    public enum Direction : byte
    {
        Up, 
        Down, 
        Left, 
        Right
    }

    public enum MapTile : byte
    {
        Wall,
        Path,
        Start,
        End,
    }
}
