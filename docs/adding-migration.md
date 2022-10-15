# Adding Migration
1. Install packages:
    - `Microsoft.EntityFrameworkCore.Tools`
    - `Microsoft.EntityFrameworkCore.Design`
2. Configure Database provider(`Program.cs`):
    ```
    builder.Services.AddDbContext<TContext>(options =>
    {
        // I'm using SQL Server (RDBMS)
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection));
    })
    ```
3. Terminal commands:
    1. `dotnet ef migrations add <Commit>`
    2. `dotnet ef database update`