﻿namespace Tetriminify
{
    /// <summary>
    ///     Kinds of Tetrimino.
    ///     <para>Legend:</para>
    ///     <para>+: Filled block</para>
    ///     <para>-: Unfilled block</para>
    ///     <para>^,v,left_arrow,right_arrow: Way up</para>
    ///     <para>o: Center</para>
    /// </summary>
    public enum TetriminoKind
    {
        /// <summary>
        ///     <para>-+^-</para>
        ///     <para>-+--</para>
        ///     <para>-+--</para>
        ///     <para>-+--</para>
        /// </summary>
        Linear,

        /// <summary>
        ///     <para>--^-</para>
        ///     <para>----</para>
        ///     <para>-++-</para>
        ///     <para>-++-</para>
        /// </summary>
        Cubic,

        /// <summary>
        ///     <para>--^-</para>
        ///     <para>-+--</para>
        ///     <para>-+--</para>
        ///     <para>-++-</para>
        /// </summary>
        LShapedCis,

        /// <summary>
        ///     <para>--^-</para>
        ///     <para>--+-</para>
        ///     <para>--+-</para>
        ///     <para>-++-</para>
        /// </summary>
        LShapedTrans,

        /// <summary>
        ///     <para>--^-</para>
        ///     <para>----</para>
        ///     <para>++--</para>
        ///     <para>-++-</para>
        /// </summary>
        ZigZagCis,

        /// <summary>
        ///     <para>--^-</para>
        ///     <para>----</para>
        ///     <para>-++-</para>
        ///     <para>++--</para>
        /// </summary>
        ZigZagTrans,

        /// <summary>
        ///     <para>--^-</para>
        ///     <para>----</para>
        ///     <para>-+--</para>
        ///     <para>+++-</para>
        /// </summary>
        TShaped,

        /// <summary>
        ///     Unfilled block.
        /// </summary>
        AvailableToFill,

        /// <summary>
        ///     Blocks that are forbidden to fill.
        /// </summary>
        UnavailableToFill
    }
}