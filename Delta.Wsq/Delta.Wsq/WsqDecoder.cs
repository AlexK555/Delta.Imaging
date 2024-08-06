﻿using System;
using System.Collections.Generic;
using SD = System.Drawing;
using SWMI = System.Windows.Media.Imaging;
namespace Delta.Wsq
{
    public class WsqDecoder
    {
        public RawImageData Decode(byte[] data)
        {
            if (data == null || data.Length == 0) throw new ArgumentNullException("data");
            return WsqCodec.Decode(data);
        }

        public SD.Bitmap DecodeGdi(byte[] data)
        {
            if (data == null || data.Length == 0) throw new ArgumentNullException("data");
            var raw = Decode(data);
            return Conversions.ImageInfoToGdiImage(raw);
        }

        public SWMI.BitmapSource DecodeWpf(byte[] data)
        {
            if (data == null || data.Length == 0) throw new ArgumentNullException("data");
            var raw = Decode(data);
            return Conversions.ImageInfoToWpfImage(raw);
        }
    }
}
