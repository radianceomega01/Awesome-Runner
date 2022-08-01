using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Transform playerTarget;
    [SerializeField]
    private float offsetX = 7f;
    [SerializeField]
    private float offsetz = -13f;
    [SerializeField]
    private float constY = 1f;
    [SerializeField]
    private float lerpTime = 0.01f;

    // Start is called before the first frame update
    void Awake()
    {
        playerTarget = GameObject.FindGameObjectWithTag(Tags.PLAYER_TAG).transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (playerTarget)
        {
            Vector3 targetPosition = new Vector3(playerTarget.position.x + offsetX, playerTarget.position.y + constY, playerTarget.position.z + offsetz);
            transform.position = Vector3.Lerp(transform.position, targetPosition, lerpTime);
        }
    }
}
