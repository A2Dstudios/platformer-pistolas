using Unity.VisualScripting;
using UnityEngine;

public class EnemyActivatorTrigger : MonoBehaviour
{
    // el script del enemigo empieza activado hasta que no te acerques lo suficiente
    [SerializeField] private MobBase _enemyScript;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _enemyScript.enabled = true;
            gameObject.SetActive(false);
        }
    }
}
