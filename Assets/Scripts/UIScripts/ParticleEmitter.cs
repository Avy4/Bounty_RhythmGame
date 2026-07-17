using UnityEngine;

public class ParticleEmitter : MonoBehaviour
{
    [SerializeField] Sprite[] textures;
    private ParticleSystem particleSystemComponent;

    void Start()
    {
        particleSystemComponent = GetComponent<ParticleSystem>();
    }

    public void ChangeAndEmitParticle(int idx)
    {
        particleSystemComponent.textureSheetAnimation.SetSprite(0, textures[idx]);
        particleSystemComponent.Emit(1);
    }
}
