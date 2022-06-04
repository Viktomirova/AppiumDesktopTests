using NUnit.Framework;
using Appium7zipTests.Window;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Appium;
using System;
using System.IO;
using OpenQA.Selenium;
using System.Threading;

namespace Appium7zipTests.Tests
{
    public class Tests7zip
    {
        private const string AppiumServer = "http://127.0.0.1:4723/wd/hub";
        // Start the Appium server (to start with IPv6 type AppiumServer = "http://[::1]:4723/wd/hub";)
        private WindowsDriver<WindowsElement> driver;
        private WindowsDriver<WindowsElement> desktopDriver;
        private string workDir;

        public WindowsElement TextBoxLocation => driver.FindElementByXPath("/Window/Pane/Pane/ComboBox/Edit");
        public WindowsElement listBoxFiles => driver.FindElementByXPath("/Window/Pane/List");
        //public WindowsElement listBoxFiles => driver.FindElementByClassName("SysListView32"));
        //public WindowsElement listBoxFiles => driver.FindElementByAccessibilityId("1001");
        public WindowsElement ButtonAdd => driver.FindElementByName("Add");


        [OneTimeSetUp]
        public void OpenApp()
        {
            var options = new AppiumOptions() { PlatformName = "Windows" };
            options.AddAdditionalCapability("app", @"C:\Program Files\7-Zip\7zFM.exe");
            driver = new WindowsDriver<WindowsElement>(new Uri(AppiumServer), options);
            workDir = Directory.GetCurrentDirectory() + @"\workDir";
            if (Directory.Exists(workDir))
            {
                Directory.Delete(workDir, true);
            }
            Directory.CreateDirectory(workDir);

            var desktopOptions = new AppiumOptions() { PlatformName = "Windows" };
            desktopOptions.AddAdditionalCapability("app", "Root");
            desktopDriver = new WindowsDriver<WindowsElement>(new Uri(AppiumServer), desktopOptions);

        }

        [OneTimeTearDown]
        public void ShutDown()
        {
            driver.Quit();
            desktopDriver.Quit();
        }

        [Test]
        public void Test1_7zip()
        {
            TextBoxLocation.SendKeys(@"C:\Program Files\7-Zip\");
            TextBoxLocation.SendKeys(Keys.Enter);
            listBoxFiles.SendKeys(Keys.Control + 'a');
            ButtonAdd.Click();
            Thread.Sleep(500);

            var windowAddToArchive = desktopDriver.FindElementByName("Add to Archive");
            var textBoxArchiveName = windowAddToArchive.FindElementByXPath("/Window/ComboBox/Edit[@Name='Archive:']");
            string archiveFileName = workDir + "\\" + DateTime.Now.Ticks + ".7z";
            textBoxArchiveName.SendKeys(archiveFileName);
            var comboArchiveFormat = windowAddToArchive.FindElementByXPath("/Window/ComboBox[@Name='Archive format:']");
            comboArchiveFormat.SendKeys("7z");
            var comboCompressionLevel = windowAddToArchive.FindElementByXPath("/Window/ComboBox[@Name='Compression level:']");
            comboCompressionLevel.SendKeys("7 - Maximum");
            var okButton = windowAddToArchive.FindElementByXPath("/Window/Button[@Name='OK']");
            okButton.Click();
            Thread.Sleep(2000);

            TextBoxLocation.SendKeys(archiveFileName + Keys.Enter);
            var buttonExtract = driver.FindElementByName("Extract");
            buttonExtract.Click();

            var buttonExtractOK = driver.FindElementByName("OK");
            buttonExtractOK.Click();

            Thread.Sleep(1000);

            var executable7zipOriginal = @"C:\Program Files\7-Zip\7zFM.exe";
            var executable7zipExtracted = workDir + @"\7zFM.exe";
            FileAssert.AreEqual(executable7zipOriginal, executable7zipExtracted);

        }
    }
}
