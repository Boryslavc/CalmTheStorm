using DG.Tweening;


public class TextPulseHandler : IPromtHandler
{
    private readonly Promt promt;

    private IPromtHandler next;

    public TextPulseHandler(Promt promt)
    {
        this.promt = promt;
    }



    public void HandlePromt()
    {
        float scaleSize = promt.Config.ScaleSize;
        float pulseDuration = promt.Config.PulseDuration;

        foreach (var t in promt.Config.Texts)
        {
            DOTween.Sequence()
                .Append(t.transform.DOScale(t.transform.localScale * scaleSize, pulseDuration))
                .Append(t.transform.DOScale(t.transform.localScale / scaleSize, pulseDuration))
                .SetLoops(-1);
        }

        if (promt.Config.HasButton)
            SetNexthandler(new ButtonHandler(promt));
        else
            SetNexthandler(new DeactivatorHandler(promt));


        HandleNext();
    }

    public void SetNexthandler(IPromtHandler nexthandler)
    {
        next = nexthandler;   
    }

    public void HandleNext()
    {
        if (next != null)
            next.HandlePromt();
    }
}