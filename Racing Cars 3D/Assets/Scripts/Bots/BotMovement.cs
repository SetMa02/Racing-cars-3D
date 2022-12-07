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

        
        public bool IsAim => _isAim;
        public event UnityAction ReadyToShoot;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _carAnimation = GetComponent<CarAnimation>();
        }

        private void FixedUpdate()
        {
            if (_rigidbody.velocity == new Vector3(0,0,0) && _isAim == false)
            {
                ReadyToShoot?.Invoke();
            }

            if (_isAim == true)
            {
                Vector3 targetPoint = _targetPoint.transform.position;
                Quaternion newRotation = transform.rotation;
                newRotation.SetLookRotation(targetPoint);
                transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, _rotateTime * Time.deltaTime);
            }
        }

        public void StartAim(BotPoint targetPoint)
        {
            StopCoroutine(RotateToPoint());
            _targetPoint = targetPoint;
            _isAim = true;
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
            _carAnimation.StartLoad();
            yield return new WaitForSeconds(_rotateTime);
            _carAnimation.Drop();
            Drop();
            yield return null;
        }
    }
}