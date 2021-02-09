namespace SimpleTetris.Model
{
    public class Block
    {
        public TetriminoKind FilledBy { get; }
        public Position Position { get; }

        public Block(TetriminoKind filledBy, Position position)
        {
            FilledBy = filledBy;
            Position = position;
        }
    }
}
