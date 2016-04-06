Feature: StockAgeing
	In order to manage the store stock, 
	As a store keeper
	I want the quality of the stock items to adjust as the product ages.

@degradingProduct
Scenario: Add a degrading item to stock
	Given an item added to stock with starting quality of 10 and shelf life of 7 days
	And the item degrades at 1 quality point per day
	When the item has been in stock for 1 days
	Then the quality should be reduced to 9
	And the remaining shelf life should be 6


	
