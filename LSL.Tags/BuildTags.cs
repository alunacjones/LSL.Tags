using System.Collections.Generic;
using System.Linq;

namespace LSL.Tags
{
    /// <summary>
    /// The main static helper class to create tags
    /// </summary>
    public static class BuildTags
    {
        /// <summary>
        /// Creates a TagBuilder instance to fluently work with
        /// </summary>
        /// <returns>A TagBuilder instance to fluently add to</returns>
        public static TagBuilder With => new TagBuilder();

        /// <summary>
        /// Creates an empty tag list
        /// </summary>
        /// <returns>The empty tag list</returns>
        public static IEnumerable<string> Empty() => Enumerable.Empty<string>();
    }
}