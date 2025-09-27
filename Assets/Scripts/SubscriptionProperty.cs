using System;

public class SubscriptionProperty<T> : IReadOnlySubscriptionProperty<T>
{
    private T _value;
    private Action<T> _onChangeValue;

    public T Value
    {
        get => _value;
        set
        {
            _value = value;
            _onChangeValue?.Invoke(value);
        }
    }
    
    public void SubscribeOnChange(Action<T> subscriptionAction)
    {
        _onChangeValue += subscriptionAction;
    }

    public void UnSubscribeOnChange(Action<T> unSubscriptionAction)
    {
        _onChangeValue -= unSubscriptionAction;
    }
}