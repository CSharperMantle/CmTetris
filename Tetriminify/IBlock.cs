namespace Tetriminify
{
    public interface IBlock
    {
        TetriminoKind FilledBy { get; }

        Position Position { get; }
    }
}
