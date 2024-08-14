public class MyStat {
    public int maxValue { get; private set; }
    public int currentValue {
        get { return currentValue; }
        set {
            if (value > maxValue) currentValue = maxValue;
            else currentValue = value;
        }
    }

    public MyStat(int maxValue, int currentValue = 0) {
        this.maxValue = maxValue;
        this.currentValue = currentValue;
    }
    
    public void IncreaseStat(int increase) { maxValue += increase; }
    
}
