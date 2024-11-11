using Utilities;
using DG.Tweening;

public class DeactivatorHandler : IPromtHandler
{
    private readonly Promt promt;

    private IPromtHandler next;

    private CountdownTimer timer;

    public DeactivatorHandler(Promt promt)
    {
        this.promt = promt;
    }


    public void HandlePromt()
    {
        timer = GlobalTimer.Instance.GetCountdownTimer();

        timer.OnTimerStop += () => 
        {
            promt.Config.Image.DOFade(0, 1f);

            if (promt.Config.HasText)
            {
                foreach (var t in promt.Config.Texts)
                    t.DOFade(0, 1f);
            }
            promt.Config.Image.gameObject.SetActive(false);
        };

        timer.Reset(promt.Config.LifeDuration);
        timer.Start(); 
    }

    public void SetNexthandler(IPromtHandler nexthandler)
    {
       //final
    }

    public void HandleNext()
    {
        throw new System.NotImplementedException();
    }
}