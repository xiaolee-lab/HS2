using System;
using System.Linq;
using System.Runtime.CompilerServices;
using Illusion.Extensions;
using UnityEngine;

namespace Studio
{
	// Token: 0x0200125A RID: 4698
	[AddComponentMenu("Studio/ItemComponent")]
	public class ItemComponent : MonoBehaviour
	{
		// Token: 0x17002114 RID: 8468
		// (get) Token: 0x06009AEB RID: 39659 RVA: 0x003F8882 File Offset: 0x003F6C82
		public bool check
		{
			get
			{
				bool result;
				if (!this.rendererInfos.IsNullOrEmpty<ItemComponent.RendererInfo>())
				{
					result = this.rendererInfos.Any((ItemComponent.RendererInfo _ri) => _ri.IsNormal || _ri.IsAlpha);
				}
				else
				{
					result = false;
				}
				return result;
			}
		}

		// Token: 0x17002115 RID: 8469
		// (get) Token: 0x06009AEC RID: 39660 RVA: 0x003F88BF File Offset: 0x003F6CBF
		public bool checkAlpha
		{
			get
			{
				bool result;
				if (!this.rendererInfos.IsNullOrEmpty<ItemComponent.RendererInfo>())
				{
					result = this.rendererInfos.Any((ItemComponent.RendererInfo _ri) => _ri.IsAlpha);
				}
				else
				{
					result = false;
				}
				return result;
			}
		}

		// Token: 0x17002116 RID: 8470
		// (get) Token: 0x06009AED RID: 39661 RVA: 0x003F88FC File Offset: 0x003F6CFC
		public bool checkGlass
		{
			get
			{
				bool result;
				if (!this.rendererInfos.IsNullOrEmpty<ItemComponent.RendererInfo>())
				{
					result = this.rendererInfos.Any((ItemComponent.RendererInfo _ri) => _ri.IsGlass);
				}
				else
				{
					result = false;
				}
				return result;
			}
		}

		// Token: 0x17002117 RID: 8471
		// (get) Token: 0x06009AEE RID: 39662 RVA: 0x003F8939 File Offset: 0x003F6D39
		public bool checkEmissionColor
		{
			[CompilerGenerated]
			get
			{
				return this.HasEmissionColor();
			}
		}

		// Token: 0x17002118 RID: 8472
		// (get) Token: 0x06009AEF RID: 39663 RVA: 0x003F8941 File Offset: 0x003F6D41
		public bool checkEmissionStrength
		{
			[CompilerGenerated]
			get
			{
				return this.HasEmissionStrength();
			}
		}

		// Token: 0x17002119 RID: 8473
		// (get) Token: 0x06009AF0 RID: 39664 RVA: 0x003F8949 File Offset: 0x003F6D49
		public bool CheckEmission
		{
			get
			{
				bool result;
				if (!this.rendererInfos.IsNullOrEmpty<ItemComponent.RendererInfo>())
				{
					result = this.rendererInfos.Any((ItemComponent.RendererInfo _ri) => _ri.IsEmission);
				}
				else
				{
					result = false;
				}
				return result;
			}
		}

		// Token: 0x1700211A RID: 8474
		// (get) Token: 0x06009AF1 RID: 39665 RVA: 0x003F8986 File Offset: 0x003F6D86
		public bool checkLightCancel
		{
			[CompilerGenerated]
			get
			{
				return this.rendererInfos.Any(delegate(ItemComponent.RendererInfo _ri)
				{
					bool result;
					if (_ri.IsNormal || _ri.IsAlpha)
					{
						result = _ri.renderer.materials.Any((Material _m) => _m.HasProperty(ItemShader._LightCancel));
					}
					else
					{
						result = false;
					}
					return result;
				});
			}
		}

		// Token: 0x1700211B RID: 8475
		// (get) Token: 0x06009AF2 RID: 39666 RVA: 0x003F89B0 File Offset: 0x003F6DB0
		public bool CheckOption
		{
			[CompilerGenerated]
			get
			{
				return !this.optionInfos.IsNullOrEmpty<ItemComponent.OptionInfo>();
			}
		}

		// Token: 0x1700211C RID: 8476
		// (get) Token: 0x06009AF3 RID: 39667 RVA: 0x003F89C0 File Offset: 0x003F6DC0
		public bool CheckAnimePattern
		{
			[CompilerGenerated]
			get
			{
				bool result;
				if (!this.animeInfos.IsNullOrEmpty<ItemComponent.AnimeInfo>())
				{
					result = this.animeInfos.Any((ItemComponent.AnimeInfo _info) => _info.Check);
				}
				else
				{
					result = false;
				}
				return result;
			}
		}

		// Token: 0x1700211D RID: 8477
		// (get) Token: 0x06009AF4 RID: 39668 RVA: 0x003F89FD File Offset: 0x003F6DFD
		public Color[] defColorMain
		{
			get
			{
				return (from i in this.info
				select i.defColor).ToArray<Color>();
			}
		}

		// Token: 0x1700211E RID: 8478
		// (get) Token: 0x06009AF5 RID: 39669 RVA: 0x003F8A2C File Offset: 0x003F6E2C
		public Color[] defColorPattern
		{
			get
			{
				return (from i in this.info
				select i.defColorPattern).ToArray<Color>();
			}
		}

