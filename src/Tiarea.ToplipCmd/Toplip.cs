using System;
using System.Threading;
using System.Threading.Tasks;

namespace Tiarea.ToplipCmd
{
    public sealed class Toplip
    {
        /// <summary>
        /// file encrypt
        /// </summary>
        /// <param name="filePath">wait encrypt file path,include file name.</param>
        /// <param name="outputPath">output encrypt file path</param>
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

            await Task.Run(() => executer.Execute(), cancelToken);
        }

        /// <summary>
        /// 文件解密
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="password">密码</param>
        /// <param name="token"></param>
        /// <returns>解密文件路径</returns>
        public static Task<string> DecryptAsync(string filePath, string password, CancellationToken? cancelToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 文件隐藏至图片
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="imageFilePath"></param>
        /// <param name="password"></param>
        /// <param name="cancelToken"></param>
        /// <returns></returns>
        public static Task<string> HiddenFileInImageAsync(string filePath, string imageFilePath, string password, CancellationToken? cancelToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 从图片中获取图片
        /// </summary>
        /// <param name="imageFilePath"></param>
        /// <param name="password"></param>
        /// <param name="cancelToken"></param>
        /// <returns></returns>
        public static Task<string> ExtractFileFromImage(string imageFilePath, string password, CancellationToken? cancelToken)
        {
            throw new NotImplementedException();
        }

    }
}
