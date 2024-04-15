using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace LSL.Tags.Tests
{
    public class BuildTagsTests
    {
        [Test]
        public void GivenATag_ThenItShouldReturnTheExpectedList()
        {
            BuildTags.With.Tag("my-tag").Build()
                .Should()
                .BeEquivalentTo(new[] 
                {
                    "my-tag"
                });
        }

        [Test]
        public void GivenAnExplicitEmptyCall_ThenItShouldReturnNoTags()
        {
            BuildTags.Empty().Should().BeEmpty();
        }

        [Test]
        public void GivenNoTags_ThenItShouldThrowTheExpectedException()
        {
            new Action(() => BuildTags.With.Build())
                .Should()
                .Throw<ArgumentException>();
        }

        [Test]
        public void GivenTwoOfTheSameTag_ThenItShouldReturnJustOneTag()
        {
            BuildTags.With.Tag("my-tag").Tag("my-tag").Build()
                .Should()
                .BeEquivalentTo(new[] 
                {
                    "my-tag"
                });
        }

        [Test]
        public void GivenMultipleTags_ThenItShouldReturnTheExpectedList()
        {
            BuildTags.With
                .Tag("my-tag")
                .Tag("another-tag")
                .Build()
                .Should()
                .BeEquivalentTo(new[] 
                {
                    "my-tag",
                    "another-tag"
                });
        }

        [Test]
        public void GivenTwoOfTheSameTagWithDifferentCase_ThenItShouldReturnTwoDistinctTags()
        {
            BuildTags.With.Tag("my-tag").Tag("my-Tag").Build()
                .Should()
                .BeEquivalentTo(new[] 
                {
                    "my-tag",
                    "my-Tag"
                });
        }        

        [TestCase("my-key", "my-value", "my-key:my-value")]
        [TestCase("my:key", "my-value", "my%3akey:my-value")]
        [TestCase("my-key", "my:value", "my-key:my%3avalue")]
        [TestCase("my:key", "my:value", "my%3akey:my%3avalue")]
        [TestCase("my:key%", "%my:value", "my%3akey%25:%25my%3avalue")]
        [TestCase("my:key%25", "%my:value", "my%3akey%2525:%25my%3avalue")]
        [TestCase("my:key%3a", "%my:value", "my%3akey%253a:%25my%3avalue")]
        public void GivenAKeyValueTag_ThenItShouldReturnTheExpectedList(string key, string value, string expectedResult)
        {
            BuildTags.With.KeyAndValueTag(key, value).Build()
                .Should()
                .BeEquivalentTo(new[] 
                {
                    expectedResult
                });
        }

        [Test]
        public void GivenACallToAddAnApplicationTagForAGivenType_ThenItShouldAddTheExpectedTag()
        {
            BuildTags.With.ApplicationTagForAssemblyOfType(typeof(BuildTagsTests))
                .Build()
                .Should()
                .BeEquivalentTo(new[] 
                {
                    "Application:LSL.Tags.Tests"
                });
        }

        [Test]
        public void GivenACallToAddAnApplicationTagForAGivenGenericType_ThenItShouldAddTheExpectedTag()
        {
            BuildTags.With.ApplicationTagForAssemblyOf<BuildTagsTests>()
                .Build()
                .Should()
                .BeEquivalentTo(new[] 
                {
                    "Application:LSL.Tags.Tests"
                });
        }        

        [TestCase("key:value", "key", "value")]
        [TestCase("my%3akey:value", "my:key", "value")]
        [TestCase("my%3akey:value%3a1", "my:key", "value:1")]
        [TestCase("my%3akey%25:%25value%3a1", "my:key%", "%value:1")]
        [TestCase("%253a:%25value%3a1", "%3a", "%value:1")]
        [TestCase("%2525:%25value%3a1", "%25", "%value:1")]
        public void Parse_GivenAnEncodedKeyValueTag_ThenItShouldReturnTheExpectedResult(string encodedValue, string expectedKey, string expectedValue)
        {
            TagBuilder.ParseKeyAndValue(encodedValue)
                .Should()
                .BeEquivalentTo(new KeyValuePair<string, string> (expectedKey, expectedValue));
        }
    }
}
