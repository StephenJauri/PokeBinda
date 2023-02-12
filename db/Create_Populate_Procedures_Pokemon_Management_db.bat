ECHO OFF

ECHO ------------------------
ECHO CREATING DATABASE
ECHO ------------------------

sqlcmd -S localhost -E -i Create_Pokemon_Management_db.sql

ECHO ------------------------
ECHO DATABASE CREATED
ECHO ------------------------

ECHO .

ECHO ------------------------
ECHO INSERTING RECORDS
ECHO ------------------------

sqlcmd -S localhost -E -i Populate_Pokemon_Management_db.sql

ECHO ------------------------
ECHO RECORDS INSERTED
ECHO ------------------------

ECHO .

ECHO ------------------------
ECHO ADDING STORED PROCEDURES
ECHO ------------------------

sqlcmd -S localhost -E -i Stored_Procedures_Pokemon_Management_db.sql

ECHO ------------------------
ECHO STORED PROCEDURES ADDED
ECHO ------------------------


rem server is localhost

ECHO .
ECHO if no errors appear DB was created
PAUSE