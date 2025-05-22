using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController cc;
    public float moveSpeed;
    public float jumpForce;
    public float gravity;
    private Vector3 moveDir;
    private bool isGrounded;

    void Update()
    {
        isGrounded = cc.isGrounded; // Vérifie si le joueur est au sol

        moveDir = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, moveDir.y, Input.GetAxis("Vertical") * moveSpeed);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            moveDir.y = jumpForce;
        }

        moveDir.y -= gravity * Time.deltaTime;

        if (moveDir.x != 0 || moveDir.z != 0)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(new Vector3(moveDir.x, 0, moveDir.z)), 0.15f);
        }

        cc.Move(moveDir * Time.deltaTime);
    }
}