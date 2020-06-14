﻿using UnityEngine;

public class PowerupController :MonoBehaviour, IEndGameObserver
{
    #region Field Declarations

    public GameObject explosion;

    [SerializeField]
    private PowerType powerType;

    #endregion

    #region Movement

    void Update()
    {
       Move();
    }

    private void Move()
    {
        transform.Translate(Vector2.down * Time.deltaTime * 3, Space.World);

        if (ScreenBounds.OutOfBounds(transform.position))
        {
            RemoveAndDestroy();
        }
    }

    #endregion

    #region Collisons

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //TODO: Apply Power ups

        if (powerType == PowerType.Shield)
        {
            PlayerController playerShip = collision.gameObject.GetComponent<PlayerController>();

            if (playerShip != null)
            {
                playerShip.EnableShield();
            }
        }

        RemoveAndDestroy();
    }

    public void Notify()
    {
        Destroy(gameObject);
    }

    private void RemoveAndDestroy()
    {
        GameSceneController gameSceneController = FindObjectOfType<GameSceneController>();
        gameSceneController.RemoveObserver(this);

        Destroy(gameObject);
    }

    #endregion
}

public enum PowerType
{
    Shield,
    X2
};