		// Token: 0x1700211F RID: 8479
		// (get) Token: 0x06009AF6 RID: 39670 RVA: 0x003F8A5C File Offset: 0x003F6E5C
		public bool[] useColor
		{
			get
			{
				if (!this.rendererInfos.IsNullOrEmpty<ItemComponent.RendererInfo>())
				{
					if (this.rendererInfos.Any((ItemComponent.RendererInfo _ri) => _ri.IsColor))
					{
						bool[] array = new bool[3];
						array[0] = this.rendererInfos.Any((ItemComponent.RendererInfo _ri) => _ri.IsColor1);
						array[1] = this.rendererInfos.Any((ItemComponent.RendererInfo _ri) => _ri.IsColor2);
						array[2] = this.rendererInfos.Any((ItemComponent.RendererInfo _ri) => _ri.IsColor3);
						return array;
					}
				}
				return Enumerable.Repeat<bool>(false, 3).ToArray<bool>();
			}
		}

		// Token: 0x17002120 RID: 8480
		// (get) Token: 0x06009AF7 RID: 39671 RVA: 0x003F8B3A File Offset: 0x003F6F3A
		public bool[] useMetallic
		{
			get
			{
				return (from i in this.info
				select i.useMetallic).ToArray<bool>();
			}
		}

		// Token: 0x17002121 RID: 8481
		// (get) Token: 0x06009AF8 RID: 39672 RVA: 0x003F8B6C File Offset: 0x003F6F6C
		public bool[] usePattern
		{
			get
			{
				if (!this.rendererInfos.IsNullOrEmpty<ItemComponent.RendererInfo>())
				{
					if (this.rendererInfos.Any((ItemComponent.RendererInfo _ri) => _ri.IsPattern))
					{
						bool[] array = new bool[3];
						array[0] = this.rendererInfos.Any((ItemComponent.RendererInfo _ri) => _ri.IsPattern1);
						array[1] = this.rendererInfos.Any((ItemComponent.RendererInfo _ri) => _ri.IsPattern2);
						array[2] = this.rendererInfos.Any((ItemComponent.RendererInfo _ri) => _ri.IsPattern3);
						return array;
					}
				}
				return Enumerable.Repeat<bool>(false, 3).ToArray<bool>();
			}
		}

		// Token: 0x17002122 RID: 8482
		// (get) Token: 0x06009AF9 RID: 39673 RVA: 0x003F8C4A File Offset: 0x003F704A
		public Color DefEmissionColor
		{
			[CompilerGenerated]
			get
			{
				return new Color(this.defEmissionColor.r, this.defEmissionColor.g, this.defEmissionColor.b);
			}
		}

		// Token: 0x17002123 RID: 8483
		public ItemComponent.Info this[int _idx]
		{
			get
			{
				return this.info.SafeGet(_idx);
			}
		}

		// Token: 0x06009AFB RID: 39675 RVA: 0x003F8C80 File Offset: 0x003F7080
		public void SetupRendererInfo()
		{
			Renderer[] componentsInChildren = base.GetComponentsInChildren<Renderer>();
			if (componentsInChildren.IsNullOrEmpty<Renderer>())
			{
				return;
			}
			this.rendererInfos = (from _r in componentsInChildren
			select new ItemComponent.RendererInfo
			{
				renderer = _r
			}).ToArray<ItemComponent.RendererInfo>();
			foreach (ItemComponent.RendererInfo rendererInfo in this.rendererInfos)
			{
				Material[] sharedMaterials = rendererInfo.renderer.sharedMaterials;
				rendererInfo.materials = new ItemComponent.MaterialInfo[sharedMaterials.Length];
				for (int j = 0; j < sharedMaterials.Length; j++)
				{
					rendererInfo.materials[j] = new ItemComponent.MaterialInfo();
				}
			}
		}

