using System.Collections.Generic;
using System.Collections.Specialized;

namespace MongoMS.Find.Addin.Controls
{
    public static class ext
    {
        public static void AddUnique(this StringCollection sc, IEnumerable<string> item)
        {
            foreach (var i in item)
            {
                AddUnique(sc, i);
            }
        }
        public static void AddUnique(this StringCollection sc, string item)
        {
            if (sc.IndexOf(item) < 0)
            {
                sc.Add(item);
            }
        }
    }
}