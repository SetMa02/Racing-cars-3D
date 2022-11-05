using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace DefaultNamespace.Bots
{
    [RequireComponent(typeof(Rigidbody), typeof(CarAnimation))]
    public class BotMovement : MonoBehaviour
    {
        [SerializeField] private float _maxSpeed = 5;
        [SerializeField] private float _rotateTime = 2;
        private Rigidbody _rigidbody;
        private IEnumerator _rotateToPointCourutine;
        private BotPoint _targetPoint;
        private bool _isAim = false;
        private CarAnimation _carAnimation;
        private GroundDetection _groundDetection;

        public event UnityAction ReadyToShoot;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _rotateToPointCourutine = RotateToPoint();
            _carAnimation = GetComponent<CarAnimation>();
        }

        private void FixedUpdate()
        {
            if (_rigidbody.velocity == new Vector3(0,0,0) && _isAim == false)
            {
                ReadyToShoot?.Invoke();
            }
        }

        public void StartAim(BotPoint targetPoint)
        {
            StopCoroutine(RotateToPoint());
            _targetPoint = targetPoint;
            StartCoroutine(RotateToPoint());
        }

        private void Drop()
        {
            _rigidbody.AddForce(transform.forward * GetRandom(_maxSpeed), ForceMode.Impulse);
            _isAim = false;
        }
        
        private float GetRandom(float maxValue)
        {
            return Random.Range(0, maxValue);
        }

        private IEnumerator RotateToPoint()
        {
            _isAim = true;
            Vector3 targetPoint = _targetPoint.transform.position;
            _carAnimation.StartLoad();
            Transform targetTransform = transform;
            targetTransform.LookAt(targetPoint);
            Quaternion newRotation = new Quaternion(transform.rotation.x, targetTransform.rotation.y, transform.rotation.z,
                transform.rotation.w);
            targetTransform.rotation = newRotation;

            float rotateTime = _rotateTime;
            
            while (rotateTime >= 0)
            {
                transform.position = Vector3.Lerp(transform.position, targetTransform.position, _rotateTime * Time.deltaTime);
                rotateTime -= Time.deltaTime;
                yield return null;
            }
            _carAnimation.Drop();
            Drop();
            yield return null;
        }
    }
}