		// Token: 0x06009AFC RID: 39676 RVA: 0x003F8D30 File Offset: 0x003F7130
		public void UpdateColor(OIItemInfo _info)
		{
			foreach (ItemComponent.RendererInfo rendererInfo in this.rendererInfos)
			{
				if (rendererInfo.IsNormal)
				{
					for (int j = 0; j < 3; j++)
					{
						ColorInfo colorInfo = _info.colors[j];
						Material[] materials = rendererInfo.renderer.materials;
						for (int k = 0; k < materials.Length; k++)
						{
							ItemComponent.MaterialInfo materialInfo = rendererInfo.materials.SafeGet(k);
							if (materialInfo != null)
							{
								if (j != 0)
								{
									if (j != 1)
									{
										if (j == 2)
										{
											if (materialInfo.isColor3)
											{
												materials[k].SetColor(ItemShader._Color3, colorInfo.mainColor);
												if (this.info[j].useMetallic)
												{
													materials[k].SetFloat(ItemShader._Metallic3, colorInfo.metallic);
													materials[k].SetFloat(ItemShader._Glossiness3, colorInfo.glossiness);
												}
												if (materialInfo.isPattern3)
												{
													materials[k].SetColor(ItemShader._Color3_2, colorInfo.pattern.color);
													materials[k].SetVector(ItemShader._patternuv3, colorInfo.pattern.uv);
													materials[k].SetFloat(ItemShader._patternuv3Rotator, colorInfo.pattern.rot);
													materials[k].SetFloat(ItemShader._patternclamp3, (!colorInfo.pattern.clamp) ? 0f : 1f);
												}
											}
										}
									}
									else if (materialInfo.isColor2)
									{
										materials[k].SetColor(ItemShader._Color2, colorInfo.mainColor);
										if (this.info[j].useMetallic)
										{
											materials[k].SetFloat(ItemShader._Metallic2, colorInfo.metallic);
											materials[k].SetFloat(ItemShader._Glossiness2, colorInfo.glossiness);
										}
										if (materialInfo.isPattern2)
										{
											materials[k].SetColor(ItemShader._Color2_2, colorInfo.pattern.color);
											materials[k].SetVector(ItemShader._patternuv2, colorInfo.pattern.uv);
											materials[k].SetFloat(ItemShader._patternuv2Rotator, colorInfo.pattern.rot);
											materials[k].SetFloat(ItemShader._patternclamp2, (!colorInfo.pattern.clamp) ? 0f : 1f);
										}
									}
								}
								else if (materialInfo.isColor1)
								{
									materials[k].SetColor(ItemShader._Color, colorInfo.mainColor);
									if (this.info[j].useMetallic)
									{
										materials[k].SetFloat(ItemShader._Metallic, colorInfo.metallic);
										materials[k].SetFloat(ItemShader._Glossiness, colorInfo.glossiness);
									}
									if (materialInfo.isPattern1)
									{
										materials[k].SetColor(ItemShader._Color1_2, colorInfo.pattern.color);
										materials[k].SetVector(ItemShader._patternuv1, colorInfo.pattern.uv);
										materials[k].SetFloat(ItemShader._patternuv1Rotator, colorInfo.pattern.rot);
										materials[k].SetFloat(ItemShader._patternclamp1, (!colorInfo.pattern.clamp) ? 0f : 1f);
									}
								}
							}
						}
					}
				}
				if (rendererInfo.IsAlpha)
				{
					Material[] materials2 = rendererInfo.renderer.materials;
					for (int l = 0; l < materials2.Length; l++)
					{
						ItemComponent.MaterialInfo materialInfo2 = rendererInfo.materials.SafeGet(l);
						if (materialInfo2 != null && materialInfo2.isAlpha)
						{
							materials2[l].SetFloat(ItemShader._alpha, _info.alpha);
						}
					}
				}
				if (rendererInfo.IsNormal || rendererInfo.IsAlpha)
				{
					Material[] materials3 = rendererInfo.renderer.materials;
					for (int m = 0; m < materials3.Length; m++)
					{
						ItemComponent.MaterialInfo materialInfo3 = rendererInfo.materials.SafeGet(m);
						if (materialInfo3 != null && materialInfo3.isEmission)
						{
							if (materials3[m].HasProperty(ItemShader._EmissionColor))
							{
								materials3[m].SetColor(ItemShader._EmissionColor, _info.emissionColor);
							}
							if (materials3[m].HasProperty(ItemShader._EmissionStrength))
							{
								materials3[m].SetFloat(ItemShader._EmissionStrength, _info.emissionPower);
							}
							if (materials3[m].HasProperty(ItemShader._LightCancel))
							{
								materials3[m].SetFloat(ItemShader._LightCancel, _info.lightCancel);
							}
						}
					}
				}
				if (rendererInfo.IsGlass)
				{
					Material[] materials4 = rendererInfo.renderer.materials;
					for (int n = 0; n < materials4.Length; n++)
					{
						ItemComponent.MaterialInfo materialInfo4 = rendererInfo.materials.SafeGet(n);
						if (materialInfo4 != null && materialInfo4.isGlass)
						{
							ColorInfo colorInfo2 = _info.colors[3];
							if (materials4[n].HasProperty(ItemShader._Color4))
							{
								materials4[n].SetColor(ItemShader._Color4, colorInfo2.mainColor);
							}
							else if (materials4[n].HasProperty(ItemShader._Color))
							{
								materials4[n].SetColor(ItemShader._Color, colorInfo2.mainColor);
							}
							materials4[n].SetColor(ItemShader._Metallic4, colorInfo2.mainColor);
							materials4[n].SetColor(ItemShader._Glossiness4, colorInfo2.mainColor);
						}
					}
				}
			}
		}

		// Token: 0x06009AFD RID: 39677 RVA: 0x003F92F4 File Offset: 0x003F76F4
		public void SetPatternTex(int _idx, Texture2D _texture)
		{
			int[] array = new int[]
			{
				ItemShader._PatternMask1,
				ItemShader._PatternMask2,
				ItemShader._PatternMask3
			};
			foreach (ItemComponent.RendererInfo rendererInfo in from v in this.rendererInfos
			where v.IsNormal
			select v)
			{
				Material[] materials = rendererInfo.renderer.materials;
				for (int i = 0; i < materials.Length; i++)
				{
					ItemComponent.MaterialInfo materialInfo = rendererInfo.materials.SafeGet(i);
					if (materialInfo != null)
					{
						if (materialInfo.CheckPattern(_idx))
						{
							materials[i].SetTexture(array[_idx], _texture);
						}
					}
				}
			}
		}

		// Token: 0x06009AFE RID: 39678 RVA: 0x003F93E0 File Offset: 0x003F77E0
		public void SetOptionVisible(bool _value)
		{
			if (this.optionInfos.IsNullOrEmpty<ItemComponent.OptionInfo>())
			{
				return;
			}
			foreach (ItemComponent.OptionInfo optionInfo in this.optionInfos)
			{
				optionInfo.Visible = _value;
			}
		}

		// Token: 0x06009AFF RID: 39679 RVA: 0x003F9424 File Offset: 0x003F7824
		public void SetOptionVisible(int _idx, bool _value)
		{
			this.optionInfos.SafeProc(_idx, delegate(ItemComponent.OptionInfo _info)
			{
				_info.Visible = _value;
			});
		}

