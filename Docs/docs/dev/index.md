# Development (Getting Started)

Welcome to the Lyzer development getting started guide! This guide will help you set up everything you need to start contributing to the project.

## Prerequisites  

Before diving into development, ensure you have the following tools installed:  

- **Docker**: For containerizing and managing the development environment.  
- **Docker Compose**: To orchestrate the various containers required for Lyzer.  
- **An IDE or Editor**: We recommend [Visual Studio](https://visualstudio.microsoft.com/) for backend development and [Visual Studio Code](https://code.visualstudio.com/) for frontend development, but feel free to use your preferred editor.

## Starting the Development Environment

Using docker, starting the development environment is as easy as running in the root directory:

```bash
docker compose watch
```

This will start the development environment and automatically load changes or rebuild containers when necessary.

You can also use the following commands for more control:

```bash
docker compose up -d --build # Build and start the containers in detached mode
docker compose up # Start the containers and attach to the logs (useful for debugging)
```

To stop the development environment, run:

```bash
docker compose down
```

### URLs

- **Backend**: `http://localhost:8080`
- **Frontend**: `http://localhost:5173`
- **Docs**: `http://localhost:8000`
- **Redis**: `http://localhost:6379`

## Troubleshooting  

### Common Issues  

- **Port Conflicts**: Ensure no other services are running on the same ports used by Lyzer.  

### Getting Help  

If you encounter issues that you can’t resolve, feel free to open an issue in the GitHub repository or reach out to the project maintainers.
