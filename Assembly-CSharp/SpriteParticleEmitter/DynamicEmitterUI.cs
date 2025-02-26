using System;
using System.Collections.Generic;
using SpriteToParticlesAsset;
using UnityEngine;
using UnityEngine.Assertions.Comparers;

namespace SpriteParticleEmitter
{
	// Token: 0x02000593 RID: 1427
	[ExecuteInEditMode]
	[RequireComponent(typeof(UIParticleRenderer))]
	public class DynamicEmitterUI : EmitterBaseUI
	{
		// Token: 0x060020FD RID: 8445 RVA: 0x000B3888 File Offset: 0x000B1C88
		protected override void Awake()
		{
			base.Awake();
			if (this.PlayOnAwake)
			{
				this.isPlaying = true;
			}
			this.currentRectTransform = base.GetComponent<RectTransform>();
			this.targetRectTransform = this.imageRenderer.GetComponent<RectTransform>();
			if ((float)this.mainModule.maxParticles < this.EmissionRate)
			{
				this.mainModule.maxParticles = Mathf.CeilToInt(this.EmissionRate);
			}
		}

		// Token: 0x060020FE RID: 8446 RVA: 0x000B38F8 File Offset: 0x000B1CF8
		protected void Update()
		{
			if (this.isPlaying)
			{
				if (this.imageRenderer == null)
				{
					if (this.verboseDebug)
					{
					}
					this.isPlaying = false;
					return;
				}
				if (this.matchImageRendererPostionData)
				{
					this.currentRectTransform.position = new Vector3(this.targetRectTransform.position.x, this.targetRectTransform.position.y, this.targetRectTransform.position.z);
				}
				this.currentRectTransform.pivot = this.targetRectTransform.pivot;
				if (this.matchImageRendererPostionData)
				{
					this.currentRectTransform.anchoredPosition = this.targetRectTransform.anchoredPosition;
					this.currentRectTransform.anchorMin = this.targetRectTransform.anchorMin;
					this.currentRectTransform.anchorMax = this.targetRectTransform.anchorMax;
					this.currentRectTransform.offsetMin = this.targetRectTransform.offsetMin;
					this.currentRectTransform.offsetMax = this.targetRectTransform.offsetMax;
				}
				if (this.matchImageRendererScale)
				{
					this.currentRectTransform.localScale = this.targetRectTransform.localScale;
				}
				this.currentRectTransform.rotation = this.targetRectTransform.rotation;
				this.currentRectTransform.sizeDelta = new Vector2(this.targetRectTransform.rect.width, this.targetRectTransform.rect.height);
				float x = (1f - this.targetRectTransform.pivot.x) * this.targetRectTransform.rect.width - this.targetRectTransform.rect.width / 2f;
				float y = (1f - this.targetRectTransform.pivot.y) * -this.targetRectTransform.rect.height + this.targetRectTransform.rect.height / 2f;
				this.offsetXY = new Vector2(x, y);
				Sprite sprite = this.imageRenderer.sprite;
				if (!sprite)
				{
					if (this.verboseDebug)
					{
					}
					return;
				}
				this.wMult = sprite.pixelsPerUnit * (this.targetRectTransform.rect.width / sprite.rect.size.x);
				this.hMult = sprite.pixelsPerUnit * (this.targetRectTransform.rect.height / sprite.rect.size.y);
				this.ParticlesToEmitThisFrame += this.EmissionRate * Time.deltaTime;
				int num = (int)this.ParticlesToEmitThisFrame;
				if (num > 0)
				{
					this.Emit(num);
				}
				this.ParticlesToEmitThisFrame -= (float)num;
			}
		}

