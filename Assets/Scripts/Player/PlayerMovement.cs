using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody myBody;
    float playerVelocity = 8f;

    float jumpPower = 500f;
    float doubleJumpPower = 400f;
    float playerLastPosY;
    bool isOnGround;
    bool canDoubleJump;
    bool isgameStarted;
    bool jumpLanding;
    [SerializeField]
    LayerMask layerGround;
    float radius = 0.2f;
    [SerializeField]
    Transform groundCheck;
    IEnumerator coroutine;
    PlayerAnimation anim;

    BGScroller bgScroller;
    Button jumpBtn;
    bool fall;
    // Start is called before the first frame update
    void Awake()
    {
        myBody = GetComponent<Rigidbody>();
        anim = GetComponent<PlayerAnimation>();
        bgScroller = GameObject.Find(Tags.BACKGROUND_TAG).GetComponent<BGScroller>();
        jumpBtn = GameObject.Find(Tags.JUMPBTN_TAG).GetComponent<Button>();
        jumpBtn.onClick.AddListener(() => PlayerJump());
    }

    private void Start()
    {
        coroutine = StartingWait();
        StartCoroutine(coroutine);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (isgameStarted)
        {
            PlayerRun();
            //PlayerJump();
            isOnGround = Physics.OverlapSphere(groundCheck.position, radius, layerGround).Length > 0;
            if (myBody.position.y < playerLastPosY && !isOnGround && !fall)
            {
                anim.JumpAnimation();
                fall = true;
            }
            else if (isOnGround)
            {
                fall = false;
            }
            playerLastPosY = myBody.position.y;
        }
    }

    void PlayerRun()
    {
        myBody.velocity = new Vector3(playerVelocity, myBody.velocity.y, 0);
    }

    private void OnCollisionEnter(Collision target)
    {
        if (target.gameObject.tag == Tags.PLATFORM_TAG)
        {
            if (jumpLanding)
            {
                jumpLanding = false;
                anim.LandingAnimation();
                anim.RuningAnimation();
            }
            else {
                if(isgameStarted)
                anim.RuningAnimation();
            }
        }
    }
    void PlayerJump()
    {
        /*if (isOnGround)
        {
            anim.JumpAnimation();
            myBody.AddForce(new Vector3(0, jumpPower, 0));
            playerJumpedOnce = true;
            jumpLanding = true;
        }
        else if (playerJumpedOnce)
        {
            anim.JumpAnimation();
            myBody.AddForce(new Vector3(0, doubleJumpPower, 0));
            playerJumpedOnce = false;
            jumpLanding = true;
        }*/
        if (!isOnGround && canDoubleJump)
        {
            canDoubleJump = false;
            anim.JumpAnimation();
            myBody.AddForce(new Vector3(0, doubleJumpPower, 0));

        }
        else if (isOnGround)
        {
            anim.JumpAnimation();
            myBody.AddForce(new Vector3(0, jumpPower, 0));
            jumpLanding = true;
            canDoubleJump = true;
        }
    }

    IEnumerator StartingWait()
    {
        yield return new WaitForSeconds(1f);
        isgameStarted = true;
        anim.RuningAnimation();
        bgScroller.canScroll = true;
    }
}
