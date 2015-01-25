using UnityEngine;
using System.Collections;

public class IKCtrl : MonoBehaviour
{
    protected Animator animator;

    public Transform tailEnd = null;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    //a callback for calculating IK
    void OnAnimatorIK()
    {
        if (animator)
        {
            //weight = 1.0 for the right hand means position and rotation will be at the IK goal (the place the character wants to grab)
            animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1.0f);
            animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1.0f);

            //set the position and the rotation of the right hand where the external object is
            if (tailEnd != null)
            {
                animator.SetIKPosition(AvatarIKGoal.RightHand, tailEnd.position);
                animator.SetIKRotation(AvatarIKGoal.RightHand, tailEnd.rotation);
            }
        }
    }
}
