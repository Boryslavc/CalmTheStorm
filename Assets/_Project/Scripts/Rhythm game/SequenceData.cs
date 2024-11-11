using System;
using System.Collections.Generic;
using UnityEngine;

namespace RhythmGame
{
    [CreateAssetMenu(fileName = "Sequence Data", menuName = "Sequences")]
    public class SequenceData : ScriptableObject
    {
        public List<string> KeysToPress = new List<string>(2);
        public List<float> TimeToPressInSec = new List<float>(2);
        public List<SoundData> OnPressSound = new List<SoundData>(2);
        public SoundData MusicFull;
        public int HidePromtsAfterTimesRepeated;
        public int RepeatTotalAmount;


        public List<KeyCode> KeyCodes = new List<KeyCode>(2);

        public void ParseKeyCodes()
        {
            KeyCodes.Clear();
            for (int i = 0; i < KeysToPress.Count; i++)
            {
                var code = (KeyCode)Enum.Parse(typeof(KeyCode), KeysToPress[i]);
                KeyCodes.Add(code);
            }
        }
    }
}

