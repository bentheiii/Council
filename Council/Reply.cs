namespace Council
{
    public interface IReply
    {
        bool Halt { get; }
        bool Ignore { get; }
    }
    public class HaltReply : IReply
    {
        public bool Halt => true;
        public bool Ignore => false;
    }
    public class IgnoreReply : IReply
    {
        public bool Halt => false;
        public bool Ignore => true;
    }
    public class StandardReply : IReply
    {
        public bool Halt => false;
        public bool Ignore => false;
    }
}
