using System;

namespace Roguelike.Event
{
    public class EventController<TDelegate> where TDelegate : Delegate
    {
        private TDelegate baseEvent;

        public void AddListener(TDelegate listener) => baseEvent = (TDelegate)Delegate.Combine(baseEvent, listener);
        public void RemoveListener(TDelegate listener) => baseEvent = (TDelegate)Delegate.Remove(baseEvent, listener);

        public void Invoke(params object[] args)
        {
            if (baseEvent == null) return;

            foreach (var handler in baseEvent.GetInvocationList())
            {
                handler?.DynamicInvoke(args);
            }
        }

        public TResult Invoke<TResult>(params object[] args)
        {
            if (baseEvent == null) return default;

            TResult result = default;

            foreach (var handler in baseEvent.GetInvocationList())
            {
                result = (TResult)handler?.DynamicInvoke(args);
            }

            return result;
        }
    }
}