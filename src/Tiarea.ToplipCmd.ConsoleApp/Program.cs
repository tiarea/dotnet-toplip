using System;

namespace Tiarea.ToplipCmd.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
             Toplip.EncryptAsync("/root/toplip/test.txt", $"/root/toplip/{Guid.NewGuid()}.txt", "test").Wait();
        }
    }
}
