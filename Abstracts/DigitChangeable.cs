using System;
using UnityEngine;


public abstract class DigitChangeable : MonoBehaviour, IDigitChangeable
{
    private int _digit;
    public virtual int Digit
    {
        get { return _digit; }
        set
        {
            if (value == Digit) return;
            _digit = value;
            UpdateDigitVisuals();
        }
    }

    public virtual int Number04
    {
        get => Digit % 5;
        protected set
        {
            Reflect04Update(value);
        }
    }

    public bool IsFiveOn
    {
        get => Digit / 5 == 1;
        set
        {
            if (IsFiveOn == value) return;
            // gets executed only when IsFiveOn is changing
            _digit += IsFiveOn ? -5 : 5;
            Update05Visual();
            UpdateTotalNumber();
        }
    }

    protected void Switch05()
    {
        IsFiveOn = !IsFiveOn;
    }

    public int DigitPosition => transform.GetSiblingIndex();

    private ITotalNumberHandler totalNumberHandler;

    protected void Reflect04Update(int value)
    {
        _digit = value + (IsFiveOn ? 5 : 0);
        Update04Visual();
        UpdateTotalNumber();
    }

    public virtual void OnEnable()
    {
        totalNumberHandler = transform.parent.GetComponent<ITotalNumberHandler>();
        OnDigitChanged += totalNumberHandler.ReflectDigitChangeToTotal;
        UpdateDigitVisuals();
    }

    public virtual void OnDisable()
    {
        OnDigitChanged -= totalNumberHandler.ReflectDigitChangeToTotal;
    }


    public event Action<int, int> OnDigitChanged;

    // not called when Digit was updated from outside of the script
    private void UpdateTotalNumber()
    {
        OnDigitChanged?.Invoke(_digit, DigitPosition);
    }

    protected void UpdateDigitVisuals()
    {
        Update04Visual();
        Update05Visual();
    }

    protected abstract void Update04Visual();

    protected abstract void Update05Visual();
}
