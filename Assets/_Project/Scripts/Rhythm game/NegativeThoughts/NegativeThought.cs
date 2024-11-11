using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// Distraction for the player. Floating during the rhythm game.
/// </summary>

[RequireComponent(typeof(Image))]
public class NegativeThought : MonoBehaviour 
{
    [SerializeField] private float speed;
    [SerializeField] private float maxDistanceFromCenter;

    private Image image;
    private TMP_Text text;

    private Camera mainCam;

    private Vector3 destination;

    private bool toMove = false;

    private void Awake()
    {
        mainCam = Camera.main;
    }

    private void Start()
    {
        image = GetComponent<Image>();
        text = GetComponentInChildren<TMP_Text>();

        var eventBus = UtilitiesHolder.Instance.GetUtility<EventBus>();
        eventBus.Subscribe<RhythmGameStartEvent>(OnGameStart);
        eventBus.Subscribe<RhythmGameFinishEvent>(OnGameFinish);
    }
    private void OnDisable()
    {
        var eventBus = UtilitiesHolder.Instance.GetUtility<EventBus>();
        eventBus.Unsubscribe<RhythmGameStartEvent>(OnGameStart);
        eventBus.Unsubscribe<RhythmGameFinishEvent>(OnGameFinish);
    }

    private void OnGameStart(RhythmGameStartEvent signal)
    {
        Physics.Raycast(mainCam.gameObject.transform.position, mainCam.gameObject.transform.forward,
            out RaycastHit hit, 100f);
        image.transform.position = hit.point;

        ChangeDestination();

        image.DOFade(1, 1);
        text.DOFade(1, 1);

        toMove = true;
    }

    private void Update()
    {
        if(toMove)
            Move();
    }

    private void OnGameFinish(RhythmGameFinishEvent signal)
    {
        image.DOFade(0, 1);
        text.DOFade(0, 1);

        toMove = false;
    }
 

    public void Move()
    {
        image.rectTransform.position = Vector3.MoveTowards(image.rectTransform.position, destination, 
            speed * Time.deltaTime);
        
        if(Vector3.Distance(image.rectTransform.position, destination) < 0.5f)
            ChangeDestination();
    }
    private void ChangeDestination()
    {
        destination =  image.transform.position + new Vector3(Random.Range(-maxDistanceFromCenter, maxDistanceFromCenter),
            Random.Range(-maxDistanceFromCenter, maxDistanceFromCenter));
    }
}

