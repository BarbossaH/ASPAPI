# ASPAPI

# dependency injection
1. we will inject services which are the logic dealing with the response, map data between the DTOs and the original data model, save data into the database, etc.
2. notice the life cycle of dependency injection. I am not so sure how to use that in all kinds of scenarios.
# response
including DTOs and other information, such as "success, error"  and other necessary and custom data

# MAP
to separate the original from transferred data, using MAP can build the relationship between them, and also it can add some new data into DTOs, 
which won't influence the original data. 
