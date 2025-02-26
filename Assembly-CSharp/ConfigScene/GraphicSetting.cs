using System;
using System.Collections.Generic;
using System.Linq;
using Manager;
using UnityEngine;
using UnityEngine.UI;

namespace ConfigScene
{
	// Token: 0x02000861 RID: 2145
	public class GraphicSetting : BaseSetting
	{
		// Token: 0x060036BB RID: 14011 RVA: 0x00143E20 File Offset: 0x00142220
		public GraphicSetting()
		{
			bool[][] array = new bool[4][];
			int num = 0;
			bool[] array2 = new bool[8];
			array2[0] = true;
			array[num] = array2;
			array[1] = new bool[]
			{
				true,
				false,
				true,
				true,
				true,
				true,
				false,
				false
			};
			array[2] = new bool[]
			{
				true,
				true,
				true,
				true,
				true,
				true,
				true,
				false
			};
			array[3] = new bool[]
			{
				true,
				true,
				true,
				true,
				true,
				true,
				true,
				true
			};
			this.effectEnables = array;
			this.easySettingInfo = new Dictionary<int, List<bool>>();
			base..ctor();
		}

		// Token: 0x060036BC RID: 14012 RVA: 0x00143E9C File Offset: 0x0014229C
		public override void Init()
		{
			for (int j = 0; j < 4; j++)
			{
				this.easySettingInfo.Add(j + 1, this.effectEnables[j].ToList<bool>());
			}
			GraphicSystem data = Config.GraphicData;
			base.LinkToggle(this.selfShadowToggle, delegate(bool isOn)
			{
				int qualityLevel = QualitySettings.GetQualityLevel() / 2 * 2 + ((!isOn) ? 1 : 0);
				QualitySettings.SetQualityLevel(qualityLevel);
				data.SelfShadow = isOn;
				this.SetEasySlider();
			});
			base.LinkToggle(this.depthOfFieldToggle, delegate(bool isOn)
			{
				data.DepthOfField = isOn;
				this.SetEasySlider();
			});
			base.LinkToggle(this.bloomToggle, delegate(bool isOn)
			{
				data.Bloom = isOn;
				this.SetEasySlider();
			});
			base.LinkToggle(this.atmosphereToggle, delegate(bool isOn)
			{
				data.Atmospheric = isOn;
				this.SetEasySlider();
			});
			base.LinkToggle(this.ssaoToggle, delegate(bool isOn)
			{
				data.SSAO = isOn;
				this.SetEasySlider();
			});
			base.LinkToggle(this.vignetteToggle, delegate(bool isOn)
			{
				data.Vignette = isOn;
				this.SetEasySlider();
			});
			base.LinkToggle(this.ssrToggle, delegate(bool isOn)
			{
				data.SSR = isOn;
				this.SetEasySlider();
			});
			base.LinkToggle(this.rainToggle, delegate(bool isOn)
			{
				data.Rain = isOn;
				this.SetEasySlider();
			});
			this.qualitySlider.onValueChanged.AddListener(delegate(float value)
			{
				this.SetNumberColor((int)value);
				if (!this.ChangeSlider)
				{
					this.ChangeSlider = true;
					return;
				}
				switch ((int)value)
				{
				case 1:
					data.SelfShadow = true;
					data.DepthOfField = false;
					data.Bloom = false;
					data.Atmospheric = false;
					data.SSAO = false;
					data.Vignette = false;
					data.SSR = false;
					data.Rain = false;
					break;
				case 2:
					data.SelfShadow = true;
					data.DepthOfField = false;
					data.Bloom = true;
					data.Atmospheric = true;
					data.SSAO = true;
					data.Vignette = true;
					data.SSR = false;
					data.Rain = false;
					break;
				case 3:
					data.SelfShadow = true;
					data.DepthOfField = true;
					data.Bloom = true;
					data.Atmospheric = true;
					data.SSAO = true;
					data.Vignette = true;
					data.SSR = true;
					data.Rain = false;
					break;
				case 4:
					data.SelfShadow = true;
					data.DepthOfField = true;
					data.Bloom = true;
					data.Atmospheric = true;
					data.SSAO = true;
					data.Vignette = true;
					data.SSR = true;
					data.Rain = true;
					break;
				}
				this.UIPresenter();
			});
			base.LinkToggleArray(this.charaLevalToggles, delegate(int i)
			{
				data.CharaGraphicQuality = (byte)i;
			});
			base.LinkToggleArray(this.mapLevalToggles, delegate(int i)
			{
				data.MapGraphicQuality = (byte)i;
			});
			base.LinkToggleArray(this.faceLightToggles, delegate(int i)
			{
				data.FaceLight = (i == 0);
			});
			base.LinkToggleArray(this.ambientToggles, delegate(int i)
			{
				data.AmbientLight = (i == 0);
			});
			base.LinkToggleArray(this.shieldToggles, delegate(int i)
			{
				data.Shield = (i == 0);
			});
			this.backGroundCololr.actUpdateColor = delegate(Color c)
			{
				data.BackColor = c;
			};
			base.LinkSlider(this.charaMaxNumSlider, delegate(float value)
			{
				data.MaxCharaNum = (int)value;
			});
			base.LinkToggle(this.Entry0Toggle, delegate(bool isOn)
			{
				data.CharasEntry[0] = isOn;
			});
			base.LinkToggle(this.Entry1Toggle, delegate(bool isOn)
			{
				data.CharasEntry[1] = isOn;
			});
			base.LinkToggle(this.Entry2Toggle, delegate(bool isOn)
			{
				data.CharasEntry[2] = isOn;
			});
			base.LinkToggle(this.Entry3Toggle, delegate(bool isOn)
			{
				data.CharasEntry[3] = isOn;
			});
			this.ChangeSlider = true;
		}

