namespace Periotris.Model.Generation
{
    internal class Block : IBlock
    {
        public Block(TetriminoKind filledBy, Position position)
            : this(filledBy, position, 0, 0)
        {
        }

        public Block(TetriminoKind filledBy, Position position, int atomicNumber)
            : this(filledBy, position, atomicNumber, 0)
        {
        }

        public Block(TetriminoKind filledBy, Position position, int atomicNumber, int identifier)
        {
            FilledBy = filledBy;
            Position = position;
            AtomicNumber = atomicNumber;
            Identifier = identifier;
        }

        public int Identifier { get; set; }
        public TetriminoKind FilledBy { get; set; }

        public Position Position { get; set; }

        public int AtomicNumber { get; set; }

        public override string ToString()
        {
            return $"<Block FilledBy:{FilledBy} Position:{Position}>";
        }
    }
}