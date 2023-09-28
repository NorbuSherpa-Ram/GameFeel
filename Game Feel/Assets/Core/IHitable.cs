
public interface IHitable
{
    void OnHit();
}
public interface IDamageable:IHitable
{
    void TakeDamage(int _damageAmount, int _knockDirection);
}