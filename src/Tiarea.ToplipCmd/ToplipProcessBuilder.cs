using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Tiarea.ToplipCmd
{
    public class ToplipProcessBuilder
    {
        public Process ToplipProcess { get; private set; }

        public ToplipContext Context { get; init; }

        public string ToplipPath { get; private set; }

        public ToplipProcessBuilder(ToplipOpType opType)
        {
            Context = new ToplipContext
            {
                OpType = opType
            };
        }

        public ToplipProcessBuilder AddPassword(string password)
        {
            if (Context.PasswordList == null)
            {
                Context.PasswordList = new List<string> { password };
            }
            else
            {
                Context.PasswordList.Add(password);
            }

            return this;
        }

        public ToplipProcessBuilder AddInputFilePath(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new ArgumentException("input file not exist.");
            }

            Context.InputFilePath = filePath;
            return this;
        }

        public ToplipProcessBuilder AddOutputFilePath(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentException("output filepath can not be null.");
            }
            Context.OutputFilePath = filePath;
            return this;
        }

        public ToplipProcessBuilder AddImageFilePath(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentException("image file path can not be null.");
            }
            return this;
        }

        public ToplipProcessBuilder SetToplipPath(string toplipPath)
        {
            if (!File.Exists(toplipPath))
            {
                throw new ArgumentException("toplip file not exist.");
            }
            ToplipPath = toplipPath;
            return this;
        }

        public Process Build()
        {
            if (Context.PasswordList == null || Context.PasswordList.Count < 1)
            {
                throw new ArgumentException("least one passowrd.");
            }

            if (string.IsNullOrWhiteSpace(Context.InputFilePath))
            {
                throw new ArgumentException("input file path can not be null or empty.");
            }

            if (string.IsNullOrWhiteSpace(ToplipPath))
            {
                ToplipPath = $"{AppDomain.CurrentDomain.BaseDirectory}/toplip";
            }

            var startInfo = new ProcessStartInfo
            {
                FileName = ToplipPath,
                RedirectStandardOutput = true,
                RedirectStandardInput = true,
                UseShellExecute = false,
                CreateNoWindow = true,
                Arguments = ArgumentsBuild()
            };

            return new Process { StartInfo = startInfo };
        }

        string ArgumentsBuild()
        {

            var builder = new StringBuilder();

            switch (Context.OpType)
            {
                case ToplipOpType.Decrypt:
                case ToplipOpType.ExtractFileFromImage:
                    builder.Append(" -d");
                    break;
                case ToplipOpType.HiddenFileInsideImage:
                    builder.Append($" -m {Context.ImgFilePath}");
                    break;
                case ToplipOpType.Encrypt:
                default:
                    break;
            }


            builder.Append($" -c {Context.PasswordCount} {Context.InputFilePath}");

            return builder.ToString();
        }
    }
}
