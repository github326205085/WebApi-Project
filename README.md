# NewBorn

## Description
This project is built using .NET 7 Core and employs a REST API. The project focuses on secure password handling, validation, and a scalable structure.

## Technologies and Packages Used
- **.NET 8 Core**
- **REST API**
- **Entity Framework (EF)** for ORM using a DB first approach
- **zxcvbn** for password strength validation
- **AutoMapper** for data transformations
- **Swagger** for API documentation
- **Configuration Files** for managing environment-specific settings

## Project Structure
The project is structured into three layers:
1. **Presentation Layer**: Handles the user interface and API endpoints.
2. **Business Logic Layer**: Contains the core business logic.
3. **Data Access Layer**: Manages database operations using EF ORM.

These layers communicate via Dependency Injection (DI) to ensure encapsulation.

## Scaling and Performance
To ensure scalability, all functions are implemented using `async` and `await` patterns.

## Data Input
Data input is managed in a separate project.

## Security and Validation
- **Password Strength**: Implemented using the `zxcvbn` package.
- **HTTPS**: The entire project uses HTTPS to ensure secure communication.

## Configuration Management
Configuration files are used to manage settings that vary between environments.

## Error Handling
Errors are properly handled:
- On the server side, exceptions are caught, emails are sent, and errors are logged in a separate file.
  
## Monitoring
All traffic is monitored using a `rating` table to analyze the service performance and utilization.

## API Documentation
The entire API is documented using Swagger, which can be accessed [here](#).
## Testing The project includes both unit tests and integration tests to ensure robustness.
- Unit Tests: Focus on testing individual components in isolation.
- Integration Tests: Verify the interactions between different components and layers.
   To run the tests, use the following command: `sh dotnet test ` Ensure you have the necessary test projects configured and added to your solution. 
## Running the Project
1. Ensure all prerequisites are installed.
2. Clone the repository.
3. Configure the environment settings.
4. Run the project using `dotnet run`.
