<h1>In-Memory User Api</h1>

Application that creates an in memory database for storing users, getting all users, getting user by ID, putting new usernames on a specific ID and deleting users by ID. This uses a swagger interface to interact with the database.  Advantages over a traditional database on hard-disk or ssd storages is that an in-memory database has much faster access.

To run executable use run.sh
To run tests and generate coverage report as html document with pages for each class. Use run_all_tests_with_coverage.sh in the UserApi.Tests directory. Then open the coverage_rendered directory and open the html page on localhost.


<img src="/instructions/1.png" alt="Alt text" title="Optional title">
Creates a swagger UI on localhost port 5000 by default. The UI can be used to interact with the database or copy and past CURL commands into an editor
<img src="/instructions/2.png" alt="Alt text" title="Optional title">
Get request sent from postman

<div style="display: flex; flex-direction: horizontal">
<img src="/instructions/3.png" alt="Alt text" title="Optional title">
Curl request sent from zsh terminal.


<img src="/instructions/4.png" alt="Alt text" title="Optional title">
Coverage report created by ReportGenerator with clickable links to each individual file to see line by line coverage.</div>
