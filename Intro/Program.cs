// See https://aka.ms/new-console-template for more information
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

//Console.WriteLine("Hello, World!");
namespace test
{
    public class test1
    {
        static void Main(String[] args)
        {
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://www.archery-electronics.com/en/");
            driver.Quit();
        }
    }
}