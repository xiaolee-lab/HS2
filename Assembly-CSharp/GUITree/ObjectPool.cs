using System;
using System.Collections.Generic;
using UnityEngine.Events;

namespace GUITree
{
	// Token: 0x02001241 RID: 4673
	internal class ObjectPool<T> where T : new()
	{
		// Token: 0x0600999F RID: 39327 RVA: 0x003F3EA2 File Offset: 0x003F22A2
		public ObjectPool(UnityAction<T> actionOnGet, UnityAction<T> actionOnRelease)
		{
			this.m_ActionOnGet = actionOnGet;
			this.m_ActionOnRelease = actionOnRelease;
		}

		// Token: 0x170020B4 RID: 8372
		// (get) Token: 0x060099A0 RID: 39328 RVA: 0x003F3EC3 File Offset: 0x003F22C3
		// (set) Token: 0x060099A1 RID: 39329 RVA: 0x003F3ECB File Offset: 0x003F22CB
		public int countAll { get; private set; }

		// Token: 0x170020B5 RID: 8373
		// (get) Token: 0x060099A2 RID: 39330 RVA: 0x003F3ED4 File Offset: 0x003F22D4
		public int countActive
		{
			get
			{
				return this.countAll - this.countInactive;
			}
		}

		// Token: 0x170020B6 RID: 8374
		// (get) Token: 0x060099A3 RID: 39331 RVA: 0x003F3EE3 File Offset: 0x003F22E3
		public int countInactive
		{
			get
			{
				return this.m_Stack.Count;
			}
		}

		// Token: 0x060099A4 RID: 39332 RVA: 0x003F3EF0 File Offset: 0x003F22F0
		public T Get()
		{
			T t;
			if (this.m_Stack.Count == 0)
			{
				t = Activator.CreateInstance<T>();
				this.countAll++;
			}
			else
			{
				t = this.m_Stack.Pop();
			}
			if (this.m_ActionOnGet != null)
			{
				this.m_ActionOnGet(t);
			}
			return t;
		}

		// Token: 0x060099A5 RID: 39333 RVA: 0x003F3F4C File Offset: 0x003F234C
		public void Release(T element)
		{
			if (this.m_Stack.Count <= 0 || object.ReferenceEquals(this.m_Stack.Peek(), element))
			{
			}
			if (this.m_ActionOnRelease != null)
			{
				this.m_ActionOnRelease(element);
			}
			this.m_Stack.Push(element);
		}

		// Token: 0x04007AE4 RID: 31460
		private readonly Stack<T> m_Stack = new Stack<T>();

		// Token: 0x04007AE5 RID: 31461
		private readonly UnityAction<T> m_ActionOnGet;

		// Token: 0x04007AE6 RID: 31462
		private readonly UnityAction<T> m_ActionOnRelease;
	}
}
