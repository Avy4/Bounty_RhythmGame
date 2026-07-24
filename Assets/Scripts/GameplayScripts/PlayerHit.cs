using System.Threading.Tasks;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    [SerializeField] int delay = 5;
    [SerializeField] Animator characterAnimator;
    [SerializeField] ScoreManager scoreManager;
    [SerializeField] AudioSource hitSoundPlayer;

    private bool initalClick = false;
    private GameObject currentOccupant = null;
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
            if (!currentOccupant)
            {   
                currentOccupant = hitObject;

                BeatObjectManager manager = hitObject.GetComponent<BeatObjectManager>();
                manager.HitObject();
                // hitSoundPlayer.Play();
                scoreManager.AddScore(manager.GetScoreToAdd());
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        currentOccupant = null;
    }

    
}
