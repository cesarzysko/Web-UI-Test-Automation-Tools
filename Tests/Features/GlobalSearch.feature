Feature: Global site search

  Scenario Outline: All global search results contain the searched word
    Given I am on the home page
    When I click the magnifier button
    And I enter <SearchTerm> into the search input
    And I click the search button
    Then every result link text should contain <SearchTerm>

    Examples:
      | SearchTerm |
      | BLOCKCHAIN |
      | Cloud      |
      | Automation |
