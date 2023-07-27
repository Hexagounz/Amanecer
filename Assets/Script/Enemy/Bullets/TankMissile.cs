using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMissile : MonoBehaviour {

    private Transform target;

    [SerializeField] Transform partToRotate;
    [SerializeField] float turnSpeed = 5f;
    [SerializeField] float speed = 4f;

    public void Seek (Transform _target)
    {
        target = _target;
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(-90f, rotation.y, 0f);

        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            //HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);

        Destroy(gameObject, 3f);
    }

    // void HitTarget()
    // {
    //something about instantiating the effect
    //update, a la final no me he atrevido a hacer el vfx de esto xd.
    //}


}
