using Autoskola.Infrastructure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;

namespace Autoskola.Infrastructure.Misc
{
    public class MyHelper
    {
        public static string CreateUrlString(string value)
        {
            value = value.ToLower();
            string newValue = "";
            for(int i = 0; i < value.Length; i++)
            {
                bool isChange = false;
                switch(value[i])
                {
                    case 'č':
                    case 'ć':
                        newValue += 'c';
                        isChange = true;
                        break;
                    case 'đ':
                        newValue += 'd';
                        isChange = true;
                        break;
                    case 'ž':
                        newValue += 'z';
                        isChange = true;
                        break;
                    case 'š':
                        newValue += 's';
                        isChange = true;
                        break;
                    case ' ':
                    case '_':
                    case '-':
                        newValue += '-';
                        isChange = true;
                        break;
                }

                if (!isChange && (value[i] >= 97 && value[i] <= 122) || (value[i] >= 48 && value[i] <= 57))
                    newValue += value[i];
            }

            return newValue;
        }

        public static string RemoveHtmlTags(string value)
        {
            return Regex.Replace(value, "<[^>]*(>|$)", "");
        }

        public static DateTime GetDateTimeBosnia(DateTime dateTime)
        {
            return dateTime.AddHours(2);
            //TimeZoneInfo tz = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time");
            //return TimeZoneInfo.ConvertTime(dateTime, TimeZoneInfo.Local, tz);
        }

        public static string GetDayName(int day)
        {
            switch(day)
            {
                case 1:
                    return "Ponedjeljak";
                case 2:
                    return "Utorak";
                case 3:
                    return "Srijeda";
                case 4:
                    return "Četvrtak";
                case 5:
                    return "Petak";
                case 6:
                    return "Subota";
                case 7:
                    return "Nedjelja";
                default:
                    return "";
            }
        }

        public static string GetMonthName(int monthId)
        {
            switch (monthId)
            {
                case 1:
                    return "Januar";
                case 2:
                    return "Februar";
                case 3:
                    return "Mart";
                case 4:
                    return "April";
                case 5:
                    return "Maj";
                case 6:
                    return "Juni";
                case 7:
                    return "Juli";
                case 8:
                    return "August";
                case 9:
                    return "Septembar";
                case 10:
                    return "Oktobar";
                case 11:
                    return "Novembar";
                case 12:
                    return "Decembar";
                default:
                    return "";
            }
        }

        public static string Routing(string value)
        {
            string new_value = "";
            for (int i = 0; i < value.Length; i++)
            {
                char c = value[i];
                switch (c)
                {
                    case ' ':
                        new_value += "-";
                        break;
                    case '.':
                        new_value += "_";
                        break;
                    default:
                        new_value += c;
                        break;
                }
            }
            return new_value;
        }

        public static bool SendMail(string caption, string mailTo, string message)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.Subject = caption;
                mail.To.Add(mailTo);
                mail.From = new MailAddress("no-reply@medresamostar.com");

                mail.Body = message;
                mail.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient();
                smtp.Host = "mail.medresamostar.com";
                smtp.Port = 587;
                smtp.EnableSsl = false;
                smtp.Credentials = new System.Net.NetworkCredential
                     ("no-reply@medresamostar.com", "IhhC04RgCEId");
                smtp.Send(mail);

