#!/bin/bash

echo "Starting all backend services and the React frontend for Easy Cart..."

# Array of ports used by the backend projects
backend_ports=(5000 5001 5002 5003 5004)

# Kill any processes using the backend ports
for port in "${backend_ports[@]}"
do
    PID=$(lsof -t -i :$port)
    if [ -n "$PID" ]; then
        echo "Killing process $PID using port $port..."
        kill -9 $PID
    else
        echo "No process found on port $port."
    fi
done

# Array of backend projects to start
backend_projects=(
  "EasyCart.API.Gateway/EasyCart.API.Gateway.csproj"
  "EasyCart.AuthService/EasyCart.AuthService.csproj"
  "EasyCart.ProductService/EasyCart.ProductService.csproj"
  "EasyCart.CartService/EasyCart.CartService.csproj"
  "EasyCart.OrderService/EasyCart.OrderService.csproj"
)

# Start each backend project
for project in "${backend_projects[@]}"
do
    echo "Starting backend: $project"
    dotnet run --project ./$project &
done

# Start the React frontend
echo "Starting frontend..."
cd ./EasyCart.Frontend
npm run dev &

echo "All services are starting..."

# Wait for user to terminate
wait
