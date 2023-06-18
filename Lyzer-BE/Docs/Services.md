**Creating a service**:
- In `Services.Interfaces`:
	- Create your interface for the service that you want to create
	- Name: I{ServiceName}Service
		- e.g. IDriverService
- In `Services.Concrete`:
	- Create your concrete service that is going to be used.
		- Ensure that it implements the interface service that you created
	- Name: {ServiceName}Service
		- e.g. DriverService
- In `Program.CS`, the main file that gets run on startup:
	- Update the `ConfigureServices` function
		- Add your newly created service to the scope using:
			- `builder.Services.AddScoped<I{ServiceName}Service, {ServiceName}Service>();`
			- ^ This creates a new instance of the service that you wanting to use, whenever an interface of said service is injected.

**Things to note**
- Ensure that your service is aptly named, based on what it is going to be used for
	- e.g. DriverService has all the methods that are related to updating, getting, or creating drivers.
- All public methods are to be added to the interface, but private methods don't need to be added to the interface.