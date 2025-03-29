using UnityEngine;

public class Sound_script : MonoBehaviour
{
    public AudioSource bg_music;
    public AudioSource walking_sound;
    public AudioSource player_attack;
    public AudioSource player_attack_swing;
    public AudioSource player_attack_hit;
    public AudioSource player_hit;
    public AudioSource enemy_hit;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bg_music.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Player_attacks()
    {
        player_attack.Play();
        player_attack_swing.Play();
        player_attack_hit.Play();        
    }
    public void Player_walk()
    {
        walking_sound.Play();
    }
    public void Player_hit()
    {
        player_hit.Play();
    }
    public void Enemy_hit()
    {
        enemy_hit.Play();
    }
}
