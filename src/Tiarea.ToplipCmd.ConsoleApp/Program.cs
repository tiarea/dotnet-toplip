using Sharprompt;
using System;

namespace Tiarea.ToplipCmd.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var opType = Prompt.Select<ToplipOpType>("Please select operation type.");
            var inputPath = Prompt.Input<string>("Please input the source file path.");
            var outputPath = Prompt.Input<string>("Please input the output file path.");
            var password = Prompt.Password("Type new password.");
            
            var builder = new ToplipProcessBuilder(opType)
              .AddPassword(password)
              .AddInputFilePath(inputPath)
              .AddOutputFilePath(outputPath);
            var executer = new ToplipExecutor(builder);
            executer.ExecuteWriteToFile();
            Console.WriteLine($"{opType} Completed.");
        }
    }
}
