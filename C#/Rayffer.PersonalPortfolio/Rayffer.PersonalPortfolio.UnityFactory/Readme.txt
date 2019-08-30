This project is a demo of the various ways unity can be used as a factory.

The resolution can be done through using any dependency resolution strategy:
* constructor injection.
* property injection.
* unity injection constructor.
* any other way of resolving dependencies.

Using unity as the factory, the abstraction of said factory is very high, since unity constructs the classes from its dependencies which are registered in the used unity container, giving a really high flexibility and ease of change that conventional factories do not provide, while also minimizing the code required for the factory.

The main drawback is that the factory strategy is obscured and some previous knowledge of unity is required to comprehend how dependencies are resolved.
