using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace Tiarea.ToplipCmd
{
    public class ToplipExecutor
    {
        public ToplipProcessBuilder Builder { get; init; }
        public ToplipContext Context { get; }
        private Process Process { get; }

        public ToplipExecutor(ToplipProcessBuilder builder)
        {
            Builder = builder;
            Process = builder.Build();
            Context = builder.Context;
        }

        public void Execute()
        {
            using (Process)
            {
                Process.Start();

                // https://stackoverflow.com/questions/64868341/interacting-with-ssh-keygen-through-c-sharp
                // now I can not interacting with toplip.
                // because Process class can not capture after input response event.
                // so may be sleep 100ms wait toplip generate key.
                // but first standard input line always effect.
                // so please use just one password. :)
                foreach (var password in Builder.Context.PasswordList)
                {
                    Process.StandardInput.WriteLine(password);
                    Thread.Sleep(TimeSpan.FromMilliseconds(100));
                }

                Process.WaitForExit();

                using (var stream = File.Create(Context.OutputFilePath))
                {
                    Process.StandardOutput.BaseStream.CopyTo(stream);
                    stream.Flush();
                }
            }
        }

        public void Stop()
        {
            try
            {
                // try get process status , throw InvalidOperationException say process not run
                var _ = Process.Id;
            }
            catch (InvalidOperationException)
            {
                return;
            }

            if (Process.HasExited)
            {
                return;
            }
            Process.Kill();
            Process.Dispose();
        }
    }
}
