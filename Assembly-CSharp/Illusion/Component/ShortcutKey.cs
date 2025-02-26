using System;
using System.Collections.Generic;
using Manager;
using UnityEngine;
using UnityEngine.Events;

namespace Illusion.Component
{
	// Token: 0x0200104B RID: 4171
	public class ShortcutKey : MonoBehaviour
	{
		// Token: 0x17001EA9 RID: 7849
		// (set) Token: 0x06008C4A RID: 35914 RVA: 0x003ADC7A File Offset: 0x003AC07A
		public static bool Enable
		{
			set
			{
				ShortcutKey._Enable = value;
			}
		}

		// Token: 0x06008C4B RID: 35915 RVA: 0x003ADC82 File Offset: 0x003AC082
		private void Start()
		{
			ShortcutKey.list.Add(this);
		}

		// Token: 0x06008C4C RID: 35916 RVA: 0x003ADC8F File Offset: 0x003AC08F
		private void OnDestroy()
		{
			ShortcutKey.list.Remove(this);
		}

		// Token: 0x06008C4D RID: 35917 RVA: 0x003ADCA0 File Offset: 0x003AC0A0
		private void Update()
		{
			if (!ShortcutKey._Enable)
			{
				return;
			}
			foreach (ShortcutKey.Proc proc in this.procList)
			{
				if (proc.enabled && UnityEngine.Input.GetKeyDown(proc.keyCode))
				{
					proc.call.Invoke();
				}
			}
		}

		// Token: 0x06008C4E RID: 35918 RVA: 0x003ADD28 File Offset: 0x003AC128
		private bool IsReglate(string sceneName)
		{
			if (!Singleton<Scene>.IsInstance())
			{
				return true;
			}
			string loadSceneName = Singleton<Scene>.Instance.LoadSceneName;
			if (loadSceneName != null)
			{
				if (loadSceneName == "Init" || loadSceneName == "Logo")
				{
					return true;
				}
			}
			return Singleton<Scene>.Instance.IsNowLoadingFade || Singleton<Scene>.Instance.NowSceneNames.Contains(sceneName);
		}

		// Token: 0x06008C4F RID: 35919 RVA: 0x003ADDA2 File Offset: 0x003AC1A2
		public void _GameEnd()
		{
			if (this.IsReglate("Exit") || Singleton<Game>.Instance.ExitScene)
			{
				return;
			}
			Singleton<Scene>.Instance.GameEnd(true);
		}

		// Token: 0x0400724B RID: 29259
		private static bool _Enable = true;

		// Token: 0x0400724C RID: 29260
		public List<ShortcutKey.Proc> procList = new List<ShortcutKey.Proc>();

		// Token: 0x0400724D RID: 29261
		private static ShortcutKey.ShortcutKeyList list = new ShortcutKey.ShortcutKeyList();

		// Token: 0x0200104C RID: 4172
		[Serializable]
		public class Proc
		{
			// Token: 0x17001EAA RID: 7850
			// (get) Token: 0x06008C52 RID: 35922 RVA: 0x003ADE00 File Offset: 0x003AC200
			// (set) Token: 0x06008C53 RID: 35923 RVA: 0x003ADE08 File Offset: 0x003AC208
			public int refCount { get; set; }

			// Token: 0x0400724E RID: 29262
			public KeyCode keyCode;

			// Token: 0x0400724F RID: 29263
			public bool enabled = true;

			// Token: 0x04007250 RID: 29264
			public UnityEvent call = new UnityEvent();
		}

		// Token: 0x0200104D RID: 4173
		private class ShortcutKeyList
		{
			// Token: 0x06008C55 RID: 35925 RVA: 0x003ADE24 File Offset: 0x003AC224
			public void Add(ShortcutKey sk)
			{
				foreach (ShortcutKey shortcutKey in this.list)
				{
					foreach (ShortcutKey.Proc proc in shortcutKey.procList)
					{
						foreach (ShortcutKey.Proc proc2 in sk.procList)
						{
							if (proc2.keyCode == proc.keyCode)
							{
								proc.refCount++;
								proc.enabled = false;
							}
						}
					}
				}
				this.list.Insert(0, sk);
			}

			// Token: 0x06008C56 RID: 35926 RVA: 0x003ADF38 File Offset: 0x003AC338
			public bool Remove(ShortcutKey sk)
			{
				if (!this.list.Remove(sk))
				{
					return false;
				}
				foreach (ShortcutKey shortcutKey in this.list)
				{
					foreach (ShortcutKey.Proc proc in shortcutKey.procList)
					{
						foreach (ShortcutKey.Proc proc2 in sk.procList)
						{
							if (proc2.keyCode == proc.keyCode && --proc.refCount == 0)
							{
								proc.enabled = true;
							}
						}
					}
				}
				return true;
			}

			// Token: 0x04007252 RID: 29266
			private List<ShortcutKey> list = new List<ShortcutKey>();
		}
	}
}