		// Token: 0x060020FF RID: 8447 RVA: 0x000B3C0C File Offset: 0x000B200C
		public void Emit(int emitCount)
		{
			Sprite sprite = this.imageRenderer.sprite;
			if (this.imageRenderer.overrideSprite)
			{
				sprite = this.imageRenderer.overrideSprite;
			}
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
			float num = (float)((int)sprite.rect.size.x);
			float num2 = (float)((int)sprite.rect.size.y);
			ParticleSystem.MinMaxCurve startSize = this.mainModule.startSize;
			float num3 = sprite.pivot.x / pixelsPerUnit;
			float num4 = sprite.pivot.y / pixelsPerUnit;
			Color[] array;
			if (this.useSpritesSharingCache && Application.isPlaying)
			{
				array = SpritesDataPool.GetSpriteColors(sprite, (int)sprite.rect.position.x, (int)sprite.rect.position.y, (int)num, (int)num2);
			}
			else if (this.CacheSprites)
			{
				if (this.spritesSoFar.ContainsKey(sprite))
				{
					array = this.spritesSoFar[sprite];
				}
				else
				{
					array = sprite.texture.GetPixels((int)sprite.rect.position.x, (int)sprite.rect.position.y, (int)num, (int)num2);
					this.spritesSoFar.Add(sprite, array);
				}
			}
			else
			{
				array = sprite.texture.GetPixels((int)sprite.rect.position.x, (int)sprite.rect.position.y, (int)num, (int)num2);
			}
			float redTolerance = this.RedTolerance;
			float greenTolerance = this.GreenTolerance;
			float blueTolerance = this.BlueTolerance;
			float num5 = num * num2;
			Color[] array2 = this.colorCache;
			int[] array3 = this.indexCache;
			if ((float)array2.Length < num5)
			{
				this.colorCache = new Color[(int)num5];
				this.indexCache = new int[(int)num5];
				array2 = this.colorCache;
				array3 = this.indexCache;
			}
			int num6 = 0;
			int num7 = 0;
			while ((float)num7 < num5)
			{
				Color color = array[num7];
				if (color.a > 0f)
				{
					if (!this.UseEmissionFromColor || (FloatComparer.AreEqual(r, color.r, redTolerance) && FloatComparer.AreEqual(g, color.g, greenTolerance) && FloatComparer.AreEqual(b, color.b, blueTolerance)))
					{
						array2[num6] = color;
						array3[num6] = num7;
						num6++;
					}
				}
				num7++;
			}
			if (num6 <= 0)
			{
				return;
			}
			Vector3 zero = Vector3.zero;
			for (int i = 0; i < emitCount; i++)
			{
				int num8 = UnityEngine.Random.Range(0, num6 - 1);
				int num9 = array3[num8];
				float num10 = (float)num9 % num / pixelsPerUnit - num3;
				float num11 = (float)num9 / num / pixelsPerUnit - num4;
				ParticleSystem.EmitParams emitParams = default(ParticleSystem.EmitParams);
				zero.x = num10 * this.wMult + this.offsetXY.x;
				zero.y = num11 * this.hMult - this.offsetXY.y;
				emitParams.position = zero;
				if (this.UsePixelSourceColor)
				{
					emitParams.startColor = array2[num8];
				}
				emitParams.startSize = startSize.constant;
				this.particlesSystem.Emit(emitParams, 1);
			}
		}

		// Token: 0x06002100 RID: 8448 RVA: 0x000B4008 File Offset: 0x000B2408
		public void EmitAll(bool hideSprite = true)
		{
			if (hideSprite)
			{
				this.imageRenderer.enabled = false;
			}
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
			float num = (float)((int)sprite.rect.size.x);
			float num2 = (float)((int)sprite.rect.size.y);
			float constant = this.mainModule.startSize.constant;
			float num3 = sprite.pivot.x / pixelsPerUnit;
			float num4 = sprite.pivot.y / pixelsPerUnit;
			Color[] array;
			if (this.useSpritesSharingCache && Application.isPlaying)
			{
				array = SpritesDataPool.GetSpriteColors(sprite, (int)sprite.rect.position.x, (int)sprite.rect.position.y, (int)num, (int)num2);
			}
			else if (this.CacheSprites)
			{
				if (this.spritesSoFar.ContainsKey(sprite))
				{
					array = this.spritesSoFar[sprite];
				}
				else
				{
					array = sprite.texture.GetPixels((int)sprite.rect.position.x, (int)sprite.rect.position.y, (int)num, (int)num2);
					this.spritesSoFar.Add(sprite, array);
				}
			}
			else
			{
				array = sprite.texture.GetPixels((int)sprite.rect.position.x, (int)sprite.rect.position.y, (int)num, (int)num2);
			}
			float redTolerance = this.RedTolerance;
			float greenTolerance = this.GreenTolerance;
			float blueTolerance = this.BlueTolerance;
			float num5 = num * num2;
			Vector3 zero = Vector3.zero;
			int num6 = 0;
			while ((float)num6 < num5)
			{
				Color c = array[num6];
				if (c.a > 0f)
				{
					if (!this.UseEmissionFromColor || (FloatComparer.AreEqual(r, c.r, redTolerance) && FloatComparer.AreEqual(g, c.g, greenTolerance) && FloatComparer.AreEqual(b, c.b, blueTolerance)))
					{
						float num7 = (float)num6 % num / pixelsPerUnit - num3;
						float num8 = (float)num6 / num / pixelsPerUnit - num4;
						ParticleSystem.EmitParams emitParams = default(ParticleSystem.EmitParams);
						zero.x = num7 * this.wMult + this.offsetXY.x;
						zero.y = num8 * this.hMult - this.offsetXY.y;
						emitParams.position = zero;
						if (this.UsePixelSourceColor)
						{
							emitParams.startColor = c;
						}
						emitParams.startSize = constant;
						this.particlesSystem.Emit(emitParams, 1);
					}
				}
				num6++;
			}
		}

