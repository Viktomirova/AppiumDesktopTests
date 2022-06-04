using AppiumSummatorTests.Window;

using NUnit.Framework;

using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;

using System;

namespace AppiumSummatorTests.Tests
{
    public class TestsSummator
    {       
        private WindowsDriver<WindowsElement> driver;
        private AppiumOptions options;
        private const string AppiumServer = "http://127.0.0.1:4723/wd/hub";

        [OneTimeSetUp]
        public void OpenApp()
        {
            // Start the Appium server (to start with IPv6 type AppiumServer = "http://[::1]:4723/wd/hub";)

            options = new AppiumOptions(){ PlatformName = "Windows"};
            options.AddAdditionalCapability(MobileCapabilityType.App, @"C:\Users\Viktomirova\Desktop\SoftUni\QA\Automation\06.Appium-Desktop-Testing-Exercises-Resources\SummatorDesktopApp.exe");
            driver = new WindowsDriver<WindowsElement>(new Uri(AppiumServer), options);
        }

        [OneTimeTearDown]
        public void ShutDown()
        {
            driver.CloseApp();
            driver.Quit();
        }

        [TestCase( "4", "5", "9" )]
        [TestCase( "5", "15", "20" )]
        [TestCase( "0", "10", "10" )]
        public void Test1_SumTwoNumbersPositive(string num1, string num2, string expected)
        {
            var window = new WindowSummator(driver);
            var result = window.Calculate(num1, num2 );
            
            Assert.That(result, Is.EqualTo(expected));
        }

        [TestCase( "-4", "-5", "-9" )]
        [TestCase( "-5", "-15", "-20" )]
        [TestCase( "5", "-15", "-10" )]
        public void Test2_SumTwoNumbersNegative(string num1, string num2, string expected)
        {
            var window = new WindowSummator(driver);
            var result = window.Calculate(num1, num2 );
            
            Assert.That(result, Is.EqualTo(expected));
        }

        [TestCase( "4", "something", "error" )]
        [TestCase( "something", "15", "error" )]
        [TestCase( "", "", "error" )]
        public void Test3_InvalidSum(string num1, string num2, string expected)
        {
            var window = new WindowSummator(driver);
            var result = window.Calculate(num1, num2 );
            
            Assert.That(result, Is.EqualTo(expected));
        }
    }
}
