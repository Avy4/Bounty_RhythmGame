using UnityEngine;

public class BeatParticleEmit : MonoBehaviour
{
    [SerializeField] Sprite[] textures;
    private ParticleSystem particleSystemComponent;

    void Start()
    {
        particleSystemComponent = GetComponent<ParticleSystem>();
    }

    public void ChangeAndEmitParticle(int idx)
    {
        GetComponent<ParticleSystem>().textureSheetAnimation.SetSprite(0, textures[idx]);
        GetComponent<ParticleSystem>().Emit(1);
    }
}
