# Coding-Guideline

## Names
    Use PascalCase for type names.
    Do not use "I" as a prefix for interface names.
    Use PascalCase for enum values.
    Use camelCase for function names.
    Use camelCase for property names and local variables.
    Do not use "_" as a prefix for private properties.
    Use whole words in names when possible.

## Components
    1 file per logical component (e.g. parser, scanner, emitter, checker).
    files with ".generated.*" suffix are auto-generated, do not hand-edit them.

## Types
    Do not export types/functions unless you need to share it across multiple components.
    Do not introduce new types/values to the global namespace.
    Shared types should be defined in 'types.ts'.
    Within a file, type definitions should come first.

## null and undefined
    Use undefined. Do not use null.

## General Assumptions
    Consider objects like Nodes, Symbols, etc. as immutable outside the component that created them. Do not change them.
    Consider arrays as immutable by default after creation.

## Flags
    More than 2 related Boolean properties on a type should be turned into a flag.

## Comments
    Use JSDoc style comments for functions, interfaces, enums, and classes.

## Strings
	Use single-quotes ' for all strings, and use double-quotes " for strings within strings.
	When you can't use double quotes, try using back ticks (`).
	
## Arrays
	Annotate arrays as foos:Foo[] instead of foos:Array<Foo>.

## Constructs in Typescript
    Consider not to use for..in statements; instead, use ts.forEach, ts.forEachKey and ts.forEachValue. Be aware of their slightly different semantics.
    Try to use ts.forEach, ts.map, and ts.filter instead of loops when it is not strongly inconvenient.

## Files
    All TypeScript files must have a ".ts" extension.
    They should be all lower case, and only include letters, numbers, and periods.
    It is even recommended to separate words with periods (e.g. my.view.ts).
    All files should end in a new line. This is necessary for some Unix systems.

	
## Style
    Only surround arrow function parameters when necessary.
    For example, (x) => x + x is wrong but the following are correct:
        x => x + x
        (x,y) => x + y
        <T>(x: T, y: T) => x === y
    Always surround loop and conditional bodies with curly braces. Statements on the same line are allowed to omit braces.
    Open curly braces always go on the same line as whatever necessitates them.
    Parenthesized constructs should have no surrounding whitespace.
    A single space follows commas, colons, and semicolons in those constructs. For example:
        for (var i = 0, n = str.length; i < 10; i++) { }
        if (x < 10) { }
        function f(x: number, y: string): void { }
    Use a single declaration per variable statement
    (i.e. use var x = 1; var y = 2; over var x = 1, y = 2;).
    else goes on a separate line from the closing curly brace.
    Use 2 spaces per indentation.


### Taken and adopted from https://github.com/Microsoft/TypeScript/wiki/Coding-guidelines
### + https://github.com/basarat/typescript-book/blob/master/docs/styleguide/styleguide.md