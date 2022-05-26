# How to reproduce bug
1. Run `dotnet-ef migrations add Initial -v`.
2. You can see in console that application has been started (which is not a desired behaviour).
3. Kill application with `ctrl+c`.
4. Migration is generated correctly.

Related issue: https://github.com/dotnet/efcore/issues/28105
