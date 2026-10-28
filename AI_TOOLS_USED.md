# AI_TOOLS_USED.md

## AI Tools Used

AI tools were used as development assistants for understanding requirements, planning the application, generating code suggestions, and troubleshooting implementation issues. I reviewed and tested the generated code and made changes where necessary to meet the project requirements.

| AI Tool            | Version / Plan                           | Purpose                                                                                                                                  |
| ------------------ | ---------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------------- |
| ChatGPT (OpenAI)   | GPT-6, if applicable to the session used | Requirement analysis, database design, ASP.NET Core MVC implementation guidance, code generation, validation logic, and troubleshooting. |
| Claude (Anthropic) | Version/plan used during development     | Code generation, implementation assistance, and support with developing project features.                                                |

## Prompts Used

The following are representative prompts used during development. They should reflect the actual prompts and tasks used in the project.

### 1. Project Planning and Architecture

**Tool:** ChatGPT

**Prompt:**  
"Help me build a Hotel Room Booking System using ASP.NET Core MVC, C#, and SQL Server. Guide me step by step, prioritize a working application, and follow the required project structure."

**Purpose:**  
To understand the assignment requirements, plan the implementation, and organize the project according to the submission guidelines.

### 2. Database Design

**Tool:** ChatGPT

**Prompt:**  
"Design the database for a Hotel Room Booking System with Rooms, Bookings, and StaffUsers tables. Include primary keys, foreign keys, validation rules, and relationships."

**Purpose:**  
To establish the database schema and identify the relationships needed for room management and booking operations.

### 3. Room Booking Functionality

**Tool:** ChatGPT

**Prompt:**  
"Implement room listing, room details, and booking functionality in ASP.NET Core MVC using Entity Framework Core and SQL Server. Include server-side validation, total-price calculation, and checks to prevent overlapping bookings."

**Purpose:**  
To develop the main guest-facing functionality and implement essential booking business rules.

### 4. Staff Authentication and Room Management

**Tool:** Claude

**Prompt:**  
"Help implement staff login and room management in an ASP.NET Core MVC application using SQL Server. Include password hashing, cookie authentication, authorization for staff-only routes, and functionality to add rooms."

**Purpose:**  
To assist with implementing staff authentication and protected room-management functionality.

### 5. Debugging and Code Review

**Tool:** ChatGPT and/or Claude

**Prompt:**  
"Review the current implementation for errors, security issues, database problems, and missing validation. Suggest fixes while keeping the solution simple enough to complete within the assignment deadline."

**Purpose:**  
To identify potential issues, improve implementation quality, and verify the application against the assignment requirements.

## Changes and Verification Performed by Me

During development, I reviewed the proposed implementation and made or verified changes as needed, including:

- Organized the solution according to the required submission folder structure.
- Created the database tables and inserted sample room records through SQL queries.
- Integrated the application with SQL Server using Entity Framework Core.
- Reviewed the room-booking overlap condition and server-side validation.
- Integrated staff login and room-management functionality.
- Tested implemented features and addressed issues encountered during development.

_Update this section with the specific changes, fixes, and tests you personally completed. Do not claim testing or corrections that you have not performed._

## Limitations and Future Improvements

With more development time, the application could be improved by adding:

- Stronger protection against simultaneous booking requests.
- More comprehensive automated tests.
- Better authentication and account-management workflows.
- Booking history for guests and more complete staff booking management.
- Improved logging, error handling, and production security configuration.

## Declaration

AI tools were used to assist with planning, code generation, and troubleshooting. They were development aids; the submitted project should be reviewed, tested, and understood by me so that I can explain the implementation and design decisions during the project review.
