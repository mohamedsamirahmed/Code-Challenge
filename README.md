# Vehicle Dashboard Code Challenge!

# Getting Started

in this page i will explain the design structure and show to you some screenshot from solution structure and UI presentation
## Page Content
 - Business Overview
 - Architecture OVerview
 - Rabbit MQ management UI screenshots
 - How to run application
 - Technologies & startegies used accross project
 - Web application Screens

## Business Overview

- Imagine you are one of our consultants and you got assigned to a project at one of our top partners.

### Scenario:
  A number of connected vehicles that belongs to a number of customers.
 - They have a need to be able to view the status of the connection among these vehicles on a monitoring display.
- The vehicles send the status of the connection one time per minute.
- The status can be compared with a ping (network trace); no request from the vehicle means no connection. So, vehicle is either Connected or Disconnected.

### IN Scope
- Implement application as microservices.
- Background Task for updating customer vehicle connection status every period of time.
- Filter customer vehicle dashboard.
- Show specific customer vehicle connection history.
- Implementing an event bus with RabbitMQ.

### OUT Scope

- Implement containers with Docker compose configuration.
- Configure Continuous Integration for application.


### Enhancement Required
- Implement SignalR context hub for life updates.
- Add event logger database for tracking events and rollback if any service fails.
-  Implement containers with Docker compose configuration.
- Configure Continuous Integration for application.
- check latest customer vehicle history and log only if status changed.
- Add sorting functionality for table.
- Add angular error interceptor.


#  Architecture overview

This reference application is cross-platform at the server and client side.
The architecture proposes a microservice oriented architecture implementation with multiple autonomous microservices (each one owning its own data/db) and implementing different approaches within each microservice using Http as the communication protocol between the client apps and the microservices and supports asynchronous communication for data updates propagation across multiple services based on Integration Events and an Event Bus (a light message broker, to choose between RabbitMQ)

