## Assessment

Complete a small full stack solution to do tax calculations using .NET and MVC Razor and do some basic CRUD
operations on SQL Server using Entity Framework (localdb provided with assignment).
A previous junior developer started this project but was unable to complete it. 
It is up to you to get it functioning as per the requirement, please feel free to add, remove or change whatever you need to.
Once you have completed the task, please zip your repo & share it with TA Specialist you are working with via a Google Drive / similar link

### A few pointers:

* Make sure you understand how progressive tax works
* Start with the API and ensure that it is functioning as required before moving on to the web project.
* Please keep performance in mind in your implementation,for example how would your application handle 1 million progresive calculations a day on limited hardware?
* Besides completing the test and getting it to work, and accuracy is important, it is also a chance to show the senior developers your understanding of a good framework so
    * Adhere to the SOLID principals
    * Complete the existing unit tests
    * Avoid scaffolding
    * Clean well-formatted code
    * Do not hardcode the calculators
    * Do not change the database, the application must use Sqlite

### Task brief:

You have been briefed to complete a tax calculator for an individual. The application will take annual income and postal code.

**Each postal code is linked to a type of Tax calculation:**

| Postal Code | Tax Calculation Type |
|-------------|----------------------|
| 7441        | Progressive          |
| A100        | Flat Value           |
| 7000        | Flat rate            |
| 1000        | Progressive          |

**The progressive tax is calculated based on this table (be sure you understand how a progressive calculation works):**

| Rate | From   | To     | 
|------|--------|--------|
| 10%  | 0      | 8350   |
| 15%  | 8351   | 33950  |    
| 25%  | 33951  | 82250  |                      
| 28%  | 82251  | 171550 |  
| 33%  | 171551 | 372950 | 
| 35%  | 372951 | 0      |


**The flat value:**
* 10000 per year
* Else if the individual earns less than 200000 per year the tax will be at 5%
 
**The flat rate:**
* All users pay 17.5% tax on their income

**Approach:**
* Use SOLID principals
* Use appropriate Design Patterns
* IOC/Dependency Injection
* Allow for entering the Postal code and annual income on the front end and submitting
* Store the calculated value to SQL Server with date/time and the two fields entered
* Security is not required but feel free to show off
* Server side should be REST API’s
