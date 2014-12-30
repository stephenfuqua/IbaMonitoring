Feature: SiteConditions
	As an IBA observer
	I want to record the conditions for a site visit 
	so that I can submit point count observations.



Scenario: Happy Path
	Given I can choose from these sites:
		| Site Name   |
		| Moria       |
		| Lothlorien  |
		| Morgul Vale |
	And I can choose from these observers and recorders:
		| First  | Last    |
		| Gollum | Smeagol |
		| Drogo  | Baggins |
		| Gaffer | Gamgee  |
	And I can choose from these sky conditions:
		| Value | Sky    |
		| 0     | Dark   |
		| 1     | Light  |
		| 2     | Dismal |
		| 3     | Gloomy |
	And I can choose from these wind conditions:
		| Value | Wind       |
		| 0     | None       |
		| 1     | Refreshing |
		| 2     | Stiff      |
		| 3     | Tornado    |
	And I have entered these values into the form:
		| Field       | Value         |
		| SiteVisited | Morgul Vale   |
		| VisitDate   | 2014-12-29    |
		| Observer    | Drogo Baggins |
		| Recorder    | Gaffer Gamgee |
		| StartTime   | 6:02          |
		| StartSky    | Dark          |
		| StartWind   | Stiff         |
		| StartTemp   | 5             |
		| StartUnit   | C             |
		| EndTime     | 8:29          |
		| EndSky      | Dismal        |
		| EndWind     | Tornado       |
		| EndTemp     | 6             |
		| EndUnit     | C             |
		| IsValid     | true          |
	When I press Next
	Then I will be directed to the 'PointCounts' page
	And the form data will be saved into the database
