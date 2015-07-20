using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ADB.SA.Reports.Configuration;
using ADB.SA.Reports.Data;
using ADB.SA.Reports.Entities.DTO;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using System.Threading;
using System.IO;
using System.Drawing.Imaging;


namespace ADB.SA.Reports.BrokenPageTester
{
    class Program
    {
        static void Main(string[] args)
        {
            NavigationData data = new NavigationData();

            MenuFilterSection menu = MenuFilterSection.GetConfig();
            List<string> ids = menu.GetItemsToBeRemove();

            string filter = string.Join(",", ids.ToArray());
            MenuOrderSection menuOrder = MenuOrderSection.GetConfig();
            List<SAMenuItemDTO> menuItems = data.GetAllUsedDiagrams(filter);

            List<string> errorUrls = new List<string>();

            foreach (var item in menuItems)
            {
                foreach (var item2 in item.ChildItems)
                {
                    InternetExplorerDriver driver = new InternetExplorerDriver();
                    
                    string url = string.Format("http://wpsa1/tobe_ea/Default.aspx?id={0}", item2.Key);
                    driver.Url = url;


                    var sc = driver.GetScreenshot();

                    sc.SaveAsFile(item2.Key.ToString() + ".jpg", ImageFormat.Jpeg);
                    //Thread.Sleep(100000);

                    var x = driver.FindElementById("reportId");
                    if (x == null)
                    {
                        errorUrls.Add(url);
                    }
                    driver.Quit();
                }
            }

            File.WriteAllLines(@"BrokenPages.txt", errorUrls.ToArray());

            Console.ReadLine();

        }
    }
}
