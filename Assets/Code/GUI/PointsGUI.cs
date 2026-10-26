using TMPro;
using UnityEngine;

public class PointsGUI : MonoBehaviour
{

    private int _score = 0;
    private TMP_Text _tmp;

    private void OnEnable()
    {
        _tmp = GetComponent<TMP_Text>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            MobBase enemyComp = enemy.GetComponent<MobBase>();
            if (enemyComp != null)
            {
                enemyComp.SendToScore += addPoints;
            }
        }
    }

    void addPoints(int added)
    {
        _score += added;
        _tmp.text = "Puntos: "+_score.ToString("D4");
    }
}
