using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public static Player Instance;
    [SerializeField] private Transform[] weaponContainers;
    [SerializeField] private float speed;
    [SerializeField] private int strength;
    
    public int Strength => strength;

    public Vector3 moveVec { get; private set; }

    private int level = 1;
    private int exp = 0;
    private int expMax = 100;

    private void Start()
    {
        Instance = this;
    }
    private void Update()
    {
        transform.position += moveVec;
    }
    public void GainExp(int value)
    {
        exp += value;

        if (exp > expMax)
        {
            level++;
            exp -= expMax;
            expMax = (int)(expMax * 1.1f);
        }
    }
    private void OnMove(InputValue value)
    {
        Vector2 v = value.Get<Vector2>();

        if (v != null)
        {
            moveVec = new Vector3(v.x, v.y, 0f) * speed;

            float angle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;

            weaponContainers[1].rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}
