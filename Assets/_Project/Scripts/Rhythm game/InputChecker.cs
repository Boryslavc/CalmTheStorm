using System;
using UnityEngine;


namespace RhythmGame
{
    public class InputChecker
    {
        public Action OnInputReceived;
        public Action OnInputFail;

        public InputChecker() { }



        public void Tick(KeyCode keyToCheck)
        {
            if (NeededButtonPressed(keyToCheck))
            {
                Debug.Log("Input Received");
                OnInputReceived?.Invoke();
            }
            else if (OtherButtonPressed(keyToCheck))
            {
                OnInputFail?.Invoke();
            }
        }

        private bool NeededButtonPressed(KeyCode keyToCheck) => Input.GetKeyDown(keyToCheck);
        

        private bool OtherButtonPressed(KeyCode keyToCheck)
        {
            var allKeyCodes = Enum.GetValues(typeof(KeyCode));
            foreach (KeyCode key in allKeyCodes)
            {
                if (Input.GetKeyDown(key) && key != keyToCheck &&
                    !Input.GetMouseButton(0) && !Input.GetMouseButton(1) && !Input.GetMouseButton(2))
                {
                    Debug.Log($"The wrong key was pressed: {keyToCheck}");
                    return true;
                }
            }
            return false;
        }
    }
}
