using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDelivaryController : MonoBehaviour
{
    public GameObject ballGO;
    public Rigidbody ballRigidBody;

    private const float speed = 1000;

    private Action OnCompletionOfBallDelivary;
    private void Reset()
    {
        ballRigidBody.useGravity = false;
        ballRigidBody.velocity = Vector3.zero;
        ballRigidBody.angularVelocity = Vector3.zero;
        ballGO.transform.localPosition = Vector3.zero;
    }

    public void DeliverBall(Action callback) 
    {
        OnCompletionOfBallDelivary = callback;

        ballRigidBody.useGravity = true;
        ballRigidBody.AddForce(Vector3.forward * speed);

        StartCoroutine(ResetBall());
    }

    private IEnumerator ResetBall()
    {
        yield return new WaitForSeconds(2f);
        Reset();
        OnCompletionOfBallDelivary();
    }
}
