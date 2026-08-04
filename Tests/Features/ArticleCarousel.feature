Feature: Insights article carousel

  Scenario Outline: Opened article matches the carousel article after swiping
    Given I am on the home page
    When I click the Insights button
    And I swipe the carousel <SwipeCount> times
    And I note the name of the currently displayed article
    And I click the Read More button for the current article
    Then the opened article name should contain the noted article name

    Examples:
      | SwipeCount |
      |          0 |
      |          1 |
      |          2 |
      |          3 |
      |          4 |
