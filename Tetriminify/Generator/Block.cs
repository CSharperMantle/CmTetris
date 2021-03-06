namespace Tetriminify.Generator
{
    internal class Block : IBlock
    {
        public Block(TetriminoKind filledBy, Position position)
        {
            FilledBy = filledBy;
            Position = position;
        }

        public TetriminoKind FilledBy { get; set; }

        public Position Position { get; set; }

        public override string ToString()
        {
            return string.Format("<Block FilledBy:{0} Position:{1}>", FilledBy, Position);
        }
    }
}