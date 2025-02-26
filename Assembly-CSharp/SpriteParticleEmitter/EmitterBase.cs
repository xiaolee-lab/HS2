using System;
using System.Diagnostics;
using UnityEngine;

namespace SpriteParticleEmitter
{
	// Token: 0x0200058D RID: 1421
	[SerializeField]
	public abstract class EmitterBase : MonoBehaviour
	{
		// Token: 0x060020BC RID: 8380 RVA: 0x000B1D84 File Offset: 0x000B0184
		protected virtual void Awake()
		{
			if (!this.spriteRenderer)
			{
				if (this.verboseDebug)
				{
				}
				this.spriteRenderer = base.GetComponent<SpriteRenderer>();
				if (this.spriteRenderer || this.verboseDebug)
				{
				}
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

		// Token: 0x060020BD RID: 8381
		public abstract void Play();

		// Token: 0x060020BE RID: 8382
		public abstract void Pause();

		// Token: 0x060020BF RID: 8383
		public abstract void Stop();

		// Token: 0x060020C0 RID: 8384
		public abstract bool IsPlaying();

		// Token: 0x060020C1 RID: 8385
		public abstract bool IsAvailableToPlay();

		// Token: 0x14000078 RID: 120
		// (add) Token: 0x060020C2 RID: 8386 RVA: 0x000B1E50 File Offset: 0x000B0250
		// (remove) Token: 0x060020C3 RID: 8387 RVA: 0x000B1E88 File Offset: 0x000B0288
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public virtual event SimpleEvent OnCacheEnded;

		// Token: 0x14000079 RID: 121
		// (add) Token: 0x060020C4 RID: 8388 RVA: 0x000B1EC0 File Offset: 0x000B02C0
		// (remove) Token: 0x060020C5 RID: 8389 RVA: 0x000B1EF8 File Offset: 0x000B02F8
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public virtual event SimpleEvent OnAvailableToPlay;

		// Token: 0x060020C6 RID: 8390 RVA: 0x000B1F2E File Offset: 0x000B032E
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

		// Token: 0x0400203C RID: 8252
		public bool verboseDebug;

		// Token: 0x0400203D RID: 8253
		[Header("References")]
		[Tooltip("If none is provided the script will look for one in this game object.")]
		public SpriteRenderer spriteRenderer;

		// Token: 0x0400203E RID: 8254
		[Tooltip("If none is provided the script will look for one in this game object.")]
		public ParticleSystem particlesSystem;

		// Token: 0x0400203F RID: 8255
		[Header("Emission Options")]
		[Tooltip("Start emitting as soon as able. (On static emission activating this will force CacheOnAwake)")]
		public bool PlayOnAwake = true;

		// Token: 0x04002040 RID: 8256
		[Tooltip("Particles to emit per second")]
		public float EmissionRate = 1000f;

		// Token: 0x04002041 RID: 8257
		[Tooltip("Should new particles override ParticleSystem's startColor and use the color in the pixel they're emitting from?")]
		public bool UsePixelSourceColor;

		// Token: 0x04002042 RID: 8258
		public EmitterBase.BorderEmission borderEmission;

		// Token: 0x04002043 RID: 8259
		[Space(10f)]
		public bool UseEmissionFromColor;

		// Token: 0x04002044 RID: 8260
		[Tooltip("Emission will take this color as only source position")]
		public Color EmitFromColor;

		// Token: 0x04002045 RID: 8261
		[Range(0.01f, 1f)]
		[Tooltip("In conjunction with EmitFromColor. Defines how much can it deviate from red spectrum for selected color.")]
		public float RedTolerance = 0.05f;

		// Token: 0x04002046 RID: 8262
		[Range(0f, 1f)]
		[Tooltip("In conjunction with EmitFromColor. Defines how much can it deviate from green spectrum for selected color.")]
		public float GreenTolerance = 0.05f;

		// Token: 0x04002047 RID: 8263
		[Range(0f, 1f)]
		[Tooltip("In conjunction with EmitFromColor. Defines how much can it deviate from blue spectrum for selected color.")]
		public float BlueTolerance = 0.05f;

		// Token: 0x04002048 RID: 8264
		protected ParticleSystemSimulationSpace SimulationSpace;

		// Token: 0x04002049 RID: 8265
		protected bool isPlaying;

		// Token: 0x0400204A RID: 8266
		protected ParticleSystem.MainModule mainModule;

		// Token: 0x0400204B RID: 8267
		[Header("Advanced")]
		[Tooltip("This will save memory size when dealing with same sprite being loaded repeatedly by different GameObjects.")]
		public bool useSpritesSharingCache;

		// Token: 0x0400204C RID: 8268
		public bool useBetweenFramesPrecision;

		// Token: 0x0400204D RID: 8269
		protected Vector3 lastTransformPosition;

		// Token: 0x0400204E RID: 8270
		protected float ParticlesToEmitThisFrame;

		// Token: 0x0200058E RID: 1422
		public enum BorderEmission
		{
			// Token: 0x04002052 RID: 8274
			Off,
			// Token: 0x04002053 RID: 8275
			Fast,
			// Token: 0x04002054 RID: 8276
			Precise
		}
	}
}
