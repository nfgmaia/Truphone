# Truphone Tech Challenge
This is my tech challenge for Truphone.

## Technologies
* .NET 5
* C#
* Dapper
* Sqlite (in-memory)
* MsTest
* Moq
* FluentAssertions

## Getting Started (build and run)
To build and run the code please follow the below instructions:
* Open a command line.
* Go to the root folder of the solution, where the .sln file is.
* Run the API with the command "dotnet run --project src/Truphone.Api"
* You can find the Swagger in "https://localhost:5001/swagger"

## Overview
This solution follows an 'Hexagonal Architecture'.
Conceptually, the core is the heart of the solution and does not depend on external frameworks for data input, data output or persistence.

### Adapters
These are the concrete implementations of the ports of the solution.
We have the Api as an entry point, and an in-memory database in Sqlite used for persistence (please remember the DB is running in-memory, and this means all data is lost when the application stops).

### Domain
This is the core of the solution we are building.
In here you can find all the interfaces and entities that represent the logic of the business.
This is where the 'ports' (interfaces for the concrete implementations - adapters) are located.
It has no dependencies on any other layers.

### Application
This is where the concrete implementation of the 'domain services' are located.
The interfaces for these services are in the domain layer, which is the only dependency this layer has.

### Tests
The unit test projects. Currently we have unit tests for the application layer and Api.

## Author
- Nuno Maia
- nuno86@gmail.com
- https://www.linkedin.com/in/nuno-maia-39b33bb7/
