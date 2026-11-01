using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float _maxHP = 5f;

    public event Action<float, float> OnHealthChanged;

    private float _hP;
    private bool _isDead;
    private bool _isInvulnerable;
    private PlayerShield _shield;

    private void Awake()
    {
        _hP = _maxHP;
        _shield = GetComponentInChildren<PlayerShield>();
    }

    private void Start()
    {
        OnHealthChanged?.Invoke(_hP, _maxHP);
    }

    public void TakeDamage(float quantity)
    {
        if (_isInvulnerable || _isDead) return;
        if (_shield != null && _shield.HasShield()) return;

        _hP -= quantity;
        OnHealthChanged?.Invoke(_hP, _maxHP);

        if (_hP <= 0)
        {
            _isDead = true;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            return;
        }

        StartCoroutine(Invulnerable());
    }

    private IEnumerator Invulnerable()
    {
        _isInvulnerable = true;
        yield return new WaitForSeconds(1f);
        _isInvulnerable = false;
    }
}