using SimpleMonads;

namespace MoreCollections
{
    public class DictionaryItemUpdateAttempt<TValue> : IDictionaryItemUpdateAttempt<TValue>
    {
        public DictionaryItemUpdateAttempt(bool updated, IMaybe<TValue> existingValue, IMaybe<TValue> newValue)
        {
            Updated = updated;
            ExistingValue = existingValue;
            NewValue = newValue;
        }

        public bool Updated { get; }
        public IMaybe<TValue> ExistingValue { get; }
        public IMaybe<TValue> NewValue { get; }
    }
}