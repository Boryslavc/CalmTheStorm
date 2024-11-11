using System;
using Utilities;


namespace RhythmGame
{
    public class RhythmModel
    {
        public Action OnKeyPressed;
        public Action OnSequenceLearned; // make player press the sequence from memory

        public Action<bool> OnSequenceMastered;

        public int previousInd { get; private set; }
        public int currentIndex { get; private set; } = 0;


        private InputChecker inputChecker;
        private CountdownTimer timer;

        private SequenceData sequenceData;

        private int timesCompleted;
        public int TimesCompleted 
        {
            get { return timesCompleted; }
            private set
            {
                timesCompleted = value;

                if(TimesCompleted == sequenceData.HidePromtsAfterTimesRepeated - 1)
                {
                    OnSequenceLearned?.Invoke();
                    AudioPlayer.Instance.Play(sequenceData.MusicFull);
                }

                if(sequenceData.RepeatTotalAmount == timesCompleted)
                {
                    OnSequenceMastered?.Invoke(true);
                }
            }
        }


        public RhythmModel()
        {
            inputChecker = new InputChecker();
            timer = new CountdownTimer();

            inputChecker.OnInputReceived += () => KeyPressed();
            inputChecker.OnInputFail += () => KeyMissed();

            timer.OnTimerStop += () => KeyMissed();
        }



        public void GetSequenceData(SequenceData sequenceData)
        {
            this.sequenceData = sequenceData;
            sequenceData.ParseKeyCodes();
        }

        public void Start()
        {
            //reset values
            currentIndex = 0;
            TimesCompleted = 0;

            timer.Reset(sequenceData.TimeToPressInSec[currentIndex]);
            timer.Start();
        }

        public void Tick(float deltaTime)
        {
            timer.Tick(deltaTime);

            if (timer.IsRunning)
                inputChecker.Tick(sequenceData.KeyCodes[currentIndex]);
        }


        private void KeyPressed()
        {
            //change indexes first, for the right execution of controllers's MoveToNextIcon method
            ChangeIndexes();

            timer.Reset(sequenceData.TimeToPressInSec[currentIndex]);
            timer.Start();

            OnKeyPressed?.Invoke();



            // after promts are hidden, full version of audio will play
            if (TimesCompleted < sequenceData.HidePromtsAfterTimesRepeated)
            {
                var ind = UnityEngine.Random.Range(0, sequenceData.OnPressSound.Count);
                AudioPlayer.Instance.Play(sequenceData.OnPressSound[ind]);
            }
        }

        private void KeyMissed()
        {
            OnSequenceMastered?.Invoke(false);

            // if player fails after promts are hidden
            AudioPlayer.Instance.StopPlaying(sequenceData.MusicFull);
        }
        private void ChangeIndexes()
        {
            if (currentIndex == sequenceData.KeysToPress.Count - 1)
            {
                TimesCompleted++;
            }

            previousInd = currentIndex;
            currentIndex = (currentIndex + 1) % sequenceData.KeysToPress.Count;           
        }

        private void OnDestroy()
        {
            inputChecker.OnInputReceived -= () => KeyPressed();
        }
    }
}

