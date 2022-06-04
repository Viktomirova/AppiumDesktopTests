using OpenQA.Selenium.Appium.Windows;

namespace AppiumSummatorTests.Window
{
    public class WindowSummator
    {
        private readonly WindowsDriver<WindowsElement> driver;

        public WindowSummator(WindowsDriver<WindowsElement> driver)
        {
            this.driver = driver;
        }

        public WindowsElement Num1Box => driver.FindElementByAccessibilityId("textBoxFirstNum");
        public WindowsElement Num2Box => driver.FindElementByAccessibilityId("textBoxSecondNum");
        public WindowsElement CalcButton => driver.FindElementByAccessibilityId("buttonCalc");
        public WindowsElement Result => driver.FindElementByAccessibilityId("textBoxSum");

        public string Calculate (string num1, string num2)
        {
            Num1Box.Clear();
            Num1Box.SendKeys(num1);

            Num2Box.Clear();
            Num2Box.SendKeys(num2);

            CalcButton.Click();

            return Result.Text;
        }
    }
}
