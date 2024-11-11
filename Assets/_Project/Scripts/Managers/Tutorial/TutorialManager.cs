using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class to wire events and specific promts together.
/// </summary>
public class TutorialManager : MonoBehaviour
{
    [SerializeField] private List<PromtConfig> configList;

    private List<Promt> promts = new List<Promt>();


    private void Awake()
    {
        foreach (var config in configList)
            promts.Add(new Promt(config));

        SortByPriority();
    }
    private void SortByPriority()
    {
        while (true)
        {
            bool stop = true;

            for (int i = 1; i < configList.Count; i++)
            {
                if (promts[i].Config.Priority < promts[i - 1].Config.Priority)
                {
                    var temp = promts[i - 1];
                    promts[i - 1] = promts[i];
                    promts[i] = temp;
                    stop = false;
                }
            }

            if (stop)
                break;
        }
    }


    private void Start()
    {
        promts[0].Start();

        var eventBus = UtilitiesHolder.Instance.GetUtility<EventBus>();
        eventBus.Subscribe<RhythmGameStartEvent>(OnGameStart);
        eventBus.Subscribe<RhythmGameFinishEvent>(OnGameFinish);
        eventBus.Subscribe<SequenceLearnedEvent>(OnSequnceLearned);
    }
    private void OnDestroy()
    {
        var eventBus = UtilitiesHolder.Instance.GetUtility<EventBus>();
        eventBus.Unsubscribe<RhythmGameStartEvent>(OnGameStart);
        eventBus.Unsubscribe<RhythmGameFinishEvent>(OnGameFinish);
        eventBus.Unsubscribe<SequenceLearnedEvent>(OnSequnceLearned);
    }

    private void OnGameStart(RhythmGameStartEvent signal)
    {
        promts[1].Start();
    }

    private void OnSequnceLearned(SequenceLearnedEvent signal)
    {
        promts[2].Start();
    }

    private void OnGameFinish(RhythmGameFinishEvent signal)
    {
        promts[3].Start();
    }
}
