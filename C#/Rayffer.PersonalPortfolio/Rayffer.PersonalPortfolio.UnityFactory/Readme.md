# Unity used as a Factory

This project is a proof of concept of the various ways **unity can be used as a factory**, as such no behaviour for any of the interface's implementations is provided as this is just a proof of concept that unity works as a factory.

By using unity as a factory the abstraction of said factory is very high, since unity constructs the classes from its dependencies which are registered in it, giving a **really high flexibility and ease of change** that conventional factories do not provide, while also **minimizing the code** that the factory requires.

The resolution can be done using any dependency resolution strategy:

* constructor injection.
* property injection.
* unity injection constructor.
* any other way of resolving dependencies.

The main drawback is that **the factory strategy is obscured** and some previous knowledge of unity is required to comprehend how dependencies are resolved.
