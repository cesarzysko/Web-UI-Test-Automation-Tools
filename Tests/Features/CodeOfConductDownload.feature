Feature: Code of Conduct download

  Scenario Outline: Downloading the Code of Ethical Conduct succeeds
    Given I am on the home page
    When I scroll to the footer
    And I click the Code of Ethical Conduct button
    Then the file <FileName> should be downloaded

    Examples:
      | FileName                  |
      | Code-Of-Conduct_01_26.pdf |
