MunicipalityTax

A small application, which manages taxes applied in different municipalities. The taxes are scheduled in time. The application provides the ability to get taxes applied in
certain municipality at the given day.

The application consists of the following projects:

1. The Tax API Application (ServiceInterface project)
2. Tax Consumer API (MunicipalityTaxConsumer/ServiceInterface project)
3. OAuth IdentityServer
4. Tax Schedule Loader

Prerequisites:
A database must exists and can be created using the DDL in ResourceAccess/Database/CreateMunicipalityTaxDB.sql

In order to import municipality tax schedules, run [4] above.

In order to run the application, set the solution to multiple startup and choose 1, 2, and 3 above
