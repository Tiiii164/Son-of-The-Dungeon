using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStrikingDisctanceCheck : MonoBehaviour
{
    public GameObject PlayerTarger { get; set; }
    private Enemy _enemy;
    private void Awake()
    {
        PlayerTarger = GameObject.FindGameObjectWithTag("Player");
        _enemy = GetComponentInParent<Enemy>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == PlayerTarger)
        {
            _enemy.SetWithinStrikingDistance(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == PlayerTarger)
        {
            _enemy.SetWithinStrikingDistance(false);
        }
    }
}
