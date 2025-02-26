using System;
using AIProject;
using Manager;
using UnityEngine;

namespace Studio.Sound
{
	// Token: 0x020012AA RID: 4778
	public class SEComponent : MonoBehaviour
	{
		// Token: 0x170021C4 RID: 8644
		// (get) Token: 0x06009DFD RID: 40445 RVA: 0x004072C2 File Offset: 0x004056C2
		// (set) Token: 0x06009DFE RID: 40446 RVA: 0x004072CA File Offset: 0x004056CA
		public AudioClip Clip
		{
			get
			{
				return this._clip;
			}
			set
			{
				this._clip = value;
			}
		}

		// Token: 0x170021C5 RID: 8645
		// (get) Token: 0x06009DFF RID: 40447 RVA: 0x004072D3 File Offset: 0x004056D3
		// (set) Token: 0x06009E00 RID: 40448 RVA: 0x004072DB File Offset: 0x004056DB
		public Sound.Type SoundType
		{
			get
			{
				return this._soundType;
			}
			set
			{
				this._soundType = value;
			}
		}

		// Token: 0x170021C6 RID: 8646
		// (get) Token: 0x06009E01 RID: 40449 RVA: 0x004072E4 File Offset: 0x004056E4
		// (set) Token: 0x06009E02 RID: 40450 RVA: 0x004072EC File Offset: 0x004056EC
		public bool IsLoop
		{
			get
			{
				return this._isLoop;
			}
			set
			{
				this._isLoop = value;
				if (this._audioSource != null)
				{
					this._audioSource.loop = value;
				}
			}
		}

		// Token: 0x170021C7 RID: 8647
		// (get) Token: 0x06009E03 RID: 40451 RVA: 0x00407312 File Offset: 0x00405712
		// (set) Token: 0x06009E04 RID: 40452 RVA: 0x0040731C File Offset: 0x0040571C
		public SEComponent.RolloffType DecayType
		{
			get
			{
				return this._type;
			}
			set
			{
				this._type = value;
				if (this._audioSource != null)
				{
					if (this._type == SEComponent.RolloffType.線形)
					{
						this._audioSource.rolloffMode = AudioRolloffMode.Linear;
					}
					else
					{
						this._audioSource.rolloffMode = AudioRolloffMode.Logarithmic;
					}
				}
			}
		}

		// Token: 0x170021C8 RID: 8648
		// (get) Token: 0x06009E05 RID: 40453 RVA: 0x0040736A File Offset: 0x0040576A
		// (set) Token: 0x06009E06 RID: 40454 RVA: 0x00407374 File Offset: 0x00405774
		public Threshold RolloffDistance
		{
			get
			{
				return this._rolloffDistance;
			}
			set
			{
				Threshold rolloffDistance = new Threshold(Mathf.Max(0f, value.min), value.max);
				this._rolloffDistance = rolloffDistance;
				if (this._audioSource != null)
				{
					this._audioSource.minDistance = this._rolloffDistance.min;
					this._audioSource.maxDistance = this._rolloffDistance.max;
				}
			}
		}

		// Token: 0x170021C9 RID: 8649
		// (get) Token: 0x06009E07 RID: 40455 RVA: 0x004073E4 File Offset: 0x004057E4
		// (set) Token: 0x06009E08 RID: 40456 RVA: 0x004073EC File Offset: 0x004057EC
		public float Volume
		{
			get
			{
				return this._volume;
			}
			set
			{
				float volume = Mathf.Max(0f, Mathf.Min(1f, value));
				this._volume = volume;
				if (this._audioSource != null)
				{
					this._audioSource.volume = volume;
				}
			}
		}

		// Token: 0x06009E09 RID: 40457 RVA: 0x00407434 File Offset: 0x00405834
		private void OnEnable()
		{
			if (this._audioSource == null)
			{
				this._audioSource = Singleton<Sound>.Instance.Play(this._soundType, this._clip, 0f);
			}
			if (!this._audioSource.isPlaying)
			{
				this._audioSource.Play();
			}
			if (this._type == SEComponent.RolloffType.線形)
			{
				this._audioSource.rolloffMode = AudioRolloffMode.Linear;
			}
			else
			{
				this._audioSource.rolloffMode = AudioRolloffMode.Logarithmic;
			}
			this._audioSource.loop = this._isLoop;
			this._audioSource.minDistance = this._rolloffDistance.min;
			this._audioSource.maxDistance = this._rolloffDistance.max;
			this._audioSource.volume = this._volume;
		}

		// Token: 0x06009E0A RID: 40458 RVA: 0x00407505 File Offset: 0x00405905
		private void OnDisable()
		{
			if (this._audioSource != null && this._audioSource.isPlaying)
			{
				this._audioSource.Stop();
			}
		}

		// Token: 0x06009E0B RID: 40459 RVA: 0x00407533 File Offset: 0x00405933
		private void Update()
		{
			if (this._audioSource == null)
			{
				return;
			}
			this._audioSource.transform.position = base.transform.position;
		}

		// Token: 0x04007DA7 RID: 32167
		[SerializeField]
		private AudioClip _clip;

		// Token: 0x04007DA8 RID: 32168
		[SerializeField]
		private Sound.Type _soundType = Sound.Type.GameSE3D;

		// Token: 0x04007DA9 RID: 32169
		[SerializeField]
		private bool _isLoop;

		// Token: 0x04007DAA RID: 32170
		[SerializeField]
		private SEComponent.RolloffType _type = SEComponent.RolloffType.線形;

		// Token: 0x04007DAB RID: 32171
		[SerializeField]
		private Threshold _rolloffDistance = new Threshold(0f, 1f);

		// Token: 0x04007DAC RID: 32172
		[SerializeField]
		[Range(0f, 1f)]
		private float _volume = 1f;

		// Token: 0x04007DAD RID: 32173
		private AudioSource _audioSource;

		// Token: 0x020012AB RID: 4779
		public enum RolloffType
		{
			// Token: 0x04007DAF RID: 32175
			対数関数,
			// Token: 0x04007DB0 RID: 32176
			線形
		}
	}
}
