using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private Button startGameButton;
    [SerializeField] private SoundData surroundingSounds;

    private void Awake()
    {
        startGameButton.onClick.AddListener(LoadFirstLevel);
    }

    private void Start()
    {
        AudioPlayer.Instance.Play(surroundingSounds);
    }

    private void LoadFirstLevel()
    {
        SceneManager.LoadScene(1);
    }
}
