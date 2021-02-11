using System;
using System.Windows;

namespace SimpleTetris.Model
{
    public struct Area : IEquatable<Area>
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public Area(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public static bool operator ==(Area area1, Area area2)
        {
            return Equals(area1, area2);
        }

        public static bool operator !=(Area area1, Area area2)
        {
            return !(area1 == area2);
        }

        public static bool Equals(Area area1, Area area2)
        {
            return area1.Width == area2.Width && area1.Height == area2.Height;
        }

        public override bool Equals(object obj)
        {
            if ((obj == null) || !(obj is Area))
            {
                return false;
            }

            var value = (Area)obj;
            return Equals(this, value);
        }

        public bool Equals(Area area)
        {
            return Equals(this, area);
        }

        public override int GetHashCode()
        {
            return Width ^ Height;
        }

        public Size ToSize()
        {
            return new Size(Width, Height);
        }

        public override string ToString()
        {
            return string.Format("<Area Width:{0} Height:{1}>", Width, Height);
        }
    }
}
