using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Threading;

namespace SeleniumTests2
{
    public class Tests
    {
        //Declared here for all methods to access it
        IWebDriver driver;

        [OneTimeSetUp]
        public void Setup()
        {
            //Initialised here
            driver = new ChromeDriver();
        }

        [Test]
        public void PageTitle()
        {
            driver.Navigate().GoToUrl("https://www.btc.ac.uk");
            string title = driver.Title;
            Assert.AreEqual(title, "Bridgwater & Taunton College, Education & Training in Somerset");
        }

        [Test]
        public void SearchAdultCourses()
        {
            //Open Website to be tested...
            driver.Navigate().GoToUrl("https://www.btc.ac.uk");

            //Find the Select Element...
            IWebElement courseFilter = driver.FindElement(By.Id("centre-select1"));
            //Create a new SelectElement...
            SelectElement select = new SelectElement(courseFilter);

            //In the select element, search for "Adult Learning"...
            select.SelectByText("Adult Learning");
            //Button on website has no ID or name, so the XPath is used to find the button within that form to click...
            IWebElement button = driver.FindElement(By.XPath("//form/button"));
            Thread.Sleep(10000); //Wait for search results to load...
          
            button.Click();
        }

        [Test]
        public void InvalidEmailTest()
        {
            driver.Navigate().GoToUrl("http://automationpractice.com/index.php");
            //Find the Sign In link and click it...
            IWebElement signInLink = driver.FindElement(By.ClassName("login"));
            signInLink.Click();

            //Find the email field and send a fake address...
            IWebElement emailAddress = driver.FindElement(By.Name("email_create"));
            emailAddress.SendKeys("fakeasf");

            //Find and enter the submit button...
            IWebElement createButton = driver.FindElement(By.Name("SubmitCreate"));
            createButton.Click();

            Thread.Sleep(10000);

            //Find the error message when a fake email is entered...
            IWebElement errorMessage = driver.FindElement(By.XPath("//ol/li[1]"));
            Assert.AreEqual(errorMessage.Text, "Invalid email address.");
        }

        [OneTimeTearDown]
        public void End()
        {
            driver.Close();
        }
    }
}