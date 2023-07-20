using System;
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
    }
}
