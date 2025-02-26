using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Studio
{
	// Token: 0x02001342 RID: 4930
	public class OutsideSoundControl : MonoBehaviour
	{
		// Token: 0x0600A505 RID: 42245 RVA: 0x00434998 File Offset: 0x00432D98
		private void OnClickRepeat()
		{
			Singleton<Studio>.Instance.outsideSoundCtrl.repeat = ((Singleton<Studio>.Instance.outsideSoundCtrl.repeat != BGMCtrl.Repeat.None) ? BGMCtrl.Repeat.None : BGMCtrl.Repeat.All);
			this.buttonRepeat.image.sprite = this.spriteRepeat[(Singleton<Studio>.Instance.outsideSoundCtrl.repeat != BGMCtrl.Repeat.None) ? 1 : 0];
		}

		// Token: 0x0600A506 RID: 42246 RVA: 0x00434A01 File Offset: 0x00432E01
		private void OnClickStop()
		{
			Singleton<Studio>.Instance.outsideSoundCtrl.Stop();
		}

		// Token: 0x0600A507 RID: 42247 RVA: 0x00434A12 File Offset: 0x00432E12
		private void OnClickPlay()
		{
			Singleton<Studio>.Instance.outsideSoundCtrl.Play();
		}

		// Token: 0x0600A508 RID: 42248 RVA: 0x00434A23 File Offset: 0x00432E23
		private void OnClickPause()
		{
		}

		// Token: 0x0600A509 RID: 42249 RVA: 0x00434A28 File Offset: 0x00432E28
		private void OnClickExpansion()
		{
			this.objBottom.SetActive(!this.objBottom.activeSelf);
			this.buttonExpansion.image.sprite = this.spriteExpansion[(!this.objBottom.activeSelf) ? 0 : 1];
		}

		// Token: 0x0600A50A RID: 42250 RVA: 0x00434A7C File Offset: 0x00432E7C
		private void OnClickSelect(int _idx)
		{
			StudioNode studioNode = null;
			if (this.dicNode.TryGetValue(this.select, out studioNode))
			{
				studioNode.select = false;
			}
			this.select = _idx;
			if (this.select != -1)
			{
				Singleton<Studio>.Instance.outsideSoundCtrl.fileName = this.listPath[_idx];
			}
			if (this.dicNode.TryGetValue(this.select, out studioNode))
			{
				studioNode.select = true;
			}
		}

		// Token: 0x0600A50B RID: 42251 RVA: 0x00434AF8 File Offset: 0x00432EF8
		private void InitList()
		{
			for (int i = 0; i < this.transformRoot.childCount; i++)
			{
				UnityEngine.Object.Destroy(this.transformRoot.GetChild(i).gameObject);
			}
			this.transformRoot.DetachChildren();
			IEnumerable<string> files = Directory.GetFiles(UserData.Create("audio"), "*.wav");
			if (OutsideSoundControl.<>f__mg$cache0 == null)
			{
				OutsideSoundControl.<>f__mg$cache0 = new Func<string, string>(Path.GetFileName);
			}
			this.listPath = files.Select(OutsideSoundControl.<>f__mg$cache0).ToList<string>();
			int count = this.listPath.Count;
			for (int j = 0; j < count; j++)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.objectNode);
				gameObject.transform.SetParent(this.transformRoot, false);
				StudioNode component = gameObject.GetComponent<StudioNode>();
				component.active = true;
				int idx = j;
				component.addOnClick = delegate()
				{
					this.OnClickSelect(idx);
				};
				component.text = Path.GetFileNameWithoutExtension(this.listPath[j]);
				this.dicNode.Add(idx, component);
			}
			this.select = this.listPath.FindIndex((string v) => v == Singleton<Studio>.Instance.outsideSoundCtrl.fileName);
			StudioNode studioNode = null;
			if (this.dicNode.TryGetValue(this.select, out studioNode))
			{
				studioNode.select = true;
			}
		}

		// Token: 0x0600A50C RID: 42252 RVA: 0x00434C78 File Offset: 0x00433078
		private void Awake()
		{
			this.buttonRepeat.onClick.AddListener(new UnityAction(this.OnClickRepeat));
			this.buttonStop.onClick.AddListener(new UnityAction(this.OnClickStop));
			this.buttonPlay.onClick.AddListener(new UnityAction(this.OnClickPlay));
			this.buttonPause.onClick.AddListener(new UnityAction(this.OnClickPause));
			this.buttonExpansion.onClick.AddListener(new UnityAction(this.OnClickExpansion));
			this.InitList();
		}

		// Token: 0x0600A50D RID: 42253 RVA: 0x00434D17 File Offset: 0x00433117
		private void OnEnable()
		{
			this.buttonRepeat.image.sprite = this.spriteRepeat[(Singleton<Studio>.Instance.outsideSoundCtrl.repeat != BGMCtrl.Repeat.None) ? 1 : 0];
		}

		// Token: 0x0600A50E RID: 42254 RVA: 0x00434D4B File Offset: 0x0043314B
		private void Update()
		{
			this.imagePlayNow.enabled = Singleton<Studio>.Instance.outsideSoundCtrl.play;
		}

		// Token: 0x040081F6 RID: 33270
		[SerializeField]
		private GameObject objectNode;

		// Token: 0x040081F7 RID: 33271
		[SerializeField]
		private Transform transformRoot;

		// Token: 0x040081F8 RID: 33272
		[SerializeField]
		private Button buttonRepeat;

		// Token: 0x040081F9 RID: 33273
		[SerializeField]
		private Sprite[] spriteRepeat;

		// Token: 0x040081FA RID: 33274
		[SerializeField]
		private Button buttonStop;

		// Token: 0x040081FB RID: 33275
		[SerializeField]
		private Button buttonPlay;

		// Token: 0x040081FC RID: 33276
		[SerializeField]
		private Image imagePlayNow;

		// Token: 0x040081FD RID: 33277
		[SerializeField]
		private Button buttonPause;

		// Token: 0x040081FE RID: 33278
		[SerializeField]
		private Button buttonExpansion;

		// Token: 0x040081FF RID: 33279
		[SerializeField]
		private Sprite[] spriteExpansion;

		// Token: 0x04008200 RID: 33280
		[SerializeField]
		private GameObject objBottom;

		// Token: 0x04008201 RID: 33281
		private List<string> listPath = new List<string>();

		// Token: 0x04008202 RID: 33282
		private Dictionary<int, StudioNode> dicNode = new Dictionary<int, StudioNode>();

		// Token: 0x04008203 RID: 33283
		private int select = -1;

		// Token: 0x04008204 RID: 33284
		[CompilerGenerated]
		private static Func<string, string> <>f__mg$cache0;
	}
}
