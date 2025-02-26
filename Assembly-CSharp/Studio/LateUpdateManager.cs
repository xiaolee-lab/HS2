using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Studio
{
	// Token: 0x0200127F RID: 4735
	public class LateUpdateManager : Singleton<LateUpdateManager>
	{
		// Token: 0x17002175 RID: 8565
		// (get) Token: 0x06009C7A RID: 40058 RVA: 0x00400D74 File Offset: 0x003FF174
		// (set) Token: 0x06009C7B RID: 40059 RVA: 0x00400D90 File Offset: 0x003FF190
		public static bool reduceArraySizeWhenNeed
		{
			get
			{
				return Singleton<LateUpdateManager>.IsInstance() && Singleton<LateUpdateManager>.Instance.m_ReduceArraySizeWhenNeed;
			}
			set
			{
				if (Singleton<LateUpdateManager>.IsInstance())
				{
					Singleton<LateUpdateManager>.Instance.m_ReduceArraySizeWhenNeed = value;
				}
			}
		}

		// Token: 0x06009C7C RID: 40060 RVA: 0x00400DA7 File Offset: 0x003FF1A7
		public static void AddUpdatableST(ILateUpdatable _updatable)
		{
			if (_updatable == null || !Singleton<LateUpdateManager>.IsInstance())
			{
				return;
			}
			Singleton<LateUpdateManager>.Instance.AddUpdatable(_updatable);
		}

		// Token: 0x06009C7D RID: 40061 RVA: 0x00400DC8 File Offset: 0x003FF1C8
		private void AddUpdatable(ILateUpdatable _updatable)
		{
			if (this.arrayUpdatable.Length == this.tail)
			{
				Array.Resize<ILateUpdatable>(ref this.arrayUpdatable, checked(this.tail * 2));
			}
			this.arrayUpdatable[this.tail++] = _updatable;
		}

		// Token: 0x06009C7E RID: 40062 RVA: 0x00400E14 File Offset: 0x003FF214
		public static void RemoveUpdatableST(ILateUpdatable _updatable)
		{
			if (_updatable == null || !Singleton<LateUpdateManager>.IsInstance())
			{
				return;
			}
			Singleton<LateUpdateManager>.Instance.RemoveUpdatable(_updatable);
		}

		// Token: 0x06009C7F RID: 40063 RVA: 0x00400E34 File Offset: 0x003FF234
		private void RemoveUpdatable(ILateUpdatable _updatable)
		{
			for (int i = 0; i < this.arrayUpdatable.Length; i++)
			{
				if (this.arrayUpdatable[i] == _updatable)
				{
					this.arrayUpdatable[i] = null;
					return;
				}
			}
		}

		// Token: 0x06009C80 RID: 40064 RVA: 0x00400E72 File Offset: 0x003FF272
		public static void RefreshArrayUpdatableST()
		{
			if (!Singleton<LateUpdateManager>.IsInstance())
			{
				return;
			}
			Singleton<LateUpdateManager>.Instance.RefreshArrayUpdatable();
		}

		// Token: 0x06009C81 RID: 40065 RVA: 0x00400E8C File Offset: 0x003FF28C
		private void RefreshArrayUpdatable()
		{
			int num = this.tail - 1;
			for (int i = 0; i < this.arrayUpdatable.Length; i++)
			{
				if (this.arrayUpdatable[i] == null)
				{
					while (i < num)
					{
						ILateUpdatable lateUpdatable = this.arrayUpdatable[num];
						if (lateUpdatable != null)
						{
							this.arrayUpdatable[i] = lateUpdatable;
							this.arrayUpdatable[num] = null;
							num--;
							goto IL_63;
						}
						num--;
					}
					this.tail = i;
					break;
				}
				IL_63:;
			}
			if (this.m_ReduceArraySizeWhenNeed && this.tail < this.arrayUpdatable.Length / 2)
			{
				Array.Resize<ILateUpdatable>(ref this.arrayUpdatable, this.arrayUpdatable.Length / 2);
			}
		}

		// Token: 0x06009C82 RID: 40066 RVA: 0x00400F48 File Offset: 0x003FF348
		protected override void Awake()
		{
			if (!base.CheckInstance())
			{
				return;
			}
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
			this.LateUpdateAsObservable().Subscribe(delegate(Unit _)
			{
				for (int i = 0; i < this.tail; i++)
				{
					if (this.arrayUpdatable[i] != null)
					{
						this.arrayUpdatable[i].LateUpdateFunc();
					}
				}
			}).AddTo(this);
		}

		// Token: 0x04007CA6 RID: 31910
		private const int InitializeSize = 16;

		// Token: 0x04007CA7 RID: 31911
		private int tail;

		// Token: 0x04007CA8 RID: 31912
		private ILateUpdatable[] arrayUpdatable = new ILateUpdatable[16];

		// Token: 0x04007CA9 RID: 31913
		[SerializeField]
		private bool m_ReduceArraySizeWhenNeed;
	}
}
