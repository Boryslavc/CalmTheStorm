using RhythmGame;

/// <summary>
/// Used to specify what instance of the class is under focus.
/// </summary>
public class RhythmGameStartEvent : BaseEventSignal
{
    public readonly CalmingObjectController controller;

    public RhythmGameStartEvent(CalmingObjectController controller)
    {
        this.controller = controller;
    }
}
