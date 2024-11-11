using RhythmGame;

/// <summary>
/// Used to specify what instance of the class is under focus.
/// </summary>
public class SequenceLearnedEvent : BaseEventSignal
{
    public readonly CalmingObjectController controller;

    public SequenceLearnedEvent(CalmingObjectController controller)
    {
        this.controller = controller; 
    }
}
