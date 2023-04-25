namespace GlobalUtils
{
    public static class Convertor
    {
        public static long ConvertToLong(this ulong input)
        {
            return input <= long.MaxValue ? (long)input : long.MaxValue;
        }

        public static float ConvertToMb(this long bytes)
        {
            const long BytesInMb = 1048576;

            float result = (float)bytes / BytesInMb;
            return result;
        }

        public static float ConvertToMb(this ulong bytes)
        {
            long bytesConverted = bytes.ConvertToLong();
            return bytesConverted.ConvertToMb();
        }
    }
}