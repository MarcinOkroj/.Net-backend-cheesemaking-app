# .Net backend cheeseMaking app
This .NET backend provides full **CRUD** functionality for managing recipes. Users can:

- `Add`, `read`, `update`, and `delete` recipes.
- Mark recipes as favourites and manage their list of favourite recipes.
- Add and remove comments on recipes.
- The project includes user registration and `login` features, along with robust `authentication` and `authorization` mechanisms using `JWT claims`. It's designed to handle user-specific actions.

## How to run
- start a SQL database. I used Microsoft SQL Server
- in `appsettings.json` change `DefaultConnection` to reflect your server and database names `Server=MSI\\SQLEXPRESS; Database=cheeseMaking;`
- go to `api` directory
- in `/api` directory run `dotnet ef migrations add migrationName`
- run `dotnet ef database update`
- and to start app run `dotnet watch run`. It will also automatically open Swagger to test application

![image](https://github.com/user-attachments/assets/462e04e9-979e-4510-a76a-1e3a17d7aa11)


### Quick presentation of the application - not every method
https://github.com/user-attachments/assets/a1d8b637-3cad-4973-85d6-02681b77f31d

