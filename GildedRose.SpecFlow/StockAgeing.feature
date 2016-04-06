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



