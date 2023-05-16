# Spendit - Personal Budget Tracking Web Application
> **NOTE:** This project is being created as part of Software Architecture course at Vilnius University and is made for **learning purposes only!**

## Table of Contents
- [Spendit - Personal Budget Tracking Web Application](#spendit---personal-budget-tracking-web-application)
  - [Table of Contents](#table-of-contents)
  - [Introduction](#introduction)
  - [Functional Requirements](#functional-requirements)
  - [Non-functional Requirements](#non-functional-requirements)
  - [Iterations](#iterations)
    - [I Iteration](#i-iteration)
    - [II Iteration](#ii-iteration)
    - [III Iteration](#iii-iteration)
    - [IV Iteration](#iv-iteration)
  - [Technologies Used](#technologies-used)
  - [Launch Instructions](#launch-instructions)
  - [Changes Made During Development](#changes-made-during-development)
  - [Notes](#notes)
## Introduction
The purpose of this personal budget tracker is to help individuals monitor and manage their income
and expenses. This tool allows individuals to see exactly where their money is being spent, identify
areas where they can reduce their spending, and create a plan to reach their financial goals. A
personal budget tracker can help individuals get a better understanding of their financial situation
and make informed decisions about their money, such as saving for the future, paying off debt, or
investing in assets. By tracking their spending, individuals can also identify trends and patterns
in their behavior that may be contributing to financial difficulties, and make changes to improve
their financial well-being.
## Functional Requirements
1. User authentication:
   - The application should allow users to create accounts and log in to access their personal budget information.
2. Transactions management:
   - A user-friendly interface to input and manage their financial transactions such as income, expenses, and transfers.
3. Budget tracking:
   - Users should be able to create budgets for different time periods (e.g. monthly, quarterly, annually) A system should the user’s budget by categorizing their transactions and providing an overview of their spending and savings towards these budgets.
4. Recurring transactions:
   - Users should be able to set up recurring transactions, such as monthly bills, so that they can easily track their expected expenses over time.
5. Categories management:
   - A system to manage and categorize the user’s transactions, such as food, entertainment, housing, etc. Also users should be able to add, edit, and delete categories to suit their specific needs.
6. Reports and insights:
   -  A system to generate reports and insights into the user’s spending patterns and help identify areas where they can save.
7. Alerts and notifications:
   -  A system to set alerts and notifications for budget limits, unusual spending, and other financial events.
8. Mobile compatibility:
   -  A mobile-friendly interface that allows users to access the web application and manage their budget from their mobile devices.
9.  User account management:
    - A system to allow users to manage their account details, such as password, email, and personal information.
10. Goal setting:
    - A system to set and track financial goals, such as saving for a down payment on a house or paying off debt.
11. Bill reminders:
    - Users should be able to set reminders for upcoming bills, so that they can keep track of when payments are due and avoid late fees.

## Non-functional Requirements
1. Performance:
   - The application should respond quickly and efficiently, even when handling large amounts of data.
2. User experience:
   - The application should have an intuitive and user-friendly interface, with clear and concise navigation and control buttons.
3. Security:
   - The application should implement robust security measures to protect sensitive user data, such as encryption and access control.
4. Availability:
   - The application should have high availability and minimal downtime, so that users can access their budget information whenever they need to.
5. Accessibility:
   - The application should be able to be accessed and operated from a mobile phone, a tablet, a laptop or a personal computer.

## Iterations
### I Iteration
- [x] User authentication
- [x] User account management
- [x] Transactions management
### II Iteration
- [x] Recurring transactions
- [x] Categories management
- [x] Goal setting
### III Iteration
- [x] Budget tracking
- [x] Reports and insights
### IV Iteration
- [x] Bill reminders
- [x] Alerts and notifications
- [x] Mobile compatibility
## Technologies Used
- Frontend
  - HTML
  - SCSS
  - Bootstrap v5.3
  - SweetAlert2
  - JavaScript
- Backend:
  - Blazor Server Web Application with SignalR
- REST API:
  - Blazor Server Web API
  - Entity Framework
- ASP.NET version:
  - .NET 7.0
- Database:
  - MariaDB
## Launch Instructions
1. Clone repo or download as archive
2. Open the solution in Visual Studio
3. In "REST API" project open Program.cs
4. Set `bool DevMode` to `true` to enable pre-filled in-memory database (or use `false` to use external MariaDB database)
5. In "Configure Startup Projects" make sure both projects are started at the same time
6. Run the project in debugger

**If you use external database (DevMode is false) - set connection string in appsettings.json as per appsettings.json.example!**
## Changes Made During Development
- Changed from using Razor pages for frontend and Blazor for backend to Blazor integrated with SignalR for server-side rendering
- Implemented User authorization using AspNetCore Authorization instead of Auth0 or oAuth for easier authentication processing
- Decided to develop both, Website and REST API using .NET 7 insdead of .NET 6 because of package dependency incompatibility issues
- For API integrated EntityFramework instead of making SQL calls to the database for ease of use and safety reasons (plus this approach allows to setup in-memory database for easier development of API controllers)
- Removed "Multi-currency support" from the plan
- Added "Bills" to database model
- Changed database from MySQL to MariaDB due to MySQL .NET connector being extremely unstable when running in other environments other than Windows OS

## Notes
- 'main' branch is meant to be treated as a "production" branch
- 'development' branch is used to save progress
