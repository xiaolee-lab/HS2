using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Events;

namespace AIProject
{
	// Token: 0x02000960 RID: 2400
	public class ObjectPool<T> where T : new()
	{
		// Token: 0x0600429D RID: 17053 RVA: 0x001A2F9B File Offset: 0x001A139B
		public ObjectPool(UnityAction<T> actionOnGet, UnityAction<T> actionOnRelease)
		{
			this._actionOnGet = actionOnGet;
			this._actionOnRelease = actionOnRelease;
		}

		// Token: 0x17000C99 RID: 3225
		// (get) Token: 0x0600429E RID: 17054 RVA: 0x001A2FBC File Offset: 0x001A13BC
		// (set) Token: 0x0600429F RID: 17055 RVA: 0x001A2FC4 File Offset: 0x001A13C4
		public int countAll { get; private set; }

		// Token: 0x17000C9A RID: 3226
		// (get) Token: 0x060042A0 RID: 17056 RVA: 0x001A2FCD File Offset: 0x001A13CD
		public int countActive
		{
			[CompilerGenerated]
			get
			{
				return this.countAll - this.countInactive;
			}
		}

		// Token: 0x17000C9B RID: 3227
		// (get) Token: 0x060042A1 RID: 17057 RVA: 0x001A2FDC File Offset: 0x001A13DC
		public int countInactive
		{
			[CompilerGenerated]
			get
			{
				return this._stack.Count;
			}
		}

		// Token: 0x060042A2 RID: 17058 RVA: 0x001A2FEC File Offset: 0x001A13EC
		public T Get()
		{
			T t;
			if (this._stack.Count == 0)
			{
				t = Activator.CreateInstance<T>();
				this.countAll++;
			}
			else
			{
				t = this._stack.Pop();
			}
			if (this._actionOnGet != null)
			{
				this._actionOnGet(t);
			}
			return t;
		}

		// Token: 0x060042A3 RID: 17059 RVA: 0x001A3048 File Offset: 0x001A1448
		public void Release(T element)
		{
			if (this._stack.Count <= 0 || object.ReferenceEquals(this._stack.Peek(), element))
			{
			}
			if (this._actionOnRelease != null)
			{
				this._actionOnRelease(element);
			}
			this._stack.Push(element);
		}

		// Token: 0x04003F89 RID: 16265
		private readonly Stack<T> _stack = new Stack<T>();

		// Token: 0x04003F8A RID: 16266
		private readonly UnityAction<T> _actionOnGet;

		// Token: 0x04003F8B RID: 16267
		private readonly UnityAction<T> _actionOnRelease;
	}
}
