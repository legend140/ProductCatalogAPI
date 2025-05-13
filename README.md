# Product Catalog API - README

## Overview

This project is a simple **Product Catalog API** built with **.NET** and uses **SQL Server** or **PostgreSQL** as the backend database. The API allows you to manage products in an e-commerce catalog, supporting operations such as retrieving products, adding new products, and updating existing ones. This README provides instructions on how to run the solution and includes explanations of the design and assumptions made during implementation.

## Features

- **Product Management**: Allows retrieval, creation, and updating of products.
- **Database**: Supports **SQL Server** and **PostgreSQL** for persistence. You can choose between these databases based on your environment.
- **Unit Testing**: Includes unit tests for the product repository and controller logic.
- **Mocking and Dependency Injection**: Uses **Moq** for unit testing the services and repositories.
  
## Prerequisites

Before running the application, make sure you have the following installed:

- **.NET SDK 8.0** or higher
- **SQL Server** or **PostgreSQL** (depending on which one you want to use)
- **Visual Studio** or **VS Code** (optional, for editing and running the project)
- **Docker** (optional, for containerization)
- **Postman** or any API client (optional, for testing endpoints)

## Instructions

### 1. Clone the Repository

git bash
git clone https://github.com/legend140/ProductCatalogAPI.git
cd ProductCatalogAPI

### 2. Restore dependencies

dotnet restore

### 3. Build the project

dotnet build

### 4. Run the API

dotnet run --project ProductCatalogAPI

### 5. Open Swagger UI

Navigate to https://localhost:<port>/swagger to explore and test the API.

### 6. Running Tests

dotnet test

## Assumptions & Notes

### An in-memory database (using InMemoryDb) is used for simplicity and demonstration.
### The project is structured with Clean Architecture principles in mind:
#### Controllers only handle request/response logic.
#### Business logic is encapsulated in services.
#### Data access is abstracted through repositories.
### The repository is configured for easy migration to SQL Server or PostgreSQL by updating the DbContext configuration.
### Input validation ensures
#### Name and Description is required and not empty.
#### Price must be a positive decimal.

## Submission

### The full source code is provided here.
### All tasks are implemented, including bonus requirements.





