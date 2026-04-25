using System.Collections;

namespace BankApp;

public class Ledger<T> : IEnumerable<T>
{
    private readonly List<T> _entries = new List<T>();

    public int Count => _entries.Count;

    public IReadOnlyList<T> Entries => _entries.AsReadOnly();

    public void Add(T entry)
    {
        _entries.Add(entry);
    }

    public IEnumerator<T> GetEnumerator()
    {
        return _entries.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}