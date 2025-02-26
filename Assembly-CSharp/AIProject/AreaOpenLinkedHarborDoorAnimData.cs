using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using AIProject.SaveData;
using Manager;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000BEC RID: 3052
	public class AreaOpenLinkedHarborDoorAnimData : HarborDoorAnimData
	{
		// Token: 0x170011C6 RID: 4550
		// (get) Token: 0x06005D40 RID: 23872 RVA: 0x00276B64 File Offset: 0x00274F64
		public int AreaOpenID
		{
			[CompilerGenerated]
			get
			{
				return this._areaOpenID;
			}
		}

		// Token: 0x06005D41 RID: 23873 RVA: 0x00276B6C File Offset: 0x00274F6C
		protected override void Awake()
		{
			base.Awake();
			if (base.DoorAnimator == null)
			{
				return;
			}
			this._openState = this.CheckAreaOpenState();
			if (this._openState)
			{
				base.PlayOpenIdleAnimation(false, 0f, 0f, 0);
			}
			else
			{
				base.PlayCloseIdleAnimation(false, 0f, 0f, 0);
			}
		}

		// Token: 0x06005D42 RID: 23874 RVA: 0x00276BD1 File Offset: 0x00274FD1
		private void OnEnable()
		{
			this.OnCheck(true);
		}

		// Token: 0x06005D43 RID: 23875 RVA: 0x00276BDC File Offset: 0x00274FDC
		public bool CheckAreaOpenState()
		{
			if (!Singleton<Game>.IsInstance())
			{
				return false;
			}
			AIProject.SaveData.Environment environment = Singleton<Game>.Instance.Environment;
			Dictionary<int, bool> dictionary = (environment != null) ? environment.AreaOpenState : null;
			bool flag;
			return !dictionary.IsNullOrEmpty<int, bool>() && dictionary.TryGetValue(this._areaOpenID, out flag) && flag;
		}

		// Token: 0x06005D44 RID: 23876 RVA: 0x00276C38 File Offset: 0x00275038
		private void OnCheck(bool forced = false)
		{
			bool flag = this.CheckAreaOpenState();
			if (flag != this._openState || forced)
			{
				if (flag)
				{
					base.PlayOpenIdleAnimation(false, 0f, 0f, 0);
				}
				else
				{
					base.PlayCloseIdleAnimation(false, 0f, 0f, 0);
				}
				this._openState = flag;
			}
		}

		// Token: 0x04005398 RID: 21400
		[SerializeField]
		private int _areaOpenID;

		// Token: 0x04005399 RID: 21401
		private bool _openState;
	}
}
