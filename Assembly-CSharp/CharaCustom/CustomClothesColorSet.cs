using System;
using System.Collections.Generic;
using AIChara;
using Illusion.Extensions;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace CharaCustom
{
	// Token: 0x02000998 RID: 2456
	public class CustomClothesColorSet : MonoBehaviour
	{
		// Token: 0x17000DB9 RID: 3513
		// (get) Token: 0x06004693 RID: 18067 RVA: 0x001B21E8 File Offset: 0x001B05E8
		private CustomBase customBase
		{
			get
			{
				return Singleton<CustomBase>.Instance;
			}
		}

		// Token: 0x17000DBA RID: 3514
		// (get) Token: 0x06004694 RID: 18068 RVA: 0x001B21EF File Offset: 0x001B05EF
		private ChaControl chaCtrl
		{
			get
			{
				return this.customBase.chaCtrl;
			}
		}

		// Token: 0x17000DBB RID: 3515
		// (get) Token: 0x06004695 RID: 18069 RVA: 0x001B21FC File Offset: 0x001B05FC
		private ChaFileClothes nowClothes
		{
			get
			{
				return this.chaCtrl.nowCoordinate.clothes;
			}
		}

		// Token: 0x17000DBC RID: 3516
		// (get) Token: 0x06004696 RID: 18070 RVA: 0x001B220E File Offset: 0x001B060E
		private ChaFileClothes orgClothes
		{
			get
			{
				return this.chaCtrl.chaFile.coordinate.clothes;
			}
		}

		// Token: 0x17000DBD RID: 3517
		// (get) Token: 0x06004697 RID: 18071 RVA: 0x001B2225 File Offset: 0x001B0625
		// (set) Token: 0x06004698 RID: 18072 RVA: 0x001B222D File Offset: 0x001B062D
		public int parts { get; set; } = -1;

		// Token: 0x17000DBE RID: 3518
		// (get) Token: 0x06004699 RID: 18073 RVA: 0x001B2236 File Offset: 0x001B0636
		// (set) Token: 0x0600469A RID: 18074 RVA: 0x001B223E File Offset: 0x001B063E
		public int idx { get; set; } = -1;

		// Token: 0x17000DBF RID: 3519
		// (get) Token: 0x0600469B RID: 18075 RVA: 0x001B2247 File Offset: 0x001B0647
		private ChaFileClothes.PartsInfo.ColorInfo nowColorInfo
		{
			get
			{
				return this.nowClothes.parts[this.parts].colorInfo[this.idx];
			}
		}

		// Token: 0x17000DC0 RID: 3520
		// (get) Token: 0x0600469C RID: 18076 RVA: 0x001B2267 File Offset: 0x001B0667
		private ChaFileClothes.PartsInfo.ColorInfo orgColorInfo
		{
			get
			{
				return this.orgClothes.parts[this.parts].colorInfo[this.idx];
			}
		}

		// Token: 0x0600469D RID: 18077 RVA: 0x001B2288 File Offset: 0x001B0688
		public void UpdateCustomUI()
		{
			if (this.parts == -1 || this.idx == -1)
			{
				return;
			}
			ChaFileClothes.PartsInfo.ColorInfo colorInfo = this.nowClothes.parts[this.parts].colorInfo[this.idx];
			this.csMainColor.SetColor(colorInfo.baseColor);
			this.ssGloss.SetSliderValue(colorInfo.glossPower);
			this.ssMetallic.SetSliderValue(colorInfo.metallicPower);
			this.ChangePatternImage();
			if (this.objPatternSet)
			{
				this.objPatternSet.SetActiveIfDifferent(0 != colorInfo.pattern);
			}
			this.csPatternColor.SetColor(colorInfo.patternColor);
			this.ssPatternW.SetSliderValue(colorInfo.layout.x);
			this.ssPatternH.SetSliderValue(colorInfo.layout.y);
			this.ssPatternX.SetSliderValue(colorInfo.layout.z);
			this.ssPatternY.SetSliderValue(colorInfo.layout.w);
			this.ssPatternRot.SetSliderValue(colorInfo.rotation);
		}

		// Token: 0x0600469E RID: 18078 RVA: 0x001B23B5 File Offset: 0x001B07B5
		public void EnableColorAlpha(bool enable)
		{
			if (this.csMainColor)
			{
				this.csMainColor.EnableColorAlpha(enable);
			}
		}

		// Token: 0x0600469F RID: 18079 RVA: 0x001B23D4 File Offset: 0x001B07D4
		public void ChangePatternImage()
		{
			ListInfoBase listInfo = this.chaCtrl.lstCtrl.GetListInfo(ChaListDefine.CategoryNo.st_pattern, this.nowClothes.parts[this.parts].colorInfo[this.idx].pattern);
			Texture2D texture2D = CommonLib.LoadAsset<Texture2D>(listInfo.GetInfo(ChaListDefine.KeyType.ThumbAB), listInfo.GetInfo(ChaListDefine.KeyType.ThumbTex), false, string.Empty);
			if (texture2D)
			{
				this.imgPattern.sprite = Sprite.Create(texture2D, new Rect(0f, 0f, (float)texture2D.width, (float)texture2D.height), new Vector2(0.5f, 0.5f));
			}
		}

		// Token: 0x060046A0 RID: 18080 RVA: 0x001B2480 File Offset: 0x001B0880
		public CustomClothesColorSet.ClothesInfo GetDefaultClothesInfo()
		{
			float[] array = new float[3];
			float[] array2 = new float[3];
			Vector4[] array3 = new Vector4[]
			{
				Vector4.zero,
				Vector4.zero,
				Vector4.zero
			};
			float[] array4 = new float[3];
			if (null != this.chaCtrl.cmpClothes[this.parts])
			{
				array[0] = this.chaCtrl.cmpClothes[this.parts].defGloss01;
				array[1] = this.chaCtrl.cmpClothes[this.parts].defGloss02;
				array[2] = this.chaCtrl.cmpClothes[this.parts].defGloss03;
				array2[0] = this.chaCtrl.cmpClothes[this.parts].defMetallic01;
				array2[1] = this.chaCtrl.cmpClothes[this.parts].defMetallic02;
				array2[2] = this.chaCtrl.cmpClothes[this.parts].defMetallic03;
				Vector4 vector;
				vector.x = Mathf.InverseLerp(20f, 1f, this.chaCtrl.cmpClothes[this.parts].defLayout01.x);
				vector.y = Mathf.InverseLerp(20f, 1f, this.chaCtrl.cmpClothes[this.parts].defLayout01.y);
				vector.z = Mathf.InverseLerp(-1f, 1f, this.chaCtrl.cmpClothes[this.parts].defLayout01.z);
				vector.w = Mathf.InverseLerp(-1f, 1f, this.chaCtrl.cmpClothes[this.parts].defLayout01.w);
				array3[0] = vector;
				vector.x = Mathf.InverseLerp(20f, 1f, this.chaCtrl.cmpClothes[this.parts].defLayout02.x);
				vector.y = Mathf.InverseLerp(20f, 1f, this.chaCtrl.cmpClothes[this.parts].defLayout02.y);
				vector.z = Mathf.InverseLerp(-1f, 1f, this.chaCtrl.cmpClothes[this.parts].defLayout02.z);
				vector.w = Mathf.InverseLerp(-1f, 1f, this.chaCtrl.cmpClothes[this.parts].defLayout02.w);
				array3[1] = vector;
				vector.x = Mathf.InverseLerp(20f, 1f, this.chaCtrl.cmpClothes[this.parts].defLayout03.x);
				vector.y = Mathf.InverseLerp(20f, 1f, this.chaCtrl.cmpClothes[this.parts].defLayout03.y);
				vector.z = Mathf.InverseLerp(-1f, 1f, this.chaCtrl.cmpClothes[this.parts].defLayout03.z);
				vector.w = Mathf.InverseLerp(-1f, 1f, this.chaCtrl.cmpClothes[this.parts].defLayout03.w);
				array3[2] = vector;
				array4[0] = Mathf.InverseLerp(-1f, 1f, this.chaCtrl.cmpClothes[this.parts].defRotation01);
				array4[1] = Mathf.InverseLerp(-1f, 1f, this.chaCtrl.cmpClothes[this.parts].defRotation02);
				array4[2] = Mathf.InverseLerp(-1f, 1f, this.chaCtrl.cmpClothes[this.parts].defRotation03);
			}
			return new CustomClothesColorSet.ClothesInfo
			{
				gloss = array[this.idx],
				metallic = array2[this.idx],
				layout = array3[this.idx],
				rot = array4[this.idx]
			};
		}

		// Token: 0x060046A1 RID: 18081 RVA: 0x001B28E0 File Offset: 0x001B0CE0
		public void Initialize(int _parts, int _idx)
		{
			this.parts = _parts;
			this.idx = _idx;
			if (this.parts == -1 || this.idx == -1)
			{
				return;
			}
			if (this.title)
			{
				this.title.text = "カラー" + (this.idx + 1).ToString("00");
			}
			if (this.lstDisposable != null && this.lstDisposable.Count != 0)
			{
				int count = this.lstDisposable.Count;
				for (int i = 0; i < count; i++)
				{
					this.lstDisposable[i].Dispose();
				}
			}
			this.csMainColor.actUpdateColor = delegate(Color color)
			{
				this.nowColorInfo.baseColor = color;
				this.orgColorInfo.baseColor = color;
				this.chaCtrl.ChangeCustomClothes(this.parts, true, false, false, false);
			};
			this.ssGloss.onChange = delegate(float value)
			{
				this.nowColorInfo.glossPower = value;
				this.orgColorInfo.glossPower = value;
				this.chaCtrl.ChangeCustomClothes(this.parts, true, false, false, false);
			};
			this.ssGloss.onSetDefaultValue = delegate()
			{
				CustomClothesColorSet.ClothesInfo defaultClothesInfo = this.GetDefaultClothesInfo();
				return defaultClothesInfo.gloss;
			};
			this.ssMetallic.onChange = delegate(float value)
			{
				this.nowColorInfo.metallicPower = value;
				this.orgColorInfo.metallicPower = value;
				this.chaCtrl.ChangeCustomClothes(this.parts, true, false, false, false);
			};
			this.ssMetallic.onSetDefaultValue = delegate()
			{
				CustomClothesColorSet.ClothesInfo defaultClothesInfo = this.GetDefaultClothesInfo();
				return defaultClothesInfo.metallic;
			};
			IDisposable item = this.btnPatternWin.OnClickAsObservable().Subscribe(delegate(Unit _)
			{
				this.customBase.customCtrl.showPattern = true;
				this.clothesPtnSel.ChangeLink(0, this.parts, this.idx);
				this.clothesPtnSel.onSelect = delegate(int p, int i)
				{
					this.ChangePatternImage();
					if (this.objPatternSet)
					{
						this.objPatternSet.SetActiveIfDifferent(0 != this.nowColorInfo.pattern);
					}
				};
			});
			this.lstDisposable.Add(item);
			this.csPatternColor.actUpdateColor = delegate(Color color)
			{
				this.nowColorInfo.patternColor = color;
				this.orgColorInfo.patternColor = color;
				this.chaCtrl.ChangeCustomClothes(this.parts, true, false, false, false);
			};
			this.ssPatternW.onChange = delegate(float value)
			{
				this.nowColorInfo.layout = new Vector4(value, this.nowColorInfo.layout.y, this.nowColorInfo.layout.z, this.nowColorInfo.layout.w);
				this.orgColorInfo.layout = this.nowColorInfo.layout;
				this.chaCtrl.ChangeCustomClothes(this.parts, true, false, false, false);
			};
			this.ssPatternW.onSetDefaultValue = delegate()
			{
				CustomClothesColorSet.ClothesInfo defaultClothesInfo = this.GetDefaultClothesInfo();
				return defaultClothesInfo.layout.x;
			};
			this.ssPatternH.onChange = delegate(float value)
			{
				this.nowColorInfo.layout = new Vector4(this.nowColorInfo.layout.x, value, this.nowColorInfo.layout.z, this.nowColorInfo.layout.w);
				this.orgColorInfo.layout = this.nowColorInfo.layout;
				this.chaCtrl.ChangeCustomClothes(this.parts, true, false, false, false);
			};
			this.ssPatternH.onSetDefaultValue = delegate()
			{
				CustomClothesColorSet.ClothesInfo defaultClothesInfo = this.GetDefaultClothesInfo();
				return defaultClothesInfo.layout.y;
			};
			this.ssPatternX.onChange = delegate(float value)
			{
				this.nowColorInfo.layout = new Vector4(this.nowColorInfo.layout.x, this.nowColorInfo.layout.y, value, this.nowColorInfo.layout.w);
				this.orgColorInfo.layout = this.nowColorInfo.layout;
				this.chaCtrl.ChangeCustomClothes(this.parts, true, false, false, false);
			};
			this.ssPatternX.onSetDefaultValue = delegate()
			{
				CustomClothesColorSet.ClothesInfo defaultClothesInfo = this.GetDefaultClothesInfo();
				return defaultClothesInfo.layout.z;
			};
			this.ssPatternY.onChange = delegate(float value)
			{
				this.nowColorInfo.layout = new Vector4(this.nowColorInfo.layout.x, this.nowColorInfo.layout.y, this.nowColorInfo.layout.z, value);
				this.orgColorInfo.layout = this.nowColorInfo.layout;
				this.chaCtrl.ChangeCustomClothes(this.parts, true, false, false, false);
			};
			this.ssPatternY.onSetDefaultValue = delegate()
			{
				CustomClothesColorSet.ClothesInfo defaultClothesInfo = this.GetDefaultClothesInfo();
				return defaultClothesInfo.layout.w;
			};
			this.ssPatternRot.onChange = delegate(float value)
			{
				this.nowColorInfo.rotation = value;
				this.orgColorInfo.rotation = value;
				this.chaCtrl.ChangeCustomClothes(this.parts, true, false, false, false);
			};
			this.ssPatternRot.onSetDefaultValue = delegate()
			{
				CustomClothesColorSet.ClothesInfo defaultClothesInfo = this.GetDefaultClothesInfo();
				return defaultClothesInfo.rot;
			};
			this.UpdateCustomUI();
			this.ssGloss.SetInputTextValue(CustomBase.ConvertTextFromRate(0, 100, this.nowColorInfo.glossPower));
			this.ssMetallic.SetInputTextValue(CustomBase.ConvertTextFromRate(0, 100, this.nowColorInfo.metallicPower));
			this.ssPatternW.SetInputTextValue(CustomBase.ConvertTextFromRate(0, 100, this.nowColorInfo.layout.x));
			this.ssPatternH.SetInputTextValue(CustomBase.ConvertTextFromRate(0, 100, this.nowColorInfo.layout.y));
			this.ssPatternX.SetInputTextValue(CustomBase.ConvertTextFromRate(0, 100, this.nowColorInfo.layout.z));
			this.ssPatternY.SetInputTextValue(CustomBase.ConvertTextFromRate(0, 100, this.nowColorInfo.layout.w));
			this.ssPatternRot.SetInputTextValue(CustomBase.ConvertTextFromRate(0, 100, this.nowColorInfo.rotation));
		}

		// Token: 0x040041C5 RID: 16837
		[SerializeField]
		private Text title;

		// Token: 0x040041C6 RID: 16838
		[SerializeField]
		private CustomColorSet csMainColor;

		// Token: 0x040041C7 RID: 16839
		[SerializeField]
		private CustomSliderSet ssGloss;

		// Token: 0x040041C8 RID: 16840
		[SerializeField]
		private CustomSliderSet ssMetallic;

		// Token: 0x040041C9 RID: 16841
		[SerializeField]
		private CustomClothesPatternSelect clothesPtnSel;

		// Token: 0x040041CA RID: 16842
		[SerializeField]
		private GameObject objPatternSet;

		// Token: 0x040041CB RID: 16843
		[SerializeField]
		private Button btnPatternWin;

		// Token: 0x040041CC RID: 16844
		[SerializeField]
		private Image imgPattern;

		// Token: 0x040041CD RID: 16845
		[SerializeField]
		private CustomColorSet csPatternColor;

		// Token: 0x040041CE RID: 16846
		[SerializeField]
		private CustomSliderSet ssPatternW;

		// Token: 0x040041CF RID: 16847
		[SerializeField]
		private CustomSliderSet ssPatternH;

		// Token: 0x040041D0 RID: 16848
		[SerializeField]
		private CustomSliderSet ssPatternX;

		// Token: 0x040041D1 RID: 16849
		[SerializeField]
		private CustomSliderSet ssPatternY;

		// Token: 0x040041D2 RID: 16850
		[SerializeField]
		private CustomSliderSet ssPatternRot;

		// Token: 0x040041D5 RID: 16853
		private List<IDisposable> lstDisposable = new List<IDisposable>();

		// Token: 0x02000999 RID: 2457
		public class ClothesInfo
		{
			// Token: 0x040041D6 RID: 16854
			public float gloss;

			// Token: 0x040041D7 RID: 16855
			public float metallic;

			// Token: 0x040041D8 RID: 16856
			public Vector4 layout = Vector4.zero;

			// Token: 0x040041D9 RID: 16857
			public float rot;
		}
	}
}
