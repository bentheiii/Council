using System;
using System.Collections.Generic;
using System.Linq;
using WhetStone.Looping;

namespace Council
{
    public class Council<TMember,TTopic,TReply> where TReply: IReply where TMember : IMember<TTopic,TReply> where TTopic:class
    {
        private readonly ISet<TMember> _members = new HashSet<TMember>();
        public IEnumerable<TMember> Members => _members;

        public void Join(TMember member)
        {
            _members.Add(member);
        }
        public void Leave(TMember member)
        {
            _members.Remove(member);
        }

        public IEnumerable<TReply> RaiseRaw(TTopic topic)
        {
            var intents = _members.Select(a => a.getIntent(topic)).ToArray();
            foreach (var intent in intents)
            {
                var reply = intent.handle(topic);
                yield return reply;
            }
        }

        public IEnumerable<TReply> RaiseTopic(TTopic topic)
        {
            return RaiseRaw(topic).EnumerationHook(a =>
            {
                if (a == null)
                    throw new Exception("member returned a null in council.");
            }).Where(a => !a.Ignore).TakeWhileInclusive(a => !a.Halt).ToArray();
        }
    }
}
