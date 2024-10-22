namespace Util
{
    public static class BoolToIntConversion
    {

        public static int ToInt(this bool b)
        {
            return b ? 1 : 0;
        }
        
        public static bool ToBool(this int i)
        {
            return i == 1 ? true : false;
        }
    }
}