using System;
using System.Threading;
using System.Threading.Tasks;

namespace Tiarea.ToplipCmd
{
    public sealed class Toplip
    {
        /// <summary>
        /// file encrypt and save to specific pos.
        /// </summary>
        /// <param name="filePath">wait encrypt file path,include file name.</param>
        /// <param name="outputPath">encrypted file path,include file name.</param>
        /// <param name="password">password</param>
        /// <param name="cancelToken"></param>
        /// <returns></returns>
        public static async Task EncryptAsync(string filePath,string outputPath, string password, CancellationToken cancelToken = default)
        {
            var builder = new ToplipProcessBuilder(ToplipOpType.Encrypt)
                .AddPassword(password)
                .AddInputFilePath(filePath)
                .AddOutputFilePath(outputPath);

            var executer = new ToplipExecutor(builder);

            cancelToken.Register((execr) => {
                ((ToplipExecutor)execr).Stop();
            }, state: executer);

            await Task.Run(() => executer.ExecuteWriteToFile(), cancelToken);
        }

        /// <summary>
        /// file decrypt and save to specific position.
        /// </summary>
        /// <param name="filePath">wait decrypt file path,include file name.</param>
        /// <param name="outputPath">decrypted file path,include file name.</param>
        /// <param name="password">password</param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static async Task DecryptAsync(string filePath,string outputPath, string password, CancellationToken cancelToken=default)
        {
            var builder = new ToplipProcessBuilder(ToplipOpType.Decrypt)
                .AddPassword(password)
                .AddInputFilePath(filePath)
                .AddOutputFilePath(outputPath);

            var executer = new ToplipExecutor(builder);

            cancelToken.Register((execr) => {
                ((ToplipExecutor)execr).Stop();
            }, state: executer);

            await Task.Run(() => executer.ExecuteWriteToFile(), cancelToken);
        }

        /// <summary>
        /// hideen file in image and save image to specific pos.
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="outputPath"></param> 
        /// <param name="imageFilePath"></param>
        /// <param name="password"></param>
        /// <param name="cancelToken"></param>
        /// <returns></returns>
        public static async Task HiddenFileInImageAsync(string filePath,string outputPath, string imageFilePath, string password, CancellationToken? cancelToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// extract file from image.
        /// </summary>
        /// <param name="imageFilePath"></param>
        /// <param name="password"></param>
        /// <param name="cancelToken"></param>
        /// <returns></returns>
        public static async Task ExtractFileFromImage(string imageFilePath,string outputFilePath, string password, CancellationToken? cancelToken)
        {
            throw new NotImplementedException();
        }

    }
}
