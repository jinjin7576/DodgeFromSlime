using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    float maxX;
    float minX;
    int jumpCount=0;
    int maxJumpCount;
    bool isJumped;
    bool isDead=false;
    public GameObject PowerUp;
    Rigidbody2D Rigidbody2D;
    Animator ani;

    void Start()
    {
        maxX = 4.5f; minX = -4.5f;
        maxJumpCount = 2;
        //gameObject.transform.position = Vector3.zero;
        Rigidbody2D = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
    }

    void Update()
    {
        if(isDead) return;
       if (transform.position.x <= minX)
        {
            transform.position = new Vector2 (minX, transform.position.y);
        }
       if (transform.position.x >= maxX)
        {
            transform.position = new Vector2(maxX, transform.position.y);
        }
        PlayerMove();
        PlayerJump();
        ani.SetBool("isJump", isJumped);
    }

    void PlayerMove()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
    }
    void PlayerJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJumpCount)
        {
            jumpCount++;
            Rigidbody2D.linearVelocity = Vector2.zero;
            Rigidbody2D.AddForce(Vector2.up * jumpForce , ForceMode2D.Impulse);
        }
        else if(Input.GetKeyUp(KeyCode.Space) && Rigidbody2D.linearVelocity.y > 0)
        {
            Rigidbody2D.linearVelocity = Rigidbody2D.linearVelocity / 2;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contacts[0].normal.y > 0.7f)

        {
            isJumped = false; //바닥에 닿은 경우
            jumpCount = 0; //점프 카운트 초기화
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        isJumped = true;
    }
    //PlayerController.cs
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Dead")
        {
            ani.SetTrigger("Die");
            isDead = true;
            GameManager.instance.GameOver();
        }
        else if (collision.gameObject.tag == "Item")
        {
            StartCoroutine(GainItem());
        }
    }
        IEnumerator GainItem()
    {
        maxJumpCount = 3;
        PowerUp.SetActive(true);
        yield return new WaitForSeconds(5f);
        PowerUp.SetActive(false);
        maxJumpCount = 2;
    }
}
