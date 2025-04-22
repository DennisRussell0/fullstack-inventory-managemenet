# Fullstack Inventory Management System

A fullstack application for managing inventory and orders for an e-commerce business.

## Description

This project is a fullstack inventory management system designed to handle product storage and orders. The system includes a backend built with C# and PostgreSQL, and a frontend built with React and Tailwind CSS. The goal was to provide a scalable and user-friendly solution for managing inventory and orders.

### Shortcomings

Because of limited time to complete the project, the frontend lacks some things - e.g.:
- The function to add, remove and update products in the frontend
- Reservations of products
- Transactions
- Authorization/access control

## Getting Started

### Dependencies

- **Backend**:
  - .NET 6.0 or later
  - PostgreSQL 13 or later
  - Npgsql library for database connection
- **Frontend**:
  - Node.js 16 or later
  - npm 8 or later
  - Tailwind CSS

### Installing

#### Backend

1. Clone the repository:
   ```bash
   git clone https://github.com/DennisRussell0/fullstack-inventory-managemenet.git
   cd fullstack-inventory-management/InventoryManagementBackend
   ```
2. Restore dependencies and build the project:
   ```bash
   dotnet restore
   dotnet build
   ```

#### Frontend
1. Navigate to the frontend directory:
   ```bash
   cd fullstack-inventory-management/Frontend/Inventory-Management
   ```
2. Install dependencies:
   ```bash
   npm install
   ```
3. Install Tailwind CSS:
   ```bash
    npm install tailwindcss @tailwindcss/vite
   ```
3. Start the development server:
   ```bash
   npm run dev
   ```

### Executing program

#### Backend
1. Start the backend server:
   ```bash
   cd fullstack-inventory-management/InventoryManagementBackend
   dotnet run
   ```
2. The API will be available via Swagger at `http://localhost:5278/swagger/index.html`.

#### Frontend
1. Start the frontend development server:
   ```bash
   cd fullstack-inventory-management/Frontend/Inventory-Management
   npm run dev
   ```
2. Open your browser and navigate to `http://localhost:5173`.

## Authors

Dennis Russell
(https://github.com/DennisRussell0)

Mikkel Silvasen
(https://github.com/galleg123)
