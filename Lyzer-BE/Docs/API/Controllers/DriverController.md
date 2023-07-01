# Driver Controller

The driver controller is responsible for handling all requests related to drivers. At this stage of time the driver controller is
not fully implemented. The driver controller was setup as a proof of concept of how we can receive data from the API. The driver controller
currently only can has one possible endpoint. 

This endpoint is api/driver/1 which will return info on the Max Verstappen that is not at all accurate. 
This is a hardcoded value and will be changed in the future.

## Base URL

The base URL for the driver controller is: `/api/driver`, this is where all the endpoints for the driver controller will be located.

## Endpoints

### Get Driver (GET /api/driver/{id})

This endpoint will return a driver object with the given id. The driver object will contain the following properties (example values are given in parentheses):
    - Id (1)
    - Name (Max Verstappen)
    - Age (23)
    - Points (278)
    - Team (Red Bull Racing)

## Service

The driver controller uses the driver service to retrieve the driver object.
