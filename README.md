# Vehicle Dashboard Code Challenge!

Hi! I'm your first Markdown file in **StackEdit**. If you want to learn about StackEdit, you can read me. If you want to play with Markdown, you can edit me. Once you have finished with me, you can create new files by opening the **file explorer** on the left corner of the navigation bar.
There are two types of synchronization and they can complement each other:

- The workspace synchronization will sync all your files, folders and settings automatically. This will allow you to fetch your workspace on any other device.
	> To start syncing your workspace, just sign in with Google in the menu.

- The file synchronization will keep one file of the workspace synced with one or multiple files in **Google Drive**, **Dropbox** or **GitHub**.
	> Before starting to sync files, you must link an account in the **Synchronize** sub-menu.


# Getting Started

StackEdit stores your files in your browser, which means all your files are automatically saved locally and are accessible **offline!**

## Business Overview

- Imagine you are one of our consultants and you got assigned to a project at one of our top partners.

### Scenario:
 - A number of connected vehicles that belongs to a number of customers.
 -- They have a need to be able to view the status of the connection among these vehicles on a monitoring display.
-- The vehicles send the status of the connection one time per minute.
-- The status can be compared with a ping (network trace); no request from the vehicle means no connection. So, vehicle is either Connected or Disconnected.

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
#### Vehicle Dashboard microservice
This microservice user for the following 3 functionalities
 1. WebI for Getting **latest connection status** , **customers lookup** , **vehicles  lookup** data.
 2. Update customer vehicles with the latest connection date and status.
> check projects under **VehicleService** folder  in image below.
##### Solution Structure
![Solution Structure](https://lh3.googleusercontent.com/9ftLUMDU80c0Losx3IbNh6dkOqqXBnHLrG9CvFPpQdSi9_TQFLp1HMpw5oarydh1TzvLil942QYO)
  
