using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace Hive.App.ViewModel;

public abstract class ViewModelBase : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    public virtual void OnPropertyChanged(string propertyName)
    {
        var handler = PropertyChanged;
        if (handler != null)
        {
            var e = new PropertyChangedEventArgs(propertyName);
            handler(this, e);
        }
    }

    public virtual void OnPropertyChanged<T>(Expression<Func<T>> propertyNameExpression)
    {
        OnPropertyChanged(((MemberExpression)propertyNameExpression.Body).Member.Name);
    }
}
