public interface IDamageable
{
    public bool IsEnabled { get; }

    public void TakeDamage(int damage);
}