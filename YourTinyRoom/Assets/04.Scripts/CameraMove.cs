using UnityEngine;
using System.Collections;
public class CameraMove : MonoBehaviour
{
    Camera mainCamera;
    public float SizeMin = 4f;
    public float SizeMax = 7f;
    private void Awake()
    {
        mainCamera = Camera.main;
    }
    void Update()
    {
        //휠버튼으로 클릭시 해당 방향으로 카메라 이동
        if (Input.GetMouseButton(2))
        {
            var screenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y,0f);// z값을 Plane Distance 값을 줘야 합니다!!
            transform.position = Vector3.Lerp(transform.position, mainCamera.ScreenToWorldPoint(screenPoint),Time.deltaTime*0.2f);
        }
        //휠버튼 돌려서 확대 및 축소
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        mainCamera.orthographicSize = Mathf.Clamp(mainCamera.orthographicSize, SizeMin, SizeMax);
        mainCamera.orthographicSize += scroll;

    }
}