namespace Util
{

    public class Pair<X, Y>
    {
        private X key;
        private Y value;

        public Pair(X key, Y value)
        {
            this.key = key;
            this.value = value;
        }

        public X GetKey()
        {
            return key;
        }

        public Y GetValue()
        {
            return value;
        }
    }
}