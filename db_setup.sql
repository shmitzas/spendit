DROP DATABASE IF EXISTS spendit;
CREATE DATABASE spendit;

CREATE TABLE spendit.Users (
    Id BINARY(16) PRIMARY KEY UNIQUE,
    UserName VARCHAR(255) NOT NULL UNIQUE,
    Password VARCHAR(255) NOT NULL,
    Email VARCHAR(255) NOT NULL UNIQUE,
    Settings VARCHAR(255) NOT NULL DEFAULT "{\"currency\": \"EUR\"}",
    CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
);

CREATE TABLE spendit.Categories (
    Id INT AUTO_INCREMENT PRIMARY KEY NOT NULL UNIQUE,
    Name VARCHAR(255) NOT NULL
);

CREATE TABLE spendit.Budgets (
    Id BINARY(16) PRIMARY KEY UNIQUE,
    UserId BINARY(16) NOT NULL,
    Description VARCHAR(255) NOT NULL,
    Amount DECIMAL(10, 2) NOT NULL,
    Currency VARCHAR(3) NOT NULL,
    CurrentAmount DECIMAL(10, 2) NOT NULL,
    IsActive TINYINT(1) NOT NULL DEFAULT 0,
    CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    FOREIGN KEY (UserId) REFERENCES users(Id)
);

CREATE TABLE spendit.Transactions (
    Id BINARY(16) PRIMARY KEY UNIQUE,
    UserId BINARY(16) NOT NULL,
    CategoryId INT NOT NULL,
    BudgetId BINARY(16) NOT NULL,
    Description VARCHAR(255),
    Type ENUM('Income', 'Expense') NOT NULL,
    Amount DECIMAL(10, 2) NOT NULL,
    Currency VARCHAR(3) NOT NULL,
    CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    FOREIGN KEY (UserId) REFERENCES users(Id),
    FOREIGN KEY (CategoryId) REFERENCES categories(Id),
    FOREIGN KEY (BudgetId) REFERENCES budgets(Id)
);

CREATE TABLE spendit.RecurringTransactions (
    Id BINARY(16) PRIMARY KEY UNIQUE,
    UserId BINARY(16) NOT NULL,
    CategoryId INT NOT NULL,
    Description VARCHAR(255),
    Type ENUM('Income', 'Expense') NOT NULL,
    Amount DECIMAL(10, 2) NOT NULL,
    Currency VARCHAR(3) NOT NULL,
    StartDate DATE NOT NULL,
    EndDate DATE NOT NULL,
    Frequency ENUM('Daily', 'Weekly', 'Monthly', 'Quarterly', 'Annually') NOT NULL,
    CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    FOREIGN KEY (UserId) REFERENCES users(Id),
    FOREIGN KEY (CategoryId) REFERENCES categories(Id)
);

CREATE TABLE spendit.Goals (
    Id BINARY(16) PRIMARY KEY UNIQUE,
    UserId BINARY(16) NOT NULL,
    CategoryId INT NOT NULL,
    Description VARCHAR(255) NOT NULL,
    StartDate DATE NOT NULL,
    EndDate DATE NOT NULL,
    Amount DECIMAL(10, 2) NOT NULL,
    CurrentAmount DECIMAL(10, 2) NOT NULL,
    Currency VARCHAR(3) NOT NULL,
    CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    FOREIGN KEY (UserId) REFERENCES users(Id),
    FOREIGN KEY (CategoryId) REFERENCES categories(Id)
);

DROP USER IF EXISTS 'api';
CREATE USER 'api';

GRANT ALL ON spendit.* TO 'api';