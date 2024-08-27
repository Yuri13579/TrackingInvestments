# TrackingInvestments

Coding Exercise:

Create a program in C# that allows a user to perform 4 calculations based on the sale of any number of shares of stock at any price utilizing one of the strategies defined earlier. For the scope of this task today we are only looking for you to implement the FIFO method, but you should tailor your solution to support any methodology. The calculations are as follows (they are defined on the previous page):

1.	The remaining number of shares after the sale 
2.	The cost basis per share of the sold shares
3.	The cost basis per share of the remaining shares after the sale
4.	The total profit or loss of the sale

The purchase lots will be as follows:

1.	100 shares purchased at $20 / share in January
2.	150 shares purchased at $30 / share in February
3.	120 shares purchased at $10 / share in March

Normally data of this type would be retrieved from a database; however for this exercise you may hard code the values into a data structure of your choosing.  This data should be stored in a data structure that allows you to easily add purchase elements as desired.

For simplicity, we will assume that all sale inputs will take place in April i.e. all shares above are held and available for sale.

The user should be able to specify the number of shares sold and the price per share.  The program should calculate the 4 items above.

The primary focus of this exercise is to look at your proficiency in C#, your coding style, your architectural design approach, and your ability to translate the business requirements into a functioning application.  This application warrants a simple 3 tier architecture.  Even though it may seem to be overkill for this task, we are looking to see how you would separate the components across multiple tiers.  Above all else, the application should perform the correct calculations based on various user input and the application should allow a user to easily enter different user input to calculate the results on the same purchase lot data above.
