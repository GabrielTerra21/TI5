using System;


public class MyStat {
    public int maxValue { get; private set; } 
    public int currentValue {
        get {
            return _currentValue;
        }
        set {
            value = Math.Clamp(value, 0, maxValue);
            _currentValue = value;
        }
    }
    private int _currentValue;
    
    
    public MyStat(int maxValue, int currentValue = 0) {
        this.maxValue = maxValue;
        this.currentValue = currentValue;
    }
    
    public void IncreaseStat(int increase) { maxValue += increase; }

}
