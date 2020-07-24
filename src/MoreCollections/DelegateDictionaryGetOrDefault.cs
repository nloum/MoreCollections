using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using SimpleMonads;

namespace MoreCollections
{
    public class DelegateDictionaryGetOrDefault<TKey, TValue> : IDictionaryEx<TKey, TValue>
    {
        private readonly IDictionaryEx<TKey, TValue> _wrapped;
        private readonly GetDefaultValue<TKey, TValue> _getDefaultValue;

        public DelegateDictionaryGetOrDefault(IDictionaryEx<TKey, TValue> wrapped, GetDefaultValue<TKey, TValue> getDefaultValue)
        {
            _wrapped = wrapped;
            _getDefaultValue = getDefaultValue;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            if (!_wrapped.TryGetValue(key, out value))
            {
                _getDefaultValue(key, out var maybeValue, out var persist);
                
                if (maybeValue.HasValue)
                {
                    if (persist)
                    {
                        _wrapped.Add(key, maybeValue.Value);
                    }

                    value = maybeValue.Value;
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        public IEnumerator<IKeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return _wrapped.GetEnumerator();
        }

        public int Count => _wrapped.Count;

        public IEqualityComparer<TKey> Comparer => _wrapped.Comparer;

        public IEnumerable<TKey> Keys => _wrapped.Keys;

        public IEnumerable<TValue> Values => _wrapped.Values;

        public bool ContainsKey(TKey key)
        {
            return TryGetValue(key, out var value);
        }

        public IMaybe<TValue> TryGetValue(TKey key)
        {
            if (TryGetValue(key, out var value))
            {
                return value.ToMaybe();
            }
            
            return Maybe<TValue>.Nothing();
        }

        public TValue this[TKey key]
        {
            get 
            {
                if (TryGetValue(key, out var value))
                {
                    return value;
                }
                
                throw new KeyNotFoundException();
            }
            set => _wrapped[key] = value;
        }

        public bool TryAdd(TKey key, TValue value)
        {
            return _wrapped.TryAdd(key, value);
        }

        public bool TryAdd(TKey key, Func<TValue> value)
        {
            return _wrapped.TryAdd(key, value);
        }

        public void TryAddRange(IEnumerable<IKeyValuePair<TKey, TValue>> newItems, out IReadOnlyDictionaryEx<TKey, bool> result)
        {
            _wrapped.TryAddRange(newItems, out result);
        }

        public void TryAddRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems, out IReadOnlyDictionaryEx<TKey, bool> result)
        {
            _wrapped.TryAddRange(newItems, out result);
        }

        public void TryAddRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value, out IReadOnlyDictionaryEx<TKey, bool> result)
        {
            _wrapped.TryAddRange(newItems, key, value, out result);
        }

        public void TryAddRange(IEnumerable<IKeyValuePair<TKey, TValue>> newItems)
        {
            _wrapped.TryAddRange(newItems);
        }

        public void TryAddRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems)
        {
            _wrapped.TryAddRange(newItems);
        }

