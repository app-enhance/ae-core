# App Enhance
This project helps to build better applications with small effort. It address 
common problems which appears in real applications and tries implement wide
known solutions as a ready to use components like DDD building blocks, CQRS, 
events mechanism etc.

Most of components in this library are **abstractions** over technical problem and they describe teoretical 
aspects in the most simpliest way.

**Status:** starting project, composition of everything can be changed :)  
Any sugestions will be welocme

## Fundamental assumptions of the library:
* No dependencies
* Simplicity
* Extensible
* IoC based
* Easy to use
* Less coupling
* Async
* Don't reinvent the wheel 

## Packages

| Package     | Description                                                                                                                                                             |
|-------------|-------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| AE.Core     | This is the base for other components with low level abstractions and implementations arising from assumptions. Contains something like DI, Logging, Transactions. [More](https://github.com/app-enhance/ae-core/wiki/AE.Core) |
| AE.Events   | Events framework. [More](https://github.com/app-enhance/ae-core/wiki/AE.Events)                                                                                                                                                 |
| AE.Data     | Query framework and abstractions over document repository. [More](https://github.com/app-enhance/ae-core/wiki/AE.Data)                                                                                                         |
| AE.Commands | Commands framework. [More](https://github.com/app-enhance/ae-core/wiki/AE.Commands)                                                                                                                                                |
| AE.DDD      | Domain Driven Design base components and useful patterns. [More](https://github.com/app-enhance/ae-core/wiki/AE.DDD)                                                                                                          |
### Contribute
Before pushing new feature or improvement please read [CONTRIBUTING.md](https://github.com/app-enhance/ae-core/blob/master/CONTRIBUTING.md)
