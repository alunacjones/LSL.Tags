using System;

namespace LSL.Tags
{
    /// <summary>
    /// TagBuilderExtensions
    /// </summary>
    public static class TagBuilderExtensions
    {
        internal const string ApplicationKey = "Application";

        /// <summary>
        /// Adds a key and value tag in the format of "{key}:{value}".
        /// If the key or value have a colon in them then it will be encoded as "%3a"
        /// </summary>
        /// <param name="source"></param>
        /// <param name="key">The key</param>
        /// <param name="value">The value</param>
        /// <returns>The original TagBuilder</returns>
        public static TagBuilder KeyAndValueTag(this TagBuilder source, string key, string value) =>
            source.Tag($"{EncodeStringForKeyOrValue(key)}:{EncodeStringForKeyOrValue(value)}");

        /// <summary>
        /// Adds a tag in the format "{tagKey}:{ContainingAssemblyNameOfTheProvidedType}"
        /// </summary>
        /// <param name="source"></param>
        /// <param name="type">The type whose assembly will be used in the tag</param>
        /// <param name="tagKey">The key for the tag</param>
        /// <returns>The original TagBuilder</returns>
        public static TagBuilder ApplicationTagForAssemblyOfType(this TagBuilder source, Type type, string tagKey = ApplicationKey) =>
            source.KeyAndValueTag(tagKey, type.Assembly.GetName().Name);

        /// <summary>
        /// Adds a tag in the format "{tagKey}:{ContainingAssemblyNameOfTheGenericTypeProvided}"
        /// </summary>
        /// <param name="source"></param>
        /// <param name="tagKey">The key to use for the tag</param>
        /// <typeparam name="T">The type whose assembly will be used in the tag</typeparam>
        /// <returns></returns>
        public static TagBuilder ApplicationTagForAssemblyOf<T>(this TagBuilder source, string tagKey = ApplicationKey) =>
            source.ApplicationTagForAssemblyOfType(typeof(T));

        private static string EncodeStringForKeyOrValue(string encodee) => encodee.Replace(":", "%3a");            
    }
}