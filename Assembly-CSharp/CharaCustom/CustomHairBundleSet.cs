using System;
using System.Collections.Generic;
using AIChara;
using Illusion.Extensions;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace CharaCustom
{
	// Token: 0x0200099C RID: 2460
	public class CustomHairBundleSet : MonoBehaviour
	{
		// Token: 0x17000DCB RID: 3531
		// (get) Token: 0x060046D2 RID: 18130 RVA: 0x001B3571 File Offset: 0x001B1971
		private CustomBase customBase
		{
			get
			{
				return Singleton<CustomBase>.Instance;
			}
		}

		// Token: 0x17000DCC RID: 3532
		// (get) Token: 0x060046D3 RID: 18131 RVA: 0x001B3578 File Offset: 0x001B1978
		private ChaControl chaCtrl
		{
			get
			{
				return this.customBase.chaCtrl;
			}
		}

		// Token: 0x17000DCD RID: 3533
		// (get) Token: 0x060046D4 RID: 18132 RVA: 0x001B3585 File Offset: 0x001B1985
		private ChaFileHair hair
		{
			get
			{
				return this.chaCtrl.fileHair;
			}
		}

		// Token: 0x17000DCE RID: 3534
		// (get) Token: 0x060046D5 RID: 18133 RVA: 0x001B3592 File Offset: 0x001B1992
		private CustomBase.CustomSettingSave.HairCtrlSetting hairCtrlSetting
		{
			get
			{
				return this.customBase.customSettingSave.hairCtrlSetting;
			}
		}

		// Token: 0x17000DCF RID: 3535
		// (get) Token: 0x060046D6 RID: 18134 RVA: 0x001B35A4 File Offset: 0x001B19A4
		private bool ctrlTogether
		{
			get
			{
				return this.hair.ctrlTogether;
			}
		}

		// Token: 0x17000DD0 RID: 3536
		// (get) Token: 0x060046D7 RID: 18135 RVA: 0x001B35B1 File Offset: 0x001B19B1
		// (set) Token: 0x060046D8 RID: 18136 RVA: 0x001B35B9 File Offset: 0x001B19B9
		public int parts { get; set; } = -1;

		// Token: 0x17000DD1 RID: 3537
		// (get) Token: 0x060046D9 RID: 18137 RVA: 0x001B35C2 File Offset: 0x001B19C2
		// (set) Token: 0x060046DA RID: 18138 RVA: 0x001B35CA File Offset: 0x001B19CA
		public int idx { get; set; } = -1;

		// Token: 0x17000DD2 RID: 3538
		// (get) Token: 0x060046DB RID: 18139 RVA: 0x001B35D3 File Offset: 0x001B19D3
		// (set) Token: 0x060046DC RID: 18140 RVA: 0x001B35DB File Offset: 0x001B19DB
		public bool reset { get; set; }

		// Token: 0x17000DD3 RID: 3539
		// (get) Token: 0x060046DD RID: 18141 RVA: 0x001B35E4 File Offset: 0x001B19E4
		// (set) Token: 0x060046DE RID: 18142 RVA: 0x001B35EC File Offset: 0x001B19EC
		public CustomGuideObject cmpGuid { get; private set; }

		// Token: 0x17000DD4 RID: 3540
		// (get) Token: 0x060046DF RID: 18143 RVA: 0x001B35F5 File Offset: 0x001B19F5
		// (set) Token: 0x060046E0 RID: 18144 RVA: 0x001B35FD File Offset: 0x001B19FD
		public bool isDrag { get; private set; }

		// Token: 0x060046E1 RID: 18145 RVA: 0x001B3608 File Offset: 0x001B1A08
		public void UpdateCustomUI()
		{
			if (this.parts == -1 || this.idx == -1)
			{
				return;
			}
			ChaFileHair.PartsInfo.BundleInfo bundleInfo;
			if (!this.hair.parts[this.parts].dictBundle.TryGetValue(this.idx, out bundleInfo))
			{
				return;
			}
			this.ssMove[0].SetSliderValue(bundleInfo.moveRate.x);
			this.ssMove[1].SetSliderValue(bundleInfo.moveRate.y);
			this.ssMove[2].SetSliderValue(bundleInfo.moveRate.z);
			this.ssRot[0].SetSliderValue(bundleInfo.rotRate.x);
			this.ssRot[1].SetSliderValue(bundleInfo.rotRate.y);
			this.ssRot[2].SetSliderValue(bundleInfo.rotRate.z);
		}

		// Token: 0x060046E2 RID: 18146 RVA: 0x001B3700 File Offset: 0x001B1B00
		public void SetControllerTransform()
		{
			Transform trfCorrect = this.chaCtrl.cmpHair[this.parts].boneInfo[this.idx].trfCorrect;
			if (null == trfCorrect)
			{
				return;
			}
			if (null == this.cmpGuid)
			{
				return;
			}
			this.cmpGuid.amount.position = trfCorrect.position;
			this.cmpGuid.amount.rotation = trfCorrect.eulerAngles;
		}

		// Token: 0x060046E3 RID: 18147 RVA: 0x001B377C File Offset: 0x001B1B7C
		public void SetHairTransform(bool updateInfo, int ctrlAxisType)
		{
			Transform trfCorrect = this.chaCtrl.cmpHair[this.parts].boneInfo[this.idx].trfCorrect;
			if (null == trfCorrect)
			{
				return;
			}
			if (null == this.cmpGuid)
			{
				return;
			}
			int flag = 1;
			switch (ctrlAxisType)
			{
			case 0:
				flag = 1;
				break;
			case 1:
				flag = 2;
				break;
			case 2:
				flag = 4;
				break;
			case 3:
				flag = 7;
				break;
			}
			if (this.customBase.customSettingSave.hairCtrlSetting.controllerType == 0)
			{
				trfCorrect.position = this.cmpGuid.amount.position;
				if (updateInfo)
				{
					this.chaCtrl.SetHairCorrectPosValue(this.parts, this.idx, this.cmpGuid.amount.position, flag);
					this.chaCtrl.ChangeSettingHairCorrectPos(this.parts, this.idx);
				}
			}
			else
			{
				trfCorrect.eulerAngles = this.cmpGuid.amount.rotation;
				if (updateInfo)
				{
					this.chaCtrl.SetHairCorrectRotValue(this.parts, this.idx, this.cmpGuid.amount.rotation, flag);
					this.chaCtrl.ChangeSettingHairCorrectRot(this.parts, this.idx);
				}
			}
			this.UpdateCustomUI();
		}

		// Token: 0x060046E4 RID: 18148 RVA: 0x001B38E8 File Offset: 0x001B1CE8
		public void CreateGuid(GameObject objParent, CmpHair.BoneInfo binfo)
		{
			Transform parent = (!(null == objParent)) ? objParent.transform : null;
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.tmpObjGuid);
			gameObject.SetActiveIfDifferent(true);
			gameObject.transform.SetParent(parent);
			this.cmpGuid = gameObject.GetComponent<CustomGuideObject>();
			this.cmpGuid.SetMode(this.hairCtrlSetting.controllerType);
			this.cmpGuid.speedMove = this.hairCtrlSetting.controllerSpeed;
			this.cmpGuid.scaleAxis = this.hairCtrlSetting.controllerScale;
			this.cmpGuid.UpdateScale();
			ObjectCategoryBehaviour component = gameObject.GetComponent<ObjectCategoryBehaviour>();
			CustomGuideLimit component2 = component.GetObject(0).GetComponent<CustomGuideLimit>();
			component2.limited = true;
			component2.trfParent = binfo.trfCorrect.parent;
			component2.limitMin = binfo.posMin;
			component2.limitMax = binfo.posMax;
			CustomGuideLimit component3 = component.GetObject(1).GetComponent<CustomGuideLimit>();
			component3.limited = true;
			component3.trfParent = binfo.trfCorrect.parent;
			component3.limitMin = binfo.rotMin;
			component3.limitMax = binfo.rotMax;
		}

		// Token: 0x060046E5 RID: 18149 RVA: 0x001B3A10 File Offset: 0x001B1E10
		public void Initialize(int _parts, int _idx, int titleNo)
		{
			this.parts = _parts;
			this.idx = _idx;
			if (this.parts == -1 || this.idx == -1)
			{
				return;
			}
			ChaFileHair.PartsInfo.BundleInfo bi;
			if (this.hair.parts[this.parts].dictBundle.TryGetValue(this.idx, out bi))
			{
				this.ssMove[0].SetInputTextValue(CustomBase.ConvertTextFromRate(0, 100, bi.moveRate.x));
				this.ssMove[1].SetInputTextValue(CustomBase.ConvertTextFromRate(0, 100, bi.moveRate.y));
				this.ssMove[2].SetInputTextValue(CustomBase.ConvertTextFromRate(0, 100, bi.moveRate.z));
				this.ssRot[0].SetInputTextValue(CustomBase.ConvertTextFromRate(0, 100, bi.rotRate.x));
				this.ssRot[1].SetInputTextValue(CustomBase.ConvertTextFromRate(0, 100, bi.rotRate.y));
				this.ssRot[2].SetInputTextValue(CustomBase.ConvertTextFromRate(0, 100, bi.rotRate.z));
				Vector3 vDefPosRate;
				this.chaCtrl.GetDefaultHairCorrectPosRate(this.parts, this.idx, out vDefPosRate);
				Vector3 vDefRotRate;
				this.chaCtrl.GetDefaultHairCorrectRotRate(this.parts, this.idx, out vDefRotRate);
				this.ssMove[0].onChange = delegate(float value)
				{
					Vector3 moveRate = new Vector3(value, bi.moveRate.y, bi.moveRate.z);
					if (this.ctrlTogether && !this.reset && !this.cvsH_Hair.allReset)
					{
						foreach (KeyValuePair<int, ChaFileHair.PartsInfo.BundleInfo> keyValuePair in this.hair.parts[this.parts].dictBundle)
						{
							keyValuePair.Value.moveRate = moveRate;
						}
						this.chaCtrl.ChangeSettingHairCorrectPosAll(this.parts);
						this.cvsH_Hair.UpdateAllBundleUI(this.idx);
					}
					else
					{
						bi.moveRate = moveRate;
						this.chaCtrl.ChangeSettingHairCorrectPos(this.parts, this.idx);
					}
				};
				this.ssMove[0].onSetDefaultValue = delegate()
				{
					this.reset = true;
					return vDefPosRate.x;
				};
				this.ssMove[0].onEndSetDefaultValue = delegate()
				{
					this.reset = false;
				};
				this.ssMove[1].onChange = delegate(float value)
				{
					Vector3 moveRate = new Vector3(bi.moveRate.x, value, bi.moveRate.z);
					if (this.ctrlTogether && !this.reset && !this.cvsH_Hair.allReset)
					{
						foreach (KeyValuePair<int, ChaFileHair.PartsInfo.BundleInfo> keyValuePair in this.hair.parts[this.parts].dictBundle)
						{
							keyValuePair.Value.moveRate = moveRate;
						}
						this.chaCtrl.ChangeSettingHairCorrectPosAll(this.parts);
						this.cvsH_Hair.UpdateAllBundleUI(this.idx);
					}
					else
					{
						bi.moveRate = moveRate;
						this.chaCtrl.ChangeSettingHairCorrectPos(this.parts, this.idx);
					}
				};
				this.ssMove[1].onSetDefaultValue = delegate()
				{
					this.reset = true;
					return vDefPosRate.y;
				};
				this.ssMove[1].onEndSetDefaultValue = delegate()
				{
					this.reset = false;
				};
				this.ssMove[2].onChange = delegate(float value)
				{
					Vector3 moveRate = new Vector3(bi.moveRate.x, bi.moveRate.y, value);
					if (this.ctrlTogether && !this.reset && !this.cvsH_Hair.allReset)
					{
						foreach (KeyValuePair<int, ChaFileHair.PartsInfo.BundleInfo> keyValuePair in this.hair.parts[this.parts].dictBundle)
						{
							keyValuePair.Value.moveRate = moveRate;
						}
						this.chaCtrl.ChangeSettingHairCorrectPosAll(this.parts);
						this.cvsH_Hair.UpdateAllBundleUI(this.idx);
					}
					else
					{
						bi.moveRate = moveRate;
						this.chaCtrl.ChangeSettingHairCorrectPos(this.parts, this.idx);
					}
				};
				this.ssMove[2].onSetDefaultValue = delegate()
				{
					this.reset = true;
					return vDefPosRate.z;
				};
				this.ssMove[2].onEndSetDefaultValue = delegate()
				{
					this.reset = false;
				};
				this.ssRot[0].onChange = delegate(float value)
				{
					Vector3 rotRate = new Vector3(value, bi.rotRate.y, bi.rotRate.z);
					if (this.ctrlTogether && !this.reset && !this.cvsH_Hair.allReset)
					{
						foreach (KeyValuePair<int, ChaFileHair.PartsInfo.BundleInfo> keyValuePair in this.hair.parts[this.parts].dictBundle)
						{
							keyValuePair.Value.rotRate = rotRate;
						}
						this.chaCtrl.ChangeSettingHairCorrectRotAll(this.parts);
						this.cvsH_Hair.UpdateAllBundleUI(this.idx);
					}
					else
					{
						bi.rotRate = rotRate;
						this.chaCtrl.ChangeSettingHairCorrectRot(this.parts, this.idx);
					}
				};
				this.ssRot[0].onSetDefaultValue = delegate()
				{
					this.reset = true;
					return vDefRotRate.x;
				};
				this.ssRot[0].onEndSetDefaultValue = delegate()
				{
					this.reset = false;
				};
				this.ssRot[1].onChange = delegate(float value)
				{
					Vector3 rotRate = new Vector3(bi.rotRate.x, value, bi.rotRate.z);
					if (this.ctrlTogether && !this.reset && !this.cvsH_Hair.allReset)
					{
						foreach (KeyValuePair<int, ChaFileHair.PartsInfo.BundleInfo> keyValuePair in this.hair.parts[this.parts].dictBundle)
						{
							keyValuePair.Value.rotRate = rotRate;
						}
						this.chaCtrl.ChangeSettingHairCorrectRotAll(this.parts);
						this.cvsH_Hair.UpdateAllBundleUI(this.idx);
					}
					else
					{
						bi.rotRate = rotRate;
						this.chaCtrl.ChangeSettingHairCorrectRot(this.parts, this.idx);
					}
				};
				this.ssRot[1].onSetDefaultValue = delegate()
				{
					this.reset = true;
					return vDefRotRate.y;
				};
				this.ssRot[1].onEndSetDefaultValue = delegate()
				{
					this.reset = false;
				};
				this.ssRot[2].onChange = delegate(float value)
				{
					Vector3 rotRate = new Vector3(bi.rotRate.x, bi.rotRate.y, value);
					if (this.ctrlTogether && !this.reset && !this.cvsH_Hair.allReset)
					{
						foreach (KeyValuePair<int, ChaFileHair.PartsInfo.BundleInfo> keyValuePair in this.hair.parts[this.parts].dictBundle)
						{
							keyValuePair.Value.rotRate = rotRate;
						}
						this.chaCtrl.ChangeSettingHairCorrectRotAll(this.parts);
						this.cvsH_Hair.UpdateAllBundleUI(this.idx);
					}
					else
					{
						bi.rotRate = rotRate;
						this.chaCtrl.ChangeSettingHairCorrectRot(this.parts, this.idx);
					}
				};
				this.ssRot[2].onSetDefaultValue = delegate()
				{
					this.reset = true;
					return vDefRotRate.z;
				};
				this.ssRot[2].onEndSetDefaultValue = delegate()
				{
					this.reset = false;
				};
				if (this.tglNoShake)
				{
					this.tglNoShake.SetIsOnWithoutCallback(bi.noShake);
					this.tglNoShake.OnValueChangedAsObservable().Subscribe(delegate(bool isOn)
					{
						if (bi.noShake != isOn)
						{
							bi.noShake = isOn;
						}
					});
				}
				if (this.tglGuidDraw)
				{
					this.tglGuidDraw.SetIsOnWithoutCallback(true);
					this.tglGuidDraw.OnValueChangedAsObservable().Subscribe(delegate(bool isOn)
					{
						if (null != this.cmpGuid)
						{
							this.cmpGuid.gameObject.SetActiveIfDifferent(isOn);
						}
					});
				}
			}
			if (this.title)
			{
				this.title.text = "調整" + titleNo.ToString("00");
			}
			this.UpdateCustomUI();
		}

		// Token: 0x060046E6 RID: 18150 RVA: 0x001B3E30 File Offset: 0x001B2230
		private void LateUpdate()
		{
			if (null != this.cmpGuid && this.cmpGuid.gameObject.activeInHierarchy)
			{
				if (this.cmpGuid.isDrag)
				{
					this.SetHairTransform(false, this.cmpGuid.ctrlAxisType);
				}
				else if (this.isDrag)
				{
					this.SetHairTransform(true, this.cmpGuid.ctrlAxisType);
				}
				else
				{
					this.SetControllerTransform();
				}
				this.isDrag = this.cmpGuid.isDrag;
				this.customBase.cursorDraw = !this.cmpGuid.isDrag;
			}
		}

		// Token: 0x040041E7 RID: 16871
		[SerializeField]
		private CvsH_Hair cvsH_Hair;

		// Token: 0x040041E8 RID: 16872
		[SerializeField]
		private Text title;

		// Token: 0x040041E9 RID: 16873
		[SerializeField]
		private CustomSliderSet[] ssMove;

		// Token: 0x040041EA RID: 16874
		[SerializeField]
		private CustomSliderSet[] ssRot;

		// Token: 0x040041EB RID: 16875
		[SerializeField]
		private Toggle tglNoShake;

		// Token: 0x040041EC RID: 16876
		[SerializeField]
		private Toggle tglGuidDraw;

		// Token: 0x040041F0 RID: 16880
		[SerializeField]
		private GameObject tmpObjGuid;
	}
}
