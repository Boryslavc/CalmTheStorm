using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;



public class StressController : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Volume volume;
    [SerializeField] private Image handle;

    private StressModel model;
    private StressView view;


    private void Start()
    {
        model = new StressModel();
        view = new StressView(slider, volume, handle);
        view.StartPulse();

        UtilitiesHolder.Instance.GetUtility<EventBus>().Subscribe<RhythmGameFinishEvent>(RhythmGameFinished);
    }

    private void OnDisable()
    {
        UtilitiesHolder.Instance.GetUtility<EventBus>().Unsubscribe<RhythmGameFinishEvent>(RhythmGameFinished);
    }


    private void RhythmGameFinished(RhythmGameFinishEvent signal)
    {
        model.ChangeStressLevel(signal.stressChangeValue);
        view.AdjustVisuals(model.currentStressLevel / model.maxStressCount);
    }
}
