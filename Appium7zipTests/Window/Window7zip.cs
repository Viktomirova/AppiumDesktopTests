using OpenQA.Selenium.Appium.Windows;

namespace Appium7zipTests.Window
{
    public class Window7zip
    {
        private readonly WindowsDriver<WindowsElement> driver;

        public Window7zip(WindowsDriver<WindowsElement> driver)
        {
            this.driver = driver;
        }

        public WindowsElement TextBoxLocation => driver.FindElementByXPath("/Window/Pane/Pane/ComboBox/Edit");
    }
}
