using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera _camera;
    [SerializeField] private Transform _target;

    void Start()
    {
        _camera = GetComponent<Camera>();

        ZoomIn();
    }

    void LateUpdate()
    {
        if(_target != null)
        {
            Vector3 newPos = new Vector3(_target.position.x, _target.position.y, transform.position.z);
            transform.position = newPos;
        }
    }

    public void ZoomIn()
    {
        _camera.orthographicSize = 5;
    }

    public void ZoomOut()
    {
        _camera.orthographicSize = 7;
    }
}
