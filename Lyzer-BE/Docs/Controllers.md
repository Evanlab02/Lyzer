**Creating a new controllers**
- In `API.Controllers`:
	- Create your new controller that implements ControllerBase.
	- Above your class, below the namespace, add your route.
		`\[Route("api/{route-name}")]`
		`\[ApiController]`
	- Below your class name, add whatever dependency injections you need from any services as a private readonly variable.
		- e.g. `private readonly IDriverService _driverService;
	- In your constructor:
		- public Controller(IDriverService driverService)
			{
				_driverService = driverService;
			}
	- (Read the services.md to understand why we are using the interface instead of the concrete service)
- Endpoint methods:
	- Above your endpoint method in the controller, add the following property, with a comment above stating an example of the full endpoint URL.
		`// GET api/driver/5`
		`[HttpGet("{id}")]`
	- If your endpoint is returning an object, ensure that you have created a DTO accordingly.
**Things to note**
- Ensure that your service is aptly named, based on what it is going to be used for
	- e.g. DriverController has all the endpoints that are driver related.
