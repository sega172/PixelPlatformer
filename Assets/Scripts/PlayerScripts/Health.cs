using System;
using UnityEngine;

[Serializable]
public class Health
{
    
    public event Action<int> HealthChanged;
    public event Action Damaged;
    public event Action Healed;
    public event Action Died;

    public bool Alive { get; private set; }
    public int Hp { get; private set; }
    public int MaxHp { get; private set; }

    public Health(int hp, int maxHp)
    {
        MaxHp = maxHp > 0 ? maxHp : throw new ArgumentException("MaxHp must be positive");
        this.Hp = Mathf.Clamp(hp, 0, MaxHp);
        Alive = this.Hp > 0;
    }
    public Health(HealthSaveData data) : this(data.Hp, data.MaxHp) { }


    public void TakeDamage(int amount)
    {
        if (amount < 0)
            throw new ArgumentException("Damage amount cannot be negative", nameof(amount));
        if (!Alive)
            return;
        ChangeHealth(-amount);
    }
    public void Heal(int amount)
    {
        if (amount < 0)
            throw new ArgumentException("Heal amount cannot be negative", nameof(amount));
        if (!Alive || Hp == MaxHp) return;
        ChangeHealth(amount);
    }



    private void ChangeHealth(int amount)
    {
        var newHealth = Hp + amount;
        newHealth = Mathf.Clamp(newHealth, 0, MaxHp);

        var isHealthChanged = Hp != newHealth;
        if (!isHealthChanged) return;

        Hp = newHealth;
        HealthChanged?.Invoke(Hp);

        if (Hp == 0)
        {
            Alive = false;
            Died?.Invoke();
        }
        else if (amount<0) Damaged?.Invoke();
        else if (amount > 0) Healed?.Invoke();

    }

    public HealthSaveData ExtractData() => new HealthSaveData(Hp, MaxHp);
}
