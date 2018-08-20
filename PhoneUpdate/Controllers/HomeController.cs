using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace PhoneUpdate.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpPost]
        public ActionResult SavePicture(string picString)
        {
            var tmpArr = picString.Split(',');
            byte[] bytes = Convert.FromBase64String(tmpArr[1]);
            MemoryStream ms = new MemoryStream(bytes);
            ms.Write(bytes, 0, bytes.Length);


            var img = Image.FromStream(ms, true);

            var path = System.AppDomain.CurrentDomain.BaseDirectory;
            var imagesPath = System.IO.Path.Combine(path, @"Img\\");
            if (!System.IO.Directory.Exists(imagesPath))
                System.IO.Directory.CreateDirectory(imagesPath);
            string fileName = DateTime.Now.ToString("yyyyMMddHHmmss_ffff", System.Globalization.DateTimeFormatInfo.InvariantInfo) + ".jpg";

            var bitImage = img;
            bitImage.Save(imagesPath + fileName);
            bool isOk = true;
            string msg = imagesPath + fileName;
            return Json(new { suc = isOk, msg = msg });
        }
        public string GetResult()
        {
            try
            {
                var path = System.AppDomain.CurrentDomain.BaseDirectory;
                var imagesPath = System.IO.Path.Combine(path, @"Result\\result.txt");

                StreamReader sr = new StreamReader(imagesPath, Encoding.Default);
                String line;
                string result = "";
                while ((line = sr.ReadLine()) != null)
                {
                    result = line.ToString();
                }
                sr.Close();

                if (!string.IsNullOrEmpty(result))
                {
                    System.IO.File.WriteAllText(imagesPath, string.Empty);
                    //FileStream fs1 = null;
                    //try
                    //{

                    //    //fs1 = new FileStream(imagesPath, FileMode.Truncate, FileAccess.ReadWrite);
                    //}
                    //catch (Exception ex)
                    //{

                    //}
                    //finally
                    //{
                    //    fs1.Close();
                    //}
                }
                return result;
            }
            catch (Exception)
            {
                return "";
            }
        }
    }
}