		// Token: 0x06009B00 RID: 39680 RVA: 0x003F9458 File Offset: 0x003F7858
		public void SetColor()
		{
			bool[] array = Enumerable.Repeat<bool>(false, 7).ToArray<bool>();
			foreach (ItemComponent.RendererInfo rendererInfo in from r in this.rendererInfos
			where r.renderer != null && !r.materials.IsNullOrEmpty<ItemComponent.MaterialInfo>()
			where r.materials.Any((ItemComponent.MaterialInfo _m) => _m.isColor1)
			select r)
			{
				if (!array.Take(3).All((bool _b) => _b))
				{
					foreach (Tuple<ItemComponent.MaterialInfo, int> tuple in rendererInfo.materials.Select((ItemComponent.MaterialInfo _m, int index) => new Tuple<ItemComponent.MaterialInfo, int>(_m, index)).Where((Tuple<ItemComponent.MaterialInfo, int> v) => v.Item1.isColor1))
					{
						Material material = rendererInfo.renderer.sharedMaterials.SafeGet(tuple.Item2);
						if (!(material == null))
						{
							if (!array[0] && material.HasProperty("_Color"))
							{
								this.info[0].defColor = material.GetColor("_Color");
								array[0] = true;
							}
							if (!array[1] && material.HasProperty("_Metallic"))
							{
								this.info[0].defMetallic = material.GetFloat("_Metallic");
								array[1] = true;
							}
							if (!array[2] && material.HasProperty("_Glossiness"))
							{
								this.info[0].defGlossiness = material.GetFloat("_Glossiness");
								array[2] = true;
							}
							if (array.Take(3).All((bool _b) => _b))
							{
								break;
							}
						}
					}
				}
				if (!array.Skip(3).All((bool _b) => _b))
				{
					foreach (Tuple<ItemComponent.MaterialInfo, int> tuple2 in rendererInfo.materials.Select((ItemComponent.MaterialInfo _m, int index) => new Tuple<ItemComponent.MaterialInfo, int>(_m, index)).Where((Tuple<ItemComponent.MaterialInfo, int> v) => v.Item1.isColor1 && v.Item1.isPattern1))
					{
						Material material2 = rendererInfo.renderer.sharedMaterials.SafeGet(tuple2.Item2);
						if (!(material2 == null))
						{
							if (!array[3] && material2.HasProperty("_Color1_2"))
							{
								this.info[0].defColorPattern = material2.GetColor("_Color1_2");
								array[3] = true;
							}
							if (!array[4] && material2.HasProperty("_patternuv1"))
							{
								this.info[0].defUV = material2.GetVector("_patternuv1");
								array[4] = true;
							}
							if (!array[5] && material2.HasProperty("_patternuv1Rotator"))
							{
								this.info[0].defRot = material2.GetFloat("_patternuv1Rotator");
								array[5] = true;
							}
							if (!array[6] && material2.HasProperty("_patternclamp1"))
							{
								this.info[0].defClamp = (material2.GetFloat("_patternclamp1") != 0f);
								array[6] = true;
							}
							if (array.Skip(3).All((bool _b) => _b))
							{
								break;
							}
						}
					}
				}
				if (array.All((bool _b) => _b))
				{
					break;
				}
			}
			bool[] array2 = Enumerable.Repeat<bool>(false, 7).ToArray<bool>();
			foreach (ItemComponent.RendererInfo rendererInfo2 in from r in this.rendererInfos
			where r.renderer != null && !r.materials.IsNullOrEmpty<ItemComponent.MaterialInfo>()
			where r.materials.Any((ItemComponent.MaterialInfo _m) => _m.isColor2)
			select r)
			{
				if (!array2.Take(3).All((bool _b) => _b))
				{
					foreach (Tuple<ItemComponent.MaterialInfo, int> tuple3 in rendererInfo2.materials.Select((ItemComponent.MaterialInfo _m, int index) => new Tuple<ItemComponent.MaterialInfo, int>(_m, index)).Where((Tuple<ItemComponent.MaterialInfo, int> v) => v.Item1.isColor2))
					{
						Material material3 = rendererInfo2.renderer.sharedMaterials.SafeGet(tuple3.Item2);
						if (!(material3 == null))
						{
							if (!array2[0] && material3.HasProperty("_Color2"))
							{
								this.info[1].defColor = material3.GetColor("_Color2");
								array2[0] = true;
							}
							if (!array2[1] && material3.HasProperty("_Metallic2"))
							{
								this.info[1].defMetallic = material3.GetFloat("_Metallic2");
								array2[1] = true;
							}
							if (!array2[2] && material3.HasProperty("_Glossiness2"))
							{
								this.info[1].defGlossiness = material3.GetFloat("_Glossiness2");
								array2[2] = true;
							}
							if (array2.Take(3).All((bool _b) => _b))
							{
								break;
							}
						}
					}
				}
				if (!array2.Skip(3).All((bool _b) => _b))
				{
					foreach (Tuple<ItemComponent.MaterialInfo, int> tuple4 in rendererInfo2.materials.Select((ItemComponent.MaterialInfo _m, int index) => new Tuple<ItemComponent.MaterialInfo, int>(_m, index)).Where((Tuple<ItemComponent.MaterialInfo, int> v) => v.Item1.isColor2 && v.Item1.isPattern2))
					{
						Material material4 = rendererInfo2.renderer.sharedMaterials.SafeGet(tuple4.Item2);
						if (!(material4 == null))
						{
							if (!array2[3] && material4.HasProperty("_Color2_2"))
							{
								this.info[1].defColorPattern = material4.GetColor("_Color2_2");
								array2[3] = true;
							}
							if (!array2[4] && material4.HasProperty("_patternuv2"))
							{
								this.info[1].defUV = material4.GetVector("_patternuv2");
								array2[4] = true;
							}
							if (!array2[5] && material4.HasProperty("_patternuv2Rotator"))
							{
								this.info[1].defRot = material4.GetFloat("_patternuv2Rotator");
								array2[5] = true;
							}
							if (!array2[6] && material4.HasProperty("_patternclamp2"))
							{
								this.info[1].defClamp = (material4.GetFloat("_patternclamp2") != 0f);
								array2[6] = true;
							}
							if (array2.Skip(3).All((bool _b) => _b))
							{
								break;
							}
						}
					}
				}
				if (array2.All((bool _b) => _b))
				{
					break;
				}
			}
			bool[] array3 = Enumerable.Repeat<bool>(false, 7).ToArray<bool>();
			foreach (ItemComponent.RendererInfo rendererInfo3 in from r in this.rendererInfos
			where r.renderer != null && !r.materials.IsNullOrEmpty<ItemComponent.MaterialInfo>()
			where r.materials.Any((ItemComponent.MaterialInfo _m) => _m.isColor3)
			select r)
			{
				if (!array3.Take(3).All((bool _b) => _b))
				{
					foreach (Tuple<ItemComponent.MaterialInfo, int> tuple5 in rendererInfo3.materials.Select((ItemComponent.MaterialInfo _m, int index) => new Tuple<ItemComponent.MaterialInfo, int>(_m, index)).Where((Tuple<ItemComponent.MaterialInfo, int> v) => v.Item1.isColor3))
					{
						Material material5 = rendererInfo3.renderer.sharedMaterials.SafeGet(tuple5.Item2);
						if (!(material5 == null))
						{
							if (!array3[0] && material5.HasProperty("_Color3"))
							{
								this.info[2].defColor = material5.GetColor("_Color3");
								array3[0] = true;
							}
							if (!array3[1] && material5.HasProperty("_Metallic3"))
							{
								this.info[2].defMetallic = material5.GetFloat("_Metallic3");
								array3[1] = true;
							}
							if (!array3[2] && material5.HasProperty("_Glossiness3"))
							{
								this.info[2].defGlossiness = material5.GetFloat("_Glossiness3");
								array3[2] = true;
							}
							if (array3.Take(3).All((bool _b) => _b))
							{
								break;
							}
						}
					}
				}
				if (!array3.Skip(3).All((bool _b) => _b))
				{
					foreach (Tuple<ItemComponent.MaterialInfo, int> tuple6 in rendererInfo3.materials.Select((ItemComponent.MaterialInfo _m, int index) => new Tuple<ItemComponent.MaterialInfo, int>(_m, index)).Where((Tuple<ItemComponent.MaterialInfo, int> v) => v.Item1.isColor3 && v.Item1.isPattern3))
					{
						Material material6 = rendererInfo3.renderer.sharedMaterials.SafeGet(tuple6.Item2);
						if (!(material6 == null))
						{
							if (!array3[3] && material6.HasProperty("_Color3_2"))
							{
								this.info[2].defColorPattern = material6.GetColor("_Color3_2");
								array3[3] = true;
							}
							if (!array3[4] && material6.HasProperty("_patternuv3"))
							{
								this.info[2].defUV = material6.GetVector("_patternuv3");
								array3[4] = true;
							}
							if (!array3[5] && material6.HasProperty("_patternuv3Rotator"))
							{
								this.info[2].defRot = material6.GetFloat("_patternuv3Rotator");
								array3[5] = true;
							}
							if (!array3[6] && material6.HasProperty("_patternclamp3"))
							{
								this.info[2].defClamp = (material6.GetFloat("_patternclamp3") != 0f);
								array3[6] = true;
							}
							if (array3.Skip(3).All((bool _b) => _b))
							{
								break;
							}
						}
					}
				}
				if (array3.All((bool _b) => _b))
				{
					break;
				}
			}
			ItemComponent.RendererInfo rendererInfo4 = (from r in this.rendererInfos
			where r.renderer != null && !r.materials.IsNullOrEmpty<ItemComponent.MaterialInfo>()
			select r).FirstOrDefault((ItemComponent.RendererInfo _i) => _i.materials.Any((ItemComponent.MaterialInfo _m) => _m.isAlpha));
			if (rendererInfo4 != null)
			{
				Material[] sharedMaterials = rendererInfo4.renderer.sharedMaterials;
				for (int i = 0; i < sharedMaterials.Length; i++)
				{
					ItemComponent.MaterialInfo materialInfo = rendererInfo4.materials.SafeGet(i);
					if (materialInfo != null && materialInfo.isAlpha)
					{
						if (null != sharedMaterials[i] && sharedMaterials[i].HasProperty("_alpha"))
						{
							this.alpha = sharedMaterials[i].GetFloat("_alpha");
						}
					}
				}
			}
			this.SetGlass();
			this.SetEmission();
		}

