Feature: StockAgeing
	In order to manage the store stock, 
	As a store keeper
	I want the quality of the stock items to adjust daily as the product ages.

@degradingProducts

Scenario Outline: Certain products degrade with age, bread for example, after the sell by date is reached the product degrades twice as quickly
	Given an item added to stock with starting quality of 10 and shelf life of 7 days
	And when the remaining shelf life is between 7 and 0 days the quality degrades at 1 points per day
	And when the remaining shelf life is less than 0 days then quality degrades at 2 points per day
	When the item has been in stock for <stockdays> days
	Then the quality should be equal to <quality>
	And the remaining shelf life should be <shelflife>

	Examples:
	| stockdays | quality | shelflife |
	| 1         | 9       | 6         |
	| 7         | 3       | 0         |
	| 10        | -3      | -3        |

@improvingProducts

Scenario Outline: Certain products improve with age, brie for example
	Given an item added to stock with starting quality of 1 and shelf life of 7 days
	And the item improves at 1 quality point per day
	When the item has been in stock for <stockdays> days
	Then the quality should be equal to <quality>
	And the remaining shelf life should be <remainShelfLife>

	Examples:
	| stockdays | quality | remainShelfLife |
	| 1         | 2       | 6               |
	| 7         | 8       | 0               |
	| 8         | 9       | -1              |


Scenario: Add an improving item to stock check the quality reaches a maximum value
	Given an item added to stock with starting quality of 1 and shelf life of 7 days
	And the item improves at 1 quality point per day
	When the item has been in stock for 100 days
	Then the quality should be equal to 50
	And the remaining shelf life should be -93

@concertTickets

Scenario Outline: A concert ticket improves in quality more quickly as the concert approaches and drops to zero after the concert
	Given an item added to stock with starting quality of 1 and shelf life of 20 days
	And when the remaining shelf life is greater than 10 days the quality improves at 1 points per day
	And when the remaining shelf life is between 10 and 6 days the quality improves at 2 points per day
	And when the remaining shelf life is between 5 and 0 days the quality improves at 3 points per day
	And when the remaining shelf life is -1 the quality becomes 0
	When the item has been in stock for <stockdays> days
	Then the quality should be equal to <quality>
	And the remaining shelf life should be <daysToConcert>

	Examples:
	| stockdays | quality | daysToConcert |
	| 1         | 2       | 19            |
	| 9         | 10      | 11            |
	| 15        | 23      | 5             |
	| 20        | 38      | 0             |
	| 21        | 0       | -1            |

@legendaryProducts

Scenario Outline: One product is legendary, its quality remains constant and the remaining shelf life does not adjust regardless of time on shelf
	Given a legendary item
	When the item has been in stock for <stockdays> days
	Then the quality should be equal to <quality>
	And the remaining shelf life should be <shelflife>

	Examples:
	| stockdays | quality | shelflife |
	| 1         | 80      | 0         |
	| 2         | 80      | 0         |
	| 10        | 80      | 0         |
	| 365       | 80      | 0         |
	| 3650      | 80      | 0         |