using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;

public struct JointData
{
    public Joint joint;
    public float previousForce;

    public JointData(Joint newJoint)
    {
        this.joint = newJoint;
        previousForce = 0f;
    }
}

public class PhysicsScorer : MonoBehaviour
{
    JointData[] _jointData;

    public Transform ragdoll;
    
    public TMP_Text forceScore;
    public float score;

    void Start()
    {
        Joint[] joints = ragdoll.GetComponentsInChildren<Joint>();
        _jointData = new JointData[joints.Length];

        for (int index = 0; index < _jointData.Length; index++)
        {
            Joint joint = joints[index];

            _jointData[index] = new JointData(joint);
        }
    }

    void FixedUpdate()
    {
        for(int index = 0; index < _jointData.Length; index++)
        {
            JointData joinData = _jointData[index];
            float tempScore = Mathf.Abs(joinData.previousForce - joinData.joint.currentForce.magnitude);

            if (tempScore >= 100)
            {
                score += tempScore;
                
            }

            joinData.previousForce = joinData.joint.currentForce.magnitude;
        }

        forceScore.text = score.ToString();

    }
}
