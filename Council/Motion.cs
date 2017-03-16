using System.Collections.Generic;

namespace Council
{
    public abstract class Motion<TTopic1,TReply1,TCouncilMember1, TTopic2, TReply2, TCouncilMember2> 
        where TReply1 : IReply where TTopic1 : class where TCouncilMember1 : IMember<TTopic1, TReply1>
        where TReply2 : IReply where TTopic2 : class where TCouncilMember2 : IMember<TTopic2, TReply2>
    {
        protected abstract TTopic2 Transform1(IEnumerable<TReply1> replies);
        public IEnumerable<TReply2> Raise(TTopic1 topic1, Council<TCouncilMember1, TTopic1, TReply1> council1, Council<TCouncilMember2, TTopic2, TReply2> council2)
        {
            var replies1 = council1.RaiseTopic(topic1);

            var topic2 = Transform1(replies1);
            if (topic2 == null)
                return null;

            var replies2 = council2.RaiseTopic(topic2);
            return replies2;
        }
    }

    public abstract class Motion<TTopic1, TReply1, TCouncilMember1
                                ,TTopic2, TReply2, TCouncilMember2
                                ,TTopic3, TReply3, TCouncilMember3>
        where TReply1 : IReply where TTopic1 : class where TCouncilMember1 : IMember<TTopic1, TReply1>
        where TReply2 : IReply where TTopic2 : class where TCouncilMember2 : IMember<TTopic2, TReply2>
        where TReply3 : IReply where TTopic3 : class where TCouncilMember3 : IMember<TTopic3, TReply3>
    {
        protected abstract TTopic2 Transform1(IEnumerable<TReply1> replies);
        protected abstract TTopic3 Transform2(IEnumerable<TReply2> replies);
        public IEnumerable<TReply3> Raise(TTopic1 topic1, Council<TCouncilMember1, TTopic1, TReply1> council1, Council<TCouncilMember2, TTopic2, TReply2> council2, Council<TCouncilMember3, TTopic3, TReply3> council3)
        {
            var replies1 = council1.RaiseTopic(topic1);

            var topic2 = Transform1(replies1);
            if (topic2 == null)
                return null;

            var replies2 = council2.RaiseTopic(topic2);
            var topic3 = Transform2(replies2);
            if (topic3 == null)
                return null;

            var replies3 = council3.RaiseTopic(topic3);
            return replies3;
        }
    }
}
