using System;
using System.Collections.Generic;

namespace Council
{
    public abstract class Motion<TSuperCouncil
                                ,TTopic1,TReply1,TCouncilMember1
                                ,TTopic2, TReply2, TCouncilMember2> 
        where TReply1 : IReply where TTopic1 : class where TCouncilMember1 : IMember<TTopic1, TReply1>
        where TReply2 : IReply where TTopic2 : class where TCouncilMember2 : IMember<TTopic2, TReply2>
    {
        protected abstract Council<TCouncilMember1, TTopic1, TReply1> FirstCouncil(TSuperCouncil sc, TTopic1 topic);
        protected abstract Tuple<TTopic2, Council<TCouncilMember2, TTopic2, TReply2>> Transform1(TSuperCouncil sc, IEnumerable<TReply1> replies);
        public IEnumerable<TReply2> Raise(TTopic1 topic, TSuperCouncil supercouncil)
        {
            var replies1 = FirstCouncil(supercouncil, topic).RaiseTopic(topic);

            var tranform2 = Transform1(supercouncil, replies1);
            var topic2 = tranform2.Item1;
            var council2 = tranform2.Item2;
            if (topic2 == null)
                return null;

            var replies2 = council2.RaiseTopic(topic2);
            return replies2;
        }
    }

    public abstract class Motion<TSuperCouncil
                                ,TTopic1, TReply1, TCouncilMember1
                                ,TTopic2, TReply2, TCouncilMember2
                                ,TTopic3, TReply3, TCouncilMember3>
        where TReply1 : IReply where TTopic1 : class where TCouncilMember1 : IMember<TTopic1, TReply1>
        where TReply2 : IReply where TTopic2 : class where TCouncilMember2 : IMember<TTopic2, TReply2>
        where TReply3 : IReply where TTopic3 : class where TCouncilMember3 : IMember<TTopic3, TReply3>
    {
        protected abstract Council<TCouncilMember1, TTopic1, TReply1> FirstCouncil(TSuperCouncil sc, TTopic1 topic);
        protected abstract Tuple<TTopic2, Council<TCouncilMember2, TTopic2, TReply2>> Transform1(TSuperCouncil sc, IEnumerable<TReply1> replies);
        protected abstract Tuple<TTopic3, Council<TCouncilMember3, TTopic3, TReply3>> Transform2(TSuperCouncil sc, IEnumerable<TReply2> replies);
        public IEnumerable<TReply3> Raise(TTopic1 topic, TSuperCouncil supercouncil)
        {
            var replies1 = FirstCouncil(supercouncil,topic).RaiseTopic(topic);

            var tranform2 = Transform1(supercouncil, replies1);
            var topic2 = tranform2.Item1;
            var council2 = tranform2.Item2;
            if (topic2 == null)
                return null;

            var replies2 = council2.RaiseTopic(topic2);
            var transform3 = Transform2(supercouncil, replies2);
            var topic3 = transform3.Item1;
            var council3 = transform3.Item2;
            if (topic3 == null)
                return null;

            var replies3 = council3.RaiseTopic(topic3);
            return replies3;
        }
    }
}