		// Token: 0x060036BD RID: 14013 RVA: 0x001440D8 File Offset: 0x001424D8
		protected override void ValueToUI()
		{
			GraphicSystem data = Config.GraphicData;
			this.selfShadowToggle.isOn = data.SelfShadow;
			this.depthOfFieldToggle.isOn = data.DepthOfField;
			this.bloomToggle.isOn = data.Bloom;
			this.atmosphereToggle.isOn = data.Atmospheric;
			this.ssaoToggle.isOn = data.SSAO;
			this.vignetteToggle.isOn = data.Vignette;
			this.ssrToggle.isOn = data.SSR;
			this.rainToggle.isOn = data.Rain;
			this.SetEasySlider();
			base.SetToggleUIArray(this.charaLevalToggles, delegate(Toggle tgl, int index)
			{
				tgl.isOn = (index == (int)data.CharaGraphicQuality);
			});
			base.SetToggleUIArray(this.mapLevalToggles, delegate(Toggle tgl, int index)
			{
				tgl.isOn = (index == (int)data.MapGraphicQuality);
			});
			base.SetToggleUIArray(this.faceLightToggles, delegate(Toggle tgl, int index)
			{
				tgl.isOn = ((index != 0) ? (!data.FaceLight) : data.FaceLight);
			});
			base.SetToggleUIArray(this.ambientToggles, delegate(Toggle tgl, int index)
			{
				tgl.isOn = ((index != 0) ? (!data.AmbientLight) : data.AmbientLight);
			});
			base.SetToggleUIArray(this.shieldToggles, delegate(Toggle tgl, int index)
			{
				tgl.isOn = ((index != 0) ? (!data.Shield) : data.Shield);
			});
			this.backGroundCololr.SetColor(data.BackColor);
			this.charaMaxNumSlider.value = (float)data.MaxCharaNum;
			this.Entry0Toggle.isOn = data.CharasEntry[0];
			this.Entry1Toggle.isOn = data.CharasEntry[1];
			this.Entry2Toggle.isOn = data.CharasEntry[2];
			this.Entry3Toggle.isOn = data.CharasEntry[3];
		}

		// Token: 0x060036BE RID: 14014 RVA: 0x001442B4 File Offset: 0x001426B4
		private void SetEasySlider()
		{
			GraphicSystem graphicData = Config.GraphicData;
			bool[] array = new bool[]
			{
				graphicData.SelfShadow,
				graphicData.DepthOfField,
				graphicData.Bloom,
				graphicData.Atmospheric,
				graphicData.SSAO,
				graphicData.Vignette,
				graphicData.SSR,
				graphicData.Rain
			};
			List<bool> list = new List<bool>();
			int num = array.Length;
			foreach (KeyValuePair<int, List<bool>> keyValuePair in this.easySettingInfo)
			{
				int num2 = 0;
				for (int i = 0; i < num; i++)
				{
					if (keyValuePair.Value[i] && array[i] == keyValuePair.Value[i])
					{
						num2++;
					}
				}
				list.Add(num2 == keyValuePair.Value.Count((bool b) => b));
			}
			int num3 = list.FindLastIndex((bool v) => v) + 1;
			if (this.qualitySlider.value != (float)num3)
			{
				this.ChangeSlider = false;
				this.qualitySlider.value = (float)num3;
			}
		}

