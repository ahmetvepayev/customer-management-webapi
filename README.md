# Customer Management

Customer management standalone Web API application. Application is divided into several services that each reside in a separate Docker container. Client communicates with the Web API via HTTP requests

## Main features :

+ Store and modify customer data
+ Store and modify commercial transaction data with customers
+ Login system which also uses access tokens and refresh tokens for role based authentication and authorization
+ Customer photos are watermarked using a separate service
+ Separate report service produces weekly and monthly reports in Excel on customers with top 5 amount of transactions and on the amount of customers in each city, respectively. The reports can also be downloaded from the API

# Architecture

![architecture](https://i.imgur.com/GlzD3hP.png)

+ The individual services were designed as close to the Clean Architecture conventions as possible
+ Client performs all the actions via communication with the web API
+ The web API communicates with the PostgreSQL server for all data related operations
+ The web API doesn't directly communicate with the other services, and instead sends messages to RabbitMQ. The related service then recieves the message and performs its own operations completely separate of the API. The services then communicate with the database for data storage related operations

# Data Model

![datamodel](https://i.imgur.com/JAbuwXy.png)

+ The users and roles are implemented by Identity Framework and the default tables of the framework are present in the application. The only difference is the refresh token and its expiration that is stored in the users table


# Using The App

You need to install [Docker Engine](https://docs.docker.com/engine/install/) and [Docker Compose](https://docs.docker.com/compose/install/). Check the required steps for installation specific to your platform. You also need the Docker Engine running before you proceed with the following steps.

+ Clone the repository or download the source code.
+ Open a terminal window from the folder containing the ```docker-compose.yml``` file or navigate to that folder on your terminal.
+ Execute ```docker-compose up -d``` to start the application. Docker will automatically download all the necessary files required for the application and will set up separate container for PostgreSQL server and RabbitMQ. You can connect to ```http://localhost:15000/swagger``` from a web browser to see the API documentation.
+ Visit ```http://localhost:15672``` for RabbitMQ Management if you want to manage the message traffic. Credentials are the default for RabbitMQ :

    | Username | Password |
    |----------|----------|
    | guest    | guest    |

+ The message queues in RabbitMQ will not be created until the API sends a message to the queue for the first time. Until then the other services will not be able to listen to those queues and the containers for those services will keep restarting until they can establish a connection.
+ The web API seeds the database with sample data when it starts. You can use the following user credentials or register your own users and use a user with an admin role to give the added users any roles:

    | Username     | Password      | Roles         |
    |--------------|---------------|---------------|
    | adminuser    | adminPass123* | admin, editor |
    | ahmetvepayev | ahmetPass123* | editor        |

+ When you're done with the application, execute ```docker-compose down --rmi all --volumes``` to shut down the containers and remove all files associated with the application.

# API

Couple of remarks about the API :

+ Customer photos are accepted and returned as base64 strings converted from the binaries

# Libraries & Technologies

+ The services target .NET 6

+ Entity Framework Core is used for ORM & database querying
+ NpgSql for EfCore
+ Automapper for entity-dto mapping
+ Microsoft Identity Framework for handling users and roles
+ ImageSharp for image processing in the watermark service
+ Quartz.NET for scheduling of the message firing from the API for the weekly and monthly Excel reports
+ ClosedXML for creating Excel tables
