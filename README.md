# App Enhance
This project is group of correct (in my opition) programing practicies 
which helps to build better applications with small effort. It address 
common problems which appears in real applications and tries implement wide
known solutions as a ready to use components like DDD building blocks, CQRS, 
events mechanism etc.

Most of components in this library are **abstractions** over technical problem
like dependency injection, logging or transactions and they describe teoretical 
aspects in the most simpliest way.

**Status:** starting project, composition of everything can be changed :)  
Any sugestions will be welocme

## Fundamental assumptions of the library:
* No dependencies
* Simplicity
* Extensible
* IoC based
* Easy to use
* Only technical problems
* Messages over RPC
* Less coupling
* Async

## AE.Core
This is the base for other components with low level abstractions arising from assumptions.  
Contains abstractions and default implementations for:
* Logging 
* Dependency Injection
* Transactions

## AE.Data
Contains abstractions for:
* Document/object repository
* Query framework

## AE.Commands
Commands framework

## AE.Events
Events framework

## AE.DDD
Contains domain driven design components:
* Aggragate root
* Entiy
* Value objects

