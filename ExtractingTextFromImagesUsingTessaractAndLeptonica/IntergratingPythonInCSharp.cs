using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Diagnostics;
using Tesseract;

namespace test
{
    public class IntergratingPythonInCSharp
    {
        public static string? CroppedFilename;
        static string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            char[] stringChars = new char[length];

            for (int i = 0; i < length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            return new string(stringChars);
        }

        static void Takingscreenshot(string filename)
        {
            IWebDriver driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("http://www.ideunom.ac.in/");
            Thread.Sleep(2000);
            //IWebElement element = driver.FindElement(By.TagName("img"));
            //((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", element);
            Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();
            screenshot.SaveAsFile($"{filename}.png");
            driver.Quit();
        }
        static void interation(string filename)
        {
            #region without using any external package
            // Creating and intializing Process Info
            var psi = new ProcessStartInfo();
            psi.FileName = @"C:\Users\anton\source\repos\KickStartSelenium\ExtractingTextFromImagesUsingTessaractAndLeptonica\CropEnv\Scripts\Python.exe";
            
            // Providing python script and passing the required arguments
            var script = @"C:\Users\anton\source\repos\KickStartSelenium\ExtractingTextFromImagesUsingTessaractAndLeptonica\ImageCrop.py";
            psi.Arguments = $"\"{script}\" \"{filename}\"";

            // Process configuration
            psi.UseShellExecute = false;
            psi.CreateNoWindow = true;
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardError = true;

            // Executing the python script and storing the output 
            var errors = "";
            var results = "";

            using (var process = Process.Start(psi))
            {
                errors = process.StandardError.ReadToEnd();
                results = process.StandardOutput.ReadToEnd();


            }

            // Diplaying the python script output
            Console.WriteLine("ERRORS:");
            Console.WriteLine(errors);
            Console.WriteLine();
            Console.WriteLine("Results");
            Console.WriteLine(results);
            CroppedFilename = results.Split("\r\n")[0];
            #endregion

        }



        static void ExtractTextFromImage(string CroppedFilename)
        {
            var testImagePath = $"{CroppedFilename}.png";
            using (var engine = new TesseractEngine(@"./tessdata", "eng", EngineMode.Default))
            {
                using (var img2 = Pix.LoadFromFile(testImagePath))
                {
                    using (var page = engine.Process(img2))
                    {
                        var text = page.GetText();
                        Console.WriteLine(text);
                    }
                }
            }
            
        }
        static void Main(String[] args)
        {
            var filename = GenerateRandomString(10);
            Takingscreenshot(filename);
            interation(filename);
            ExtractTextFromImage(CroppedFilename!);
            
        }
    }
}