		// Token: 0x06009B01 RID: 39681 RVA: 0x003FA34C File Offset: 0x003F874C
		public void SetGlass()
		{
			if (this.rendererInfos.IsNullOrEmpty<ItemComponent.RendererInfo>())
			{
				return;
			}
			ItemComponent.RendererInfo rendererInfo = (from r in this.rendererInfos
			where r.renderer != null && !r.materials.IsNullOrEmpty<ItemComponent.MaterialInfo>()
			select r).FirstOrDefault((ItemComponent.RendererInfo _i) => _i.materials.Any((ItemComponent.MaterialInfo _m) => _m.isGlass));
			if (rendererInfo != null)
			{
				Material[] sharedMaterials = rendererInfo.renderer.sharedMaterials;
				for (int i = 0; i < sharedMaterials.Length; i++)
				{
					ItemComponent.MaterialInfo materialInfo = rendererInfo.materials.SafeGet(i);
					if (materialInfo != null && materialInfo.isGlass)
					{
						if (null != sharedMaterials[i])
						{
							if (sharedMaterials[i].HasProperty("_Color4"))
							{
								this.defGlass = sharedMaterials[i].GetColor("_Color4");
							}
							else if (sharedMaterials[i].HasProperty("_Color"))
							{
								this.defGlass = sharedMaterials[i].GetColor("_Color");
							}
						}
					}
				}
			}
		}

