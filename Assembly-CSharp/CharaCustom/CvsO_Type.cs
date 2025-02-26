using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AIChara;
using Illusion.Extensions;
using Illusion.Game;
using Manager;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace CharaCustom
{
	// Token: 0x02000A06 RID: 2566
	public class CvsO_Type : CvsBase
	{
		// Token: 0x06004C0C RID: 19468 RVA: 0x001D3CB9 File Offset: 0x001D20B9
		public override void ChangeMenuFunc()
		{
			base.ChangeMenuFunc();
			base.customBase.customCtrl.showColorCvs = false;
			base.customBase.customCtrl.showFileList = false;
		}

		// Token: 0x06004C0D RID: 19469 RVA: 0x001D3CE3 File Offset: 0x001D20E3
		private void CalculateUI()
		{
			this.ssVoiceRate.SetSliderValue(base.parameter.voiceRate);
		}

		// Token: 0x06004C0E RID: 19470 RVA: 0x001D3CFC File Offset: 0x001D20FC
		public override void UpdateCustomUI()
		{
			base.UpdateCustomUI();
			this.CalculateUI();
			int[] array = base.customBase.dictPersonality.Keys.ToArray<int>();
			int num = Array.IndexOf<int>(array, base.parameter.personality);
			this.tglType[num].SetIsOnWithoutCallback(true);
			for (int i = 0; i < this.tglType.Length; i++)
			{
				if (i != num)
				{
					this.tglType[i].SetIsOnWithoutCallback(false);
				}
			}
		}

		// Token: 0x06004C0F RID: 19471 RVA: 0x001D3D80 File Offset: 0x001D2180
		public IEnumerator SetInputText()
		{
			yield return new WaitUntil(() => null != base.chaCtrl);
			this.ssVoiceRate.SetInputTextValue(CustomBase.ConvertTextFromRate(0, 100, base.parameter.voiceRate));
			yield break;
		}

		// Token: 0x06004C10 RID: 19472 RVA: 0x001D3D9C File Offset: 0x001D219C
		public void PlayVoice()
		{
			if (!base.customBase.playVoiceBackup.playSampleVoice)
			{
				base.customBase.playVoiceBackup.backEyebrowPtn = base.chaCtrl.fileStatus.eyebrowPtn;
				base.customBase.playVoiceBackup.backEyesPtn = base.chaCtrl.fileStatus.eyesPtn;
				base.customBase.playVoiceBackup.backBlink = base.chaCtrl.fileStatus.eyesBlink;
				base.customBase.playVoiceBackup.backEyesOpen = base.chaCtrl.fileStatus.eyesOpenMax;
				base.customBase.playVoiceBackup.backMouthPtn = base.chaCtrl.fileStatus.mouthPtn;
				base.customBase.playVoiceBackup.backMouthFix = base.chaCtrl.fileStatus.mouthFixed;
				base.customBase.playVoiceBackup.backMouthOpen = base.chaCtrl.fileStatus.mouthOpenMax;
			}
			ListInfoBase listInfo = Singleton<Character>.Instance.chaListCtrl.GetListInfo(ChaListDefine.CategoryNo.cha_sample_voice, base.parameter.personality);
			if (listInfo == null)
			{
				return;
			}
			ChaListDefine.KeyType[] array = new ChaListDefine.KeyType[]
			{
				ChaListDefine.KeyType.Eyebrow01,
				ChaListDefine.KeyType.Eyebrow02,
				ChaListDefine.KeyType.Eyebrow03
			};
			ChaListDefine.KeyType[] array2 = new ChaListDefine.KeyType[]
			{
				ChaListDefine.KeyType.Eye01,
				ChaListDefine.KeyType.Eye02,
				ChaListDefine.KeyType.Eye03
			};
			ChaListDefine.KeyType[] array3 = new ChaListDefine.KeyType[]
			{
				ChaListDefine.KeyType.EyeMax01,
				ChaListDefine.KeyType.EyeMax02,
				ChaListDefine.KeyType.EyeMax03
			};
			ChaListDefine.KeyType[] array4 = new ChaListDefine.KeyType[]
			{
				ChaListDefine.KeyType.Mouth01,
				ChaListDefine.KeyType.Mouth02,
				ChaListDefine.KeyType.Mouth03
			};
			ChaListDefine.KeyType[] array5 = new ChaListDefine.KeyType[]
			{
				ChaListDefine.KeyType.MouthMax01,
				ChaListDefine.KeyType.MouthMax02,
				ChaListDefine.KeyType.MouthMax03
			};
			ChaListDefine.KeyType[] array6 = new ChaListDefine.KeyType[]
			{
				ChaListDefine.KeyType.EyeHiLight01,
				ChaListDefine.KeyType.EyeHiLight02,
				ChaListDefine.KeyType.EyeHiLight03
			};
			ChaListDefine.KeyType[] array7 = new ChaListDefine.KeyType[]
			{
				ChaListDefine.KeyType.Data01,
				ChaListDefine.KeyType.Data02,
				ChaListDefine.KeyType.Data03
			};
			int num = this.voiceCnt[base.parameter.personality] = (this.voiceCnt[base.parameter.personality] + 1) % array.Length;
			base.chaCtrl.ChangeEyebrowPtn(listInfo.GetInfoInt(array[num]), true);
			base.chaCtrl.ChangeEyesPtn(listInfo.GetInfoInt(array2[num]), true);
			base.chaCtrl.HideEyeHighlight("0" == listInfo.GetInfo(array6[num]));
			base.chaCtrl.ChangeEyesBlinkFlag(false);
			base.chaCtrl.ChangeEyesOpenMax(listInfo.GetInfoFloat(array3[num]));
			base.chaCtrl.ChangeMouthPtn(listInfo.GetInfoInt(array4[num]), true);
			base.chaCtrl.ChangeMouthFixed(false);
			base.chaCtrl.ChangeMouthOpenMax(listInfo.GetInfoFloat(array5[num]));
			base.customBase.playVoiceBackup.playSampleVoice = true;
			Singleton<Sound>.Instance.Stop(Sound.Type.SystemSE);
			Transform transform = Illusion.Game.Utils.Sound.Play(new Illusion.Game.Utils.Sound.Setting
			{
				type = Sound.Type.SystemSE,
				assetBundleName = listInfo.GetInfo(ChaListDefine.KeyType.MainAB),
				assetName = listInfo.GetInfo(array7[num])
			});
			this.audioSource = transform.GetComponent<AudioSource>();
			this.audioSource.pitch = base.parameter.voicePitch;
			base.chaCtrl.SetVoiceTransform(transform);
		}

		// Token: 0x06004C11 RID: 19473 RVA: 0x001D40AC File Offset: 0x001D24AC
		protected override void Start()
		{
			base.Start();
			base.customBase.actUpdateCvsType += this.UpdateCustomUI;
			this.voiceCnt = new int[base.customBase.dictPersonality.Count];
			this.tglType = new Toggle[base.customBase.dictPersonality.Keys.Count];
			foreach (var <>__AnonType in base.customBase.dictPersonality.Select((KeyValuePair<int, string> val, int idx) => new
			{
				val,
				idx
			}))
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.objTemp);
				gameObject.name = "tglRbSel_" + <>__AnonType.idx.ToString("00");
				this.tglType[<>__AnonType.idx] = gameObject.GetComponent<Toggle>();
				ToggleGroup component = this.objTop.GetComponent<ToggleGroup>();
				this.tglType[<>__AnonType.idx].group = component;
				gameObject.transform.SetParent(this.objTop.transform, false);
				Transform transform = gameObject.transform.Find("textRbSelect");
				if (null != transform)
				{
					Text component2 = transform.GetComponent<Text>();
					if (component2)
					{
						component2.text = <>__AnonType.val.Value;
					}
				}
				gameObject.SetActiveIfDifferent(true);
			}
			this.tglType.Select((Toggle p, int idx) => new
			{
				toggle = p,
				index = (byte)idx
			}).ToList().ForEach(delegate(p)
			{
				p.toggle.onValueChanged.AsObservable<bool>().Subscribe(delegate(bool isOn)
				{
					if (!this.customBase.updateCustomUI && isOn)
					{
						int[] array = this.customBase.dictPersonality.Keys.ToArray<int>();
						this.parameter.personality = array[(int)p.index];
						this.PlayVoice();
					}
				});
			});
			this.ssVoiceRate.onChange = delegate(float value)
			{
				base.parameter.voiceRate = value;
				if (Singleton<Sound>.Instance.IsPlay(Sound.Type.SystemSE, null))
				{
					this.audioSource.pitch = base.parameter.voicePitch;
				}
			};
			this.ssVoiceRate.onPointerUp = delegate()
			{
				this.PlayVoice();
			};
			this.ssVoiceRate.onSetDefaultValue = (() => 0.5f);
			base.StartCoroutine(this.SetInputText());
		}

		// Token: 0x040045CD RID: 17869
		[SerializeField]
		private GameObject objTop;

		// Token: 0x040045CE RID: 17870
		[SerializeField]
		private GameObject objTemp;

		// Token: 0x040045CF RID: 17871
		[SerializeField]
		private CustomSliderSet ssVoiceRate;

		// Token: 0x040045D0 RID: 17872
		private Toggle[] tglType;

		// Token: 0x040045D1 RID: 17873
		private AudioSource audioSource;

		// Token: 0x040045D2 RID: 17874
		private int[] voiceCnt;
	}
}
