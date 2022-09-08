This task was writen loosly, giving a lot of place for your imagination.
Also I was mentioned by HR that it is estimated 30 min for task, so I did not overthink everything.
That said, here are some cut corners:

1. No unit/integration tests.
2. Database structure is not real world application (country list, departments etc are not stored as foreign keys), everything is stored in a single table.
3. Only unique key validation on insert (you can have a Wakanda as a country, duplicate emails etc).
4. Search is performed across all fields simulteniously, maybe you wanter a selector for search option by specific column.
5. I assumed case insensitive search.
6. I did not know how you expected to reference csv file, drag and drop on executable to get file path or something else. Ended up simply hardcoding it.
7. In real app some of the Consts should be moved/stored in appsettings.json (or other config file) or database altogether.

P.S. In VS Code integratedTerminal clears console in a weird way I suggest launching executible after build.
