FROM mcr.microsoft.com/dotnet/sdk:8.0

# Set working directory
WORKDIR /app

# Install the dotnet-watch tool for hot reloading
RUN dotnet tool install --global dotnet-watch

# Set environment variables for development
ENV ASPNETCORE_ENVIRONMENT=Development
ENV DOTNET_USE_POLLING_FILE_WATCHER=1
ENV DOTNET_WATCH_RESTART_ON_RUDE_EDIT=1
ENV ASPNETCORE_HTTP_PORTS=8080

# Create a directory for the project and copy csproj first to restore dependencies
COPY . /app/
RUN dotnet restore

# Expose port
EXPOSE 8080

# Set default command to use dotnet watch for hot reload
CMD ["dotnet", "watch", "run", "--no-restore", "--urls", "http://0.0.0.0:8080"]
