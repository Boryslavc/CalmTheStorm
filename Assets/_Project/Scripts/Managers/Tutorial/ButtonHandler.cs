using DG.Tweening;

public class ButtonHandler : IPromtHandler
{
    private readonly Promt promt;

    public ButtonHandler(Promt promt)
    {
        this.promt = promt;
    }

    public void HandlePromt()
    {
        promt.Config.Button.onClick.AddListener(Deactivate);
    }

    public void Deactivate()
    {
        promt.Config.Image.DOFade(0, 1f);

        if (promt.Config.HasText)
        {
            foreach (var t in promt.Config.Texts)
                t.DOFade(0, 1f);
        }
        promt.Config.Image.gameObject.SetActive(false);
    }

    public void SetNexthandler(IPromtHandler nexthandler)
    {

    }

    public void HandleNext()
    {
        
    }
}