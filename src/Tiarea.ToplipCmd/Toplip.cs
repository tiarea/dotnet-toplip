using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Tiarea.ToplipCmd
{
    public sealed class Toplip
    {
        /// <summary>
        /// 文件加密
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="password">密码</param>
        /// <param name="cancelToken"></param>
        /// <returns>加密文件路径</returns>
        public static Task<string> EncryptAsync(string filePath, string password, CancellationToken? cancelToken)
        {
            throw new NotImplementedException();
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
