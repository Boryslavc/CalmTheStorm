using UnityEngine;
using UnityEngine.UI;

namespace RhythmGame
{
    public class CalmingObjectController : MonoBehaviour
    {
        [SerializeField] private SequenceData sequenceData;
        [SerializeField] private Image[] Images;
        [SerializeField] private float WinLooseCost = 50f;

        private RhythmModel model;
        private RhythmView view;


        private bool isGameRunning = false;

        private void Awake()
        {
            model = new RhythmModel();
            view = new RhythmView();

            if (sequenceData != null)
                model.GetSequenceData(sequenceData);
            else
                Debug.LogError($"No Sequence Data Found On Object {this.name}");

            BindActions();

            if(Images != null) 
                view.HidePromts(Images);
            else
                Debug.LogError($"No Images Found On Object {this.name}");
        }

        private void BindActions()
        {
            model.OnKeyPressed += MoveToNextIcon;

            model.OnSequenceLearned += () =>
            {
                view.HidePromts(Images);
                UtilitiesHolder.Instance.GetUtility<EventBus>()
                .InvokeEvent<SequenceLearnedEvent>(new SequenceLearnedEvent(this));
            };
            
            model.OnSequenceMastered += OnRhythmGameFinished;
        }

        private void MoveToNextIcon()
        {
            view.ReduceImage(Images[model.previousInd], 1, 0.5f);
            view.PromtButton(Images[model.currentIndex], 2, 0.5f);
        }

        private void OnRhythmGameFinished(bool HasPlayerWon)
        {
            view.HidePromts(Images);
            view.ReduceImage(Images[model.currentIndex], 1, 1);


            //deduct or add to stress value
            float stressChange /*= HasPlayerWon ? WinLooseCost * -1 : WinLooseCost*/;


            // easier to debug 
            if (HasPlayerWon)
            {
                Debug.Log("Player Succeded");
                stressChange = WinLooseCost * -1; // deduct from stress value
            }
            else
            {
                Debug.Log("Player Lost");
                stressChange = WinLooseCost;// add to stress value
            }

            var evBus = UtilitiesHolder.Instance.GetUtility<EventBus>();
            evBus.InvokeEvent(new RhythmGameFinishEvent(stressChange));

            isGameRunning = false;
        }

        public void StartGame(RhythmGameStartEvent signal)
        {
            //cheks if the camera is looking at this object
            if(signal.controller == this)
            {
                view.ResetImages(Images);
                view.ShowPromts(Images);
                view.PromtButton(Images[model.currentIndex], 2, 0.5f);

                model.Start();

                isGameRunning = true;
            }
        }

        private void Start()
        {
            UtilitiesHolder.Instance.GetUtility<EventBus>().Subscribe<RhythmGameStartEvent>(StartGame);
        }

        private void Update()
        {
            if(isGameRunning)
                model.Tick(Time.deltaTime);
        }
 

        private void OnDestroy()
        {
            UtilitiesHolder.Instance.GetUtility<EventBus>().Unsubscribe<RhythmGameStartEvent>(StartGame);

            model.OnSequenceLearned -= () => view.HidePromts(Images);
            model.OnSequenceMastered -= OnRhythmGameFinished;

            model.OnKeyPressed -= MoveToNextIcon;
        }
    }
}
