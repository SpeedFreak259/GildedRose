Gilded Rose Refactor Notes
==========================

Requirements and Rules
----------------------

- Allow addition of new category with new aageing behaviour.
- Do not alter Item class or Items property
- UpdateQuality method and Items property can be made static.


Assumptions
------------------------------------
- Inconsistency between specification and implementation, Brie increases in quality at 2 points per day once the SellIn drops below zero. Assumed to be a problem with the specification as it is not clear.
- Assume items are never automatically removed from stock even when Quality or SellIn reaches zero.
- Performance : No requirement for application to complete in a specific time limit.
- "he doesn't believe in shared code ownership" Is not a sufficient argument to consider for good application design.
- SellIn values continues to decrement beyond zero.


Code Review
-----------

The following problems with the solution were identified with the existing version of the code base.


|Symptom                              | Problem                                                                           |
|-------------------------------------|-----------------------------------------------------------------------------------|
| Not Testable                        | The program class is not public, the UpdateQuality method is private to that class. |
| No Unit tests                       | No confidence that code works as described or that changes are unintentionally influencing behaviour.   |
| No exception handling               | Runtime exceptions are not captured or handled. |
| Insufficient separation of code     | Everything exists in single file; Program.cs   |
| Product Catalogue Hard-coded        | Adding a product requires developer.  |
| Product Ageing Rules Hard-Coded     | Very messy implementation of rules has lead to obfuscation. Rules specifically tied to product names. (case sensitive) |
| No validation                       | Items are not validated as they are added to the catalogue, their initial state may breach the business rules.|
| Data not persisted                  | Application state is not recorded between executes, each instance has its own 'source of truth' |
| S.R.P. Violation                    | Products in stock and definition of products are mixed in the model. |
| No record of when UpdateQuality ran | UpdateQuality always assumes a day has elapsed since it was last executed |
| No automated code analysis          | Code Analysis and style rules are not enforced reducing maintainability |


Recommended Actions
-------------------
0. In the first instance Make minor modification to allow unit testing.
  - Make UpdateQuality method public and static.
  - Make UpdateQuality method take items as a parameter.
1. Write unit tests
2. Add SpecFlow tests
3. Separation of catalogue and stock
4. Move catalogue to persisted storage
5. Separation of business rules from the UpdateQuality method
6. Add interfaces to allow for new specialist ageing rules to be implemented.

Notes
=====

In the first instance I added a simple Debug.WriteLine to output the catalogue before and after the UpdateQuality to confirm the current behaviour before making any changes;

Initial State
-------------

|Name|SellIn|Quality|
|----|------|-------|
|+5 Dexterity Vest|10|20|
|Aged Brie|2|0|
|Elixir of the Mongoose|5|7|
|Sulfuras, Hand of Ragnaros|0|80|
|Backstage passes to a TAFKAL80ETC concert|15|20|
|Conjured Mana Cake|3|6|

After UpdateQuality()
---------------------

|Name|SellIn|Quality|
|----|------|-------|
|+5 Dexterity Vest|9|19|
|Aged Brie|1|1|
|Elixir of the Mongoose|4|6|
|Sulfuras, Hand of Ragnaros|0|80|
|Backstage passes to a TAFKAL80ETC concert|14|21|
|Conjured Mana Cake|2|5|

 Changelog
 ---------
 Separation of Business logic, domain model and hosting process.
 Extended model to allow definition of ageing rules against each item.
 Added rule processors and factory to apply the rules
 Removed hard-coded ageing rules and stock list
 Added interfaces to allow extensibilty enabling further ageing rules are to be introduced.
 Added Unity IoC
 Unit tests with high degree of coverage
 Added spec flow tests to allow end user / developers to understand specification.
 Introduced Stylecop and code refactored to align with style rules
 Added simple persistence layer to store current stock iventory outside of the app (local json file)
 Record last quality update processing date to stock items so update quality only runs once per day and covers missed days.


 Suggested further refactoring
 -----------------------------
 Add more tracing to make it clearer how the stock ageing process applied the rules to the items.
 
