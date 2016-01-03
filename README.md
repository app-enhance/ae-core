# App Enhance
<img src="https://camo.githubusercontent.com/1d001058924ce9b3bcd0b73083540e3dc9abf61d/687474703a2f2f6e6f72646963617069732e636f6d2f77702d636f6e74656e742f75706c6f6164732f54726176697343492d35307835302e706e67" alt="Travis CI" data-canonical-src="http://nordicapis.com/wp-content/uploads/TravisCI-50x50.png" width="32" height="32" style="max-width:100%;"> [![Build Status](https://travis-ci.org/app-enhance/ae-core.svg)](https://travis-ci.org/app-enhance/ae-core)

<img src="https://camo.githubusercontent.com/ec5f039ffeae8536ae92c8914a9d56a1b9bac1ae/68747470733a2f2f6170707665796f722e67616c6c6572792e76736173736574732e696f2f5f617069732f7075626c69632f67616c6c6572792f7075626c69736865722f6170707665796f722f657874656e73696f6e2f7673732d73657276696365732d6170707665796f722f302e302e39302e352f617373657462796e616d652f696d616765732f6170707665796f722d6c6f676f2d6c617267652e706e67" alt="appv" data-canonical-src="https://appveyor.gallery.vsassets.io/_apis/public/gallery/publisher/appveyor/extension/vss-services-appveyor/0.0.90.5/assetbyname/images/appveyor-logo-large.png"  width="32" height="32" style="max-width:100%;">
[![Build status](https://ci.appveyor.com/api/projects/status/yjktwfhrd9can0af/branch/master?svg=true)](https://ci.appveyor.com/project/Ermesx/ae-core/branch/master)

This project helps to build better applications with small effort. It address 
common problems which appear in real applications and tries to implement wide
known solutions as ready to use components like DDD building blocks, CQRS, 
events mechanism etc.

Most of components in this library are **abstractions** over technical problem and they describe teoretical 
aspects in the most simpliest way.

**Status:** starting project, composition of everything can be changed :)  
Any suggestions are welocome

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
## Contribute
Before pushing new feature or improvement please read [CONTRIBUTING.md](https://github.com/app-enhance/ae-core/blob/master/CONTRIBUTING.md)
