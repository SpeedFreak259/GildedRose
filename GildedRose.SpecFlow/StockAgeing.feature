Feature: StockAgeing
	In order to manage the store stock, 
	As a store keeper
	I want the quality of the stock items to adjust as the product ages.

@degradingProducts
Scenario: Add a degrading item to stock check the quality after a day
	Given an item added to stock with starting quality of 10 and shelf life of 7 days
	And the item degrades at 1 quality point per day
	When the item has been in stock for 1 days
	Then the quality should be equal to 9
	And the remaining shelf life should be 6


Scenario: Add a degrading item to stock check quality when sell by date reached
	Given an item added to stock with starting quality of 10 and shelf life of 7 days
	And the item degrades at 1 quality point per day
	When the item has been in stock for 7 days
	Then the quality should be equal to 3
	And the remaining shelf life should be 0


Scenario: Add a degrading item to stock after shelf life reaches zero item degrades twice as quickly 
	Given an item added to stock with starting quality of 10 and shelf life of 7 days
	And when the remaining shelf life is between 7 and 0 days the quality degrades at 1 points per day
	And when the remaining shelf life is less than 0 days then quality degrades at 2 points per day
	When the item has been in stock for 10 days
	Then the quality should be equal to -3
	And the remaining shelf life should be -3
	
@improvingProducts
Scenario: Add an improving item to stock check the quality after 1 day
	Given an item added to stock with starting quality of 1 and shelf life of 7 days
	And the item improves at 1 quality point per day
	When the item has been in stock for 1 days
	Then the quality should be equal to 2
	And the remaining shelf life should be 6

Scenario: Add an improving item to stock check the quality continues to improve after sell by date is reached
	Given an item added to stock with starting quality of 1 and shelf life of 7 days
	And the item improves at 1 quality point per day
	When the item has been in stock for 8 days
	Then the quality should be equal to 9
	And the remaining shelf life should be -1

Scenario: Add an improving item to stock check the quality reaches a maximum value
	Given an item added to stock with starting quality of 1 and shelf life of 7 days
	And the item improves at 1 quality point per day
	When the item has been in stock for 100 days
	Then the quality should be equal to 50
	And the remaining shelf life should be -93

@concertTickets
Scenario: Add a concert ticket and check the quality improves slowly two weeks out
	Given an item added to stock with starting quality of 1 and shelf life of 20 days
	And when the remaining shelf life is greater than 10 days the quality improves at 1 points per day
	And when the remaining shelf life is between 10 and 6 days the quality improves at 2 points per day
	And when the remaining shelf life is between 5 and 0 days the quality improves at 3 points per day
	And when the remaining shelf life is 0 the quality becomes 0
	When the item has been in stock for 9 days
	Then the quality should be equal to 10
	And the remaining shelf life should be 11

Scenario: Add a concert ticket and check the quality improves more quickly 10 days out
	Given an item added to stock with starting quality of 1 and shelf life of 20 days
	And when the remaining shelf life is greater than 10 days the quality improves at 1 points per day
	And when the remaining shelf life is between 10 and 6 days the quality improves at 2 points per day
	And when the remaining shelf life is between 5 and 0 days the quality improves at 3 points per day
	And when the remaining shelf life is 0 the quality becomes 0
	When the item has been in stock for 15 days
	Then the quality should be equal to 23
	And the remaining shelf life should be 5

Scenario: Add a concert ticket and check the quality is highest on the day of the concert
	Given an item added to stock with starting quality of 1 and shelf life of 20 days
	And when the remaining shelf life is greater than 10 days the quality improves at 1 points per day
	And when the remaining shelf life is between 10 and 6 days the quality improves at 2 points per day
	And when the remaining shelf life is between 5 and 0 days the quality improves at 3 points per day
	And when the remaining shelf life is -1 the quality becomes 0
	When the item has been in stock for 20 days
	Then the quality should be equal to 38
	And the remaining shelf life should be 0

Scenario: Add a concert ticket and check the quality is zero after the date of the concert
	Given an item added to stock with starting quality of 1 and shelf life of 20 days
	And when the remaining shelf life is greater than 10 days the quality improves at 1 points per day
	And when the remaining shelf life is between 10 and 6 days the quality improves at 2 points per day
	And when the remaining shelf life is between 5 and 0 days the quality improves at 3 points per day
	And when the remaining shelf life is -1 the quality becomes 0
	When the item has been in stock for 21 days
	Then the quality should be equal to 0
	And the remaining shelf life should be -1
