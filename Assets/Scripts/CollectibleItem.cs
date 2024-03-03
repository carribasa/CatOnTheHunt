using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
	public GameObject lightingParticles;
	public GameObject burstParticles;
	private AudioSource audioSource;

	private SpriteRenderer _renderer;
	private Collider2D _collider;

	private void Awake()
	{
		_renderer = GetComponent<SpriteRenderer>();
		_collider = GetComponent<Collider2D>();
		audioSource = GetComponent<AudioSource>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			_collider.enabled = false;

			audioSource.Play();
			lightingParticles.SetActive(false);
			burstParticles.SetActive(true);
			_renderer.enabled = false;

			Destroy(gameObject, 2f);
		}
	}
}
