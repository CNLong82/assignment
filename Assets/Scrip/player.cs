using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class player : MonoBehaviour
{
    private AudioSource audioSource;

    private int killCount = 0;
    public int killToWin = 10;

    public Image healthBar;
    public float maxHealth = 1f;
    public float currentHealth = 1f;
    public float healAmount = 0.1f;

    public float moveSpeed = 5f;
    private Animator animator;
    private Rigidbody2D rb;

    public float attackRange = 1f;
    public LayerMask enemyLayer;
    public Transform attackPoint;

    public TextMeshProUGUI scoreText;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = rb.GetComponent<Animator>();
        UpdateHealthUI();
        UpdateScoreUI();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, vertical, 0).normalized;
        transform.Translate(movement * moveSpeed * Time.deltaTime);

        if (horizontal > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (horizontal < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        bool isRunning = movement.magnitude > 0.1f;

        if (isRunning)
        {
            animator.SetBool("IsRunning", true);
        }
        else
        {
            animator.SetBool("IsRunning", false);
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            animator.SetTrigger("Attack");

            if (audioSource != null)
            {
                audioSource.Play(); 
            }

            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

            foreach (Collider2D enemy in hitEnemies)
            {
                enemy enemyScript = enemy.GetComponent<enemy>();
                if (enemyScript != null)
                {
                    enemyScript.Die();
                    killCount++;
                    UpdateScoreUI();

                    if (killCount >= killToWin)
                    {
                        Win();
                    }
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    void UpdateHealthUI()
    {
        if (healthBar != null)
        {
            healthBar.fillAmount = currentHealth / maxHealth;
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
        UpdateHealthUI();
    }

    void Die()
    {
        SceneManager.LoadScene("Die");
    }
    void Win()
    {
        SceneManager.LoadScene("Win");
    }
    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + killCount.ToString();
        }
    }
}

