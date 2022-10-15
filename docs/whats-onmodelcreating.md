# What's `OnModelCreating` method in EFCore?
- The `DbContext` class has a method called `OnModelCreating` that takes an instance of `ModelBuilder` as a parameter. This method is called by the framework when your context is first created to build the model and its mappings in memory. You can override this method to add your own configurations: