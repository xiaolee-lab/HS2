using System;
using System.Collections.Generic;
using System.Diagnostics;
using SpriteToParticlesAsset;
using UnityEngine;
using UnityEngine.Assertions.Comparers;

namespace SpriteParticleEmitter
{
	// Token: 0x02000590 RID: 1424
	[ExecuteInEditMode]
	public class StaticSpriteEmitter : EmitterBase
	{
		// Token: 0x1400007C RID: 124
		// (add) Token: 0x060020D4 RID: 8404 RVA: 0x000B2168 File Offset: 0x000B0568
		// (remove) Token: 0x060020D5 RID: 8405 RVA: 0x000B21A0 File Offset: 0x000B05A0
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public override event SimpleEvent OnCacheEnded;

		// Token: 0x1400007D RID: 125
		// (add) Token: 0x060020D6 RID: 8406 RVA: 0x000B21D8 File Offset: 0x000B05D8
		// (remove) Token: 0x060020D7 RID: 8407 RVA: 0x000B2210 File Offset: 0x000B0610
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public override event SimpleEvent OnAvailableToPlay;

		// Token: 0x060020D8 RID: 8408 RVA: 0x000B2246 File Offset: 0x000B0646
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

		// Token: 0x060020D9 RID: 8409 RVA: 0x000B227C File Offset: 0x000B067C
		public virtual void CacheSprite(bool relativeToParent = false)
		{
			this.hasCachingEnded = false;
			this.particlesCacheCount = 0;
			Sprite sprite = this.spriteRenderer.sprite;
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
			Vector3 position = this.spriteRenderer.gameObject.transform.position;
			Quaternion rotation = this.spriteRenderer.gameObject.transform.rotation;
			Vector3 lossyScale = this.spriteRenderer.gameObject.transform.lossyScale;
			bool flipX = this.spriteRenderer.flipX;
			bool flipY = this.spriteRenderer.flipY;
			float pixelsPerUnit = sprite.pixelsPerUnit;
			if ((!(this.spriteRenderer == null) && !(this.spriteRenderer.sprite == null)) || this.verboseDebug)
			{
			}
			float num = (float)((int)sprite.rect.size.x);
			float num2 = (float)((int)sprite.rect.size.y);
			this.particleStartSize = 1f / pixelsPerUnit;
			this.particleStartSize *= this.mainModule.startSize.constant;
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
						float num7 = (float)num6 % num / pixelsPerUnit - num3;
						float num8 = (float)num6 / num / pixelsPerUnit - num4;
						if (flipX)
						{
							num7 = num / pixelsPerUnit - num7 - num3 * 2f;
						}
						if (flipY)
						{
							num8 = num2 / pixelsPerUnit - num8 - num4 * 2f;
						}
						Vector3 item2;
						if (relativeToParent)
						{
							item2 = rotation * new Vector3(num7 * lossyScale.x, num8 * lossyScale.y, 0f) + position;
						}
						else
						{
							item2 = new Vector3(num7, num8, 0f);
						}
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

		// Token: 0x060020DA RID: 8410 RVA: 0x000B2680 File Offset: 0x000B0A80
		protected virtual void Update()
		{
		}

		// Token: 0x060020DB RID: 8411 RVA: 0x000B2682 File Offset: 0x000B0A82
		public override void Play()
		{
		}

		// Token: 0x060020DC RID: 8412 RVA: 0x000B2684 File Offset: 0x000B0A84
		public override void Stop()
		{
		}

		// Token: 0x060020DD RID: 8413 RVA: 0x000B2686 File Offset: 0x000B0A86
		public override void Pause()
		{
		}

		// Token: 0x060020DE RID: 8414 RVA: 0x000B2688 File Offset: 0x000B0A88
		public override bool IsPlaying()
		{
			return this.isPlaying;
		}

		// Token: 0x060020DF RID: 8415 RVA: 0x000B2690 File Offset: 0x000B0A90
		public override bool IsAvailableToPlay()
		{
			return this.hasCachingEnded;
		}

		// Token: 0x060020E0 RID: 8416 RVA: 0x000B2698 File Offset: 0x000B0A98
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

		// Token: 0x04002067 RID: 8295
		[Header("Awake Options")]
		[Tooltip("Should the system cache on Awake method? - Static emission needs to be cached first, if this property is not checked the CacheSprite() method should be called by code. (Refer to manual for further explanation)")]
		public bool CacheOnAwake = true;

		// Token: 0x04002068 RID: 8296
		protected bool hasCachingEnded;

		// Token: 0x04002069 RID: 8297
		protected int particlesCacheCount;

		// Token: 0x0400206A RID: 8298
		protected float particleStartSize;

		// Token: 0x0400206B RID: 8299
		protected Vector3[] particleInitPositionsCache;

		// Token: 0x0400206C RID: 8300
		protected Color[] particleInitColorCache;
	}
}
