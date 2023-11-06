using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamBuilder.Managers.Busy
{
    public class BusyManager : IBusyManager
    {
        /// <summary>
        /// The busy stack.
        /// </summary>
        private readonly ConcurrentStack<int> _busyStack = new();
        public event EventHandler BusyChangedEvent;

        /// <summary>
        /// Gets a value indicating whether is busy.
        /// </summary>
        public bool IsBusy => _busyStack.Any();

        #region Public Methods
        /// <summary>
        /// Sets the busy.
        /// </summary>
        /// <returns>A Task.</returns>
        public async Task SetBusy()
        {
            _busyStack.Push(1);
            await InvokeBusyChangedEvent();
        }

        /// <summary>
        /// Sets the un busy.
        /// </summary>
        /// <returns>A Task.</returns>
        public async Task SetUnBusy()
        {
            _busyStack.TryPop(out _);
            await InvokeBusyChangedEvent();
        }
        #endregion  Public Methods      

        #region Private Methods
        /// <summary>
        /// Invokes the busy changed event.
        /// </summary>
        /// <returns>A Task.</returns>
        private async Task InvokeBusyChangedEvent()
        {
            await Task.Run(() => BusyChangedEvent?.Invoke(this, null));
        }

        #endregion Private Methods
    }
}
