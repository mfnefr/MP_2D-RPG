using UnityEngine;

public class InteractIcon : MonoBehaviour
{
    private Vector3 _offset = new Vector3(0, 1.5f, 0);
    private Transform _target;

    void Start()
    {
        gameObject.SetActive(false);
    }
    
    public void Show(Transform target)
    {
        _target = target;
        transform.position = _target.position + _offset;
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
