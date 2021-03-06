﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Common.Extensions
{
    public static class CollectionExtensions
    {
        /// <summary>
        /// Selects the objects that contain the searchstring on the selected property.
        /// </summary>
        /// <typeparam name="T">The type of the object that contains the desired property.</typeparam>
        /// <param name="source">The source to perform the query on.</param>
        /// <param name="property">The property to be queried.</param>
        /// <param name="value">The value to search for.</param>
        /// <returns>Returns an IEnumerable containing the queried objects.</returns>
        public static IEnumerable<T> CollectionPropertyContains<T>(this IEnumerable<T> source, string property, string value)
        {
            if (source == null)
                throw new ArgumentNullException("collection");

            return source
                .Where(item => item.PropertyContains<T>(property, value));
        }
    }
}