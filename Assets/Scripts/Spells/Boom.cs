using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Boom : MonoBehaviour {
    public float velocity;
    public float timeToLive;
    public int damage;
    Rigidbody rb;
    public Transform particles;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * velocity;
        Destroy(gameObject, timeToLive);
    }

    // Update is called once per frame
    void Update () {
        
    }

    private void OnTriggerEnter(Collider other) {
        EnemyStats stats = other.GetComponent<EnemyStats>();
        if (stats != null) {
            stats.TakeDamage(damage, DamageType.Fire);
            StopParticles();
            Destroy(gameObject);
        }
    }

    private void StopParticles() {
        ParticleSystem ps = particles.GetComponent<ParticleSystem>();
        ps.Stop();
        particles.parent = null;
        var mainModule = ps.main;
        Destroy(particles.gameObject, mainModule.startLifetimeMultiplier);
    }


}
