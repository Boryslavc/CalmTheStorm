using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;


public class StressView 
{
    private Slider slider;
    private Image handle;

    private Volume volume;
    private Vignette vignette;

    public StressView(Slider slider, Volume volume, Image handle)
    {
        this.slider = slider;
        this.volume = volume;
        this.handle = handle;

        SetUpVignette(volume);
    }
    private void SetUpVignette(Volume volume)
    {

        if (volume.profile.TryGet(out vignette))
        {
            vignette.intensity.Override(slider.value);
        }
        else
        {
            vignette = volume.profile.Add<Vignette>();
            vignette.color.Override(Color.black);
            vignette.intensity.Override(slider.value);
        }
    }


    public void StartPulse()
    {
        DOTween.Sequence()
            .Append(DOTween.To(() => volume.weight, x => volume.weight = x, .6f, 2f))
            .AppendInterval(.5f)
            .Append(DOTween.To(() => volume.weight, x => volume.weight = x, 1f, 2f))
            .SetLoops(-1);
    }

    public void AdjustVisuals(float fillPercent)
    {
        var initScale = handle.transform.localScale;

        DOTween.Sequence()
            .Append(handle.transform.DOScale(handle.transform.localScale * 2, 1f))
            .Append(slider.DOValue(fillPercent, 2f))
            .Append(handle.transform.DOScale(initScale, 1f));

        vignette.intensity.Override(fillPercent); 
        // to refactor (vignette.intensity is of type ClampedFloatValue, conlict with DoTween)
    }
}
