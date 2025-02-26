using System;
using System.Collections.Generic;
using System.Linq;
using AIChara;
using Illusion.Extensions;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CharaCustom
{
	// Token: 0x02000997 RID: 2455
	public class CustomAcsCorrectSet : MonoBehaviour
	{
		// Token: 0x17000DB2 RID: 3506
		// (get) Token: 0x06004670 RID: 18032 RVA: 0x001AEF8D File Offset: 0x001AD38D
		private CustomBase customBase
		{
			get
			{
				return Singleton<CustomBase>.Instance;
			}
		}

		// Token: 0x17000DB3 RID: 3507
		// (get) Token: 0x06004671 RID: 18033 RVA: 0x001AEF94 File Offset: 0x001AD394
		private ChaControl chaCtrl
		{
			get
			{
				return this.customBase.chaCtrl;
			}
		}

		// Token: 0x17000DB4 RID: 3508
		// (get) Token: 0x06004672 RID: 18034 RVA: 0x001AEFA1 File Offset: 0x001AD3A1
		private ChaFileAccessory orgAcs
		{
			get
			{
				return this.chaCtrl.chaFile.coordinate.accessory;
			}
		}

		// Token: 0x17000DB5 RID: 3509
		// (get) Token: 0x06004673 RID: 18035 RVA: 0x001AEFB8 File Offset: 0x001AD3B8
		private ChaFileAccessory nowAcs
		{
			get
			{
				return this.chaCtrl.nowCoordinate.accessory;
			}
		}

		// Token: 0x17000DB6 RID: 3510
		// (get) Token: 0x06004674 RID: 18036 RVA: 0x001AEFCA File Offset: 0x001AD3CA
		private CustomBase.CustomSettingSave.AcsCtrlSetting acsCtrlSetting
		{
			get
			{
				return this.customBase.customSettingSave.acsCtrlSetting;
			}
		}

		// Token: 0x17000DB7 RID: 3511
		// (get) Token: 0x06004675 RID: 18037 RVA: 0x001AEFDC File Offset: 0x001AD3DC
		// (set) Token: 0x06004676 RID: 18038 RVA: 0x001AEFE4 File Offset: 0x001AD3E4
		public int slotNo { get; set; } = -1;

		// Token: 0x17000DB8 RID: 3512
		// (get) Token: 0x06004677 RID: 18039 RVA: 0x001AEFED File Offset: 0x001AD3ED
		// (set) Token: 0x06004678 RID: 18040 RVA: 0x001AEFF5 File Offset: 0x001AD3F5
		public int correctNo { get; set; } = -1;

		// Token: 0x06004679 RID: 18041 RVA: 0x001AF000 File Offset: 0x001AD400
		public void UpdateCustomUI()
		{
			if (this.slotNo == -1 || this.correctNo == -1)
			{
				return;
			}
			int posRate = this.acsCtrlSetting.correctSetting[this.correctNo].posRate;
			int rotRate = this.acsCtrlSetting.correctSetting[this.correctNo].rotRate;
			int sclRate = this.acsCtrlSetting.correctSetting[this.correctNo].sclRate;
			this.tglPosRate[posRate].SetIsOnWithoutCallback(true);
			this.tglRotRate[rotRate].SetIsOnWithoutCallback(true);
			this.tglSclRate[sclRate].SetIsOnWithoutCallback(true);
			for (int i = 0; i < 3; i++)
			{
				if (i != posRate)
				{
					this.tglPosRate[i].SetIsOnWithoutCallback(false);
				}
				if (i != rotRate)
				{
					this.tglRotRate[i].SetIsOnWithoutCallback(false);
				}
				if (i != sclRate)
				{
					this.tglSclRate[i].SetIsOnWithoutCallback(false);
				}
			}
			for (int j = 0; j < 3; j++)
			{
				this.inpPos[j].text = this.nowAcs.parts[this.slotNo].addMove[this.correctNo, 0][j].ToString();
				this.inpRot[j].text = this.nowAcs.parts[this.slotNo].addMove[this.correctNo, 1][j].ToString();
				this.inpScl[j].text = this.nowAcs.parts[this.slotNo].addMove[this.correctNo, 2][j].ToString();
			}
			this.tglDrawCtrl.SetIsOnWithoutCallback(this.acsCtrlSetting.correctSetting[this.correctNo].draw);
			this.tglCtrlType[this.acsCtrlSetting.correctSetting[this.correctNo].type].SetIsOnWithoutCallback(true);
			this.tglCtrlType[this.acsCtrlSetting.correctSetting[this.correctNo].type & 1].SetIsOnWithoutCallback(false);
			this.sldCtrlSpeed.value = this.acsCtrlSetting.correctSetting[this.correctNo].speed;
			this.sldCtrlSize.value = this.acsCtrlSetting.correctSetting[this.correctNo].scale;
		}

		// Token: 0x0600467A RID: 18042 RVA: 0x001AF284 File Offset: 0x001AD684
		public void UpdateDragValue(int type, int xyz, float move)
		{
			int[] array = new int[]
			{
				1,
				2,
				4
			};
			switch (type)
			{
			case 0:
			{
				float value = move * this.movePosValue[this.acsCtrlSetting.correctSetting[this.correctNo].posRate];
				this.chaCtrl.SetAccessoryPos(this.slotNo, this.correctNo, value, true, array[xyz]);
				this.orgAcs.parts[this.slotNo].addMove[this.correctNo, 0] = this.nowAcs.parts[this.slotNo].addMove[this.correctNo, 0];
				this.inpPos[xyz].text = this.nowAcs.parts[this.slotNo].addMove[this.correctNo, 0][xyz].ToString();
				break;
			}
			case 1:
			{
				float value2 = move * this.moveRotValue[this.acsCtrlSetting.correctSetting[this.correctNo].rotRate];
				this.chaCtrl.SetAccessoryRot(this.slotNo, this.correctNo, value2, true, array[xyz]);
				this.orgAcs.parts[this.slotNo].addMove[this.correctNo, 1] = this.nowAcs.parts[this.slotNo].addMove[this.correctNo, 1];
				this.inpRot[xyz].text = this.nowAcs.parts[this.slotNo].addMove[this.correctNo, 1][xyz].ToString();
				break;
			}
			case 2:
			{
				float value3 = move * this.moveSclValue[this.acsCtrlSetting.correctSetting[this.correctNo].sclRate];
				this.chaCtrl.SetAccessoryScl(this.slotNo, this.correctNo, value3, true, array[xyz]);
				this.orgAcs.parts[this.slotNo].addMove[this.correctNo, 2] = this.nowAcs.parts[this.slotNo].addMove[this.correctNo, 2];
				this.inpScl[xyz].text = this.nowAcs.parts[this.slotNo].addMove[this.correctNo, 2][xyz].ToString();
				break;
			}
			}
			this.SetControllerTransform();
		}

		// Token: 0x0600467B RID: 18043 RVA: 0x001AF540 File Offset: 0x001AD940
		public void SetControllerTransform()
		{
			Transform transform = this.chaCtrl.trfAcsMove[this.slotNo, this.correctNo];
			if (null == transform)
			{
				return;
			}
			if (null == this.cmpGuid)
			{
				return;
			}
			this.cmpGuid.amount.position = transform.position;
			this.cmpGuid.amount.rotation = transform.eulerAngles;
		}

		// Token: 0x0600467C RID: 18044 RVA: 0x001AF5B8 File Offset: 0x001AD9B8
		public void SetAccessoryTransform(bool updateInfo)
		{
			Transform transform = this.chaCtrl.trfAcsMove[this.slotNo, this.correctNo];
			if (null == transform)
			{
				return;
			}
			if (null == this.cmpGuid)
			{
				return;
			}
			if (this.customBase.customSettingSave.acsCtrlSetting.correctSetting[this.correctNo].type == 0)
			{
				transform.position = this.cmpGuid.amount.position;
				if (updateInfo)
				{
					Vector3 localPosition = transform.localPosition;
					localPosition.x = Mathf.Clamp(localPosition.x * 10f, -100f, 100f);
					localPosition.y = Mathf.Clamp(localPosition.y * 10f, -100f, 100f);
					localPosition.z = Mathf.Clamp(localPosition.z * 10f, -100f, 100f);
					this.chaCtrl.SetAccessoryPos(this.slotNo, this.correctNo, localPosition.x, false, 1);
					this.chaCtrl.SetAccessoryPos(this.slotNo, this.correctNo, localPosition.y, false, 2);
					this.chaCtrl.SetAccessoryPos(this.slotNo, this.correctNo, localPosition.z, false, 4);
					this.orgAcs.parts[this.slotNo].addMove[this.correctNo, 0] = this.nowAcs.parts[this.slotNo].addMove[this.correctNo, 0];
					this.chaCtrl.UpdateAccessoryMoveFromInfo(this.slotNo);
					this.cmpGuid.amount.position = transform.position;
				}
			}
			else
			{
				transform.eulerAngles = this.cmpGuid.amount.rotation;
				if (updateInfo)
				{
					Vector3 localEulerAngles = transform.localEulerAngles;
					this.chaCtrl.SetAccessoryRot(this.slotNo, this.correctNo, localEulerAngles.x, false, 1);
					this.chaCtrl.SetAccessoryRot(this.slotNo, this.correctNo, localEulerAngles.y, false, 2);
					this.chaCtrl.SetAccessoryRot(this.slotNo, this.correctNo, localEulerAngles.z, false, 4);
					this.orgAcs.parts[this.slotNo].addMove[this.correctNo, 1] = this.nowAcs.parts[this.slotNo].addMove[this.correctNo, 1];
					this.chaCtrl.UpdateAccessoryMoveFromInfo(this.slotNo);
					this.cmpGuid.amount.rotation = transform.eulerAngles;
				}
			}
			this.UpdateCustomUI();
		}

		// Token: 0x0600467D RID: 18045 RVA: 0x001AF890 File Offset: 0x001ADC90
		public void UpdateDrawControllerState()
		{
			int type = this.customBase.customSettingSave.acsCtrlSetting.correctSetting[this.correctNo].type;
			bool draw = this.customBase.customSettingSave.acsCtrlSetting.correctSetting[this.correctNo].draw;
			float speed = this.customBase.customSettingSave.acsCtrlSetting.correctSetting[this.correctNo].speed;
			float scale = this.customBase.customSettingSave.acsCtrlSetting.correctSetting[this.correctNo].scale;
			this.tglDrawCtrl.SetIsOnWithoutCallback(draw);
			this.tglCtrlType[type].SetIsOnWithoutCallback(true);
			this.sldCtrlSpeed.value = speed;
			this.sldCtrlSize.value = scale;
		}

		// Token: 0x0600467E RID: 18046 RVA: 0x001AF957 File Offset: 0x001ADD57
		public bool IsDrag()
		{
			return null != this.cmpGuid && this.cmpGuid.isDrag;
		}

		// Token: 0x0600467F RID: 18047 RVA: 0x001AF97D File Offset: 0x001ADD7D
		public void ShortcutChangeGuidType(int type)
		{
			if (null != this.cmpGuid && !this.cmpGuid.isDrag)
			{
				this.tglCtrlType[type].isOn = true;
			}
		}

		// Token: 0x06004680 RID: 18048 RVA: 0x001AF9B0 File Offset: 0x001ADDB0
		public void CreateGuid(GameObject objParent)
		{
			Transform parent = (!(null == objParent)) ? objParent.transform : null;
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.tmpObjGuid);
			gameObject.transform.SetParent(parent);
			this.cmpGuid = gameObject.GetComponent<CustomGuideObject>();
			gameObject.SetActiveIfDifferent(true);
		}

		// Token: 0x06004681 RID: 18049 RVA: 0x001AFA04 File Offset: 0x001ADE04
		public void Initialize(int _slotNo, int _correctNo)
		{
			CustomAcsCorrectSet.<Initialize>c__AnonStorey1 <Initialize>c__AnonStorey = new CustomAcsCorrectSet.<Initialize>c__AnonStorey1();
			<Initialize>c__AnonStorey.$this = this;
			this.slotNo = _slotNo;
			this.correctNo = _correctNo;
			if (this.slotNo == -1 || this.correctNo == -1)
			{
				return;
			}
			if (this.title)
			{
				this.title.text = string.Format("{0}{1:00}", "調整", this.correctNo + 1);
			}
			this.UpdateCustomUI();
			if (this.lstDisposable != null && this.lstDisposable.Count != 0)
			{
				int count = this.lstDisposable.Count;
				for (int j = 0; j < count; j++)
				{
					this.lstDisposable[j].Dispose();
				}
			}
			<Initialize>c__AnonStorey.disposable = null;
			this.tglPosRate.Select((Toggle p, int i) => new
			{
				toggle = p,
				index = (byte)i
			}).ToList().ForEach(delegate(p)
			{
				<Initialize>c__AnonStorey.disposable = (from isOn in p.toggle.OnValueChangedAsObservable()
				where isOn
				select isOn).Subscribe(delegate(bool _)
				{
					<Initialize>c__AnonStorey.acsCtrlSetting.correctSetting[<Initialize>c__AnonStorey.correctNo].posRate = (int)p.index;
				});
				<Initialize>c__AnonStorey.$this.lstDisposable.Add(<Initialize>c__AnonStorey.disposable);
			});
			this.tglRotRate.Select((Toggle p, int i) => new
			{
				toggle = p,
				index = (byte)i
			}).ToList().ForEach(delegate(p)
			{
				<Initialize>c__AnonStorey.disposable = (from isOn in p.toggle.OnValueChangedAsObservable()
				where isOn
				select isOn).Subscribe(delegate(bool _)
				{
					<Initialize>c__AnonStorey.acsCtrlSetting.correctSetting[<Initialize>c__AnonStorey.correctNo].rotRate = (int)p.index;
				});
				<Initialize>c__AnonStorey.$this.lstDisposable.Add(<Initialize>c__AnonStorey.disposable);
			});
			this.tglSclRate.Select((Toggle p, int i) => new
			{
				toggle = p,
				index = (byte)i
			}).ToList().ForEach(delegate(p)
			{
				<Initialize>c__AnonStorey.disposable = (from isOn in p.toggle.OnValueChangedAsObservable()
				where isOn
				select isOn).Subscribe(delegate(bool _)
				{
					<Initialize>c__AnonStorey.acsCtrlSetting.correctSetting[<Initialize>c__AnonStorey.correctNo].sclRate = (int)p.index;
				});
				<Initialize>c__AnonStorey.$this.lstDisposable.Add(<Initialize>c__AnonStorey.disposable);
			});
			<Initialize>c__AnonStorey.downTimeCnt = 0f;
			<Initialize>c__AnonStorey.loopTimeCnt = 0f;
			<Initialize>c__AnonStorey.change = false;
			<Initialize>c__AnonStorey.flag = new int[]
			{
				1,
				2,
				4
			};
			this.btnPos.Select((Button p, int i) => new
			{
				btn = p,
				index = i
			}).ToList().ForEach(delegate(p)
			{
				<Initialize>c__AnonStorey.disposable = p.btn.OnClickAsObservable().Subscribe(delegate(Unit _)
				{
					if (!<Initialize>c__AnonStorey.change)
					{
						int num = p.index / 2;
						int num2 = (p.index % 2 != 0) ? 1 : -1;
						if (num == 0)
						{
							num2 *= -1;
						}
						float value = (float)num2 * <Initialize>c__AnonStorey.movePosValue[<Initialize>c__AnonStorey.acsCtrlSetting.correctSetting[<Initialize>c__AnonStorey.correctNo].posRate];
						<Initialize>c__AnonStorey.chaCtrl.SetAccessoryPos(<Initialize>c__AnonStorey.slotNo, <Initialize>c__AnonStorey.correctNo, value, true, <Initialize>c__AnonStorey.flag[num]);
						<Initialize>c__AnonStorey.orgAcs.parts[<Initialize>c__AnonStorey.slotNo].addMove[<Initialize>c__AnonStorey.correctNo, 0] = <Initialize>c__AnonStorey.nowAcs.parts[<Initialize>c__AnonStorey.slotNo].addMove[<Initialize>c__AnonStorey.correctNo, 0];
						<Initialize>c__AnonStorey.inpPos[num].text = <Initialize>c__AnonStorey.nowAcs.parts[<Initialize>c__AnonStorey.slotNo].addMove[<Initialize>c__AnonStorey.correctNo, 0][num].ToString();
						<Initialize>c__AnonStorey.SetControllerTransform();
					}
				});
				<Initialize>c__AnonStorey.$this.lstDisposable.Add(<Initialize>c__AnonStorey.disposable);
				<Initialize>c__AnonStorey.disposable = p.btn.UpdateAsObservable().SkipUntil(p.btn.OnPointerDownAsObservable().Do(delegate(PointerEventData _)
				{
					<Initialize>c__AnonStorey.downTimeCnt = 0f;
					<Initialize>c__AnonStorey.loopTimeCnt = 0f;
					<Initialize>c__AnonStorey.change = false;
				})).TakeUntil(p.btn.OnPointerUpAsObservable()).RepeatUntilDestroy(<Initialize>c__AnonStorey.$this).Subscribe(delegate(Unit _)
				{
					int num = p.index / 2;
					int num2 = (p.index % 2 != 0) ? 1 : -1;
					if (num == 0)
					{
						num2 *= -1;
					}
					float num3 = (float)num2 * <Initialize>c__AnonStorey.movePosValue[<Initialize>c__AnonStorey.acsCtrlSetting.correctSetting[<Initialize>c__AnonStorey.correctNo].posRate];
					float num4 = 0f;
					<Initialize>c__AnonStorey.downTimeCnt += Time.deltaTime;
					if (<Initialize>c__AnonStorey.downTimeCnt > 0.3f)
					{
						<Initialize>c__AnonStorey.loopTimeCnt += Time.deltaTime;
						while (<Initialize>c__AnonStorey.loopTimeCnt > 0.05f)
						{
							num4 += num3;
							<Initialize>c__AnonStorey.loopTimeCnt -= 0.05f;
						}
						if (num4 != 0f)
						{
							<Initialize>c__AnonStorey.chaCtrl.SetAccessoryPos(<Initialize>c__AnonStorey.slotNo, <Initialize>c__AnonStorey.correctNo, num4, true, <Initialize>c__AnonStorey.flag[num]);
							<Initialize>c__AnonStorey.orgAcs.parts[<Initialize>c__AnonStorey.slotNo].addMove[<Initialize>c__AnonStorey.correctNo, 0] = <Initialize>c__AnonStorey.nowAcs.parts[<Initialize>c__AnonStorey.slotNo].addMove[<Initialize>c__AnonStorey.correctNo, 0];
							<Initialize>c__AnonStorey.inpPos[num].text = <Initialize>c__AnonStorey.nowAcs.parts[<Initialize>c__AnonStorey.slotNo].addMove[<Initialize>c__AnonStorey.correctNo, 0][num].ToString();
							<Initialize>c__AnonStorey.change = true;
							<Initialize>c__AnonStorey.SetControllerTransform();
						}
					}
				}).AddTo(<Initialize>c__AnonStorey.$this);
				<Initialize>c__AnonStorey.$this.lstDisposable.Add(<Initialize>c__AnonStorey.disposable);
			});
			this.inpPos.Select((InputField p, int i) => new
			{
				inp = p,
				index = i
			}).ToList().ForEach(delegate(p)
			{
				<Initialize>c__AnonStorey.disposable = p.inp.onEndEdit.AsObservable<string>().Subscribe(delegate(string value)
				{
					int num = p.index % 3;
					float value2 = CustomBase.ConvertValueFromTextLimit(-100f, 100f, 1, value);
					p.inp.text = value2.ToString();
					<Initialize>c__AnonStorey.chaCtrl.SetAccessoryPos(<Initialize>c__AnonStorey.slotNo, <Initialize>c__AnonStorey.correctNo, value2, false, <Initialize>c__AnonStorey.flag[num]);
					<Initialize>c__AnonStorey.orgAcs.parts[<Initialize>c__AnonStorey.slotNo].addMove[<Initialize>c__AnonStorey.correctNo, 0] = <Initialize>c__AnonStorey.nowAcs.parts[<Initialize>c__AnonStorey.slotNo].addMove[<Initialize>c__AnonStorey.correctNo, 0];
					<Initialize>c__AnonStorey.SetControllerTransform();
				});
				<Initialize>c__AnonStorey.$this.lstDisposable.Add(<Initialize>c__AnonStorey.disposable);
			});
			this.btnPosReset.Select((Button p, int i) => new
			{
				btn = p,
				index = i
			}).ToList().ForEach(delegate(p)
			{
				<Initialize>c__AnonStorey.disposable = p.btn.OnClickAsObservable().Subscribe(delegate(Unit _)
				{
					<Initialize>c__AnonStorey.inpPos[p.index].text = "0";
					<Initialize>c__AnonStorey.chaCtrl.SetAccessoryPos(<Initialize>c__AnonStorey.slotNo, <Initialize>c__AnonStorey.correctNo, 0f, false, <Initialize>c__AnonStorey.flag[p.index]);
					<Initialize>c__AnonStorey.orgAcs.parts[<Initialize>c__AnonStorey.slotNo].addMove[<Initialize>c__AnonStorey.correctNo, 0] = <Initialize>c__AnonStorey.nowAcs.parts[<Initialize>c__AnonStorey.slotNo].addMove[<Initialize>c__AnonStorey.correctNo, 0];
					<Initialize>c__AnonStorey.SetControllerTransform();
				});
				<Initialize>c__AnonStorey.$this.lstDisposable.Add(<Initialize>c__AnonStorey.disposable);
			});
			this.btnRot.Select((Button p, int i) => new
			{
				btn = p,
				index = i
			}).ToList().ForEach(delegate(p)
			{
				<Initialize>c__AnonStorey.disposable = p.btn.OnClickAsObservable().Subscribe(delegate(Unit _)
				{
					if (!<Initialize>c__AnonStorey.change)
					{
						int num = p.index / 2;
						int num2 = (p.index % 2 != 0) ? 1 : -1;
						float value = (float)num2 * <Initialize>c__AnonStorey.moveRotValue[<Initialize>c__AnonStorey.acsCtrlSetting.correctSetting[<Initialize>c__AnonStorey.correctNo].rotRate];
						<Initialize>c__AnonStorey.chaCtrl.SetAccessoryRot(<Initialize>c__AnonStorey.slotNo, <Initialize>c__AnonStorey.correctNo, value, true, <Initialize>c__AnonStorey.flag[num]);
						<Initialize>c__AnonStorey.orgAcs.parts[<Initialize>c__AnonStorey.slotNo].addMove[<Initialize>c__AnonStorey.correctNo, 1] = <Initialize>c__AnonStorey.nowAcs.parts[<Initialize>c__AnonStorey.slotNo].addMove[<Initialize>c__AnonStorey.correctNo, 1];
						<Initialize>c__AnonStorey.inpRot[num].text = <Initialize>c__AnonStorey.nowAcs.parts[<Initialize>c__AnonStorey.slotNo].addMove[<Initialize>c__AnonStorey.correctNo, 1][num].ToString();
						<Initialize>c__AnonStorey.SetControllerTransform();
					}
				});
				<Initialize>c__AnonStorey.$this.lstDisposable.Add(<Initialize>c__AnonStorey.disposable);
				<Initialize>c__AnonStorey.disposable = p.btn.UpdateAsObservable().SkipUntil(p.btn.OnPointerDownAsObservable().Do(delegate(PointerEventData _)
				{
					<Initialize>c__AnonStorey.downTimeCnt = 0f;
					<Initialize>c__AnonStorey.loopTimeCnt = 0f;
					<Initialize>c__AnonStorey.change = false;
				})).TakeUntil(p.btn.OnPointerUpAsObservable()).RepeatUntilDestroy(<Initialize>c__AnonStorey.$this).Subscribe(delegate(Unit _)
				{
					int num = p.index / 2;
					int num2 = (p.index % 2 != 0) ? 1 : -1;
					float num3 = (float)num2 * <Initialize>c__AnonStorey.moveRotValue[<Initialize>c__AnonStorey.acsCtrlSetting.correctSetting[<Initialize>c__AnonStorey.correctNo].rotRate];
					float num4 = 0f;
					<Initialize>c__AnonStorey.downTimeCnt += Time.deltaTime;
					if (<Initialize>c__AnonStorey.downTimeCnt > 0.3f)
					{
						<Initialize>c__AnonStorey.loopTimeCnt += Time.deltaTime;
						while (<Initialize>c__AnonStorey.loopTimeCnt > 0.05f)
						{
							num4 += num3;
							<Initialize>c__AnonStorey.loopTimeCnt -= 0.05f;
						}
						if (num4 != 0f)
						{
							<Initialize>c__AnonStorey.chaCtrl.SetAccessoryRot(<Initialize>c__AnonStorey.slotNo, <Initialize>c__AnonStorey.correctNo, num4, true, <Initialize>c__AnonStorey.flag[num]);
							<Initialize>c__AnonStorey.orgAcs.parts[<Initialize>c__AnonStorey.slotNo].addMove[<Initialize>c__AnonStorey.correctNo, 1] = <Initialize>c__AnonStorey.nowAcs.parts[<Initialize>c__AnonStorey.slotNo].addMove[<Initialize>c__AnonStorey.correctNo, 1];
							<Initialize>c__AnonStorey.inpRot[num].text = <Initialize>c__AnonStorey.nowAcs.parts[<Initialize>c__AnonStorey.slotNo].addMove[<Initialize>c__AnonStorey.correctNo, 1][num].ToString();
							<Initialize>c__AnonStorey.change = true;
							<Initialize>c__AnonStorey.SetControllerTransform();
						}
					}
				}).AddTo(<Initialize>c__AnonStorey.$this);
				<Initialize>c__AnonStorey.$this.lstDisposable.Add(<Initialize>c__AnonStorey.disposable);
			});
			this.inpRot.Select((InputField p, int i) => new
			{
				inp = p,
				index = i
			}).ToList().ForEach(delegate(p)
			{
				<Initialize>c__AnonStorey.disposable = p.inp.onEndEdit.AsObservable<string>().Subscribe(delegate(string value)
				{
					int num = p.index % 3;
					float value2 = CustomBase.ConvertValueFromTextLimit(0f, 360f, 0, value);
					p.inp.text = value2.ToString();
					<Initialize>c__AnonStorey.chaCtrl.SetAccessoryRot(<Initialize>c__AnonStorey.slotNo, <Initialize>c__AnonStorey.correctNo, value2, false, <Initialize>c__AnonStorey.flag[num]);
					<Initialize>c__AnonStorey.orgAcs.parts[<Initialize>c__AnonStorey.slotNo].addMove[<Initialize>c__AnonStorey.correctNo, 1] = <Initialize>c__AnonStorey.nowAcs.parts[<Initialize>c__AnonStorey.slotNo].addMove[<Initialize>c__AnonStorey.correctNo, 1];
					<Initialize>c__AnonStorey.SetControllerTransform();
				});
				<Initialize>c__AnonStorey.$this.lstDisposable.Add(<Initialize>c__AnonStorey.disposable);
			});
			this.btnRotReset.Select((Button p, int i) => new
			{
				btn = p,
				index = i
			}).ToList().ForEach(delegate(p)
			{
				<Initialize>c__AnonStorey.disposable = p.btn.OnClickAsObservable().Subscribe(delegate(Unit _)
				{
					<Initialize>c__AnonStorey.inpRot[p.index].text = "0";
					<Initialize>c__AnonStorey.chaCtrl.SetAccessoryRot(<Initialize>c__AnonStorey.slotNo, <Initialize>c__AnonStorey.correctNo, 0f, false, <Initialize>c__AnonStorey.flag[p.index]);
					<Initialize>c__AnonStorey.orgAcs.parts[<Initialize>c__AnonStorey.slotNo].addMove[<Initialize>c__AnonStorey.correctNo, 1] = <Initialize>c__AnonStorey.nowAcs.parts[<Initialize>c__AnonStorey.slotNo].addMove[<Initialize>c__AnonStorey.correctNo, 1];
					<Initialize>c__AnonStorey.SetControllerTransform();
				});
				<Initialize>c__AnonStorey.$this.lstDisposable.Add(<Initialize>c__AnonStorey.disposable);
			});
			this.btnScl.Select((Button p, int i) => new
			{
				btn = p,
				index = i
			}).ToList().ForEach(delegate(p)
			{
				<Initialize>c__AnonStorey.disposable = p.btn.OnClickAsObservable().Subscribe(delegate(Unit _)
				{
					if (!<Initialize>c__AnonStorey.change)
					{
						int num = p.index / 2;
						int num2 = (p.index % 2 != 0) ? 1 : -1;
						float value = (float)num2 * <Initialize>c__AnonStorey.moveSclValue[<Initialize>c__AnonStorey.acsCtrlSetting.correctSetting[<Initialize>c__AnonStorey.correctNo].sclRate];
						<Initialize>c__AnonStorey.chaCtrl.SetAccessoryScl(<Initialize>c__AnonStorey.slotNo, <Initialize>c__AnonStorey.correctNo, value, true, <Initialize>c__AnonStorey.flag[num]);
						<Initialize>c__AnonStorey.orgAcs.parts[<Initialize>c__AnonStorey.slotNo].addMove[<Initialize>c__AnonStorey.correctNo, 2] = <Initialize>c__AnonStorey.nowAcs.parts[<Initialize>c__AnonStorey.slotNo].addMove[<Initialize>c__AnonStorey.correctNo, 2];
						<Initialize>c__AnonStorey.inpScl[num].text = <Initialize>c__AnonStorey.nowAcs.parts[<Initialize>c__AnonStorey.slotNo].addMove[<Initialize>c__AnonStorey.correctNo, 2][num].ToString();
					}
				});
				<Initialize>c__AnonStorey.$this.lstDisposable.Add(<Initialize>c__AnonStorey.disposable);
				<Initialize>c__AnonStorey.disposable = p.btn.UpdateAsObservable().SkipUntil(p.btn.OnPointerDownAsObservable().Do(delegate(PointerEventData _)
				{
					<Initialize>c__AnonStorey.downTimeCnt = 0f;
					<Initialize>c__AnonStorey.loopTimeCnt = 0f;
					<Initialize>c__AnonStorey.change = false;
				})).TakeUntil(p.btn.OnPointerUpAsObservable()).RepeatUntilDestroy(<Initialize>c__AnonStorey.$this).Subscribe(delegate(Unit _)
				{
					int num = p.index / 2;
					int num2 = (p.index % 2 != 0) ? 1 : -1;
					float num3 = (float)num2 * <Initialize>c__AnonStorey.moveSclValue[<Initialize>c__AnonStorey.acsCtrlSetting.correctSetting[<Initialize>c__AnonStorey.correctNo].sclRate];
					float num4 = 0f;
					<Initialize>c__AnonStorey.downTimeCnt += Time.deltaTime;
					if (<Initialize>c__AnonStorey.downTimeCnt > 0.3f)
					{
						<Initialize>c__AnonStorey.loopTimeCnt += Time.deltaTime;
						while (<Initialize>c__AnonStorey.loopTimeCnt > 0.05f)
						{
							num4 += num3;
							<Initialize>c__AnonStorey.loopTimeCnt -= 0.05f;
						}
						if (num4 != 0f)
						{
							<Initialize>c__AnonStorey.chaCtrl.SetAccessoryScl(<Initialize>c__AnonStorey.slotNo, <Initialize>c__AnonStorey.correctNo, num4, true, <Initialize>c__AnonStorey.flag[num]);
							<Initialize>c__AnonStorey.orgAcs.parts[<Initialize>c__AnonStorey.slotNo].addMove[<Initialize>c__AnonStorey.correctNo, 2] = <Initialize>c__AnonStorey.nowAcs.parts[<Initialize>c__AnonStorey.slotNo].addMove[<Initialize>c__AnonStorey.correctNo, 2];
							<Initialize>c__AnonStorey.inpScl[num].text = <Initialize>c__AnonStorey.nowAcs.parts[<Initialize>c__AnonStorey.slotNo].addMove[<Initialize>c__AnonStorey.correctNo, 2][num].ToString();
							<Initialize>c__AnonStorey.change = true;
						}
					}
				}).AddTo(<Initialize>c__AnonStorey.$this);
				<Initialize>c__AnonStorey.$this.lstDisposable.Add(<Initialize>c__AnonStorey.disposable);
			});
			this.inpScl.Select((InputField p, int i) => new
			{
				inp = p,
				index = i
			}).ToList().ForEach(delegate(p)
			{
				<Initialize>c__AnonStorey.disposable = p.inp.onEndEdit.AsObservable<string>().Subscribe(delegate(string value)
				{
					int num = p.index % 3;
					float value2 = CustomBase.ConvertValueFromTextLimit(0.01f, 100f, 2, value);
					p.inp.text = value2.ToString();
					<Initialize>c__AnonStorey.chaCtrl.SetAccessoryScl(<Initialize>c__AnonStorey.slotNo, <Initialize>c__AnonStorey.correctNo, value2, false, <Initialize>c__AnonStorey.flag[num]);
					<Initialize>c__AnonStorey.orgAcs.parts[<Initialize>c__AnonStorey.slotNo].addMove[<Initialize>c__AnonStorey.correctNo, 2] = <Initialize>c__AnonStorey.nowAcs.parts[<Initialize>c__AnonStorey.slotNo].addMove[<Initialize>c__AnonStorey.correctNo, 2];
				});
				<Initialize>c__AnonStorey.$this.lstDisposable.Add(<Initialize>c__AnonStorey.disposable);
			});
			this.btnSclReset.Select((Button p, int i) => new
			{
				btn = p,
				index = i
			}).ToList().ForEach(delegate(p)
			{
				<Initialize>c__AnonStorey.disposable = p.btn.OnClickAsObservable().Subscribe(delegate(Unit _)
				{
					<Initialize>c__AnonStorey.inpScl[p.index].text = "1";
					<Initialize>c__AnonStorey.chaCtrl.SetAccessoryScl(<Initialize>c__AnonStorey.slotNo, <Initialize>c__AnonStorey.correctNo, 1f, false, <Initialize>c__AnonStorey.flag[p.index]);
					<Initialize>c__AnonStorey.orgAcs.parts[<Initialize>c__AnonStorey.slotNo].addMove[<Initialize>c__AnonStorey.correctNo, 2] = <Initialize>c__AnonStorey.nowAcs.parts[<Initialize>c__AnonStorey.slotNo].addMove[<Initialize>c__AnonStorey.correctNo, 2];
				});
				<Initialize>c__AnonStorey.$this.lstDisposable.Add(<Initialize>c__AnonStorey.disposable);
			});
			<Initialize>c__AnonStorey.disposable = this.btnAllReset.OnClickAsObservable().Subscribe(delegate(Unit _)
			{
				for (int i = 0; i < 3; i++)
				{
					<Initialize>c__AnonStorey.$this.inpPos[i].text = "0";
					<Initialize>c__AnonStorey.$this.chaCtrl.SetAccessoryPos(<Initialize>c__AnonStorey.$this.slotNo, <Initialize>c__AnonStorey.$this.correctNo, 0f, false, <Initialize>c__AnonStorey.flag[i]);
					<Initialize>c__AnonStorey.$this.orgAcs.parts[<Initialize>c__AnonStorey.$this.slotNo].addMove[<Initialize>c__AnonStorey.$this.correctNo, 0] = <Initialize>c__AnonStorey.$this.nowAcs.parts[<Initialize>c__AnonStorey.$this.slotNo].addMove[<Initialize>c__AnonStorey.$this.correctNo, 0];
					<Initialize>c__AnonStorey.$this.SetControllerTransform();
					<Initialize>c__AnonStorey.$this.inpRot[i].text = "0";
					<Initialize>c__AnonStorey.$this.chaCtrl.SetAccessoryRot(<Initialize>c__AnonStorey.$this.slotNo, <Initialize>c__AnonStorey.$this.correctNo, 0f, false, <Initialize>c__AnonStorey.flag[i]);
					<Initialize>c__AnonStorey.$this.orgAcs.parts[<Initialize>c__AnonStorey.$this.slotNo].addMove[<Initialize>c__AnonStorey.$this.correctNo, 1] = <Initialize>c__AnonStorey.$this.nowAcs.parts[<Initialize>c__AnonStorey.$this.slotNo].addMove[<Initialize>c__AnonStorey.$this.correctNo, 1];
					<Initialize>c__AnonStorey.$this.SetControllerTransform();
					<Initialize>c__AnonStorey.$this.inpScl[i].text = "1";
					<Initialize>c__AnonStorey.$this.chaCtrl.SetAccessoryScl(<Initialize>c__AnonStorey.$this.slotNo, <Initialize>c__AnonStorey.$this.correctNo, 1f, false, <Initialize>c__AnonStorey.flag[i]);
					<Initialize>c__AnonStorey.$this.orgAcs.parts[<Initialize>c__AnonStorey.$this.slotNo].addMove[<Initialize>c__AnonStorey.$this.correctNo, 2] = <Initialize>c__AnonStorey.$this.nowAcs.parts[<Initialize>c__AnonStorey.$this.slotNo].addMove[<Initialize>c__AnonStorey.$this.correctNo, 2];
				}
			});
			this.lstDisposable.Add(<Initialize>c__AnonStorey.disposable);
			<Initialize>c__AnonStorey.disposable = this.tglDrawCtrl.OnValueChangedAsObservable().Subscribe(delegate(bool isOn)
			{
				<Initialize>c__AnonStorey.$this.customBase.customSettingSave.acsCtrlSetting.correctSetting[<Initialize>c__AnonStorey.$this.correctNo].draw = isOn;
			});
			this.lstDisposable.Add(<Initialize>c__AnonStorey.disposable);
			if (this.tglCtrlType.Any<Toggle>())
			{
				(from item in this.tglCtrlType.Select((Toggle val, int idx) => new
				{
					val,
					idx
				})
				where item.val != null
				select item).ToList().ForEach(delegate(item)
				{
					<Initialize>c__AnonStorey.disposable = (from isOn in item.val.OnValueChangedAsObservable()
					where isOn
					select isOn).Subscribe(delegate(bool isOn)
					{
						<Initialize>c__AnonStorey.customBase.customSettingSave.acsCtrlSetting.correctSetting[<Initialize>c__AnonStorey.correctNo].type = item.idx;
						if (<Initialize>c__AnonStorey.cmpGuid)
						{
							<Initialize>c__AnonStorey.cmpGuid.SetMode(item.idx);
						}
					});
					<Initialize>c__AnonStorey.$this.lstDisposable.Add(<Initialize>c__AnonStorey.disposable);
				});
			}
			<Initialize>c__AnonStorey.disposable = this.sldCtrlSpeed.OnValueChangedAsObservable().Subscribe(delegate(float val)
			{
				<Initialize>c__AnonStorey.$this.customBase.customSettingSave.acsCtrlSetting.correctSetting[<Initialize>c__AnonStorey.$this.correctNo].speed = val;
				if (<Initialize>c__AnonStorey.$this.cmpGuid)
				{
					<Initialize>c__AnonStorey.$this.cmpGuid.speedMove = val;
				}
			});
			this.lstDisposable.Add(<Initialize>c__AnonStorey.disposable);
			<Initialize>c__AnonStorey.disposable = this.sldCtrlSpeed.OnScrollAsObservable().Subscribe(delegate(PointerEventData scl)
			{
				if (<Initialize>c__AnonStorey.$this.customBase.sliderControlWheel)
				{
					<Initialize>c__AnonStorey.$this.sldCtrlSpeed.value = Mathf.Clamp(<Initialize>c__AnonStorey.$this.sldCtrlSpeed.value + scl.scrollDelta.y * -0.01f, 0.1f, 1f);
				}
			});
			this.lstDisposable.Add(<Initialize>c__AnonStorey.disposable);
			<Initialize>c__AnonStorey.disposable = this.sldCtrlSize.OnValueChangedAsObservable().Subscribe(delegate(float val)
			{
				<Initialize>c__AnonStorey.$this.customBase.customSettingSave.acsCtrlSetting.correctSetting[<Initialize>c__AnonStorey.$this.correctNo].scale = val;
				if (<Initialize>c__AnonStorey.$this.cmpGuid)
				{
					<Initialize>c__AnonStorey.$this.cmpGuid.scaleAxis = val;
					<Initialize>c__AnonStorey.$this.cmpGuid.UpdateScale();
				}
			});
			this.lstDisposable.Add(<Initialize>c__AnonStorey.disposable);
			<Initialize>c__AnonStorey.disposable = this.sldCtrlSize.OnScrollAsObservable().Subscribe(delegate(PointerEventData scl)
			{
				if (<Initialize>c__AnonStorey.$this.customBase.sliderControlWheel)
				{
					<Initialize>c__AnonStorey.$this.sldCtrlSize.value = Mathf.Clamp(<Initialize>c__AnonStorey.$this.sldCtrlSize.value + scl.scrollDelta.y * -0.01f, 0.3f, 3f);
				}
			});
			this.lstDisposable.Add(<Initialize>c__AnonStorey.disposable);
			this.UpdateDrawControllerState();
		}

		// Token: 0x06004682 RID: 18050 RVA: 0x001AFF98 File Offset: 0x001AE398
		private void Start()
		{
			for (int i = 0; i < 3; i++)
			{
				this.customBase.lstInputField.Add(this.inpPos[i]);
				this.customBase.lstInputField.Add(this.inpRot[i]);
				this.customBase.lstInputField.Add(this.inpScl[i]);
			}
		}

		// Token: 0x06004683 RID: 18051 RVA: 0x001B0000 File Offset: 0x001AE400
		private void LateUpdate()
		{
			if (null != this.cmpGuid && this.cmpGuid.gameObject.activeInHierarchy)
			{
				if (this.cmpGuid.isDrag)
				{
					this.SetAccessoryTransform(false);
				}
				else if (this.isDrag)
				{
					this.SetAccessoryTransform(true);
				}
				else
				{
					this.SetControllerTransform();
				}
				this.isDrag = this.cmpGuid.isDrag;
				this.customBase.cursorDraw = !this.cmpGuid.isDrag;
			}
		}

		// Token: 0x0400419C RID: 16796
		private readonly float[] movePosValue = new float[]
		{
			0.1f,
			1f,
			10f
		};

		// Token: 0x0400419D RID: 16797
		private readonly float[] moveRotValue = new float[]
		{
			1f,
			5f,
			10f
		};

		// Token: 0x0400419E RID: 16798
		private readonly float[] moveSclValue = new float[]
		{
			0.01f,
			0.1f,
			1f
		};

		// Token: 0x0400419F RID: 16799
		[SerializeField]
		private Text title;

		// Token: 0x040041A0 RID: 16800
		[SerializeField]
		private Toggle[] tglPosRate;

		// Token: 0x040041A1 RID: 16801
		[SerializeField]
		private Button[] btnPos;

		// Token: 0x040041A2 RID: 16802
		[SerializeField]
		private InputField[] inpPos;

		// Token: 0x040041A3 RID: 16803
		[SerializeField]
		private Button[] btnPosReset;

		// Token: 0x040041A4 RID: 16804
		[SerializeField]
		private Toggle[] tglRotRate;

		// Token: 0x040041A5 RID: 16805
		[SerializeField]
		private Button[] btnRot;

		// Token: 0x040041A6 RID: 16806
		[SerializeField]
		private InputField[] inpRot;

		// Token: 0x040041A7 RID: 16807
		[SerializeField]
		private Button[] btnRotReset;

		// Token: 0x040041A8 RID: 16808
		[SerializeField]
		private Toggle[] tglSclRate;

		// Token: 0x040041A9 RID: 16809
		[SerializeField]
		private Button[] btnScl;

		// Token: 0x040041AA RID: 16810
		[SerializeField]
		private InputField[] inpScl;

		// Token: 0x040041AB RID: 16811
		[SerializeField]
		private Button[] btnSclReset;

		// Token: 0x040041AC RID: 16812
		[SerializeField]
		private Button btnAllReset;

		// Token: 0x040041AD RID: 16813
		[SerializeField]
		private Toggle tglDrawCtrl;

		// Token: 0x040041AE RID: 16814
		[SerializeField]
		private Toggle[] tglCtrlType;

		// Token: 0x040041AF RID: 16815
		[SerializeField]
		private Slider sldCtrlSpeed;

		// Token: 0x040041B0 RID: 16816
		[SerializeField]
		private Slider sldCtrlSize;

		// Token: 0x040041B3 RID: 16819
		private List<IDisposable> lstDisposable = new List<IDisposable>();

		// Token: 0x040041B4 RID: 16820
		[SerializeField]
		private GameObject tmpObjGuid;

		// Token: 0x040041B5 RID: 16821
		private CustomGuideObject cmpGuid;

		// Token: 0x040041B6 RID: 16822
		private bool isDrag;
	}
}
