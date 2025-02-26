using System;
using System.Linq;
using IllusionUtility.GetUtility;
using IllusionUtility.SetUtility;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Studio
{
	// Token: 0x020011E3 RID: 4579
	public class PVCopy : MonoBehaviour
	{
		// Token: 0x17001FE8 RID: 8168
		// (get) Token: 0x0600967B RID: 38523 RVA: 0x003E1A8E File Offset: 0x003DFE8E
		private bool enable
		{
			get
			{
				return this._enable.Any<bool>();
			}
		}

		// Token: 0x17001FE9 RID: 8169
		public bool this[int _idx]
		{
			get
			{
				return this._enable.SafeGet(_idx);
			}
			set
			{
				if (MathfEx.RangeEqualOn<int>(0, _idx, this._enable.Length - 1))
				{
					this._enable[_idx] = value;
				}
			}
		}

		// Token: 0x0600967E RID: 38526 RVA: 0x003E1ACA File Offset: 0x003DFECA
		private void Start()
		{
			(from _ in this.LateUpdateAsObservable()
			where this.enable
			select _).Subscribe(delegate(Unit _)
			{
				for (int i = 0; i < this.pv.Length; i++)
				{
					if (this._enable[i])
					{
						this.bone[i].transform.CopyPosRotScl(this.pv[i].transform);
					}
				}
			});
		}

		// Token: 0x0600967F RID: 38527 RVA: 0x003E1AF8 File Offset: 0x003DFEF8
		private void Reset()
		{
			string[] array = new string[]
			{
				"f_pv_arm_L",
				"f_pv_elbo_L",
				"f_pv_arm_R",
				"f_pv_elbo_R",
				"f_pv_leg_L",
				"f_pv_knee_L",
				"f_pv_leg_R",
				"f_pv_knee_R"
			};
			this.pv = new GameObject[8];
			for (int i = 0; i < array.Length; i++)
			{
				this.pv[i] = base.transform.FindLoop(array[i]);
			}
			string[] array2 = new string[]
			{
				"f_t_arm_L",
				"f_t_elbo_L",
				"f_t_arm_R",
				"f_t_elbo_R",
				"f_t_leg_L",
				"f_t_knee_L",
				"f_t_leg_R",
				"f_t_knee_R"
			};
			this.bone = new GameObject[8];
			for (int j = 0; j < array2.Length; j++)
			{
				this.bone[j] = base.transform.FindLoop(array2[j]);
			}
		}

		// Token: 0x040078F8 RID: 30968
		[SerializeField]
		private GameObject[] pv;

		// Token: 0x040078F9 RID: 30969
		[SerializeField]
		private GameObject[] bone;

		// Token: 0x040078FA RID: 30970
		private bool[] _enable = new bool[]
		{
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true
		};
	}
}
