using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Obstacle))]
public class Mine : MonoBehaviour
{
   [SerializeField] private float _radius;
   [SerializeField] private float _force;
   private ParticleSystem _explodeEffect;
   private MeshRenderer _meshRenderer;

   private void Start()
   {
      _explodeEffect = GetComponentInChildren<ParticleSystem>();
      _meshRenderer = GetComponent<MeshRenderer>();
   }
   
   private void OnTriggerEnter(Collider other)
   {
      if (other.gameObject.TryGetComponent<LooseRespawn>(out LooseRespawn looseRespawn))
      {
         Explode();
      }
   }
   
   private void Explode()
   {
      Collider[] overlappedColliders = Physics.OverlapSphere(transform.position, _radius);

      foreach (var collider in overlappedColliders)
      {
         Rigidbody rigidbody = collider.attachedRigidbody;

         if (rigidbody == true && !collider.TryGetComponent(out Obstacle obstacle))
         {
            rigidbody.AddExplosionForce(_force, transform.position, _radius);
         }

         if (_explodeEffect.isPlaying == true)
         {
            _explodeEffect.Stop();
         }

         StartCoroutine(HideByParticleEnd());
         _meshRenderer.enabled = false;
         this.enabled = false;
         _explodeEffect.Play();
      }
   }
   
   private IEnumerator HideByParticleEnd()
   {
      yield return new WaitForSeconds(_explodeEffect.startLifetime);
      gameObject.SetActive(false);
   }
}
