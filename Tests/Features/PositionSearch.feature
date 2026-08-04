Feature: Remote position search by criteria

  Scenario Outline: Searching for a remote position returns results matching the programming language
    Given I am on the home page
    When I click the Careers button
    And I click the Start Your Search Here button
    And I search for a remote position with programming language <ProgrammingLanguage> and country <Country>
    Then the latest search result should contain <ProgrammingLanguage> in its description

    Examples:
      | ProgrammingLanguage | Country  |
      | C++                 | Poland   |
      | JavaScript          | Mexico   |
      | Python              | Portugal |
