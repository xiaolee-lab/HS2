using System;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x020010C8 RID: 4296
public class CSS_UI_NoControlCamera : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IEventSystemHandler
{
	// Token: 0x06008F23 RID: 36643 RVA: 0x003B7D8C File Offset: 0x003B618C
	private void Start()
	{
		if (null == this.camCtrl)
		{
			GameObject gameObject = GameObject.FindGameObjectWithTag("MainCamera");
			if (gameObject)
			{
				this.camCtrl = gameObject.GetComponent<BaseCameraControl>();
			}
		}
	}

	// Token: 0x06008F24 RID: 36644 RVA: 0x003B7DCC File Offset: 0x003B61CC
	public void OnPointerEnter(PointerEventData eventData)
	{
		this.over = true;
	}

	// Token: 0x06008F25 RID: 36645 RVA: 0x003B7DD5 File Offset: 0x003B61D5
	public void OnPointerExit(PointerEventData eventData)
	{
		this.over = false;
	}

	// Token: 0x06008F26 RID: 36646 RVA: 0x003B7DE0 File Offset: 0x003B61E0
	public void Update()
	{
		if (Input.GetMouseButtonUp(0))
		{
			if (this.camCtrl)
			{
				this.camCtrl.NoCtrlCondition = (() => false);
			}
		}
		else if (Input.GetMouseButtonDown(0) && this.over && this.camCtrl)
		{
			this.camCtrl.NoCtrlCondition = (() => true);
		}
	}

	// Token: 0x0400739D RID: 29597
	private BaseCameraControl camCtrl;

	// Token: 0x0400739E RID: 29598
	private bool over;
}
