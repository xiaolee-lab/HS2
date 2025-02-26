using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using AIChara;
using AIProject;
using UnityEngine;

namespace Studio
{
	// Token: 0x020011E7 RID: 4583
	[DefaultExecutionOrder(-1)]
	public class YureCtrl : MonoBehaviour
	{
		// Token: 0x17001FF3 RID: 8179
		// (get) Token: 0x0600969D RID: 38557 RVA: 0x003E2488 File Offset: 0x003E0888
		// (set) Token: 0x0600969E RID: 38558 RVA: 0x003E2490 File Offset: 0x003E0890
		public int FemaleID { get; private set; }

		// Token: 0x17001FF4 RID: 8180
		// (get) Token: 0x0600969F RID: 38559 RVA: 0x003E2499 File Offset: 0x003E0899
		// (set) Token: 0x060096A0 RID: 38560 RVA: 0x003E24A1 File Offset: 0x003E08A1
		public bool IsInit { get; private set; }

		// Token: 0x17001FF5 RID: 8181
		// (get) Token: 0x060096A1 RID: 38561 RVA: 0x003E24AA File Offset: 0x003E08AA
		// (set) Token: 0x060096A2 RID: 38562 RVA: 0x003E24B2 File Offset: 0x003E08B2
		private OCIChar OCIChar { get; set; }

		// Token: 0x17001FF6 RID: 8182
		// (get) Token: 0x060096A3 RID: 38563 RVA: 0x003E24BC File Offset: 0x003E08BC
		private ChaControl ChaControl
		{
			[CompilerGenerated]
			get
			{
				ChaControl result;
				if ((result = this._chaControl) == null)
				{
					OCIChar ocichar = this.OCIChar;
					result = (this._chaControl = ((ocichar != null) ? ocichar.charInfo : null));
				}
				return result;
			}
		}

		// Token: 0x060096A4 RID: 38564 RVA: 0x003E24F4 File Offset: 0x003E08F4
		private void LateUpdate()
		{
			if (!this.IsInit)
			{
				return;
			}
			if (this.ChaControl != null)
			{
				this.Proc(this.ChaControl.getAnimatorStateInfo(0));
			}
		}

		// Token: 0x060096A5 RID: 38565 RVA: 0x003E2528 File Offset: 0x003E0928
		public void Init(OCIChar _ocic)
		{
			this.OCIChar = _ocic;
			for (int i = 0; i < 2; i++)
			{
				this.aBreastShape[i].MemberInit();
				this.aBreastShapeEnable[i].MemberInit();
			}
		}

		// Token: 0x060096A6 RID: 38566 RVA: 0x003E2570 File Offset: 0x003E0970
		public bool Load(string _bundle, string _file, int _motionId, int _femaleID)
		{
			this.IsInit = false;
			this.ResetShape(true);
			if (!GlobalMethod.AssetFileExist(_bundle, _file, string.Empty))
			{
				return false;
			}
			this.FemaleID = _femaleID;
			this.lstInfo.Clear();
			ExcelData excelData = CommonLib.LoadAsset<ExcelData>(_bundle, _file, false, string.Empty);
			string[] array = _file.Split(new char[]
			{
				'_'
			});
			string[] source = new string[]
			{
				"ail",
				"ai3p"
			};
			bool flag = source.Contains(array[0]);
			int[] array2 = new int[]
			{
				2,
				3,
				4,
				5,
				6,
				7,
				8
			};
			int work = 0;
			foreach (ExcelData.Param param in from v in excelData.list
			where int.TryParse(v.list.SafeGet(0), out work) && work == _motionId
			select v)
			{
				int num = 1;
				YureCtrl.Info info = new YureCtrl.Info();
				List<string> list = param.list;
				int nFemale = 0;
				if (flag && !int.TryParse(list.GetElement(num++), out nFemale))
				{
					nFemale = 0;
				}
				info.nFemale = nFemale;
				info.nameAnimation = list.GetElement(num++);
				info.aIsActive[0] = (list.GetElement(num++) == "1");
				info.aBreastShape[0].MemberInit();
				for (int i = 0; i < array2.Length; i++)
				{
					info.aBreastShape[0].breast[i] = (list.GetElement(num++) == "1");
				}
				info.aBreastShape[0].nip = (list.GetElement(num++) == "1");
				info.aIsActive[1] = (list.GetElement(num++) == "1");
				info.aBreastShape[1].MemberInit();
				for (int j = 0; j < array2.Length; j++)
				{
					info.aBreastShape[1].breast[j] = (list.GetElement(num++) == "1");
				}
				info.aBreastShape[1].nip = (list.GetElement(num++) == "1");
				info.aIsActive[2] = (list.GetElement(num++) == "1");
				info.aIsActive[3] = (list.GetElement(num++) == "1");
				this.lstInfo.Add(info);
			}
			this.IsInit = true;
			return true;
		}

		// Token: 0x060096A7 RID: 38567 RVA: 0x003E2878 File Offset: 0x003E0C78
		public bool Proc(AnimatorStateInfo _ai)
		{
			if (!this.IsInit)
			{
				return false;
			}
			YureCtrl.Info info = null;
			if (this.lstInfo != null)
			{
				for (int i = 0; i < this.lstInfo.Count; i++)
				{
					if (_ai.IsName(this.lstInfo[i].nameAnimation))
					{
						if (this.lstInfo[i].nFemale == this.FemaleID)
						{
							info = this.lstInfo[i];
							break;
						}
					}
				}
			}
			if (info != null)
			{
				this.Active(info.aIsActive);
				this.Shape(info.aBreastShape);
				return true;
			}
			this.Active(this.aYureEnableActive);
			this.Shape(this.aBreastShapeEnable);
			return false;
		}

