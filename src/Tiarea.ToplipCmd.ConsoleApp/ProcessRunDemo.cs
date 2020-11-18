using System;
using System.Diagnostics;
using System.IO;

namespace Tiarea.ToplipCmd.ConsoleApp
{
    public static class ProcessRunDemo
    {
        /// <summary>
        /// System.IO.IOException: 
        /// Unable to write data to the transport connection: Broken pipe.
        /// </summary>
        public static void PipeOutput()
        {
            var process = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = @"/root/toplip/toplip",
                    RedirectStandardOutput = false,
                    RedirectStandardInput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    Arguments = $"-b -c {1} /root/toplip/test.txt > /root/toplip/test_pip_encrypt.txt"
                }
            };

            process.Start();

            process.StandardInput.WriteLine("test");

            process.WaitForExit();
        }

        /// <summary>
        /// output base64 string then convert to byte[] save the file.
        /// </summary>
        public static void RedierctStandardOutputUseBase64()
        {
            var process = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = @"/root/toplip/toplip",
                    RedirectStandardOutput = true,
                    RedirectStandardInput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    Arguments = $"-b -c {1} /root/toplip/test.txt"
                }
            };

            process.Start();

            process.StandardInput.WriteLine("test");


            var encryptBytes = Convert.FromBase64String(process.StandardOutput.ReadToEnd());

            process.WaitForExit();

            using (var stream = File.Create("/root/toplip/test_encrypt.txt"))
            {
                stream.Write(encryptBytes);
                stream.Flush();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static void RedierctStandardOutputUseStreamReader()
        {
            using (var process = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = @"/root/toplip/toplip",
                    RedirectStandardOutput = true,
                    RedirectStandardInput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    Arguments = $" -c {1} /root/toplip/test.txt"
                }
            })
            {

                process.Start();

                process.StandardInput.WriteLine("test");

                process.WaitForExit();

                using (var stream = File.Create("/root/toplip/test_encrypt_stream.txt"))
                {
                    process.StandardOutput.BaseStream.CopyTo(stream);
                    stream.Flush();
                }
            }
        }
    }
}
