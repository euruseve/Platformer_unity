using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float totalHealth = 100f;
    [SerializeField] private Animator animator;
    [SerializeField] private Slider slider;
    [SerializeField] private GameObject gameOverCancas;
    [SerializeField] private AudioSource playerHitSound;

    private float _health;
    private void Start() {
        _health = totalHealth;
        InitHealth();
    }


    public void ReduceHealth(float damage)
    {
        _health -= damage;
        InitHealth();
        playerHitSound.Play();
        animator.SetTrigger("takeDamage");
        if(_health <= 0)
        {
            Death();
        }

    }

    private void InitHealth()
    {
        slider.value = _health / totalHealth;
    }
    private void Death()
    {
        gameObject.SetActive(false);
        gameOverCancas.SetActive(true);
    }
}
