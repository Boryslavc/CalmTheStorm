using DG.Tweening;

public class PromtEnableHandler : IPromtHandler
{
    private readonly Promt promt;

    private IPromtHandler next;

    public PromtEnableHandler(Promt promt)
    { 
        this.promt = promt; 
    }


    public void HandlePromt()
    {
        if (promt.Config.PulseDuration > 0)
        {
            SetNexthandler(new TextPulseHandler(promt));
        }
        else
        {
            if (promt.Config.HasButton)
                SetNexthandler(new ButtonHandler(promt));
            else
                SetNexthandler(new DeactivatorHandler(promt));
        }


        promt.Config.Image.gameObject.SetActive(true);
        promt.Config.Image.DOFade(1, 1f);
        if(promt.Config.HasText)
        {
            foreach(var t in promt.Config.Texts) 
                t.DOFade(1, 1f);
        }
        if (promt.Config.HasButton)
        {
            promt.Config.Button.gameObject.SetActive(true);
        }

        HandleNext();
    }

    public void SetNexthandler(IPromtHandler nexthandler)
    {
       next = nexthandler;
    }

    public void HandleNext()
    {
        if (next != null)
        {
            next.HandlePromt();
        }
    }
}
