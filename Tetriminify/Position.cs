﻿using System;
using System.Windows;

namespace Tetriminify
{
    public struct Position : IEquatable<Position>
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static bool operator ==(Position pos1, Position pos2)
        {
            return Equals(pos1, pos2);
        }

        public static bool operator !=(Position pos1, Position pos2)
        {
            return !(pos1 == pos2);
        }

        public static bool Equals(Position pos1, Position pos2)
        {
            return pos1.X == pos2.X && pos1.Y == pos2.Y;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Position)) return false;

            var value = (Position) obj;
            return Equals(this, value);
        }

        public bool Equals(Position pos)
        {
            return Equals(this, pos);
        }

        public override int GetHashCode()
        {
            return X ^ Y;
        }

        public Point ToPoint()
        {
            return new Point(X, Y);
        }

        public override string ToString()
        {
            return string.Format("<Position X:{0} Y:{1}>", X, Y);
        }
    }
}