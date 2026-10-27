
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float _maxHP = 5f;

    private float _hP;
    private PlayerShield _shield;

    private void Awake()
    {
        _hP = _maxHP;
        _shield = GetComponent<PlayerShield>();
    }

    public void TakeDamage(float quantity)
    {
        if (quantity <= 0f)
        {
            return;
        }

        if (_shield != null && _shield.HasShield())
        {
            return;
        }

        _hP -= quantity;

        Debug.Log($"Vida del jugador: {_hP}/{_maxHP}");

        if (_hP <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }
}
