using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace Tiarea.ToplipCmd
{
    public class ToplipExecutor
    {
        public ToplipProcessBuilder Builder { get; set; }
        public ToplipContext Context { get; }
        public bool HasAborted { get; private set; }
        private Process process { get; }

        // lock is not a good idea to reslove throw exception problem
        // when process exit but excute function still run. 
        // when execute get lock, stop will pending.
        private static readonly object abortLock = new();

        public ToplipExecutor(ToplipProcessBuilder builder)
        {
            Builder = builder;
            Context = builder.Context;
            process = builder.Build();
        }

        public byte[] ExecuteGetBytes()
        {
            using (process)
            {
                Execute();
                lock (abortLock)
                {
                    if (HasAborted)
                    {
                        return null;
                    }

                    using var stream = new MemoryStream();
                    process.StandardOutput.BaseStream.CopyTo(stream);
                    return stream.ToArray();
                }
            }
        }

        public void ExecuteWriteToFile()
        {
            using (process)
            {
                Execute();
                CheckHasAbortedThenRun(() =>
                {
                    using var stream = File.Create(Context.OutputFilePath);
                    process.StandardOutput.BaseStream.CopyTo(stream);
                    stream.Flush();
                });
            }
        }

        private void Execute()
        {
            CheckHasAbortedThenRun(() =>
            {
                process.Start();
            });


            foreach (var password in Builder.Context.PasswordList)
            {
                CheckHasAbortedThenRun(() =>
                {
                    // https://stackoverflow.com/questions/64868341/interacting-with-ssh-keygen-through-c-sharp
                    // until now I can not interacting with toplip.
                    // because Process class can not capture after input response event.
                    // so may be sleep 100ms wait toplip generate key will effect.
                    // but first standard input line always effect.
                    // so please use just one password. :)
                    process.StandardInput.WriteLine(password);
                    Thread.Sleep(TimeSpan.FromMilliseconds(100));
                });
            }

            CheckHasAbortedThenRun(() =>
            {
                process.WaitForExit();
            });
        }

        private void CheckHasAbortedThenRun(Action action)
        {
            lock (abortLock)
            {
                if (HasAborted)
                {
                    return;
                }

                action.Invoke();
            }
        }

        public void Stop()
        {
            lock (abortLock)
            {
                HasAborted = true;

                try
                {
                    // try get process status,
                    // if it not run will throw InvalidOperationException
                    // https://docs.microsoft.com/zh-cn/dotnet/api/system.diagnostics.process.id?f1url=%3FappId%3DDev16IDEF1%26l%3DZH-CN%26k%3Dk(System.Diagnostics.Process.Id)%3Bk(DevLang-csharp)%26rd%3Dtrue&view=net-5.0#--
                    var _ = process.Id;
                }
                catch (InvalidOperationException)
                {
                    return;
                }

                if (process.HasExited)
                {
                    return;
                }

                process.Kill();
            }
        }
    }
}
