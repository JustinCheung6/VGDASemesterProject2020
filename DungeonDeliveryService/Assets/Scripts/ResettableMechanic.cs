using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResettableMechanic : MonoBehaviour
{
    protected delegate void ResetDelegate();
    protected static ResetDelegate resetDelegate;

    protected Vector2 initialPos = new Vector2();

    [SerializeField] protected bool roomResetable = true;

    private void OnEnable()
    {
        if (roomResetable)
        {
            initialPos = new Vector2(transform.position.x, transform.position.y);
            resetDelegate += MechanicReset;
        }
    }
    private void OnDisable()
    {
        if (roomResetable)
            resetDelegate -= MechanicReset;
    }

    protected virtual void MechanicReset()
    {
        transform.position = new Vector3(initialPos.x, initialPos.y, 0);
    }

    public static void ResetAll()
    {
        if (resetDelegate != null)
            resetDelegate();
    }
}
