using System;
using System.Linq;
using Illusion.Extensions;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CharaCustom
{
	// Token: 0x020009D0 RID: 2512
	public class CustomDrawMenu : CvsBase
	{
		// Token: 0x0600498A RID: 18826 RVA: 0x001BFBD4 File Offset: 0x001BDFD4
		public override void ChangeMenuFunc()
		{
			base.ChangeMenuFunc();
			base.customBase.customCtrl.showColorCvs = false;
			base.customBase.customCtrl.showFileList = false;
			base.customBase.customCtrl.showDrawMenu = true;
			this.UpdateUI();
		}

		// Token: 0x0600498B RID: 18827 RVA: 0x001BFC20 File Offset: 0x001BE020
		public void UpdateUI()
		{
			int num = base.customBase.clothesStateNo + 1;
			if (base.customBase.autoClothesState)
			{
				num = 0;
			}
			this.tglClothesState[num].SetIsOnWithoutCallback(true);
			for (int i = 0; i < this.tglClothesState.Length; i++)
			{
				if (i != num)
				{
					this.tglClothesState[i].SetIsOnWithoutCallback(false);
				}
			}
			this.tglAcsState[0].SetIsOnWithoutCallback(base.customBase.accessoryDraw);
			this.tglAcsState[1].SetIsOnWithoutCallback(!base.customBase.accessoryDraw);
			this.tglEyeLook[0].SetIsOnWithoutCallback(0 == base.customBase.eyelook);
			this.tglEyeLook[1].SetIsOnWithoutCallback(1 == base.customBase.eyelook);
			this.tglNeckLook[0].SetIsOnWithoutCallback(0 == base.customBase.necklook);
			this.tglNeckLook[1].SetIsOnWithoutCallback(1 == base.customBase.necklook);
			this.inpPoseNo.text = base.customBase.poseNo.ToString();
			this.inpEyebrowNo.text = (base.customBase.eyebrowPtn + 1).ToString();
			this.inpEyeNo.text = (base.customBase.eyePtn + 1).ToString();
			this.inpMouthNo.text = (base.customBase.mouthPtn + 1).ToString();
			this.tglPlayAnime.SetIsOnWithoutCallback(base.customBase.playPoseAnime);
			Vector3 localEulerAngles = base.customBase.lightCustom.transform.localEulerAngles;
			this.sldLightRotX.value = ((89f >= localEulerAngles.x) ? localEulerAngles.x : (localEulerAngles.x - 360f));
			this.sldLightRotY.value = ((180f > localEulerAngles.y) ? localEulerAngles.y : (localEulerAngles.y - 360f));
			this.csLight.SetColor(base.customBase.lightCustom.color);
			this.sldLightPower.value = base.customBase.lightCustom.intensity;
			this.tglBG[0].SetIsOnWithoutCallback(base.customBase.customCtrl.draw3D);
			this.tglBG[1].SetIsOnWithoutCallback(!base.customBase.customCtrl.draw3D);
			this.csBG.SetColor(base.customBase.customCtrl.GetBGColor());
			this.objBGIndex.SetActiveIfDifferent(!base.customBase.customCtrl.draw3D);
			this.objBGColor.SetActiveIfDifferent(base.customBase.customCtrl.draw3D);
		}

		// Token: 0x0600498C RID: 18828 RVA: 0x001BFF28 File Offset: 0x001BE328
		public void ChangeClothesStateForCapture(bool capture)
		{
			if (capture)
			{
				this.backAutoClothesState = base.customBase.autoClothesState;
				this.backClothesNo = base.customBase.clothesStateNo;
				this.backAutoClothesStateNo = base.customBase.autoClothesStateNo;
				base.customBase.clothesStateNo = 0;
				base.customBase.ChangeClothesState(-1);
				this.tglClothesState[1].SetIsOnWithoutCallback(true);
				for (int i = 0; i < this.tglClothesState.Length; i++)
				{
					if (i != 1)
					{
						this.tglClothesState[i].SetIsOnWithoutCallback(false);
					}
				}
				this.backAcsDraw = base.customBase.accessoryDraw;
				base.customBase.accessoryDraw = true;
			}
			else
			{
				base.customBase.autoClothesState = this.backAutoClothesState;
				base.customBase.clothesStateNo = this.backClothesNo;
				base.customBase.autoClothesStateNo = this.backAutoClothesStateNo;
				base.customBase.ChangeClothesState(-1);
				int num = base.customBase.clothesStateNo + 1;
				if (base.customBase.autoClothesState)
				{
					num = 0;
				}
				this.tglClothesState[num].SetIsOnWithoutCallback(true);
				for (int j = 0; j < this.tglClothesState.Length; j++)
				{
					if (j != num)
					{
						this.tglClothesState[j].SetIsOnWithoutCallback(false);
					}
				}
				base.customBase.accessoryDraw = this.backAcsDraw;
			}
		}

		// Token: 0x0600498D RID: 18829 RVA: 0x001C009C File Offset: 0x001BE49C
		protected override void Start()
		{
			CustomDrawMenu.<Start>c__AnonStorey8 <Start>c__AnonStorey = new CustomDrawMenu.<Start>c__AnonStorey8();
			<Start>c__AnonStorey.$this = this;
			base.customBase.lstInputField.Add(this.inpPoseNo);
			base.customBase.lstInputField.Add(this.inpEyebrowNo);
			base.customBase.lstInputField.Add(this.inpEyeNo);
			base.customBase.lstInputField.Add(this.inpMouthNo);
			if (this.tglClothesState.Any<Toggle>())
			{
				(from item in this.tglClothesState.Select((Toggle val, int idx) => new
				{
					val,
					idx
				})
				where item.val != null
				select item).ToList().ForEach(delegate(item)
				{
					(from isOn in item.val.onValueChanged.AsObservable<bool>()
					where isOn
					select isOn).Subscribe(delegate(bool _)
					{
						<Start>c__AnonStorey.customBase.ChangeClothesState(item.idx);
					});
				});
			}
			if (this.tglAcsState.Any<Toggle>())
			{
				(from item in this.tglAcsState.Select((Toggle val, int idx) => new
				{
					val,
					idx
				})
				where item.val != null
				select item).ToList().ForEach(delegate(item)
				{
					(from isOn in item.val.OnValueChangedAsObservable()
					where isOn
					select isOn).Subscribe(delegate(bool _)
					{
						<Start>c__AnonStorey.customBase.accessoryDraw = (item.idx == 0);
					});
				});
			}
			if (this.tglEyeLook.Any<Toggle>())
			{
				(from item in this.tglEyeLook.Select((Toggle val, int idx) => new
				{
					val,
					idx
				})
				where item.val != null
				select item).ToList().ForEach(delegate(item)
				{
					(from isOn in item.val.OnValueChangedAsObservable()
					where isOn
					select isOn).Subscribe(delegate(bool _)
					{
						<Start>c__AnonStorey.customBase.eyelook = item.idx;
					});
				});
			}
			if (this.tglNeckLook.Any<Toggle>())
			{
				(from item in this.tglNeckLook.Select((Toggle val, int idx) => new
				{
					val,
					idx
				})
				where item.val != null
				select item).ToList().ForEach(delegate(item)
				{
					(from isOn in item.val.OnValueChangedAsObservable()
					where isOn
					select isOn).Subscribe(delegate(bool _)
					{
						<Start>c__AnonStorey.customBase.necklook = item.idx;
					});
				});
			}
			if (this.btnPose.Any<Button>())
			{
				(from item in this.btnPose.Select((Button val, int idx) => new
				{
					val,
					idx
				})
				where item != null
				select item).ToList().ForEach(delegate(item)
				{
					item.val.OnClickAsObservable().Subscribe(delegate(Unit _)
					{
						if (item.idx == 2)
						{
							<Start>c__AnonStorey.customBase.poseNo = 1;
						}
						else
						{
							<Start>c__AnonStorey.customBase.ChangeAnimationNext(item.idx);
						}
						<Start>c__AnonStorey.inpPoseNo.text = <Start>c__AnonStorey.customBase.poseNo.ToString();
					});
				});
			}
			if (this.inpPoseNo)
			{
				this.inpPoseNo.onEndEdit.AsObservable<string>().Subscribe(delegate(string value)
				{
					int no;
					if (!int.TryParse(value, out no))
					{
						no = 0;
					}
					<Start>c__AnonStorey.$this.customBase.ChangeAnimationNo(no, false);
					<Start>c__AnonStorey.$this.inpPoseNo.text = <Start>c__AnonStorey.$this.customBase.poseNo.ToString();
				});
			}
			if (this.btnEyebrow.Any<Button>())
			{
				(from item in this.btnEyebrow.Select((Button val, int idx) => new
				{
					val,
					idx
				})
				where item != null
				select item).ToList().ForEach(delegate(item)
				{
					item.val.OnClickAsObservable().Subscribe(delegate(Unit _)
					{
						if (item.idx == 2)
						{
							<Start>c__AnonStorey.customBase.ChangeEyebrowPtnNext(-1);
						}
						else
						{
							<Start>c__AnonStorey.customBase.ChangeEyebrowPtnNext(item.idx);
						}
						<Start>c__AnonStorey.inpEyebrowNo.text = (<Start>c__AnonStorey.customBase.eyebrowPtn + 1).ToString();
					});
				});
			}
			if (this.inpEyebrowNo)
			{
				this.inpEyebrowNo.onEndEdit.AsObservable<string>().Subscribe(delegate(string value)
				{
					int no;
					if (!int.TryParse(value, out no))
					{
						no = 0;
					}
					<Start>c__AnonStorey.$this.customBase.ChangeEyebrowPtnNo(no);
					<Start>c__AnonStorey.$this.inpEyebrowNo.text = (<Start>c__AnonStorey.$this.customBase.eyebrowPtn + 1).ToString();
				});
			}
			if (this.btnEyePtn.Any<Button>())
			{
				(from item in this.btnEyePtn.Select((Button val, int idx) => new
				{
					val,
					idx
				})
				where item != null
				select item).ToList().ForEach(delegate(item)
				{
					item.val.OnClickAsObservable().Subscribe(delegate(Unit _)
					{
						if (item.idx == 2)
						{
							<Start>c__AnonStorey.customBase.ChangeEyePtnNext(-1);
						}
						else
						{
							<Start>c__AnonStorey.customBase.ChangeEyePtnNext(item.idx);
						}
						<Start>c__AnonStorey.inpEyeNo.text = (<Start>c__AnonStorey.customBase.eyePtn + 1).ToString();
					});
				});
			}
			if (this.inpEyeNo)
			{
				this.inpEyeNo.onEndEdit.AsObservable<string>().Subscribe(delegate(string value)
				{
					int no;
					if (!int.TryParse(value, out no))
					{
						no = 0;
					}
					<Start>c__AnonStorey.$this.customBase.ChangeEyePtnNo(no);
					<Start>c__AnonStorey.$this.inpEyeNo.text = (<Start>c__AnonStorey.$this.customBase.eyePtn + 1).ToString();
				});
			}
			if (this.btnMouthPtn.Any<Button>())
			{
				(from item in this.btnMouthPtn.Select((Button val, int idx) => new
				{
					val,
					idx
				})
				where item != null
				select item).ToList().ForEach(delegate(item)
				{
					item.val.OnClickAsObservable().Subscribe(delegate(Unit _)
					{
						if (item.idx == 2)
						{
							<Start>c__AnonStorey.customBase.ChangeMouthPtnNext(-1);
						}
						else
						{
							<Start>c__AnonStorey.customBase.ChangeMouthPtnNext(item.idx);
						}
						<Start>c__AnonStorey.inpMouthNo.text = (<Start>c__AnonStorey.customBase.mouthPtn + 1).ToString();
					});
				});
			}
			if (this.inpMouthNo)
			{
				this.inpMouthNo.onEndEdit.AsObservable<string>().Subscribe(delegate(string value)
				{
					int no;
					if (!int.TryParse(value, out no))
					{
						no = 0;
					}
					<Start>c__AnonStorey.$this.customBase.ChangeMouthPtnNo(no);
					<Start>c__AnonStorey.$this.inpMouthNo.text = (<Start>c__AnonStorey.$this.customBase.mouthPtn + 1).ToString();
				});
			}
			if (this.tglPlayAnime)
			{
				this.tglPlayAnime.OnValueChangedAsObservable().Subscribe(delegate(bool isOn)
				{
					<Start>c__AnonStorey.$this.customBase.playPoseAnime = isOn;
				});
			}
			<Start>c__AnonStorey.veclight = base.customBase.lightCustom.transform.localEulerAngles;
			if (this.sldLightRotX)
			{
				this.sldLightRotX.value = ((89f >= <Start>c__AnonStorey.veclight.x) ? <Start>c__AnonStorey.veclight.x : (<Start>c__AnonStorey.veclight.x - 360f));
				this.sldLightRotX.OnValueChangedAsObservable().Subscribe(delegate(float val)
				{
					<Start>c__AnonStorey.$this.customBase.lightCustom.transform.localEulerAngles = new Vector3(val, <Start>c__AnonStorey.$this.customBase.lightCustom.transform.localEulerAngles.y, <Start>c__AnonStorey.$this.customBase.lightCustom.transform.localEulerAngles.z);
				});
			}
			this.sldLightRotX.OnScrollAsObservable().Subscribe(delegate(PointerEventData scl)
			{
				if (<Start>c__AnonStorey.$this.customBase.sliderControlWheel)
				{
					<Start>c__AnonStorey.$this.sldLightRotX.value = Mathf.Clamp(<Start>c__AnonStorey.$this.sldLightRotX.value + scl.scrollDelta.y, -88f, 88f);
				}
			});
			if (this.sldLightRotY)
			{
				this.sldLightRotY.value = ((180f > <Start>c__AnonStorey.veclight.y) ? <Start>c__AnonStorey.veclight.y : (<Start>c__AnonStorey.veclight.y - 360f));
				this.sldLightRotY.OnValueChangedAsObservable().Subscribe(delegate(float val)
				{
					<Start>c__AnonStorey.$this.customBase.lightCustom.transform.localEulerAngles = new Vector3(<Start>c__AnonStorey.$this.customBase.lightCustom.transform.localEulerAngles.x, val, <Start>c__AnonStorey.$this.customBase.lightCustom.transform.localEulerAngles.z);
				});
			}
			this.sldLightRotY.OnScrollAsObservable().Subscribe(delegate(PointerEventData scl)
			{
				if (<Start>c__AnonStorey.$this.customBase.sliderControlWheel)
				{
					<Start>c__AnonStorey.$this.sldLightRotY.value = Mathf.Clamp(<Start>c__AnonStorey.$this.sldLightRotY.value + scl.scrollDelta.y, -178f, 178f);
				}
			});
			if (this.csLight)
			{
				this.csLight.actUpdateColor = delegate(Color color)
				{
					<Start>c__AnonStorey.$this.customBase.lightCustom.color = color;
				};
			}
			if (this.sldLightPower)
			{
				this.sldLightPower.OnValueChangedAsObservable().Subscribe(delegate(float val)
				{
					<Start>c__AnonStorey.$this.customBase.lightCustom.intensity = val;
				});
			}
			this.sldLightPower.OnScrollAsObservable().Subscribe(delegate(PointerEventData scl)
			{
				if (<Start>c__AnonStorey.$this.customBase.sliderControlWheel)
				{
					<Start>c__AnonStorey.$this.sldLightPower.value = Mathf.Clamp(<Start>c__AnonStorey.$this.sldLightPower.value + scl.scrollDelta.y * -0.01f, 0f, 100f);
				}
			});
			if (this.btnLightReset)
			{
				this.btnLightReset.OnClickAsObservable().Subscribe(delegate(Unit _)
				{
					<Start>c__AnonStorey.$this.customBase.ResetLightSetting();
					<Start>c__AnonStorey.veclight = <Start>c__AnonStorey.$this.customBase.lightCustom.transform.localEulerAngles;
					<Start>c__AnonStorey.$this.sldLightRotX.value = ((89f >= <Start>c__AnonStorey.veclight.x) ? <Start>c__AnonStorey.veclight.x : (<Start>c__AnonStorey.veclight.x - 360f));
					<Start>c__AnonStorey.$this.sldLightRotY.value = ((180f > <Start>c__AnonStorey.veclight.y) ? <Start>c__AnonStorey.veclight.y : (<Start>c__AnonStorey.veclight.y - 360f));
					<Start>c__AnonStorey.$this.csLight.SetColor(<Start>c__AnonStorey.$this.customBase.lightCustom.color);
					<Start>c__AnonStorey.$this.sldLightPower.value = <Start>c__AnonStorey.$this.customBase.lightCustom.intensity;
				});
			}
			if (this.tglBG.Any<Toggle>())
			{
				(from item in this.tglBG.Select((Toggle val, int idx) => new
				{
					val,
					idx
				})
				where item.val != null
				select item).ToList().ForEach(delegate(item)
				{
					(from isOn in item.val.OnValueChangedAsObservable()
					where isOn
					select isOn).Subscribe(delegate(bool _)
					{
						<Start>c__AnonStorey.customBase.customCtrl.draw3D = (0 == item.idx);
						<Start>c__AnonStorey.objBGIndex.SetActiveIfDifferent(!<Start>c__AnonStorey.customBase.customCtrl.draw3D);
						<Start>c__AnonStorey.objBGColor.SetActiveIfDifferent(<Start>c__AnonStorey.customBase.customCtrl.draw3D);
						<Start>c__AnonStorey.customBase.forceBackFrameHide = <Start>c__AnonStorey.customBase.customCtrl.draw3D;
					});
				});
			}
			if (this.btnBGIndex.Any<Button>())
			{
				(from item in this.btnBGIndex.Select((Button val, int idx) => new
				{
					val,
					idx
				})
				where item != null
				select item).ToList().ForEach(delegate(item)
				{
					item.val.OnClickAsObservable().Subscribe(delegate(Unit _)
					{
						<Start>c__AnonStorey.customBase.customCtrl.ChangeBGImage(item.idx);
					});
				});
			}
			if (this.csBG)
			{
				this.csBG.actUpdateColor = delegate(Color color)
				{
					<Start>c__AnonStorey.$this.customBase.customCtrl.ChangeBGColor(color);
				};
			}
			if (this.btnClose)
			{
				this.btnClose.OnClickAsObservable().Subscribe(delegate(Unit _)
				{
					<Start>c__AnonStorey.$this.customBase.customCtrl.showDrawMenu = false;
				});
			}
			this.UpdateUI();
		}

		// Token: 0x04004414 RID: 17428
		[SerializeField]
		private Button btnClose;

		// Token: 0x04004415 RID: 17429
		[Header("【描画】------------------------")]
		[SerializeField]
		private Toggle[] tglClothesState;

		// Token: 0x04004416 RID: 17430
		[SerializeField]
		private Toggle[] tglAcsState;

		// Token: 0x04004417 RID: 17431
		[Header("【キャラ】----------------------")]
		[SerializeField]
		private Toggle[] tglEyeLook;

		// Token: 0x04004418 RID: 17432
		[SerializeField]
		private Toggle[] tglNeckLook;

		// Token: 0x04004419 RID: 17433
		[SerializeField]
		private Button[] btnPose;

		// Token: 0x0400441A RID: 17434
		[SerializeField]
		private InputField inpPoseNo;

		// Token: 0x0400441B RID: 17435
		[SerializeField]
		private Button[] btnEyebrow;

		// Token: 0x0400441C RID: 17436
		[SerializeField]
		private InputField inpEyebrowNo;

		// Token: 0x0400441D RID: 17437
		[SerializeField]
		private Button[] btnEyePtn;

		// Token: 0x0400441E RID: 17438
		[SerializeField]
		private InputField inpEyeNo;

		// Token: 0x0400441F RID: 17439
		[SerializeField]
		private Button[] btnMouthPtn;

		// Token: 0x04004420 RID: 17440
		[SerializeField]
		private InputField inpMouthNo;

		// Token: 0x04004421 RID: 17441
		[SerializeField]
		private UI_ToggleOnOffEx tglPlayAnime;

		// Token: 0x04004422 RID: 17442
		[Header("【ライト】----------------------")]
		[SerializeField]
		private Slider sldLightRotX;

		// Token: 0x04004423 RID: 17443
		[SerializeField]
		private Slider sldLightRotY;

		// Token: 0x04004424 RID: 17444
		[SerializeField]
		private CustomColorSet csLight;

		// Token: 0x04004425 RID: 17445
		[SerializeField]
		private Slider sldLightPower;

		// Token: 0x04004426 RID: 17446
		[SerializeField]
		private Button btnLightReset;

		// Token: 0x04004427 RID: 17447
		[Header("【背景】------------------------")]
		[SerializeField]
		private Toggle[] tglBG;

		// Token: 0x04004428 RID: 17448
		[SerializeField]
		private GameObject objBGIndex;

		// Token: 0x04004429 RID: 17449
		[SerializeField]
		private Button[] btnBGIndex;

		// Token: 0x0400442A RID: 17450
		[SerializeField]
		private GameObject objBGColor;

		// Token: 0x0400442B RID: 17451
		[SerializeField]
		private CustomColorSet csBG;

		// Token: 0x0400442C RID: 17452
		private bool backAutoClothesState;

		// Token: 0x0400442D RID: 17453
		private int backClothesNo;

		// Token: 0x0400442E RID: 17454
		private int backAutoClothesStateNo;

		// Token: 0x0400442F RID: 17455
		private bool backAcsDraw = true;
	}
}
