using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public Transform target;
    public float rotateSpeed = 50f;
    public Vector3 axis = new Vector3(0f, 1f, 0f);
    public Vector3 diff = new Vector3(4f, 0f, 0f);
    private float t = 0;
    public bool rot0;
    public bool rot1;
    public bool rot2;
    public bool rotSlow;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (rot0)
        {
            RotateAround0(axis, rotateSpeed);
        }
        if (rot1)
        {
            RotateAround1(axis, diff, rotateSpeed, ref t);
        }
        if (rot2)
        {
            RotateAround2(axis, diff, rotateSpeed, ref t);
        }
        if (rotSlow)
        {
            LookAtSlowly(target);
        }
    }
    // axis  : ȸ���� ����
    // speed : ȸ�� �ӵ�
    // ���� Ÿ�ٰ��� ���迡 ���� ȸ���ϱ�
    private void RotateAround0(in Vector3 axis, float speed)
    {
        float t = speed * Time.deltaTime;
        transform.RotateAround(target.position, axis, t);
    }

    // axis  : ȸ���� ����
    // diff  : (Ÿ���� ��ġ - �ڽ��� ��ġ) ����
    // speed : ȸ�� �ӵ�
    // t     : ���� ȸ������ ����� ����
    //Ÿ�ٰ� �Ÿ����踦 ������ ä�� ȸ���ϱ�
    private void RotateAround1(in Vector3 axis, in Vector3 diff, float speed, ref float t)
    {
        t += speed * Time.deltaTime;

        Vector3 offset = Quaternion.AngleAxis(t, axis) * diff;
        transform.position = target.position + offset;
    }

    // axis  : ȸ���� ����
    // diff  : (Ÿ���� ��ġ - �ڽ��� ��ġ) ����
    // speed : ȸ�� �ӵ�
    // t     : ���� ȸ������ ����� ����
    //Ÿ�ٰ��� �Ÿ��� �����ϰ� Ÿ���� �ٶ󺸸� ȸ���ϱ�
    private void RotateAround2(in Vector3 axis, in Vector3 diff, float speed, ref float t)
    {
        t += speed * Time.deltaTime;

        Vector3 offset = Quaternion.AngleAxis(t, Vector3.up) * diff;
        transform.position = target.position + offset;

        Quaternion rot = Quaternion.LookRotation(-offset, axis);
        transform.rotation = rot;
    }

    //������� õõ�� �ٶ󺸱�
    private void LookAtSlowly(Transform target, float speed = 1f)
    {
        if (target == null) return;

        Vector3 dir = target.position - transform.position;
        var nextRot = Quaternion.LookRotation(dir);

        transform.rotation = Quaternion.Slerp(transform.rotation, nextRot, Time.deltaTime * speed);
    }
}

