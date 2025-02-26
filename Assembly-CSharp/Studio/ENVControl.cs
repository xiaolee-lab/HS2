using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Studio
{
	// Token: 0x02001341 RID: 4929
	public class ENVControl : MonoBehaviour
	{
		// Token: 0x0600A4FA RID: 42234 RVA: 0x00434540 File Offset: 0x00432940
		private void OnClickRepeat()
		{
			Singleton<Studio>.Instance.envCtrl.repeat = ((Singleton<Studio>.Instance.envCtrl.repeat != BGMCtrl.Repeat.None) ? BGMCtrl.Repeat.None : BGMCtrl.Repeat.All);
			this.buttonRepeat.image.sprite = this.spriteRepeat[(Singleton<Studio>.Instance.envCtrl.repeat != BGMCtrl.Repeat.None) ? 1 : 0];
		}

		// Token: 0x0600A4FB RID: 42235 RVA: 0x004345A9 File Offset: 0x004329A9
		private void OnClickStop()
		{
			Singleton<Studio>.Instance.envCtrl.Stop();
		}

		// Token: 0x0600A4FC RID: 42236 RVA: 0x004345BA File Offset: 0x004329BA
		private void OnClickPlay()
		{
			Singleton<Studio>.Instance.envCtrl.Play();
		}

		// Token: 0x0600A4FD RID: 42237 RVA: 0x004345CB File Offset: 0x004329CB
		private void OnClickPause()
		{
		}

		// Token: 0x0600A4FE RID: 42238 RVA: 0x004345D0 File Offset: 0x004329D0
		private void OnClickExpansion()
		{
			this.objBottom.SetActive(!this.objBottom.activeSelf);
			this.buttonExpansion.image.sprite = this.spriteExpansion[(!this.objBottom.activeSelf) ? 0 : 1];
		}

		// Token: 0x0600A4FF RID: 42239 RVA: 0x00434624 File Offset: 0x00432A24
		private void OnClickSelect(int _idx)
		{
			StudioNode studioNode = null;
			if (this.dicNode.TryGetValue(Singleton<Studio>.Instance.envCtrl.no, out studioNode))
			{
				studioNode.select = false;
			}
			Singleton<Studio>.Instance.envCtrl.no = _idx;
			if (this.dicNode.TryGetValue(Singleton<Studio>.Instance.envCtrl.no, out studioNode))
			{
				studioNode.select = true;
			}
		}

		// Token: 0x0600A500 RID: 42240 RVA: 0x00434694 File Offset: 0x00432A94
		private void InitList()
		{
			for (int i = 0; i < this.transformRoot.childCount; i++)
			{
				UnityEngine.Object.Destroy(this.transformRoot.GetChild(i).gameObject);
			}
			this.transformRoot.DetachChildren();
			foreach (KeyValuePair<int, Info.LoadCommonInfo> keyValuePair in Singleton<Info>.Instance.dicENVLoadInfo)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.objectNode);
				gameObject.transform.SetParent(this.transformRoot, false);
				StudioNode component = gameObject.GetComponent<StudioNode>();
				component.active = true;
				int idx = keyValuePair.Key;
				component.addOnClick = delegate()
				{
					this.OnClickSelect(idx);
				};
				component.text = keyValuePair.Value.name;
				this.dicNode.Add(idx, component);
			}
			StudioNode studioNode = null;
			if (this.dicNode.TryGetValue(Singleton<Studio>.Instance.envCtrl.no, out studioNode))
			{
				studioNode.select = true;
			}
		}

		// Token: 0x0600A501 RID: 42241 RVA: 0x004347DC File Offset: 0x00432BDC
		private void Awake()
		{
			this.buttonRepeat.onClick.AddListener(new UnityAction(this.OnClickRepeat));
			this.buttonStop.onClick.AddListener(new UnityAction(this.OnClickStop));
			this.buttonPlay.onClick.AddListener(new UnityAction(this.OnClickPlay));
			this.buttonPause.onClick.AddListener(new UnityAction(this.OnClickPause));
			this.buttonExpansion.onClick.AddListener(new UnityAction(this.OnClickExpansion));
			this.InitList();
		}

		// Token: 0x0600A502 RID: 42242 RVA: 0x0043487C File Offset: 0x00432C7C
		private void OnEnable()
		{
			this.buttonRepeat.image.sprite = this.spriteRepeat[(Singleton<Studio>.Instance.envCtrl.repeat != BGMCtrl.Repeat.None) ? 1 : 0];
			foreach (KeyValuePair<int, StudioNode> keyValuePair in this.dicNode)
			{
				keyValuePair.Value.select = false;
			}
			StudioNode studioNode = null;
			if (this.dicNode.TryGetValue(Singleton<Studio>.Instance.envCtrl.no, out studioNode))
			{
				studioNode.select = true;
			}
		}

		// Token: 0x0600A503 RID: 42243 RVA: 0x0043493C File Offset: 0x00432D3C
		private void Update()
		{
			this.imagePlayNow.enabled = Singleton<Studio>.Instance.envCtrl.play;
		}

		// Token: 0x040081EA RID: 33258
		[SerializeField]
		private GameObject objectNode;

		// Token: 0x040081EB RID: 33259
		[SerializeField]
		private Transform transformRoot;

		// Token: 0x040081EC RID: 33260
		[SerializeField]
		private Button buttonRepeat;

		// Token: 0x040081ED RID: 33261
		[SerializeField]
		private Sprite[] spriteRepeat;

		// Token: 0x040081EE RID: 33262
		[SerializeField]
		private Button buttonStop;

		// Token: 0x040081EF RID: 33263
		[SerializeField]
		private Button buttonPlay;

		// Token: 0x040081F0 RID: 33264
		[SerializeField]
		private Image imagePlayNow;

		// Token: 0x040081F1 RID: 33265
		[SerializeField]
		private Button buttonPause;

		// Token: 0x040081F2 RID: 33266
		[SerializeField]
		private Button buttonExpansion;

		// Token: 0x040081F3 RID: 33267
		[SerializeField]
		private Sprite[] spriteExpansion;

		// Token: 0x040081F4 RID: 33268
		[SerializeField]
		private GameObject objBottom;

		// Token: 0x040081F5 RID: 33269
		private Dictionary<int, StudioNode> dicNode = new Dictionary<int, StudioNode>();
	}
}
