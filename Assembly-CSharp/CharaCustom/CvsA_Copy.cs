using System;
using System.Linq;
using AIChara;
using AIProject;
using Manager;
using MessagePack;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;

namespace CharaCustom
{
	// Token: 0x020009FE RID: 2558
	public class CvsA_Copy : CvsBase
	{
		// Token: 0x06004B93 RID: 19347 RVA: 0x001CE6E4 File Offset: 0x001CCAE4
		public override void ChangeMenuFunc()
		{
			base.ChangeMenuFunc();
			base.customBase.customCtrl.showColorCvs = false;
			base.customBase.customCtrl.showFileList = false;
		}

		// Token: 0x06004B94 RID: 19348 RVA: 0x001CE710 File Offset: 0x001CCB10
		public void CalculateUI()
		{
			for (int i = 0; i < 20; i++)
			{
				ListInfoBase listInfo = base.chaCtrl.lstCtrl.GetListInfo((ChaListDefine.CategoryNo)base.nowAcs.parts[i].type, base.nowAcs.parts[i].id);
				if (listInfo == null)
				{
					this.textDst[i].text = "なし";
					this.textSrc[i].text = "なし";
				}
				else
				{
					TextCorrectLimit.Correct(this.textDst[i], listInfo.Name, "…");
					this.textSrc[i].text = this.textDst[i].text;
				}
			}
		}

		// Token: 0x06004B95 RID: 19349 RVA: 0x001CE7C6 File Offset: 0x001CCBC6
		public override void UpdateCustomUI()
		{
			this.CalculateUI();
		}

		// Token: 0x06004B96 RID: 19350 RVA: 0x001CE7D0 File Offset: 0x001CCBD0
		private void CopyAccessory()
		{
			byte[] bytes = MessagePackSerializer.Serialize<ChaFileAccessory.PartsInfo>(base.nowAcs.parts[this.selSrc]);
			base.nowAcs.parts[this.selDst] = MessagePackSerializer.Deserialize<ChaFileAccessory.PartsInfo>(bytes);
			if (this.tglChgParentLR.isOn)
			{
				string reverseParent = ChaAccessoryDefine.GetReverseParent(base.nowAcs.parts[this.selDst].parentKey);
				if (string.Empty != reverseParent)
				{
					base.nowAcs.parts[this.selDst].parentKey = reverseParent;
				}
			}
			base.chaCtrl.AssignCoordinate();
			Singleton<Character>.Instance.customLoadGCClear = false;
			base.chaCtrl.Reload(false, true, true, true, true);
			Singleton<Character>.Instance.customLoadGCClear = true;
			this.CalculateUI();
			base.customBase.ChangeAcsSlotName(-1);
			base.customBase.forceUpdateAcsList = true;
			base.customBase.updateCvsAccessory = true;
		}

