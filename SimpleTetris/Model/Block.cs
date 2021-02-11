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

        public override string ToString()
        {
            return string.Format("<Block FilledBy:{0} Position:{1}>", FilledBy, Position);
        }
    }
}
