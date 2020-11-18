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
    }
}