        public void TryAddRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value)
        {
            _wrapped.TryAddRange(newItems, key, value);
        }

        public void TryAddRange(params IKeyValuePair<TKey, TValue>[] newItems)
        {
            _wrapped.TryAddRange(newItems);
        }

        public void TryAddRange(params KeyValuePair<TKey, TValue>[] newItems)
        {
            _wrapped.TryAddRange(newItems);
        }

        public void Add(TKey key, TValue value)
        {
            _wrapped.Add(key, value);
        }

        public void AddRange(IEnumerable<IKeyValuePair<TKey, TValue>> newItems)
        {
            _wrapped.AddRange(newItems);
        }

        public void AddRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems)
        {
            _wrapped.AddRange(newItems);
        }

        public void AddRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value)
        {
            _wrapped.AddRange(newItems, key, value);
        }

        public void AddRange(params IKeyValuePair<TKey, TValue>[] newItems)
        {
            _wrapped.AddRange(newItems);
        }

        public void AddRange(params KeyValuePair<TKey, TValue>[] newItems)
        {
            _wrapped.AddRange(newItems);
        }

        public bool TryUpdate(TKey key, TValue value)
        {
            return _wrapped.TryUpdate(key, value);
        }

        public bool TryUpdate(TKey key, TValue value, out TValue previousValue)
        {
            return _wrapped.TryUpdate(key, value, out previousValue);
        }

        public bool TryUpdate(TKey key, Func<TValue, TValue> value, out TValue previousValue)
        {
            return _wrapped.TryUpdate(key, value, out previousValue);
        }

        public void TryUpdateRange(IEnumerable<IKeyValuePair<TKey, TValue>> newItems)
        {
            _wrapped.TryUpdateRange(newItems);
        }

        public void TryUpdateRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems)
        {
            _wrapped.TryUpdateRange(newItems);
        }

        public void TryUpdateRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value)
        {
            _wrapped.TryUpdateRange(newItems, key, value);
        }

        public void TryUpdateRange(params IKeyValuePair<TKey, TValue>[] newItems)
        {
            _wrapped.TryUpdateRange(newItems);
        }

        public void TryUpdateRange(params KeyValuePair<TKey, TValue>[] newItems)
        {
            _wrapped.TryUpdateRange(newItems);
        }

        public void TryUpdateRange(IEnumerable<IKeyValuePair<TKey, TValue>> newItems, out IReadOnlyDictionaryEx<TKey, TValue> result)
        {
            _wrapped.TryUpdateRange(newItems, out result);
        }

        public void TryUpdateRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems, out IReadOnlyDictionaryEx<TKey, TValue> result)
        {
            _wrapped.TryUpdateRange(newItems, out result);
        }

        public void TryUpdateRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value, out IReadOnlyDictionaryEx<TKey, TValue> result)
        {
            _wrapped.TryUpdateRange(newItems, key, value, out result);
        }

        public void Update(TKey key, TValue value)
        {
            _wrapped.Update(key, value);
        }

        public void UpdateRange(IEnumerable<IKeyValuePair<TKey, TValue>> newItems)
        {
            _wrapped.UpdateRange(newItems);
        }

        public void UpdateRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems)
        {
            _wrapped.UpdateRange(newItems);
        }

        public void UpdateRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value)
        {
            _wrapped.UpdateRange(newItems, key, value);
        }

        public void Update(TKey key, TValue value, out TValue previousValue)
        {
            _wrapped.Update(key, value, out previousValue);
        }

        public void UpdateRange(IEnumerable<IKeyValuePair<TKey, TValue>> newItems, out IReadOnlyDictionaryEx<TKey, TValue> previousValues)
        {
            _wrapped.UpdateRange(newItems, out previousValues);
        }

        public void UpdateRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems, out IReadOnlyDictionaryEx<TKey, TValue> previousValues)
        {
            _wrapped.UpdateRange(newItems, out previousValues);
        }

        public void UpdateRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value, out IReadOnlyDictionaryEx<TKey, TValue> previousValues)
        {
            _wrapped.UpdateRange(newItems, key, value, out previousValues);
        }

        public void UpdateRange(params IKeyValuePair<TKey, TValue>[] newItems)
        {
            _wrapped.UpdateRange(newItems);
        }

        public void UpdateRange(params KeyValuePair<TKey, TValue>[] newItems)
        {
            _wrapped.UpdateRange(newItems);
        }

        public IMaybe<TValue> AddOrUpdate(TKey key, TValue value)
        {
            return _wrapped.AddOrUpdate(key, value);
        }

        public IMaybe<TValue> AddOrUpdate(TKey key, Func<TValue> valueIfAdding, Func<TValue, TValue> valueIfUpdating)
        {
            return _wrapped.AddOrUpdate(key, valueIfAdding, valueIfUpdating);
        }

        public void AddOrUpdateRange(IEnumerable<IKeyValuePair<TKey, TValue>> newItems, out IReadOnlyDictionaryEx<TKey, IMaybe<TValue>> result)
        {
            _wrapped.AddOrUpdateRange(newItems, out result);
        }

        public void AddOrUpdateRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems, out IReadOnlyDictionaryEx<TKey, IMaybe<TValue>> result)
        {
            _wrapped.AddOrUpdateRange(newItems, out result);
        }

        public void AddOrUpdateRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value, out IReadOnlyDictionaryEx<TKey, IMaybe<TValue>> result)
        {
            _wrapped.AddOrUpdateRange(newItems, key, value, out result);
        }

        public void AddOrUpdateRange(IEnumerable<IKeyValuePair<TKey, TValue>> newItems)
        {
            _wrapped.AddOrUpdateRange(newItems);
        }

        public void AddOrUpdateRange(IEnumerable<KeyValuePair<TKey, TValue>> newItems)
        {
            _wrapped.AddOrUpdateRange(newItems);
        }

        public void AddOrUpdateRange<TKeyValuePair>(IEnumerable<TKeyValuePair> newItems, Func<TKeyValuePair, TKey> key, Func<TKeyValuePair, TValue> value)
        {
            _wrapped.AddOrUpdateRange(newItems, key, value);
        }

        public void AddOrUpdateRange(params IKeyValuePair<TKey, TValue>[] newItems)
        {
            _wrapped.AddOrUpdateRange(newItems);
        }

        public void AddOrUpdateRange(params KeyValuePair<TKey, TValue>[] newItems)
        {
            _wrapped.AddOrUpdateRange(newItems);
        }

        public void TryRemoveRange(IEnumerable<TKey> keysToRemove)
        {
            _wrapped.TryRemoveRange(keysToRemove);
        }

        public void RemoveRange(IEnumerable<TKey> keysToRemove)
        {
            _wrapped.RemoveRange(keysToRemove);
        }

        public void RemoveWhere(Func<TKey, TValue, bool> predicate)
        {
            _wrapped.RemoveWhere(predicate);
        }

        public void RemoveWhere(Func<IKeyValuePair<TKey, TValue>, bool> predicate)
        {
            _wrapped.RemoveWhere(predicate);
        }

        public void Clear()
        {
            _wrapped.Clear();
        }

        public bool TryRemove(TKey key)
        {
            return _wrapped.TryRemove(key);
        }

        public void Remove(TKey key)
        {
            _wrapped.Remove(key);
        }

        public void TryRemoveRange(IEnumerable<TKey> keysToRemove, out IReadOnlyDictionaryEx<TKey, TValue> removedItems)
        {
            _wrapped.TryRemoveRange(keysToRemove, out removedItems);
        }

        public void RemoveRange(IEnumerable<TKey> keysToRemove, out IReadOnlyDictionaryEx<TKey, TValue> removedItems)
        {
            _wrapped.RemoveRange(keysToRemove, out removedItems);
        }

        public void RemoveWhere(Func<TKey, TValue, bool> predicate, out IReadOnlyDictionaryEx<TKey, TValue> removedItems)
        {
            _wrapped.RemoveWhere(predicate, out removedItems);
        }

        public void RemoveWhere(Func<IKeyValuePair<TKey, TValue>, bool> predicate, out IReadOnlyDictionaryEx<TKey, TValue> removedItems)
        {
            _wrapped.RemoveWhere(predicate, out removedItems);
        }

        public void Clear(out IReadOnlyDictionaryEx<TKey, TValue> removedItems)
        {
            _wrapped.Clear(out removedItems);
        }

        public bool TryRemove(TKey key, out TValue removedItem)
        {
            return _wrapped.TryRemove(key, out removedItem);
        }

        public void Remove(TKey key, out TValue removedItem)
        {
            _wrapped.Remove(key, out removedItem);
        }
    }
}