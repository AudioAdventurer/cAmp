using System.Collections.Generic;
using cAmp.Libraries.Common.Interfaces;
using cAmp.Libraries.Common.Objects;

namespace cAmp.Libraries.Common.Helpers
{
    public static class ListHelper
    {
        public static List<IcAmpObject> ToUserInterfaceObjects<T>(this List<T> libraryObjects) 
            where T:AbstractcAmpObject
        {
            List<IcAmpObject> output = new List<IcAmpObject>();

            foreach (var libraryObject in libraryObjects)
            {
                output.Add(libraryObject.ToUserInterfaceObject());
            }

            return output;
        }

        /// <summary>
        /// Copies the list, but the objects are the same as in the source.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static List<T> Copy<T>(this List<T> source)
        {
            var output = new List<T>();

            foreach (T obj in source)
            {
                output.Add(obj);
            }

            return output;
        }
    }
}
