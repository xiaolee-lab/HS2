using System;
using System.Collections.Generic;
using SpriteToParticlesAsset;
using UnityEngine;
using UnityEngine.Assertions.Comparers;

namespace SpriteParticleEmitter
{
	// Token: 0x02000592 RID: 1426
	[ExecuteInEditMode]
	public class DynamicEmitter : EmitterBase
	{
		// Token: 0x060020F0 RID: 8432 RVA: 0x000B2B8C File Offset: 0x000B0F8C
		protected override void Awake()
		{
			base.Awake();
			if (this.PlayOnAwake)
			{
				this.isPlaying = true;
			}
			if ((float)this.mainModule.maxParticles < this.EmissionRate)
			{
				this.mainModule.maxParticles = Mathf.CeilToInt(this.EmissionRate);
			}
		}

		// Token: 0x060020F1 RID: 8433 RVA: 0x000B2BE0 File Offset: 0x000B0FE0
		protected void Update()
		{
			if (this.isPlaying)
			{
				this.ParticlesToEmitThisFrame += this.EmissionRate * Time.deltaTime;
				int num = (int)this.ParticlesToEmitThisFrame;
				if (num > 0)
				{
					this.Emit(num);
				}
				this.ParticlesToEmitThisFrame -= (float)num;
			}
		}

