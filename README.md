# SimpleCQRSDotCore

## Introduction

Simple Unambitious cqrs.

## Usage

In StartUp
```
services.AddSimpleCQRS(Assembly.GetExecutingAssembly());
```

then just use interfaces: ICommand, ICommandHandler, IQuery, IQueryHandler

for commands definitions and handlers.

and use:

ICommandDispatcher, IQueryProcessor to execute them