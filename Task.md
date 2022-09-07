Create C# Console application, that can read csv attached.
App functionality:
1.	Read and save entries from file into DB (can be MSSQL, PSQL, MySQL, etc., in production Azure SQL would be used)
	1. It can be done in-memory without saving to DB, but models are mandatory in each case
	2. If non In-Memory saving is used, DB schema should be added to task for review
2.	Entries on Upsert need to have 2 additional columns added
    1.	HR Status - Based on termination date and in training column
        1.	Non terminated employees should have status Active
        2.	In training Employees should have status InTraining
        3.	Terminated employees should have status Terminated
    2.	Company email
        1.	Should be formed using first letter of firstname + lastname + @ibsat.com
3.	Console app should have option to search employees by entering partial email or partial first name or partial last name
    1.	In case of multiple entries a list of max 5 entries should be shown
    2.	Each entry should have a employee data card shown in console app