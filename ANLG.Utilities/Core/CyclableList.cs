namespace ANLG.Utilities.Core;

public class CyclableList<T> : List<T>
{
    private int _currentIndex = 0;

    public CyclableList() { }
    
    public CyclableList(IEnumerable<T> existingList)
    {
        foreach (var item in existingList)
        {
            Add(item);
        }
    }
    
    public T CurrentItem => Count > 0 ? this[_currentIndex] : throw new InvalidOperationException("List is empty. Cannot get current item.");

    public bool TryGetCurrentItem(out T currentItem)
    {
        if (Count > 0)
        {
            currentItem = CurrentItem;
            return true;
        }

        currentItem = default;
        return false;
    }

    public T SetCurrentItem(int index)
    {
        _currentIndex = ValidateIndex(index);
        return this[_currentIndex];
    }

    public T CycleToNextItem() => SetCurrentItem(_currentIndex + 1);
    public T CycleToPreviousItem() => SetCurrentItem(_currentIndex - 1);

    private int ValidateIndex(int index)
    {
        if (Count == 0)
        {
            throw new InvalidOperationException("List has no items.");
        }

        return index.Regulate(Count);
    }
}
