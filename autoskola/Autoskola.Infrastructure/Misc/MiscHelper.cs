using Autoskola.Infrastructure;
using Autoskola.Infrastructure.Encryption;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Autoskola.Infrastructure.Misc
{
    public static class SIPrefix
    {
        private static List<SIPrefixInfo> _SIPrefixInfoList = new
            List<SIPrefixInfo>();

        static SIPrefix()
        {
            _SIPrefixInfoList = new List<SIPrefixInfo>();
            LoadSIPrefix();
        }

        public static List<SIPrefixInfo> SIPrefixInfoList
        {
            get
            {
                SIPrefixInfo[] siPrefixInfoList = new SIPrefixInfo[6];
                _SIPrefixInfoList.CopyTo(siPrefixInfoList);
                return siPrefixInfoList.ToList();
            }
        }

        private static void LoadSIPrefix()
        {
            _SIPrefixInfoList.AddRange(new SIPrefixInfo[]{
            new SIPrefixInfo() {Symbol = "Y", Prefix = "yotta", Example = 1000000000000000000000000.00M, ZeroLength = 24, ShortScaleName = "Septillion", LongScaleName = "Quadrillion"},
            new SIPrefixInfo() {Symbol = "Z", Prefix = "zetta", Example = 1000000000000000000000M, ZeroLength = 21, ShortScaleName = "Sextillion", LongScaleName = "Trilliard"},
            new SIPrefixInfo() {Symbol = "E", Prefix = "exa", Example = 1000000000000000000M, ZeroLength = 18, ShortScaleName = "Quintillion", LongScaleName = "Trillion"},
            new SIPrefixInfo() {Symbol = "P", Prefix = "peta", Example = 1000000000000000M, ZeroLength = 15, ShortScaleName = "Quadrillion", LongScaleName = "Billiard"},
            new SIPrefixInfo() {Symbol = "T", Prefix = "tera", Example = 1000000000000M, ZeroLength = 12, ShortScaleName = "Trillion", LongScaleName = "Billion"},
            new SIPrefixInfo() {Symbol = "G", Prefix = "giga", Example = 1000000000M, ZeroLength = 9, ShortScaleName = "Billion", LongScaleName = "Milliard"},
            new SIPrefixInfo() {Symbol = "M", Prefix = "mega", Example = 1000000M, ZeroLength = 6, ShortScaleName = "Million", LongScaleName = "Million"},
            new SIPrefixInfo() {Symbol = "K", Prefix = "kilo", Example = 1000M, ZeroLength = 3, ShortScaleName = "Thousand", LongScaleName = "Thousand"},
            new SIPrefixInfo() {Symbol = "h", Prefix = "hecto", Example = 100M, ZeroLength = 2, ShortScaleName = "Hundred", LongScaleName = "Hundred"},
            new SIPrefixInfo() {Symbol = "da", Prefix = "deca", Example = 10M, ZeroLength = 1, ShortScaleName = "Ten", LongScaleName = "Ten"},
            new SIPrefixInfo() {Symbol = "", Prefix = "", Example = 1M, ZeroLength = 0, ShortScaleName = "One", LongScaleName = "One"},
        });
        }

        public static SIPrefixInfo GetInfo(long amount, int decimals)
        {
            return GetInfo(Convert.ToDecimal(amount), decimals);
        }

        public static SIPrefixInfo GetInfo(decimal amount, int decimals)
        {
            SIPrefixInfo siPrefixInfo = null;
            decimal amountToTest = Math.Abs(amount);

            var amountLength = amountToTest.ToString("0").Length;
            if (amountLength < 3)
            {
                siPrefixInfo = _SIPrefixInfoList.Find(i => i.ZeroLength == amountLength).Clone() as SIPrefixInfo;
                siPrefixInfo.AmountWithPrefix = Math.Round(amount, decimals).ToString();

                return siPrefixInfo;
            }

            siPrefixInfo = _SIPrefixInfoList.Find(i => amountToTest > i.Example).Clone() as SIPrefixInfo;

            siPrefixInfo.AmountWithPrefix = Math.Round(
                amountToTest / Convert.ToDecimal(siPrefixInfo.Example), decimals).ToString()
                                            + siPrefixInfo.Symbol;

            return siPrefixInfo;
        }
    }

    public class SIPrefixInfo : ICloneable
    {
        public string Symbol { get; set; }
        public decimal Example { get; set; }
        public string Prefix { get; set; }
        public int ZeroLength { get; set; }
        public string ShortScaleName { get; set; }
        public string LongScaleName { get; set; }
        public string AmountWithPrefix { get; set; }

        public object Clone()
        {
            return new SIPrefixInfo()
            {
                Example = this.Example,
                LongScaleName = this.LongScaleName,
                ShortScaleName = this.ShortScaleName,
                Symbol = this.Symbol,
                Prefix = this.Prefix,
                ZeroLength = this.ZeroLength
            };

        }
    }
    public static class Helper
    {
        public static int? IsInt(this string value)
        {
            Int32 result;
            if (Int32.TryParse(value, out result))
            {
                return Convert.ToInt32(value);
            }
            return null;
        }
        public static bool? IsBool(this string value)
        {
            bool result;
            if (bool.TryParse(value, out result))
            {
                return Convert.ToBoolean(value);
            }
            return null;
        }
        public static string ImagePathFromExt(this string ext)
        {
            string d = string.Empty;
            ".jpg .jpeg .bmp .png".Split(' ').ToList().ForEach(f => { if (ext.Contains(f)) d = "~/Templates/Shared/FileTypes/ext_image.png"; });
            ".doc .docx".Split(' ').ToList().ForEach(f => { if (ext.Contains(f)) d = "~/Templates/Shared/FileTypes/ext_word.png"; });
            ".xls .xlsx".Split(' ').ToList().ForEach(f => { if (ext.Contains(f)) d = "~/Templates/Shared/FileTypes/ext_excel.png"; });
            ".mpeg .avi .flv".Split(' ').ToList().ForEach(f => { if (ext.Contains(f)) d = "~/Templates/Shared/FileTypes/ext_video.png"; });
            ".mp3 .vmw".Split(' ').ToList().ForEach(f => { if (ext.Contains(f)) d = "~/Templates/Shared/FileTypes/ext_audio.png"; });
            ".txt".Split(' ').ToList().ForEach(f => { if (ext.Contains(f)) d = "~/Templates/Shared/FileTypes/ext_text.png"; });
            ".pdf".Split(' ').ToList().ForEach(f => { if (ext.Contains(f)) d = "~/Templates/Shared/FileTypes/ext_pdf.png"; });
            return String.IsNullOrEmpty(d) ? "~/Templates/Shared/FileTypes/ext_default.png" : d;
        }
        public static bool IsString(this string value)
        {
            return !String.IsNullOrEmpty(value);
        }

        public static bool IsEmailValid(this string email)
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);

            return match.Success;
        }

        public static bool IsValidPassword(this string password)
        {
            Regex regex = new Regex(@"(?!^[0-9])(?!^[a-zA-Z]*$)^(.{5,15})$");
            Match match = regex.Match(password);

            return match.Success;

        }

        public static string RemoveHtml(this string html)
        {
            return Regex.Replace(html, "<.+?>", string.Empty).Replace(Environment.NewLine, "").Replace("\t", string.Empty);

        }

        public static Image ResizeImageInCenter(Image image)
        {
            return Images.ResizeImageInCenter(image, 800, 800);
        }

        public static string EncodeElements(this string value)
        {
            var eh = new EncryptionHelper();
            return eh.Encrypt(value);
        }
        public static string DencodeElements(this string value)
        {
            var eh = new EncryptionHelper();
            return eh.Decrypt(value);
        }
        public static bool IsDecodable(this string value)
        {
            try
            {
                EncryptionHelper eh = new EncryptionHelper();
                eh.Decrypt(value);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        //public static string ConvertBytesToFrendlyUnit(this long value)
        //{
        //    return Files.FilesHelper.FormatByteSize(value);
        //}

    }


    internal class Images
    {
        private static Image ResizeByWidth(Image Img, int NewWidth)
        {

            float PercentW = ((float)Img.Width / (float)NewWidth);

            Bitmap bmp = new Bitmap(NewWidth, Img.Height);
            Graphics g = Graphics.FromImage(bmp);

            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.DrawImage(Img, 0, 0, bmp.Width, bmp.Height);
            g.Dispose();

            return bmp;
        }
        private static Image ResizeByHeight(Image Img, int NewHeight)
        {

            float PercentH = ((float)Img.Height / (float)NewHeight);

            Bitmap bmp = new Bitmap(Img.Width, NewHeight);
            Graphics g = Graphics.FromImage(bmp);

            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.DrawImage(Img, 0, 0, bmp.Width, bmp.Height);
            g.Dispose();

            return bmp;
        }
        private static Image IzreziSlikuPravilno(Image img, int w, int h, int tmp)
        {
            using (img)
            {
                Bitmap newBitmap = new Bitmap(w, h);

                using (Graphics graphics = Graphics.FromImage(newBitmap))
                {
                    if (tmp == 1)
                    {

                        graphics.DrawImage(
                            img, 0, 0,
                            new Rectangle((img.Width / 2) - (w / 2), 0, newBitmap.Width, newBitmap.Height), GraphicsUnit.Pixel);
                        graphics.Flush();
                    }
                    else
                    {
                        graphics.DrawImage(
                           img, 0, 0,
                           new Rectangle(0, ((img.Height / 2) - (h / 2)), newBitmap.Width, newBitmap.Height), GraphicsUnit.Pixel);
                        graphics.Flush();
                    }
                }

                return newBitmap;
            }

        }
        public static Image ResizeImageInCenter(Image img, int noviWidth, int noviHeight)
        {

            float width = (float)img.Width;
            float height = (float)img.Height;

            float WidthF = 0;
            float heightF = 0;

            if (width >= noviWidth && height >= noviHeight)
            {
                float wPar = width / noviWidth;
                float hPar = height / noviHeight;

                if ((height / wPar) > noviHeight)
                {
                    WidthF = width / wPar;
                    heightF = height / wPar;
                }
                if ((width / hPar) >= noviWidth)
                {
                    heightF = height / hPar;
                    WidthF = width / hPar;
                }


            }
            if (width >= noviWidth && height < noviHeight)
            {
                WidthF = width * (noviHeight / height);
                heightF = noviHeight;
            }
            if (width < noviWidth && height >= noviHeight)
            {
                heightF = height * (noviWidth / width);
                WidthF = noviWidth;
            }
            if (width < noviWidth && height < noviHeight)
            {
                float wPar = noviWidth / width;
                float hPar = noviHeight / height;

                if ((width * hPar) > noviHeight)
                {
                    heightF = height * hPar;
                    WidthF = width * hPar;


                }
                if ((height * wPar) >= noviWidth)
                {

                    heightF = height * wPar;
                    WidthF = width * wPar;


                }


            }
            int hf = (int)Math.Round(heightF, 1);
            int wf = (int)Math.Round(WidthF, 1);

            img = ResizeByWidth(img, wf);
            img = ResizeByHeight(img, hf);

            if (WidthF > noviWidth)
            {
                img = IzreziSlikuPravilno(img, noviWidth, noviHeight, 1);
            }
            if (heightF > noviHeight)
            {
                img = IzreziSlikuPravilno(img, noviWidth, noviHeight, 2);
            }

            return img;

        }
    }
}