![Architecture Overview](https://lh3.googleusercontent.com/QR0nyEk33wZM4VNV1E3ZkD3S-WVbyILeZufwtdqR5LH5HxbN0FE7H69basyPxHShskKb_8eBwBy_ "Architecture overview")


## Internal Architecture Design

Customer Vehicle Dashboard application splitted into multiple project for achieving **SOLID** principle and **DDD** strategy.

System design implemented divided into two sections:

### Client Application
Client application could be mobile,Traditional MVC web or  application or SP web application .Our design implemented with SPA web application
### Backend Application
 Vehicle dashboard system backend designed using microservices each microservice has it's own structure and implemented separately.
 ####  Job Simulator Scheduler microservice 
 Functionality of this microservice divided into two parts:
	       1. job hosting scheduler working every period of time. 
	       2. push message into RabbitMQ with random status for        
     	      customer vehicles.
> check project under **HostingJobs** folder in image below.

 #### Vehicle Connection microService
This microservice designed with three functionality. 
 1. Subscribe event handler on RabbitMQ event bus waiting for any connection status change.
 2.  Web API for getting **connection history** of customer specific vehicle.
 3. Adding new connection history for customer vehicle.
> check projects under **VehicleConnection** folder  in image below.
#### Vehicle Service microservice
This microservice user for the following 3 functionalities
 1. WebI for Getting **latest connection status** , **customers lookup** , **vehicles  lookup** data.
 2. Update customer vehicles with the latest connection date and status.
> check projects under **VehicleService** folder  in image below.
##### Solution Structure
![Solution Structure](https://lh3.googleusercontent.com/9ftLUMDU80c0Losx3IbNh6dkOqqXBnHLrG9CvFPpQdSi9_TQFLp1HMpw5oarydh1TzvLil942QYO)
  
  ## Deep Dive into Internal Architecture Design
 i will explain more about each project architecture design.
### Vehicle Dashboard Service microservice
#### Solution Structure
![Vehicle Service](https://lh3.googleusercontent.com/ECSi-A9VfcUVL1J-T6ILO_9Ln7ua3qwykEi_rbB2F9U9UA_M8li0DT7w1505YbP313WDu_BOXOmj "Vehicle Service")
 ### Vehicle Service microservice Design
![Vehicle Service Design Structure](https://lh3.googleusercontent.com/bJbNiRMOs4uBaNHrRiOQjj6wz-hwAljCISVBDKPVigtiSIs-4FiIF8yFI01pvIe1ZSSliabhLCef)

The above diagram for Vehicle Dashboard **Service** contains the following components:
1- **Core layer**: contains abstraction functionalities describe the core behavior of the system like (Repository,Unit Of Work,Response Models and paging functionality) to be used in the infrastructure layer to implement the corresponding
interfaces.
2- **Infrastructure Layer**: Contains the
- **Data Model** project :  which is responsible for creating all entity DB models.
- **Data** project :  which is responsible for creating DB entity framework context references **Data Model** project.
- **Domain** project : responsible for creating entity repositories and mapping **Data Models** to **DTO Models** and implementing services which consumed by **API** Layer.
![VehicleDashboardService.cs](https://lh3.googleusercontent.com/zTFWEonCaHm82DAGiiuqaQcdpQKBc6vXDwqcAfAkICGQh41LeYUy9bnKTy1xmEtTz25hxQ4cao6u)
--**VehicleDashboardService** class : Unit of work implementation for all repositories to achieve CRUD operation (Get Customer Vehicles , Get Customers Lookup , Get Vehicles Lookup and Update Customer Vehicle with latest connection status ).
3-**API Layer**: contains API project  responsible for creating APIs used by **Presentation** Layer and execute all business operations on beside to subscribe & handle events.

For Recap this microservice you can check the following diagram.
![Vehicle Dashboard Service Microservice](https://lh3.googleusercontent.com/ankgonW1WfzvJ9qBlaI02z1Px__5mS0gf9ZWSEq6tOPQOaUFldbbkMr72ufe4Yk1UC-mptAlFSN-)
 #### Database Design
 This database designed for saving lates customer vehicle status , customers and vehicles
 ![Vehicle Service DB](https://lh3.googleusercontent.com/QK2uqW1MsdSjiTFHwenQBNKDyZhUmQAYEiLO5PvHv-c80cTKjVeO51543A65LJVCbGi7pQy_aT4t)
 ### Vehicle Connection microservice Design
![Vechicle Connection Design Diagram](https://lh3.googleusercontent.com/Ng1w6YFutW7-avbz9jaixYifsi8fOLiGUUlPte00-tCWVkhpoFgWiVGrf4ZEYVWebUSj4sCgyN2j)
The above diagram for Vehicle Dashboard **Connection** contains the following components:
1- **Core layer**: contains abstraction functionalities describe the core behavior of the system like (Repository,Unit Of Work,Response Models and paging functionality) to be used in the infrastructure layer to implement the corresponding
interfaces.
2- **Infrastructure Layer**: Contains the
- **Data Model** project :  which is responsible for creating all entity DB models.
- **Data** project :  which is responsible for creating DB entity framework context references **Data Model** project.
- **Domain** project : responsible for creating entity repositories and mapping **Data Models** to **DTO Models** and implementing services which consumed by **API** Layer.
![CustomerVehicleHistoryService.cs](https://lh3.googleusercontent.com/7cyizComB5TX0Tj0JaVXLAFNNBFH8AUo9HdiIhdh6do8WPcSPBIEMNpVWGwn3jZFmiCG-4LY5L9l)
-- **CustomerVehicleHistoryService** class : Unit of work implementation for all repositories to achieve CRUD operation (Get/Add Customer Vehicles History).
3-**API Layer**: contains API project  responsible for creating APIs used by **Presentation** Layer and execute all business operations on beside to subscribe & handle events.

For Recap this microservice you can check the following diagram.
![Vehicle Connection Microservice](https://lh3.googleusercontent.com/iXNPK3jSVi3DlmWTEmc9vPToBjRYifyRcpN1GCGb-sNKI3gEEqYMlCHbwqu9s7bKcSgnbkDxv6HM)

#### Design DB
Database designed for inserting customer vehicle history stats so that it contains only one table.
![Vehicle History DB Design](https://lh3.googleusercontent.com/F87TKn25kmDdpIaD-7h7PPiFEOPKVncepGv5PKUYhOg08J4hHQUoyX_oKgIBEU6yGvUjCNpVEmqO)

 ### Job Simulator Scheduler microservice Design
 ![Job Simulator Scheduler](https://lh3.googleusercontent.com/xLKzvws7_PzEVOAUFbF98GB-zvrummq9adFXfuN-WbVKn3LY548PVv1Bb8rbES3lNRzDia2fnlVg)
The above diagram for **Job Simulator Scheduler** contains the following components:
1- **Core layer**: contains abstraction functionalities describe the core behavior of the system like (Repository,Unit Of Work,Response Models and paging functionality) to be used in the infrastructure layer to implement the corresponding
interfaces.
![Simulator classes](https://lh3.googleusercontent.com/26K2e_DMxnHmqvi6vX6Y8oYAdieSLLGRdegTsJ0QMp7i04nLG76Ogo-MLO3wAYAJRGZu3qB83jKG)
2-**Task Scheduler**: this layer responsible for creating task scheduler job using Quartz
> [Quartz.NET](https://www.quartz-scheduler.net/) is a full-featured, open source job scheduling system that can be used from smallest apps to large scale enterprise systems.

--**CustomerVehiclesHistoryJob** class : this class responsible for generating random status for customer vehicles.
--** CustomerVehicleHistoryIntegrationEventService** class : responsible for publish generated random vehicle status to Queue.


For Recap this microservice you can check the following diagram.
![Job Simulator Scheduler Diagram](https://lh3.googleusercontent.com/VedIcRx3yu4ZQsXnjEK_VlE-TywLKdZ7nci7bHdKyuJw1xNebG7qksykJH2qm7yATGVSMeIJpeX8)
### Customer Vehicle Presentation Layer
![enter image description here](https://lh3.googleusercontent.com/O6JxNezj097I3J4cfUbwbwOWYgkE0Qrlw7EE5AWqGEPbkpLFT1p8ij17jDZl88it-MLsPBTxmHa3)

The above diagram for Vehicle Dashboard **SPA** contains the following components:
1- **Common layer**: contains abstraction functionalities describe the core behavior of the system like (Repository,Unit Of Work,Response Models and paging functionality) to be used in the infrastructure layer to implement the corresponding
interfaces.
2- **Web API Layer**: which consume web api for presenting data from data layer.
#### Technology used for implementing SPA web application
- Angular 5.2
- Bootstrap 3.3.7
- ngx-Bootstrap : 2.0.2
- Alertifyjs

#### Solution Structure
![SPA files](https://lh3.googleusercontent.com/NwDEQ2sIeZ3MTxlftKg-JuF3NTSI6NI_wVvuLmiw8twED8235ULxcBnAnF7IvRxQSlp1PeHz5pFw)
in the previous images it shows the most important files in SPA web application
 - **customer-vehicle-dashboard** folder contains component and view html for representing customer vehicle list and filter.
 - **customer-vehicle-details** folder contains component and view html for representing details of specific customer vehicle.
 - **services**folder contains service class which call web api.
 - **shared** folder contains implementation of shared services which user across application.
#### Web application Screens
![Customer Vehicle Dashboard](https://lh3.googleusercontent.com/ZsJ5iSouXjNWxle7N2X1TJ4bkA3CLdndS33eLolFtF_9LFGIjHDQorgaV1RlcFZXcohKnwZLzpFo "Customer Vehicle Dashboard")
The above picture shows customer vehicle home page (Dashboard)
![Customer vehicle status Details](https://lh3.googleusercontent.com/8-7whEBO5b-W93Nyf5E2BFF01LUD-JWniHPkGO8XFSXzySI0vx7rW8mVdQcSHJNAloocH58ZeCAS)
The above picture shows customer vehicle connection history .
### Rabbit MQ management UI screenshots
 - Customer Vehicle Exchange 
 ![Exchange Event Bus](https://lh3.googleusercontent.com/rnJaUJXOPKVaWpKlF-kAs0cO2LDTXSzV45eY-o8IogbqN8g1No4qdZtUpxktiKJXy974Widt05Fh)
 - Customer Vehicle Bindings
 ![Vehicle Connections](https://lh3.googleusercontent.com/bKXoIuO2jUfRCSOhWBuwVjfoEusIUgMBepf0Sx8VfyhEVMKF00o2s9KwOhat8W1iRXBT_Iqb-Rfi)
 - Customer Vehicle Queues
 ![Customer Vehicle Queues](https://lh3.googleusercontent.com/PZEz0pfVgZWwbNwnMhdrEJo5mfid3vpHHRdO6LFugqReil0A6MZXN7R8TgiOozZhqGJFksseJwwk)
- Customer Vehicle Consumer Channel
- ![Consummer Channel](https://lh3.googleusercontent.com/fqX-AsJDt0wZpnoN8x0N3XszIipAkEqE-if-8OTfxaomQ1OOnAOPONRvGuvJc8G6dtijTfRsqFmh)
- Customer Vehicle Queue Message 
![Message](https://lh3.googleusercontent.com/g2h8yUthwfXVYD0ZmSYawkCN8eRQ-iy6n9s-7zW6WPVnIg0pcGULReS2dPCCKK19cH89D-yUZsX9)

### How to run application

#### install packages 
    npm install for all projects 
#### run the following projects after configuring appsettings.json file for each project

 1. VehiclesDashboard.VehicleServices.API
 2. VehiclesDashboard.VehicleConnection.API
 3. VehicleDashboard.Simulator.HostScheduler
 4. VehicleDashboard.SPA

### Technologies & startegies used accross project

 1. RabbitMQ
 2. Angular 5
 3. ASP .net core 2.1
 4. SqLite DB
 5. Entity Framework core.
 6. Quartz .Net
 7. SOLID principles
 8. microservices design
 9. DDD  principle
 10. 10.Repository Design Pattern
 11. Unit Of Work
 12. Dependency Injection
 13. Data Transformation Object (DTO)
