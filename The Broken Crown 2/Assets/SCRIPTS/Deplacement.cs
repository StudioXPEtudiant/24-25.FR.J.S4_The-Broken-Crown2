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

    public Transform cameraTransform; // Référence à la caméra
    public float mouseSensitivity = 3f; // Sensibilité de la souris
    private float rotationX = 0f;
    private float rotationY = 0f;

    void Update()
    {
        isGrounded = cc.isGrounded;

        // Déplacement relatif à la caméra
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        Vector3 moveDirection = (right * Input.GetAxis("Horizontal") + forward * Input.GetAxis("Vertical")) * moveSpeed;
        moveDir = new Vector3(moveDirection.x, moveDir.y, moveDirection.z);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            moveDir.y = jumpForce;
        }

        moveDir.y -= gravity * Time.deltaTime;

        if (moveDir.magnitude > 0)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDir), 0.15f);
        }

        cc.Move(moveDir * Time.deltaTime);

        // La caméra suit la souris en permanence
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f); // On garde la limitation verticale pour éviter les angles extrêmes

        rotationY += mouseX;

        cameraTransform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
        transform.rotation = Quaternion.Euler(0f, rotationY, 0f);
    }
}