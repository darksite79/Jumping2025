using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 3f;         // Velocidad de movimiento horizontal
    public float maxJumpForce = 3f;     // Fuerza máxima de salto
    public float jumpChargeRate = 0.5f;   // Velocidad de carga del salto

    private Rigidbody2D rb;
    private bool isGrounded;
    private float jumpCharge;
    private bool isChargingJump;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Movimiento horizontal
        float move = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(move * moveSpeed, rb.velocity.y);

        // Iniciar carga de salto
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            isChargingJump = true;
            jumpCharge = 0f;
        }

        // Cargar el salto
        if (isChargingJump && Input.GetKey(KeyCode.Space))
        {
            jumpCharge += jumpChargeRate * Time.deltaTime;
            jumpCharge = Mathf.Clamp(jumpCharge, 0, maxJumpForce);
        }

        // Liberar el salto
        if (Input.GetKeyUp(KeyCode.Space) && isChargingJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpCharge);
            isChargingJump = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
