ECHO OFF

ECHO ------------------------
ECHO CREATING DATABASE
ECHO ------------------------

sqlcmd -S localhost -E -i test_sp.sql

ECHO ------------------------
ECHO DATABASE CREATED
ECHO ------------------------



rem server is localhost

ECHO .
ECHO if no errors appear DB was created
PAUSE