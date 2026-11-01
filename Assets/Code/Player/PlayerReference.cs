using UnityEngine;

public class PlayerReference : MonoBehaviour
{
    // guarda referencia estatica al player para que cada enemigo lo pueda asignar sin hacer un findobjects o algo asi que es mas caro
    public static Transform Player { get; private set; }

    private void Awake()
    {
        Player = transform;
    }

    private void OnDestroy()
    {
        if (Player == transform)
        {
            Player = null;
        }
    }
}
