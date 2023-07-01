# Driver service

The driver service is responsible for handling all requests related to drivers. At this stage of time the driver service is
not fully implemented. The driver service was setup as a proof of concept of how we can receive data from the API. The driver service
currently only can has one function.

This function is GetDriverById which will return info on the Max Verstappen that is not at all accurate.
This is a hardcoded value and will be changed in the future.

## GetDriver

This function will return a driver object with the given id. The driver object will contain the following properties (example values are given in parentheses):
    - Id (1)
    - Name (Max Verstappen)
    - Age (23)
    - Points (278)
    - Team (Red Bull Racing)
