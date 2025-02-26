using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using Illusion.Extensions;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Studio
{
	// Token: 0x02001316 RID: 4886
	public class MPItemCtrl : MonoBehaviour
	{
		// Token: 0x17002240 RID: 8768
		// (get) Token: 0x0600A2DA RID: 41690 RVA: 0x0042A313 File Offset: 0x00428713
		// (set) Token: 0x0600A2DB RID: 41691 RVA: 0x0042A31B File Offset: 0x0042871B
		public OCIItem ociItem
		{
			get
			{
				return this.m_OCIItem;
			}
			set
			{
				this.m_OCIItem = value;
				if (this.m_OCIItem != null)
				{
					this.UpdateInfo();
				}
			}
		}

		// Token: 0x17002241 RID: 8769
		// (get) Token: 0x0600A2DC RID: 41692 RVA: 0x0042A335 File Offset: 0x00428735
		// (set) Token: 0x0600A2DD RID: 41693 RVA: 0x0042A340 File Offset: 0x00428740
		public bool active
		{
			get
			{
				return this.m_Active;
			}
			set
			{
				this.m_Active = value;
				if (this.m_Active)
				{
					base.gameObject.SetActive(this.m_OCIItem != null && this.m_OCIItem.CheckAnim);
					this.animeControl.active = (this.m_OCIItem != null && this.m_OCIItem.isAnime);
				}
				else
				{
					if (!this.mpCharCtrl.active)
					{
						this.animeControl.active = false;
					}
					base.gameObject.SetActive(false);
					if (this.isColorFunc)
					{
						Singleton<Studio>.Instance.colorPalette.Close();
					}
					this.isColorFunc = false;
					Singleton<Studio>.Instance.colorPalette.visible = false;
					this.panelList.active = false;
					this.cgPattern.Enable(false, false);
				}
			}
		}

		// Token: 0x0600A2DE RID: 41694 RVA: 0x0042A424 File Offset: 0x00428824
		public bool Deselect(OCIItem _ociItem)
		{
			if (this.m_OCIItem != _ociItem)
			{
				return false;
			}
			this.ociItem = null;
			this.active = false;
			return true;
		}

		// Token: 0x0600A2DF RID: 41695 RVA: 0x0042A444 File Offset: 0x00428844
		public void UpdateInfo()
		{
			if (this.m_OCIItem == null)
			{
				return;
			}
			this.isUpdateInfo = true;
			bool[] useColor = this.m_OCIItem.useColor;
			bool[] useMetallic = this.m_OCIItem.useMetallic;
			bool[] usePattern = this.m_OCIItem.usePattern;
			for (int i = 0; i < 3; i++)
			{
				this.colorInfo[i].enable = useColor[i];
				if (useColor[i])
				{
					Studio.ColorInfo colorInfo = this.m_OCIItem.itemInfo.colors[i];
					this.colorInfo[i].colorMain = colorInfo.mainColor;
					this.colorInfo[i].EnableMetallic = useMetallic[i];
					if (useMetallic[i])
					{
						this.colorInfo[i].inputMetallic.value = colorInfo.metallic;
						this.colorInfo[i].inputGlossiness.value = colorInfo.glossiness;
					}
					this.colorInfo[i].EnablePattern = usePattern[i];
					if (usePattern[i])
					{
						this.colorInfo[i].textPattern = colorInfo.pattern.name;
						this.colorInfo[i].colorPattern = colorInfo.pattern.color;
						this.colorInfo[i].isOn = !colorInfo.pattern.clamp;
						this.colorInfo[i][0].value = colorInfo.pattern.ut;
						this.colorInfo[i][1].value = colorInfo.pattern.vt;
						this.colorInfo[i][2].value = colorInfo.pattern.us;
						this.colorInfo[i][3].value = colorInfo.pattern.vs;
						this.colorInfo[i][4].value = colorInfo.pattern.rot;
					}
				}
			}
			this.colorInfo[3].enable = this.m_OCIItem.useColor4;
			if (this.colorInfo[3].enable)
			{
				this.colorInfo[3].colorMain = this.m_OCIItem.itemInfo.colors[3].mainColor;
				this.colorInfo[3].EnableMetallic = false;
				this.colorInfo[3].EnablePattern = false;
			}
			this.panelInfo.enable = this.m_OCIItem.checkPanel;
			this.panelList.active = false;
			this.SetPanelTexName(this.m_OCIItem.itemInfo.panel.filePath);
			this.panelInfo.color = this.m_OCIItem.itemInfo.colors[0].mainColor;
			this.panelInfo.isOn = !this.m_OCIItem.itemInfo.colors[0].pattern.clamp;
			this.panelInfo[0].value = this.m_OCIItem.itemInfo.colors[0].pattern.ut;
			this.panelInfo[1].value = this.m_OCIItem.itemInfo.colors[0].pattern.vt;
			this.panelInfo[2].value = this.m_OCIItem.itemInfo.colors[0].pattern.us;
			this.panelInfo[3].value = this.m_OCIItem.itemInfo.colors[0].pattern.vs;
			this.panelInfo[4].value = this.m_OCIItem.itemInfo.colors[0].pattern.rot;
			this.inputAlpha.value = this.m_OCIItem.itemInfo.alpha;
			this.inputAlpha.active = this.m_OCIItem.CheckAlpha;
			this.emissionInfo.active = this.m_OCIItem.CheckEmission;
			if (this.m_OCIItem.CheckEmissionColor)
			{
				this.emissionInfo.color.interactable = true;
				this.emissionInfo.color.color = this.m_OCIItem.itemInfo.EmissionColor;
			}
			else
			{
				this.emissionInfo.color.interactable = false;
				this.emissionInfo.color.color = Color.white;
			}
			if (this.m_OCIItem.CheckEmissionPower)
			{
				this.emissionInfo.input.interactable = true;
				this.emissionInfo.input.value = this.m_OCIItem.itemInfo.emissionPower;
			}
			else
			{
				this.emissionInfo.input.interactable = false;
				this.emissionInfo.input.value = 0f;
			}
			this.inputLightCancel.active = this.m_OCIItem.CheckLightCancel;
			this.inputLightCancel.value = this.m_OCIItem.itemInfo.lightCancel;
			this.kinematicInfo.Active = (this.m_OCIItem.isFK || this.m_OCIItem.isDynamicBone);
			this.kinematicInfo.toggleFK.interactable = this.m_OCIItem.isFK;
			this.kinematicInfo.toggleFK.isOn = this.m_OCIItem.itemInfo.enableFK;
			this.kinematicInfo.toggleDynamicBone.interactable = this.m_OCIItem.isDynamicBone;
			this.kinematicInfo.toggleDynamicBone.isOn = this.m_OCIItem.itemInfo.enableDynamicBone;
			this.optionInfo.Active = this.m_OCIItem.CheckOption;
			this.optionInfo.toggleAll.isOn = this.m_OCIItem.itemInfo.option.SafeGet(0);
			this.animeInfo.Active = this.m_OCIItem.CheckAnimePattern;
			this.animeInfo.InitDropdown(this.m_OCIItem);
			this.animeControl.objectCtrlInfo = this.m_OCIItem;
			this.cgPattern.Enable(false, false);
			this.isUpdateInfo = false;
		}

		// Token: 0x0600A2E0 RID: 41696 RVA: 0x0042AA78 File Offset: 0x00428E78
		private void OnClickColorMain(int _idx)
		{
			string[] array2 = new string[]
			{
				"アイテム カラー１",
				"アイテム カラー２",
				"アイテム カラー３"
			};
			if (Singleton<Studio>.Instance.colorPalette.Check(array2[_idx]))
			{
				this.isColorFunc = false;
				Singleton<Studio>.Instance.colorPalette.visible = false;
				return;
			}
			IEnumerable<OCIItem> array = from v in Studio.GetSelectObjectCtrl()
			where v.kind == 1
			select v as OCIItem;
			Singleton<Studio>.Instance.colorPalette.Setup(array2[_idx], this.m_OCIItem.itemInfo.colors[_idx].mainColor, delegate(Color _c)
			{
				foreach (OCIItem ociitem in array)
				{
					ociitem.SetColor((!ociitem.IsParticle) ? new Color(_c.r, _c.g, _c.b, 1f) : _c, _idx);
				}
				this.colorInfo[_idx].colorMain = _c;
			}, this.m_OCIItem.IsParticle);
			this.isColorFunc = true;
		}

		// Token: 0x0600A2E1 RID: 41697 RVA: 0x0042AB8C File Offset: 0x00428F8C
		private void OnClickColor4()
		{
			if (Singleton<Studio>.Instance.colorPalette.Check("アイテム カラー４"))
			{
				this.isColorFunc = false;
				Singleton<Studio>.Instance.colorPalette.visible = false;
				return;
			}
			IEnumerable<OCIItem> array = from v in Studio.GetSelectObjectCtrl()
			where v.kind == 1
			select v as OCIItem;
			Singleton<Studio>.Instance.colorPalette.Setup("アイテム カラー４", this.m_OCIItem.itemInfo.colors[3].mainColor, delegate(Color _c)
			{
				foreach (OCIItem ociitem in array)
				{
					ociitem.SetColor(_c, 3);
				}
				this.colorInfo[3].colorMain = _c;
			}, true);
			this.isColorFunc = true;
		}

		// Token: 0x0600A2E2 RID: 41698 RVA: 0x0042AC68 File Offset: 0x00429068
		private void OnClickColorMainDef(int _idx)
		{
			foreach (OCIItem ociitem in from v in Studio.GetSelectObjectCtrl()
			where v.kind == 1
			select v as OCIItem)
			{
				if (ociitem.isChangeColor)
				{
					if (ociitem.useColor.SafeGet(_idx))
					{
						ociitem.SetColor(ociitem.defColor[_idx], _idx);
					}
				}
			}
			this.m_OCIItem.defColor.SafeProc(_idx, delegate(Color _c)
			{
				this.colorInfo[_idx].colorMain = _c;
			});
			this.isColorFunc = false;
			Singleton<Studio>.Instance.colorPalette.visible = false;
		}

		// Token: 0x0600A2E3 RID: 41699 RVA: 0x0042AD98 File Offset: 0x00429198
		private void OnClickColor4Def()
		{
			foreach (OCIItem ociitem in from v in Studio.GetSelectObjectCtrl()
			where v.kind == 1
			select v as OCIItem)
			{
				ociitem.SetColor(ociitem.itemComponent.defGlass, 3);
			}
			this.colorInfo[3].colorMain = this.m_OCIItem.itemComponent.defGlass;
			this.isColorFunc = false;
			Singleton<Studio>.Instance.colorPalette.visible = false;
		}

		// Token: 0x0600A2E4 RID: 41700 RVA: 0x0042AE74 File Offset: 0x00429274
		private void OnValueChangeMetallic(int _idx, float _value)
		{
			if (this.isUpdateInfo)
			{
				return;
			}
			foreach (OCIItem ociitem in from v in Studio.GetSelectObjectCtrl()
			where v.kind == 1
			select v as OCIItem)
			{
				ociitem.SetMetallic(_idx, _value);
			}
			this.colorInfo[_idx].inputMetallic.value = _value;
		}

		// Token: 0x0600A2E5 RID: 41701 RVA: 0x0042AF30 File Offset: 0x00429330
		private void OnEndEditMetallic(int _idx, string _text)
		{
			float value = Mathf.Clamp(Utility.StringToFloat(_text), 0f, 1f);
			foreach (OCIItem ociitem in from v in Studio.GetSelectObjectCtrl()
			where v.kind == 1
			select v as OCIItem)
			{
				ociitem.SetMetallic(_idx, value);
			}
			this.colorInfo[_idx].inputMetallic.value = value;
		}

		// Token: 0x0600A2E6 RID: 41702 RVA: 0x0042AFF8 File Offset: 0x004293F8
		private void OnClickMetallicDef(int _idx)
		{
			using (IEnumerator<OCIItem> enumerator = (from v in Studio.GetSelectObjectCtrl()
			where v.kind == 1
			select v as OCIItem).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					OCIItem v = enumerator.Current;
					v.itemComponent[_idx].SafeProc(delegate(ItemComponent.Info info)
					{
						v.SetMetallic(_idx, info.defMetallic);
					});
				}
			}
			this.m_OCIItem.itemComponent[_idx].SafeProc(delegate(ItemComponent.Info info)
			{
				this.colorInfo[_idx].inputMetallic.value = info.defMetallic;
			});
		}

		// Token: 0x0600A2E7 RID: 41703 RVA: 0x0042B104 File Offset: 0x00429504
		private void OnValueChangeGlossiness(int _idx, float _value)
		{
			if (this.isUpdateInfo)
			{
				return;
			}
			foreach (OCIItem ociitem in from v in Studio.GetSelectObjectCtrl()
			where v.kind == 1
			select v as OCIItem)
			{
				ociitem.SetGlossiness(_idx, _value);
			}
			this.colorInfo[_idx].inputGlossiness.value = _value;
		}

		// Token: 0x0600A2E8 RID: 41704 RVA: 0x0042B1C0 File Offset: 0x004295C0
		private void OnEndEditGlossiness(int _idx, string _text)
		{
			float value = Mathf.Clamp(Utility.StringToFloat(_text), 0f, 1f);
			foreach (OCIItem ociitem in from v in Studio.GetSelectObjectCtrl()
			where v.kind == 1
			select v as OCIItem)
			{
				ociitem.SetGlossiness(_idx, value);
			}
			this.colorInfo[_idx].inputGlossiness.value = value;
		}

		// Token: 0x0600A2E9 RID: 41705 RVA: 0x0042B288 File Offset: 0x00429688
		private void OnClickGlossinessDef(int _idx)
		{
			using (IEnumerator<OCIItem> enumerator = (from v in Studio.GetSelectObjectCtrl()
			where v.kind == 1
			select v as OCIItem).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					OCIItem v = enumerator.Current;
					v.itemComponent[_idx].SafeProc(delegate(ItemComponent.Info info)
					{
						v.SetGlossiness(_idx, info.defGlossiness);
					});
				}
			}
			this.m_OCIItem.itemComponent[_idx].SafeProc(delegate(ItemComponent.Info info)
			{
				this.colorInfo[_idx].inputGlossiness.value = info.defGlossiness;
			});
		}

		// Token: 0x0600A2EA RID: 41706 RVA: 0x0042B394 File Offset: 0x00429794
		private void OnClickColorPattern(int _idx)
		{
			string[] array2 = new string[]
			{
				"柄の色１",
				"柄の色２",
				"柄の色３"
			};
			if (Singleton<Studio>.Instance.colorPalette.Check(array2[_idx]))
			{
				this.isColorFunc = false;
				Singleton<Studio>.Instance.colorPalette.visible = false;
				return;
			}
			IEnumerable<OCIItem> array = from v in Studio.GetSelectObjectCtrl()
			where v.kind == 1
			select v as OCIItem;
			Singleton<Studio>.Instance.colorPalette.Setup(array2[_idx], this.m_OCIItem.itemInfo.colors[_idx].pattern.color, delegate(Color _c)
			{
				foreach (OCIItem ociitem in array)
				{
					ociitem.SetPatternColor(_idx, _c);
				}
				this.colorInfo[_idx].colorPattern = _c;
			}, true);
			this.isColorFunc = true;
		}

		// Token: 0x0600A2EB RID: 41707 RVA: 0x0042B4A4 File Offset: 0x004298A4
		private void OnClickColorPatternDef(int _idx)
		{
			foreach (OCIItem ociitem in from v in Studio.GetSelectObjectCtrl()
			where v.kind == 1
			select v as OCIItem)
			{
				ociitem.SetPatternColor(_idx, ociitem.itemComponent.defColorPattern[_idx]);
			}
			this.colorInfo[_idx].colorPattern = this.m_OCIItem.itemComponent.defColorPattern[_idx];
			this.isColorFunc = false;
			Singleton<Studio>.Instance.colorPalette.visible = false;
		}

		// Token: 0x0600A2EC RID: 41708 RVA: 0x0042B598 File Offset: 0x00429998
		private void OnToggleColor(int _idx, bool _flag)
		{
			if (this.m_OCIItem == null)
			{
				return;
			}
			foreach (OCIItem ociitem in from v in Studio.GetSelectObjectCtrl()
			where v.kind == 1
			select v as OCIItem)
			{
				ociitem.SetPatternClamp(_idx, !_flag);
			}
		}

		// Token: 0x0600A2ED RID: 41709 RVA: 0x0042B644 File Offset: 0x00429A44
		private void OnValueChangeUT(int _idx, float _value)
		{
			if (this.isUpdateInfo)
			{
				return;
			}
			foreach (OCIItem ociitem in from v in Studio.GetSelectObjectCtrl()
			where v.kind == 1
			select v as OCIItem)
			{
				ociitem.SetPatternUT(_idx, _value);
			}
			this.colorInfo[_idx][0].value = _value;
		}

		// Token: 0x0600A2EE RID: 41710 RVA: 0x0042B704 File Offset: 0x00429B04
		private void OnEndEditUT(int _idx, string _text)
		{
			float value = Mathf.Clamp(Utility.StringToFloat(_text), -1f, 1f);
			foreach (OCIItem ociitem in from v in Studio.GetSelectObjectCtrl()
			where v.kind == 1
			select v as OCIItem)
			{
				ociitem.SetPatternUT(_idx, value);
			}
			this.colorInfo[_idx][0].value = value;
		}

		// Token: 0x0600A2EF RID: 41711 RVA: 0x0042B7CC File Offset: 0x00429BCC
		private void OnClickUTDef(int _idx)
		{
			using (IEnumerator<OCIItem> enumerator = (from v in Studio.GetSelectObjectCtrl()
			where v.kind == 1
			select v as OCIItem).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					OCIItem v = enumerator.Current;
					v.itemComponent[_idx].SafeProc(delegate(ItemComponent.Info info)
					{
						v.SetPatternUT(_idx, info.ut);
					});
				}
			}
			this.m_OCIItem.itemComponent[_idx].SafeProc(delegate(ItemComponent.Info info)
			{
				this.colorInfo[_idx][0].value = info.ut;
			});
		}

		// Token: 0x0600A2F0 RID: 41712 RVA: 0x0042B8D8 File Offset: 0x00429CD8
		private void OnValueChangeVT(int _idx, float _value)
		{
			if (this.isUpdateInfo)
			{
				return;
			}
			foreach (OCIItem ociitem in from v in Studio.GetSelectObjectCtrl()
			where v.kind == 1
			select v as OCIItem)
			{
				ociitem.SetPatternVT(_idx, _value);
			}
			this.colorInfo[_idx][1].value = _value;
		}

		// Token: 0x0600A2F1 RID: 41713 RVA: 0x0042B998 File Offset: 0x00429D98
		private void OnEndEditVT(int _idx, string _text)
		{
			float value = Mathf.Clamp(Utility.StringToFloat(_text), -1f, 1f);
			foreach (OCIItem ociitem in from v in Studio.GetSelectObjectCtrl()
			where v.kind == 1
			select v as OCIItem)
			{
				ociitem.SetPatternVT(_idx, value);
			}
			this.colorInfo[_idx][1].value = value;
		}

		// Token: 0x0600A2F2 RID: 41714 RVA: 0x0042BA60 File Offset: 0x00429E60
		private void OnClickVTDef(int _idx)
		{
			using (IEnumerator<OCIItem> enumerator = (from v in Studio.GetSelectObjectCtrl()
			where v.kind == 1
			select v as OCIItem).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					OCIItem v = enumerator.Current;
					v.itemComponent[_idx].SafeProc(delegate(ItemComponent.Info info)
					{
						v.SetPatternVT(_idx, info.vt);
					});
				}
			}
			this.m_OCIItem.itemComponent[_idx].SafeProc(delegate(ItemComponent.Info info)
			{
				this.colorInfo[_idx][1].value = info.vt;
			});
		}

		// Token: 0x0600A2F3 RID: 41715 RVA: 0x0042BB6C File Offset: 0x00429F6C
		private void OnValueChangeUS(int _idx, float _value)
		{
			if (this.isUpdateInfo)
			{
				return;
			}
			foreach (OCIItem ociitem in from v in Studio.GetSelectObjectCtrl()
			where v.kind == 1
			select v as OCIItem)
			{
				ociitem.SetPatternUS(_idx, _value);
			}
			this.colorInfo[_idx][2].value = _value;
		}

		// Token: 0x0600A2F4 RID: 41716 RVA: 0x0042BC2C File Offset: 0x0042A02C
		private void OnEndEditUS(int _idx, string _text)
		{
			float value = Mathf.Clamp(Utility.StringToFloat(_text), 0.01f, 20f);
			foreach (OCIItem ociitem in from v in Studio.GetSelectObjectCtrl()
			where v.kind == 1
			select v as OCIItem)
			{
				ociitem.SetPatternUS(_idx, value);
			}
			this.colorInfo[_idx][2].value = value;
		}

		// Token: 0x0600A2F5 RID: 41717 RVA: 0x0042BCF4 File Offset: 0x0042A0F4
		private void OnClickUSDef(int _idx)
		{
			using (IEnumerator<OCIItem> enumerator = (from v in Studio.GetSelectObjectCtrl()
			where v.kind == 1
			select v as OCIItem).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					OCIItem v = enumerator.Current;
					v.itemComponent[_idx].SafeProc(delegate(ItemComponent.Info info)
					{
						v.SetPatternUS(_idx, info.us);
					});
				}
			}
			this.m_OCIItem.itemComponent[_idx].SafeProc(delegate(ItemComponent.Info info)
			{
				this.colorInfo[_idx][2].value = info.us;
			});
		}

		// Token: 0x0600A2F6 RID: 41718 RVA: 0x0042BE00 File Offset: 0x0042A200
		private void OnValueChangeVS(int _idx, float _value)
		{
			if (this.isUpdateInfo)
			{
				return;
			}
			foreach (OCIItem ociitem in from v in Studio.GetSelectObjectCtrl()
			where v.kind == 1
			select v as OCIItem)
			{
				ociitem.SetPatternVS(_idx, _value);
			}
			this.colorInfo[_idx][3].value = _value;
		}

		// Token: 0x0600A2F7 RID: 41719 RVA: 0x0042BEC0 File Offset: 0x0042A2C0
		private void OnEndEditVS(int _idx, string _text)
		{
			float value = Mathf.Clamp(Utility.StringToFloat(_text), 0.01f, 20f);
			foreach (OCIItem ociitem in from v in Studio.GetSelectObjectCtrl()
			where v.kind == 1
			select v as OCIItem)
			{
				ociitem.SetPatternVS(_idx, value);
			}
			this.colorInfo[_idx][3].value = value;
		}

		// Token: 0x0600A2F8 RID: 41720 RVA: 0x0042BF88 File Offset: 0x0042A388
		private void OnClickVSDef(int _idx)
		{
			using (IEnumerator<OCIItem> enumerator = (from v in Studio.GetSelectObjectCtrl()
			where v.kind == 1
			select v as OCIItem).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					OCIItem v = enumerator.Current;
					v.itemComponent[_idx].SafeProc(delegate(ItemComponent.Info info)
					{
						v.SetPatternVS(_idx, info.vs);
					});
				}
			}
			this.m_OCIItem.itemComponent[_idx].SafeProc(delegate(ItemComponent.Info info)
			{
				this.colorInfo[_idx][3].value = info.vs;
			});
		}

		// Token: 0x0600A2F9 RID: 41721 RVA: 0x0042C094 File Offset: 0x0042A494
		private void OnValueChangeRot(int _idx, float _value)
		{
			if (this.isUpdateInfo)
			{
				return;
			}
			foreach (OCIItem ociitem in from v in Studio.GetSelectObjectCtrl()
			where v.kind == 1
			select v as OCIItem)
			{
				ociitem.SetPatternRot(_idx, _value);
			}
			this.colorInfo[_idx][4].value = _value;
		}

		// Token: 0x0600A2FA RID: 41722 RVA: 0x0042C154 File Offset: 0x0042A554
		private void OnEndEditRot(int _idx, string _text)
		{
			float value = Mathf.Clamp(Utility.StringToFloat(_text), -1f, 1f);
			foreach (OCIItem ociitem in from v in Studio.GetSelectObjectCtrl()
			where v.kind == 1
			select v as OCIItem)
			{
				ociitem.SetPatternRot(_idx, value);
			}
			this.colorInfo[_idx][4].value = value;
		}

		// Token: 0x0600A2FB RID: 41723 RVA: 0x0042C21C File Offset: 0x0042A61C
		private void OnClickPanel()
		{
			if (this.panelList.active)
			{
				this.panelList.active = false;
				return;
			}
			this.panelList.Setup(this.m_OCIItem.itemInfo.panel.filePath, new Action<string>(this.SetMainTex));
			this.isColorFunc = true;
		}

		// Token: 0x0600A2FC RID: 41724 RVA: 0x0042C279 File Offset: 0x0042A679
		private void SetMainTex(string _file)
		{
			this.SetPanelTexName(_file);
			this.m_OCIItem.SetMainTex(_file);
		}

		// Token: 0x0600A2FD RID: 41725 RVA: 0x0042C28E File Offset: 0x0042A68E
		private void SetPanelTexName(string _str)
		{
			this.panelInfo.textTex = ((!_str.IsNullOrEmpty()) ? Path.GetFileNameWithoutExtension(_str) : "なし");
		}

		// Token: 0x0600A2FE RID: 41726 RVA: 0x0042C2B8 File Offset: 0x0042A6B8
		private void OnClickColorPanel()
		{
			if (Singleton<Studio>.Instance.colorPalette.Check("画像板"))
			{
				this.isColorFunc = false;
				Singleton<Studio>.Instance.colorPalette.visible = false;
				return;
			}
			Singleton<Studio>.Instance.colorPalette.Setup("画像板", this.m_OCIItem.itemInfo.colors[0].mainColor, delegate(Color _c)
			{
				Color color = new Color(_c.r, _c.g, _c.b, 1f);
				this.m_OCIItem.SetColor(color, 0);
				this.panelInfo.color = color;
			}, false);
			this.isColorFunc = true;
		}

		// Token: 0x0600A2FF RID: 41727 RVA: 0x0042C335 File Offset: 0x0042A735
		private void OnToggleColor(bool _flag)
		{
			if (this.m_OCIItem == null)
			{
				return;
			}
			this.m_OCIItem.SetPatternClamp(0, !_flag);
		}

		// Token: 0x0600A300 RID: 41728 RVA: 0x0042C353 File Offset: 0x0042A753
		private void OnValueChangeUT(float _value)
		{
			if (this.isUpdateInfo)
			{
				return;
			}
			this.m_OCIItem.SetPatternUT(0, _value);
			this.panelInfo[0].value = _value;
		}

		// Token: 0x0600A301 RID: 41729 RVA: 0x0042C380 File Offset: 0x0042A780
		private void OnEndEditUT(string _text)
		{
			float value = Mathf.Clamp(Utility.StringToFloat(_text), -1f, 1f);
			this.m_OCIItem.SetPatternUT(0, value);
			this.panelInfo[0].value = value;
		}

		// Token: 0x0600A302 RID: 41730 RVA: 0x0042C3C2 File Offset: 0x0042A7C2
		private void OnClickUTDef()
		{
			this.panelInfo[0].value = 0f;
		}

		// Token: 0x0600A303 RID: 41731 RVA: 0x0042C3DA File Offset: 0x0042A7DA
		private void OnValueChangeVT(float _value)
		{
			if (this.isUpdateInfo)
			{
				return;
			}
			this.m_OCIItem.SetPatternVT(0, _value);
			this.panelInfo[1].value = _value;
		}

		// Token: 0x0600A304 RID: 41732 RVA: 0x0042C408 File Offset: 0x0042A808
		private void OnEndEditVT(string _text)
		{
			float value = Mathf.Clamp(Utility.StringToFloat(_text), -1f, 1f);
			this.m_OCIItem.SetPatternVT(0, value);
			this.panelInfo[1].value = value;
		}

		// Token: 0x0600A305 RID: 41733 RVA: 0x0042C44A File Offset: 0x0042A84A
		private void OnClickVTDef()
		{
			this.panelInfo[1].value = 0f;
		}

		// Token: 0x0600A306 RID: 41734 RVA: 0x0042C462 File Offset: 0x0042A862
		private void OnValueChangeUS(float _value)
		{
			if (this.isUpdateInfo)
			{
				return;
			}
			this.m_OCIItem.SetPatternUS(0, _value);
			this.panelInfo[2].value = _value;
		}

		// Token: 0x0600A307 RID: 41735 RVA: 0x0042C490 File Offset: 0x0042A890
		private void OnEndEditUS(string _text)
		{
			float value = Mathf.Clamp(Utility.StringToFloat(_text), 0.01f, 20f);
			this.m_OCIItem.SetPatternUS(0, value);
			this.panelInfo[2].value = value;
		}

		// Token: 0x0600A308 RID: 41736 RVA: 0x0042C4D2 File Offset: 0x0042A8D2
		private void OnClickUSDef()
		{
			this.panelInfo[2].value = 1f;
		}

		// Token: 0x0600A309 RID: 41737 RVA: 0x0042C4EA File Offset: 0x0042A8EA
		private void OnValueChangeVS(float _value)
		{
			if (this.isUpdateInfo)
			{
				return;
			}
			this.m_OCIItem.SetPatternVS(0, _value);
			this.panelInfo[3].value = _value;
		}

		// Token: 0x0600A30A RID: 41738 RVA: 0x0042C518 File Offset: 0x0042A918
		private void OnEndEditVS(string _text)
		{
			float value = Mathf.Clamp(Utility.StringToFloat(_text), 0.01f, 20f);
			this.m_OCIItem.SetPatternVS(0, value);
			this.panelInfo[3].value = value;
		}

		// Token: 0x0600A30B RID: 41739 RVA: 0x0042C55A File Offset: 0x0042A95A
		private void OnClickVSDef()
		{
			this.panelInfo[3].value = 1f;
		}

		// Token: 0x0600A30C RID: 41740 RVA: 0x0042C572 File Offset: 0x0042A972
		private void OnValueChangeRot(float _value)
		{
			if (this.isUpdateInfo)
			{
				return;
			}
			this.m_OCIItem.SetPatternRot(0, _value);
			this.panelInfo[4].value = _value;
		}

		// Token: 0x0600A30D RID: 41741 RVA: 0x0042C5A0 File Offset: 0x0042A9A0
		private void OnEndEditRot(string _text)
		{
			float value = Mathf.Clamp(Utility.StringToFloat(_text), -1f, 1f);
			this.m_OCIItem.SetPatternRot(0, value);
			this.panelInfo[4].value = value;
		}

		// Token: 0x0600A30E RID: 41742 RVA: 0x0042C5E4 File Offset: 0x0042A9E4
		private void OnValueChangeAlpha(float _value)
		{
			if (this.isUpdateInfo)
			{
				return;
			}
			foreach (OCIItem ociitem in from v in Studio.GetSelectObjectCtrl()
			where v.kind == 1
			select v as OCIItem)
			{
				ociitem.SetAlpha(_value);
			}
			this.inputAlpha.value = _value;
		}

		// Token: 0x0600A30F RID: 41743 RVA: 0x0042C698 File Offset: 0x0042AA98
		private void OnEndEditAlpha(string _text)
		{
			float num = Mathf.Clamp(Utility.StringToFloat(_text), 0f, 1f);
			foreach (OCIItem ociitem in from v in Studio.GetSelectObjectCtrl()
			where v.kind == 1
			select v as OCIItem)
			{
				ociitem.SetAlpha(num);
			}
			this.inputAlpha.value = num;
		}

		// Token: 0x0600A310 RID: 41744 RVA: 0x0042C758 File Offset: 0x0042AB58
		private void OnClickEmissionColor()
		{
			if (Singleton<Studio>.Instance.colorPalette.Check("発光色"))
			{
				this.isColorFunc = false;
				Singleton<Studio>.Instance.colorPalette.visible = false;
				return;
			}
			IEnumerable<OCIItem> array = from v in Studio.GetSelectObjectCtrl()
			where v.kind == 1
			select v as OCIItem;
			Singleton<Studio>.Instance.colorPalette.Setup("発光色", this.m_OCIItem.itemInfo.EmissionColor, delegate(Color _c)
			{
				foreach (OCIItem ociitem in array)
				{
					ociitem.itemInfo.EmissionColor = _c;
					ociitem.SetEmissionColor(ociitem.itemInfo.emissionColor);
				}
				this.emissionInfo.color.color = _c;
			}, false);
			this.isColorFunc = true;
		}

		// Token: 0x0600A311 RID: 41745 RVA: 0x0042C82C File Offset: 0x0042AC2C
		private void OnClickEmissionColorDef()
		{
			foreach (OCIItem ociitem in from v in Studio.GetSelectObjectCtrl()
			where v.kind == 1
			select v as OCIItem)
			{
				ociitem.SetEmissionColor(ociitem.itemComponent.DefEmissionColor);
			}
			this.emissionInfo.color.color = this.m_OCIItem.itemComponent.DefEmissionColor;
			this.isColorFunc = false;
			Singleton<Studio>.Instance.colorPalette.visible = false;
		}

		// Token: 0x0600A312 RID: 41746 RVA: 0x0042C90C File Offset: 0x0042AD0C
		private void OnValueChangeEmissionPower(float _value)
		{
			if (this.isUpdateInfo)
			{
				return;
			}
			foreach (OCIItem ociitem in from v in Studio.GetSelectObjectCtrl()
			where v.kind == 1
			select v as OCIItem)
			{
				ociitem.SetEmissionPower(_value);
			}
			this.emissionInfo.input.value = _value;
		}

		// Token: 0x0600A313 RID: 41747 RVA: 0x0042C9C8 File Offset: 0x0042ADC8
		private void OnEndEditEmissionPower(string _text)
		{
			float num = Mathf.Clamp(Utility.StringToFloat(_text), 0f, 1f);
			foreach (OCIItem ociitem in from v in Studio.GetSelectObjectCtrl()
			where v.kind == 1
			select v as OCIItem)
			{
				ociitem.SetEmissionPower(num);
			}
			this.emissionInfo.input.value = num;
		}

		// Token: 0x0600A314 RID: 41748 RVA: 0x0042CA8C File Offset: 0x0042AE8C
		private void OnClickEmissionPowerDef()
		{
			foreach (OCIItem ociitem in from v in Studio.GetSelectObjectCtrl()
			where v.kind == 1
			select v as OCIItem)
			{
				ociitem.SetEmissionPower(ociitem.itemComponent.defEmissionStrength);
			}
			this.emissionInfo.input.value = this.m_OCIItem.itemComponent.defEmissionStrength;
		}

		// Token: 0x0600A315 RID: 41749 RVA: 0x0042CB54 File Offset: 0x0042AF54
		private void OnValueChangeLightCancel(float _value)
		{
			if (this.isUpdateInfo)
			{
				return;
			}
			this.m_OCIItem.SetLightCancel(_value);
			this.inputLightCancel.value = _value;
		}

		// Token: 0x0600A316 RID: 41750 RVA: 0x0042CB7C File Offset: 0x0042AF7C
		private void OnEndEditLightCancel(string _text)
		{
			float num = Mathf.Clamp(Utility.StringToFloat(_text), 0f, 1f);
			this.m_OCIItem.SetLightCancel(num);
			this.inputLightCancel.value = num;
		}

		// Token: 0x0600A317 RID: 41751 RVA: 0x0042CBB7 File Offset: 0x0042AFB7
		private void OnClickLightCancelDef()
		{
			this.m_OCIItem.SetLightCancel(this.m_OCIItem.itemComponent.defLightCancel);
			this.inputLightCancel.value = this.m_OCIItem.itemComponent.defLightCancel;
		}

		// Token: 0x0600A318 RID: 41752 RVA: 0x0042CBEF File Offset: 0x0042AFEF
		private void OnValueChangedFK(bool _value)
		{
			if (this.isUpdateInfo)
			{
				return;
			}
			this.m_OCIItem.ActiveFK(_value);
			this.kinematicInfo.toggleDynamicBone.interactable = this.m_OCIItem.isDynamicBone;
		}

		// Token: 0x0600A319 RID: 41753 RVA: 0x0042CC24 File Offset: 0x0042B024
		private void OnValueChangedDynamicBone(bool _value)
		{
			if (this.isUpdateInfo)
			{
				return;
			}
			this.m_OCIItem.ActiveDynamicBone(_value);
		}

		// Token: 0x0600A31A RID: 41754 RVA: 0x0042CC40 File Offset: 0x0042B040
		private void OnValueChangedOption(bool _value)
		{
			if (this.isUpdateInfo)
			{
				return;
			}
			foreach (OCIItem ociitem in from v in Studio.GetSelectObjectCtrl()
			where v.kind == 1
			select v as OCIItem)
			{
				ociitem.SetOptionVisible(_value);
			}
		}

		// Token: 0x0600A31B RID: 41755 RVA: 0x0042CCE8 File Offset: 0x0042B0E8
		private void OnClickPattern(int _idx)
		{
			if (this.cgPattern.alpha != 0f)
			{
				this.cgPattern.Enable(false, false);
				return;
			}
			Singleton<Studio>.Instance.patternSelectListCtrl.onChangeItemFunc = delegate(int _index)
			{
				string text = string.Empty;
				foreach (OCIItem ociitem in from v in Studio.GetSelectObjectCtrl()
				where v.kind == 1
				select v as OCIItem)
				{
					string text2 = ociitem.SetPatternTex(_idx, _index);
					if (text.IsNullOrEmpty())
					{
						text = text2;
					}
				}
				this.colorInfo[_idx].textPattern = Path.GetFileNameWithoutExtension(text);
				this.cgPattern.Enable(false, false);
			};
			this.cgPattern.Enable(true, false);
		}

		// Token: 0x0600A31C RID: 41756 RVA: 0x0042CD54 File Offset: 0x0042B154
		private void OnValueChangedAnimePattern(int _value)
		{
			if (this.isUpdateInfo)
			{
				return;
			}
			this.m_OCIItem.SetAnimePattern(_value);
		}

		// Token: 0x0600A31D RID: 41757 RVA: 0x0042CD70 File Offset: 0x0042B170
		private string ConvertString(float _t)
		{
			return ((int)Mathf.Lerp(0f, 100f, _t)).ToString();
		}

		// Token: 0x0600A31E RID: 41758 RVA: 0x0042CD9C File Offset: 0x0042B19C
		private void Awake()
		{
			this.panelList.Init();
			for (int i = 0; i < 3; i++)
			{
				int no = i;
				this.colorInfo[i]._colorMain.buttonColor.OnClickAsObservable().Subscribe(delegate(Unit _)
				{
					this.OnClickColorMain(no);
				});
				this.colorInfo[i]._colorMain.buttonColorDefault.OnClickAsObservable().Subscribe(delegate(Unit _)
				{
					this.OnClickColorMainDef(no);
				});
				this.colorInfo[i].inputMetallic.slider.onValueChanged.AddListener(delegate(float f)
				{
					this.OnValueChangeMetallic(no, f);
				});
				this.colorInfo[i].inputMetallic.input.onEndEdit.AddListener(delegate(string s)
				{
					this.OnEndEditMetallic(no, s);
				});
				this.colorInfo[i].inputMetallic.buttonDefault.OnClickAsObservable().Subscribe(delegate(Unit _)
				{
					this.OnClickMetallicDef(no);
				});
				this.colorInfo[i].inputGlossiness.slider.onValueChanged.AddListener(delegate(float f)
				{
					this.OnValueChangeGlossiness(no, f);
				});
				this.colorInfo[i].inputGlossiness.input.onEndEdit.AddListener(delegate(string s)
				{
					this.OnEndEditGlossiness(no, s);
				});
				this.colorInfo[i].inputGlossiness.buttonDefault.OnClickAsObservable().Subscribe(delegate(Unit _)
				{
					this.OnClickGlossinessDef(no);
				});
				this.colorInfo[i]._buttonPattern.OnClickAsObservable().Subscribe(delegate(Unit _)
				{
					this.OnClickPattern(no);
				});
				this.colorInfo[i]._colorPattern.buttonColor.OnClickAsObservable().Subscribe(delegate(Unit _)
				{
					this.OnClickColorPattern(no);
				});
				this.colorInfo[i]._colorPattern.buttonColorDefault.OnClickAsObservable().Subscribe(delegate(Unit _)
				{
					this.OnClickColorPatternDef(no);
				});
				this.colorInfo[i]._toggleClamp.OnValueChangedAsObservable().Subscribe(delegate(bool f)
				{
					this.OnToggleColor(no, f);
				});
				this.colorInfo[i][0].slider.onValueChanged.AddListener(delegate(float f)
				{
					this.OnValueChangeUT(no, f);
				});
				this.colorInfo[i][0].input.onEndEdit.AddListener(delegate(string s)
				{
					this.OnEndEditUT(no, s);
				});
				this.colorInfo[i][0].buttonDefault.OnClickAsObservable().Subscribe(delegate(Unit _)
				{
					this.OnClickUTDef(no);
				});
				this.colorInfo[i][1].slider.onValueChanged.AddListener(delegate(float f)
				{
					this.OnValueChangeVT(no, f);
				});
				this.colorInfo[i][1].input.onEndEdit.AddListener(delegate(string s)
				{
					this.OnEndEditVT(no, s);
				});
				this.colorInfo[i][1].buttonDefault.OnClickAsObservable().Subscribe(delegate(Unit _)
				{
					this.OnClickVTDef(no);
				});
				this.colorInfo[i][2].slider.onValueChanged.AddListener(delegate(float f)
				{
					this.OnValueChangeUS(no, f);
				});
				this.colorInfo[i][2].input.onEndEdit.AddListener(delegate(string s)
				{
					this.OnEndEditUS(no, s);
				});
				this.colorInfo[i][2].buttonDefault.OnClickAsObservable().Subscribe(delegate(Unit _)
				{
					this.OnClickUSDef(no);
				});
				this.colorInfo[i][3].slider.onValueChanged.AddListener(delegate(float f)
				{
					this.OnValueChangeVS(no, f);
				});
				this.colorInfo[i][3].input.onEndEdit.AddListener(delegate(string s)
				{
					this.OnEndEditVS(no, s);
				});
				this.colorInfo[i][3].buttonDefault.OnClickAsObservable().Subscribe(delegate(Unit _)
				{
					this.OnClickVSDef(no);
				});
				this.colorInfo[i][4].slider.onValueChanged.AddListener(delegate(float f)
				{
					this.OnValueChangeRot(no, f);
				});
				this.colorInfo[i][4].input.onEndEdit.AddListener(delegate(string s)
				{
					this.OnEndEditRot(no, s);
				});
			}
			this.colorInfo[3]._colorMain.buttonColor.OnClickAsObservable().Subscribe(delegate(Unit _)
			{
				this.OnClickColor4();
			});
			this.colorInfo[3]._colorMain.buttonColorDefault.OnClickAsObservable().Subscribe(delegate(Unit _)
			{
				this.OnClickColor4Def();
			});
			this.panelInfo._buttonTex.OnClickAsObservable().Subscribe(delegate(Unit _)
			{
				this.OnClickPanel();
			});
			this.panelInfo._color.buttonColor.OnClickAsObservable().Subscribe(delegate(Unit _)
			{
				this.OnClickColorPanel();
			});
			this.panelInfo._toggleClamp.OnValueChangedAsObservable().Subscribe(delegate(bool f)
			{
				this.OnToggleColor(f);
			});
			this.panelInfo[0].slider.onValueChanged.AddListener(delegate(float f)
			{
				this.OnValueChangeUT(f);
			});
			this.panelInfo[0].input.onEndEdit.AddListener(delegate(string s)
			{
				this.OnEndEditUT(s);
			});
			this.panelInfo[0].buttonDefault.OnClickAsObservable().Subscribe(delegate(Unit _)
			{
				this.OnClickUTDef();
			});
			this.panelInfo[1].slider.onValueChanged.AddListener(delegate(float f)
			{
				this.OnValueChangeVT(f);
			});
			this.panelInfo[1].input.onEndEdit.AddListener(delegate(string s)
			{
				this.OnEndEditVT(s);
			});
			this.panelInfo[1].buttonDefault.OnClickAsObservable().Subscribe(delegate(Unit _)
			{
				this.OnClickVTDef();
			});
			this.panelInfo[2].slider.onValueChanged.AddListener(delegate(float f)
			{
				this.OnValueChangeUS(f);
			});
			this.panelInfo[2].input.onEndEdit.AddListener(delegate(string s)
			{
				this.OnEndEditUS(s);
			});
			this.panelInfo[2].buttonDefault.OnClickAsObservable().Subscribe(delegate(Unit _)
			{
				this.OnClickUSDef();
			});
			this.panelInfo[3].slider.onValueChanged.AddListener(delegate(float f)
			{
				this.OnValueChangeVS(f);
			});
			this.panelInfo[3].input.onEndEdit.AddListener(delegate(string s)
			{
				this.OnEndEditVS(s);
			});
			this.panelInfo[3].buttonDefault.OnClickAsObservable().Subscribe(delegate(Unit _)
			{
				this.OnClickVSDef();
			});
			this.panelInfo[4].slider.onValueChanged.AddListener(delegate(float f)
			{
				this.OnValueChangeRot(f);
			});
			this.panelInfo[4].input.onEndEdit.AddListener(delegate(string s)
			{
				this.OnEndEditRot(s);
			});
			this.inputAlpha.slider.onValueChanged.AddListener(delegate(float f)
			{
				this.OnValueChangeAlpha(f);
			});
			this.inputAlpha.input.onEndEdit.AddListener(delegate(string s)
			{
				this.OnEndEditAlpha(s);
			});
			this.emissionInfo.color.buttonColor.OnClickAsObservable().Subscribe(delegate(Unit _)
			{
				this.OnClickEmissionColor();
			});
			this.emissionInfo.color.buttonColorDefault.OnClickAsObservable().Subscribe(delegate(Unit _)
			{
				this.OnClickEmissionColorDef();
			});
			this.emissionInfo.input.slider.onValueChanged.AddListener(delegate(float f)
			{
				this.OnValueChangeEmissionPower(f);
			});
			this.emissionInfo.input.input.onEndEdit.AddListener(delegate(string s)
			{
				this.OnEndEditEmissionPower(s);
			});
			this.emissionInfo.input.buttonDefault.OnClickAsObservable().Subscribe(delegate(Unit _)
			{
				this.OnClickEmissionPowerDef();
			});
			this.inputLightCancel.slider.onValueChanged.AddListener(delegate(float f)
			{
				this.OnValueChangeLightCancel(f);
			});
			this.inputLightCancel.input.onEndEdit.AddListener(delegate(string s)
			{
				this.OnEndEditLightCancel(s);
			});
			this.inputLightCancel.buttonDefault.OnClickAsObservable().Subscribe(delegate(Unit _)
			{
				this.OnClickLightCancelDef();
			});
			this.kinematicInfo.toggleFK.onValueChanged.AddListener(new UnityAction<bool>(this.OnValueChangedFK));
			this.kinematicInfo.toggleDynamicBone.onValueChanged.AddListener(new UnityAction<bool>(this.OnValueChangedDynamicBone));
			this.optionInfo.toggleAll.onValueChanged.AddListener(new UnityAction<bool>(this.OnValueChangedOption));
			this.animeInfo.dropdownAnime.onValueChanged.AddListener(new UnityAction<int>(this.OnValueChangedAnimePattern));
			this.isUpdateInfo = false;
			this.m_Active = false;
			base.gameObject.SetActive(false);
		}

		// Token: 0x04008092 RID: 32914
		[SerializeField]
		private MPItemCtrl.ColorInfo[] colorInfo;

		// Token: 0x04008093 RID: 32915
		[SerializeField]
		private Sprite[] spriteBack;

		// Token: 0x04008094 RID: 32916
		[SerializeField]
		private MPItemCtrl.PanelInfo panelInfo;

		// Token: 0x04008095 RID: 32917
		[SerializeField]
		private MPItemCtrl.PanelList panelList;

		// Token: 0x04008096 RID: 32918
		[SerializeField]
		private MPItemCtrl.ColorCombination colorShadow;

		// Token: 0x04008097 RID: 32919
		[SerializeField]
		private MPItemCtrl.InputCombination inputAlpha;

		// Token: 0x04008098 RID: 32920
		[SerializeField]
		private MPItemCtrl.EmissionInfo emissionInfo;

		// Token: 0x04008099 RID: 32921
		[SerializeField]
		private MPItemCtrl.InputCombination inputLightCancel;

		// Token: 0x0400809A RID: 32922
		[SerializeField]
		private MPItemCtrl.LineInfo lineInfo;

		// Token: 0x0400809B RID: 32923
		[SerializeField]
		[Header("キネマティクス関係")]
		private MPItemCtrl.KinematicInfo kinematicInfo = new MPItemCtrl.KinematicInfo();

		// Token: 0x0400809C RID: 32924
		[SerializeField]
		[Header("オプション関係")]
		private MPItemCtrl.OptionInfo optionInfo = new MPItemCtrl.OptionInfo();

		// Token: 0x0400809D RID: 32925
		[SerializeField]
		[Header("パターン関係")]
		private CanvasGroup cgPattern;

		// Token: 0x0400809E RID: 32926
		[SerializeField]
		[Header("アニメ関係")]
		private MPItemCtrl.AnimeInfo animeInfo = new MPItemCtrl.AnimeInfo();

		// Token: 0x0400809F RID: 32927
		[SerializeField]
		private AnimeControl animeControl;

		// Token: 0x040080A0 RID: 32928
		[SerializeField]
		private MPCharCtrl mpCharCtrl;

		// Token: 0x040080A1 RID: 32929
		private OCIItem m_OCIItem;

		// Token: 0x040080A2 RID: 32930
		private bool m_Active;

		// Token: 0x040080A3 RID: 32931
		private bool isUpdateInfo;

		// Token: 0x040080A4 RID: 32932
		private bool isColorFunc;

		// Token: 0x02001317 RID: 4887
		[Serializable]
		private class ColorCombination
		{
			// Token: 0x17002242 RID: 8770
			// (set) Token: 0x0600A384 RID: 41860 RVA: 0x0042DAC4 File Offset: 0x0042BEC4
			public bool interactable
			{
				set
				{
					this.buttonColor.interactable = value;
					if (this.buttonColorDefault)
					{
						this.buttonColorDefault.interactable = value;
					}
				}
			}

			// Token: 0x17002243 RID: 8771
			// (get) Token: 0x0600A385 RID: 41861 RVA: 0x0042DAEE File Offset: 0x0042BEEE
			// (set) Token: 0x0600A386 RID: 41862 RVA: 0x0042DAFB File Offset: 0x0042BEFB
			public Color color
			{
				get
				{
					return this.imageColor.color;
				}
				set
				{
					this.imageColor.color = value;
				}
			}

			// Token: 0x17002244 RID: 8772
			// (set) Token: 0x0600A387 RID: 41863 RVA: 0x0042DB09 File Offset: 0x0042BF09
			public bool active
			{
				set
				{
					this.objRoot.SetActiveIfDifferent(value);
				}
			}

			// Token: 0x040080EB RID: 33003
			public GameObject objRoot;

			// Token: 0x040080EC RID: 33004
			public Image imageColor;

			// Token: 0x040080ED RID: 33005
			public Button buttonColor;

			// Token: 0x040080EE RID: 33006
			public Button buttonColorDefault;
		}

		// Token: 0x02001318 RID: 4888
		[Serializable]
		private class InputCombination
		{
			// Token: 0x17002245 RID: 8773
			// (set) Token: 0x0600A389 RID: 41865 RVA: 0x0042DB20 File Offset: 0x0042BF20
			public bool interactable
			{
				set
				{
					this.input.interactable = value;
					this.slider.interactable = value;
					if (this.buttonDefault)
					{
						this.buttonDefault.interactable = value;
					}
				}
			}

			// Token: 0x17002246 RID: 8774
			// (get) Token: 0x0600A38A RID: 41866 RVA: 0x0042DB56 File Offset: 0x0042BF56
			// (set) Token: 0x0600A38B RID: 41867 RVA: 0x0042DB63 File Offset: 0x0042BF63
			public string text
			{
				get
				{
					return this.input.text;
				}
				set
				{
					this.input.text = value;
					this.slider.value = Utility.StringToFloat(value);
				}
			}

			// Token: 0x17002247 RID: 8775
			// (get) Token: 0x0600A38C RID: 41868 RVA: 0x0042DB82 File Offset: 0x0042BF82
			// (set) Token: 0x0600A38D RID: 41869 RVA: 0x0042DB8F File Offset: 0x0042BF8F
			public float value
			{
				get
				{
					return this.slider.value;
				}
				set
				{
					this.slider.value = value;
					this.input.text = value.ToString("0.0");
				}
			}

			// Token: 0x17002248 RID: 8776
			// (set) Token: 0x0600A38E RID: 41870 RVA: 0x0042DBB4 File Offset: 0x0042BFB4
			public bool active
			{
				set
				{
					this.objRoot.SetActiveIfDifferent(value);
				}
			}

			// Token: 0x040080EF RID: 33007
			public GameObject objRoot;

			// Token: 0x040080F0 RID: 33008
			public TMP_InputField input;

			// Token: 0x040080F1 RID: 33009
			public Slider slider;

			// Token: 0x040080F2 RID: 33010
			public Button buttonDefault;
		}

		// Token: 0x02001319 RID: 4889
		[Serializable]
		private class ColorInfo
		{
			// Token: 0x17002249 RID: 8777
			// (set) Token: 0x0600A390 RID: 41872 RVA: 0x0042DBF7 File Offset: 0x0042BFF7
			public Color colorMain
			{
				set
				{
					this._colorMain.imageColor.color = value;
				}
			}

			// Token: 0x1700224A RID: 8778
			// (set) Token: 0x0600A391 RID: 41873 RVA: 0x0042DC0A File Offset: 0x0042C00A
			public string textPattern
			{
				set
				{
					this._textPattern.text = value;
				}
			}

			// Token: 0x1700224B RID: 8779
			// (set) Token: 0x0600A392 RID: 41874 RVA: 0x0042DC18 File Offset: 0x0042C018
			public Color colorPattern
			{
				set
				{
					this._colorPattern.imageColor.color = value;
				}
			}

			// Token: 0x1700224C RID: 8780
			// (set) Token: 0x0600A393 RID: 41875 RVA: 0x0042DC2B File Offset: 0x0042C02B
			public bool isOn
			{
				set
				{
					this._toggleClamp.isOn = value;
				}
			}

			// Token: 0x1700224D RID: 8781
			public MPItemCtrl.InputCombination this[int _idx]
			{
				get
				{
					return this._input.SafeGet(_idx);
				}
			}

			// Token: 0x1700224E RID: 8782
			// (get) Token: 0x0600A395 RID: 41877 RVA: 0x0042DC47 File Offset: 0x0042C047
			// (set) Token: 0x0600A396 RID: 41878 RVA: 0x0042DC54 File Offset: 0x0042C054
			public bool enable
			{
				get
				{
					return this.objRoot.activeSelf;
				}
				set
				{
					if (this.objRoot.activeSelf != value)
					{
						this.objRoot.SetActive(value);
					}
				}
			}

			// Token: 0x1700224F RID: 8783
			// (set) Token: 0x0600A397 RID: 41879 RVA: 0x0042DC73 File Offset: 0x0042C073
			public bool EnableMetallic
			{
				set
				{
					this.objMetallic.SetActiveIfDifferent(value);
				}
			}

			// Token: 0x17002250 RID: 8784
			// (set) Token: 0x0600A398 RID: 41880 RVA: 0x0042DC82 File Offset: 0x0042C082
			public bool EnablePattern
			{
				set
				{
					this.objPattern.SetActiveIfDifferent(value);
				}
			}

			// Token: 0x040080F3 RID: 33011
			public GameObject objRoot;

			// Token: 0x040080F4 RID: 33012
			public MPItemCtrl.ColorCombination _colorMain = new MPItemCtrl.ColorCombination();

			// Token: 0x040080F5 RID: 33013
			[Header("メタリック関係")]
			public GameObject objMetallic;

			// Token: 0x040080F6 RID: 33014
			public MPItemCtrl.InputCombination inputMetallic = new MPItemCtrl.InputCombination();

			// Token: 0x040080F7 RID: 33015
			public MPItemCtrl.InputCombination inputGlossiness = new MPItemCtrl.InputCombination();

			// Token: 0x040080F8 RID: 33016
			[Header("柄関係")]
			public GameObject objPattern;

			// Token: 0x040080F9 RID: 33017
			public Button _buttonPattern;

			// Token: 0x040080FA RID: 33018
			public TextMeshProUGUI _textPattern;

			// Token: 0x040080FB RID: 33019
			public MPItemCtrl.ColorCombination _colorPattern = new MPItemCtrl.ColorCombination();

			// Token: 0x040080FC RID: 33020
			public Toggle _toggleClamp;

			// Token: 0x040080FD RID: 33021
			public MPItemCtrl.InputCombination[] _input;
		}

		// Token: 0x0200131A RID: 4890
		[Serializable]
		private class ColorInputCombination
		{
			// Token: 0x17002251 RID: 8785
			// (set) Token: 0x0600A39A RID: 41882 RVA: 0x0042DCAF File Offset: 0x0042C0AF
			public bool active
			{
				set
				{
					this.objRoot.SetActiveIfDifferent(value);
				}
			}

			// Token: 0x040080FE RID: 33022
			public GameObject objRoot;

			// Token: 0x040080FF RID: 33023
			public MPItemCtrl.ColorCombination color = new MPItemCtrl.ColorCombination();

			// Token: 0x04008100 RID: 33024
			public MPItemCtrl.InputCombination input = new MPItemCtrl.InputCombination();
		}

		// Token: 0x0200131B RID: 4891
		[Serializable]
		private class EmissionInfo : MPItemCtrl.ColorInputCombination
		{
		}

		// Token: 0x0200131C RID: 4892
		[Serializable]
		private class LineInfo : MPItemCtrl.ColorInputCombination
		{
		}

		// Token: 0x0200131D RID: 4893
		[Serializable]
		private class KinematicInfo
		{
			// Token: 0x17002252 RID: 8786
			// (get) Token: 0x0600A39E RID: 41886 RVA: 0x0042DCD6 File Offset: 0x0042C0D6
			// (set) Token: 0x0600A39F RID: 41887 RVA: 0x0042DCE3 File Offset: 0x0042C0E3
			public bool Active
			{
				get
				{
					return this.objRoot.activeSelf;
				}
				set
				{
					this.objRoot.SetActiveIfDifferent(value);
				}
			}

			// Token: 0x04008101 RID: 33025
			public GameObject objRoot;

			// Token: 0x04008102 RID: 33026
			public Toggle toggleFK;

			// Token: 0x04008103 RID: 33027
			public Toggle toggleDynamicBone;
		}

		// Token: 0x0200131E RID: 4894
		[Serializable]
		private class PanelInfo
		{
			// Token: 0x17002253 RID: 8787
			// (set) Token: 0x0600A3A1 RID: 41889 RVA: 0x0042DCFA File Offset: 0x0042C0FA
			public string textTex
			{
				set
				{
					this._textTex.text = value;
				}
			}

			// Token: 0x17002254 RID: 8788
			// (set) Token: 0x0600A3A2 RID: 41890 RVA: 0x0042DD08 File Offset: 0x0042C108
			public Color color
			{
				set
				{
					this._color.imageColor.color = value;
				}
			}

			// Token: 0x17002255 RID: 8789
			// (set) Token: 0x0600A3A3 RID: 41891 RVA: 0x0042DD1B File Offset: 0x0042C11B
			public bool isOn
			{
				set
				{
					this._toggleClamp.isOn = value;
				}
			}

			// Token: 0x17002256 RID: 8790
			public MPItemCtrl.InputCombination this[int _idx]
			{
				get
				{
					return this._input.SafeGet(_idx);
				}
			}

			// Token: 0x17002257 RID: 8791
			// (get) Token: 0x0600A3A5 RID: 41893 RVA: 0x0042DD37 File Offset: 0x0042C137
			// (set) Token: 0x0600A3A6 RID: 41894 RVA: 0x0042DD44 File Offset: 0x0042C144
			public bool enable
			{
				get
				{
					return this.objRoot.activeSelf;
				}
				set
				{
					this.objRoot.SetActiveIfDifferent(value);
				}
			}

			// Token: 0x04008104 RID: 33028
			public GameObject objRoot;

			// Token: 0x04008105 RID: 33029
			public Button _buttonTex;

			// Token: 0x04008106 RID: 33030
			public TextMeshProUGUI _textTex;

			// Token: 0x04008107 RID: 33031
			public MPItemCtrl.ColorCombination _color;

			// Token: 0x04008108 RID: 33032
			public Toggle _toggleClamp;

			// Token: 0x04008109 RID: 33033
			public MPItemCtrl.InputCombination[] _input;
		}

		// Token: 0x0200131F RID: 4895
		[Serializable]
		private class PanelList
		{
			// Token: 0x17002258 RID: 8792
			// (get) Token: 0x0600A3A8 RID: 41896 RVA: 0x0042DD78 File Offset: 0x0042C178
			// (set) Token: 0x0600A3A9 RID: 41897 RVA: 0x0042DD85 File Offset: 0x0042C185
			public bool active
			{
				get
				{
					return this.objRoot.activeSelf;
				}
				set
				{
					this.objRoot.SetActiveIfDifferent(value);
				}
			}

			// Token: 0x0600A3AA RID: 41898 RVA: 0x0042DD94 File Offset: 0x0042C194
			public void Init()
			{
				for (int i = 0; i < this.transformRoot.childCount; i++)
				{
					UnityEngine.Object.Destroy(this.transformRoot.GetChild(i).gameObject);
				}
				this.transformRoot.DetachChildren();
				IEnumerable<string> files = Directory.GetFiles(UserData.Create(BackgroundList.dirName), "*.png");
				if (MPItemCtrl.PanelList.<>f__mg$cache0 == null)
				{
					MPItemCtrl.PanelList.<>f__mg$cache0 = new Func<string, string>(Path.GetFileName);
				}
				this.listPath = files.Select(MPItemCtrl.PanelList.<>f__mg$cache0).ToList<string>();
				this.CreateNode(-1, "なし");
				int count = this.listPath.Count;
				for (int j = 0; j < count; j++)
				{
					this.CreateNode(j, Path.GetFileNameWithoutExtension(this.listPath[j]));
				}
			}

			// Token: 0x0600A3AB RID: 41899 RVA: 0x0042DE64 File Offset: 0x0042C264
			public void Setup(string _file, Action<string> _actUpdate)
			{
				this.SetSelect(this.select, false);
				this.select = this.listPath.FindIndex((string s) => s == _file);
				this.SetSelect(this.select, true);
				this.actUpdatePath = _actUpdate;
				this.active = true;
			}

			// Token: 0x0600A3AC RID: 41900 RVA: 0x0042DEC4 File Offset: 0x0042C2C4
			private void OnClickSelect(int _idx)
			{
				this.SetSelect(this.select, false);
				this.select = _idx;
				this.SetSelect(this.select, true);
				if (this.actUpdatePath != null)
				{
					this.actUpdatePath((this.select == -1) ? string.Empty : this.listPath[_idx]);
				}
				this.active = false;
			}

			// Token: 0x0600A3AD RID: 41901 RVA: 0x0042DF34 File Offset: 0x0042C334
			private void SetSelect(int _idx, bool _flag)
			{
				StudioNode studioNode = null;
				if (this.dicNode.TryGetValue(_idx, out studioNode))
				{
					studioNode.select = _flag;
				}
			}

			// Token: 0x0600A3AE RID: 41902 RVA: 0x0042DF60 File Offset: 0x0042C360
			private void CreateNode(int _idx, string _text)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.objectNode);
				gameObject.transform.SetParent(this.transformRoot, false);
				StudioNode component = gameObject.GetComponent<StudioNode>();
				component.active = true;
				component.addOnClick = delegate()
				{
					this.OnClickSelect(_idx);
				};
				component.text = _text;
				this.dicNode.Add(_idx, component);
			}

			// Token: 0x0400810A RID: 33034
			[SerializeField]
			private GameObject objRoot;

			// Token: 0x0400810B RID: 33035
			[SerializeField]
			private GameObject objectNode;

			// Token: 0x0400810C RID: 33036
			[SerializeField]
			private Transform transformRoot;

			// Token: 0x0400810D RID: 33037
			public Action<string> actUpdatePath;

			// Token: 0x0400810E RID: 33038
			private List<string> listPath = new List<string>();

			// Token: 0x0400810F RID: 33039
			private Dictionary<int, StudioNode> dicNode = new Dictionary<int, StudioNode>();

			// Token: 0x04008110 RID: 33040
			private int select = -1;

			// Token: 0x04008111 RID: 33041
			[CompilerGenerated]
			private static Func<string, string> <>f__mg$cache0;
		}

		// Token: 0x02001320 RID: 4896
		[Serializable]
		private class OptionInfo
		{
			// Token: 0x17002259 RID: 8793
			// (get) Token: 0x0600A3B0 RID: 41904 RVA: 0x0042E011 File Offset: 0x0042C411
			// (set) Token: 0x0600A3B1 RID: 41905 RVA: 0x0042E01E File Offset: 0x0042C41E
			public bool Active
			{
				get
				{
					return this.objRoot.activeSelf;
				}
				set
				{
					this.objRoot.SetActiveIfDifferent(value);
				}
			}

			// Token: 0x04008112 RID: 33042
			public GameObject objRoot;

			// Token: 0x04008113 RID: 33043
			public Toggle toggleAll;
		}

		// Token: 0x02001321 RID: 4897
		[Serializable]
		private class AnimeInfo
		{
			// Token: 0x1700225A RID: 8794
			// (get) Token: 0x0600A3B3 RID: 41907 RVA: 0x0042E035 File Offset: 0x0042C435
			// (set) Token: 0x0600A3B4 RID: 41908 RVA: 0x0042E042 File Offset: 0x0042C442
			public bool Active
			{
				get
				{
					return this.objRoot.activeSelf;
				}
				set
				{
					this.objRoot.SetActiveIfDifferent(value);
				}
			}

			// Token: 0x0600A3B5 RID: 41909 RVA: 0x0042E054 File Offset: 0x0042C454
			public void InitDropdown(OCIItem _ocii)
			{
				this.dropdownAnime.ClearOptions();
				if (_ocii == null)
				{
					return;
				}
				if (_ocii.itemComponent == null)
				{
					return;
				}
				ItemComponent.AnimeInfo[] animeInfos = _ocii.itemComponent.animeInfos;
				List<TMP_Dropdown.OptionData> list;
				if (animeInfos.IsNullOrEmpty<ItemComponent.AnimeInfo>())
				{
					list = new List<TMP_Dropdown.OptionData>();
				}
				else
				{
					list = (from c in animeInfos
					select new TMP_Dropdown.OptionData(c.name)).ToList<TMP_Dropdown.OptionData>();
				}
				List<TMP_Dropdown.OptionData> options = list;
				this.dropdownAnime.options = options;
				this.dropdownAnime.value = _ocii.itemInfo.animePattern;
			}

			// Token: 0x04008114 RID: 33044
			public GameObject objRoot;

			// Token: 0x04008115 RID: 33045
			public TMP_Dropdown dropdownAnime;
		}
	}
}
