using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;
using TFLProject.Hooks;
using TFLProject.PageObjects;

namespace TFLProject.StepDefinitions
{
    [Binding]
    public class ValidateJourneyPlannerStepDefinitions
    {
        public IWebDriver driver = WebHooks.driver;
        public string FromPlace = String.Empty;
        public string ToPlace = String.Empty;
        public string EditedToPlace = String.Empty;
        HomePageObjs _homePageObjs = new HomePageObjs();
        JourneyResultPageObjs _journeyResultPageObjs = new JourneyResultPageObjs();

        [Given(@"user navigated to the TFL application")]
        public void GivenUserNavigatedToTheTFLApplication()
        {
            driver.Navigate().GoToUrl("https://tfl.gov.uk/");
            _homePageObjs.AcceptAllCookies();
        }

        [When(@"user enters two valid places ""([^""]*)"" and ""([^""]*)""")]
        public void WhenUserEntersTwoValidPlacesAnd(string place1, string place2)
        {
            FromPlace = place1;
            ToPlace = place2;
            _homePageObjs.EnterFromPlace(FromPlace);
            _homePageObjs.EnterToPlace(ToPlace);
        }

        [When(@"clicks on Plan my Journey")]
        public void WhenClicksOnPlanMyJourney()
        {
            _homePageObjs.ClickPlanMyJourney();
        }
        
        [Then(@"journey is ""([^""]*)"" successfully")]
        public void ThenJourneyIsSuccessfully_(string status)
        {
            _journeyResultPageObjs.ValidateJourneyResult(FromPlace, ToPlace, status);
        }

        [Then(@"user failed to plan the journey")]
        public void ThenUserFailedToPlanTheJourney()
        {
            _homePageObjs.ValidateFromToRequiredError();
        }

        [When(@"user selects ""([^""]*)"" time")]
        public void WhenUserSelectsTime(string time)
        {
            _homePageObjs.VerifyChangeTime(time);
        }


        [Then(@"journey is planned successfully based on ""([^""]*)"" time")]
        public void ThenJourneyIsPlannedSuccessfullyBasedOnTime(string journeyType)
        {
            _journeyResultPageObjs.ValidateJourneyType(journeyType);
        }

        [When(@"user edits the planned to journey with To place as ""([^""]*)""")]
        public void WhenUserEditsThePlannedToJourneyWithToPlaceAs(string place)
        {
            EditedToPlace = place;
            _journeyResultPageObjs.ValidateEditingJourney(EditedToPlace);
        }

        [Then(@"journey is ""([^""]*)"" successfully with newly updated place")]
        public void ThenJourneyIsSuccessfullyWithNewlyUpdatedPlace(string status)
        {
            _journeyResultPageObjs.ValidateJourneyResult(FromPlace, EditedToPlace, status);
        }

        [When(@"navigates back to home page")]
        public void WhenNavigatesBackToHomePage()
        {
            _journeyResultPageObjs.ClickHomePageBtn();
        }

        [Then(@"recent section displays latest planned journey")]
        public void ThenRecentSectionDisplaysLatestPlannedJourney()
        {
            _homePageObjs.ValidateRecentJourneyDetails(FromPlace, ToPlace);
        }

    }
}
