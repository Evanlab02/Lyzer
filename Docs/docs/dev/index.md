<<<<<<< HEAD
# Development (Getting Started)

Welcome to the Lyzer development environment! This guide will help you set up everything you need to start contributing to the project.

## Prerequisites  

Before diving into development, ensure you have the following tools installed:  

- **Docker**: For containerizing and managing the development environment.  
- **Docker Compose**: To orchestrate the various containers required for Lyzer.  
- **An IDE or Editor**: We recommend [Visual Studio](https://visualstudio.microsoft.com/) for backend development and [Visual Studio Code](https://code.visualstudio.com/) for frontend development, but feel free to use your preferred editor.

## Starting the Backend Development Environment  

Follow these steps to spin up the development environment:  

- **Clone the Repository**  
   Ensure you have the latest version of the Lyzer repository on your local machine:  

   ```bash  
   git clone https://github.com/LittleClumsy/Lyzer.git  
   cd lyzer  
   ```

- **Start the Containers in Watch Mode**  
   Use the following command to start the Docker containers with **watch mode** enabled:

   ```bash  
   docker compose watch
   ```  

   > The `watch` command ensures that changes to the API source code automatically trigger a rebuild for the container.  

- **Verify the Environment**  
   Once the containers are up, confirm that the API is running by navigating to its swagger docs (e.g., `http://localhost:8080/swagger/index.html`).

## Starting the Frontend Development Environment  

Ensure you have spun up the backend development environment first. Follow these steps to spin up the frontend development environment:  

- **Clone the Repository**  
   Ensure you have the latest version of the Lyzer repository on your local machine:  

   ```bash  
   git clone https://github.com/LittleClumsy/Lyzer.git  
   cd lyzer  
   ```

- **Install Dependencies**  
   Navigate to the `frontend` directory and install the dependencies:

   ```bash  
   cd frontend  
   npm install  
   ```

- **Start the Development Server**  
   Use the following command to start the development server:

   ```bash  
   npm run dev  
   ```

   > The development server will automatically reload if you make changes to the code. The frontend will be available at `http://localhost:5173`.

## Development Workflow  

### Making Changes  

   - All API code is located in the `/Backend` directory.
   - All frontend code is located in the `/Frontend` directory.
   - Save changes, and the code will be automatically rebuilt and the application will be automatically restarted.

### Testing the API  

- Use the swagger documentation to test the endpoints
- Or use tools like [Postman](https://www.postman.com/) or [cURL](https://curl.se/) to test API endpoints.

## Troubleshooting  

### Common Issues  

- **Port Conflicts**: Ensure no other services are running on the same ports used by Lyzer.  

### Getting Help  

If you encounter issues that you can’t resolve, feel free to open an issue in the GitHub repository or reach out to the project maintainers.
=======
# Development (Getting Started)

Welcome to the Lyzer development environment! This guide will help you set up everything you need to start contributing to the project.

## Prerequisites  

Before diving into development, ensure you have the following tools installed:  

- **Docker**: For containerizing and managing the development environment.  
- **Docker Compose**: To orchestrate the various containers required for Lyzer.  
- **An IDE or Editor**: We recommend [Visual Studio Code](https://code.visualstudio.com/) for its Docker and C# support, but feel free to use your preferred editor.

> **Note:** At this stage, only the **API** is ready for active development. The frontend is not yet in a functional state.  

## Starting the Development Environment  

Follow these steps to spin up the development environment:  

- **Clone the Repository**  
   Ensure you have the latest version of the Lyzer repository on your local machine:  

   ```bash  
   git clone https://github.com/LittleClumsy/Lyzer.git  
   cd lyzer  
   ```

- **Start the Containers in Watch Mode**  
   Use the following command to start the Docker containers with **watch mode** enabled:

   ```bash  
   docker compose watch
   ```  

   > The `watch` command ensures that changes to the API source code automatically trigger a rebuild for the container.  

- **Verify the Environment**  
   Once the containers are up, confirm that the API is running by navigating to its swagger docs (e.g., `http://localhost:8080/swagger/index.html`).

## Development Workflow  

### Making Changes  

- All API code is located in the `/Backend` directory.
- Save changes, and the watch mode will rebuild the application automatically.

### Testing the API  

- Use the swagger documentation to test the endpoints
- Or Use tools like [Postman](https://www.postman.com/) or [cURL](https://curl.se/) to test API endpoints.  

## Troubleshooting  

### Common Issues  

- **Port Conflicts**: Ensure no other services are running on the same ports used by Lyzer.  

### Getting Help  

If you encounter issues that you can’t resolve, feel free to open an issue in the GitHub repository or reach out to the project maintainers.
>>>>>>> eaa569023632fe12a97fa8b4017edc5d69ed7104
