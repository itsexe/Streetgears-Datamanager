namespace SG_ResManagerLib
{
    public class SgFile
    {
        #region "Constructors"
        /// <summary>
        /// Creates a new instance without initializing any properties
        /// </summary>
        public SgFile() { }
        /// <summary>
        /// Inits a new instance of SgFile and sets values to all properties
        /// </summary>
        /// <param name="fileName">File Name</param>
        /// <param name="encodedFileName"></param>
        /// <param name="fileSize">File Size</param>
        /// <param name="offset"></param>
        public SgFile(string fileName, string encodedFileName, int fileSize, int offset)
        {
            FileName = fileName;
            EncodedFileName = encodedFileName;
            FileSize = fileSize;
            Offset = offset;
        }
        #endregion

        #region "Properties"
        //File name
        public string FileName { get; set; }
        //Encoded file name
        public string EncodedFileName { get; set; }
        //File size
        public int FileSize { get; set; }
        //Position in File
        public int Offset { get; set; }
        //File Number
        public int FileNumber
        {
            get { return FileHelper.GetFileNumber(EncodedFileName); }
        }
        #endregion

        #region "voids & functions"
        public bool IsEncrypted()
        {
            string ext = System.IO.Path.GetExtension(FileName).Replace(".", "").ToLower();
            if (ext == "bmp") return true;
            if (ext == "item") return true;
            if (ext == "jpg") return true;
            if (ext == "jtv") return true;
            if (ext == "lua") return true;
            if (ext == "map") return true;
            if (ext == "naf") return true;
            if (ext == "npc") return true;
            if (ext == "nui") return true;
            if (ext == "nus") return true;
            if (ext == "nx3") return true;
            if (ext == "rsg") return true;
            if (ext == "spr") return true;
            if (ext == "trick") return true;
            if (ext == "ttf") return true;
            if (ext == "txt") return true;
            if (ext == "wav") return true;

            if (ext == "tga") return false;
            if (ext == "dds") return false;
            if (ext == "ffe") return false;
            if (ext == "fx") return false;
            return true;
        }
        #endregion
    }
}
