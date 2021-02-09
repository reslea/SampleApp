using System;
using System.Collections;
using System.Collections.Generic;

namespace Collections
{
    public class OurList<T> : ICollection<T>
    {
        private const int DefaultArraySize = 8;
        private int _arraySize = DefaultArraySize;
        private T[] _array = new T[DefaultArraySize];
        
        public int Count { get; private set; } = 0;

        public bool IsReadOnly { get; } = false;

        public void Add(T element)
        {
            if (Count >= _arraySize)
            {
                Resize();
            }
            _array[Count] = element;
            Count++;
        }

        public void Clear()
        {
            Count = 0;
            _array = new T[DefaultArraySize];
        }

        public bool Contains(T item)
        {
            foreach (T element in this)
            {
                if (element.Equals(item))
                    return true;
            }

            return false;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool Remove(T item)
        {
            for (int i = 0; i < Count; i++)
            {
                if (!_array[i].Equals(item)) continue;
                Delete(i);
                return true;
            }

            return false;
        }

        public void Delete(int index)
        {
            Count--;
            for (int i = index; i < Count; i++)
            {
                _array[i] = _array[i + 1];
            }
        }

        public T GetValue(int index)
        {
            return index < Count 
                ? _array[index]
                : throw new ArgumentOutOfRangeException();
        }

        public T[] GetValues()
        {
            T[] result = new T[Count];

            for (int i = 0; i < Count; i++)
            {
                result[i] = _array[i];
            }
            return result;
        }

        private void Resize()
        {
            _arraySize *= 2;
            T[] newArray = new T[_arraySize];
            for (int i = 0; i < _array.Length; i++)
            {
                newArray[i] = _array[i];
            }

            _array = newArray;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new OurListEnumerator<T>(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        class OurListEnumerator<Type> : IEnumerator<Type>
        {
            private readonly OurList<Type> _list;
            private int currentIndex = 0;

            public OurListEnumerator(OurList<Type> list)
            {
                _list = list;
            }

            public bool MoveNext()
            {
                if (currentIndex >= _list.Count)
                {
                    return false;
                }

                Current = _list.GetValue(currentIndex);
                currentIndex++;
                return true;
            }

            public void Reset()
            {
                currentIndex = 0;
            }

            public Type Current { get; private set; }

            object? IEnumerator.Current => Current;

            public void Dispose() { }
        }
    }
}