		// Token: 0x060020F2 RID: 8434 RVA: 0x000B2C38 File Offset: 0x000B1038
		public void Emit(int emitCount)
		{
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
			Vector3 b2 = this.spriteRenderer.gameObject.transform.position;
			Quaternion rotation = this.spriteRenderer.gameObject.transform.rotation;
			Vector3 vector = this.spriteRenderer.gameObject.transform.lossyScale;
			if (this.SimulationSpace == ParticleSystemSimulationSpace.Local)
			{
				b2 = Vector3.zero;
				vector = Vector3.one;
				rotation = Quaternion.identity;
			}
			bool flipX = this.spriteRenderer.flipX;
			bool flipY = this.spriteRenderer.flipY;
			float pixelsPerUnit = sprite.pixelsPerUnit;
			float num = (float)((int)sprite.rect.size.x);
			float num2 = (float)((int)sprite.rect.size.y);
			int num3 = (int)num;
			float num4 = 1f / pixelsPerUnit / 2f;
			float num5 = 1f / pixelsPerUnit;
			num5 *= this.mainModule.startSize.constant;
			float num6 = sprite.pivot.x / pixelsPerUnit;
			float num7 = sprite.pivot.y / pixelsPerUnit;
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
			float num8 = num * num2;
			Color[] array2 = this.colorCache;
			int[] array3 = this.indexCache;
			if ((float)array2.Length < num8)
			{
				this.colorCache = new Color[(int)num8];
				this.indexCache = new int[(int)num8];
				array2 = this.colorCache;
				array3 = this.indexCache;
			}
			bool useEmissionFromColor = this.UseEmissionFromColor;
			int num9 = 0;
			bool flag = this.borderEmission == EmitterBase.BorderEmission.Fast || this.borderEmission == EmitterBase.BorderEmission.Precise;
			if (flag)
			{
				bool flag2 = false;
				Color color = array[0];
				int num10 = (int)num;
				bool flag3 = this.borderEmission == EmitterBase.BorderEmission.Precise;
				int num11 = 0;
				while ((float)num11 < num8)
				{
					Color color2 = array[num11];
					bool flag4 = color2.a > 0f;
					if (!flag3)
					{
						goto IL_48F;
					}
					int num12 = num11 - num10;
					if (num12 <= 0)
					{
						goto IL_48F;
					}
					Color color3 = array[num12];
					bool flag5 = color3.a > 0f;
					if (flag4)
					{
						if (flag5)
						{
							goto IL_48F;
						}
						if (!useEmissionFromColor || (FloatComparer.AreEqual(r, color2.r, redTolerance) && FloatComparer.AreEqual(g, color2.g, greenTolerance) && FloatComparer.AreEqual(b, color2.b, blueTolerance)))
						{
							array2[num9] = color2;
							array3[num9] = num11;
							num9++;
							color = color2;
							flag2 = true;
						}
					}
					else
					{
						if (!flag5)
						{
							goto IL_48F;
						}
						if (!useEmissionFromColor || (FloatComparer.AreEqual(r, color3.r, redTolerance) && FloatComparer.AreEqual(g, color3.g, greenTolerance) && FloatComparer.AreEqual(b, color3.b, blueTolerance)))
						{
							array2[num9] = color3;
							array3[num9] = num12;
							num9++;
							goto IL_48F;
						}
					}
					IL_59E:
					num11++;
					continue;
					IL_48F:
					if (flag && !flag4 && flag2)
					{
						if (useEmissionFromColor && (!FloatComparer.AreEqual(r, color.r, redTolerance) || !FloatComparer.AreEqual(g, color.g, greenTolerance) || !FloatComparer.AreEqual(b, color.b, blueTolerance)))
						{
							goto IL_59E;
						}
						array2[num9] = color;
						array3[num9] = num11 - 1;
						num9++;
						flag2 = true;
					}
					color = color2;
					if (!flag4)
					{
						flag2 = false;
						goto IL_59E;
					}
					if (flag && (!flag4 || flag2))
					{
						goto IL_59E;
					}
					if (useEmissionFromColor && (!FloatComparer.AreEqual(r, color2.r, redTolerance) || !FloatComparer.AreEqual(g, color2.g, greenTolerance) || !FloatComparer.AreEqual(b, color2.b, blueTolerance)))
					{
						goto IL_59E;
					}
					array2[num9] = color2;
					array3[num9] = num11;
					num9++;
					flag2 = true;
					goto IL_59E;
				}
			}
			else
			{
				int num13 = 0;
				while ((float)num13 < num8)
				{
					Color color4 = array[num13];
					if (color4.a > 0f)
					{
						if (!useEmissionFromColor || (FloatComparer.AreEqual(r, color4.r, redTolerance) && FloatComparer.AreEqual(g, color4.g, greenTolerance) && FloatComparer.AreEqual(b, color4.b, blueTolerance)))
						{
							array2[num9] = color4;
							array3[num9] = num13;
							num9++;
						}
					}
					num13++;
				}
			}
			if (num9 <= 0)
			{
				return;
			}
			Vector3 zero = Vector3.zero;
			for (int i = 0; i < emitCount; i++)
			{
				int num14 = UnityEngine.Random.Range(0, num9 - 1);
				int num15 = array3[num14];
				float num16 = (float)num15 % num / pixelsPerUnit - num6;
				float num17 = (float)(num15 / num3) / pixelsPerUnit - num7;
				if (flipX)
				{
					num16 = num / pixelsPerUnit - num16 - num6 * 2f;
				}
				if (flipY)
				{
					num17 = num2 / pixelsPerUnit - num17 - num7 * 2f;
				}
				zero.x = num16 * vector.x - num4;
				zero.y = num17 * vector.y + num4;
				ParticleSystem.EmitParams emitParams = default(ParticleSystem.EmitParams);
				emitParams.position = rotation * zero + b2;
				if (this.UsePixelSourceColor)
				{
					emitParams.startColor = array2[num14];
				}
				emitParams.startSize = num5;
				this.particlesSystem.Emit(emitParams, 1);
			}
		}

