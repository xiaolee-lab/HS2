using System;
using UnityEngine;

// Token: 0x020002DC RID: 732
[RequireComponent(typeof(AudioSource))]
public class FlockChildSound : MonoBehaviour
{
	// Token: 0x06000C53 RID: 3155 RVA: 0x0002FDF0 File Offset: 0x0002E1F0
	public void Start()
	{
		this._flockChild = base.GetComponent<FlockChild>();
		this._audio = base.GetComponent<AudioSource>();
		base.InvokeRepeating("PlayRandomSound", UnityEngine.Random.value + 1f, 1f);
		if (this._scareSounds.Length > 0)
		{
			base.InvokeRepeating("ScareSound", 1f, 0.01f);
		}
	}

	// Token: 0x06000C54 RID: 3156 RVA: 0x0002FE54 File Offset: 0x0002E254
	public void PlayRandomSound()
	{
		if (base.gameObject.activeInHierarchy)
		{
			if (!this._audio.isPlaying && this._flightSounds.Length > 0 && this._flightSoundRandomChance > UnityEngine.Random.value && !this._flockChild._landing)
			{
				this._audio.clip = this._flightSounds[UnityEngine.Random.Range(0, this._flightSounds.Length)];
				this._audio.pitch = UnityEngine.Random.Range(this._pitchMin, this._pitchMax);
				this._audio.volume = UnityEngine.Random.Range(this._volumeMin, this._volumeMax);
				this._audio.Play();
			}
			else if (!this._audio.isPlaying && this._idleSounds.Length > 0 && this._idleSoundRandomChance > UnityEngine.Random.value && this._flockChild._landing)
			{
				this._audio.clip = this._idleSounds[UnityEngine.Random.Range(0, this._idleSounds.Length)];
				this._audio.pitch = UnityEngine.Random.Range(this._pitchMin, this._pitchMax);
				this._audio.volume = UnityEngine.Random.Range(this._volumeMin, this._volumeMax);
				this._audio.Play();
				this._hasLanded = true;
			}
		}
	}

	// Token: 0x06000C55 RID: 3157 RVA: 0x0002FFC0 File Offset: 0x0002E3C0
	public void ScareSound()
	{
		if (base.gameObject.activeInHierarchy && this._hasLanded && !this._flockChild._landing && this._idleSoundRandomChance * 2f > UnityEngine.Random.value)
		{
			this._audio.clip = this._scareSounds[UnityEngine.Random.Range(0, this._scareSounds.Length)];
			this._audio.volume = UnityEngine.Random.Range(this._volumeMin, this._volumeMax);
			this._audio.PlayDelayed(UnityEngine.Random.value * 0.2f);
			this._hasLanded = false;
		}
	}

	// Token: 0x04000AFD RID: 2813
	public AudioClip[] _idleSounds;

	// Token: 0x04000AFE RID: 2814
	public float _idleSoundRandomChance = 0.05f;

	// Token: 0x04000AFF RID: 2815
	public AudioClip[] _flightSounds;

	// Token: 0x04000B00 RID: 2816
	public float _flightSoundRandomChance = 0.05f;

	// Token: 0x04000B01 RID: 2817
	public AudioClip[] _scareSounds;

	// Token: 0x04000B02 RID: 2818
	public float _pitchMin = 0.85f;

	// Token: 0x04000B03 RID: 2819
	public float _pitchMax = 1f;

	// Token: 0x04000B04 RID: 2820
	public float _volumeMin = 0.6f;

	// Token: 0x04000B05 RID: 2821
	public float _volumeMax = 0.8f;

	// Token: 0x04000B06 RID: 2822
	private FlockChild _flockChild;

	// Token: 0x04000B07 RID: 2823
	private AudioSource _audio;

	// Token: 0x04000B08 RID: 2824
	private bool _hasLanded;
}
