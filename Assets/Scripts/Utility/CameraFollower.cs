using UnityEngine;
using System;

public class CameraFollower : MonoBehaviour {

    [SerializeField]
    private bool followPosition;

    [SerializeField]
    private bool followDirection;

    [SerializeField]
    private Transform target;

    [SerializeField]
    private Vector3 offSetDirection;

    [SerializeField]
    private Vector3 offSetPosition;

    private Transform m_transform;

    [SerializeField]
    private float height = 5f;

	void Awake () {
	    if(!target)
        {
            Debug.Log("Not found target in scrip CameraFollower in GameObject " + name);
            Destroy(this);
        }

        m_transform = transform;
    }
	
	void Update () {
        if(followPosition)
        {
            Vector3 targetPosition = target.TransformPoint(offSetPosition);
            targetPosition.y = height;
            m_transform.position = targetPosition;
        }

        if (followDirection)
        {
            m_transform.LookAt (target);
        }
    }
}
