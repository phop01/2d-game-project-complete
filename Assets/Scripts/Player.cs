using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.UI;


public class Player : MonoBehaviour
{
    

    Gun[] guns;
    bool shoot;

    private Rigidbody2D rb;
    public float jumpForce = 5f;
    public bool isGround;
    private Animator anim;

    public AudioSource jumpSound;

    public int maxHealth = 3; // จำนวนเลือดสูงสุด
    private int currentHealth; // จำนวนเลือดปัจจุบัน
    public Text healthText; // อ้างอิงถึง Text UI สำหรับแสดงเลือด

    public float blinkDuration = 0.1f; // ระยะเวลาที่กระพริบ
    public int blinkCount = 6; // จำนวนครั้งที่กระพริบ

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentHealth = maxHealth; // กำหนดเลือดเริ่มต้น
    }

    void Start()
    {
        guns = GetComponentsInChildren<Gun>();
        UpdateHealthText(); // อัปเดต Text เมื่อตัวละครเริ่ม
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && isGround)
        {
            PlayerJump();
            jumpSound.Play();
        }
        anim.SetBool("isJumping", isGround);

        shoot = Input.GetMouseButton(1);
        if (shoot)
        {
            shoot = false;
            foreach (Gun gun in guns)
            {
                if (gun.gameObject.activeSelf)
                {
                    gun.Shoot();
                }
            }
        }
    }

    void PlayerJump()
    {
        isGround = false;
        rb.velocity = Vector2.up * jumpForce;
        GameManager.Instance.AddScore();
    }

    private void OnCollisionEnter2D(Collision2D target)
    {
        if (target.gameObject.CompareTag("Ground"))
        {
            isGround = true;
        }
        if (target.gameObject.CompareTag("obstacle"))
        {
            TakeDamage(target); // เรียกใช้ฟังก์ชันลดเลือดและส่ง target
        }
    }

    private void TakeDamage(Collision2D target)
    {
        currentHealth--; // ลดเลือดลง 1 หน่วย
        UpdateHealthText(); // อัปเดต Text

        if (currentHealth <= 0)
        {
            GameManager.Instance.Gameover(); // เรียกใช้ Game Over เมื่อเลือดหมด
        }
        else
        {
            StartCoroutine(BlinkCoroutine()); // เริ่มกระพริบเมื่อโดนโจมตี
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), target.collider, true); // ข้ามการชน
            Debug.Log("Current Health: " + currentHealth);
        }
    }

    private void UpdateHealthText()
    {
        if (healthText != null)
        {
            healthText.text = "Health: " + currentHealth; // อัปเดตข้อความ Health
        }
    }

    private IEnumerator BlinkCoroutine()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Color originalColor = spriteRenderer.color;

        for (int i = 0; i < blinkCount; i++)
        {
            spriteRenderer.color = Color.red; // เปลี่ยนสีเป็นสีแดง
            yield return new WaitForSeconds(blinkDuration);
            spriteRenderer.color = originalColor; // คืนค่ากลับไปที่สีเดิม
            yield return new WaitForSeconds(blinkDuration);
        }
    }
}