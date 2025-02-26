using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Assertions.Comparers;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace SpriteToParticlesAsset
{
	// Token: 0x0200059B RID: 1435
	[ExecuteInEditMode]
	public class SpriteToParticles : MonoBehaviour
	{
		// Token: 0x14000083 RID: 131
		// (add) Token: 0x0600213A RID: 8506 RVA: 0x000B5788 File Offset: 0x000B3B88
		// (remove) Token: 0x0600213B RID: 8507 RVA: 0x000B57C0 File Offset: 0x000B3BC0
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event SimpleEvent OnCacheEnded;

		// Token: 0x14000084 RID: 132
		// (add) Token: 0x0600213C RID: 8508 RVA: 0x000B57F8 File Offset: 0x000B3BF8
		// (remove) Token: 0x0600213D RID: 8509 RVA: 0x000B5830 File Offset: 0x000B3C30
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event SimpleEvent OnAvailableToPlay;

		// Token: 0x0600213E RID: 8510 RVA: 0x000B5868 File Offset: 0x000B3C68
		protected virtual void Awake()
		{
			this.spritesSoFar = new Dictionary<Sprite, Color[]>();
			this.colorCache = new Color[1];
			this.indexCache = new int[1];
			this.particleInitPositionsCache = null;
			this.particleInitColorCache = null;
			if (this.renderSystemType == RenderSystemUsing.SpriteRenderer)
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
				if (this.spriteRenderer)
				{
					this.spriteTransformReference = this.spriteRenderer.gameObject.transform;
					this.lastTransformPosition = this.spriteTransformReference.position;
				}
			}
			else
			{
				this.uiParticleSystem = base.GetComponent<UIParticleRenderer>();
				if (!this.uiParticleSystem)
				{
					if (this.verboseDebug)
					{
					}
					this.isPlaying = false;
					return;
				}
				if (!this.imageRenderer)
				{
					if (this.verboseDebug)
					{
					}
					this.isPlaying = false;
					return;
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
					this.isPlaying = false;
					return;
				}
			}
			this.mainModule = this.particlesSystem.main;
			this.mainModule.loop = true;
			this.mainModule.playOnAwake = true;
			this.particlesSystem.Stop();
			this.SimulationSpace = this.mainModule.simulationSpace;
			if (this.PlayOnAwake)
			{
				this.isPlaying = true;
				this.particlesSystem.Emit(1);
				this.particlesSystem.Clear();
				if (Application.isPlaying)
				{
					this.particlesSystem.Play();
				}
			}
			if (this.renderSystemType == RenderSystemUsing.ImageRenderer)
			{
				this.currentRectTransform = base.GetComponent<RectTransform>();
				this.targetRectTransform = this.imageRenderer.GetComponent<RectTransform>();
			}
			if ((float)this.mainModule.maxParticles < this.EmissionRate)
			{
				this.mainModule.maxParticles = Mathf.CeilToInt(this.EmissionRate);
			}
			if (this.mode == SpriteMode.Static)
			{
				if (this.PlayOnAwake)
				{
					this.CacheOnAwake = true;
				}
				if (this.CacheOnAwake)
				{
					this.CacheSprite(false);
				}
			}
		}

		// Token: 0x0600213F RID: 8511 RVA: 0x000B5ACC File Offset: 0x000B3ECC
		public void Update()
		{
			bool flag = this.isPlaying;
			if (this.mode == SpriteMode.Static)
			{
				flag = (this.isPlaying && this.hasCachingEnded);
			}
			if (!flag)
			{
				return;
			}
			if (this.renderSystemType == RenderSystemUsing.ImageRenderer)
			{
				this.HandlePositionAndScaleForUI();
				if (!this.isPlaying)
				{
					return;
				}
			}
			else
			{
				this.HandlePosition();
			}
			this.ParticlesToEmitThisFrame += this.EmissionRate * Time.deltaTime;
			int num = (int)this.ParticlesToEmitThisFrame;
			if (num > 0)
			{
				this.Emit(num);
			}
			this.ParticlesToEmitThisFrame -= (float)num;
			if (this.renderSystemType == RenderSystemUsing.SpriteRenderer)
			{
				if (!this.spriteTransformReference)
				{
					this.spriteTransformReference = this.spriteRenderer.transform;
				}
				this.lastTransformPosition = this.spriteTransformReference.position;
			}
		}

		// Token: 0x06002140 RID: 8512 RVA: 0x000B5BAC File Offset: 0x000B3FAC
		public void Emit(int emitCount)
		{
			this.HackUnityCrash2017();
			if (this.mode == SpriteMode.Dynamic)
			{
				this.EmitDynamic(emitCount);
			}
			else
			{
				this.EmitStatic(emitCount);
			}
		}

		// Token: 0x06002141 RID: 8513 RVA: 0x000B5BD4 File Offset: 0x000B3FD4
		public void EmitAll(bool hideSprite = true)
		{
			if (hideSprite)
			{
				if (this.renderSystemType == RenderSystemUsing.SpriteRenderer)
				{
					this.spriteRenderer.enabled = false;
				}
				else
				{
					this.imageRenderer.enabled = false;
				}
			}
			if (this.mode == SpriteMode.Dynamic)
			{
				this.EmitAllDynamic();
			}
			else
			{
				this.EmitAllStatic();
			}
		}

		// Token: 0x06002142 RID: 8514 RVA: 0x000B5C2B File Offset: 0x000B402B
		public void RestoreSprite()
		{
			if (this.renderSystemType == RenderSystemUsing.SpriteRenderer)
			{
				this.spriteRenderer.enabled = true;
			}
		}

		// Token: 0x06002143 RID: 8515 RVA: 0x000B5C44 File Offset: 0x000B4044
		public void Play()
		{
			if (!this.isPlaying)
			{
				this.particlesSystem.Play();
			}
			this.isPlaying = true;
		}

		// Token: 0x06002144 RID: 8516 RVA: 0x000B5C63 File Offset: 0x000B4063
		public void Pause()
		{
			if (this.isPlaying)
			{
				this.particlesSystem.Pause();
			}
			this.isPlaying = false;
		}

		// Token: 0x06002145 RID: 8517 RVA: 0x000B5C82 File Offset: 0x000B4082
		public void Stop()
		{
			this.isPlaying = false;
		}

		// Token: 0x06002146 RID: 8518 RVA: 0x000B5C8B File Offset: 0x000B408B
		public bool IsPlaying()
		{
			return this.isPlaying;
		}

		// Token: 0x06002147 RID: 8519 RVA: 0x000B5C93 File Offset: 0x000B4093
		public bool IsAvailableToPlay()
		{
			return this.mode != SpriteMode.Static || this.hasCachingEnded;
		}

		// Token: 0x06002148 RID: 8520 RVA: 0x000B5CAA File Offset: 0x000B40AA
		public void ClearCachedSprites()
		{
			this.spritesSoFar = new Dictionary<Sprite, Color[]>();
		}

		// Token: 0x06002149 RID: 8521 RVA: 0x000B5CB7 File Offset: 0x000B40B7
		private void HandlePositionAndScaleForUI()
		{
			if (this.mode == SpriteMode.Dynamic)
			{
				this.ProcessPositionAndScaleDynamic();
			}
			else
			{
				this.ProcessPositionAndScaleStatic();
			}
		}

		// Token: 0x0600214A RID: 8522 RVA: 0x000B5CD8 File Offset: 0x000B40D8
		private void HandlePosition()
		{
			if (this.matchTargetGOPostionData && this.spriteRenderer != null)
			{
				base.transform.position = this.spriteRenderer.transform.position;
			}
			if (this.matchTargetGOScale && this.spriteRenderer != null)
			{
				base.transform.localScale = this.spriteRenderer.transform.lossyScale;
			}
		}

		// Token: 0x0600214B RID: 8523 RVA: 0x000B5D54 File Offset: 0x000B4154
		private void ProcessPositionAndScaleDynamic()
		{
			if (this.imageRenderer == null)
			{
				if (this.verboseDebug)
				{
				}
				this.isPlaying = false;
				return;
			}
			if (this.matchTargetGOPostionData)
			{
				this.currentRectTransform.position = new Vector3(this.targetRectTransform.position.x, this.targetRectTransform.position.y, this.targetRectTransform.position.z);
			}
			this.currentRectTransform.pivot = this.targetRectTransform.pivot;
			if (this.matchTargetGOPostionData)
			{
				this.currentRectTransform.anchoredPosition = this.targetRectTransform.anchoredPosition;
				this.currentRectTransform.anchorMin = this.targetRectTransform.anchorMin;
				this.currentRectTransform.anchorMax = this.targetRectTransform.anchorMax;
				this.currentRectTransform.offsetMin = this.targetRectTransform.offsetMin;
				this.currentRectTransform.offsetMax = this.targetRectTransform.offsetMax;
			}
			if (this.matchTargetGOScale)
			{
				this.currentRectTransform.localScale = this.targetRectTransform.localScale;
			}
			this.currentRectTransform.rotation = this.targetRectTransform.rotation;
			this.currentRectTransform.sizeDelta = new Vector2(this.targetRectTransform.rect.width, this.targetRectTransform.rect.height);
			float x = (1f - this.targetRectTransform.pivot.x) * this.targetRectTransform.rect.width - this.targetRectTransform.rect.width / 2f;
			float y = (1f - this.targetRectTransform.pivot.y) * -this.targetRectTransform.rect.height + this.targetRectTransform.rect.height / 2f;
			this.offsetXY = new Vector2(x, y);
			Sprite sprite = this.GetSprite();
			if (this.imageRenderer.preserveAspect)
			{
				float num = sprite.rect.size.y / sprite.rect.size.x;
				float num2 = this.targetRectTransform.rect.height / this.targetRectTransform.rect.width;
				if (num > num2)
				{
					this.wMult = sprite.pixelsPerUnit * (this.targetRectTransform.rect.width / sprite.rect.size.x) * (num2 / num);
					this.hMult = sprite.pixelsPerUnit * (this.targetRectTransform.rect.height / sprite.rect.size.y);
				}
				else
				{
					this.wMult = sprite.pixelsPerUnit * (this.targetRectTransform.rect.width / sprite.rect.size.x);
					this.hMult = sprite.pixelsPerUnit * (this.targetRectTransform.rect.height / sprite.rect.size.y) * (num / num2);
				}
			}
			else
			{
				this.wMult = sprite.pixelsPerUnit * (this.targetRectTransform.rect.width / sprite.rect.size.x);
				this.hMult = sprite.pixelsPerUnit * (this.targetRectTransform.rect.height / sprite.rect.size.y);
			}
		}

		// Token: 0x0600214C RID: 8524 RVA: 0x000B6180 File Offset: 0x000B4580
		private void EmitDynamic(int emitCount)
		{
			Sprite sprite = this.GetSprite();
			if (!sprite)
			{
				return;
			}
			float r = this.EmitFromColor.r;
			float g = this.EmitFromColor.g;
			float b = this.EmitFromColor.b;
			float pixelsPerUnit = sprite.pixelsPerUnit;
			float num = (float)((int)sprite.rect.size.x);
			float num2 = (float)((int)sprite.rect.size.y);
			float startSize = this.GetParticleStartSize(pixelsPerUnit);
			float num3 = sprite.pivot.x / pixelsPerUnit;
			float num4 = sprite.pivot.y / pixelsPerUnit;
			Color[] spriteColorsData = this.GetSpriteColorsData(sprite);
			float redTolerance = this.RedTolerance;
			float greenTolerance = this.GreenTolerance;
			float blueTolerance = this.BlueTolerance;
			float num5 = num * num2;
			Color[] array = this.colorCache;
			int[] array2 = this.indexCache;
			if ((float)array.Length < num5)
			{
				this.colorCache = new Color[(int)num5];
				this.indexCache = new int[(int)num5];
				array = this.colorCache;
				array2 = this.indexCache;
			}
			int num6 = (int)num;
			float num7 = 1f / pixelsPerUnit / 2f;
			Vector3 vector = Vector3.zero;
			Quaternion rotation = Quaternion.identity;
			Vector3 vector2 = Vector3.one;
			bool flag = false;
			bool flag2 = false;
			if (this.renderSystemType == RenderSystemUsing.SpriteRenderer)
			{
				if (this.SimulationSpace != ParticleSystemSimulationSpace.Local)
				{
					vector = this.spriteTransformReference.position;
					rotation = this.spriteTransformReference.rotation;
					vector2 = this.spriteTransformReference.lossyScale;
				}
				flag = this.spriteRenderer.flipX;
				flag2 = this.spriteRenderer.flipY;
			}
			int num8 = 0;
			bool useEmissionFromColor = this.UseEmissionFromColor;
			bool flag3 = this.borderEmission == SpriteToParticles.BorderEmission.Fast || this.borderEmission == SpriteToParticles.BorderEmission.Precise;
			if (flag3)
			{
				bool flag4 = false;
				Color color = spriteColorsData[0];
				int num9 = (int)num;
				bool flag5 = this.borderEmission == SpriteToParticles.BorderEmission.Precise;
				int num10 = 0;
				while ((float)num10 < num5)
				{
					Color color2 = spriteColorsData[num10];
					bool flag6 = color2.a > 0f;
					if (!flag5)
					{
						goto IL_33C;
					}
					int num11 = num10 - num9;
					if (num11 <= 0)
					{
						goto IL_33C;
					}
					Color color3 = spriteColorsData[num11];
					bool flag7 = color3.a > 0f;
					if (flag6)
					{
						if (flag7)
						{
							goto IL_33C;
						}
						if (!useEmissionFromColor || (FloatComparer.AreEqual(r, color2.r, redTolerance) && FloatComparer.AreEqual(g, color2.g, greenTolerance) && FloatComparer.AreEqual(b, color2.b, blueTolerance)))
						{
							array[num8] = color2;
							array2[num8] = num10;
							num8++;
							color = color2;
							flag4 = true;
						}
					}
					else
					{
						if (!flag7)
						{
							goto IL_33C;
						}
						if (!useEmissionFromColor || (FloatComparer.AreEqual(r, color3.r, redTolerance) && FloatComparer.AreEqual(g, color3.g, greenTolerance) && FloatComparer.AreEqual(b, color3.b, blueTolerance)))
						{
							array[num8] = color3;
							array2[num8] = num11;
							num8++;
							goto IL_33C;
						}
					}
					IL_444:
					num10++;
					continue;
					IL_33C:
					if (!flag6 && flag4)
					{
						if (useEmissionFromColor && (!FloatComparer.AreEqual(r, color.r, redTolerance) || !FloatComparer.AreEqual(g, color.g, greenTolerance) || !FloatComparer.AreEqual(b, color.b, blueTolerance)))
						{
							goto IL_444;
						}
						array[num8] = color;
						array2[num8] = num10 - 1;
						num8++;
						flag4 = true;
					}
					color = color2;
					if (!flag6)
					{
						flag4 = false;
						goto IL_444;
					}
					if (flag3 && (!flag6 || flag4))
					{
						goto IL_444;
					}
					if (useEmissionFromColor && (!FloatComparer.AreEqual(r, color2.r, redTolerance) || !FloatComparer.AreEqual(g, color2.g, greenTolerance) || !FloatComparer.AreEqual(b, color2.b, blueTolerance)))
					{
						goto IL_444;
					}
					array[num8] = color2;
					array2[num8] = num10;
					num8++;
					flag4 = true;
					goto IL_444;
				}
			}
			else
			{
				int num12 = 0;
				while ((float)num12 < num5)
				{
					Color color4 = spriteColorsData[num12];
					if (color4.a > 0f)
					{
						if (!useEmissionFromColor || (FloatComparer.AreEqual(r, color4.r, redTolerance) && FloatComparer.AreEqual(g, color4.g, greenTolerance) && FloatComparer.AreEqual(b, color4.b, blueTolerance)))
						{
							array[num8] = color4;
							array2[num8] = num12;
							num8++;
						}
					}
					num12++;
				}
			}
			if (num8 <= 0)
			{
				return;
			}
			Vector3 zero = Vector3.zero;
			Vector3 b2 = vector;
			if (this.renderSystemType == RenderSystemUsing.SpriteRenderer)
			{
				for (int i = 0; i < emitCount; i++)
				{
					int num13 = UnityEngine.Random.Range(0, num8 - 1);
					int num14 = array2[num13];
					if (this.useBetweenFramesPrecision)
					{
						float t = UnityEngine.Random.Range(0f, 1f);
						b2 = Vector3.Lerp(this.lastTransformPosition, vector, t);
					}
					float num15 = (float)num14 % num / pixelsPerUnit - num3 + num7;
					float num16 = (float)(num14 / num6) / pixelsPerUnit - num4 + num7;
					if (flag)
					{
						num15 = num / pixelsPerUnit - num15 - num3 * 2f;
					}
					if (flag2)
					{
						num16 = num2 / pixelsPerUnit - num16 - num4 * 2f;
					}
					zero.x = num15 * vector2.x;
					zero.y = num16 * vector2.y;
					ParticleSystem.EmitParams emitParams = default(ParticleSystem.EmitParams);
					emitParams.position = rotation * zero + b2;
					if (this.UsePixelSourceColor)
					{
						emitParams.startColor = array[num13];
					}
					emitParams.startSize = startSize;
					this.particlesSystem.Emit(emitParams, 1);
				}
			}
			else
			{
				for (int j = 0; j < emitCount; j++)
				{
					int num17 = UnityEngine.Random.Range(0, num8 - 1);
					int num18 = array2[num17];
					float num19 = (float)num18 % num / pixelsPerUnit - num3 + num7;
					float num20 = (float)(num18 / num6) / pixelsPerUnit - num4 + num7;
					ParticleSystem.EmitParams emitParams2 = default(ParticleSystem.EmitParams);
					zero.x = num19 * this.wMult + this.offsetXY.x;
					zero.y = num20 * this.hMult - this.offsetXY.y;
					emitParams2.position = zero;
					if (this.UsePixelSourceColor)
					{
						emitParams2.startColor = array[num17];
					}
					emitParams2.startSize = startSize;
					this.particlesSystem.Emit(emitParams2, 1);
				}
			}
		}

		// Token: 0x0600214D RID: 8525 RVA: 0x000B68A8 File Offset: 0x000B4CA8
		private void EmitAllDynamic()
		{
			Sprite sprite = this.GetSprite();
			if (!sprite)
			{
				return;
			}
			float r = this.EmitFromColor.r;
			float g = this.EmitFromColor.g;
			float b = this.EmitFromColor.b;
			float pixelsPerUnit = sprite.pixelsPerUnit;
			float num = (float)((int)sprite.rect.size.x);
			float num2 = (float)((int)sprite.rect.size.y);
			float startSize = this.GetParticleStartSize(pixelsPerUnit);
			float num3 = sprite.pivot.x / pixelsPerUnit;
			float num4 = sprite.pivot.y / pixelsPerUnit;
			Color[] spriteColorsData = this.GetSpriteColorsData(sprite);
			float redTolerance = this.RedTolerance;
			float greenTolerance = this.GreenTolerance;
			float blueTolerance = this.BlueTolerance;
			float num5 = num * num2;
			int num6 = (int)num;
			float num7 = 1f / pixelsPerUnit / 2f;
			Vector3 vector = Vector3.zero;
			Quaternion rotation = Quaternion.identity;
			Vector3 vector2 = Vector3.one;
			bool flag = false;
			bool flag2 = false;
			if (this.renderSystemType == RenderSystemUsing.SpriteRenderer && this.SimulationSpace != ParticleSystemSimulationSpace.Local)
			{
				vector = this.spriteTransformReference.position;
				rotation = this.spriteTransformReference.rotation;
				vector2 = this.spriteTransformReference.lossyScale;
				flag = this.spriteRenderer.flipX;
				flag2 = this.spriteRenderer.flipY;
			}
			Vector3 zero = Vector3.zero;
			Vector3 b2 = vector;
			if (this.renderSystemType == RenderSystemUsing.SpriteRenderer)
			{
				int num8 = 0;
				while ((float)num8 < num5)
				{
					Color c = spriteColorsData[num8];
					if (c.a > 0f)
					{
						if (!this.UseEmissionFromColor || (FloatComparer.AreEqual(r, c.r, redTolerance) && FloatComparer.AreEqual(g, c.g, greenTolerance) && FloatComparer.AreEqual(b, c.b, blueTolerance)))
						{
							float num9 = (float)num8 % num / pixelsPerUnit - num3;
							float num10 = (float)(num8 / num6) / pixelsPerUnit - num4;
							if (flag)
							{
								num9 = num / pixelsPerUnit - num9 - num3 * 2f;
							}
							if (flag2)
							{
								num10 = num2 / pixelsPerUnit - num10 - num4 * 2f;
							}
							zero.x = num9 * vector2.x + num7;
							zero.y = num10 * vector2.y + num7;
							ParticleSystem.EmitParams emitParams = default(ParticleSystem.EmitParams);
							emitParams.position = rotation * zero + b2;
							if (this.UsePixelSourceColor)
							{
								emitParams.startColor = c;
							}
							emitParams.startSize = startSize;
							this.particlesSystem.Emit(emitParams, 1);
						}
					}
					num8++;
				}
			}
			else
			{
				int num11 = 0;
				while ((float)num11 < num5)
				{
					Color c2 = spriteColorsData[num11];
					if (c2.a > 0f)
					{
						if (!this.UseEmissionFromColor || (FloatComparer.AreEqual(r, c2.r, redTolerance) && FloatComparer.AreEqual(g, c2.g, greenTolerance) && FloatComparer.AreEqual(b, c2.b, blueTolerance)))
						{
							float num12 = (float)num11 % num / pixelsPerUnit - num3;
							float num13 = (float)(num11 / num6) / pixelsPerUnit - num4;
							ParticleSystem.EmitParams emitParams2 = default(ParticleSystem.EmitParams);
							zero.x = num12 * this.wMult + this.offsetXY.x + num7;
							zero.y = num13 * this.hMult - this.offsetXY.y + num7;
							emitParams2.position = zero;
							if (this.UsePixelSourceColor)
							{
								emitParams2.startColor = c2;
							}
							emitParams2.startSize = startSize;
							this.particlesSystem.Emit(emitParams2, 1);
						}
					}
					num11++;
				}
			}
		}

		// Token: 0x0600214E RID: 8526 RVA: 0x000B6CA0 File Offset: 0x000B50A0
		public void CacheSprite(bool relativeToParent = false)
		{
			if (!this.particlesSystem)
			{
				return;
			}
			this.hasCachingEnded = false;
			this.particlesCacheCount = 0;
			Sprite sprite = this.GetSprite();
			if (!sprite)
			{
				return;
			}
			float r = this.EmitFromColor.r;
			float g = this.EmitFromColor.g;
			float b = this.EmitFromColor.b;
			float pixelsPerUnit = sprite.pixelsPerUnit;
			float num = (float)((int)sprite.rect.size.x);
			float num2 = (float)((int)sprite.rect.size.y);
			int num3 = (int)num;
			float num4 = 1f / pixelsPerUnit / 2f;
			this.particleStartSize = this.GetParticleStartSize(pixelsPerUnit);
			float num5 = sprite.pivot.x / pixelsPerUnit;
			float num6 = sprite.pivot.y / pixelsPerUnit;
			Color[] spriteColorsData = this.GetSpriteColorsData(sprite);
			float redTolerance = this.RedTolerance;
			float greenTolerance = this.GreenTolerance;
			float blueTolerance = this.BlueTolerance;
			float num7 = num * num2;
			Vector3 b2 = Vector3.zero;
			Quaternion rotation = Quaternion.identity;
			Vector3 vector = Vector3.one;
			bool flag = false;
			bool flag2 = false;
			if (this.renderSystemType == RenderSystemUsing.SpriteRenderer)
			{
				b2 = this.spriteTransformReference.position;
				rotation = this.spriteTransformReference.rotation;
				vector = this.spriteTransformReference.lossyScale;
				flag = this.spriteRenderer.flipX;
				flag2 = this.spriteRenderer.flipY;
			}
			List<Color> list = new List<Color>();
			List<Vector3> list2 = new List<Vector3>();
			if (this.renderSystemType == RenderSystemUsing.SpriteRenderer)
			{
				int num8 = 0;
				while ((float)num8 < num7)
				{
					Color item = spriteColorsData[num8];
					if (item.a > 0f)
					{
						if (!this.UseEmissionFromColor || (FloatComparer.AreEqual(r, item.r, redTolerance) && FloatComparer.AreEqual(g, item.g, greenTolerance) && FloatComparer.AreEqual(b, item.b, blueTolerance)))
						{
							float num9 = (float)num8 % num / pixelsPerUnit - num5 + num4;
							float num10 = (float)(num8 / num3) / pixelsPerUnit - num6 + num4;
							if (flag)
							{
								num9 = num / pixelsPerUnit - num9 - num5 * 2f;
							}
							if (flag2)
							{
								num10 = num2 / pixelsPerUnit - num10 - num6 * 2f;
							}
							Vector3 item2;
							if (relativeToParent)
							{
								item2 = rotation * new Vector3(num9 * vector.x, num10 * vector.y, 0f) + b2;
							}
							else
							{
								item2 = new Vector3(num9, num10, 0f);
							}
							list2.Add(item2);
							list.Add(item);
							this.particlesCacheCount++;
						}
					}
					num8++;
				}
			}
			else
			{
				int num11 = 0;
				while ((float)num11 < num7)
				{
					Color item3 = spriteColorsData[num11];
					if (item3.a > 0f)
					{
						if (!this.UseEmissionFromColor || (FloatComparer.AreEqual(r, item3.r, redTolerance) && FloatComparer.AreEqual(g, item3.g, greenTolerance) && FloatComparer.AreEqual(b, item3.b, blueTolerance)))
						{
							float x = (float)num11 % num / pixelsPerUnit - num5 + num4;
							float y = (float)(num11 / num3) / pixelsPerUnit - num6 + num4;
							Vector3 item4 = new Vector3(x, y, 0f);
							list2.Add(item4);
							list.Add(item3);
							this.particlesCacheCount++;
						}
					}
					num11++;
				}
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
			if (this.OnAvailableToPlay != null)
			{
				this.OnAvailableToPlay();
			}
		}

		// Token: 0x0600214F RID: 8527 RVA: 0x000B70E4 File Offset: 0x000B54E4
		private void ProcessPositionAndScaleStatic()
		{
			if (this.matchTargetGOPostionData)
			{
				this.currentRectTransform.position = new Vector3(this.targetRectTransform.position.x, this.targetRectTransform.position.y, this.targetRectTransform.position.z);
			}
			this.currentRectTransform.pivot = this.targetRectTransform.pivot;
			if (this.matchTargetGOPostionData)
			{
				this.currentRectTransform.anchoredPosition = this.targetRectTransform.anchoredPosition;
				this.currentRectTransform.anchorMin = this.targetRectTransform.anchorMin;
				this.currentRectTransform.anchorMax = this.targetRectTransform.anchorMax;
				this.currentRectTransform.offsetMin = this.targetRectTransform.offsetMin;
				this.currentRectTransform.offsetMax = this.targetRectTransform.offsetMax;
			}
			if (this.matchTargetGOScale)
			{
				this.currentRectTransform.localScale = this.targetRectTransform.localScale;
			}
			this.currentRectTransform.rotation = this.targetRectTransform.rotation;
			this.currentRectTransform.sizeDelta = new Vector2(this.targetRectTransform.rect.width, this.targetRectTransform.rect.height);
			float x = (1f - this.currentRectTransform.pivot.x) * this.currentRectTransform.rect.width - this.currentRectTransform.rect.width / 2f;
			float y = (1f - this.currentRectTransform.pivot.y) * -this.currentRectTransform.rect.height + this.currentRectTransform.rect.height / 2f;
			this.offsetXY = new Vector2(x, y);
			Sprite sprite = this.GetSprite();
			if (!sprite)
			{
				return;
			}
			this.wMult = sprite.pixelsPerUnit * (this.currentRectTransform.rect.width / sprite.rect.size.x);
			this.hMult = sprite.pixelsPerUnit * (this.currentRectTransform.rect.height / sprite.rect.size.y);
		}

		// Token: 0x06002150 RID: 8528 RVA: 0x000B7378 File Offset: 0x000B5778
		private void EmitStatic(int emitCount)
		{
			if (!this.hasCachingEnded)
			{
				return;
			}
			int max = this.particlesCacheCount;
			float startSize = this.particleStartSize;
			bool flag = this.renderSystemType == RenderSystemUsing.SpriteRenderer;
			if (this.particlesCacheCount <= 0)
			{
				return;
			}
			Vector3 position;
			Quaternion rotation;
			Vector3 lossyScale;
			if (flag)
			{
				position = this.spriteTransformReference.position;
				rotation = this.spriteTransformReference.rotation;
				lossyScale = this.spriteTransformReference.lossyScale;
			}
			else
			{
				position = this.currentRectTransform.position;
				rotation = this.currentRectTransform.rotation;
				lossyScale = this.currentRectTransform.lossyScale;
			}
			Vector3 b = position;
			ParticleSystemSimulationSpace simulationSpace = this.SimulationSpace;
			Color[] array = this.particleInitColorCache;
			Vector3[] array2 = this.particleInitPositionsCache;
			Vector3 zero = Vector3.zero;
			for (int i = 0; i < emitCount; i++)
			{
				int num = UnityEngine.Random.Range(0, max);
				if (this.useBetweenFramesPrecision)
				{
					float t = UnityEngine.Random.Range(0f, 1f);
					b = Vector3.Lerp(this.lastTransformPosition, position, t);
				}
				ParticleSystem.EmitParams emitParams = default(ParticleSystem.EmitParams);
				if (this.UsePixelSourceColor)
				{
					emitParams.startColor = array[num];
				}
				emitParams.startSize = startSize;
				Vector3 vector = array2[num];
				if (simulationSpace == ParticleSystemSimulationSpace.World)
				{
					if (flag)
					{
						zero.x = vector.x * lossyScale.x;
						zero.y = vector.y * lossyScale.y;
					}
					else
					{
						zero.x = vector.x * this.wMult * lossyScale.x + this.offsetXY.x;
						zero.y = vector.y * this.hMult * lossyScale.y - this.offsetXY.y;
					}
					emitParams.position = rotation * zero + b;
					this.particlesSystem.Emit(emitParams, 1);
				}
				else
				{
					if (flag)
					{
						emitParams.position = array2[num];
					}
					else
					{
						zero.x = vector.x * this.wMult + this.offsetXY.x;
						zero.y = vector.y * this.hMult - this.offsetXY.y;
						emitParams.position = zero;
					}
					this.particlesSystem.Emit(emitParams, 1);
				}
			}
		}

		// Token: 0x06002151 RID: 8529 RVA: 0x000B75F4 File Offset: 0x000B59F4
		private void EmitAllStatic()
		{
			if (!this.hasCachingEnded)
			{
				return;
			}
			int num = this.particlesCacheCount;
			float startSize = this.particleStartSize;
			bool flag = this.renderSystemType == RenderSystemUsing.SpriteRenderer;
			if (this.particlesCacheCount <= 0)
			{
				return;
			}
			Vector3 position;
			Quaternion rotation;
			Vector3 lossyScale;
			if (flag)
			{
				position = this.spriteTransformReference.position;
				rotation = this.spriteTransformReference.rotation;
				lossyScale = this.spriteTransformReference.lossyScale;
			}
			else
			{
				position = this.currentRectTransform.position;
				rotation = this.currentRectTransform.rotation;
				lossyScale = this.currentRectTransform.lossyScale;
			}
			Vector3 b = position;
			ParticleSystemSimulationSpace simulationSpace = this.SimulationSpace;
			Color[] array = this.particleInitColorCache;
			Vector3[] array2 = this.particleInitPositionsCache;
			Vector3 zero = Vector3.zero;
			for (int i = 0; i < num; i++)
			{
				if (this.useBetweenFramesPrecision)
				{
					float t = UnityEngine.Random.Range(0f, 1f);
					b = Vector3.Lerp(this.lastTransformPosition, position, t);
				}
				ParticleSystem.EmitParams emitParams = default(ParticleSystem.EmitParams);
				if (this.UsePixelSourceColor)
				{
					emitParams.startColor = array[i];
				}
				emitParams.startSize = startSize;
				Vector3 vector = array2[i];
				if (simulationSpace == ParticleSystemSimulationSpace.World)
				{
					if (flag)
					{
						zero.x = vector.x * lossyScale.x;
						zero.y = vector.y * lossyScale.y;
					}
					else
					{
						zero.x = vector.x * this.wMult * lossyScale.x + this.offsetXY.x;
						zero.y = vector.y * this.hMult * lossyScale.y - this.offsetXY.y;
					}
					emitParams.position = rotation * zero + b;
					this.particlesSystem.Emit(emitParams, 1);
				}
				else
				{
					if (flag)
					{
						emitParams.position = array2[i];
					}
					else
					{
						zero.x = vector.x * this.wMult + this.offsetXY.x;
						zero.y = vector.y * this.hMult - this.offsetXY.y;
						emitParams.position = zero;
					}
					this.particlesSystem.Emit(emitParams, 1);
				}
			}
		}

		// Token: 0x06002152 RID: 8530 RVA: 0x000B7868 File Offset: 0x000B5C68
		public Sprite GetSprite()
		{
			Sprite sprite;
			if (this.renderSystemType == RenderSystemUsing.ImageRenderer)
			{
				if (!this.imageRenderer)
				{
					if (this.verboseDebug)
					{
					}
					return null;
				}
				sprite = this.imageRenderer.sprite;
				if (this.imageRenderer.overrideSprite)
				{
					sprite = this.imageRenderer.overrideSprite;
				}
			}
			else
			{
				if (!this.spriteRenderer)
				{
					if (this.verboseDebug)
					{
					}
					return null;
				}
				sprite = this.spriteRenderer.sprite;
			}
			if (!sprite)
			{
				if (this.verboseDebug)
				{
				}
				this.isPlaying = false;
				return null;
			}
			return sprite;
		}

		// Token: 0x06002153 RID: 8531 RVA: 0x000B791C File Offset: 0x000B5D1C
		private float GetParticleStartSize(float PixelsPerUnit)
		{
			float num;
			if (this.renderSystemType == RenderSystemUsing.SpriteRenderer)
			{
				num = 1f / PixelsPerUnit;
				num *= this.mainModule.startSize.constant;
			}
			else
			{
				num = this.mainModule.startSize.constant;
			}
			return num;
		}

		// Token: 0x06002154 RID: 8532 RVA: 0x000B796C File Offset: 0x000B5D6C
		private Color[] GetSpriteColorsData(Sprite sprite)
		{
			Rect rect = sprite.rect;
			Color[] array;
			if (this.useSpritesSharingPool && Application.isPlaying)
			{
				array = SpritesDataPool.GetSpriteColors(sprite, (int)rect.position.x, (int)rect.position.y, (int)rect.size.x, (int)rect.size.y);
			}
			else if (this.CacheSprites && this.mode == SpriteMode.Dynamic)
			{
				if (this.spritesSoFar.ContainsKey(sprite))
				{
					array = this.spritesSoFar[sprite];
				}
				else
				{
					array = sprite.texture.GetPixels((int)rect.position.x, (int)rect.position.y, (int)rect.size.x, (int)rect.size.y);
					this.spritesSoFar.Add(sprite, array);
				}
			}
			else
			{
				array = sprite.texture.GetPixels((int)rect.position.x, (int)rect.position.y, (int)rect.size.x, (int)rect.size.y);
			}
			return array;
		}

		// Token: 0x06002155 RID: 8533 RVA: 0x000B7ACF File Offset: 0x000B5ECF
		private void HackUnityCrash2017()
		{
			if (this.forceHack && !this.particlesSystem.isStopped)
			{
				this.particlesSystem.Emit(1);
				this.particlesSystem.Clear();
				this.forceHack = false;
			}
		}

		// Token: 0x06002156 RID: 8534 RVA: 0x000B7B0A File Offset: 0x000B5F0A
		private void ForceNextUseOfHack()
		{
			this.forceHack = true;
		}

		// Token: 0x040020A5 RID: 8357
		[Tooltip("Weather the system is being used for Sprite or Image component. ")]
		public RenderSystemUsing renderSystemType;

		// Token: 0x040020A6 RID: 8358
		[Tooltip("Weather the system is using static or dynamic mode.")]
		public SpriteMode mode = SpriteMode.Static;

		// Token: 0x040020A7 RID: 8359
		[Tooltip("Should log warnings and errors?")]
		public bool verboseDebug;

		// Token: 0x040020A8 RID: 8360
		[Tooltip("If none is provided the script will look for one in this game object.")]
		public SpriteRenderer spriteRenderer;

		// Token: 0x040020A9 RID: 8361
		[Tooltip("Must be provided by other GameObject's ImageRenderer.")]
		public Image imageRenderer;

		// Token: 0x040020AA RID: 8362
		[Tooltip("If none is provided the script will look for one in this game object.")]
		public ParticleSystem particlesSystem;

		// Token: 0x040020AB RID: 8363
		[Tooltip("Start emitting as soon as able. (On static emission activating this will force CacheOnAwake)")]
		public bool PlayOnAwake = true;

		// Token: 0x040020AC RID: 8364
		[Tooltip("Particles to emit per second")]
		public float EmissionRate = 1000f;

		// Token: 0x040020AD RID: 8365
		[Tooltip("Should new particles override ParticleSystem's startColor and use the color in the pixel they're emitting from?")]
		public bool UsePixelSourceColor;

		// Token: 0x040020AE RID: 8366
		[Tooltip("Emit from sprite border. Fast will work on the x axis only. Precise works on both x and y axis but is more performance heavy. (Border emission only works in dynamic mode currently)")]
		public SpriteToParticles.BorderEmission borderEmission;

		// Token: 0x040020AF RID: 8367
		[Tooltip("Activating this will make the Emitter only emit from selected color")]
		public bool UseEmissionFromColor;

		// Token: 0x040020B0 RID: 8368
		[Tooltip("Emission will take this color as only source position")]
		public Color EmitFromColor;

		// Token: 0x040020B1 RID: 8369
		[Range(0.01f, 1f)]
		[Tooltip("In conjunction with EmitFromColor. Defines how much can it deviate from red spectrum for selected color.")]
		public float RedTolerance = 0.05f;

		// Token: 0x040020B2 RID: 8370
		[Range(0f, 1f)]
		[Tooltip("In conjunction with EmitFromColor. Defines how much can it deviate from green spectrum for selected color.")]
		public float GreenTolerance = 0.05f;

		// Token: 0x040020B3 RID: 8371
		[Range(0f, 1f)]
		[Tooltip("In conjunction with EmitFromColor. Defines how much can it deviate from blue spectrum for selected color.")]
		public float BlueTolerance = 0.05f;

		// Token: 0x040020B4 RID: 8372
		[Tooltip("This will save memory size when dealing with same sprite being loaded repeatedly by different GameObjects.")]
		public bool useSpritesSharingPool;

		// Token: 0x040020B5 RID: 8373
		[Tooltip("Weather use BetweenFrames precision or not. (Refer to manual for further explanation)")]
		public bool useBetweenFramesPrecision;

		// Token: 0x040020B6 RID: 8374
		[Tooltip("Should the system cache sprites data? (Refer to manual for further explanation)")]
		public bool CacheSprites = true;

		// Token: 0x040020B7 RID: 8375
		[Tooltip("Should the transform match target Renderer GameObject Position? (For Image Component(UI) StP Object must have same parent as the Renderer Image component Transform)")]
		[FormerlySerializedAs("matchImageRendererPostionData")]
		public bool matchTargetGOPostionData;

		// Token: 0x040020B8 RID: 8376
		[Tooltip("Should the transform match target Renderer Renderer Scale? (For Image Component(UI) StP Object must have same parent as the Image component Transform. For Sprite Component it will match local scale data)")]
		[FormerlySerializedAs("matchImageRendererScale")]
		public bool matchTargetGOScale;

		// Token: 0x040020B9 RID: 8377
		private ParticleSystemSimulationSpace SimulationSpace;

		// Token: 0x040020BA RID: 8378
		private bool isPlaying;

		// Token: 0x040020BB RID: 8379
		public UIParticleRenderer uiParticleSystem;

		// Token: 0x040020BC RID: 8380
		private ParticleSystem.MainModule mainModule;

		// Token: 0x040020BD RID: 8381
		private float ParticlesToEmitThisFrame;

		// Token: 0x040020BE RID: 8382
		private Vector3 lastTransformPosition;

		// Token: 0x040020BF RID: 8383
		private Transform spriteTransformReference;

		// Token: 0x040020C0 RID: 8384
		private Color[] colorCache = new Color[1];

		// Token: 0x040020C1 RID: 8385
		private int[] indexCache = new int[1];

		// Token: 0x040020C2 RID: 8386
		private Dictionary<Sprite, Color[]> spritesSoFar = new Dictionary<Sprite, Color[]>();

		// Token: 0x040020C3 RID: 8387
		private RectTransform targetRectTransform;

		// Token: 0x040020C4 RID: 8388
		private RectTransform currentRectTransform;

		// Token: 0x040020C5 RID: 8389
		private Vector2 offsetXY;

		// Token: 0x040020C6 RID: 8390
		private float wMult = 100f;

		// Token: 0x040020C7 RID: 8391
		private float hMult = 100f;

		// Token: 0x040020C8 RID: 8392
		[Tooltip("Should the system cache on Awake method? - Static emission needs to be cached first, if this property is not checked the CacheSprite() method should be called by code. (Refer to manual for further explanation)")]
		public bool CacheOnAwake = true;

		// Token: 0x040020C9 RID: 8393
		private bool hasCachingEnded;

		// Token: 0x040020CA RID: 8394
		private int particlesCacheCount;

		// Token: 0x040020CB RID: 8395
		private float particleStartSize;

		// Token: 0x040020CC RID: 8396
		private Vector3[] particleInitPositionsCache;

		// Token: 0x040020CD RID: 8397
		private Color[] particleInitColorCache;

		// Token: 0x040020D0 RID: 8400
		private bool forceHack = true;

		// Token: 0x0200059C RID: 1436
		public enum BorderEmission
		{
			// Token: 0x040020D2 RID: 8402
			Off,
			// Token: 0x040020D3 RID: 8403
			Fast,
			// Token: 0x040020D4 RID: 8404
			Precise
		}
	}
}
