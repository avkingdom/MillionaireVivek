Feature: ValidateJourneyPlanner

This feature file contains positive and negative scenarios to test the functionality of Plan-A-Journey module.
@tag1
Scenario: Test1 Verify valid journey can be planned with two valid places
	Given user navigated to the TFL application
	When user enters two valid places "Stanford University Centre, High Street, Oxford, UK" and "Stanford-le-Hope Rail Station"
	And clicks on Plan my Journey
	Then journey is "planned" successfully

@tag1
Scenario: Test2 Verify valid journey can not be planned with invalid from place
	Given user navigated to the TFL application
	When user enters two valid places "ALJKHDKJAGDK185315928713721" and "Stanford-le-Hope Rail Station"
	And clicks on Plan my Journey
	Then journey is "not planned" successfully

@tag1
Scenario: Test3 Verify valid journey can not be planned with invalid to place
	Given user navigated to the TFL application
	When user enters two valid places "Stanford-le-Hope Rail Station" and "ALJKHDKJAGDK185315928713721"
	And clicks on Plan my Journey
	Then journey is "not planned" successfully

@tag1
Scenario: Test4 Verify valid journey can not be planned with two invalid places
	Given user navigated to the TFL application
	When user enters two valid places "ALJKHDKJAGDK18531598713721" and "AKLJKHDKJAGDK18531598723213"
	And clicks on Plan my Journey
	Then journey is "not planned" successfully

@tag1
Scenario: Test5 Verify valid journey can not be planned when From and To fields are blank
	Given user navigated to the TFL application
	When user enters two valid places "" and ""
	And clicks on Plan my Journey
	Then user failed to plan the journey 

@tag1
Scenario: Test6 Verify valid journey can be planned based on arrival timing
	Given user navigated to the TFL application
	When user enters two valid places "Stanford University Centre, High Street, Oxford, UK" and "Stanford-le-Hope Rail Station"
	And user selects "arrival" time
	And clicks on Plan my Journey
	Then journey is planned successfully based on "Arrival" time

@tag1
Scenario: Test7 Verify valid journey can be edited and new journey can be planned after the edit
	Given user navigated to the TFL application
	When user enters two valid places "Stanford University Centre, High Street, Oxford, UK" and "Stanford-le-Hope Rail Station"
	And clicks on Plan my Journey
	And user edits the planned to journey with To place as "Oxford Circus Underground Station"
	Then journey is "planned" successfully with newly updated place

@tag1
Scenario: Test8 Verify recent section displays recently planned journey
	Given user navigated to the TFL application
	When user enters two valid places "Stanford University Centre, High Street, Oxford, UK" and "Stanford-le-Hope Rail Station"
	And clicks on Plan my Journey
	And navigates back to home page
	Then recent section displays latest planned journey