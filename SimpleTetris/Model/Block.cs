namespace SimpleTetris.Model
{
    public class Block
    {
        public Block(TetriminoKind filledBy, Position position)
        {
            FilledBy = filledBy;
            Position = position;
        }

        public TetriminoKind FilledBy { get; }
        public Position Position { get; }

        public override string ToString()
        {
            return string.Format("<Block FilledBy:{0} Position:{1}>", FilledBy, Position);
        }
    }
}