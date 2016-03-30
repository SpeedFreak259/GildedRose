Gilded Rose Refactor Notes
==========================

Requirements and Rules
----------------------

- Allow addition of new category
- Do not alter Item class or Items property
- UpdateQuality method and Items property can be made static.


Assumptions
------------------------------------
- Assume items are never automatically removed from stock even when Quality or SellIn reaches zero.
- Performance : No requirement for application to complete in a specific time limit.
- "he doesn't believe in shared code ownership" Is not a sufficient argument to consider for good application design.


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
| Data not persisted                  | Application state is not recorded between executes, each instance has its own 'source of truth' |
| S.R.P. Violation                    | Products in stock and definition of products are mixed in the model. |
| No record of when UpdateQuality ran | UpdateQuality always assumes a day has elapsed since it was last executed |
| No automated code analysis          | Code Analysis and style rules are not enforced,

Recommended Actions
-------------------
0. Make minor modification to allow unit testing.
  - Make UpdateQuality method public and static.
  - Make UpdateQuality method take items as a parameter.
1. Write unit tests
2. Add SpecFlow tests
3. Separation of catalogue and stock
4. Move catalogue to persisted storage
5. Separation of business rules from the UpdateQuality method
6. Add interfaces to allow for new specialist ageing rules to be implemented.