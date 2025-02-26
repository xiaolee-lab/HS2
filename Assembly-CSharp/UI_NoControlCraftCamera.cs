using System;
using Manager;
using UnityEngine;

// Token: 0x02000EF9 RID: 3833
public class UI_NoControlCraftCamera : MonoBehaviour
{
	// Token: 0x06007D27 RID: 32039 RVA: 0x0035899F File Offset: 0x00356D9F
	private void Start()
	{
		this.SearchCanvas();
		if (null == this.camCtrl && Camera.main)
		{
			this.camCtrl = Camera.main.GetComponent<CraftCamera>();
		}
	}

	// Token: 0x06007D28 RID: 32040 RVA: 0x003589D8 File Offset: 0x00356DD8
	private void SearchCanvas()
	{
		GameObject gameObject = base.gameObject;
		for (;;)
		{
			Canvas component = gameObject.GetComponent<Canvas>();
			if (component)
			{
				break;
			}
			if (null == gameObject.transform.parent)
			{
				return;
			}
			gameObject = gameObject.transform.parent.gameObject;
		}
		this.rtCanvas = (gameObject.transform as RectTransform);
	}

	// Token: 0x06007D29 RID: 32041 RVA: 0x00358A48 File Offset: 0x00356E48
	public void Update()
	{
		if (!Singleton<Manager.Input>.Instance.IsDown(ActionID.MouseLeft) && !Singleton<Manager.Input>.Instance.IsDown(ActionID.MouseRight))
		{
			if (this.camCtrl)
			{
				this.camCtrl.NoCtrlCondition = (() => false);
			}
		}
		else if (Singleton<Manager.Input>.Instance.IsDown(ActionID.MouseLeft) || Singleton<Manager.Input>.Instance.IsDown(ActionID.MouseRight))
		{
			if (null == this.rtCanvas)
			{
				return;
			}
			RectTransform rectTransform = base.transform as RectTransform;
			float x = UnityEngine.Input.mousePosition.x;
			float y = UnityEngine.Input.mousePosition.y;
			if (rectTransform.position.x <= x && x <= rectTransform.position.x + rectTransform.sizeDelta.x * this.rtCanvas.localScale.x && rectTransform.position.y >= y && y >= rectTransform.position.y - rectTransform.sizeDelta.y * this.rtCanvas.localScale.y && this.camCtrl)
			{
				this.camCtrl.NoCtrlCondition = (() => true);
			}
		}
		if (Mathf.Approximately(Singleton<Manager.Input>.Instance.ScrollValue(), 0f))
		{
			if (this.camCtrl)
			{
				this.camCtrl.ZoomCondition = (() => false);
			}
		}
		else
		{
			if (null == this.rtCanvas)
			{
				return;
			}
			RectTransform rectTransform2 = base.transform as RectTransform;
			float x2 = UnityEngine.Input.mousePosition.x;
			float y2 = UnityEngine.Input.mousePosition.y;
			if (rectTransform2.position.x <= x2 && x2 <= rectTransform2.position.x + rectTransform2.sizeDelta.x * this.rtCanvas.localScale.x && rectTransform2.position.y >= y2 && y2 >= rectTransform2.position.y - rectTransform2.sizeDelta.y * this.rtCanvas.localScale.y && this.camCtrl)
			{
				this.camCtrl.ZoomCondition = (() => true);
			}
		}
	}

	// Token: 0x0400653B RID: 25915
	public RectTransform rtCanvas;

	// Token: 0x0400653C RID: 25916
	private CraftCamera camCtrl;
}
