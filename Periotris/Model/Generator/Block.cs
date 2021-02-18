namespace Periotris.Model.Generator
{
    internal class Block : IBlock
    {
        public TetriminoKind FilledBy { get; set; }

        public Position Position { get; }
        
        public int AtomicNumber { get; set; }

        public Block(TetriminoKind filledBy, Position position, int atomicNumber = 0)
        {
            FilledBy = filledBy;
            Position = position;
            AtomicNumber = atomicNumber;
        }

        public override string ToString()
        {
            return string.Format("<Block FilledBy:{0} Position:{1}>", FilledBy, Position);
        }
    }
}
