using System;
using System.Collections.Generic;
using System.Diagnostics;
using SpriteToParticlesAsset;
using UnityEngine;
using UnityEngine.Assertions.Comparers;

namespace SpriteParticleEmitter
{
	// Token: 0x02000591 RID: 1425
	[ExecuteInEditMode]
	[RequireComponent(typeof(UIParticleRenderer))]
	public class StaticUIImageEmitter : EmitterBaseUI
	{
		// Token: 0x1400007E RID: 126
		// (add) Token: 0x060020E2 RID: 8418 RVA: 0x000B26DC File Offset: 0x000B0ADC
		// (remove) Token: 0x060020E3 RID: 8419 RVA: 0x000B2714 File Offset: 0x000B0B14
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public override event SimpleEvent OnCacheEnded;

		// Token: 0x1400007F RID: 127
		// (add) Token: 0x060020E4 RID: 8420 RVA: 0x000B274C File Offset: 0x000B0B4C
		// (remove) Token: 0x060020E5 RID: 8421 RVA: 0x000B2784 File Offset: 0x000B0B84
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public override event SimpleEvent OnAvailableToPlay;

		// Token: 0x060020E6 RID: 8422 RVA: 0x000B27BA File Offset: 0x000B0BBA
		protected override void Awake()
		{
			base.Awake();
			if (this.PlayOnAwake)
			{
				this.isPlaying = true;
				this.CacheOnAwake = true;
			}
			if (this.CacheOnAwake)
			{
				this.CacheSprite(false);
			}
		}

		// Token: 0x060020E7 RID: 8423 RVA: 0x000B27F0 File Offset: 0x000B0BF0
		public virtual void CacheSprite(bool relativeToParent = false)
		{
			this.hasCachingEnded = false;
			this.particlesCacheCount = 0;
			Sprite sprite = this.imageRenderer.sprite;
			if (!sprite)
			{
				if (this.verboseDebug)
				{
				}
				return;
			}
			float r = this.EmitFromColor.r;
			float g = this.EmitFromColor.g;
			float b = this.EmitFromColor.b;
			float pixelsPerUnit = sprite.pixelsPerUnit;
			if ((!(this.imageRenderer == null) && !(this.imageRenderer.sprite == null)) || this.verboseDebug)
			{
			}
			float num = (float)((int)sprite.rect.size.x);
			float num2 = (float)((int)sprite.rect.size.y);
			this.particleStartSize = this.mainModule.startSize.constant;
			float num3 = sprite.pivot.x / pixelsPerUnit;
			float num4 = sprite.pivot.y / pixelsPerUnit;
			Color[] array;
			if (this.useSpritesSharingCache && Application.isPlaying)
			{
				array = SpritesDataPool.GetSpriteColors(sprite, (int)sprite.rect.position.x, (int)sprite.rect.position.y, (int)num, (int)num2);
			}
			else
			{
				array = sprite.texture.GetPixels((int)sprite.rect.position.x, (int)sprite.rect.position.y, (int)num, (int)num2);
			}
			float redTolerance = this.RedTolerance;
			float greenTolerance = this.GreenTolerance;
			float blueTolerance = this.BlueTolerance;
			float num5 = num * num2;
			List<Color> list = new List<Color>();
			List<Vector3> list2 = new List<Vector3>();
			int num6 = 0;
			while ((float)num6 < num5)
			{
				Color item = array[num6];
				if (item.a > 0f)
				{
					if (!this.UseEmissionFromColor || (FloatComparer.AreEqual(r, item.r, redTolerance) && FloatComparer.AreEqual(g, item.g, greenTolerance) && FloatComparer.AreEqual(b, item.b, blueTolerance)))
					{
						float x = (float)num6 % num / pixelsPerUnit - num3;
						float y = (float)num6 / num / pixelsPerUnit - num4;
						Vector3 item2 = new Vector3(x, y, 0f);
						list2.Add(item2);
						list.Add(item);
						this.particlesCacheCount++;
					}
				}
				num6++;
			}
			this.particleInitPositionsCache = list2.ToArray();
			this.particleInitColorCache = list.ToArray();
			if (this.particlesCacheCount <= 0)
			{
				if (this.verboseDebug)
				{
				}
				return;
			}
			list2.Clear();
			list.Clear();
			GC.Collect();
			this.hasCachingEnded = true;
			if (this.OnCacheEnded != null)
			{
				this.OnCacheEnded();
			}
		}

		// Token: 0x060020E8 RID: 8424 RVA: 0x000B2B13 File Offset: 0x000B0F13
		protected virtual void Update()
		{
		}

		// Token: 0x060020E9 RID: 8425 RVA: 0x000B2B15 File Offset: 0x000B0F15
		public override void Play()
		{
		}

		// Token: 0x060020EA RID: 8426 RVA: 0x000B2B17 File Offset: 0x000B0F17
		public override void Stop()
		{
		}

		// Token: 0x060020EB RID: 8427 RVA: 0x000B2B19 File Offset: 0x000B0F19
		public override void Pause()
		{
		}

		// Token: 0x060020EC RID: 8428 RVA: 0x000B2B1B File Offset: 0x000B0F1B
		public override bool IsPlaying()
		{
			return this.isPlaying;
		}

		// Token: 0x060020ED RID: 8429 RVA: 0x000B2B23 File Offset: 0x000B0F23
		public override bool IsAvailableToPlay()
		{
			return this.hasCachingEnded;
		}

		// Token: 0x060020EE RID: 8430 RVA: 0x000B2B2B File Offset: 0x000B0F2B
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

		// Token: 0x0400206F RID: 8303
		[Header("Awake Options")]
		[Tooltip("Activating this will force CacheOnAwake")]
		public bool PlayOnAwake = true;

		// Token: 0x04002070 RID: 8304
		[Tooltip("Should the system cache on Awake method? - Static emission needs to be cached first, if this property is not checked the CacheSprite() method should be called by code. (Refer to manual for further explanation)")]
		public bool CacheOnAwake = true;

		// Token: 0x04002071 RID: 8305
		protected bool hasCachingEnded;

		// Token: 0x04002072 RID: 8306
		protected int particlesCacheCount;

		// Token: 0x04002073 RID: 8307
		protected float particleStartSize;

		// Token: 0x04002074 RID: 8308
		protected Vector3[] particleInitPositionsCache;

		// Token: 0x04002075 RID: 8309
		protected Color[] particleInitColorCache;
	}
}
