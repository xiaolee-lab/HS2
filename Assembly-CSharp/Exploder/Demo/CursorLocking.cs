using System;
using Exploder.Utils;
using UnityEngine;

namespace Exploder.Demo
{
	// Token: 0x0200037B RID: 891
	public class CursorLocking : MonoBehaviour
	{
		// Token: 0x06000FC7 RID: 4039 RVA: 0x00058A93 File Offset: 0x00056E93
		private void Awake()
		{
			CursorLocking.instance = this;
		}

		// Token: 0x06000FC8 RID: 4040 RVA: 0x00058A9C File Offset: 0x00056E9C
		private void Update()
		{
			if (this.LockCursor)
			{
				CursorLocking.Lock();
			}
			else
			{
				CursorLocking.Unlock();
			}
			CursorLocking.IsLocked = Compatibility.IsCursorLocked();
			if (Input.GetKeyDown(this.LockKey))
			{
				CursorLocking.Lock();
			}
			if (Input.GetKeyDown(this.UnlockKey))
			{
				CursorLocking.Unlock();
			}
			if (!Compatibility.IsCursorLocked())
			{
				Compatibility.SetCursorVisible(true);
			}
		}

		// Token: 0x06000FC9 RID: 4041 RVA: 0x00058B07 File Offset: 0x00056F07
		public static void Lock()
		{
			Compatibility.LockCursor(true);
			Compatibility.SetCursorVisible(false);
			CursorLocking.instance.LockCursor = true;
		}

		// Token: 0x06000FCA RID: 4042 RVA: 0x00058B20 File Offset: 0x00056F20
		public static void Unlock()
		{
			Compatibility.LockCursor(false);
			Compatibility.SetCursorVisible(true);
			CursorLocking.instance.LockCursor = false;
		}

		// Token: 0x04001174 RID: 4468
		public bool LockCursor;

		// Token: 0x04001175 RID: 4469
		public KeyCode LockKey;

		// Token: 0x04001176 RID: 4470
		public KeyCode UnlockKey;

		// Token: 0x04001177 RID: 4471
		public static bool IsLocked;

		// Token: 0x04001178 RID: 4472
		private static CursorLocking instance;
	}
}
