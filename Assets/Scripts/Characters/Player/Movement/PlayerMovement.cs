using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player.Interfaces;
namespace Player.Movement
{
    public class PlayerMovement : MonoBehaviour,IPlayerMovement
    {
        [SerializeField] private float moveSpeed;
        [SerializeField] private float dashBoost;
        [SerializeField] private float dashTime;
        [SerializeField] private float dashCooldownTime;
        [SerializeField] private GameObject _ghostEffect;
        [SerializeField] float ghostDelayTime;
        [SerializeField] AudioClip dashSound;
        private Coroutine dashEffectCoroutine;

        private float currentDashTime;
        private float currentDashCooldownTime;
        private bool dashOnce;


        private Vector2 _movement;
        private Rigidbody2D _rigidbody;
        private Animator _animator;

        private const string _horizontal = "Horizontal";
        private const string _vertical = "Vertical";
        private const string _lastHorizontal = "LastHorizontal";
        private const string _lastVertical = "LastVertical";


        private void Awake()
        {
            _animator = GetComponentInChildren<Animator>();
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        public void Moving()
        {
            //move
            _movement.Set(InputManager.Movement.x, InputManager.Movement.y);
            _rigidbody.velocity = _movement * moveSpeed;
            //set moving animation 
            _animator.SetFloat(_horizontal, _movement.x);
            _animator.SetFloat(_vertical, _movement.y);
            //stop moving animation, keep the direction
            if (_movement != Vector2.zero)
            {
                _animator.SetFloat(_lastHorizontal, _movement.x);
                _animator.SetFloat(_lastVertical, _movement.y);
            }
        }
        public void Dash()
        {
            // Kiểm tra nếu người chơi nhấn phím Dash và cả thời gian dash lẫn thời gian hồi chiêu đều bằng hoặc dưới 0
            if (InputManager.Dash && currentDashTime <= 0 && currentDashCooldownTime <= 0)
            {
                moveSpeed += dashBoost;
                currentDashTime = dashTime;
                dashOnce = true;
                StartDashEffect();
                // Đặt lại thời gian hồi chiêu
                currentDashCooldownTime = dashCooldownTime;
                SoundFXManager.Instance.PlaySoundFXClip(dashSound, transform, 1f);
            }

            // Xử lý việc kết thúc dash
            if (currentDashTime <= 0 && dashOnce)
            {
                moveSpeed -= dashBoost;
                dashOnce = false;
                StopDashEffect();
            }
            else if (dashOnce)
            {
                currentDashTime -= Time.deltaTime;
            }

            // Giảm dần thời gian hồi chiêu
            if (currentDashCooldownTime > 0)
            {
                currentDashCooldownTime -= Time.deltaTime;
            }
        }
        private void StopDashEffect()
        {
            if (dashEffectCoroutine != null) StopCoroutine(dashEffectCoroutine);

        }
        private void StartDashEffect()
        {
            if (dashEffectCoroutine != null) StopCoroutine(dashEffectCoroutine);
            dashEffectCoroutine = StartCoroutine(DashEffectCoroutine());
        }
        IEnumerator DashEffectCoroutine()
        {
            while (true)
            {
                GameObject ghost = Instantiate(_ghostEffect, transform.position, transform.rotation);
                Destroy(ghost, 0.3f);
                yield return new WaitForSeconds(ghostDelayTime);
            }
        }
    }


}

