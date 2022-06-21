using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    private ControllerAnimator controllerAnimator;

    public float moveSpeed = 5f;
    private Vector2 moveIput;
    private new Rigidbody2D rigidbody;
    public bool isPlayer = false;

    // Start is called before the first frame update
    void Awake()
    {

        moveIput.x = 0;
        rigidbody = this.transform.GetComponent<Rigidbody2D>();

        controllerAnimator = transform.GetComponent<ControllerAnimator>();
    }


    public float chance = 10f;
    // Update is called once per frame
    void Update()
    {
        if (isPlayer)
        {
            moveIput.x = Math.Sign(Input.GetAxisRaw("Horizontal"));
            moveIput.y = Math.Sign(Input.GetAxisRaw("Vertical"));
            controllerAnimator.ChangeAnimationState(GetAnimeState());
        }
        else
        {
            if(UnityEngine.Random.Range(0f, 1000.0f) <= chance)
                controllerAnimator.ChangeAnimationState("VMove",true);
        }
    }

    void FixedUpdate()
    {
        if (isPlayer)
        {
            rigidbody.MovePosition(rigidbody.position + moveIput.normalized * moveSpeed * Time.fixedDeltaTime);
        }
    }

    private string GetAnimeState()
    {
        string animeState = "New State";

        if(moveIput.y > 0.01f)
        {
            animeState = "ANIM_PLAYER_UP";
        }
        else if (moveIput.x > 0.1f)
        {
            animeState = "ANIM_PLAYER_RIGHT";
        }
        else if (moveIput.x < -0.1f)
        {
            animeState = "ANIM_PLAYER_LEFT";
        }
        else if (moveIput.y < -0.01f)
        {
            animeState = "ANIM_PLAYER_DOWN";
        }
        return animeState;
    }
}
