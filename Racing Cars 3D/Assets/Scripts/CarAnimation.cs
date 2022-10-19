using System;
using UnityEngine;

namespace DefaultNamespace
{
    [RequireComponent(typeof(Animator))]
    public class CarAnimation : MonoBehaviour
    {
        private Animator _animator;
        private string _startLoad = "StartLoad";
        private string _dropLoad = "DropLoad";

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void StartLoad()
        {
            _animator.SetTrigger(_startLoad);
        }

        public void Drop()
        {
            _animator.SetTrigger(_dropLoad);
        }
    }
}