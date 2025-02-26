using System;
using System.Diagnostics;
using SpriteToParticlesAsset;
using UnityEngine;
using UnityEngine.UI;

namespace SpriteParticleEmitter
{
	// Token: 0x0200058F RID: 1423
	[SerializeField]
	public abstract class EmitterBaseUI : MonoBehaviour
	{
		// Token: 0x060020C8 RID: 8392 RVA: 0x000B1F94 File Offset: 0x000B0394
		protected virtual void Awake()
		{
			this.uiParticleSystem = base.GetComponent<UIParticleRenderer>();
			if (!this.imageRenderer)
			{
				if (this.verboseDebug)
				{
				}
				this.isPlaying = false;
			}
			if (!this.particlesSystem)
			{
				this.particlesSystem = base.GetComponent<ParticleSystem>();
				if (!this.particlesSystem)
				{
					if (this.verboseDebug)
					{
					}
					return;
				}
			}
			this.mainModule = this.particlesSystem.main;
			this.mainModule.loop = false;
			this.mainModule.playOnAwake = false;
			this.particlesSystem.Stop();
			this.SimulationSpace = this.mainModule.simulationSpace;
		}

		// Token: 0x060020C9 RID: 8393
		public abstract void Play();

		// Token: 0x060020CA RID: 8394
		public abstract void Pause();

		// Token: 0x060020CB RID: 8395
		public abstract void Stop();

		// Token: 0x060020CC RID: 8396
		public abstract bool IsPlaying();

		// Token: 0x060020CD RID: 8397
		public abstract bool IsAvailableToPlay();

		// Token: 0x1400007A RID: 122
		// (add) Token: 0x060020CE RID: 8398 RVA: 0x000B204C File Offset: 0x000B044C
		// (remove) Token: 0x060020CF RID: 8399 RVA: 0x000B2084 File Offset: 0x000B0484
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public virtual event SimpleEvent OnCacheEnded;

		// Token: 0x1400007B RID: 123
		// (add) Token: 0x060020D0 RID: 8400 RVA: 0x000B20BC File Offset: 0x000B04BC
		// (remove) Token: 0x060020D1 RID: 8401 RVA: 0x000B20F4 File Offset: 0x000B04F4
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public virtual event SimpleEvent OnAvailableToPlay;

		// Token: 0x060020D2 RID: 8402 RVA: 0x000B212A File Offset: 0x000B052A
		private void DummyMethod()
		{
			if (this.OnAvailableToPlay != null)
			{
				this.OnAvailableToPlay();
			}
			if (this.OnCacheEnded != null)
			{
				this.OnCacheEnded();
			}
		}

		// Token: 0x04002055 RID: 8277
		public bool verboseDebug;

		// Token: 0x04002056 RID: 8278
		[Header("References")]
		[Tooltip("Must be provided by other GameObject's ImageRenderer.")]
		public Image imageRenderer;

		// Token: 0x04002057 RID: 8279
		[Tooltip("If none is provided the script will look for one in this game object.")]
		public ParticleSystem particlesSystem;

		// Token: 0x04002058 RID: 8280
		[Header("Color Emission Options")]
		public bool UseEmissionFromColor;

		// Token: 0x04002059 RID: 8281
		[Tooltip("Emission will take this color as only source position")]
		public Color EmitFromColor;

		// Token: 0x0400205A RID: 8282
		[Range(0.01f, 1f)]
		[Tooltip("In conjunction with EmitFromColor. Defines how much can it deviate from red spectrum for selected color.")]
		public float RedTolerance = 0.05f;

		// Token: 0x0400205B RID: 8283
		[Range(0f, 1f)]
		[Tooltip("In conjunction with EmitFromColor. Defines how much can it deviate from green spectrum for selected color.")]
		public float GreenTolerance = 0.05f;

		// Token: 0x0400205C RID: 8284
		[Range(0f, 1f)]
		[Tooltip("In conjunction with EmitFromColor. Defines how much can it deviate from blue spectrum for selected color.")]
		public float BlueTolerance = 0.05f;

		// Token: 0x0400205D RID: 8285
		[Tooltip("Should new particles override ParticleSystem's startColor and use the color in the pixel they're emitting from?")]
		public bool UsePixelSourceColor;

		// Token: 0x0400205E RID: 8286
		[Tooltip("Must match Particle System's same option")]
		protected ParticleSystemSimulationSpace SimulationSpace;

		// Token: 0x0400205F RID: 8287
		protected bool isPlaying;

		// Token: 0x04002060 RID: 8288
		protected UIParticleRenderer uiParticleSystem;

		// Token: 0x04002061 RID: 8289
		protected ParticleSystem.MainModule mainModule;

		// Token: 0x04002062 RID: 8290
		[Tooltip("Should the transform match target Image Renderer Position?")]
		public bool matchImageRendererPostionData = true;

		// Token: 0x04002063 RID: 8291
		[Tooltip("Should the transform match target Image Renderer Scale?")]
		public bool matchImageRendererScale = true;

		// Token: 0x04002064 RID: 8292
		[Header("Advanced")]
		[Tooltip("This will save memory size when dealing with same sprite being loaded repeatedly by different GameObjects.")]
		public bool useSpritesSharingCache;
	}
}
