using UnityEngine;
using System.Collections;

public class CCameraZoom : MonoBehaviour {

    float zoom = 20;
    float normal = 60;
    float smooth = 5.0f;

    bool isZoom = false;
    [SerializeField]
    GameObject _target;
    [SerializeField]
    GameObject _spotLight;
    Camera _camera;
    
    // Use this for initialization
    void Start () {
        _camera = GetComponent<Camera>();
	}

    void GetTarget(GameObject unit)
    {
        _target = unit;
        _target.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        StartCoroutine("Zoom");
    }

    public void DestroyTarget()
    {
        _target = null;
    }

    IEnumerator Zoom()
    {
        while (_target != null)
        {
            _spotLight.SetActive(true);
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(_target.transform.position.x, 0.1f, _target.transform.position.z + 0.7f), 0.1f);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0f, 180f, 0f), 3f);

            _camera.fieldOfView = Mathf.Lerp(_camera.fieldOfView, zoom, Time.deltaTime * smooth);
            yield return null;
        }

        _spotLight.SetActive(false);
        Invoke("ZoomOut", 1f);

        while (true)
        {
            _camera.fieldOfView = Mathf.Lerp(_camera.fieldOfView, normal, Time.deltaTime * smooth);
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(0f, 5f, 0f), 0.1f);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(90f,0f,0f),5f);
            yield return null;
        }
    }

    void ZoomOut()
    {
        StopCoroutine("Zoom");
    }
}