		// Token: 0x06002101 RID: 8449 RVA: 0x000B4357 File Offset: 0x000B2757
		public void RestoreSprite()
		{
			if (this.imageRenderer)
			{
				this.imageRenderer.enabled = true;
			}
		}

		// Token: 0x06002102 RID: 8450 RVA: 0x000B4375 File Offset: 0x000B2775
		public override void Play()
		{
			if (!this.isPlaying && this.particlesSystem)
			{
				this.particlesSystem.Play();
			}
			this.isPlaying = true;
		}

		// Token: 0x06002103 RID: 8451 RVA: 0x000B43A4 File Offset: 0x000B27A4
		public override void Pause()
		{
			if (this.isPlaying && this.particlesSystem)
			{
				this.particlesSystem.Pause();
			}
			this.isPlaying = false;
		}

		// Token: 0x06002104 RID: 8452 RVA: 0x000B43D3 File Offset: 0x000B27D3
		public override void Stop()
		{
			this.isPlaying = false;
		}

		// Token: 0x06002105 RID: 8453 RVA: 0x000B43DC File Offset: 0x000B27DC
		public override bool IsPlaying()
		{
			return this.isPlaying;
		}

		// Token: 0x06002106 RID: 8454 RVA: 0x000B43E4 File Offset: 0x000B27E4
		public override bool IsAvailableToPlay()
		{
			return true;
		}

		// Token: 0x06002107 RID: 8455 RVA: 0x000B43E7 File Offset: 0x000B27E7
		public void ClearCachedSprites()
		{
			this.spritesSoFar = new Dictionary<Sprite, Color[]>();
		}

		// Token: 0x0400207C RID: 8316
		[Tooltip("Start emitting as soon as able")]
		public bool PlayOnAwake = true;

		// Token: 0x0400207D RID: 8317
		[Header("Emission")]
		[Tooltip("Particles to emit per second")]
		public float EmissionRate = 1000f;

		// Token: 0x0400207E RID: 8318
		protected float ParticlesToEmitThisFrame;

		// Token: 0x0400207F RID: 8319
		[Tooltip("Should the system cache sprites data? (Refer to manual for further explanation)")]
		public bool CacheSprites = true;

		// Token: 0x04002080 RID: 8320
		private Color[] colorCache = new Color[1];

		// Token: 0x04002081 RID: 8321
		private int[] indexCache = new int[1];

		// Token: 0x04002082 RID: 8322
		protected Dictionary<Sprite, Color[]> spritesSoFar = new Dictionary<Sprite, Color[]>();

		// Token: 0x04002083 RID: 8323
		private RectTransform targetRectTransform;

		// Token: 0x04002084 RID: 8324
		private RectTransform currentRectTransform;

		// Token: 0x04002085 RID: 8325
		protected Vector2 offsetXY;

		// Token: 0x04002086 RID: 8326
		protected float wMult = 100f;

		// Token: 0x04002087 RID: 8327
		protected float hMult = 100f;
	}
}
