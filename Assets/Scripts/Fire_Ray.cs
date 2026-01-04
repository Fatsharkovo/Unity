using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire_Ray : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;//锁定隐藏鼠标
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            EmissionRay();
        }
    }

    private void EmissionRay()
    {
        Ray ray=Camera.main.ScreenPointToRay(Input.mousePosition);  //发射射线
        RaycastHit raycasthit;  //存储射线碰到的物体信息
        if(Physics.Raycast(ray,out raycasthit))
        {
            print(raycasthit.transform.name);
        }
    }
}