		// Token: 0x06009B02 RID: 39682 RVA: 0x003FA45C File Offset: 0x003F885C
		public void SetEmission()
		{
			bool[] array = new bool[3];
			foreach (ItemComponent.RendererInfo rendererInfo in from v in this.rendererInfos
			where v.materials.Any((ItemComponent.MaterialInfo m) => m.isEmission)
			select v)
			{
				foreach (Tuple<ItemComponent.MaterialInfo, int> tuple in rendererInfo.materials.Select((ItemComponent.MaterialInfo _m, int index) => new Tuple<ItemComponent.MaterialInfo, int>(_m, index)).Where((Tuple<ItemComponent.MaterialInfo, int> v) => v.Item1.isEmission))
				{
					Material material = rendererInfo.renderer.sharedMaterials[tuple.Item2];
					if (!(material == null))
					{
						if (!array[0] && material.HasProperty("_EmissionColor"))
						{
							this.defEmissionColor = material.GetColor("_EmissionColor");
							array[0] = true;
						}
						if (!array[1] && material.HasProperty("_EmissionStrength"))
						{
							this.defEmissionStrength = material.GetFloat("_EmissionStrength");
							array[1] = true;
						}
						if (!array[2] && material.HasProperty("_LightCancel"))
						{
							this.defLightCancel = material.GetFloat("_LightCancel");
							array[2] = true;
						}
						if (array.All((bool _b) => _b))
						{
							break;
						}
					}
				}
				if (array.All((bool _b) => _b))
				{
					break;
				}
			}
		}

		// Token: 0x06009B03 RID: 39683 RVA: 0x003FA688 File Offset: 0x003F8A88
		public void SetSeaRenderer()
		{
			if (this.objSeaParent == null)
			{
				return;
			}
			this.renderersSea = this.objSeaParent.GetComponentsInChildren<Renderer>();
		}

		// Token: 0x06009B04 RID: 39684 RVA: 0x003FA6B0 File Offset: 0x003F8AB0
		public void SetupSea()
		{
			if (this.renderersSea.IsNullOrEmpty<Renderer>())
			{
				return;
			}
			foreach (Renderer renderer in from v in this.renderersSea
			where v != null
			select v)
			{
				Material material = renderer.material;
				material.DisableKeyword("USINGWATERVOLUME");
				renderer.material = material;
			}
		}

		// Token: 0x06009B05 RID: 39685 RVA: 0x003FA750 File Offset: 0x003F8B50
		private bool HasProperty(Renderer[] _renderer, int _nameID)
		{
			return _renderer.Any((Renderer r) => r.materials.Any((Material m) => m.HasProperty(_nameID)));
		}

