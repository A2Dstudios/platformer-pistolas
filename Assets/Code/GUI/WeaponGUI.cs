using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class WeaponGUI : MonoBehaviour
{

    [SerializeField]
    private PlayerShooting _player;

    private Image _sr;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        _player.OnNewWeapon += changeSprite;
        _sr = gameObject.GetComponent<Image>();
        _sr.preserveAspect = true;
    }

    private void OnDisable()
    {
        _player.OnNewWeapon -= changeSprite;
    }

    void changeSprite(Sprite sprite)
    {
        _sr.sprite = sprite;
    }
}
