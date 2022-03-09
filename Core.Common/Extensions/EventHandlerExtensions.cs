using System.Collections.Specialized;

namespace Core.Common.Extensions;

public static class EventHandlerExtensions
{
    public static void OnAdd<T>(this INotifyCollectionChanged collectionChanged, Action<T> action)
    {
        collectionChanged.CollectionChanged += (s, e) =>
        {
            if (e.Action != NotifyCollectionChangedAction.Add) return;
            foreach (T item in e.NewItems) action(item);
        };
    }

    public static void OnRemove<T>(this INotifyCollectionChanged collectionChanged, Action<T> action)
    {
        collectionChanged.CollectionChanged += (s, e) =>
        {
            if (e.Action != NotifyCollectionChangedAction.Remove) return;
            foreach (T item in e.OldItems) action(item);
        };
    }
}
