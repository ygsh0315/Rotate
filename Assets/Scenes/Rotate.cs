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
    // axis  : 회전축 벡터
    // speed : 회전 속도
    // 현재 타겟과의 관계에 따라 회전하기
    private void RotateAround0(in Vector3 axis, float speed)
    {
        float t = speed * Time.deltaTime;
        transform.RotateAround(target.position, axis, t);
    }

    // axis  : 회전축 벡터
    // diff  : (타겟의 위치 - 자신의 위치) 벡터
    // speed : 회전 속도
    // t     : 현재 회전값을 기억할 변수
    //타겟과 거리관계를 유지한 채로 회전하기
    private void RotateAround1(in Vector3 axis, in Vector3 diff, float speed, ref float t)
    {
        t += speed * Time.deltaTime;

        Vector3 offset = Quaternion.AngleAxis(t, axis) * diff;
        transform.position = target.position + offset;
    }

    // axis  : 회전축 벡터
    // diff  : (타겟의 위치 - 자신의 위치) 벡터
    // speed : 회전 속도
    // t     : 현재 회전값을 기억할 변수
    //타겟과의 거리를 유지하고 타겟을 바라보며 회전하기
    private void RotateAround2(in Vector3 axis, in Vector3 diff, float speed, ref float t)
    {
        t += speed * Time.deltaTime;

        Vector3 offset = Quaternion.AngleAxis(t, Vector3.up) * diff;
        transform.position = target.position + offset;

        Quaternion rot = Quaternion.LookRotation(-offset, axis);
        transform.rotation = rot;
    }

    //대상지점 천천히 바라보기
    private void LookAtSlowly(Transform target, float speed = 1f)
    {
        if (target == null) return;

        Vector3 dir = target.position - transform.position;
        var nextRot = Quaternion.LookRotation(dir);

        transform.rotation = Quaternion.Slerp(transform.rotation, nextRot, Time.deltaTime * speed);
    }
}

