namespace Periotris.Model.Sorting
{
    internal class MemoizedBlock : Generator.Block
    {
        public MemoizedBlock(TetriminoKind filledBy, Position position, TetriminoNode owner, int atomicNumber = 0)
            : base(filledBy, position, atomicNumber)
        {
            Owner = owner;
        }

        public TetriminoNode Owner { get; set; }
    }
}
