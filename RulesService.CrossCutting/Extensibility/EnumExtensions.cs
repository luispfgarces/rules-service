namespace System
{
    public static class EnumExtensions
    {
        public static T As<T>(this object obj)
            where T : struct, IConvertible => (T)obj;
    }
}