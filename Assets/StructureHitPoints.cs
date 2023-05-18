using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureHitPoints : MonoBehaviour
{
    public float StartingHealth = 100f;
    public float armourValue = 10f;
    public float magicResistValue = 10f;

    public float HitPoints
    {
        get { return _HealthPoints; }
        set
        {
            _HealthPoints = Mathf.Clamp(value, 0f, 100f);
        }
    }

    public void TakeDamage(int amount)
    {
        HitPoints -= amount;
        if (_HealthPoints <= 0f)
        {
            Destroy(gameObject);
        }
    }

    private float _HealthPoints = 100f;
    // Start is called before the first frame update
    void Start()
    {
        HitPoints = StartingHealth;
    }

    public float get(){
        return _HealthPoints;
    }
}
