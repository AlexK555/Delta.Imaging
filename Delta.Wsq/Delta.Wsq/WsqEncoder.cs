using System;
using SD = System.Drawing;
using SDI = System.Drawing;

namespace Delta.Wsq
{
    public class WsqEncoder
    {
        public string Comment { get; set; }

        public byte[] EncodeQuality(RawImageData image, int quality)
        {
            return EncodeCompressionRatio(image, WsqCodec.QualityToCompressionRatio(quality));
        }

        public byte[] EncodeCompressionRatio(RawImageData image, float compressionRatio)
        {
            return Encode(image, WsqCodec.CompressionRatioToBitrate(compressionRatio));
        }

        public byte[] Encode(RawImageData image, float bitrate = WsqCodec.Constants.DefaultBitrate)
        {
            return WsqCodec.Encode(image, bitrate, Comment);
        }

        #region GDI+

        public byte[] EncodeQualityGdi(SD.Bitmap image, int quality, bool autoConvertToGrayscale = true)
        {
            return EncodeCompressionRatioGdi(image, WsqCodec.QualityToCompressionRatio(quality), autoConvertToGrayscale);
        }

        public byte[] EncodeCompressionRatioGdi(SD.Bitmap image, float compressionRatio, bool autoConvertToGrayscale = true)
        {
            return EncodeGdi(image, WsqCodec.CompressionRatioToBitrate(compressionRatio), autoConvertToGrayscale);
        }

        public byte[] EncodeGdi(SD.Bitmap image, float bitrate = WsqCodec.Constants.DefaultBitrate, bool autoConvertToGrayscale = true)
        {
            if (image == null) throw new ArgumentNullException("image");

            RawImageData data = null;
            if (autoConvertToGrayscale)
            {
                using (var source = Conversions.To8bppBitmap(image))
                    data = Conversions.GdiImageToImageInfo(source);
            }
            else data = Conversions.GdiImageToImageInfo(image);

            return WsqCodec.Encode(data, bitrate, Comment);
        }

        #endregion

       
    }
}
