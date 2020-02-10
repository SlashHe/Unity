using UnityEngine;
using System.Collections;

public class Camera_Control : MonoBehaviour
{


    private float speed = 0.1f;
    private float m_deltX = 0;
    private float m_deltY = 0;
    private float m_mSpeed = 5f;
    private Vector3 V3_chushi;
    public Vector3 V3_dongtai;
    public Vector3 V3_final;
    public bool diyici = true;
    void start()
    {


    }


    float ScrollWheel;
    void Update()
    {

        if (diyici)
        {

            V3_chushi = transform.eulerAngles;
        }

        //鼠标右键控制旋转
        if (Input.GetMouseButton(1))
        {

            diyici = false;
            m_deltX += Input.GetAxis("Mouse X") * m_mSpeed / 2;
            m_deltY -= Input.GetAxis("Mouse Y") * m_mSpeed / 2;
            V3_dongtai = new Vector3(m_deltY, m_deltX, 0);
            V3_final = V3_chushi + V3_dongtai;
            transform.rotation = Quaternion.Euler(V3_final);

        }
        if (Input.GetMouseButtonUp(1))
        {

            diyici = true;
            m_deltX = 0;
            m_deltY = 0;
        }
        Vector3 dir = new Vector3(transform.forward.x, 0, transform.forward.z);
        //键盘控制移动
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * speed, Space.Self);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * speed, Space.Self);
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(dir * speed, Space.World);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(-dir * speed, Space.World);
        }

        ScrollWheel = Input.GetAxis("Mouse ScrollWheel");
        transform.Translate(Vector3.forward * ScrollWheel, Space.Self);


        if (Input.GetKey(KeyCode.Space))
        {

            transform.Translate(Vector3.up * speed, Space.Self);
        }
        if (Input.GetKey(KeyCode.LeftControl))
        {
            transform.Translate(Vector3.down * speed, Space.Self);
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {

            speed = 0.2f;

        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = 0.1f;
        }
    }

}
