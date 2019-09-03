using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathFX;
    [SerializeField] Transform parent;
    [SerializeField] int scorePerHit = 12;
    [SerializeField] int hits = 3;
    [SerializeField] GameObject hitFX;
    ScoreBoard scoreBoard;

    // Start is called before the first frame update
    void Start()
    {
        AddBoxCollider();
        scoreBoard = FindObjectOfType<ScoreBoard>();
    }

    private void AddBoxCollider()
    {
        Collider enemyBoxCollider = gameObject.AddComponent<BoxCollider>();
        enemyBoxCollider.isTrigger = false;
    }

    private void OnParticleCollision(GameObject other)
    {
        print("Enemy hit");
        CreateHitEffect();
        scoreBoard.ScoreHit(scorePerHit);
        hits--;
        if (hits < 1)
        {
            KillEnemy();
        }
        
    }

    private void CreateHitEffect()
    {
        GameObject fx = Instantiate(hitFX, transform.position, Quaternion.identity);
        fx.transform.parent = parent;
    }

    private void KillEnemy()
    {
        GameObject fx = Instantiate(deathFX, transform.position, Quaternion.identity);
        // set the parent of the transform to the parent provided
        fx.transform.parent = parent;
        Destroy(this.gameObject);
    }
}
