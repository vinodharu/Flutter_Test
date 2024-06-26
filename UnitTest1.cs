using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;

namespace FlutterProject
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {

            String url = "http://localhost:61795/";
            ChromeDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl(url);
            driver.Manage().Window.Maximize();

            Thread.Sleep(7000);

            IWebElement AddEmployeeButton = driver.FindElement(By.Id("flt-semantic-node-7"));
            AddEmployeeButton.Click();


            IWebElement EmployeeName = driver.FindElement(By.CssSelector("input[aria-label='Employee name']"));
            string textToType = "Hello, Flutter!";
            ((IJavaScriptExecutor)driver).ExecuteScript($"arguments[0].value='{textToType}';", EmployeeName);
            EmployeeName.Click();
            string UserName = "Andrew";
            EmployeeName.SendKeys(UserName);

            IWebElement EmployeeRole = driver.FindElement(By.CssSelector("input[aria-label='Select role']"));
            EmployeeRole.Click();
            IWebElement Role = driver.FindElement(By.CssSelector("flt-semantics[aria-label='Flutter Developer']"));
            string SelectedEmployeeRole = Role.GetAttribute("aria-label");
            Role.Click();
            Thread.Sleep(3000);

            IWebElement TodayDateField = driver.FindElement(By.XPath("//flt-semantics[@id='flt-semantic-node-17']//input"));
            TodayDateField.Click();
            IWebElement TodayButton = driver.FindElement(By.CssSelector("flt-semantics[aria-label='Today']"));
            TodayButton.Click();
            IWebElement SaveButton = driver.FindElement(By.CssSelector("flt-semantics[aria-label='Save']"));           
            SaveButton.Click();

            Thread.Sleep(3000);

            IWebElement NoDateField = driver.FindElement(By.XPath("//flt-semantics[@id='flt-semantic-node-18']//input"));       
            NoDateField.Click();
            IWebElement AfterOneWeekButton = driver.FindElement(By.CssSelector("flt-semantics[aria-label='After 1 Week']"));
            AfterOneWeekButton.Click();
            IWebElement NoDateSaveButton = driver.FindElement(By.CssSelector("flt-semantics[aria-label='Save']"));
            NoDateSaveButton.Click();

            Thread.Sleep(3000);

            IWebElement AddButton = driver.FindElement(By.CssSelector("flt-semantics[aria-label='Add']"));
            AddButton.Click();

            Thread.Sleep(3000);

            //Validation

            IWebElement PreviousEmployees = driver.FindElement(By.Id("flt-semantic-node-197"));
            string PreviousEmployeeName = PreviousEmployees.GetAttribute("aria-label");
            string[] details = PreviousEmployeeName.Split(new string[] { "\r\n" }, StringSplitOptions.None);

            // Store the parts in separate strings
            string name = details[0];
            string jobTitle = details[1];
            string date = details[2];

            string formattedToday = DateTime.Today.ToString("dd,MMM yyyy");
            string formattedNextWeek = DateTime.Today.AddDays(7).ToString("dd,MMM yyyy");

            // Combine formatted dates with a dash
            string formattedRange = $"{formattedToday} - {formattedNextWeek}";

            // Validate each part using NUnit assertions
            Assert.AreEqual(UserName, name);
            Assert.AreEqual(SelectedEmployeeRole, jobTitle);
            Assert.AreEqual(formattedRange, date);

            driver.Close();
        }
    }
}