# RaspiRobot

## Solution Setup

The solution uses solution folders to group projects.

### Solution Folders

#### Startup

Project to setup and configure the application.

#### Common

Utility projects containing classes for cross-cutting concerns.

#### Interfaces

Projects implementing public APIs of the application.

#### Features

Projects implementing the functionality of the application.

### Projects

#### Startup: RaspiRobot

Entry point to the solution is the RaspiRobot.csproj in the Startup folder. 

It is resposible for:

 - Setting up Autofac as DI container
 - Configure logging framework
 - Creation of the application host containing
    - gRPC endpoints for EROWA Robot OpenAPI
    - web hosting for the future robot web UI
    - api hosting the backend of the future robot web ui
    - Swagger to document and test the api endpoints

#### Features - RobotControl: RaspiRobot.RobotControl

Contains the infrastructure, common settings and interfaces to implement a
robot control. This project is not specific to the Joy-It Grab-It but could
also be used for any other robot implementation.

#### Features - RobotControl: RaspiRobot.RobotControl.GrabIt

Contains the Joy-It Grab-It specific implementation of this project, including 
the driver required to communicate with the robots servos. 