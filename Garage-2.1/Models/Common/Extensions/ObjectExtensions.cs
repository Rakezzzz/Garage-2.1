using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Common.Extensions
{
    public static class ObjectExtensions
    {

        /// <summary>
        /// Overrides all the properties of the object from a second object. 
        /// Effectively performing a shallow clone.
        /// </summary>
        /// <param name="to">The object to be overwritten.</param>
        /// <param name="from">The object to be cloned.</param>
        public static void OverrideProperties(this Object to, Object from)
        {
            PropertyInfo[] properties = from.GetType().GetProperties();

            foreach (PropertyInfo pi in properties)
            {
                if (pi.CanWrite)
                {
                    pi.SetValue(to, pi.GetValue(from, null), null);
                }
            }
        }


        /// <summary>
        /// Checks to see if provided Type has a property with the provided name.
        /// </summary>
        /// <param name="item">The Type on which the check is to be performed on.</param>
        /// <param name="propertyName">The name of the property.</param>
        /// <returns>Returns true if the Type has the property or false if not.</returns>
        public static bool HasProperty(this Type item, string propertyName)
        {
            return
                item.GetProperties()
                    .Select(p => p.Name)
                    .Contains(propertyName);
        }


        /// <summary>
        /// Searches a property of inidividual items.
        /// </summary>
        /// <typeparam name="T">The Type of the object to search.</typeparam>
        /// <param name="item">The Object to search.</param>
        /// <param name="property">The Property to search.</param>
        /// <param name="value">The Value to search for.</param>
        /// <returns>Returns true if property contains the value (or part of it).</returns>
        public static bool PropertyContains<T>(this T item, string property, string value)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            if (!typeof(T).HasProperty(property))
                throw new ArgumentException("Provided Search Property is not present in Type.");

            return typeof(T).GetProperty(property)
                                    .GetValue(item, null).ToString()
                                    .Contains(value);
        }
    }
}