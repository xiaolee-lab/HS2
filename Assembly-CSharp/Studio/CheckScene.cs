using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Studio
{
	// Token: 0x02001290 RID: 4752
	public class CheckScene : MonoBehaviour
	{
		// Token: 0x06009D3A RID: 40250 RVA: 0x00403FE3 File Offset: 0x004023E3
		private void Awake()
		{
			this.timeScale = Time.timeScale;
			Time.timeScale = 0f;
		}

		// Token: 0x06009D3B RID: 40251 RVA: 0x00403FFA File Offset: 0x004023FA
		private void Start()
		{
			this.back.sprite = CheckScene.sprite;
			this.buttonYes.addOnClick = CheckScene.unityActionYes;
			this.buttonNo.addOnClick = CheckScene.unityActionNo;
		}

		// Token: 0x06009D3C RID: 40252 RVA: 0x0040402C File Offset: 0x0040242C
		private void OnDestroy()
		{
			Time.timeScale = this.timeScale;
		}

		// Token: 0x04007D17 RID: 32023
		[SerializeField]
		private Image back;

		// Token: 0x04007D18 RID: 32024
		[SerializeField]
		private VoiceNode buttonYes;

		// Token: 0x04007D19 RID: 32025
		[SerializeField]
		private VoiceNode buttonNo;

		// Token: 0x04007D1A RID: 32026
		private float timeScale = 1f;

		// Token: 0x04007D1B RID: 32027
		public static Sprite sprite;

		// Token: 0x04007D1C RID: 32028
		public static UnityAction unityActionYes;

		// Token: 0x04007D1D RID: 32029
		public static UnityAction unityActionNo;
	}
}