		// Token: 0x06004B97 RID: 19351 RVA: 0x001CE8C0 File Offset: 0x001CCCC0
		protected override void Start()
		{
			base.Start();
			base.customBase.actUpdateCvsAcsCopy += this.UpdateCustomUI;
			this.tglDst[this.selDst].isOn = true;
			this.tglSrc[this.selSrc].isOn = true;
			this.tglSrc.Select((UI_ToggleEx p, int index) => new
			{
				tgl = p,
				index = index
			}).ToList().ForEach(delegate(p)
			{
				(from isOn in p.tgl.OnValueChangedAsObservable()
				where isOn
				select isOn).Subscribe(delegate(bool isOn)
				{
					this.selSrc = p.index;
				});
			});
			this.tglDst.Select((UI_ToggleEx p, int index) => new
			{
				tgl = p,
				index = index
			}).ToList().ForEach(delegate(p)
			{
				(from isOn in p.tgl.OnValueChangedAsObservable()
				where isOn
				select isOn).Subscribe(delegate(bool isOn)
				{
					this.selDst = p.index;
				});
			});
			this.btnCopySlot.OnClickAsObservable().Subscribe(delegate(Unit _)
			{
				this.CopyAccessory();
				Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_S);
			});
			this.btnCopySlot.UpdateAsObservable().Subscribe(delegate(Unit _)
			{
				this.btnCopySlot.interactable = (this.selSrc != this.selDst);
			});
			this.btnCopy01.OnClickAsObservable().Subscribe(delegate(Unit _)
			{
				for (int i = 0; i < 3; i++)
				{
					base.nowAcs.parts[this.selDst].addMove[0, i] = (base.orgAcs.parts[this.selDst].addMove[0, i] = base.nowAcs.parts[this.selSrc].addMove[0, i]);
				}
				base.chaCtrl.UpdateAccessoryMoveFromInfo(this.selDst);
				base.customBase.updateCvsAccessory = true;
				Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_S);
			});
			this.btnCopy01.UpdateAsObservable().Subscribe(delegate(Unit _)
			{
				bool flag = true;
				flag &= (null != base.chaCtrl.cmpAccessory[this.selDst] && null != base.chaCtrl.cmpAccessory[this.selDst].trfMove01);
				flag &= (null != base.chaCtrl.cmpAccessory[this.selSrc] && null != base.chaCtrl.cmpAccessory[this.selSrc].trfMove01);
				flag &= (this.selSrc != this.selDst);
				this.btnCopy01.interactable = flag;
			});
			this.btnCopy02.OnClickAsObservable().Subscribe(delegate(Unit _)
			{
				for (int i = 0; i < 3; i++)
				{
					base.nowAcs.parts[this.selDst].addMove[1, i] = (base.orgAcs.parts[this.selDst].addMove[1, i] = base.nowAcs.parts[this.selSrc].addMove[1, i]);
				}
				base.chaCtrl.UpdateAccessoryMoveFromInfo(this.selDst);
				base.customBase.updateCvsAccessory = true;
				Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_S);
			});
			this.btnCopy02.UpdateAsObservable().Subscribe(delegate(Unit _)
			{
				bool flag = true;
				flag &= (null != base.chaCtrl.cmpAccessory[this.selDst] && null != base.chaCtrl.cmpAccessory[this.selDst].trfMove02);
				flag &= (null != base.chaCtrl.cmpAccessory[this.selSrc] && null != base.chaCtrl.cmpAccessory[this.selSrc].trfMove02);
				flag &= (this.selSrc != this.selDst);
				this.btnCopy02.interactable = flag;
			});
			this.btnRevLR01.OnClickAsObservable().Subscribe(delegate(Unit _)
			{
				for (int i = 0; i < 3; i++)
				{
					Vector3 vector = base.nowAcs.parts[this.selSrc].addMove[0, i];
					if (i == 1)
					{
						vector.y += 180f;
						if (vector.y >= 360f)
						{
							vector.y -= 360f;
						}
					}
					base.nowAcs.parts[this.selDst].addMove[0, i] = (base.orgAcs.parts[this.selDst].addMove[0, i] = vector);
				}
				base.chaCtrl.UpdateAccessoryMoveFromInfo(this.selDst);
				base.customBase.updateCvsAccessory = true;
				Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_S);
			});
			this.btnRevLR01.UpdateAsObservable().Subscribe(delegate(Unit _)
			{
				bool flag = true;
				flag &= (null != base.chaCtrl.cmpAccessory[this.selDst] && null != base.chaCtrl.cmpAccessory[this.selDst].trfMove01);
				flag &= (null != base.chaCtrl.cmpAccessory[this.selSrc] && null != base.chaCtrl.cmpAccessory[this.selSrc].trfMove01);
				flag &= (this.selSrc != this.selDst);
				this.btnRevLR01.interactable = flag;
			});
			this.btnRevLR02.OnClickAsObservable().Subscribe(delegate(Unit _)
			{
				for (int i = 0; i < 3; i++)
				{
					Vector3 vector = base.nowAcs.parts[this.selSrc].addMove[1, i];
					if (i == 1)
					{
						vector.y += 180f;
						if (vector.y >= 360f)
						{
							vector.y -= 360f;
						}
					}
					base.nowAcs.parts[this.selDst].addMove[1, i] = (base.orgAcs.parts[this.selDst].addMove[1, i] = vector);
				}
				base.chaCtrl.UpdateAccessoryMoveFromInfo(this.selDst);
				base.customBase.updateCvsAccessory = true;
				Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_S);
			});
			this.btnRevLR02.UpdateAsObservable().Subscribe(delegate(Unit _)
			{
				bool flag = true;
				flag &= (null != base.chaCtrl.cmpAccessory[this.selDst] && null != base.chaCtrl.cmpAccessory[this.selDst].trfMove02);
				flag &= (null != base.chaCtrl.cmpAccessory[this.selSrc] && null != base.chaCtrl.cmpAccessory[this.selSrc].trfMove02);
				flag &= (this.selSrc != this.selDst);
				this.btnRevLR02.interactable = flag;
			});
			this.btnRevTB01.OnClickAsObservable().Subscribe(delegate(Unit _)
			{
				for (int i = 0; i < 3; i++)
				{
					Vector3 vector = base.nowAcs.parts[this.selSrc].addMove[0, i];
					if (i == 1)
					{
						vector.x += 180f;
						if (vector.x >= 360f)
						{
							vector.x -= 360f;
						}
					}
					base.nowAcs.parts[this.selDst].addMove[0, i] = (base.orgAcs.parts[this.selDst].addMove[0, i] = vector);
				}
				base.chaCtrl.UpdateAccessoryMoveFromInfo(this.selDst);
				base.customBase.updateCvsAccessory = true;
				Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_S);
			});
			this.btnRevTB01.UpdateAsObservable().Subscribe(delegate(Unit _)
			{
				bool flag = true;
				flag &= (null != base.chaCtrl.cmpAccessory[this.selDst] && null != base.chaCtrl.cmpAccessory[this.selDst].trfMove01);
				flag &= (null != base.chaCtrl.cmpAccessory[this.selSrc] && null != base.chaCtrl.cmpAccessory[this.selSrc].trfMove01);
				flag &= (this.selSrc != this.selDst);
				this.btnRevTB01.interactable = flag;
			});
			this.btnRevTB02.OnClickAsObservable().Subscribe(delegate(Unit _)
			{
				for (int i = 0; i < 3; i++)
				{
					Vector3 vector = base.nowAcs.parts[this.selSrc].addMove[1, i];
					if (i == 1)
					{
						vector.x += 180f;
						if (vector.x >= 360f)
						{
							vector.x -= 360f;
						}
					}
					base.nowAcs.parts[this.selDst].addMove[1, i] = (base.orgAcs.parts[this.selDst].addMove[1, i] = vector);
				}
				base.chaCtrl.UpdateAccessoryMoveFromInfo(this.selDst);
				base.customBase.updateCvsAccessory = true;
				Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_S);
			});
			this.btnRevTB02.UpdateAsObservable().Subscribe(delegate(Unit _)
			{
				bool flag = true;
				flag &= (null != base.chaCtrl.cmpAccessory[this.selDst] && null != base.chaCtrl.cmpAccessory[this.selDst].trfMove02);
				flag &= (null != base.chaCtrl.cmpAccessory[this.selSrc] && null != base.chaCtrl.cmpAccessory[this.selSrc].trfMove02);
				flag &= (this.selSrc != this.selDst);
				this.btnRevTB02.interactable = flag;
			});
		}

		// Token: 0x0400457D RID: 17789
		[SerializeField]
		private UI_ToggleEx[] tglSrc;

		// Token: 0x0400457E RID: 17790
		[SerializeField]
		private Text[] textSrc;

		// Token: 0x0400457F RID: 17791
		[SerializeField]
		private UI_ToggleEx[] tglDst;

		// Token: 0x04004580 RID: 17792
		[SerializeField]
		private Text[] textDst;

		// Token: 0x04004581 RID: 17793
		[SerializeField]
		private Toggle tglChgParentLR;

		// Token: 0x04004582 RID: 17794
		[SerializeField]
		private Button btnCopySlot;

		// Token: 0x04004583 RID: 17795
		[SerializeField]
		private Button btnCopy01;

		// Token: 0x04004584 RID: 17796
		[SerializeField]
		private Button btnCopy02;

		// Token: 0x04004585 RID: 17797
		[SerializeField]
		private Button btnRevLR01;

		// Token: 0x04004586 RID: 17798
		[SerializeField]
		private Button btnRevLR02;

		// Token: 0x04004587 RID: 17799
		[SerializeField]
		private Button btnRevTB01;

		// Token: 0x04004588 RID: 17800
		[SerializeField]
		private Button btnRevTB02;

		// Token: 0x04004589 RID: 17801
		private int selSrc;

		// Token: 0x0400458A RID: 17802
		private int selDst;
	}
}