		// Token: 0x060020F3 RID: 8435 RVA: 0x000B33A8 File Offset: 0x000B17A8
		public void EmitAll(bool hideSprite = true)
		{
			if (hideSprite)
			{
				this.spriteRenderer.enabled = false;
			}
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
			Vector3 b2 = this.spriteRenderer.gameObject.transform.position;
			Quaternion rotation = this.spriteRenderer.gameObject.transform.rotation;
			Vector3 vector = this.spriteRenderer.gameObject.transform.lossyScale;
			if (this.SimulationSpace == ParticleSystemSimulationSpace.Local)
			{
				b2 = Vector3.zero;
				vector = Vector3.one;
				rotation = Quaternion.identity;
			}
			bool flipX = this.spriteRenderer.flipX;
			bool flipY = this.spriteRenderer.flipY;
			float pixelsPerUnit = sprite.pixelsPerUnit;
			float num = (float)((int)sprite.rect.size.x);
			float num2 = (float)((int)sprite.rect.size.y);
			float num3 = 1f / pixelsPerUnit;
			num3 *= this.mainModule.startSize.constant;
			float num4 = sprite.pivot.x / pixelsPerUnit;
			float num5 = sprite.pivot.y / pixelsPerUnit;
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
			float num6 = num * num2;
			Vector3 zero = Vector3.zero;
			int num7 = 0;
			while ((float)num7 < num6)
			{
				Color c = array[num7];
				if (c.a > 0f)
				{
					if (!this.UseEmissionFromColor || (FloatComparer.AreEqual(r, c.r, redTolerance) && FloatComparer.AreEqual(g, c.g, greenTolerance) && FloatComparer.AreEqual(b, c.b, blueTolerance)))
					{
						float num8 = (float)num7 % num / pixelsPerUnit - num4;
						float num9 = (float)num7 / num / pixelsPerUnit - num5;
						if (flipX)
						{
							num8 = num / pixelsPerUnit - num8 - num4 * 2f;
						}
						if (flipY)
						{
							num9 = num2 / pixelsPerUnit - num9 - num5 * 2f;
						}
						zero.x = num8 * vector.x;
						zero.y = num9 * vector.y;
						ParticleSystem.EmitParams emitParams = default(ParticleSystem.EmitParams);
						emitParams.position = rotation * zero + b2;
						if (this.UsePixelSourceColor)
						{
							emitParams.startColor = c;
						}
						emitParams.startSize = num3;
						this.particlesSystem.Emit(emitParams, 1);
					}
				}
				num7++;
			}
		}

		// Token: 0x060020F4 RID: 8436 RVA: 0x000B37AF File Offset: 0x000B1BAF
		public void RestoreSprite()
		{
			this.spriteRenderer.enabled = true;
		}

		// Token: 0x060020F5 RID: 8437 RVA: 0x000B37BD File Offset: 0x000B1BBD
		public override void Play()
		{
			if (!this.isPlaying)
			{
				this.particlesSystem.Play();
			}
			this.isPlaying = true;
		}

		// Token: 0x060020F6 RID: 8438 RVA: 0x000B37DC File Offset: 0x000B1BDC
		public override void Pause()
		{
			if (this.isPlaying)
			{
				this.particlesSystem.Pause();
			}
			this.isPlaying = false;
		}

		// Token: 0x060020F7 RID: 8439 RVA: 0x000B37FB File Offset: 0x000B1BFB
		public override void Stop()
		{
			this.isPlaying = false;
		}

		// Token: 0x060020F8 RID: 8440 RVA: 0x000B3804 File Offset: 0x000B1C04
		public override bool IsPlaying()
		{
			return this.isPlaying;
		}

		// Token: 0x060020F9 RID: 8441 RVA: 0x000B380C File Offset: 0x000B1C0C
		public override bool IsAvailableToPlay()
		{
			return true;
		}

		// Token: 0x060020FA RID: 8442 RVA: 0x000B380F File Offset: 0x000B1C0F
		public void ClearCachedSprites()
		{
			this.spritesSoFar = new Dictionary<Sprite, Color[]>();
		}

		// Token: 0x060020FB RID: 8443 RVA: 0x000B381C File Offset: 0x000B1C1C
		private void DummyMethod()
		{
		}

		// Token: 0x04002078 RID: 8312
		[Tooltip("Should the system cache sprites data? (Refer to manual for further explanation)")]
		public bool CacheSprites = true;

		// Token: 0x04002079 RID: 8313
		private Color[] colorCache = new Color[1];

		// Token: 0x0400207A RID: 8314
		private int[] indexCache = new int[1];

		// Token: 0x0400207B RID: 8315
		protected Dictionary<Sprite, Color[]> spritesSoFar = new Dictionary<Sprite, Color[]>();
	}
}
