# Weather App
This is Weather Application using WeatherMap API. 

This is created as part of technical test for for interview.
To deploy this application, you will need to change appSettings.json [ weather api key and elastic search endpoint] and you will require Visual Studio 2017, and ASP.NET Core 2.0 (need to download separately if not available in VS 2017)
To deploy elastic search for this project, you can use this tutorial https://medium.com/@cecildt/quick-start-using-elasticsearch-kibana-with-asp-net-core-2-0-6f179b6d2d02

The application has front end and back end. The front end is ASP.NET MVC, and the backend is ASP.NET core.
The information logged into elastic search is HttpTraffic, Exception, and Auditing. This is to avoid using SQL or any datastore all together. 
The log can be viewed using Kibana (however will need to configure Elastic search index to make it works)


This project is to demonstrate a number of important feature for software development:
- Caching (Use ASP.NET memoery cache) to improve performance and avoid jamming the weather API calls. 
- Task Async to increase throughput for I/O bound operation. 
- Paralelism (Task.WhenAll) to maximise the multi-core processor feature to gain some performances. 
- IOC container across multiple dependant assemblies, and ability to specify the life cycle of the classes. 
- Architecutre aspects of application
    * Client - Server for the overall solution
    * Layered approach in the backend  (service layer) and separation in to separate modules.    
    * Configurability
    * Logging and Auditing
    * Exception handling (using Exception Filter)
    * User experience for error handling (give a clean message and reference for internal debuggin)
 - Unit tests with mocking (Xunit and Mock for mocking)
 - Design patterns:
    * Command Pattern, in this case is to separate the MVC abstraction (controller, and View) from business abstraction.
    * Lazy pattern (in controller), to allow commands in the controllers only be invoked only when it is actually invoked)
 - SOLID principles and Clean code in general
    * minimalist interfaces
    * Depency inversion by using IOC container. 
    * Meaningful name for method and variable
    * Avoid duplication (e.g using generic helper classes)
    * Minimilast responsiblity in each class.
 - Project quality
    * Static analysis, and style cops
    
    
    










