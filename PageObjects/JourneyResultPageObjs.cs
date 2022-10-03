using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TFLProject.Hooks;

namespace TFLProject.PageObjects
{
    internal class JourneyResultPageObjs
    {
        public IWebDriver driver;
        private string ExpectedFromPlace = string.Empty;
        private string ExpectedToPlace = string.Empty;
        private string ActualFromPlace = string.Empty;
        private string ActualToPlace = string.Empty;
        private string JOURNEY_TYPE_ARRIVING = "Arriving:";
        
        public JourneyResultPageObjs()
        {
            driver = WebHooks.driver;
        }

        private string JOURNEY_RESULT_PAGE_TITLE = "Journey results - Transport for London";
        private string INVALID_LOCATION_MSG = "Sorry, we can't find a journey matching your criteria";

        private By txtFromPlace = By.XPath("//*[@id=\"plan-a-journey\"]/div[1]/div[1]/div[1]/span[2]/strong");
        private By txtToPlace = By.XPath("//*[@id=\"plan-a-journey\"]/div[1]/div[1]/div[2]/span[2]/strong");
        private By txtJourneyType = By.XPath("//span[contains(text(),'Arriving:')]");
        private By lnkEditJourney = By.LinkText("Edit journey");
        private By fTxtToPlace = By.Id("InputTo");
        private By btnUpdateJourney = By.Id("plan-journey-button");
        private By lnkHomePage = By.LinkText("Home");

        public void ValidateJourneyResult(string place1, string place2, string status)
        {
            //Validate Page Title
            Assert.AreEqual(JOURNEY_RESULT_PAGE_TITLE, driver.Title, "Invalid result page");
            ActualFromPlace = driver.FindElement(txtFromPlace).Text;
            ActualToPlace = driver.FindElement(txtToPlace).Text;

            ExpectedFromPlace = place1;
            ExpectedToPlace = place2;

            Assert.AreEqual(ExpectedFromPlace, ActualFromPlace, "From place is not matching");
            Assert.AreEqual(ExpectedToPlace, ActualToPlace, "To place is not matching");
            Thread.Sleep(5000);

            switch (status)
            {
                case ("planned"):
                    {
                        Assert.IsFalse(driver.PageSource.Contains(INVALID_LOCATION_MSG));
                        break;
                    }
                case ("not planned"):
                    {
                        Assert.IsTrue(driver.PageSource.Contains(INVALID_LOCATION_MSG));
                        break;
                    }
                default:
                    break;
            }                        
        }

        public void ValidateJourneyType(string journeyType)
        {
            Assert.AreEqual(JOURNEY_RESULT_PAGE_TITLE, driver.Title, "Invalid result page");
            Thread.Sleep(5000);
            switch (journeyType)
            {
                case "Arrival":
                    {
                        Assert.AreEqual(JOURNEY_TYPE_ARRIVING, driver.FindElement(txtJourneyType).Text);
                        break;
                    }
            }
        }

        public void ValidateEditingJourney(string place)
        {
            Assert.AreEqual(JOURNEY_RESULT_PAGE_TITLE, driver.Title, "Invalid result page");
            driver.FindElement(lnkEditJourney).Click();
            Thread.Sleep(5000);
            driver.FindElement(fTxtToPlace).SendKeys(Keys.Control+ "a" + Keys.Delete);
            Thread.Sleep(5000);
            driver.FindElement(fTxtToPlace).SendKeys(place);
            driver.FindElement(btnUpdateJourney).Click();
        }

        public void ClickHomePageBtn()
        {
            Thread.Sleep(3000);
            driver.FindElement(lnkHomePage).Click();
            Thread.Sleep(5000);
        }
    }
}
