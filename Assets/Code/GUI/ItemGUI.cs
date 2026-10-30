using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class ItemGUI : MonoBehaviour
{

    [SerializeField]
    private PlayerShooting _player;

    private Image _sr;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        _player.OnNewItem += changeSprite;
        _sr = gameObject.GetComponent<Image>();
        _sr.preserveAspect = true;
    }

    private void OnDisable()
    {
        _player.OnNewWeapon -= changeSprite;
    }

    void changeSprite(Sprite sprite)
    {
        if (sprite == null)
        {
            _sr.color = new Color(1, 1, 1, 0);
        } else
        {

            _sr.color = new Color(1, 1, 1, 1);
        }
        _sr.sprite = sprite;
    }
}
