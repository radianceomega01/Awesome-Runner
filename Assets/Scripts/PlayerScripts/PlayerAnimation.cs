using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    Animation animation;
    void Start()
    {
        animation = GetComponent<Animation>();
    }

    public void JumpAnimation()
    {
        animation.Stop(Tags.PLAYER_RUN);
        animation.Play(Tags.PLAYER_JUMP);
        animation.PlayQueued(Tags.PLAYER_JUMPFALL);
    }

    public void LandingAnimation()
    {
        animation.Stop(Tags.PLAYER_JUMPFALL);
        animation.Blend(Tags.PLAYER_JUMPLAND, 0);
    }

    public void RuningAnimation()
    {
        animation.Stop(Tags.PLAYER_IDLE);
        /*animation.Stop(Tags.PLAYER_JUMP);
        animation.Stop(Tags.PLAYER_JUMPFALL);*/
        animation.Play(Tags.PLAYER_RUN);
    }
}
