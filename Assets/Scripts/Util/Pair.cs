namespace Util
{

    public class Pair<TX, TY>
    {
        public TX Key { get; set; }

        public TY Value { get; set; }


        public Pair(TX key, TY value)
        {
            Key = key;
            Value = value;
        }
    }
}