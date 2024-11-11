using UnityEngine.Events;

public class StressModel
{
    public UnityAction<float> OnStressLevelChanged;

    public readonly float maxStressCount = 100;
    public float currentStressLevel { get; private set; } = 50;


    public void ChangeStressLevel(float delta)
    {
        currentStressLevel += delta;

        OnStressLevelChanged?.Invoke(currentStressLevel);
    }
}
