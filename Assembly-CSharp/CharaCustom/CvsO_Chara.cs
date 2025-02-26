using System;
using System.Collections.Generic;
using AIChara;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace CharaCustom
{
	// Token: 0x02000A00 RID: 2560
	public class CvsO_Chara : CvsBase
	{
		// Token: 0x06004BDA RID: 19418 RVA: 0x001D18A9 File Offset: 0x001CFCA9
		public override void ChangeMenuFunc()
		{
			base.ChangeMenuFunc();
			base.customBase.customCtrl.showColorCvs = false;
			base.customBase.customCtrl.showFileList = false;
		}

		// Token: 0x06004BDB RID: 19419 RVA: 0x001D18D3 File Offset: 0x001CFCD3
		private void CalculateUI()
		{
			this.ddBirthMonth.value = (int)(base.parameter.birthMonth - 1);
			this.ddBirthDay.value = (int)(base.parameter.birthDay - 1);
		}

		// Token: 0x06004BDC RID: 19420 RVA: 0x001D1905 File Offset: 0x001CFD05
		public override void UpdateCustomUI()
		{
			base.UpdateCustomUI();
			this.CalculateUI();
			this.inpName.text = base.parameter.fullname;
		}

		// Token: 0x06004BDD RID: 19421 RVA: 0x001D192C File Offset: 0x001CFD2C
		public void UpdateBirthDayDD()
		{
			int num = (int)(base.parameter.birthDay - 1);
			this.ddBirthDay.ClearOptions();
			int[] array = new int[]
			{
				31,
				29,
				31,
				30,
				31,
				30,
				31,
				31,
				30,
				31,
				30,
				31
			};
			List<string> list = new List<string>();
			for (int i = 0; i < array[(int)(base.parameter.birthMonth - 1)]; i++)
			{
				list.Add((i + 1).ToString());
			}
			this.ddBirthDay.AddOptions(list);
			if (num > array[(int)(base.parameter.birthMonth - 1)] - 1)
			{
				this.ddBirthDay.value = 0;
			}
			else
			{
				this.ddBirthDay.value = num;
			}
			base.parameter.birthDay = (byte)(this.ddBirthDay.value + 1);
		}

		// Token: 0x06004BDE RID: 19422 RVA: 0x001D19FC File Offset: 0x001CFDFC
		protected override void Start()
		{
			base.Start();
			this.UpdateBirthDayDD();
			base.customBase.lstInputField.Add(this.inpName);
			base.customBase.actUpdateCvsChara += this.UpdateCustomUI;
			if (this.inpName)
			{
				this.inpName.ActivateInputField();
			}
			if (this.inpName)
			{
				this.inpName.OnEndEditAsObservable().Subscribe(delegate(string str)
				{
					base.parameter.fullname = str;
					base.customBase.changeCharaName = true;
				});
			}
			this.randomName.Initialize();
			if (this.btnRandom)
			{
				this.btnRandom.OnClickAsObservable().Subscribe(delegate(Unit _)
				{
					this.inpName.text = this.randomName.GetRandName(base.chaCtrl.sex);
					base.parameter.fullname = this.inpName.text;
					base.customBase.changeCharaName = true;
				});
			}
			this.ddBirthMonth.onValueChanged.AddListener(delegate(int idx)
			{
				if (!base.customBase.updateCustomUI && base.parameter.birthMonth != (byte)(idx + 1))
				{
					base.parameter.birthMonth = (byte)(idx + 1);
					this.UpdateBirthDayDD();
				}
			});
			this.ddBirthDay.onValueChanged.AddListener(delegate(int idx)
			{
				if (!base.customBase.updateCustomUI && base.parameter.birthDay != (byte)(idx + 1))
				{
					base.parameter.birthDay = (byte)(idx + 1);
				}
			});
		}

		// Token: 0x040045B4 RID: 17844
		[SerializeField]
		private InputField inpName;

		// Token: 0x040045B5 RID: 17845
		[SerializeField]
		private Button btnRandom;

		// Token: 0x040045B6 RID: 17846
		[SerializeField]
		private Dropdown ddBirthMonth;

		// Token: 0x040045B7 RID: 17847
		[SerializeField]
		private Dropdown ddBirthDay;

		// Token: 0x040045B8 RID: 17848
		private ChaRandomName randomName = new ChaRandomName();
	}
}
