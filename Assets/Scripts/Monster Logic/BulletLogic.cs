using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BulletLogic : MonoBehaviour
{
    [SerializeField] private float MoveSpeed = 5f;
    [SerializeField] private GameObject LockedTarget;
    private bool Locked = false;
    [SerializeField] private Rigidbody2D BulletRB;
    private int target;
    private GameObject[] Targets;

    private Vector2 direction;
    void Start()
    {
        target = Random.Range(1, 3);
        Targets = GameObject.FindGameObjectsWithTag("Player");
        if (!Locked)
        {
            if (target == 1)
            {
                if (Targets.Length == 0)
                {
                    Destroy(this.gameObject);
                }
                else
                {
                    LockedTarget = Targets[0];
                }
            }
            else if (target == 2)
            {
                if (Targets.Length < 2)
                {
                    Destroy(this.gameObject);
                }
                else
                {
                    LockedTarget = Targets[1];
                }
            }
            Locked = true;
        }
        if (LockedTarget)
        {
            direction = (LockedTarget.transform.position - transform.position).normalized;
        }
        else
        {
            Destroy (this.gameObject);
        }
    }
    void Update()
    {

        if (LockedTarget != null)
        {
            BulletRB.velocity = direction * MoveSpeed;

            Destroy(this.gameObject, 3.4f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerHealth TargetHealth = collision.GetComponent<PlayerHealth>();
            if (TargetHealth != null) 
            {
                TargetHealth.TakeDamage(1);
            }
            Destroy(this.gameObject); 
        }
    }

    private void OnDestroy()
    {
        // Optional: Cleanup or effects before the bullet is destroyed
    }
}
