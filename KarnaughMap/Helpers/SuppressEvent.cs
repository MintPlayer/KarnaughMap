using System;
using System.ComponentModel;
using System.Reflection;

namespace KarnaughMap.Helpers
{
    internal class SuppressEvent<T> : IDisposable
    {
        public SuppressEvent(T target, string eventName)
        {
            // https://stackoverflow.com/questions/91778/how-to-remove-all-event-handlers-from-an-event
            this.target = target;
            var fi = typeof(T).GetField(eventName, BindingFlags.Instance | BindingFlags.NonPublic);
            handlers = fi.GetValue(target);
            var pi = typeof(T).GetProperty("Events", BindingFlags.Instance | BindingFlags.NonPublic);
            list = (EventHandlerList)pi.GetValue(target, null);
            buffer = list[handlers];
            list.RemoveHandler(handlers, buffer);
        }

        private T target;
        private EventHandlerList list;
        private Delegate buffer;
        private object handlers;

        #region Dispose
        private bool disposed = false;
        public void Dispose()
        {
            if (disposed) return;
            disposed = true;

            list.AddHandler(handlers, buffer);

            target = default;
            list = null;
            buffer = null;
            handlers = null;
        }
        #endregion
    }
}
