<h1>In-Memory User Api</h1>

Application that creates an in memory database for:
- Storing users 
- Fetching all users
- Fetching user by ID
- Putting new usernames on a specific ID 
- Deleteing users by ID. 
\n This uses a swagger interface to interact with the database.  Advantages over a traditional database on hard-disk or ssd storage devices is that an in-memory database has much faster access.

<h2>Running the application</h2>
1. James Madison
2. James Monroe
3. John Quincy Adams
1. Clone the repository
2. Install DOTNET 6.0 https://dotnet.microsoft.com/en-us/download
3. To run executable use run.sh
4. To run tests and generate coverage report as html document with pages for each class. Use run_all_tests_with_coverage.sh in the UserApi.Tests directory. Then open the coverage_rendered directory and open the html page on localhost.

<h2>The Swagger UI</h2>

<img src="/instructions/1.png" alt="Alt text" title="Optional title">
Creates a swagger UI on localhost port 5000 by default. The UI can be used to interact with the database or copy and past CURL commands into an editor

<h2>Accessing the API without the UI</h2>
<div>
<img align=top src="/instructions/2.png" alt="Alt text" title="Optional title">
<img align=top src="/instructions/3.png" alt="Alt text" title="Optional title">
</div>
Get request sent from postman
Curl request sent from zsh terminal.

<h2>Interactive Coverage report</h2>
<img src="/instructions/4.png" alt="Alt text" title="Optional title">
Coverage report created by ReportGenerator with clickable links to each individual file to see line by line coverage.</div>
