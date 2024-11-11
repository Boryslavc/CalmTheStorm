using UnityEngine;


/// <summary>
/// Serves as a data container that harbours the information about the result of the mini-game,
/// expressed as a negative or positive float value that reflects the stress level change.
/// </summary>
public class RhythmGameFinishEvent : BaseEventSignal
{
    public readonly float stressChangeValue;

    public RhythmGameFinishEvent(float value)
    { 
        this.stressChangeValue = value;
    }
}
