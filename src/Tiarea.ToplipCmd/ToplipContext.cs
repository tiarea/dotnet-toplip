using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Tiarea.ToplipCmd
{
    public class ToplipContext
    {
        /// <summary>
        /// 输入文件路径
        /// </summary>
        public string InputFilePath { set; get; }
        /// <summary>
        /// 输入文件扩展名
        /// </summary>
        public string InputFileExt { get { return Path.GetExtension(InputFileExt); } }
        /// <summary>
        /// 图片文件路径，通常在进行图片隐写时使用
        /// </summary>
        public string ImgFilePath { set; get; }
        /// <summary>
        /// 图片文件扩展名
        /// </summary>
        public string ImgFileExt { get { return Path.GetExtension(ImgFilePath); } }
        /// <summary>
        /// 输出问文件路径（包括文件名）
        /// </summary>
        public string OutputFilePath { set; get; }
        /// <summary>
        /// 密码列表
        /// </summary>
        public IList<string> PasswordList { set; get; }
        /// <summary>
        /// 密码数量
        /// </summary>
        public int PasswordCount { get { return PasswordList.Count(); } }

        /// <summary>
        /// 操作类型
        /// </summary>
        public ToplipOpType OpType { get; init; }
    }

    /// <summary>
    /// toplip操作类型
    /// </summary>
    public enum ToplipOpType
    {
        Encrypt,
        Decrypt,
        HiddenFileInsideImage,
        ExtractFileFromImage
    }
}
