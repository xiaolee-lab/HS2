using System;
using System.Linq;
using AIChara;
using AIProject;
using Illusion.Extensions;
using Manager;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CharaCustom
{
	// Token: 0x02000A07 RID: 2567
	public class CvsCaptureMenu : MonoBehaviour
	{
		// Token: 0x17000E58 RID: 3672
		// (get) Token: 0x06004C19 RID: 19481 RVA: 0x001D44D7 File Offset: 0x001D28D7
		protected CustomBase customBase
		{
			get
			{
				return Singleton<CustomBase>.Instance;
			}
		}

		// Token: 0x17000E59 RID: 3673
		// (get) Token: 0x06004C1A RID: 19482 RVA: 0x001D44DE File Offset: 0x001D28DE
		protected ChaListControl lstCtrl
		{
			get
			{
				return Singleton<Character>.Instance.chaListCtrl;
			}
		}

		// Token: 0x17000E5A RID: 3674
		// (get) Token: 0x06004C1B RID: 19483 RVA: 0x001D44EA File Offset: 0x001D28EA
		protected ChaControl chaCtrl
		{
			get
			{
				return this.customBase.chaCtrl;
			}
		}

		// Token: 0x06004C1C RID: 19484 RVA: 0x001D44F8 File Offset: 0x001D28F8
		public void BeginCapture()
		{
			this.backAutoClothesState = this.customBase.autoClothesState;
			this.backClothesNo = this.customBase.clothesStateNo;
			this.backAutoClothesStateNo = this.customBase.autoClothesStateNo;
			int clothesStateNo = this.customBase.clothesStateNo;
			this.tglClothesState[clothesStateNo].SetIsOnWithoutCallback(true);
			for (int i = 0; i < this.tglClothesState.Length; i++)
			{
				if (i != clothesStateNo)
				{
					this.tglClothesState[i].SetIsOnWithoutCallback(false);
				}
			}
			this.tglAcsState[0].SetIsOnWithoutCallback(this.customBase.accessoryDraw);
			this.tglAcsState[1].SetIsOnWithoutCallback(!this.customBase.accessoryDraw);
			this.tglEyeLook[0].SetIsOnWithoutCallback(0 == this.customBase.eyelook);
			this.tglEyeLook[1].SetIsOnWithoutCallback(1 == this.customBase.eyelook);
			this.tglNeckLook[0].SetIsOnWithoutCallback(0 == this.customBase.necklook);
			this.tglNeckLook[1].SetIsOnWithoutCallback(1 == this.customBase.necklook);
			this.chaCtrl.ChangeEyesBlinkFlag(false);
			this.chaCtrl.ChangeMouthFixed(true);
			this.chaCtrl.ChangeMouthOpenMax(0f);
			this.sldEyeOpen.value = 1f;
			this.sldMouthOpen.value = 0f;
			this.chaCtrl.SetEnableShapeHand(0, false);
			this.chaCtrl.SetShapeHandIndex(0, 0, -1);
			this.chaCtrl.SetEnableShapeHand(1, false);
			this.chaCtrl.SetShapeHandIndex(1, 0, -1);
			this.inpPoseNo.text = this.customBase.poseNo.ToString();
			this.inpEyebrowNo.text = (this.customBase.eyebrowPtn + 1).ToString();
			this.inpEyeNo.text = (this.customBase.eyePtn + 1).ToString();
			this.inpMouthNo.text = (this.customBase.mouthPtn + 1).ToString();
			this.inpHandLNo.text = "ポーズ";
			this.inpHandRNo.text = "ポーズ";
			this.tglPlayAnime.SetIsOnWithoutCallback(this.customBase.playPoseAnime);
			Vector3 localEulerAngles = this.customBase.lightCustom.transform.localEulerAngles;
			this.sldLightRotX.value = ((89f >= localEulerAngles.x) ? localEulerAngles.x : (localEulerAngles.x - 360f));
			this.sldLightRotY.value = ((180f > localEulerAngles.y) ? localEulerAngles.y : (localEulerAngles.y - 360f));
			this.csLight.SetColor(this.customBase.lightCustom.color);
			this.sldLightPower.value = this.customBase.lightCustom.intensity;
			this.customBase.drawSaveFrameTop = true;
			this.tglBFrameDraw.isOn = this.customBase.drawSaveFrameBack;
			this.tglFFrameDraw.isOn = this.customBase.drawSaveFrameFront;
			this.tglBG[0].SetIsOnWithoutCallback(this.customBase.customCtrl.draw3D);
			this.tglBG[1].SetIsOnWithoutCallback(!this.customBase.customCtrl.draw3D);
			this.csBG.SetColor(this.customBase.customCtrl.GetBGColor());
			this.objBGIndex.SetActiveIfDifferent(!this.customBase.customCtrl.draw3D);
			this.objBGColor.SetActiveIfDifferent(this.customBase.customCtrl.draw3D);
			this.objBackFrame.SetActiveIfDifferent(!this.customBase.customCtrl.draw3D);
			if (this.customBase.customCtrl.saveMode)
			{
				this.textSaveName.text = "保存";
			}
			else
			{
				this.textSaveName.text = "撮影";
			}
		}

		// Token: 0x06004C1D RID: 19485 RVA: 0x001D4950 File Offset: 0x001D2D50
		public void EndCapture()
		{
			this.customBase.autoClothesState = this.backAutoClothesState;
			this.customBase.clothesStateNo = this.backClothesNo;
			this.customBase.autoClothesStateNo = this.backAutoClothesStateNo;
			this.customBase.ChangeClothesState(-1);
			this.chaCtrl.ChangeEyesBlinkFlag(true);
			this.chaCtrl.ChangeMouthFixed(false);
			this.chaCtrl.ChangeEyesOpenMax(1f);
			this.chaCtrl.ChangeMouthOpenMax(1f);
			this.chaCtrl.SetEnableShapeHand(0, false);
			this.chaCtrl.SetEnableShapeHand(1, false);
			this.customBase.drawSaveFrameTop = false;
			this.customBase.drawMenu.UpdateUI();
		}

		// Token: 0x06004C1E RID: 19486 RVA: 0x001D4A0C File Offset: 0x001D2E0C
		protected virtual void Start()
		{
			this.customBase.lstInputField.Add(this.inpPoseNo);
			this.customBase.lstInputField.Add(this.inpEyebrowNo);
			this.customBase.lstInputField.Add(this.inpEyeNo);
			this.customBase.lstInputField.Add(this.inpMouthNo);
			this.customBase.lstInputField.Add(this.inpHandLNo);
			this.customBase.lstInputField.Add(this.inpHandRNo);
			this.customBase.drawSaveFrameBack = true;
			this.customBase.drawSaveFrameFront = true;
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
					(from isOn in item.val.OnValueChangedAsObservable()
					where isOn
					select isOn).Subscribe(delegate(bool _)
					{
						this.customBase.ChangeClothesState(item.idx + 1);
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
						this.customBase.accessoryDraw = (item.idx == 0);
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
						this.customBase.eyelook = item.idx;
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
						this.customBase.necklook = item.idx;
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
							this.customBase.poseNo = 1;
						}
						else
						{
							this.customBase.ChangeAnimationNext(item.idx);
						}
						this.inpPoseNo.text = this.customBase.poseNo.ToString();
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
					this.customBase.ChangeAnimationNo(no, false);
					this.inpPoseNo.text = this.customBase.poseNo.ToString();
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
							this.customBase.ChangeEyebrowPtnNext(-1);
						}
						else
						{
							this.customBase.ChangeEyebrowPtnNext(item.idx);
						}
						this.inpEyebrowNo.text = (this.customBase.eyebrowPtn + 1).ToString();
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
					this.customBase.ChangeEyebrowPtnNo(no);
					this.inpEyebrowNo.text = (this.customBase.eyebrowPtn + 1).ToString();
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
							this.customBase.ChangeEyePtnNext(-1);
						}
						else
						{
							this.customBase.ChangeEyePtnNext(item.idx);
						}
						this.inpEyeNo.text = (this.customBase.eyePtn + 1).ToString();
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
					this.customBase.ChangeEyePtnNo(no);
					this.inpEyeNo.text = (this.customBase.eyePtn + 1).ToString();
				});
			}
			if (this.sldEyeOpen)
			{
				this.sldEyeOpen.OnValueChangedAsObservable().Subscribe(delegate(float val)
				{
					this.chaCtrl.ChangeEyesOpenMax(val);
				});
			}
			this.sldEyeOpen.OnScrollAsObservable().Subscribe(delegate(PointerEventData scl)
			{
				if (this.customBase.sliderControlWheel)
				{
					this.sldEyeOpen.value = Mathf.Clamp(this.sldEyeOpen.value + scl.scrollDelta.y * -0.01f, 0f, 100f);
				}
			});
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
							this.customBase.ChangeMouthPtnNext(-1);
						}
						else
						{
							this.customBase.ChangeMouthPtnNext(item.idx);
						}
						this.inpMouthNo.text = (this.customBase.mouthPtn + 1).ToString();
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
					this.customBase.ChangeMouthPtnNo(no);
					this.inpMouthNo.text = (this.customBase.mouthPtn + 1).ToString();
				});
			}
			if (this.sldMouthOpen)
			{
				this.sldMouthOpen.OnValueChangedAsObservable().Subscribe(delegate(float val)
				{
					this.chaCtrl.ChangeMouthOpenMax(val);
				});
			}
			this.sldMouthOpen.OnScrollAsObservable().Subscribe(delegate(PointerEventData scl)
			{
				if (this.customBase.sliderControlWheel)
				{
					this.sldMouthOpen.value = Mathf.Clamp(this.sldMouthOpen.value + scl.scrollDelta.y * -0.01f, 0f, 100f);
				}
			});
			if (this.btnHandLPtn.Any<Button>())
			{
				(from item in this.btnHandLPtn.Select((Button val, int idx) => new
				{
					val,
					idx
				})
				where item != null
				select item).ToList().ForEach(delegate(item)
				{
					item.val.OnClickAsObservable().Subscribe(delegate(Unit _)
					{
						int num = this.chaCtrl.GetShapeIndexHandCount() + 1;
						if (item.idx == 2)
						{
							this.handLPtn = 0;
						}
						else if (item.idx == 0)
						{
							this.handLPtn = (this.handLPtn + num - 1) % num;
						}
						else
						{
							this.handLPtn = (this.handLPtn + 1) % num;
						}
						if (this.handLPtn == 0)
						{
							this.chaCtrl.SetEnableShapeHand(0, false);
						}
						else
						{
							this.chaCtrl.SetEnableShapeHand(0, true);
							this.chaCtrl.SetShapeHandIndex(0, this.handLPtn - 1, -1);
						}
						if (this.handLPtn == 0)
						{
							this.inpHandLNo.text = "ポーズ";
						}
						else
						{
							this.inpHandLNo.text = this.handLPtn.ToString();
						}
					});
				});
			}
			if (this.inpHandLNo)
			{
				this.inpHandLNo.onEndEdit.AsObservable<string>().Subscribe(delegate(string value)
				{
					int num;
					if (value == "ポーズ")
					{
						num = 0;
					}
					else if (!int.TryParse(value, out num))
					{
						num = 0;
					}
					this.handLPtn = num;
					if (this.handLPtn == 0)
					{
						this.chaCtrl.SetEnableShapeHand(0, false);
					}
					else
					{
						this.chaCtrl.SetEnableShapeHand(0, true);
						this.chaCtrl.SetShapeHandIndex(0, this.handLPtn - 1, -1);
					}
					if (this.handLPtn == 0)
					{
						this.inpHandLNo.text = "ポーズ";
					}
					else
					{
						this.inpHandLNo.text = this.handLPtn.ToString();
					}
				});
			}
			if (this.btnHandRPtn.Any<Button>())
			{
				(from item in this.btnHandRPtn.Select((Button val, int idx) => new
				{
					val,
					idx
				})
				where item != null
				select item).ToList().ForEach(delegate(item)
				{
					item.val.OnClickAsObservable().Subscribe(delegate(Unit _)
					{
						int num = this.chaCtrl.GetShapeIndexHandCount() + 1;
						if (item.idx == 2)
						{
							this.handRPtn = 0;
						}
						else if (item.idx == 0)
						{
							this.handRPtn = (this.handRPtn + num - 1) % num;
						}
						else
						{
							this.handRPtn = (this.handRPtn + 1) % num;
						}
						if (this.handRPtn == 0)
						{
							this.chaCtrl.SetEnableShapeHand(1, false);
						}
						else
						{
							this.chaCtrl.SetEnableShapeHand(1, true);
							this.chaCtrl.SetShapeHandIndex(1, this.handRPtn - 1, -1);
						}
						if (this.handRPtn == 0)
						{
							this.inpHandRNo.text = "ポーズ";
						}
						else
						{
							this.inpHandRNo.text = this.handRPtn.ToString();
						}
					});
				});
			}
			if (this.inpHandRNo)
			{
				this.inpHandRNo.onEndEdit.AsObservable<string>().Subscribe(delegate(string value)
				{
					int num;
					if (value == "ポーズ")
					{
						num = 0;
					}
					else if (!int.TryParse(value, out num))
					{
						num = 0;
					}
					this.handRPtn = num;
					if (this.handRPtn == 0)
					{
						this.chaCtrl.SetEnableShapeHand(1, false);
					}
					else
					{
						this.chaCtrl.SetEnableShapeHand(1, true);
						this.chaCtrl.SetShapeHandIndex(1, this.handRPtn - 1, -1);
					}
					if (this.handRPtn == 0)
					{
						this.inpHandRNo.text = "ポーズ";
					}
					else
					{
						this.inpHandRNo.text = this.handRPtn.ToString();
					}
				});
			}
			if (this.tglPlayAnime)
			{
				this.tglPlayAnime.OnValueChangedAsObservable().Subscribe(delegate(bool isOn)
				{
					this.customBase.playPoseAnime = isOn;
				});
			}
			if (this.sldLightRotX)
			{
				this.sldLightRotX.OnValueChangedAsObservable().Subscribe(delegate(float val)
				{
					this.customBase.lightCustom.transform.localEulerAngles = new Vector3(val, this.customBase.lightCustom.transform.localEulerAngles.y, this.customBase.lightCustom.transform.localEulerAngles.z);
				});
			}
			this.sldLightRotX.OnScrollAsObservable().Subscribe(delegate(PointerEventData scl)
			{
				if (this.customBase.sliderControlWheel)
				{
					this.sldLightRotX.value = Mathf.Clamp(this.sldLightRotX.value + scl.scrollDelta.y * -0.01f, 0f, 100f);
				}
			});
			if (this.sldLightRotY)
			{
				this.sldLightRotY.OnValueChangedAsObservable().Subscribe(delegate(float val)
				{
					this.customBase.lightCustom.transform.localEulerAngles = new Vector3(this.customBase.lightCustom.transform.localEulerAngles.x, val, this.customBase.lightCustom.transform.localEulerAngles.z);
				});
			}
			this.sldLightRotY.OnScrollAsObservable().Subscribe(delegate(PointerEventData scl)
			{
				if (this.customBase.sliderControlWheel)
				{
					this.sldLightRotY.value = Mathf.Clamp(this.sldLightRotY.value + scl.scrollDelta.y, -88f, 88f);
				}
			});
			if (this.csLight)
			{
				this.csLight.actUpdateColor = delegate(Color color)
				{
					this.customBase.lightCustom.color = color;
				};
			}
			if (this.sldLightPower)
			{
				this.sldLightPower.OnValueChangedAsObservable().Subscribe(delegate(float val)
				{
					this.customBase.lightCustom.intensity = val;
				});
			}
			this.sldLightPower.OnScrollAsObservable().Subscribe(delegate(PointerEventData scl)
			{
				if (this.customBase.sliderControlWheel)
				{
					this.sldLightPower.value = Mathf.Clamp(this.sldLightPower.value + scl.scrollDelta.y, -178f, 178f);
				}
			});
			if (this.btnLightReset)
			{
				this.btnLightReset.OnClickAsObservable().Subscribe(delegate(Unit _)
				{
					this.customBase.ResetLightSetting();
					Vector3 localEulerAngles = this.customBase.lightCustom.transform.localEulerAngles;
					this.sldLightRotX.value = ((89f >= localEulerAngles.x) ? localEulerAngles.x : (localEulerAngles.x - 360f));
					this.sldLightRotY.value = ((180f > localEulerAngles.y) ? localEulerAngles.y : (localEulerAngles.y - 360f));
					this.csLight.SetColor(this.customBase.lightCustom.color);
					this.sldLightPower.value = this.customBase.lightCustom.intensity;
				});
			}
			if (this.tglBFrameDraw)
			{
				this.tglBFrameDraw.OnValueChangedAsObservable().Subscribe(delegate(bool isOn)
				{
					this.customBase.drawSaveFrameBack = isOn;
				});
			}
			if (this.btnBFramePtn.Any<Button>())
			{
				(from item in this.btnBFramePtn.Select((Button val, int idx) => new
				{
					val,
					idx
				})
				where item != null
				select item).ToList().ForEach(delegate(item)
				{
					item.val.OnClickAsObservable().Subscribe(delegate(Unit _)
					{
						this.customBase.saveFrameAssist.ChangeSaveFrameBack((byte)item.idx, true);
					});
				});
			}
			if (this.tglFFrameDraw)
			{
				this.tglFFrameDraw.OnValueChangedAsObservable().Subscribe(delegate(bool isOn)
				{
					this.customBase.drawSaveFrameFront = isOn;
				});
			}
			if (this.btnFFramePtn.Any<Button>())
			{
				(from item in this.btnFFramePtn.Select((Button val, int idx) => new
				{
					val,
					idx
				})
				where item != null
				select item).ToList().ForEach(delegate(item)
				{
					item.val.OnClickAsObservable().Subscribe(delegate(Unit _)
					{
						this.customBase.saveFrameAssist.ChangeSaveFrameFront((byte)item.idx, true);
					});
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
						this.customBase.customCtrl.draw3D = (0 == item.idx);
						this.objBGIndex.SetActiveIfDifferent(!this.customBase.customCtrl.draw3D);
						this.objBGColor.SetActiveIfDifferent(this.customBase.customCtrl.draw3D);
						this.objBackFrame.SetActiveIfDifferent(!this.customBase.customCtrl.draw3D);
						this.customBase.forceBackFrameHide = this.customBase.customCtrl.draw3D;
						if (!this.customBase.customCtrl.draw3D)
						{
							this.customBase.customCtrl.showColorCvs = false;
						}
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
						this.customBase.customCtrl.ChangeBGImage(item.idx);
					});
				});
			}
			if (this.csBG)
			{
				this.csBG.actUpdateColor = delegate(Color color)
				{
					this.customBase.customCtrl.ChangeBGColor(color);
				};
			}
			if (this.btnCancel)
			{
				this.btnCancel.OnClickAsObservable().Subscribe(delegate(Unit _)
				{
					Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Cancel);
					this.customBase.customCtrl.overwriteSavePath = string.Empty;
					this.EndCapture();
					this.customBase.customCtrl.saveMode = false;
					this.customBase.customCtrl.updatePng = false;
				});
			}
			if (this.btnSave)
			{
				this.btnSave.OnClickAsObservable().Subscribe(delegate(Unit _)
				{
					if (this.customBase.customCtrl.saveMode)
					{
						Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Save);
						byte[] pngData = this.customBase.customCtrl.customCap.CapCharaCard(true, this.customBase.saveFrameAssist, this.customBase.customCtrl.draw3D);
						this.chaCtrl.chaFile.pngData = pngData;
						string filename = string.Empty;
						if (this.customBase.customCtrl.overwriteSavePath.IsNullOrEmpty())
						{
							if (this.chaCtrl.sex == 0)
							{
								filename = "AISChaM_" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
							}
							else
							{
								filename = "AISChaF_" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
							}
						}
						else
						{
							filename = this.customBase.customCtrl.overwriteSavePath;
							this.customBase.customCtrl.overwriteSavePath = string.Empty;
						}
						this.chaCtrl.chaFile.SaveCharaFile(filename, byte.MaxValue, false);
						this.customBase.updateCvsCharaSaveDelete = true;
						this.customBase.updateCvsCharaLoad = true;
					}
					else
					{
						Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Photo);
						byte[] pngData2 = this.customBase.customCtrl.customCap.CapCharaCard(true, this.customBase.saveFrameAssist, this.customBase.customCtrl.draw3D);
						this.chaCtrl.chaFile.pngData = pngData2;
					}
					this.EndCapture();
					this.customBase.customCtrl.saveMode = false;
					this.customBase.customCtrl.updatePng = false;
				});
			}
		}

		// Token: 0x040045D6 RID: 17878
		[Header("【描画】------------------------")]
		[SerializeField]
		private Toggle[] tglClothesState;

		// Token: 0x040045D7 RID: 17879
		[SerializeField]
		private Toggle[] tglAcsState;

		// Token: 0x040045D8 RID: 17880
		[Header("【キャラ】----------------------")]
		[SerializeField]
		private Toggle[] tglEyeLook;

		// Token: 0x040045D9 RID: 17881
		[SerializeField]
		private Toggle[] tglNeckLook;

		// Token: 0x040045DA RID: 17882
		[SerializeField]
		private Button[] btnPose;

		// Token: 0x040045DB RID: 17883
		[SerializeField]
		private InputField inpPoseNo;

		// Token: 0x040045DC RID: 17884
		[SerializeField]
		private Button[] btnEyebrow;

		// Token: 0x040045DD RID: 17885
		[SerializeField]
		private InputField inpEyebrowNo;

		// Token: 0x040045DE RID: 17886
		[SerializeField]
		private Button[] btnEyePtn;

		// Token: 0x040045DF RID: 17887
		[SerializeField]
		private InputField inpEyeNo;

		// Token: 0x040045E0 RID: 17888
		[SerializeField]
		private Slider sldEyeOpen;

		// Token: 0x040045E1 RID: 17889
		[SerializeField]
		private Button[] btnMouthPtn;

		// Token: 0x040045E2 RID: 17890
		[SerializeField]
		private InputField inpMouthNo;

		// Token: 0x040045E3 RID: 17891
		[SerializeField]
		private Slider sldMouthOpen;

		// Token: 0x040045E4 RID: 17892
		[SerializeField]
		private Button[] btnHandLPtn;

		// Token: 0x040045E5 RID: 17893
		[SerializeField]
		private InputField inpHandLNo;

		// Token: 0x040045E6 RID: 17894
		[SerializeField]
		private Button[] btnHandRPtn;

		// Token: 0x040045E7 RID: 17895
		[SerializeField]
		private InputField inpHandRNo;

		// Token: 0x040045E8 RID: 17896
		[SerializeField]
		private UI_ToggleOnOffEx tglPlayAnime;

		// Token: 0x040045E9 RID: 17897
		[Header("【ライト】----------------------")]
		[SerializeField]
		private Slider sldLightRotX;

		// Token: 0x040045EA RID: 17898
		[SerializeField]
		private Slider sldLightRotY;

		// Token: 0x040045EB RID: 17899
		[SerializeField]
		private CustomColorSet csLight;

		// Token: 0x040045EC RID: 17900
		[SerializeField]
		private Slider sldLightPower;

		// Token: 0x040045ED RID: 17901
		[SerializeField]
		private Button btnLightReset;

		// Token: 0x040045EE RID: 17902
		[Header("【フレーム】--------------------")]
		[SerializeField]
		private GameObject objBackFrame;

		// Token: 0x040045EF RID: 17903
		[SerializeField]
		private Toggle tglBFrameDraw;

		// Token: 0x040045F0 RID: 17904
		[SerializeField]
		private Button[] btnBFramePtn;

		// Token: 0x040045F1 RID: 17905
		[SerializeField]
		private Toggle tglFFrameDraw;

		// Token: 0x040045F2 RID: 17906
		[SerializeField]
		private Button[] btnFFramePtn;

		// Token: 0x040045F3 RID: 17907
		[Header("【背景】------------------------")]
		[SerializeField]
		private Toggle[] tglBG;

		// Token: 0x040045F4 RID: 17908
		[SerializeField]
		private GameObject objBGIndex;

		// Token: 0x040045F5 RID: 17909
		[SerializeField]
		private Button[] btnBGIndex;

		// Token: 0x040045F6 RID: 17910
		[SerializeField]
		private GameObject objBGColor;

		// Token: 0x040045F7 RID: 17911
		[SerializeField]
		private CustomColorSet csBG;

		// Token: 0x040045F8 RID: 17912
		[Header("【終了】------------------------")]
		[SerializeField]
		private Button btnCancel;

		// Token: 0x040045F9 RID: 17913
		[SerializeField]
		private Button btnSave;

		// Token: 0x040045FA RID: 17914
		[SerializeField]
		private Text textSaveName;

		// Token: 0x040045FB RID: 17915
		private int handLPtn;

		// Token: 0x040045FC RID: 17916
		private int handRPtn;

		// Token: 0x040045FD RID: 17917
		private bool backAutoClothesState;

		// Token: 0x040045FE RID: 17918
		private int backClothesNo;

		// Token: 0x040045FF RID: 17919
		private int backAutoClothesStateNo;
	}
}