		// Token: 0x06009B06 RID: 39686 RVA: 0x003FA77C File Offset: 0x003F8B7C
		private bool HasEmissionColor()
		{
			foreach (ItemComponent.RendererInfo rendererInfo in from v in this.rendererInfos
			where v.materials.Any((ItemComponent.MaterialInfo m) => m.isEmission)
			select v)
			{
				Material[] materials = rendererInfo.renderer.materials;
				for (int i = 0; i < materials.Length; i++)
				{
					ItemComponent.MaterialInfo materialInfo = rendererInfo.materials.SafeGet(i);
					if (materialInfo != null && materialInfo.isEmission)
					{
						if (materials[i].HasProperty(ItemShader._EmissionColor))
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06009B07 RID: 39687 RVA: 0x003FA854 File Offset: 0x003F8C54
		private bool HasEmissionStrength()
		{
			foreach (ItemComponent.RendererInfo rendererInfo in from v in this.rendererInfos
			where v.materials.Any((ItemComponent.MaterialInfo m) => m.isEmission)
			select v)
			{
				Material[] materials = rendererInfo.renderer.materials;
				for (int i = 0; i < materials.Length; i++)
				{
					ItemComponent.MaterialInfo materialInfo = rendererInfo.materials.SafeGet(i);
					if (materialInfo != null && materialInfo.isEmission)
					{
						if (materials[i].HasProperty(ItemShader._EmissionStrength))
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x04007B84 RID: 31620
		[Header("レンダラー管理")]
		public ItemComponent.RendererInfo[] rendererInfos;

		// Token: 0x04007B85 RID: 31621
		[Space]
		[Header("構成情報")]
		public ItemComponent.Info[] info;

		// Token: 0x04007B86 RID: 31622
		public float alpha = 1f;

		// Token: 0x04007B87 RID: 31623
		[Header("ガラス関係")]
		public Color defGlass = Color.white;

		// Token: 0x04007B88 RID: 31624
		[Header("エミッション関係")]
		[ColorUsage(false, true)]
		public Color defEmissionColor = Color.clear;

		// Token: 0x04007B89 RID: 31625
		public float defEmissionStrength;

		// Token: 0x04007B8A RID: 31626
		public float defLightCancel;

		// Token: 0x04007B8B RID: 31627
		[Header("子の接続先")]
		public Transform childRoot;

		// Token: 0x04007B8C RID: 31628
		[Header("アニメ関係")]
		[Tooltip("アニメがあるか")]
		public bool isAnime;

		// Token: 0x04007B8D RID: 31629
		public ItemComponent.AnimeInfo[] animeInfos;

		// Token: 0x04007B8E RID: 31630
		[Header("拡縮判定")]
		public bool isScale = true;

		// Token: 0x04007B8F RID: 31631
		[Header("オプション")]
		public ItemComponent.OptionInfo[] optionInfos;

		// Token: 0x04007B90 RID: 31632
		[Header("海面関係")]
		public GameObject objSeaParent;

		// Token: 0x04007B91 RID: 31633
		public Renderer[] renderersSea;

		// Token: 0x04007B92 RID: 31634
		[Space]
		[Button("SetColor", "初期色を設定", new object[]
		{

		})]
		public int setcolor;

		// Token: 0x0200125B RID: 4699
		[Serializable]
		public class MaterialInfo
		{
			// Token: 0x06009B52 RID: 39762 RVA: 0x003FAD9B File Offset: 0x003F919B
			public bool CheckColor(int _idx)
			{
				if (_idx == 0)
				{
					return this.isColor1;
				}
				if (_idx != 1)
				{
					return _idx == 2 && this.isColor3;
				}
				return this.isColor2;
			}

			// Token: 0x06009B53 RID: 39763 RVA: 0x003FADCC File Offset: 0x003F91CC
			public bool CheckPattern(int _idx)
			{
				if (_idx == 0)
				{
					return this.isPattern1;
				}
				if (_idx != 1)
				{
					return _idx == 2 && this.isPattern3;
				}
				return this.isPattern2;
			}

			// Token: 0x04007BDC RID: 31708
			public bool isColor1;

			// Token: 0x04007BDD RID: 31709
			public bool isPattern1;

			// Token: 0x04007BDE RID: 31710
			public bool isColor2;

			// Token: 0x04007BDF RID: 31711
			public bool isPattern2;

			// Token: 0x04007BE0 RID: 31712
			public bool isColor3;

			// Token: 0x04007BE1 RID: 31713
			public bool isPattern3;

			// Token: 0x04007BE2 RID: 31714
			public bool isEmission;

			// Token: 0x04007BE3 RID: 31715
			public bool isAlpha;

			// Token: 0x04007BE4 RID: 31716
			public bool isGlass;
		}

		// Token: 0x0200125C RID: 4700
		[Serializable]
		public class RendererInfo
		{
			// Token: 0x17002124 RID: 8484
			// (get) Token: 0x06009B55 RID: 39765 RVA: 0x003FAE05 File Offset: 0x003F9205
			public bool IsNormal
			{
				[CompilerGenerated]
				get
				{
					return this.materials.Any((ItemComponent.MaterialInfo m) => m.isColor1 || m.isColor2 || m.isColor3 || m.isEmission);
				}
			}

			// Token: 0x17002125 RID: 8485
			// (get) Token: 0x06009B56 RID: 39766 RVA: 0x003FAE2F File Offset: 0x003F922F
			public bool IsAlpha
			{
				[CompilerGenerated]
				get
				{
					return this.materials.Any((ItemComponent.MaterialInfo m) => m.isAlpha);
				}
			}

			// Token: 0x17002126 RID: 8486
			// (get) Token: 0x06009B57 RID: 39767 RVA: 0x003FAE59 File Offset: 0x003F9259
			public bool IsGlass
			{
				[CompilerGenerated]
				get
				{
					return this.materials.Any((ItemComponent.MaterialInfo m) => m.isGlass);
				}
			}

			// Token: 0x17002127 RID: 8487
			// (get) Token: 0x06009B58 RID: 39768 RVA: 0x003FAE83 File Offset: 0x003F9283
			public bool IsColor
			{
				[CompilerGenerated]
				get
				{
					return this.materials.Any((ItemComponent.MaterialInfo m) => m.isColor1 || m.isColor2 || m.isColor3);
				}
			}

			// Token: 0x17002128 RID: 8488
			// (get) Token: 0x06009B59 RID: 39769 RVA: 0x003FAEAD File Offset: 0x003F92AD
			public bool IsColor1
			{
				[CompilerGenerated]
				get
				{
					return this.materials.Any((ItemComponent.MaterialInfo m) => m.isColor1);
				}
			}

			// Token: 0x17002129 RID: 8489
			// (get) Token: 0x06009B5A RID: 39770 RVA: 0x003FAED7 File Offset: 0x003F92D7
			public bool IsColor2
			{
				[CompilerGenerated]
				get
				{
					return this.materials.Any((ItemComponent.MaterialInfo m) => m.isColor2);
				}
			}

			// Token: 0x1700212A RID: 8490
			// (get) Token: 0x06009B5B RID: 39771 RVA: 0x003FAF01 File Offset: 0x003F9301
			public bool IsColor3
			{
				[CompilerGenerated]
				get
				{
					return this.materials.Any((ItemComponent.MaterialInfo m) => m.isColor3);
				}
			}

			// Token: 0x1700212B RID: 8491
			// (get) Token: 0x06009B5C RID: 39772 RVA: 0x003FAF2B File Offset: 0x003F932B
			public bool IsPattern
			{
				[CompilerGenerated]
				get
				{
					return this.materials.Any((ItemComponent.MaterialInfo m) => m.isPattern1 || m.isPattern2 || m.isPattern3);
				}
			}

			// Token: 0x1700212C RID: 8492
			// (get) Token: 0x06009B5D RID: 39773 RVA: 0x003FAF55 File Offset: 0x003F9355
			public bool IsPattern1
			{
				[CompilerGenerated]
				get
				{
					return this.materials.Any((ItemComponent.MaterialInfo m) => m.isPattern1);
				}
			}

			// Token: 0x1700212D RID: 8493
			// (get) Token: 0x06009B5E RID: 39774 RVA: 0x003FAF7F File Offset: 0x003F937F
			public bool IsPattern2
			{
				[CompilerGenerated]
				get
				{
					return this.materials.Any((ItemComponent.MaterialInfo m) => m.isPattern2);
				}
			}

			// Token: 0x1700212E RID: 8494
			// (get) Token: 0x06009B5F RID: 39775 RVA: 0x003FAFA9 File Offset: 0x003F93A9
			public bool IsPattern3
			{
				[CompilerGenerated]
				get
				{
					return this.materials.Any((ItemComponent.MaterialInfo m) => m.isPattern3);
				}
			}

			// Token: 0x1700212F RID: 8495
			// (get) Token: 0x06009B60 RID: 39776 RVA: 0x003FAFD3 File Offset: 0x003F93D3
			public bool IsEmission
			{
				[CompilerGenerated]
				get
				{
					return this.materials.Any((ItemComponent.MaterialInfo m) => m.isEmission);
				}
			}

			// Token: 0x04007BE5 RID: 31717
			public Renderer renderer;

			// Token: 0x04007BE6 RID: 31718
			public ItemComponent.MaterialInfo[] materials;
		}

		// Token: 0x0200125D RID: 4701
		[Serializable]
		public class Info
		{
			// Token: 0x17002130 RID: 8496
			// (get) Token: 0x06009B6E RID: 39790 RVA: 0x003FB0E3 File Offset: 0x003F94E3
			// (set) Token: 0x06009B6F RID: 39791 RVA: 0x003FB0F0 File Offset: 0x003F94F0
			public float ut
			{
				get
				{
					return this.defUV.z;
				}
				set
				{
					this.defUV.z = value;
				}
			}

			// Token: 0x17002131 RID: 8497
			// (get) Token: 0x06009B70 RID: 39792 RVA: 0x003FB0FE File Offset: 0x003F94FE
			// (set) Token: 0x06009B71 RID: 39793 RVA: 0x003FB10B File Offset: 0x003F950B
			public float vt
			{
				get
				{
					return this.defUV.w;
				}
				set
				{
					this.defUV.w = value;
				}
			}

			// Token: 0x17002132 RID: 8498
			// (get) Token: 0x06009B72 RID: 39794 RVA: 0x003FB119 File Offset: 0x003F9519
			// (set) Token: 0x06009B73 RID: 39795 RVA: 0x003FB126 File Offset: 0x003F9526
			public float us
			{
				get
				{
					return this.defUV.x;
				}
				set
				{
					this.defUV.x = value;
				}
			}

			// Token: 0x17002133 RID: 8499
			// (get) Token: 0x06009B74 RID: 39796 RVA: 0x003FB134 File Offset: 0x003F9534
			// (set) Token: 0x06009B75 RID: 39797 RVA: 0x003FB141 File Offset: 0x003F9541
			public float vs
			{
				get
				{
					return this.defUV.y;
				}
				set
				{
					this.defUV.y = value;
				}
			}

			// Token: 0x04007BF3 RID: 31731
			[Header("色替え")]
			public Color defColor = Color.white;

			// Token: 0x04007BF4 RID: 31732
			[Header("メタル")]
			public bool useMetallic;

			// Token: 0x04007BF5 RID: 31733
			public float defMetallic;

			// Token: 0x04007BF6 RID: 31734
			public float defGlossiness;

			// Token: 0x04007BF7 RID: 31735
			[Header("柄")]
			public Color defColorPattern = Color.white;

			// Token: 0x04007BF8 RID: 31736
			public bool defClamp = true;

			// Token: 0x04007BF9 RID: 31737
			public Vector4 defUV = Vector4.zero;

			// Token: 0x04007BFA RID: 31738
			public float defRot;
		}

		// Token: 0x0200125E RID: 4702
		[Serializable]
		public class OptionInfo
		{
			// Token: 0x17002134 RID: 8500
			// (set) Token: 0x06009B77 RID: 39799 RVA: 0x003FB158 File Offset: 0x003F9558
			public bool Visible
			{
				set
				{
					if (value)
					{
						this.SetVisible(this.objectsOff, false);
						this.SetVisible(this.objectsOn, true);
					}
					else
					{
						this.SetVisible(this.objectsOn, false);
						this.SetVisible(this.objectsOff, true);
					}
				}
			}

			// Token: 0x06009B78 RID: 39800 RVA: 0x003FB1A4 File Offset: 0x003F95A4
			private void SetVisible(GameObject[] _objects, bool _value)
			{
				foreach (GameObject self in from v in _objects
				where v != null
				select v)
				{
					self.SetActiveIfDifferent(_value);
				}
			}

			// Token: 0x04007BFB RID: 31739
			public GameObject[] objectsOn;

			// Token: 0x04007BFC RID: 31740
			public GameObject[] objectsOff;
		}

		// Token: 0x0200125F RID: 4703
		[Serializable]
		public class AnimeInfo
		{
			// Token: 0x17002135 RID: 8501
			// (get) Token: 0x06009B7B RID: 39803 RVA: 0x003FB243 File Offset: 0x003F9643
			public bool Check
			{
				[CompilerGenerated]
				get
				{
					return !this.name.IsNullOrEmpty() && !this.state.IsNullOrEmpty();
				}
			}

			// Token: 0x04007BFE RID: 31742
			public string name = string.Empty;

			// Token: 0x04007BFF RID: 31743
			public string state = string.Empty;
		}
	}
}
