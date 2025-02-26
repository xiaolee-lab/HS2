using System;
using System.Collections;
using Manager;
using UnityEngine;
using UnityEngine.UI;

namespace Studio
{
	// Token: 0x02001292 RID: 4754
	public class NotificationScene : MonoBehaviour
	{
		// Token: 0x06009D47 RID: 40263 RVA: 0x004041A4 File Offset: 0x004025A4
		private IEnumerator NotificationCoroutine()
		{
			yield return new global::WaitForSecondsRealtime(NotificationScene.waitTime);
			Singleton<Scene>.Instance.UnLoad();
			yield break;
		}

		// Token: 0x06009D48 RID: 40264 RVA: 0x004041B8 File Offset: 0x004025B8
		private void Awake()
		{
			this.imageMessage.sprite = NotificationScene.spriteMessage;
			this.transImage.sizeDelta = new Vector2(NotificationScene.width, NotificationScene.height);
			base.StartCoroutine(this.NotificationCoroutine());
			NotificationScene.width = 416f;
			NotificationScene.height = 48f;
		}

		// Token: 0x04007D25 RID: 32037
		[SerializeField]
		private Image imageMessage;

		// Token: 0x04007D26 RID: 32038
		[SerializeField]
		private RectTransform transImage;

		// Token: 0x04007D27 RID: 32039
		public static Sprite spriteMessage;

		// Token: 0x04007D28 RID: 32040
		public static float waitTime = 1f;

		// Token: 0x04007D29 RID: 32041
		public static float width = 416f;

		// Token: 0x04007D2A RID: 32042
		public static float height = 48f;
	}
}