		// Token: 0x060096A8 RID: 38568 RVA: 0x003E294C File Offset: 0x003E0D4C
		private void Active(bool[] _aIsActive)
		{
			for (int i = 0; i < this.aIsActive.Length; i++)
			{
				if (this.aIsActive[i] != _aIsActive[i])
				{
					if (i != 0)
					{
						if (i != 1)
						{
							this.OCIChar.EnableDynamicBonesBustAndHip(_aIsActive[i], i);
						}
						else
						{
							this.OCIChar.DynamicAnimeBustR = _aIsActive[i];
						}
					}
					else
					{
						this.OCIChar.DynamicAnimeBustL = _aIsActive[i];
					}
					this.aIsActive[i] = _aIsActive[i];
				}
			}
		}

		// Token: 0x060096A9 RID: 38569 RVA: 0x003E29E0 File Offset: 0x003E0DE0
		private void Shape(YureCtrl.BreastShapeInfo[] _shapeInfo)
		{
			for (int i = 0; i < 2; i++)
			{
				int lr = i;
				YureCtrl.BreastShapeInfo breastShapeInfo = _shapeInfo[i];
				YureCtrl.BreastShapeInfo breastShapeInfo2 = this.aBreastShape[i];
				if (breastShapeInfo.breast != breastShapeInfo2.breast)
				{
					for (int j = 0; j < ChaFileDefine.cf_BustShapeMaskID.Length - 1; j++)
					{
						int num = j;
						if (breastShapeInfo.breast[num] != breastShapeInfo2.breast[num])
						{
							if (breastShapeInfo.breast[num])
							{
								this.ChaControl.DisableShapeBodyID(lr, num, false);
							}
							else
							{
								this.ChaControl.DisableShapeBodyID(lr, num, true);
							}
						}
					}
					breastShapeInfo2.breast = breastShapeInfo.breast;
				}
				if (breastShapeInfo.nip != breastShapeInfo2.nip)
				{
					if (breastShapeInfo.nip)
					{
						this.ChaControl.DisableShapeBodyID(lr, 7, false);
					}
					else
					{
						this.ChaControl.DisableShapeBodyID(lr, 7, true);
					}
					breastShapeInfo2.nip = breastShapeInfo.nip;
				}
				this.aBreastShape[i] = breastShapeInfo2;
			}
		}

		// Token: 0x060096AA RID: 38570 RVA: 0x003E2B14 File Offset: 0x003E0F14
		public void ResetShape(bool _dynamicBone = true)
		{
			if (this.ChaControl == null)
			{
				return;
			}
			for (int i = 0; i < ChaFileDefine.cf_BustShapeMaskID.Length; i++)
			{
				this.ChaControl.DisableShapeBodyID(2, i, false);
			}
			for (int j = 0; j < 2; j++)
			{
				this.aBreastShape[j].MemberInit();
			}
			if (_dynamicBone)
			{
				for (int k = 0; k < this.aIsActive.Length; k++)
				{
					this.aIsActive[k] = true;
				}
				this.OCIChar.DynamicAnimeBustL = true;
				this.OCIChar.DynamicAnimeBustR = true;
				this.OCIChar.EnableDynamicBonesBustAndHip(true, 2);
				this.OCIChar.EnableDynamicBonesBustAndHip(true, 3);
			}
			this.IsInit = false;
			if (this.lstInfo != null)
			{
				this.lstInfo.Clear();
			}
		}

		// Token: 0x0400790C RID: 30988
		public List<YureCtrl.Info> lstInfo = new List<YureCtrl.Info>();

		// Token: 0x0400790F RID: 30991
		[Tooltip("動いているかの確認用")]
		public bool[] aIsActive = new bool[]
		{
			true,
			true,
			true,
			true
		};

		// Token: 0x04007910 RID: 30992
		[Tooltip("動いているかの確認用")]
		public YureCtrl.BreastShapeInfo[] aBreastShape = new YureCtrl.BreastShapeInfo[2];

		// Token: 0x04007911 RID: 30993
		private bool[] aYureEnableActive = new bool[]
		{
			true,
			true,
			true,
			true
		};

		// Token: 0x04007912 RID: 30994
		private YureCtrl.BreastShapeInfo[] aBreastShapeEnable = new YureCtrl.BreastShapeInfo[2];

		// Token: 0x04007914 RID: 30996
		private ChaControl _chaControl;

		// Token: 0x020011E8 RID: 4584
		[Serializable]
		public struct BreastShapeInfo
		{
			// Token: 0x060096AB RID: 38571 RVA: 0x003E2BF3 File Offset: 0x003E0FF3
			public void MemberInit()
			{
				this.breast = new bool[]
				{
					true,
					true,
					true,
					true,
					true,
					true,
					true
				};
				this.nip = true;
			}

			// Token: 0x04007915 RID: 30997
			public bool[] breast;

			// Token: 0x04007916 RID: 30998
			public bool nip;
		}

		// Token: 0x020011E9 RID: 4585
		[Serializable]
		public class Info
		{
			// Token: 0x04007917 RID: 30999
			public string nameAnimation = string.Empty;

			// Token: 0x04007918 RID: 31000
			public bool[] aIsActive = new bool[4];

			// Token: 0x04007919 RID: 31001
			public YureCtrl.BreastShapeInfo[] aBreastShape = new YureCtrl.BreastShapeInfo[2];

			// Token: 0x0400791A RID: 31002
			public int nFemale;
		}
	}
}
