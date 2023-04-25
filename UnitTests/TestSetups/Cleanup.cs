namespace UnitTests.TestSetups
{
    public static class Cleanup
    {
        public static void DeleteFileIfItExists(string path)
        {
            if (File.Exists(path))
                File.Delete(path);
        }
    }
}