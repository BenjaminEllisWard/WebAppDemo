# WebAppDemo
Demo of a business web application.

I started this over a weekend piecing together some stuff I have learned working on business web apps for the past 6 months. The goal is to put together a sizeable demonstration of common functionalities in these types of applications. It's an MVC project on .NET 4.6.1.


## Progress as of 04/22/2019:

  - Put together a simple backend that features a hardcoded data access layer (DAL) that is meant to mimic a access to a db. I decided to do this so that the project could be pulled from github and run on anyone's localhost without any problems with configuring EF with a SQL db.
    
  - Added client-side search and limited filtering functionality to DemoPage. Will write an example of an ajax call later on this week.
  
## 04/30/2019
  
  - Have the general development of the demo page complete. Looking toward correcting the model design. The DTO originating from DAL is simply assigned as a property to the model in UI layer instead of the model having its own properties that are gotten via a method.
  
  - Also up next, writing in the rest of the server controls' functionality.
