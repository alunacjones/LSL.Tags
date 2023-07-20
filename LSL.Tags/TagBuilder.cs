using System;
using System.Collections.Generic;
using System.Linq;

namespace LSL.Tags
{
    /// <summary>
    /// The main class that starts the fluent build
    /// </summary>
    public class TagBuilder
    {
        private ISet<string> _tags = new HashSet<string>();

        /// <summary>
        /// Add a tag to the TagBuilder instance
        /// </summary>
        /// <param name="tag"></param>
        /// <returns>The original TagBuilder</returns>
        public TagBuilder Tag(string tag)
        {
            _tags.Add(tag);
            return this;
        }

        /// <summary>
        /// Generates the IEnumerable of strings that represent the tags
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">Thrown when no tags have been setup. Use TagBuilder.Empty() if you intend to have an empty set of tags</exception>
        public IEnumerable<string> Build() => _tags.Any() ? _tags : throw new ArgumentException("TagBuilder requires at least one tag to be defined to build a tag list. Use TagBuilder.Empty() if you intend to have an empty tag list");
    }
}