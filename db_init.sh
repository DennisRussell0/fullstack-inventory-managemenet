#!/bin/bash

if [ "$#" -ne 2 ]; then
    echo "Usage: $0 <Username> <Password>"
    exit 1
fi

# Variables
DB_NAME="cerealdatabase"
DB_USER=$1
DB_PASSWORD=$2
TABLE_ONE_NAME="products"
TABLE_TWO_NAME="orders"
TABLE_THREE_NAME="order_products"

# Create the database
echo "Creating database $DB_NAME..."
PGPASSWORD=$DB_PASSWORD psql -U $DB_USER -c "CREATE DATABASE $DB_NAME;"

echo "Creating table $TABLE_ONE_NAME"

SQL_COMMANDS="
CREATE TABLE $TABLE_ONE_NAME (
    Id SERIAL PRIMARY KEY,               -- Auto-incremented integer as primary key
    Price FLOAT,                         -- Integer for price
    Storage INT,                         -- Units in storage
    Name VARCHAR(255),                   -- String for the product name
    Manufacturer VARCHAR(255),           -- String for manufacturer name
    Type VARCHAR(255),                   -- String for type of product
    Calories INT,                        -- Integer for calories
    Protein INT,                         -- Integer for protein content
    Fat INT,                             -- Integer for fat content
    Sodium INT,                          -- Integer for sodium content
    Fiber FLOAT,                         -- Float for fiber content
    Carbs FLOAT,                         -- Float for carbon content
    Sugars INT,                          -- Integer for sugars
    Potassium INT,                       -- Integer for potassium
    Vitamins INT,                        -- Integer for vitamins
    Shelf INT,                           -- Integer for shelf number
    Weight FLOAT,                        -- Float for weight of the product
    Cups FLOAT,                          -- Float for cups
    Rating FLOAT,                        -- Float for rating
    image_path VARCHAR(255)              -- String for image path
);

CREATE TABLE $TABLE_TWO_NAME (
    Id SERIAL PRIMARY KEY,               -- Auto-incremented integer as primary key
    Price FLOAT,                         -- Integer for total price
    Date TIMESTAMP,                      -- Date and time for the order
    Buyer VARCHAR(255),                  -- String for the buyer name
    Address VARCHAR(255)                 -- String for the address
);

CREATE TABLE $TABLE_THREE_NAME (
    OrderId INT,                         -- References Orders table
    ProductId INT,                       -- References Products table
    Quantity INT,                        -- Quantity of this product in the order
    PRIMARY KEY (OrderId, ProductId),    -- Composite primary key
    FOREIGN KEY (OrderId) REFERENCES Orders(Id) ON DELETE CASCADE,  -- Ensures referential integrity
    FOREIGN KEY (ProductId) REFERENCES Products(Id) ON DELETE CASCADE  -- Ensures referential integrity
);

"

# Connect to the new database and execute the SQL commands
echo "Creating TABLES in the database..."
PGPASSWORD=$DB_PASSWORD psql -U $DB_USER -d $DB_NAME -c "$SQL_COMMANDS"