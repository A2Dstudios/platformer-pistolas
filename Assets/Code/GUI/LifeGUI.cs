using TMPro;
using TMPro.EditorUtilities;
using UnityEngine;

public class LifeGUI : MonoBehaviour
{
    [SerializeField]
    private PlayerHealth _player;

    private TMP_Text _tmp;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        _player.OnHealthChanged += changeText;
        _tmp = gameObject.GetComponent<TMP_Text>();
    }

    private void OnDisable()
    {
        _player.OnHealthChanged -= changeText;
    }

    void changeText(float curHp, float maxHp)
    {
        _tmp.text = "";
        for (int i = 1; i <= maxHp; i++)
        {
            _tmp.text += "♥";
            if (i == curHp)
            {
                _tmp.text += "<color=black>";
            }
        }
    }
}
