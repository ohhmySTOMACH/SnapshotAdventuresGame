using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.Animations;

namespace Player
{
    public class MovementManager : MonoBehaviour
    {
        [SerializeField] float mouseSensitivity = 3f;
        [SerializeField] float moveSpeed = 5;
        [SerializeField] float mass = 1f;
        [SerializeField] float jumpHeight = 5f;
        [SerializeField] Transform cameraTransform;
        private CharacterController controller;
        private Vector2 look;
        private Vector3 velocity;
        private bool groundedPlayer;

        void Start()
        {
            controller = GetComponent<CharacterController>();
            Cursor.lockState = CursorLockMode.Locked;
        }

        void Update()
        {
            UpdateGravity();
            RotateView();
            MovePlayer();
        }

        void UpdateGravity()
        {
            var gravity = Physics.gravity * mass * Time.deltaTime;
            if (controller.isGrounded)
            {
                velocity.y = -1f;
            }
            else
            {
                velocity.y = velocity.y + gravity.y;
            }
        }

        void RotateView()
        {
            look.x += Input.GetAxis("Mouse X") * mouseSensitivity;
            look.y += Input.GetAxis("Mouse Y") * mouseSensitivity;

            cameraTransform.localRotation = Quaternion.Euler(-look.y, 0f, 0f);
            transform.localRotation = Quaternion.Euler(0f, look.x, 0f);
        }

        void MovePlayer()
        {
            float xValue = Input.GetAxis("Horizontal");
            float zValue = Input.GetAxis("Vertical");

            Vector3 move = Vector3.ClampMagnitude(new Vector3(xValue, 0f, zValue), 1f);
            move = transform.TransformDirection(move);

            if (Input.GetKeyDown(KeyCode.Space) && controller.isGrounded)
            {
                velocity.y += jumpHeight;
            }

            controller.Move((move * moveSpeed + velocity) * Time.deltaTime);
        }
    }

}
