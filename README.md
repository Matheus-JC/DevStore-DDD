<h1 align="center"> DevStore </h1>

## Introduction ##

DevStore-DDD is a project built with a focus on studying the main practices used in the Domain-Driven Design (DDD) approach. For this, an application was built that simulates some e-commerce functionalities such as: add and edit products, place orders, make payments, etc.

It is important to highlight that the design of the system screens is very simple because the focus of the project is on the backend.

## Installation Instructions ##
To run the project, simply have Docker installed on the machine and type the following command in the project root:
```
docker compose up
```

## Project Arquitecture ##

The application was divided into 5 delimited contexts. The context map below displays the contexts and their relationships in greater detail.

![context-map-devstore-ddd](https://github.com/Matheus-JC/DevStore-DDD/assets/28056967/dfea874a-8691-487a-a01e-e475a100d255)


### Catalog Context ### 
It has 3 layers, they are: Application, Domain and Data. This context follows the Domain Model Pattern.

### Payment Context ###
Was implemented with 3 layers, they are: Business, Data and AntiCorruption. This context is much simpler because the external payment tool would be responsible for the greatest complexity. In the AntiCorruption layer, a Facade was built to access payment methods without polluting the project's ubiquitous language. External access to a tool has not yet been implemented, for now, this is only done through a simulation.

### Sales Context ###
It also has 3 layers, they are: Application, Domain and Data. This context is the most complex of the application, it has an event-oriented architecture and uses EventSourcing to store events. The Application layer was implemented using the CQRS architectural pattern.

### Register Context ###
Has not yet been implemented.

### Tax Context ###
Has not yet been implemented.

## Technologies Used ##
- .NET 8 with C#
- SQL Server
- EventStore
- Docker

**Some Libs and Frameworks:**
- ASP.NET MVC
- Entity Framework
- MediatR
- FluentValidation
- AutoMapper  
  Tests:
  - xUnit
  - Bogus
  - FluentAssertions
  - MOQ
