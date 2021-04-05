using System;

namespace WinScanProfile.IO {
    internal sealed class FileTypeProperty : Property {

        public FileTypeProperty(int id, int type, string value)
            : base(id, type, value) {
            Name = "File type";
            // {B96B3CAB-0728-11D3-9D7B-0000F81EF32E} BMP
            // {B96B3CAE-0728-11D3-9D7B-0000F81EF32E} JPG
            // {B96B3CAF-0728-11D3-9D7B-0000F81EF32E} PNG
            // {B96B3CB1-0728-11D3-9D7B-0000F81EF32E} TIF
        }

    }
}