                return true;
            }
            catch (Exception ex) { return false; }
        }

        //public static void Support(Exception exception)
        //{
        //    if (exception.Message == "Thread was being aborted.")
        //    {
        //        return;
        //    }
        //    else
        //    {
        //        Configuration webConfig = WebConfigurationManager.OpenWebConfiguration("~/Web.config");
        //        string Email = System.Web.HttpUtility.HtmlDecode(webConfig.AppSettings.Settings["EmailSupport"].Value);

        //        string Message = "<b>" + exception.Message + "</b><br/><br/>" + exception.StackTrace + "<br/><br/>" + DateTime.Now;

        //        SendMail("KBM - Exception", Email, Message);
        //    }
        //}

        public static string ConvertForAbbr(string value)
        {
            return value.Replace("'", "");
        }

        public static string ParseToEnglishAlphabet(string value, bool withDash)
        {
            string new_value = "";
            for (int i = 0; i < value.Length; i++)
            {
                char c = value[i];
                if ((c >= 65 && c <= 90) || (c >= 97 && c <= 122))
                    new_value += c;
                else if (c == 32 && withDash)
                    new_value += "-";
                else
                {
                    switch (c)
                    {
                        case 'č':
                        case 'ć':
                            new_value += 'c';
                            break;
                        case 'ž':
                            new_value += 'z';
                            break;
                        case 'đ':
                            new_value += "dj";
                            break;
                        case ' ':
                            new_value += ' ';
                            break;
                    }
                }
            }
            return new_value;
        }

        public static string GetSubstring(string value, int length)
        {
            if (value.Length > length)
                return value.Substring(0, length) + "...";
            return value;
        }

        public static Image ByteArrayToImage(byte[] byteArrayIn)
        {
            Image returnImage = null;
            using (MemoryStream ms = new MemoryStream(byteArrayIn))
            {
                returnImage = Image.FromStream(ms);
            }
            return returnImage;
        }

        public static void CompressImage(Image sourceImage, int imageQuality, string savePath)
        {
            try
            {
                //Create an ImageCodecInfo-object for the codec information
                ImageCodecInfo jpegCodec = null;

                //Set quality factor for compression
                EncoderParameter imageQualitysParameter = new EncoderParameter(
                            System.Drawing.Imaging.Encoder.Quality, imageQuality);

                //List all avaible codecs (system wide)
                ImageCodecInfo[] alleCodecs = ImageCodecInfo.GetImageEncoders();

                EncoderParameters codecParameter = new EncoderParameters(1);
                codecParameter.Param[0] = imageQualitysParameter;

                //Find and choose JPEG codec
                for (int i = 0; i < alleCodecs.Length; i++)
                {
                    if (alleCodecs[i].MimeType == "image/jpeg")
                    {
                        jpegCodec = alleCodecs[i];
                        break;
                    }
                }

                //Save compressed image
                sourceImage.Save(HttpContext.Current.Server.MapPath(savePath), jpegCodec, codecParameter);
            }
            catch (Exception ex)
            {

            }
        }

        public static Image CropImg(Image img, Rectangle cropArea)
        {
            Bitmap b = new Bitmap(cropArea.Width, cropArea.Height);
            Graphics g = Graphics.FromImage((Image)b);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            g.DrawImage(img, 0, 0, cropArea.Width, cropArea.Height);
            g.Dispose();
            return (Image)b;
        }

        public static Image CropImage_v2(Image img, Rectangle cropArea)
        {
            Bitmap bmpImage = new Bitmap(img);
            Bitmap bmpCrop = bmpImage.Clone(cropArea,
            bmpImage.PixelFormat);
            return (Image)(bmpCrop);
        }

        public static Image ResizeImage(Image imgToResize, Size size)
        {
            if (imgToResize.Width > size.Width && imgToResize.Height > size.Height)
            {
                try
                {
                    int sourceWidth = imgToResize.Width;
                    int sourceHeight = imgToResize.Height;

                    float nPercent = 0;
                    float nPercentW = 0;
                    float nPercentH = 0;

                    nPercentW = ((float)size.Width / (float)sourceWidth);
                    nPercentH = ((float)size.Height / (float)sourceHeight);

                    if (nPercentH < nPercentW)
                        nPercent = nPercentH;
                    else
                        nPercent = nPercentW;

                    int destWidth = (int)(sourceWidth * nPercent);
                    int destHeight = (int)(sourceHeight * nPercent);

                    Bitmap b = new Bitmap(destWidth, destHeight);
                    Graphics g = Graphics.FromImage((Image)b);
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;

                    g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
                    g.Dispose();

                    return (Image)b;
                }
                catch (Exception ex)
                {

                }
                return null;
            }
            return imgToResize;
        }

        public static Image EditImage(Image image, Size picture)
        {
            try
            {
                if (image != null)
                {

                    if (image.Width > (picture.Width + 200))
                    {
                        Image img = ResizeImage(image, new Size(picture.Width + 100, picture.Height + 100));
                        if (img.Width >= picture.Width && img.Height >= picture.Height)
                            return CropImg(img, new Rectangle(0, 0, picture.Width, picture.Height));
                    }

                    if (image.Width >= picture.Width && image.Height >= picture.Height)
                    {
                        return CropImage_v2(image, new Rectangle(0, 0, picture.Width, picture.Height));

                    }
                    else if (image.Width < picture.Width)
                    {
                        return CropImage_v2(new Bitmap(image, new Size(picture.Width, image.Height)), new Rectangle(0, 0, picture.Width, picture.Height));
                    }
                    else if (image.Height < picture.Height)
                    {
                        return CropImage_v2(new Bitmap(image, new Size(image.Width, picture.Height)), new Rectangle(0, 0, picture.Width, picture.Height));
                    }

                    return new Bitmap(image, new Size(picture.Width, picture.Height));
                }
            }
            catch (Exception ex) { }
            return image;
        }

        public static void VaryQualityLevel(Image sourceImage, string savePath)
        {

            try
            {
                // Get a bitmap.
                Bitmap bmp1 = new Bitmap(sourceImage);
                ImageCodecInfo jgpEncoder = GetEncoder(ImageFormat.Jpeg);

                // Create an Encoder object based on the GUID 
                // for the Quality parameter category.
                System.Drawing.Imaging.Encoder myEncoder =
                    System.Drawing.Imaging.Encoder.Quality;

                // Create an EncoderParameters object. 
                // An EncoderParameters object has an array of EncoderParameter 
                // objects. In this case, there is only one 
                // EncoderParameter object in the array.
                EncoderParameters myEncoderParameters = new EncoderParameters(1);

                EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, 50L);
                myEncoderParameters.Param[0] = myEncoderParameter;
                bmp1.Save(savePath, jgpEncoder, myEncoderParameters);

                myEncoderParameter = new EncoderParameter(myEncoder, 100L);
                myEncoderParameters.Param[0] = myEncoderParameter;
                bmp1.Save(savePath + "TestPhotoQualityHundred.jpg", jgpEncoder, myEncoderParameters);

                // Save the bitmap as a JPG file with zero quality level compression.
                myEncoderParameter = new EncoderParameter(myEncoder, 0L);
                myEncoderParameters.Param[0] = myEncoderParameter;
                bmp1.Save(savePath + "TestPhotoQualityZero.jpg", jgpEncoder, myEncoderParameters);
            }
            catch (Exception ex)
            {

            }

        }

        public static ImageCodecInfo GetEncoder(ImageFormat format)
        {

            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();

            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }

        public static string ConvertToASCII(string value)
        {
            string new_value = "";
            for (int i = 0; i < value.Length; i++)
            {
                char c = value[i];
                new_value += (int)c;
            }
            return new_value;
        }

        public static Image FixedSize(Image imgPhoto, int Width, int Height)
        {
            int sourceWidth = imgPhoto.Width;
            int sourceHeight = imgPhoto.Height;
            int sourceX = 0;
            int sourceY = 0;
            int destX = 0;
            int destY = 0;

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;

            nPercentW = ((float)Width / (float)sourceWidth);
            nPercentH = ((float)Height / (float)sourceHeight);
            if (nPercentH < nPercentW)
            {
                nPercent = nPercentH;
                destX = System.Convert.ToInt16((Width -
                              (sourceWidth * nPercent)) / 2);
            }
            else
            {
                nPercent = nPercentW;
                destY = System.Convert.ToInt16((Height -
                              (sourceHeight * nPercent)) / 2);
            }

            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap bmPhoto = new Bitmap(Width, Height,
                              PixelFormat.Format24bppRgb);
            bmPhoto.SetResolution(imgPhoto.HorizontalResolution,
                             imgPhoto.VerticalResolution);

            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            grPhoto.Clear(Color.Gainsboro);
            grPhoto.InterpolationMode =
                    InterpolationMode.HighQualityBicubic;

            grPhoto.DrawImage(imgPhoto,
                new Rectangle(destX, destY, destWidth, destHeight),
                new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight),
                GraphicsUnit.Pixel);

            grPhoto.Dispose();
            return bmPhoto;
        }

        public static Image ScaleImageHeight(Image image, int maxHeight)
        {
            var ratio = (double)maxHeight / image.Height;

            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);

            var newImage = new Bitmap(newWidth, newHeight);
            using (var g = Graphics.FromImage(newImage))
            {
                g.DrawImage(image, 0, 0, newWidth, newHeight);
            }
            return newImage;
        }

        public static Image ScaleImageWidth(Image image, int maxWidth)
        {
            var ratio = (double)maxWidth / image.Width;

            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);

            var newImage = new Bitmap(newWidth, newHeight);
            using (var g = Graphics.FromImage(newImage))
            {
                g.DrawImage(image, 0, 0, newWidth, newHeight);
            }
            return newImage;
        }

        public static Image EditImage_v2(Bitmap bmpNew, Size size)
        {
            Image resizeImage = null;

            if (bmpNew.Height > (size.Height + 400))
                resizeImage = ScaleImageHeight(bmpNew, size.Height + 200);
            else if (bmpNew.Height > (size.Height + 100))
                resizeImage = ScaleImageHeight(bmpNew, size.Height + 100);

            if (resizeImage != null)
            {
                if (resizeImage.Width >= size.Width && resizeImage.Height >= size.Height)
                    return CropImage_v2(resizeImage, new Rectangle((resizeImage.Width - size.Width) / 2, (resizeImage.Height - size.Height) / 2, size.Width, size.Height));

                if (resizeImage.Width < size.Width)
                {
                    if (bmpNew.Width > (size.Width + 50))
                    {
                        Image rs = ScaleImageWidth(bmpNew, size.Width + 50);
                        return CropImage_v2(rs, new Rectangle(0, 0, size.Width, size.Height));
                    }
                }
            }

            if (bmpNew.Width >= size.Width && bmpNew.Height >= size.Height)
                return CropImage_v2(bmpNew, new Rectangle((bmpNew.Width - size.Width) / 2, (bmpNew.Height - size.Height) / 2, size.Width, size.Height));

            if (bmpNew.Width >= size.Width)
                return FixedSize(CropImage_v2(bmpNew, new Rectangle(0, 0, size.Width, bmpNew.Height)), size.Width, size.Height);

            if (bmpNew.Height >= size.Height)
                return FixedSize(CropImage_v2(bmpNew, new Rectangle(0, 0, bmpNew.Width, size.Height)), size.Width, size.Height);

            return FixedSize(bmpNew, size.Width, size.Height);
        }
    }
}
