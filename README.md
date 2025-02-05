# Distributed-Logging-System

# Distributed Logging System

A Distributed Logging System developed using C# (ASP.NET Core) for the backend and Angular for the frontend. This system enables efficient logging of various application events across multiple services and provides real-time access to log data with filtering and searching capabilities.

 

## Overview

This Distributed Logging System allows developers and system administrators to capture, store, and analyze log data from multiple applications or services. The backend is built using C# and ASP.NET Core to handle logging requests and interact with a database. The frontend is developed in Angular and provides an intuitive user interface for viewing logs, filtering, and searching through log entries in real time.

## Features
- **Centralized Logging:** Collect logs from multiple sources and services in one location.
- **Search and Filtering:** Easily search logs based on various parameters (e.g., date, severity, service).
- **Real-Time Log Updates:** Real-time log viewing and updates using WebSocket technology.
- **User Authentication:** Secure access to logs with role-based authentication.
- **Customizable Log Levels:** Ability to log different levels of information (e.g., Info, Warning, Error).
- **API for External Integration:** Expose RESTful APIs to send log data from other systems.

## Tech Stack

- **Backend:** C#, ASP.NET Core
- **Frontend:** Angular
- **Database:** SQL Server (or any other relational database of choice) 

## System Architecture

The system is composed of two main parts:
1. **Backend (C# with ASP.NET Core):** Handles log ingestion, storing logs in a database, and providing APIs to access logs.
2. **Frontend (Angular):** Displays logs in real-time, allows filtering and searching, and interacts with the backend API.

Additionally, a real-time WebSocket connection is established using SignalR to push new logs to the frontend instantly.

## Prerequisites

To run this project locally, you need the following installed:

- .NET 6.0 or later
- Node.js and npm
- Angular CLI
- SQL Server (or another relational database)
- Visual Studio or Visual Studio Code (optional, but recommended)

## Installation

### Backend Setup

1. Clone the repository:
   ```bash
   git clone https://github.com/yourusername/distributed-logging-system.git
   cd distributed-logging-system/backend
