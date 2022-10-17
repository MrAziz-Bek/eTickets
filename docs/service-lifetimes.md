# Service Lifetimes
“When we register services in a container, we need to set the lifetime that we want to use. The service lifetime controls how long a result object will live for after it has been created by the container. The lifetime can be created by using the appropriate extension method on the IServiceColletion when registering the service.”

There are three lifetimes that can be used with Microsoft Dependency Injection Container, they are:

- Transient — Services are created each time they are requested. It gets a new instance of the injected object, on each request of this object. For each time you inject this object is injected in the class, it will create a new instance.
- Scoped — Services are created on each request (once per request). This is most recommended for WEB applications. So for example, if during a request you use the same dependency injection, in many places, you will use the same instance of that object, it will make reference to the same memory allocation.
- Singleton — Services are created once for the lifetime of the application. It uses the same instance for the whole application.

The dependency injection container keeps track of all instances of the created services, and they are disposed of or released for garbage collector once their lifetime has ended. This is how we can configure the DI in .NET core:
```
public void ConfigureServices(IServiceCollection services)
{
    services.AddTransient<IMyDependencyA, MyDependencyA>();
    services.AddSingleton<IMyDependencyB, MyDependencyB>();
    services.AddScoped<IMyDependencyC, MyDependencyC>();
}
```
> Depending on how the lifetime of an operation’s service is configured for the following interfaces, the container provides either the same or different instances of the service when requested by a class.

© [Medium: henriquesd](https://henriquesd.medium.com/dependency-injection-and-service-lifetimes-in-net-core-ab9189349420)