using System;
using UnityEngine;

public class BeatObjectManager : MonoBehaviour
{   
    // Variables to help with finding objects / that are objects
    const String TAGOUTER = "Outer", TAGMIDDLE = "Middle", TAGINNER = "Inner";
    const String PARTICLEEMITTERNAME = "Particle System";
    private ParticleEmitter particlerEmitter;

    // BeatObject Settings
    private float speed;
    private LineRenderer line;

    // Movement Related Variables
    private Vector3[] lerpPoints;
    private Vector3 startingPos, nextPoint;
    private int currentPointIdx;

    // OnHit Related Variables
    private int scoreToAdd = 0;
    private bool gotScore = false;
    private bool hit = false;
    private int emitterIdx;
    
    // Function that initializes speed and line from the BeatObject's individual values
    public void Initialize(LineRenderer ln, float spd)
    {
        line = ln;
        speed = spd;
    }
    void Start()
    {   
        // Object Innit
        particlerEmitter = GameObject.Find(PARTICLEEMITTERNAME).GetComponent<ParticleEmitter>();    

        // Need to create an array large enough to hold all the positions, fills that array with points
        lerpPoints = new Vector3[line.positionCount];
        line.GetPositions(lerpPoints);

        // Set where in global space the actual line is. We use this to augment the position of the points.
        startingPos = line.transform.position;

        // Init idx
        currentPointIdx = lerpPoints.Length - 1;

        // Set starting pos, sub idx by 1, set next pos, sub idx by 1
        transform.position = lerpPoints[currentPointIdx--] + startingPos;
        nextPoint = lerpPoints[currentPointIdx--] + startingPos;
    }

    void Update()
    {   
        // Update the position everyframe, base speed is 3
        transform.position = Vector3.MoveTowards(transform.position, nextPoint, speed * Time.deltaTime);

        // If its close enough, then we can start going to the next point
        if (Vector3.Distance(transform.position, nextPoint) < .2)
        {   
            // Out of bounds check
            if (currentPointIdx >= 0)
            {
                nextPoint = lerpPoints[currentPointIdx--] + startingPos;
            }
            else
            {
                gameObject.SetActive(false);
                if (!hit)
                {
                    // ScoreManager.AddScore(scoreToAdd);
                    particlerEmitter.ChangeAndEmitParticle(emitterIdx);
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {   
        var currentLayerTag = collision.gameObject.tag;
        if (!gotScore) {
            if (currentLayerTag == TAGINNER)
            {
                scoreToAdd = 0;
                emitterIdx = 0;
            }
            else if (currentLayerTag == TAGMIDDLE)
            {
                scoreToAdd = 300;
                emitterIdx = 1;
            }
            else if (currentLayerTag == TAGOUTER)
            {
                scoreToAdd = 100;
                emitterIdx = 2;
            }
        }   
    }

    public void HitObject()
    {
        gotScore = true;
        particlerEmitter.ChangeAndEmitParticle(emitterIdx);
        gameObject.SetActive(false);
        hit = true;
    }

    public int GetScoreToAdd()
    {
        return scoreToAdd;
    }
}
