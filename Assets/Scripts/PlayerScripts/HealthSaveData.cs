
[System.Serializable]
public class HealthSaveData
{
    public int Hp;
    public int MaxHp;

    public HealthSaveData(int hp, int maxHp)
    {
        Hp = hp;
        MaxHp = maxHp;
    }
}
