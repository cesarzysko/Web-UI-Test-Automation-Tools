Feature: Services section navigation

Scenario Outline: Navigating to a service category from the Services menu shows the correct page
    Given I am on the home page
    When I click the Main Navigation menu button
    And I click the Services dropdown arrow
    And I click the Artificial Intelligence dropdown button
    And I select the <ServiceCategory> category
    Then the page title should be <PageTitle>
    And the Our Related Expertise section should be displayed

Examples:
    | ServiceCategory | PageTitle                            |
    | Generative AI   | Generative AI                        |
    | Responsible AI  | Responsible AI Assessment & Services |