using System;
using System.IO;

namespace FilenameRandomizer
{
    class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();
            foreach (string arg in args)
            {
                try
                {
                    string name = arg;
                    while (File.Exists(name))
                    {
                        name = Path.Combine(Path.GetDirectoryName(arg), $"{random.Next()}{Path.GetExtension(arg)}");
                    }
                    File.Move(arg, name);
                }
                catch
                {

                }
            }
        }
    }
}
