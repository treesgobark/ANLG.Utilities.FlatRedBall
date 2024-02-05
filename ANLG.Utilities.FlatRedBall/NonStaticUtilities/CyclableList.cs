using ANLG.Utilities.FlatRedBall.StaticUtilities;

namespace ANLG.Utilities.FlatRedBall.NonStaticUtilities;

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
    
    public T CurrentItem { get; private set; }

    public T SetCurrentItem(int index)
    {
        _currentIndex = ValidateIndex(index);
        return CurrentItem = this[_currentIndex];
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
