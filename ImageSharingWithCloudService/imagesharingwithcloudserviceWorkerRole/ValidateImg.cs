using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;


namespace imagesharingwithcloudserviceWorkerRole
{
    class ValidateImg
    {
        public static bool ValidateImage(Stream inputImage)
        {
            bool flag = false;
            try
            {
                System.Drawing.Image img = System.Drawing.Image.FromStream(inputImage);
                if (img.RawFormat.Guid == System.Drawing.Imaging.ImageFormat.Jpeg.Guid)
                {
                    //do somthing send message
                    flag = true;
                }
            }
            catch
            {
                return false;
            }

            return flag;
        }
    }
}
