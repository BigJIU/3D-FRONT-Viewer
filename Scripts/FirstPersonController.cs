using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 人物移动的脚本，挂载到player上
/// </summary>
public class FirstPersonController : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;//设定移动速度
    public float mousespeed = 5;

    Vector3 move;//用来控制角色的三维方向运动

    // Update is called once per frame
    void Update()
    {
        float x=0, z=0,y = 0;
        
        x = Input.GetAxis("Horizontal");//获得水平和垂直两个坐标
        z = Input.GetAxis("Vertical");//默认上下左右移动
        if (Input.GetKey(KeyCode.E))
        {
            y = 1;
        }else if (Input.GetKey(KeyCode.Q))
        {
            y = -1;
        }
        move = (transform.right * x + transform.forward * z + transform.up * y)*speed;
        transform.position += move * Time.deltaTime;
        
        float mx, my;
        my = Input.GetAxis("Mouse X")*mousespeed*Time.deltaTime;//鼠标的x位置
        mx = -Input.GetAxis("Mouse Y") * mousespeed * Time.deltaTime;//鼠标的y位置
        this.transform.localEulerAngles += new Vector3(mx, my, 0);
        // var angle = this.transform.localEulerAngles;
        // angle.x = Mathf.Clamp(angle.x, 0, 45);
        // angle.y = Mathf.Clamp(angle.y, -90, 90);
        // if (angle.x > 90) angle.x = 90;
        // else if (angle.x < -90) angle.x = -90;
        // this.transform.localEulerAngles = angle;

    }
}