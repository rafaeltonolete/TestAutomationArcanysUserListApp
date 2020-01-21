using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;
using System.Threading;
using AutomatedTestArcanysSample.HelperClass;
using RestSharp;
using Octokit.Internal;
using System.Net;
using System.IO;

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

            string fullName = "User" + Helper.GetRandomString(3, 3) + "LastName_" + Helper.GetRandomString(2, 2);
            string passWrd = "P@ssw0Rd_" + Helper.GetRandomString(3, 3);
            string emailAddress = "emailADD_" + Helper.GetRandomString(3, 3) + "@apps.com";

            //Enter Full Name
            driver.FindElement(By.XPath("//input[@id='inputName']")).Click();
            driver.FindElement(By.XPath("//input[@id='inputName']")).Clear();
            driver.FindElement(By.XPath("//input[@id='inputName']")).SendKeys(fullName);
            //Enter Email Address
            driver.FindElement(By.Id("inputEmail")).Click();
            driver.FindElement(By.XPath("//input[@id='inputEmail']")).Clear();
            driver.FindElement(By.XPath("//input[@id='inputEmail']")).SendKeys(emailAddress);
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
            Helper.WaitUntilElementIsPresent(driver, By.XPath("//input[@value='New User']"), 10);


            


            //Click New User
            driver.FindElement(By.XPath("//input[@value='New User']")).Click();




            //Assertion with GET request to confirm added Sign Up User record

            string response = GETrequest("http://54.251.184.170:3030/api/users");

            Assert.IsTrue(response.Contains(fullName));
            Assert.IsTrue(response.Contains(emailAddress));
            Assert.IsTrue(response.Contains(passWrd));


        }








        public string GETrequest(string url)
        {
            try
            {
                string rt;

                WebRequest request = WebRequest.Create(url);

                WebResponse response = request.GetResponse();

                Stream dataStream = response.GetResponseStream();

                StreamReader reader = new StreamReader(dataStream);

                rt = reader.ReadToEnd();

                Console.WriteLine(rt);

                reader.Close();
                response.Close();

                return rt;
            }

            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }







        [TearDown]
        public void CleanUp()
        {

            driver.Close();
            driver.Dispose();
        }


    }
}
