using System;
using UnityEngine;

public class Health
{
    public event Action<int> HealthChanged;
    public event Action Damaged;
    public event Action Healed;
    public event Action Died;

    public bool Alive { get; private set; }
    public int Hp { get; private set; }
    public int MaxHp { get; private set; }

    public Health(HealthSaveData data)
    {
        MaxHp = data.MaxHp> 0 ? data.MaxHp : throw new ArgumentException("MaxHp must be positive");
        Hp = Mathf.Clamp(data.Hp, 0, MaxHp);
        
        Alive = Hp > 0;
    }

    public Health(int hp, int maxHp)
    {
        MaxHp = maxHp > 0 ? maxHp : throw new ArgumentException("MaxHp must be positive");
        this.Hp = Mathf.Clamp(hp, 0, MaxHp);
        Alive = this.Hp > 0;
    }

    public void ChangeHealth(int amount)
    {
        int newHealth = Hp + amount;
        newHealth = Mathf.Clamp(newHealth, 0, MaxHp);

        bool isHealthChanged = Hp != newHealth;
        if (!isHealthChanged) return;


        if (newHealth == 0)
        {
            Alive = false;
            Died?.Invoke();
        }
        else if (newHealth < Hp) Damaged?.Invoke();
        if (newHealth > Hp) Healed?.Invoke();

        Hp = newHealth;
        HealthChanged?.Invoke(Hp);
    }

    public HealthSaveData ExtractData() => new HealthSaveData(Hp, MaxHp);
}
