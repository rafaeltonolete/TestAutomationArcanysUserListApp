using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;
using System.Threading;
using AutomatedTestArcanysSample.HelperClass;
using RestSharp;

namespace AutomatedTestArcanysSample
{

    public class TestArcanysSampleWebApp
    {

        public IWebDriver driver;


        [SetUp]
        public void InitializeTest()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();

        }

        

        [Test]
        public void SignUpUser()
        {

            driver.Navigate().GoToUrl("http://54.251.184.170:3030/");

            Thread.Sleep(10000);

            string passWrd = "P@ssw0Rd_" + Helper.GetRandomString(3, 3);

            //Enter Full Name
            driver.FindElement(By.XPath("//input[@id='inputName']")).Click();
            driver.FindElement(By.XPath("//input[@id='inputName']")).Clear();
            driver.FindElement(By.XPath("//input[@id='inputName']")).SendKeys("User"+ Helper.GetRandomString(3, 3)+  "LastName_"+ Helper.GetRandomString(2, 2));
            //Enter Email Address
            driver.FindElement(By.Id("inputEmail")).Click();
            driver.FindElement(By.XPath("//input[@id='inputEmail']")).Clear();
            driver.FindElement(By.XPath("//input[@id='inputEmail']")).SendKeys("emailADD_"+ Helper.GetRandomString(3, 3) + "@apps.com");
            //Enter Password
            driver.FindElement(By.Id("inputPassword")).Click();
            driver.FindElement(By.XPath("//input[@id='inputPassword']")).Clear();
            driver.FindElement(By.XPath("//input[@id='inputPassword']")).SendKeys(passWrd);
            //Re Enter Password
            driver.FindElement(By.Id("inputConfirmPassword")).Click();
            driver.FindElement(By.XPath("//input[@id='inputConfirmPassword']")).Clear();
            driver.FindElement(By.XPath("//input[@id='inputConfirmPassword']")).SendKeys(passWrd);
            //Click Sign Up button
            driver.FindElement(By.XPath("//button[@type='button']")).Click();

            //Wait for Page to Load
            Thread.Sleep(20000);
            WaitUntilElementIsPresent(driver, 10);




            var client = new RestClient("http://54.251.184.170:3030/api/users");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);







            //Click New User
            driver.FindElement(By.XPath("//input[@value='New User']")).Click();





        }

        public static bool WaitUntilElementIsPresent(IWebDriver driver, int timeout = 5)
        {
            for (var i = 0; i < timeout; i++)
            {
                if (driver.FindElement(By.XPath("//input[@value='New User']")).Displayed) return true;
            }
            return false;
        }


        [TearDown]
        public void CleanUp()
        {

            driver.Close();
            driver.Dispose();
        }


    }
}
