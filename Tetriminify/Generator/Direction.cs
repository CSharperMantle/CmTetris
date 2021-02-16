﻿namespace Tetriminify.Generator
{
    public enum Direction
    {
        /// <summary>
        /// <para>----</para>
        /// <para>----</para>
        /// <para>~-o+</para>
        /// <para>-+++</para>
        /// </summary>
        Left,

        /// <summary>
        /// <para>--^-</para>
        /// <para>-+--</para>
        /// <para>-+o-</para>
        /// <para>-++-</para>
        /// </summary>
        Up,

        /// <summary>
        /// <para>----</para>
        /// <para>-+++</para>
        /// <para>-+o-</para>
        /// <para>----</para>
        /// </summary>
        Right,

        /// <summary>
        /// <para>----</para>
        /// <para>--++</para>
        /// <para>--o+</para>
        /// <para>--v+</para>
        /// </summary>
        Down
    }
}
