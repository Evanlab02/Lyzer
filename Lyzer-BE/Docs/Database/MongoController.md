# Mongo Controller

## Description

This controller is used to connect to the MongoDB database and perform CRUD operations on the database. This controller is used by several services to perform these operations.
MongoController can be built around many different models, hence the generic type. This allows us to use this controller for different models.

## Constructor

First and foremost in the constructor we initialize the MongoClient. This is done by using the connection string. This connection string is stored as an environment variable. The connection string is to be treated like a password and should not be shared with anyone. We initialize the MongoClient in the constructor because we only want to initialize it once. 

We pass the collection name to the constructor so we can use the controller for different collections.
There is a second constructor in which you can specify the database name as well.
