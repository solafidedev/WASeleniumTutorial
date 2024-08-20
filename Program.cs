using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

internal class Program
{
    private static void Main(string[] args)
    {
        var options = new ChromeOptions();
        string chromeUserData = "--user-data-dir=C:\\Users\\solaf\\AppData\\Local\\Google\\Chrome\\User Data\\Default";
        //string chromeUserData = "--user-data-dir=C:\\Users\\solaf\\AppData\\Local\\Google\\Chrome\\User Data"; Jika chrome tidak memiliki profile sama sekali
        options.AddArgument(chromeUserData);
        string chromeDriverPath = Path.Combine(Directory.GetCurrentDirectory(),"chromedriver");
        Console.WriteLine("Chrome Path : {0}", chromeDriverPath);
        using (IWebDriver driver = new ChromeDriver(chromeDriverPath, options))
        {
            try
            {

                driver.Navigate().GoToUrl("https://web.whatsapp.com/");
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMinutes(60);
                //berikan delay, disesuaikan dengan berapa lama WA web load data anda, atau jika belum login, bisa melakukan scan di sini
                Thread.Sleep(10000);
                
                //nama element pencarian contact
                string searchBox = "//div[@aria-label='Search' and @class='x1hx0egp x6ikm8r x1odjw0f x6prxxf x1k6rcq7 x1whj5v']";

                //deklarasi element pencarian contact
                IWebElement searchBoxElement = driver.FindElement(By.XPath(searchBox));

                //nama penerima, bisa perorangan atau group yang sudah terdaftar di contact WA
                string recipient = "Test WA"; 
                
                //eksekusi untuk mencari nama penerima di list contact WA
                searchBoxElement.SendKeys(recipient); 
                //beri delay untuk melakukan pencarian
                Thread.Sleep(3000); 

                //nama element hasil pencarian
                string selectSearchResult = string.Format("//span[@title='{0}']/parent::div[@class='_ak8q']", recipient);
                //deklarasi element hasil pencarian
                IWebElement searchResultElement = driver.FindElement(By.XPath(selectSearchResult));
                //perintahkan kursor untuk melakukan click pada element tersebut
                searchResultElement.Click();

                //nama element untuk menulis pesan, perlu penyesuaian dengan bahasa (indonesia atau inggris) yang digunakan pada atribut @aria-label
                string textBoxMessage = "//div[@aria-label='Type a message' and @class='x1hx0egp x6ikm8r x1odjw0f x1k6rcq7 x6prxxf' and @role='textbox']";
                //deklarasi element kotak pesan
                IWebElement textBoxMessageElement = driver.FindElement(By.XPath(textBoxMessage));

                //pesan yg akan di kirim ke penerima, bisa disesuaikan sesuai kebutuhan masing-masing
                string message = "test mengirim pesan";

                //perintahkan untuk menulis pesan pada kotak pesan, pesan kita akan otomatis tertulis
                textBoxMessageElement.SendKeys(message);
                //perintah untuk menkan tombol enter, untuk mengirim pesan kita
                textBoxMessageElement.SendKeys(Keys.Enter);

                //berikan delay
                Thread.Sleep(1000);

                //perintah untuk keluar dari chrome jika proses sudah slesai
                driver.Quit();
            }
            catch (Exception err)
            {
                Console.WriteLine(err);
                driver.Quit();
            }
        }
    }
}