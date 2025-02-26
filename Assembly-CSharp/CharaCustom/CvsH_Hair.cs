using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AIChara;
using Illusion.Extensions;
using Manager;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CharaCustom
{
	// Token: 0x020009F6 RID: 2550
	public class CvsH_Hair : CvsBase
	{
		// Token: 0x17000E4C RID: 3660
		// (get) Token: 0x06004B1F RID: 19231 RVA: 0x001CA8E2 File Offset: 0x001C8CE2
		// (set) Token: 0x06004B20 RID: 19232 RVA: 0x001CA8EA File Offset: 0x001C8CEA
		public bool allReset { get; set; }

		// Token: 0x17000E4D RID: 3661
		// (get) Token: 0x06004B21 RID: 19233 RVA: 0x001CA8F3 File Offset: 0x001C8CF3
		private bool sameSetting
		{
			get
			{
				return base.hair.sameSetting;
			}
		}

		// Token: 0x17000E4E RID: 3662
		// (get) Token: 0x06004B22 RID: 19234 RVA: 0x001CA900 File Offset: 0x001C8D00
		private bool autoSetting
		{
			get
			{
				return base.hair.autoSetting;
			}
		}

		// Token: 0x17000E4F RID: 3663
		// (get) Token: 0x06004B23 RID: 19235 RVA: 0x001CA90D File Offset: 0x001C8D0D
		private bool ctrlTogether
		{
			get
			{
				return base.hair.ctrlTogether;
			}
		}

		// Token: 0x17000E50 RID: 3664
		// (get) Token: 0x06004B24 RID: 19236 RVA: 0x001CA91A File Offset: 0x001C8D1A
		private CustomBase.CustomSettingSave.HairCtrlSetting hairCtrlSetting
		{
			get
			{
				return base.customBase.customSettingSave.hairCtrlSetting;
			}
		}

		// Token: 0x06004B25 RID: 19237 RVA: 0x001CA92C File Offset: 0x001C8D2C
		public override void ChangeMenuFunc()
		{
			base.ChangeMenuFunc();
			base.customBase.customCtrl.showColorCvs = false;
			base.customBase.customCtrl.showFileList = false;
		}

		// Token: 0x06004B26 RID: 19238 RVA: 0x001CA958 File Offset: 0x001C8D58
		public void UpdateHairList()
		{
			ChaListDefine.CategoryNo[] array = new ChaListDefine.CategoryNo[]
			{
				ChaListDefine.CategoryNo.so_hair_b,
				ChaListDefine.CategoryNo.so_hair_f,
				ChaListDefine.CategoryNo.so_hair_s,
				ChaListDefine.CategoryNo.so_hair_o
			};
			List<CustomSelectInfo> lst = CvsBase.CreateSelectList(array[base.SNo], ChaListDefine.KeyType.Unknown);
			this.sscHairType.CreateList(lst);
		}

		// Token: 0x06004B27 RID: 19239 RVA: 0x001CA994 File Offset: 0x001C8D94
		private void CalculateUI()
		{
			this.ssMetallic.SetSliderValue(base.hair.parts[base.SNo].metallic);
			this.ssSmoothness.SetSliderValue(base.hair.parts[base.SNo].smoothness);
			this.sldGuidSpeed.value = this.hairCtrlSetting.controllerSpeed;
			this.sldGuidScale.value = this.hairCtrlSetting.controllerScale;
		}

		// Token: 0x06004B28 RID: 19240 RVA: 0x001CAA14 File Offset: 0x001C8E14
		public override void UpdateCustomUI()
		{
			if (this.backSNo != base.SNo)
			{
				this.UpdateHairList();
				this.backSNo = base.SNo;
			}
			base.UpdateCustomUI();
			this.CalculateUI();
			this.tglGuidDraw.SetIsOnWithoutCallback(this.hairCtrlSetting.drawController);
			this.tglGuidType[this.hairCtrlSetting.controllerType].SetIsOnWithoutCallback(true);
			this.tglGuidType[this.hairCtrlSetting.controllerType & 1].SetIsOnWithoutCallback(false);
			this.sscHairType.SetToggleID(base.hair.parts[base.SNo].id);
			this.csBaseColor.SetColor(base.hair.parts[base.SNo].baseColor);
			this.csTopColor.SetColor(base.hair.parts[base.SNo].topColor);
			this.csUnderColor.SetColor(base.hair.parts[base.SNo].underColor);
			this.csSpecular.SetColor(base.hair.parts[base.SNo].specular);
			this.InitializeHairMesh();
			this.SetDrawSettingByHair();
		}

		// Token: 0x06004B29 RID: 19241 RVA: 0x001CAB50 File Offset: 0x001C8F50
		public void ChangePatternImage()
		{
			ListInfoBase listInfo = base.chaCtrl.lstCtrl.GetListInfo(ChaListDefine.CategoryNo.st_hairmeshptn, base.hair.parts[base.SNo].meshType);
			if (listInfo != null)
			{
				Texture2D texture2D = CommonLib.LoadAsset<Texture2D>(listInfo.GetInfo(ChaListDefine.KeyType.ThumbAB), listInfo.GetInfo(ChaListDefine.KeyType.ThumbTex), false, string.Empty);
				if (texture2D)
				{
					this.imgPattern.sprite = Sprite.Create(texture2D, new Rect(0f, 0f, (float)texture2D.width, (float)texture2D.height), new Vector2(0.5f, 0.5f));
				}
			}
		}

		// Token: 0x06004B2A RID: 19242 RVA: 0x001CABF4 File Offset: 0x001C8FF4
		public void UpdateDrawControllerState()
		{
			int controllerType = this.hairCtrlSetting.controllerType;
			bool drawController = this.hairCtrlSetting.drawController;
			float controllerSpeed = this.hairCtrlSetting.controllerSpeed;
			float controllerScale = this.hairCtrlSetting.controllerScale;
			this.tglGuidDraw.SetIsOnWithoutCallback(drawController);
			this.tglGuidType[controllerType].SetIsOnWithoutCallback(true);
			this.sldGuidSpeed.value = controllerSpeed;
			this.sldGuidScale.value = controllerScale;
		}

		// Token: 0x06004B2B RID: 19243 RVA: 0x001CAC64 File Offset: 0x001C9064
		public void SetDrawSettingByHair()
		{
			if (base.chaCtrl.cmpHair == null)
			{
				return;
			}
			this.lstHairBundleSet.Clear();
			for (int i = this.trfContent.childCount - 1; i >= 0; i--)
			{
				Transform child = this.trfContent.GetChild(i);
				if (!(child.name == "CtrlSetting") && !(child.name == "control"))
				{
					child.SetParent(null);
					child.name = "delete_reserve";
					UnityEngine.Object.Destroy(child.gameObject);
				}
			}
			if (null != base.customBase.objHairControllerTop)
			{
				int childCount = base.customBase.objHairControllerTop.transform.childCount;
				for (int j = childCount - 1; j >= 0; j--)
				{
					Transform child2 = base.customBase.objHairControllerTop.transform.GetChild(j);
					child2.SetParent(null);
					child2.name = "delete_reserve";
					UnityEngine.Object.Destroy(child2.gameObject);
				}
			}
			base.customBase.customCtrl.camCtrl.ClearListCollider();
			if (null == base.chaCtrl.cmpHair[base.SNo])
			{
				base.ShowOrHideTab(false, new int[]
				{
					1,
					2,
					3,
					4
				});
				return;
			}
			base.ShowOrHideTab(true, new int[]
			{
				1
			});
			bool[] array = new bool[]
			{
				base.chaCtrl.cmpHair[base.SNo].useAcsColor01,
				base.chaCtrl.cmpHair[base.SNo].useAcsColor02,
				base.chaCtrl.cmpHair[base.SNo].useAcsColor03
			};
			bool show = array[0] | array[1] | array[2];
			base.ShowOrHideTab(show, new int[]
			{
				2
			});
			base.customBase.drawTopHairColor = base.chaCtrl.cmpHair[base.SNo].useTopColor;
			base.customBase.drawUnderHairColor = base.chaCtrl.cmpHair[base.SNo].useUnderColor;
			this.btnSameSkinColor.transform.parent.gameObject.SetActiveIfDifferent(base.chaCtrl.cmpHair[base.SNo].useSameSkinColorButton);
			for (int k = 0; k < this.csAcsColor.Length; k++)
			{
				this.csAcsColor[k].gameObject.SetActiveIfDifferent(array[k]);
				this.csAcsColor[k].SetColor(base.hair.parts[base.SNo].acsColorInfo[k].color);
			}
			int num = 1;
			for (int l = 0; l < base.hair.parts[base.SNo].dictBundle.Count; l++)
			{
				if (!(null == base.chaCtrl.cmpHair[base.SNo].boneInfo[l].trfCorrect))
				{
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.tmpBundleObj);
					gameObject.transform.SetParent(this.trfContent, false);
					CustomHairBundleSet component = gameObject.GetComponent<CustomHairBundleSet>();
					component.CreateGuid(base.customBase.objHairControllerTop, base.chaCtrl.cmpHair[base.SNo].boneInfo[l]);
					component.Initialize(base.SNo, l, num);
					this.lstHairBundleSet.Add(component);
					gameObject.SetActiveIfDifferent(true);
					num++;
				}
			}
			show = (0 != this.lstHairBundleSet.Count);
			base.ShowOrHideTab(show, new int[]
			{
				3
			});
			show = (Game.isAdd30 && base.chaCtrl.cmpHair[base.SNo].useMesh);
			base.ShowOrHideTab(show, new int[]
			{
				4
			});
		}

		// Token: 0x06004B2C RID: 19244 RVA: 0x001CB070 File Offset: 0x001C9470
		public void UpdateAllBundleUI(int excludeIdx = -1)
		{
			this.allReset = true;
			int count = this.lstHairBundleSet.Count;
			for (int i = 0; i < count; i++)
			{
				if (i != excludeIdx)
				{
					this.lstHairBundleSet[i].UpdateCustomUI();
				}
			}
			this.allReset = false;
		}

		// Token: 0x06004B2D RID: 19245 RVA: 0x001CB0C8 File Offset: 0x001C94C8
		public void UpdateGuidType()
		{
			if (this.lstHairBundleSet == null)
			{
				return;
			}
			int count = this.lstHairBundleSet.Count;
			for (int i = 0; i < count; i++)
			{
				if (!(null == this.lstHairBundleSet[i].cmpGuid))
				{
					this.lstHairBundleSet[i].cmpGuid.SetMode(this.hairCtrlSetting.controllerType);
				}
			}
		}

		// Token: 0x06004B2E RID: 19246 RVA: 0x001CB144 File Offset: 0x001C9544
		public void UpdateGuidSpeed()
		{
			if (this.lstHairBundleSet == null)
			{
				return;
			}
			int count = this.lstHairBundleSet.Count;
			for (int i = 0; i < count; i++)
			{
				if (!(null == this.lstHairBundleSet[i].cmpGuid))
				{
					this.lstHairBundleSet[i].cmpGuid.speedMove = this.hairCtrlSetting.controllerSpeed;
				}
			}
		}

		// Token: 0x06004B2F RID: 19247 RVA: 0x001CB1C0 File Offset: 0x001C95C0
		public void UpdateGuidScale()
		{
			if (this.lstHairBundleSet == null)
			{
				return;
			}
			int count = this.lstHairBundleSet.Count;
			for (int i = 0; i < count; i++)
			{
				if (!(null == this.lstHairBundleSet[i].cmpGuid))
				{
					this.lstHairBundleSet[i].cmpGuid.scaleAxis = this.hairCtrlSetting.controllerScale;
					this.lstHairBundleSet[i].cmpGuid.UpdateScale();
				}
			}
		}

		// Token: 0x06004B30 RID: 19248 RVA: 0x001CB250 File Offset: 0x001C9650
		public bool IsDrag()
		{
			if (this.lstHairBundleSet == null)
			{
				return false;
			}
			int count = this.lstHairBundleSet.Count;
			for (int i = 0; i < count; i++)
			{
				if (!(null == this.lstHairBundleSet[i].cmpGuid))
				{
					if (this.lstHairBundleSet[i].isDrag)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06004B31 RID: 19249 RVA: 0x001CB2C2 File Offset: 0x001C96C2
		public void ShortcutChangeGuidType(int type)
		{
			if (!this.IsDrag())
			{
				this.tglGuidType[type].isOn = true;
			}
		}

		// Token: 0x06004B32 RID: 19250 RVA: 0x001CB2E0 File Offset: 0x001C96E0
		public IEnumerator SetInputText()
		{
			yield return new WaitUntil(() => null != base.chaCtrl);
			this.ssMetallic.SetInputTextValue(CustomBase.ConvertTextFromRate(0, 100, base.hair.parts[base.SNo].metallic));
			this.ssSmoothness.SetInputTextValue(CustomBase.ConvertTextFromRate(0, 100, base.hair.parts[base.SNo].smoothness));
			yield break;
		}

		// Token: 0x06004B33 RID: 19251 RVA: 0x001CB2FC File Offset: 0x001C96FC
		public void InitializeHairMesh()
		{
			if (this.lstDisposable != null && this.lstDisposable.Count != 0)
			{
				int count = this.lstDisposable.Count;
				for (int i = 0; i < count; i++)
				{
					this.lstDisposable[i].Dispose();
				}
			}
			IDisposable item = this.btnPatternWin.OnClickAsObservable().Subscribe(delegate(Unit _)
			{
				base.customBase.customCtrl.showPattern = true;
				this.hairMeshPtnSel.ChangeLink(1, base.SNo, 0);
				this.hairMeshPtnSel.onSelect = delegate(int p, int i)
				{
					this.ChangePatternImage();
					if (this.objPatternSet)
					{
						this.objPatternSet.SetActiveIfDifferent(0 != base.hair.parts[base.SNo].meshType);
					}
				};
			});
			this.lstDisposable.Add(item);
			this.csPatternColor.actUpdateColor = delegate(Color color)
			{
				base.hair.parts[base.SNo].meshColor = color;
				base.chaCtrl.ChangeSettingHairMeshColor(base.SNo);
			};
			this.ssPatternW.onChange = delegate(float value)
			{
				base.hair.parts[base.SNo].meshLayout = new Vector4(value, base.hair.parts[base.SNo].meshLayout.y, base.hair.parts[base.SNo].meshLayout.z, base.hair.parts[base.SNo].meshLayout.w);
				base.chaCtrl.ChangeSettingHairMeshLayout(base.SNo);
			};
			this.ssPatternW.onSetDefaultValue = (() => 1f);
			this.ssPatternH.onChange = delegate(float value)
			{
				base.hair.parts[base.SNo].meshLayout = new Vector4(base.hair.parts[base.SNo].meshLayout.x, value, base.hair.parts[base.SNo].meshLayout.z, base.hair.parts[base.SNo].meshLayout.w);
				base.chaCtrl.ChangeSettingHairMeshLayout(base.SNo);
			};
			this.ssPatternH.onSetDefaultValue = (() => 1f);
			this.ssPatternX.onChange = delegate(float value)
			{
				base.hair.parts[base.SNo].meshLayout = new Vector4(base.hair.parts[base.SNo].meshLayout.x, base.hair.parts[base.SNo].meshLayout.y, value, base.hair.parts[base.SNo].meshLayout.w);
				base.chaCtrl.ChangeSettingHairMeshLayout(base.SNo);
			};
			this.ssPatternX.onSetDefaultValue = (() => 0f);
			this.ssPatternY.onChange = delegate(float value)
			{
				base.hair.parts[base.SNo].meshLayout = new Vector4(base.hair.parts[base.SNo].meshLayout.x, base.hair.parts[base.SNo].meshLayout.y, base.hair.parts[base.SNo].meshLayout.z, value);
				base.chaCtrl.ChangeSettingHairMeshLayout(base.SNo);
			};
			this.ssPatternY.onSetDefaultValue = (() => 0f);
			this.ChangePatternImage();
			if (this.objPatternSet)
			{
				this.objPatternSet.SetActiveIfDifferent(0 != base.hair.parts[base.SNo].meshType);
			}
			base.hair.parts[base.SNo].meshColor = new Color(base.hair.parts[base.SNo].meshColor.r, base.hair.parts[base.SNo].meshColor.g, base.hair.parts[base.SNo].meshColor.b, 1f);
			this.csPatternColor.SetColor(base.hair.parts[base.SNo].meshColor);
			this.ssPatternW.SetSliderValue(base.hair.parts[base.SNo].meshLayout.x);
			this.ssPatternH.SetSliderValue(base.hair.parts[base.SNo].meshLayout.y);
			this.ssPatternX.SetSliderValue(base.hair.parts[base.SNo].meshLayout.z);
			this.ssPatternY.SetSliderValue(base.hair.parts[base.SNo].meshLayout.w);
			this.ssPatternW.SetInputTextValue(CustomBase.ConvertTextFromRate(0, 100, base.hair.parts[base.SNo].meshLayout.x));
			this.ssPatternH.SetInputTextValue(CustomBase.ConvertTextFromRate(0, 100, base.hair.parts[base.SNo].meshLayout.y));
			this.ssPatternX.SetInputTextValue(CustomBase.ConvertTextFromRate(0, 100, base.hair.parts[base.SNo].meshLayout.z));
			this.ssPatternY.SetInputTextValue(CustomBase.ConvertTextFromRate(0, 100, base.hair.parts[base.SNo].meshLayout.w));
		}

		// Token: 0x06004B34 RID: 19252 RVA: 0x001CB6EC File Offset: 0x001C9AEC
		protected override void Start()
		{
			base.Start();
			base.customBase.actUpdateCvsHair += this.UpdateCustomUI;
			this.UpdateHairList();
			this.sscHairType.SetToggleID(base.hair.parts[base.SNo].id);
			this.sscHairType.onSelect = delegate(CustomSelectInfo info)
			{
				if (info != null && base.hair.parts[base.SNo].id != info.id)
				{
					base.chaCtrl.ChangeHair(base.SNo, info.id, false);
					base.chaCtrl.SetHairAcsDefaultColorParameterOnly(base.SNo);
					base.chaCtrl.ChangeSettingHairAcsColor(base.SNo);
					this.SetDrawSettingByHair();
				}
			};
			this.csBaseColor.actUpdateColor = delegate(Color color)
			{
				if (this.autoSetting)
				{
					Color topColor;
					Color underColor;
					Color specular;
					base.chaCtrl.CreateHairColor(color, out topColor, out underColor, out specular);
					for (int i = 0; i < base.hair.parts.Length; i++)
					{
						if (this.sameSetting || i == base.SNo)
						{
							base.hair.parts[i].baseColor = color;
							base.hair.parts[i].topColor = topColor;
							base.hair.parts[i].underColor = underColor;
							base.hair.parts[i].specular = specular;
							base.chaCtrl.ChangeSettingHairColor(i, true, this.autoSetting, this.autoSetting);
							base.chaCtrl.ChangeSettingHairSpecular(i);
							this.csTopColor.SetColor(base.hair.parts[base.SNo].topColor);
							this.csUnderColor.SetColor(base.hair.parts[base.SNo].underColor);
							this.csSpecular.SetColor(base.hair.parts[base.SNo].specular);
						}
					}
				}
				else
				{
					for (int j = 0; j < base.hair.parts.Length; j++)
					{
						if (this.sameSetting || j == base.SNo)
						{
							base.hair.parts[j].baseColor = color;
							base.chaCtrl.ChangeSettingHairColor(j, true, this.autoSetting, this.autoSetting);
						}
					}
				}
			};
			this.csTopColor.actUpdateColor = delegate(Color color)
			{
				for (int i = 0; i < base.hair.parts.Length; i++)
				{
					if (this.sameSetting || i == base.SNo)
					{
						base.hair.parts[i].topColor = color;
						base.chaCtrl.ChangeSettingHairColor(i, false, true, false);
					}
				}
			};
			this.csUnderColor.actUpdateColor = delegate(Color color)
			{
				for (int i = 0; i < base.hair.parts.Length; i++)
				{
					if (this.sameSetting || i == base.SNo)
					{
						base.hair.parts[i].underColor = color;
						base.chaCtrl.ChangeSettingHairColor(i, false, false, true);
					}
				}
			};
			this.csSpecular.actUpdateColor = delegate(Color color)
			{
				for (int i = 0; i < base.hair.parts.Length; i++)
				{
					if (this.sameSetting || i == base.SNo)
					{
						base.hair.parts[i].specular = color;
						base.chaCtrl.ChangeSettingHairSpecular(i);
					}
				}
			};
			this.ssMetallic.onChange = delegate(float value)
			{
				for (int i = 0; i < base.hair.parts.Length; i++)
				{
					if (this.sameSetting || i == base.SNo)
					{
						base.hair.parts[i].metallic = value;
						base.chaCtrl.ChangeSettingHairMetallic(i);
					}
				}
			};
			this.ssMetallic.onSetDefaultValue = (() => base.defChaCtrl.custom.hair.parts[base.SNo].metallic);
			this.ssSmoothness.onChange = delegate(float value)
			{
				for (int i = 0; i < base.hair.parts.Length; i++)
				{
					if (this.sameSetting || i == base.SNo)
					{
						base.hair.parts[i].smoothness = value;
						base.chaCtrl.ChangeSettingHairSmoothness(i);
					}
				}
			};
			this.ssSmoothness.onSetDefaultValue = (() => base.defChaCtrl.custom.hair.parts[base.SNo].smoothness);
			this.hcPreset.onClick = delegate(CustomHairColorPreset.HairColorInfo preset)
			{
				for (int i = 0; i < base.hair.parts.Length; i++)
				{
					if (this.sameSetting || i == base.SNo)
					{
						base.hair.parts[i].baseColor = preset.baseColor;
						base.hair.parts[i].topColor = preset.topColor;
						base.hair.parts[i].underColor = preset.underColor;
						base.hair.parts[i].specular = preset.specular;
						base.hair.parts[i].metallic = preset.metallic;
						base.hair.parts[i].smoothness = preset.smoothness;
						base.chaCtrl.ChangeSettingHairColor(i, true, true, true);
						base.chaCtrl.ChangeSettingHairSpecular(i);
						base.chaCtrl.ChangeSettingHairMetallic(i);
						base.chaCtrl.ChangeSettingHairSmoothness(i);
						this.csBaseColor.SetColor(base.hair.parts[i].baseColor);
						this.csTopColor.SetColor(base.hair.parts[i].topColor);
						this.csUnderColor.SetColor(base.hair.parts[i].underColor);
						this.csSpecular.SetColor(base.hair.parts[i].specular);
						this.ssMetallic.SetSliderValue(base.hair.parts[i].metallic);
						this.ssSmoothness.SetSliderValue(base.hair.parts[i].smoothness);
					}
				}
			};
			this.btnSameSkinColor.OnClickAsObservable().Subscribe(delegate(Unit isPush)
			{
				float h;
				float num;
				float num2;
				Color.RGBToHSV(base.body.skinColor, out h, out num, out num2);
				base.hair.parts[base.SNo].underColor = Color.HSVToRGB(h, Mathf.Max(0f, num - 0.06f), Mathf.Max(0f, num2 - 0.06f));
				base.hair.parts[base.SNo].smoothness = Mathf.Max(0f, base.body.skinGlossPower - 0.3f);
				base.chaCtrl.ChangeSettingHairColor(base.SNo, false, false, true);
				base.chaCtrl.ChangeSettingHairSmoothness(base.SNo);
				this.csUnderColor.SetColor(base.hair.parts[base.SNo].underColor);
				this.ssSmoothness.SetSliderValue(base.hair.parts[base.SNo].smoothness);
			});
			if (this.csAcsColor != null && this.csAcsColor.Any<CustomColorSet>())
			{
				this.csAcsColor.ToList<CustomColorSet>().ForEach(delegate(CustomColorSet item)
				{
					item.actUpdateColor = delegate(Color color)
					{
						base.hair.parts[base.SNo].acsColorInfo[0].color = color;
						base.chaCtrl.ChangeSettingHairAcsColor(base.SNo);
					};
				});
			}
			if (this.btnCorrectAllReset)
			{
				this.btnCorrectAllReset.OnClickAsObservable().Subscribe(delegate(Unit _)
				{
					base.chaCtrl.SetDefaultHairCorrectPosRateAll(base.SNo);
					base.chaCtrl.SetDefaultHairCorrectRotRateAll(base.SNo);
					this.UpdateAllBundleUI(-1);
				});
			}
			this.tglGuidDraw.onValueChanged.AsObservable<bool>().Subscribe(delegate(bool isOn)
			{
				this.hairCtrlSetting.drawController = isOn;
			});
			if (this.tglGuidType.Any<Toggle>())
			{
				(from item in this.tglGuidType.Select((Toggle val, int idx) => new
				{
					val,
					idx
				})
				where item.val != null
				select item).ToList().ForEach(delegate(item)
				{
					(from isOn in item.val.onValueChanged.AsObservable<bool>()
					where isOn
					select isOn).Subscribe(delegate(bool isOn)
					{
						this.hairCtrlSetting.controllerType = item.idx;
						this.UpdateGuidType();
					});
				});
			}
			this.sldGuidSpeed.OnValueChangedAsObservable().Subscribe(delegate(float val)
			{
				this.hairCtrlSetting.controllerSpeed = val;
				this.UpdateGuidSpeed();
			});
			this.sldGuidSpeed.OnScrollAsObservable().Subscribe(delegate(PointerEventData scl)
			{
				if (base.customBase.sliderControlWheel)
				{
					this.sldGuidSpeed.value = Mathf.Clamp(this.sldGuidSpeed.value + scl.scrollDelta.y * -0.01f, 0.1f, 1f);
				}
			});
			this.sldGuidScale.OnValueChangedAsObservable().Subscribe(delegate(float val)
			{
				this.hairCtrlSetting.controllerScale = val;
				this.UpdateGuidScale();
			});
			this.sldGuidScale.OnScrollAsObservable().Subscribe(delegate(PointerEventData scl)
			{
				if (base.customBase.sliderControlWheel)
				{
					this.sldGuidScale.value = Mathf.Clamp(this.sldGuidScale.value + scl.scrollDelta.y * -0.01f, 0.3f, 3f);
				}
			});
			this.UpdateDrawControllerState();
			base.StartCoroutine(this.SetInputText());
			this.backSNo = base.SNo;
		}

		// Token: 0x04004530 RID: 17712
		[Header("【設定01】----------------------")]
		[SerializeField]
		private CustomSelectScrollController sscHairType;

		// Token: 0x04004531 RID: 17713
		[Header("【設定02】----------------------")]
		[SerializeField]
		private CustomColorSet csBaseColor;

		// Token: 0x04004532 RID: 17714
		[SerializeField]
		private CustomColorSet csTopColor;

		// Token: 0x04004533 RID: 17715
		[SerializeField]
		private CustomColorSet csUnderColor;

		// Token: 0x04004534 RID: 17716
		[SerializeField]
		private CustomColorSet csSpecular;

		// Token: 0x04004535 RID: 17717
		[SerializeField]
		private CustomSliderSet ssMetallic;

		// Token: 0x04004536 RID: 17718
		[SerializeField]
		private CustomSliderSet ssSmoothness;

		// Token: 0x04004537 RID: 17719
		[SerializeField]
		private CustomHairColorPreset hcPreset;

		// Token: 0x04004538 RID: 17720
		[SerializeField]
		private Button btnSameSkinColor;

		// Token: 0x04004539 RID: 17721
		[Header("【設定03】----------------------")]
		[SerializeField]
		private CustomColorSet[] csAcsColor;

		// Token: 0x0400453A RID: 17722
		[Header("【設定04】----------------------")]
		[SerializeField]
		private Transform trfContent;

		// Token: 0x0400453B RID: 17723
		[SerializeField]
		private GameObject tmpBundleObj;

		// Token: 0x0400453C RID: 17724
		private List<CustomHairBundleSet> lstHairBundleSet = new List<CustomHairBundleSet>();

		// Token: 0x0400453D RID: 17725
		[SerializeField]
		private Button btnCorrectAllReset;

		// Token: 0x0400453E RID: 17726
		[Header("【設定05】----------------------")]
		[SerializeField]
		private CustomClothesPatternSelect hairMeshPtnSel;

		// Token: 0x0400453F RID: 17727
		[SerializeField]
		private GameObject objPatternSet;

		// Token: 0x04004540 RID: 17728
		[SerializeField]
		private Button btnPatternWin;

		// Token: 0x04004541 RID: 17729
		[SerializeField]
		private Image imgPattern;

		// Token: 0x04004542 RID: 17730
		[SerializeField]
		private CustomColorSet csPatternColor;

		// Token: 0x04004543 RID: 17731
		[SerializeField]
		private CustomSliderSet ssPatternW;

		// Token: 0x04004544 RID: 17732
		[SerializeField]
		private CustomSliderSet ssPatternH;

		// Token: 0x04004545 RID: 17733
		[SerializeField]
		private CustomSliderSet ssPatternX;

		// Token: 0x04004546 RID: 17734
		[SerializeField]
		private CustomSliderSet ssPatternY;

		// Token: 0x04004547 RID: 17735
		[SerializeField]
		private Toggle tglGuidDraw;

		// Token: 0x04004548 RID: 17736
		[SerializeField]
		private Toggle[] tglGuidType;

		// Token: 0x04004549 RID: 17737
		[SerializeField]
		private Slider sldGuidSpeed;

		// Token: 0x0400454A RID: 17738
		[SerializeField]
		private Slider sldGuidScale;

		// Token: 0x0400454B RID: 17739
		private int backSNo = -1;

		// Token: 0x0400454D RID: 17741
		private List<IDisposable> lstDisposable = new List<IDisposable>();
	}
}
