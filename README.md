# Books Management Application

This project is a .NET application for managing a list of books, including a REST API, GraphQL API, and an Angular frontend.

## Getting Started

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) (version 8.0)
- SQL Server (LocalDB) (You most likely have)

### Installation

1. **Clone the repository:**

   ```bash
   git clone https://github.com/qudusG/Books.git
   cd Books.API
   dotnet restore

   or
   Launch the project with visual studio which would usually restore the packages automatically

   Auto migration is enabled. This would apply the migration file to your localdb.

   Running the Application
    Run Backend (API):
    
    cd Books.API
    dotnet run
