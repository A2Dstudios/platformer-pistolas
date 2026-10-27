using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float _maxHP = 5f;

    public event Action<float, float> OnHealthChanged;

    private float _hP;
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
        Debug.Log("Daño recibido: " + quantity + " | vida antes: " + _hP);
        if (_shield != null && _shield.HasShield()) return;

        _hP -= quantity;
        OnHealthChanged?.Invoke(_hP, _maxHP);

        if (_hP <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}