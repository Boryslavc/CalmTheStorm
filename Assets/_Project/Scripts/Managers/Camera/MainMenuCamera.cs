using UnityEngine;



/// <summary>
/// Camera for the main menu.
/// </summary>
public class MainMenuCamera : MonoBehaviour
{
    private Camera camera;

    private float moveSpeed = 2f;
    private bool moveLeft = true;
    private float border = 26f;


    public void Awake()
    {
        camera = GetComponent<Camera>();

        //Set up camera component
        camera.orthographic = false;
        camera.fieldOfView = 40;
    }
    

    public void Update()
    {
        var dir = moveLeft ? -1 : 1;

        camera.gameObject.transform.Translate(new Vector3(dir * moveSpeed * Time.deltaTime,0,0));

        if (camera.gameObject.transform.position.x < -36f)
            moveLeft = false;
        else if(camera.gameObject.transform.position.x > 36f)
            moveLeft = true;
    }
}
