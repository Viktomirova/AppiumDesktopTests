
using NUnit.Framework;

using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Appium.Windows;

using System;

namespace AppiumSummatorTests.Variant1
{
    public class LocalSummatorTest
    {
        private WindowsDriver<WindowsElement> driver;
        private AppiumLocalService localservice;
        private AppiumOptions options;

        [OneTimeSetUp]
        public void OpenApp()
        {
            // Start the Appium server as a local Node.js app

            options = new AppiumOptions() { PlatformName = "Windows" };
            options.AddAdditionalCapability(MobileCapabilityType.App, @"C:\Users\Viktomirova\Desktop\SoftUni\QA\Automation\06.Appium-Desktop-Testing-Exercises-Resources\SummatorDesktopApp.exe");

            localservice = new AppiumServiceBuilder().UsingAnyFreePort().Build();
            localservice.Start();
            driver = new WindowsDriver<WindowsElement>(localservice, options);
        }
        [OneTimeTearDown]
        public void ShutDown()
        {
            driver.CloseApp();
            driver.Quit();
            localservice.Dispose();
        }

        [TestCase("4", "5", "9")]
        [TestCase("5", "15", "20")]
        public void Test1_SumTwoPositiveNumbers(string num1, string num2, string expected)
        {
            var num1Box = driver.FindElementByAccessibilityId("textBoxFirstNum");
            num1Box.Clear();
            num1Box.SendKeys(num1);

            var num2Box = driver.FindElementByAccessibilityId("textBoxSecondNum");
            num2Box.Clear();
            num2Box.SendKeys(num2);

            var calcButton = driver.FindElementByAccessibilityId("buttonCalc");
            calcButton.Click();

            var result = driver.FindElementByAccessibilityId("textBoxSum").Text;

            Assert.That(result, Is.EqualTo(expected));
        }

        [TestCase("4", "something", "error")]
        [TestCase("something", "15", "error")]
        public void Test1_SumInvalidValues(string num1, string num2, string expected)
        {
            var num1Box = driver.FindElementByAccessibilityId("textBoxFirstNum");
            num1Box.Clear();
            num1Box.SendKeys(num1);

            var num2Box = driver.FindElementByAccessibilityId("textBoxSecondNum");
            num2Box.Clear();
            num2Box.SendKeys(num2);

            var calcButton = driver.FindElementByAccessibilityId("buttonCalc");
            calcButton.Click();

            var result = driver.FindElementByAccessibilityId("textBoxSum").Text;

            Assert.That(result, Is.EqualTo(expected));
        }
    }
}