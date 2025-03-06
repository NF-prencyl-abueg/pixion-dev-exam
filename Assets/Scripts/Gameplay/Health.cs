
public class Health : MonoExt, IDamageable
{
    public float HP = 100;
    public void ApplyDamage(float damageValue)
    {
        HP -= damageValue;

        if (HP <= 0)
            Destroy(gameObject, 0.2f);
    }
}