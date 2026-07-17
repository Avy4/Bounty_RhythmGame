using System.Threading.Tasks;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    [SerializeField] int delay = 20;
    [SerializeField] Animator characterAnimator;
    [SerializeField] ScoreManager scoreManager;

    private bool initalClick = false;
    private CapsuleCollider2D beatDetectionCollider;
    void Start()
    {
        beatDetectionCollider = GetComponent<CapsuleCollider2D>();
        beatDetectionCollider.enabled = false;
    }

    async Task OnAttack()
    {   
        beatDetectionCollider.enabled = true;

        if (!initalClick)
        {
            initalClick = true;
            characterAnimator.SetBool("isAiming", true);
        }

        // delay Miliseconds, There must be a better way to implement this
        await Task.Delay(delay);
        beatDetectionCollider.enabled = false;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {   
        GameObject hitObject = collision.gameObject;
        if (hitObject.CompareTag("Beat"))
        {
            BeatObjectManager manager = hitObject.GetComponent<BeatObjectManager>();

            manager.HitObject();
            scoreManager.AddScore(manager.GetScoreToAdd());
        }
    }

    
}
