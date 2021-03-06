namespace Tetriminify.Sorter
{
    internal class MemoizedBlock : IBlock
    {
        public TetriminoNode Owner { get; set; }
        public TetriminoKind FilledBy { get; set; }

        public Position Position { get; set; }
    }
}