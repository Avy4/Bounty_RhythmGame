using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerHit : MonoBehaviour
{
    [SerializeField] int delay = 20;
    private CapsuleCollider2D beatDetectionCollider;
    void Start()
    {
        beatDetectionCollider = GetComponent<CapsuleCollider2D>();
        beatDetectionCollider.enabled = false;
    }

    async Task OnAttack()
    {   
        beatDetectionCollider.enabled = true;
        // delay Miliseconds, There must be a better way to implement this
        await Task.Delay(delay);
        beatDetectionCollider.enabled = false;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {   
        GameObject hitObject = collision.gameObject;
        if (hitObject.CompareTag("Beat"))
        {
            Debug.Log("Beat has been hit");
            hitObject.GetComponent<Beat>().HitObject();
            Debug.Log(ScoreManager.GetCombo());
            Debug.Log(ScoreManager.GetScore());
        }
    }
}
