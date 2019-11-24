using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flap : MonoBehaviour
{
    Transform t;

    [SerializeField]
    private GameObject Right;

    [SerializeField]
    private GameObject Left;


    // Start is called before the first frame update
    void Start()
    {
        t = transform;

        if (Right == null)
        {
            Debug.LogErrorFormat("Missing 'Right' GameObject!");
        }
        if (Left == null)
        {
            Debug.LogErrorFormat("Missing 'Left' GameObject!");
        }

    }

    float rightAngle = 60;
    float leftAngle = 60;
    // Update is called once per frame
    void Update()
    {
        t.eulerAngles = new Vector3(t.eulerAngles.x, t.eulerAngles.y, t.eulerAngles.z);

        var move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        transform.position += move * 8 * Time.deltaTime;
        if (transform.position.y < -1)
        {
            transform.position = new Vector3(0, 0, 0);
        }

        if (Input.GetKeyDown("space")){
            Vector3 jump = GetComponent<Rigidbody>().velocity;
            jump.y = 10;
            GetComponent<Rigidbody>().velocity = jump;
            rightAngle = leftAngle = 120;
        }

        RotateRightLimb(Right, rightAngle);
        RotateLeftLimb(Left, leftAngle);
        if (Right.transform.localRotation.eulerAngles.z> 100) {
            rightAngle = leftAngle = 60;

        }

    }


    private void RotateLeftLimb(GameObject limb, float limbTargetAngle)
    {
        Quaternion limbRot = limb.transform.localRotation;
        Vector3 limbRotEuler = limbRot.eulerAngles;
        if (!Mathf.Approximately(limbRotEuler.z, limbTargetAngle))
        {
            float deltaAngle = limbTargetAngle - limbRotEuler.z;

            Vector3 localRotationAxis = new Vector3(0,0,1);

            limb.transform.Rotate(localRotationAxis, deltaAngle * 3 * Time.deltaTime);
        }
    }

    private void RotateRightLimb(GameObject limb, float limbTargetAngle)
    {
        Quaternion limbRot = limb.transform.localRotation;
        Vector3 limbRotEuler = limbRot.eulerAngles;
        if (!Mathf.Approximately(limbRotEuler.z, limbTargetAngle))
        {
            float deltaAngle = limbTargetAngle - limbRotEuler.z;

            Vector3 localRotationAxis = limb.transform.InverseTransformVector(Vector3.forward);

            limb.transform.Rotate(localRotationAxis, deltaAngle * 3 * Time.deltaTime);
        }
    }


}
