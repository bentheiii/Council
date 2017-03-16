using System;

namespace Council
{
    public interface IMember<in TTopic, out TReply> where TReply : IReply where TTopic : class
    {
        IMemberIntent<TTopic, TReply> getIntent(TTopic topic);
    }
    public interface IMemberIntent<in TTopic, out TReply> where TReply : IReply where TTopic : class
    {
        TReply handle(TTopic topic);
    }
    public abstract class Member<TTopic, TReply> : IMember<TTopic, TReply> where TReply : IReply where TTopic : class
    {
        private class Intent : IMemberIntent<TTopic, TReply>
        {
            private readonly Member<TTopic, TReply> _upper;
            private bool _done = false;
            public Intent(Member<TTopic, TReply> upper)
            {
                _upper = upper;
            }
            public TReply handle(TTopic topic)
            {
                if (_done)
                    throw new InvalidOperationException("intent asked twice!");
                _done = true;
                return _upper.Handle(topic);
            }
        }
        protected abstract TReply Handle(TTopic topic);
        public IMemberIntent<TTopic, TReply> getIntent(TTopic topic)
        {
            return new Intent(this);
        }
    }
    public abstract class VoidMember<TTopic, TBlankReply> : Member<TTopic, TBlankReply> where TTopic : class where TBlankReply:IgnoreReply, new()
    {
        protected sealed override TBlankReply Handle(TTopic topic)
        {
            HandleNoReply(topic);
            return new TBlankReply();
        }
        protected abstract void HandleNoReply(TTopic topic);
    }
}
