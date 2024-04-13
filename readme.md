[![Build status](https://img.shields.io/appveyor/ci/alunacjones/lsl-tags.svg)](https://ci.appveyor.com/project/alunacjones/lsl-tags)
[![Coveralls branch](https://img.shields.io/coverallsCoverage/github/alunacjones/LSL.Tags)](https://coveralls.io/github/alunacjones/LSL.Tags)
[![NuGet](https://img.shields.io/nuget/v/LSL.Tags.svg)](https://www.nuget.org/packages/LSL.Tags/)

# LSL.Tags

This package provides a static class to build an enumerable list of strings that represent tags.

## Quick Start

```csharp

// basic setup example
var tags = BuildTags.With.Tag("my-tag").Build();
// tags will contain ["my-tag"]

// adding multiple tags
var tags2 = BuildTags.With.Tag("tag1").Tag("tag2").Build();
// tags2 will contain ["tag1", "tag2"]

// adding tags with a key and value
var tags3 = BuildTags.With.KeyAndValueTag("my-key", "my-value").Build();
// tags3 will contain ["my-key:my-value"]

// adding multiple tags with a key and value
var tags4 = BuildTags.With
    .KeyAndValueTag("my-key", "my-value")
    .KeyAndValueTag("my-other-key", "my-other-value")
    .Build();    
// tags4 will contain ["my-key:my-value", "my-other-key:my-other-value"]

// adding tags with a colon in the provided key and value
var tags5 = BuildTags.With.KeyAndValueTag("my-key:with-colon", "my-value:with-colon").Build();
// tags5 will contain ["my-key%3awith-colon:my-value%3awith-colon"]

// attempting to build an empty tag list
var tags6 = BuildTags.With.Build();
// this will throw an ArgumentException due to no tags being provided

// Build an empty tag list
var tags7 = BuildTags.Empty();
// this will return an empty tag list as we have explicitly asked for it
// NOTE: Enumerable.Empty<string>() could also be used here

// Setting an application tag using the Assembly name of a given type:
var tags = BuildTags.ApplicationTagForAssemblyOfType(typeof(Program))
    .Build();
// tags will contain ["Application:MyAssemblyName"]
// NOTE: This assumes the name of the assemby containing Program is called MyAssemblyName

// Setting an application tag using the Assembly name of a given type:
var tags = BuildTags.ApplicationTagForAssemblyOf<Program>()
    .Build();    
// tags will contain ["Application:MyAssemblyName"]
// NOTE: This assumes the name of the assemby containing Program is called MyAssemblyName

// Parsing an encoded key/value tag
var keyValuePair = TagBuilder.Parse("my-key:my-value");
// keyValuePair.Key == "my-key"
// keyValuePair.Value == "my-value"
```
