using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetWalking(bool isWalking)
    {
        animator.SetBool("IsWalking", isWalking);
    }

    public void SetAttacking(bool isAttacking)
    {
        animator.SetBool("IsAttacking", isAttacking);
    }

    public void SetHit()
    {
        animator.SetTrigger("IsHit");
    }

    public void SetDead(bool isDead)
    {
        animator.SetBool("IsDead", isDead);
    }
}
