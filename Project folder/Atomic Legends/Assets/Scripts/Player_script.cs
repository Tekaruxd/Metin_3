using UnityEngine;
using UnityEngine.InputSystem;

public class Player_script : MonoBehaviour
{
    [Header("Controller")]
    public CharacterController controller;

    [Header("Camera")]
    public Transform cam;

    [Header("Player")]
    public string player_name;
    public float speed;
    public float jump_speed;
    public float gravity;
    public float turn_smooth_time;
    public float turn_smooth_velocity;
    public float max_hp;
    public float hp_per_sec;
    public float hp;
    public float max_mana;
    public float mana_per_sec;
    public float mana;
    public float player_dmg;
    public float attack_speed;
    public float exp;
    public float max_exp;
    public int level;

    [Header("Animator")]
    public Animator Sword_Animator;

    [Header("Input")]
    public InputAction player_controls;

    [Header("Hud")]
    [SerializeField] private Hud_script hud_script;
    [SerializeField] private Sound_script sound_script;

    private Vector3 velocity;

    private void OnEnable()
    {
        player_controls.Enable();
    }
    private void OnDisable()
    {
        player_controls.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        Jump();
        MovePlayer();
        Attack();
        while(exp > max_exp){
            Level_up();
        }

        // Gravitace
        velocity.y -= gravity * Time.deltaTime;

        // Použití řízeného pohybu na kontroleru postavy
        controller.Move(velocity * Time.deltaTime);
    }

    private void MovePlayer()
    {
        Vector3 direction = player_controls.ReadValue<Vector3>();

        if (direction.magnitude >= 0.1f)
        {
            sound_script.Player_walk();
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turn_smooth_velocity, turn_smooth_time);
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
    }

    private void Jump()
    {

        if (Input.GetKeyDown(KeyCode.Space) && controller.isGrounded)
        {
            velocity.y = jump_speed;
        }
    }

    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Sword_Animator.SetTrigger("Swing");
            sound_script.Player_attacks();
            Debug.Log("whooosh");
        }

    }
    public void Get_damage(float dmg)
    {
        hp -= dmg;
        if (hp <= 0)
        {
            hp = 0;
            Debug.Log("Player dead");
        }
        hud_script.Update_health(hp, max_hp, hp_per_sec);
        Debug.Log(hp);
    }
    public void Enemy_killed(int amount) // update exp
    {
        exp += amount;
        hud_script.Update_experience(exp, max_exp);
        Debug.Log(exp);
        
    }
    public void Level_up()
    {
        level += 1;
        max_hp += 10;
        hp = max_hp;
        max_mana += 5;
        mana = max_mana;
        player_dmg += 2;
        attack_speed += 0.1f;
        exp -= max_exp;
        max_exp += 10;
    }

    private void OnTriggerEnter(Collider other)
    {

    }
    
}

