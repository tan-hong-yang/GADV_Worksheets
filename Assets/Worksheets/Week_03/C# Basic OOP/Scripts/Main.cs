using UnityEngine;


public class Main : MonoBehaviour
{    void Start()
    {
        Player player = new Player(10);
        player.TakeDamage(3);
        Debug.Log("Player health: " + player.GetHealth());
    }
}
public class Projectile
{
    private float speed;

    public Projectile(float initialSpeed)
    {
        speed = initialSpeed;
    }

    public void Fire()
    {
        if (speed > 0)
        {
            Debug.Log("Firing projectile at speed " + speed);
        }
        else
        {
            Debug.Log("Cannot fire: speed too low.");
            AutoFire();
        }
    }

    // AutoFire method
    private void AutoFire()
    {
        speed = 100;
        Debug.Log("Speed was zero. AutoFire set speed to 100 and launched!");
    }
}

public class Player
{
    private int health;

    // Constructor to set starting health
    public Player(int startingHealth)
    {
        // Clamp health to a maximum of 10
        health = Mathf.Clamp(startingHealth, 0, 10);
    }

    // Method to reduce health
    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health < 0)
        {
            health = 0;
        }
    }

    // Method to get current health
    public int GetHealth()
    {
        return health;
    }
}
public class TreasureChest
{
    // Original virtual method (can be overridden by subclasses)
    public virtual void Unlock()
    {
        Debug.Log("You try to unlock the chest.");
    }

    // Overloaded method with different parameters
    public void Unlock(bool hasToken)
    {
        if (hasToken)
        {
            Debug.Log("You unlock the chest with a key and a special token. Bonus treasure inside!");
        }
        else
        {
            Unlock(); // Call the base unlock behavior
        }
    }

    // Non-virtual method for shaking the chest
    public void Shake()
    {
        Debug.Log("You hear something rattle inside the chest.");
    }
}


