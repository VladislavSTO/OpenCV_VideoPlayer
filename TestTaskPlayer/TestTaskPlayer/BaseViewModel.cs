using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;

namespace TestTaskPlayer
{
    [Serializable]
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        private readonly Dictionary<string, object> _notifyPropertyValues;

        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        protected BaseViewModel()
        {
            _notifyPropertyValues = new Dictionary<string, object>();
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Use  NotifyPropertySet(() => [PropertyName], value)
        /// </summary>
        protected void NotifyPropertySet<T>(Expression<Func<T>> notifyProperty, T newValue)
        {
            var propertyName = GetPropertyName(notifyProperty);

            if (!_notifyPropertyValues.ContainsKey(propertyName))
                _notifyPropertyValues.Add(propertyName, default(T));

            var lastValue = (T)_notifyPropertyValues[propertyName];
            if (IsEquals(lastValue, newValue))
                return;

            _notifyPropertyValues[propertyName] = newValue;
            OnPropertyChanged(propertyName);
        }

        /// <summary>
        /// Use  NotifyPropertyGet(() => [PropertyName])
        /// </summary>
        protected T NotifyPropertyGet<T>(Expression<Func<T>> notifyProperty)
        {
            var propertyName = GetPropertyName(notifyProperty);

            if (!_notifyPropertyValues.ContainsKey(propertyName))
                return default;

            return (T)_notifyPropertyValues[propertyName];
        }

        public string GetPropertyName<T>(Expression<Func<T>> expr)
        {
            var member = (MemberExpression)expr.Body;
            return member.Member.Name;
        }

        private bool IsEquals<T>(T obj1, T obj2)
        {
            if (obj1 == null)
                return obj2 == null;

            return obj1.Equals(obj2);
        }
    }
}
