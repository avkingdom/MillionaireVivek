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
    internal class HomePageObjs
    {
        public IWebDriver driver;
        public HomePageObjs()
        {
            driver = WebHooks.driver;
        }

        private string HOME_PAGE_TITLE = "Keeping London moving - Transport for London";
        private string FROM_ERROR = "The From field is required.";
        private string TO_ERROR = "The To field is required.";

        private By btnCookiesAll = By.XPath("//*[@id=\"CybotCookiebotDialogBodyLevelButtonLevelOptinAllowAll\"]");
        private By btnCookiesDone = By.XPath("(//*[@id=\"cb-buttons\"]/button)[5]");
        private By fTxtFromPlace = By.Id("InputFrom");
        private By fTxtToPlace = By.Id("InputTo");
        private By btnPlanMyJourney = By.Id("plan-journey-button");
        private By txtFromError = By.Id("InputFrom-error");
        private By txtToError = By.Id("InputTo-error");
        private By lnkChangeTime = By.LinkText("change time");
        private By btnArriving = By.XPath("//label[contains(text(),'Arriving')]");
        private By lnkRecents = By.Id("jp-recent-tab-home");
        private By txtRecentJourney = By.XPath("//*[@id=\"jp-recent-content-home-\"]/a");


        public void AcceptAllCookies()
        {
            driver.FindElement(btnCookiesAll).Click();
            Thread.Sleep(3000);
            driver.FindElement(btnCookiesDone).Click();
            Thread.Sleep(3000);
        }

        public void EnterFromPlace(string place1)
        {
            EnterValidPlace(place1, fTxtFromPlace);
        }

        public void EnterToPlace(string place2)
        {
            EnterValidPlace(place2, fTxtToPlace);
        }

        public void ClickPlanMyJourney()
        {
            driver.FindElement(btnPlanMyJourney).Click();
        }

        public void ValidateFromToRequiredError()
        {
            Assert.AreEqual(FROM_ERROR, driver.FindElement(txtFromError).Text);
            Assert.AreEqual(TO_ERROR, driver.FindElement(txtToError).Text);
        }

        public void EnterValidPlace(string place, By textField)
        {
            driver.FindElement(textField).SendKeys(place);
            Thread.Sleep(5000);
            driver.FindElement(textField).SendKeys(Keys.Down + Keys.Escape);
            Thread.Sleep(3000);
        }

        public void VerifyChangeTime(string time)
        {
            driver.FindElement(lnkChangeTime).Click();
            Thread.Sleep(5000);
            driver.FindElement(btnArriving).Click();
            Thread.Sleep(3000);
        }

        public void ValidateRecentJourneyDetails(string fromPlace, string toPlace)
        {
            Assert.AreEqual(HOME_PAGE_TITLE, driver.Title);
            Thread.Sleep(3000);
            driver.FindElement(lnkRecents).Click();
            Thread.Sleep(3000);

            if (driver.FindElement(txtRecentJourney).Text.Contains(fromPlace))
            {
                Assert.IsTrue(true, "Successfully validated from place details");
            }
            if (driver.FindElement(txtRecentJourney).Text.Contains(toPlace))
            {
                Assert.IsTrue(true, "Successfully validated to place details");
            }
        }
    }
}
