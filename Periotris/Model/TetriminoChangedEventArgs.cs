using System;

namespace Periotris.Model
{
    /// <summary>
    ///     A subclass of <see cref="EventArgs" /> which is used when a <see cref="IBlock" /> changes, either in position or if
    ///     being shown.
    /// </summary>
    public class BlockChangedEventArgs : EventArgs
    {
        public BlockChangedEventArgs(IBlock blockUpdated, bool disappeared)
        {
            BlockUpdated = blockUpdated;
            Disappeared = disappeared;
        }

        public IBlock BlockUpdated { get; }
        public bool Disappeared { get; }
    }
}