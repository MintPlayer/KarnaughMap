using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace KarnaughMap.Extensions
{
    internal static class ObservableCollection
    {
        public static void AddRange<T, U>(this T collection, IEnumerable<U> items) where T : ObservableCollection<U>
        {
            using (var suppressEvent = new Helpers.SuppressEvent<ObservableCollection<U>>(collection, nameof(ObservableCollection<T>.CollectionChanged)))
            {
                foreach (var item in items)
                    collection.Add(item);
            }
        }
    }
}
