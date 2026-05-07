using System;
using UnityEngine;

public class Beat : MonoBehaviour
{   
    const String TAGOuter = "Outer", TAGMiddle = "Middle", TAGInner = "Inner";
    const String particleEmitterName = "Particle System";
    private float speed;
    private LineRenderer line;
    private Vector3[] lerpPoints;
    private Vector3 startingPos, nextPoint;
    private int idx;
    private int scoreToAdd = 0;
    private bool gotScore = false;
    private ParticleEmitter particlerEmitter;
    private int emitterIdx;
    private bool hit = false;
    void Start()
    {   
        // CHANGE IN FINAL IMPLEMENTATION
        startingPos = line.transform.position;

        // Need to create an array large enough to hold all the positions
        lerpPoints = new Vector3[line.positionCount];

        // Gets the positions of each point of the line
        line.GetPositions(lerpPoints);

        // Init idx
        idx = lerpPoints.Length - 1;

        // Set starting pos, sub idx by 1, set next pos, sub idx by 1
        transform.position = lerpPoints[idx--] + startingPos;
        nextPoint = lerpPoints[idx--] + startingPos;
        particlerEmitter = GameObject.Find(particleEmitterName).GetComponent<ParticleEmitter>();
    }

    void Update()
    {   
        // Update the position everyframe, base speed is 3
        transform.position = Vector3.MoveTowards(transform.position, nextPoint, speed * Time.deltaTime);

        // If its close enough, then we can start going to the next point
        if (Vector3.Distance(transform.position, nextPoint) < .2)
        {   
            // Out of bounds check
            if (idx >= 0)
            {
                nextPoint = lerpPoints[idx--] + startingPos;
            }
            else
            {
                gameObject.SetActive(false);
                if (!hit)
                {
                    ScoreManager.AddScore(scoreToAdd);
                    particlerEmitter.ChangeAndEmitParticle(emitterIdx);
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {   
        var currentLayerTag = collision.gameObject.tag;
        if (!gotScore) {
            if (currentLayerTag == TAGInner)
            {
                scoreToAdd = 0;
                emitterIdx = 0;
            }
            else if (currentLayerTag == TAGMiddle)
            {
                scoreToAdd = 300;
                emitterIdx = 1;
            }
            else if (currentLayerTag == TAGOuter)
            {
                scoreToAdd = 100;
                emitterIdx = 2;
            }
        }   
    }

    public void Initialize(LineRenderer ln, float spd)
    {
        line = ln;
        speed = spd;
    }
    public void HitObject()
    {
        gotScore = true;
        ScoreManager.AddScore(scoreToAdd);
        particlerEmitter.ChangeAndEmitParticle(emitterIdx);
        gameObject.SetActive(false);
        hit = true;
    }
}
