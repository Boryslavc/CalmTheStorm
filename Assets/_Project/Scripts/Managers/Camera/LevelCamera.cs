using DG.Tweening;
using RhythmGame;
using UnityEngine;
using UnityEngine.Rendering;

/// <summary>
/// Camera that is used during actual gameplay.
/// </summary>
public class LevelCamera : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    [SerializeField] private float zoomInDuration = 1f;
    [SerializeField] private float distToPicture = 27;
    [SerializeField] private float zoomedPOV = 20;


    private Camera camera;
    private Volume volume;

    private CalmingObjectController calmingObject;

    private Vector3 lastMousePos;
    private float initFOV;
    private LayerMask calmingObjLayer;

    private bool toMoveCamera = true;


    private void Awake()
    {
        camera = GetComponent<Camera>();
        volume = GetComponentInChildren<Volume>();


        calmingObjLayer = LayerMask.GetMask("CalmingObject");
        initFOV = camera.fieldOfView;
        lastMousePos = Input.mousePosition;
    }


    public void Start()
    {
        var eventBus = UtilitiesHolder.Instance.GetUtility<EventBus>();
        eventBus.Subscribe<RhythmGameFinishEvent>(ResetFOV);
    }

    public void OnDestroy()
    {
        UtilitiesHolder.Instance.GetUtility<EventBus>().Unsubscribe<RhythmGameFinishEvent>(ResetFOV);
    }


    private void ResetFOV(RhythmGameFinishEvent signal)
    {
        camera.DOFieldOfView(initFOV, zoomInDuration);
        toMoveCamera = true;
    }


    #region movement
    // To move the camera player needs to click right mouse button and drag the mouse.
    public void Update()
    {
        // safe mouse pos during the first frame of the button click to prevent camera jumps
        if (Input.GetMouseButtonDown(1))
            lastMousePos = Input.mousePosition;

        if (Input.GetMouseButton(1) && toMoveCamera)
            MoveCamera();
    }
    private void MoveCamera()
    {
        Vector3 mousePos = Input.mousePosition / 10;
        Vector3 lastMousePosAdjusted = (lastMousePos / 10);
        Vector3 delta = mousePos - lastMousePosAdjusted;


        Vector3 moveDir = new Vector3(delta.x, delta.y, 0);
        Vector3 movement = moveDir * moveSpeed * Time.deltaTime;
        camera.transform.Translate(movement);

        //Debug.Log($"Delta = {delta}");
        //Debug.Log($"Movement = {movement}");


        lastMousePos = Input.mousePosition;
    }
    #endregion

    #region focusing
    public void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (CalmingObjectAhead(out calmingObject))
            {
                toMoveCamera = false;
                AlignTheCameraWithTheObject(calmingObject); // put target obj in center of the view
                ZoomIn();
                Invoke(nameof(FireEvent), zoomInDuration);
            }
        }
    }
    private bool CalmingObjectAhead(out CalmingObjectController calmingObj)
    {
        RaycastHit hit;

        var ray = camera.ScreenPointToRay(Input.mousePosition);
        //Debug.DrawRay(gameObject.transform.position, ray.direction * 100f, Color.green, 5f);

        if (Physics.Raycast(ray, out hit, 100f, calmingObjLayer))
        {
            calmingObj = hit.transform.GetComponent<CalmingObjectController>();
            return true;
        }
        else
        {
            calmingObj = null;
            return false;
        }
    }
    private void AlignTheCameraWithTheObject(CalmingObjectController objToAlignAgainst)
    {
        //create a ray
        Ray ray = new Ray(objToAlignAgainst.transform.position, -Vector3.forward);
        Debug.DrawRay(objToAlignAgainst.transform.position, -Vector3.forward * 10f, Color.blue, 5f);

        //cast it
        Physics.Raycast(ray, distToPicture);

        //take the furthest point
        Vector3 cameraPos = ray.GetPoint(distToPicture);
        camera.transform.DOMove(cameraPos, 2f);
    }
    private void ZoomIn()
    {
        camera.DOFieldOfView(zoomedPOV, zoomInDuration);
    }
    private void FireEvent()
    {
        UtilitiesHolder.Instance.GetUtility<EventBus>()
                .InvokeEvent<RhythmGameStartEvent>(new RhythmGameStartEvent(calmingObject));
    }
    #endregion
}