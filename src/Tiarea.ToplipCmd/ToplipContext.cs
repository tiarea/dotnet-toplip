using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Tiarea.ToplipCmd
{
    public class ToplipContext
    {
        public string InputFilePath { set; get; }
        public string InputFileExt { get { return Path.GetExtension(InputFileExt); } }
        public string ImgFilePath { set; get; }
        public string ImgFileExt { get { return Path.GetExtension(ImgFilePath); } }
        public string OutputFilePath { set; get; }
        public IList<string> PasswordList { set; get; }
        public int PasswordCount { get { return PasswordList.Count(); } }
        public ToplipOpType OpType { get; init; }
    }

    /// <summary>
    /// define toplip operation type
    /// </summary>
    public enum ToplipOpType
    {
        Encrypt,
        Decrypt,
        HiddenFileInImage,
        ExtractFileFromImage
    }
}
