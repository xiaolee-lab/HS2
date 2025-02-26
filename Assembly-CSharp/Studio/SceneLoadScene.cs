using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using Illusion.Extensions;
using Manager;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Studio
{
	// Token: 0x0200129C RID: 4764
	public class SceneLoadScene : MonoBehaviour
	{
		// Token: 0x06009D80 RID: 40320 RVA: 0x00404F78 File Offset: 0x00403378
		public void OnClickThumbnail(int _id)
		{
			this.canvasWork.enabled = true;
			this.canvasGroupWork.Enable(true, false);
			this.select = 12 * SceneLoadScene.page + _id;
			this.imageThumbnail.texture = this.buttonThumbnail[_id].texture;
		}

		// Token: 0x06009D81 RID: 40321 RVA: 0x00404FC6 File Offset: 0x004033C6
		private void OnClickClose()
		{
			Singleton<Scene>.Instance.UnLoad();
		}

		// Token: 0x06009D82 RID: 40322 RVA: 0x00404FD3 File Offset: 0x004033D3
		private void OnClickPage(int _page)
		{
			this.SetPage(_page);
		}

		// Token: 0x06009D83 RID: 40323 RVA: 0x00404FDC File Offset: 0x004033DC
		private void OnClickLoad()
		{
			this.canvasGroupWork.Enable(false, false);
			base.StartCoroutine(this.LoadScene(this.listPath[this.select]));
		}

		// Token: 0x06009D84 RID: 40324 RVA: 0x0040500C File Offset: 0x0040340C
		private IEnumerator LoadScene(string _path)
		{
			yield return Singleton<Studio>.Instance.LoadSceneCoroutine(_path);
			yield return null;
			this.canvasWork.enabled = false;
			NotificationScene.spriteMessage = this.spriteLoad;
			NotificationScene.waitTime = 1f;
			Singleton<Scene>.Instance.LoadReserve(new Scene.Data
			{
				levelName = "StudioNotification",
				isAdd = true
			}, false);
			yield break;
		}

		// Token: 0x06009D85 RID: 40325 RVA: 0x00405030 File Offset: 0x00403430
		private IEnumerator NotificationLoadCoroutine()
		{
			yield return null;
			NotificationScene.spriteMessage = this.spriteLoad;
			NotificationScene.waitTime = 1f;
			Singleton<Scene>.Instance.LoadReserve(new Scene.Data
			{
				levelName = "StudioNotification",
				isAdd = true
			}, false);
			yield break;
		}

		// Token: 0x06009D86 RID: 40326 RVA: 0x0040504B File Offset: 0x0040344B
		private void OnClickImport()
		{
			Singleton<Studio>.Instance.ImportScene(this.listPath[this.select]);
			this.canvasWork.enabled = false;
			base.StartCoroutine("NotificationImportCoroutine");
		}

		// Token: 0x06009D87 RID: 40327 RVA: 0x00405084 File Offset: 0x00403484
		private IEnumerator NotificationImportCoroutine()
		{
			yield return null;
			NotificationScene.spriteMessage = this.spriteImport;
			NotificationScene.waitTime = 1f;
			Singleton<Scene>.Instance.LoadReserve(new Scene.Data
			{
				levelName = "StudioNotification",
				isAdd = true
			}, false);
			yield break;
		}

		// Token: 0x06009D88 RID: 40328 RVA: 0x0040509F File Offset: 0x0040349F
		private void OnClickCancel()
		{
			this.canvasWork.enabled = false;
		}

		// Token: 0x06009D89 RID: 40329 RVA: 0x004050B0 File Offset: 0x004034B0
		private void OnClickDelete()
		{
			CheckScene.sprite = this.spriteDelete;
			CheckScene.unityActionYes = new UnityAction(this.OnSelectDeleteYes);
			CheckScene.unityActionNo = new UnityAction(this.OnSelectDeleteNo);
			Singleton<Scene>.Instance.LoadReserve(new Scene.Data
			{
				levelName = "StudioCheck",
				isAdd = true
			}, false);
		}

		// Token: 0x06009D8A RID: 40330 RVA: 0x0040510E File Offset: 0x0040350E
		private void OnSelectDeleteYes()
		{
			Singleton<Scene>.Instance.UnLoad();
			File.Delete(this.listPath[this.select]);
			this.canvasWork.enabled = false;
			this.InitInfo();
			this.SetPage(SceneLoadScene.page);
		}

		// Token: 0x06009D8B RID: 40331 RVA: 0x0040514E File Offset: 0x0040354E
		private void OnSelectDeleteNo()
		{
			Singleton<Scene>.Instance.UnLoad();
		}

		// Token: 0x06009D8C RID: 40332 RVA: 0x0040515C File Offset: 0x0040355C
		private void InitInfo()
		{
			for (int i = 0; i < this.transformRoot.childCount; i++)
			{
				UnityEngine.Object.Destroy(this.transformRoot.GetChild(i).gameObject);
			}
			this.transformRoot.DetachChildren();
			List<KeyValuePair<DateTime, string>> list = (from s in Directory.GetFiles(UserData.Create("studio/scene"), "*.png")
			select new KeyValuePair<DateTime, string>(File.GetLastWriteTime(s), s)).ToList<KeyValuePair<DateTime, string>>();
			CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
			Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("ja-JP");
			list.Sort((KeyValuePair<DateTime, string> a, KeyValuePair<DateTime, string> b) => b.Key.CompareTo(a.Key));
			Thread.CurrentThread.CurrentCulture = currentCulture;
			this.listPath = (from v in list
			select v.Value).ToList<string>();
			this.thumbnailNum = this.listPath.Count;
			this.pageNum = this.thumbnailNum / 12 + ((this.thumbnailNum % 12 == 0) ? 0 : 1);
			this.dicPage.Clear();
			for (int j = 0; j < this.pageNum; j++)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.prefabButton);
				gameObject.transform.SetParent(this.transformRoot, false);
				StudioNode component = gameObject.GetComponent<StudioNode>();
				component.active = true;
				int page = j;
				component.addOnClick = delegate()
				{
					this.OnClickPage(page);
				};
				component.text = string.Format("{0}", j + 1);
				this.dicPage.Add(j, component);
			}
		}

		// Token: 0x06009D8D RID: 40333 RVA: 0x00405338 File Offset: 0x00403738
		private void SetPage(int _page)
		{
			StudioNode studioNode = null;
			if (this.dicPage.TryGetValue(SceneLoadScene.page, out studioNode))
			{
				studioNode.select = false;
			}
			_page = Mathf.Clamp(_page, 0, this.pageNum - 1);
			int num = 12 * _page;
			for (int i = 0; i < 12; i++)
			{
				int num2 = num + i;
				if (!MathfEx.RangeEqualOn<int>(0, num2, this.thumbnailNum - 1))
				{
					this.buttonThumbnail[i].interactable = false;
				}
				else
				{
					this.buttonThumbnail[i].texture = PngAssist.LoadTexture(this.listPath[num2]);
					this.buttonThumbnail[i].interactable = true;
				}
			}
			SceneLoadScene.page = _page;
			if (this.dicPage.TryGetValue(SceneLoadScene.page, out studioNode))
			{
				studioNode.select = true;
			}
			UnityEngine.Resources.UnloadUnusedAssets();
			GC.Collect();
		}

		// Token: 0x06009D8E RID: 40334 RVA: 0x00405414 File Offset: 0x00403814
		private void Awake()
		{
			this.InitInfo();
			this.SetPage(SceneLoadScene.page);
			this.buttonClose.onClick.AddListener(new UnityAction(this.OnClickClose));
			this.buttonLoad.onClick.AddListener(new UnityAction(this.OnClickLoad));
			this.buttonImport.onClick.AddListener(new UnityAction(this.OnClickImport));
			this.buttonCancel.onClick.AddListener(new UnityAction(this.OnClickCancel));
			this.buttonDelete.onClick.AddListener(new UnityAction(this.OnClickDelete));
			this.canvasWork.enabled = false;
		}

		// Token: 0x04007D46 RID: 32070
		public static int page;

		// Token: 0x04007D47 RID: 32071
		[SerializeField]
		private ThumbnailNode[] buttonThumbnail;

		// Token: 0x04007D48 RID: 32072
		[SerializeField]
		private Button buttonClose;

		// Token: 0x04007D49 RID: 32073
		[SerializeField]
		private Canvas canvasWork;

		// Token: 0x04007D4A RID: 32074
		[SerializeField]
		private CanvasGroup canvasGroupWork;

		// Token: 0x04007D4B RID: 32075
		[SerializeField]
		private RawImage imageThumbnail;

		// Token: 0x04007D4C RID: 32076
		[SerializeField]
		private Button buttonLoad;

		// Token: 0x04007D4D RID: 32077
		[SerializeField]
		private Sprite spriteLoad;

		// Token: 0x04007D4E RID: 32078
		[SerializeField]
		private Button buttonImport;

		// Token: 0x04007D4F RID: 32079
		[SerializeField]
		private Sprite spriteImport;

		// Token: 0x04007D50 RID: 32080
		[SerializeField]
		private Button buttonCancel;

		// Token: 0x04007D51 RID: 32081
		[SerializeField]
		private Button buttonDelete;

		// Token: 0x04007D52 RID: 32082
		[SerializeField]
		private Sprite spriteDelete;

		// Token: 0x04007D53 RID: 32083
		[SerializeField]
		private Transform transformRoot;

		// Token: 0x04007D54 RID: 32084
		[SerializeField]
		private GameObject prefabButton;

		// Token: 0x04007D55 RID: 32085
		private List<string> listPath;

		// Token: 0x04007D56 RID: 32086
		private int thumbnailNum = -1;

		// Token: 0x04007D57 RID: 32087
		private Dictionary<int, StudioNode> dicPage = new Dictionary<int, StudioNode>();

		// Token: 0x04007D58 RID: 32088
		private int pageNum = -1;

		// Token: 0x04007D59 RID: 32089
		private int select = -1;
	}
}
