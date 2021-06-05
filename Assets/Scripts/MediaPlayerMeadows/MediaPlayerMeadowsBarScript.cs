using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MediaPlayerMeadowsBarScript : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    public AudioSource audioSource;

    /// <summary>
    /// Positiions; [1] is X pos and [2] is sprite renderer width
    /// </summary>
    private (float, float) startPos = (-13.833f, 3.467932f);
    private (float, float) endPos   = (27.326f, 30.90716f);

    /// <summary>
    /// Position of bar, from 0 to 1
    /// </summary>
    private float _pos = 0.25f;
    public float Pos { get => _pos; set
        {
            _pos = value;
            if (_pos < 0)
                _pos += 1;
            else if (_pos > 1)
                _pos -= 1;
        }
    }

    private float _posVelocity = Reciprocal(120f);
    private float _posVelocityRange = Reciprocal(2.5f);
    private float _posAcceleration = 0;

    private bool _jumpOffAble = true;

    /// <summary>
    /// Yoshi on the bar
    /// </summary>
    private GameObject yoshi = null;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        ResetPosVelocity();
    }

    /// <summary>
    /// Gets reciprocal and turns to seconds
    /// </summary>
    /// <param name="x">Seconds</param>
    /// <returns>What I just said</returns>
    private static float Reciprocal(float x)
    {
        return 1f / (x * 60f);
    }

    /// <summary>
    /// Resets the position velocity to x1 speed
    /// </summary>
    private void ResetPosVelocity()
    {
        // Sets initial pos velocity range
        _posVelocity = _posVelocityRange * (1f / 2f);
    }

    // Update is called once per frame
    void Update()
    {
        // Sets pitch based on pos
        audioSource.pitch = (_posVelocity / _posVelocityRange) * 2;

        // Displaces pos
        Pos += _posVelocity * Time.deltaTime * 60;
        _posVelocity = Mathf.Clamp(_posVelocity + _posAcceleration * Time.deltaTime * 60, -_posVelocityRange, _posVelocityRange);

        // Move bar depending on pos
        transform.position = new Vector3(
            Mathf.Lerp(startPos.Item1, endPos.Item1, Pos),
            transform.position.y,
            transform.position.z);
        spriteRenderer.size = new Vector2(
            Mathf.Lerp(startPos.Item2, endPos.Item2, Pos),
            spriteRenderer.size.y);

        // If yoshi is on the bar
        if (yoshi != null)
        {
            // Set Yoshi to position
            yoshi.transform.position = new Vector3(
                Mathf.Lerp(startPos.Item1, endPos.Item1, Pos) + spriteRenderer.bounds.size.x / 2,
                transform.position.y,
                yoshi.transform.position.z);

            // If we go left
            if (Input.GetAxis("Horizontal") < 0)
            {
                _posAcceleration = -Reciprocal(80);
            }

            // If we go right
            if (Input.GetAxis("Horizontal") > 0)
            {
                _posAcceleration = Reciprocal(80);
            }

            // If we're not pressing anything
            if (Input.GetAxis("Horizontal") == 0)
            {
                _posAcceleration = 0;
            }

            // If jump AND we can jump off, let yoshi go
            if (Input.GetAxis("Vertical") != 0 && _jumpOffAble)
            {
                // Put Yoshi down
                yoshi.GetComponent<PlayerMovement>().Enabled = true;
                yoshi = null;

                // Reset velocity and acceleration
                ResetPosVelocity();
                _posAcceleration = 0;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If yoshi touches bar
        if (collision.GetComponent<Yoshi>() != null)
        {
            // Store yoshi and lock him
            yoshi = collision.gameObject;
            yoshi.GetComponent<PlayerMovement>().SetDirection(Assets.Classes.Direction.RIGHT);
            yoshi.GetComponent<PlayerMovement>().Enabled = false;

            // Stops the yoshi from moving
            yoshi.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

            // Set bar to not jump-offable but set coroutine that will set it
            _jumpOffAble = false;
            StartCoroutine("SetJumpOffAble");
        }
    }

    private IEnumerator SetJumpOffAble()
    {
        yield return new WaitForSeconds(0.5f);
        _jumpOffAble = true;
    }
}
