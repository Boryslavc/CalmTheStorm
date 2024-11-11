using DG.Tweening;
using UnityEngine.UI;

namespace RhythmGame
{
    public class RhythmView
    {
        public void ResetImages(Image[] images)
        {
            foreach (var image in images)
                image.transform.localScale = new UnityEngine.Vector3(1,1,1);
        }

        public void PromtButton(Image image, float size, float duration)
        {
            image.transform.DOScale(size, duration);
        }

        public void ReduceImage(Image image, float size, float duration)
        {
            image.transform.DOScale(size, duration);
        }

        public void ShowPromts(Image[] images)
        {
            foreach (Image image in images)
                image.DOFade(255, 2f);
        }

        public void HidePromts(Image[] images)
        {
            foreach (Image image in images)
                image.DOFade(0, 0.01f);
        }
    }

}