		// Token: 0x060036BF RID: 14015 RVA: 0x00144438 File Offset: 0x00142838
		private void SetNumberColor(int _value)
		{
			for (int i = 0; i < this.qualityNumberText.Length; i++)
			{
				Text text = this.qualityNumberText[i];
				text.color = ((_value != i + 1) ? this.qualitySelectColor[1] : this.qualitySelectColor[0]);
			}
		}

		// Token: 0x060036C0 RID: 14016 RVA: 0x0014449D File Offset: 0x0014289D
		public void EntryInteractable(bool _interactable)
		{
			this.Entry0Toggle.interactable = _interactable;
			this.Entry1Toggle.interactable = _interactable;
			this.Entry2Toggle.interactable = _interactable;
			this.Entry3Toggle.interactable = _interactable;
		}

		// Token: 0x04003732 RID: 14130
		[Header("描画レベルスライダー")]
		[SerializeField]
		private Slider qualitySlider;

		// Token: 0x04003733 RID: 14131
		[Header("描画レベルスライダー数字")]
		[SerializeField]
		private Text[] qualityNumberText;

		// Token: 0x04003734 RID: 14132
		[Header("数字の色")]
		[SerializeField]
		private Color[] qualitySelectColor;

		// Token: 0x04003735 RID: 14133
		[Header("セルフシャドウ")]
		[SerializeField]
		private Toggle selfShadowToggle;

		// Token: 0x04003736 RID: 14134
		[Header("被写界深度")]
		[SerializeField]
		private Toggle depthOfFieldToggle;

		// Token: 0x04003737 RID: 14135
		[Header("ブルーム")]
		[SerializeField]
		private Toggle bloomToggle;

		// Token: 0x04003738 RID: 14136
		[Header("大気表現")]
		[SerializeField]
		private Toggle atmosphereToggle;

		// Token: 0x04003739 RID: 14137
		[Header("SSAO")]
		[SerializeField]
		private Toggle ssaoToggle;

		// Token: 0x0400373A RID: 14138
		[Header("ビグネット")]
		[SerializeField]
		private Toggle vignetteToggle;

		// Token: 0x0400373B RID: 14139
		[Header("SSR")]
		[SerializeField]
		private Toggle ssrToggle;

		// Token: 0x0400373C RID: 14140
		[Header("雨の描画")]
		[SerializeField]
		private Toggle rainToggle;

		// Token: 0x0400373D RID: 14141
		[Header("キャラ解像度")]
		[SerializeField]
		private Toggle[] charaLevalToggles;

		// Token: 0x0400373E RID: 14142
		[Header("マップ解像度")]
		[SerializeField]
		private Toggle[] mapLevalToggles;

		// Token: 0x0400373F RID: 14143
		[Header("フェイスライト")]
		[SerializeField]
		private Toggle[] faceLightToggles;

		// Token: 0x04003740 RID: 14144
		[Header("環境ライト")]
		[SerializeField]
		private Toggle[] ambientToggles;

		// Token: 0x04003741 RID: 14145
		[Header("遮蔽")]
		[SerializeField]
		private Toggle[] shieldToggles;

		// Token: 0x04003742 RID: 14146
		[Header("背景色")]
		[SerializeField]
		private UI_SampleColor backGroundCololr;

		// Token: 0x04003743 RID: 14147
		[Header("登場人数制限")]
		[SerializeField]
		private Slider charaMaxNumSlider;

		// Token: 0x04003744 RID: 14148
		[Header("登場制限")]
		[SerializeField]
		private Toggle Entry0Toggle;

		// Token: 0x04003745 RID: 14149
		[SerializeField]
		private Toggle Entry1Toggle;

		// Token: 0x04003746 RID: 14150
		[SerializeField]
		private Toggle Entry2Toggle;

		// Token: 0x04003747 RID: 14151
		[SerializeField]
		private Toggle Entry3Toggle;

		// Token: 0x04003748 RID: 14152
		private bool ChangeSlider = true;

		// Token: 0x04003749 RID: 14153
		private bool[][] effectEnables;

		// Token: 0x0400374A RID: 14154
		private Dictionary<int, List<bool>> easySettingInfo;
	}
}
