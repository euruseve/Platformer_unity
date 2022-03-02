using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private float totalHeath = 100f;
    [SerializeField] private Slider slider;
    private float _health;

    private void Start() {
        _health = totalHeath;
        InitHealth();
    }

    public void ReduceHealth(float damage)
    {
        _health -= damage;
        InitHealth();
        animator.SetTrigger("takeDamage");
        if(_health <= 0)
        {
            Death();
        }

    }

    private void Death()
    {
        gameObject.SetActive(false);
    }

    private void InitHealth()
    {
        slider.value = _health / totalHeath;
    }
}
