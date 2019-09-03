using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathFX;
    [SerializeField] Transform parent;

    // Start is called before the first frame update
    void Start()
    {
        Collider enemyBoxCollider = gameObject.AddComponent<BoxCollider>();
        enemyBoxCollider.isTrigger = false;
    }


    private void OnParticleCollision(GameObject other)
    {
        print("Particles collided with enemy " + gameObject.name);
        GameObject fx = Instantiate(deathFX, transform.position, Quaternion.identity);
        // set the parent of the transform to the parent provided
        fx.transform.parent = parent;
        Destroy(this.gameObject);
    }
}
