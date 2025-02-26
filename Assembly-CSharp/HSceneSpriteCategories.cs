using System;
using Manager;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEx;

// Token: 0x02000B09 RID: 2825
public class HSceneSpriteCategories : MonoBehaviour
{
	// Token: 0x060052F0 RID: 21232 RVA: 0x00243D2C File Offset: 0x0024212C
	private void Start()
	{
		this.MainCategoryActive = new bool[this.MainCategory.Length];
		this.SubCategoryActive = new bool[this.SubCategory.Length];
		for (int i = 0; i < this.MainCategoryActive.Length; i++)
		{
			this.MainCategoryActive[i] = false;
		}
		for (int j = 0; j < this.SubCategoryActive.Length; j++)
		{
			this.SubCategoryActive[j] = false;
		}
		UITrigger.TriggerEvent triggerEvent = new UITrigger.TriggerEvent();
		UITrigger.TriggerEvent triggerEvent2 = new UITrigger.TriggerEvent();
		this.bMainCategory = new bool[this.MainCategory.Length];
		this.btMainCategory = new Button[this.MainCategory.Length];
		for (int k = 0; k < this.MainCategory.Length; k++)
		{
			int no = k;
			triggerEvent = new UITrigger.TriggerEvent();
			PointerEnterTrigger pointerEnterTrigger = this.MainCategory[no].gameObject.AddComponent<PointerEnterTrigger>();
			pointerEnterTrigger.Triggers.Add(triggerEvent);
			triggerEvent.AddListener(delegate(BaseEventData x)
			{
				if (Cursor.visible)
				{
					this.bMainCategory[no] = true;
				}
			});
			triggerEvent2 = new UITrigger.TriggerEvent();
			PointerExitTrigger pointerExitTrigger = this.MainCategory[no].gameObject.AddComponent<PointerExitTrigger>();
			pointerExitTrigger.Triggers.Add(triggerEvent2);
			triggerEvent2.AddListener(delegate(BaseEventData x)
			{
				this.bMainCategory[no] = false;
			});
			this.bMainCategory[no] = false;
			this.btMainCategory[no] = this.MainCategory[no].GetComponentInChildren<Button>();
		}
		this.bSubCategory = new bool[this.SubCategory.Length];
		this.btSubCategory = new Button[this.SubCategory.Length];
		for (int l = 0; l < this.SubCategory.Length; l++)
		{
			int no = l;
			triggerEvent = new UITrigger.TriggerEvent();
			PointerEnterTrigger pointerEnterTrigger = this.SubCategory[no].gameObject.AddComponent<PointerEnterTrigger>();
			pointerEnterTrigger.Triggers.Add(triggerEvent);
			triggerEvent.AddListener(delegate(BaseEventData x)
			{
				if (Cursor.visible)
				{
					this.bSubCategory[no] = true;
				}
			});
			triggerEvent2 = new UITrigger.TriggerEvent();
			PointerExitTrigger pointerExitTrigger = this.SubCategory[no].gameObject.AddComponent<PointerExitTrigger>();
			pointerExitTrigger.Triggers.Add(triggerEvent2);
			triggerEvent2.AddListener(delegate(BaseEventData x)
			{
				this.bSubCategory[no] = false;
			});
			this.bSubCategory[no] = false;
			this.btSubCategory[no] = this.SubCategory[no].GetComponentInChildren<Button>();
		}
	}

	// Token: 0x060052F1 RID: 21233 RVA: 0x00243FD8 File Offset: 0x002423D8
	private void Update()
	{
		float deltaTime = Time.deltaTime;
		Vector3 v = Vector3.zero;
		float num = 0f;
		for (int i = 0; i < this.btMainCategory.Length; i++)
		{
			int num2 = i;
			num = 0f;
			RectTransform component = this.btMainCategory[num2].GetComponent<RectTransform>();
			v = component.anchoredPosition;
			if (this.bMainCategory[num2] || this.MainCategoryActive[num2])
			{
				v.x = Mathf.SmoothDamp(v.x, this.MainPosX[0], ref num, this.smoothTime, float.PositiveInfinity, deltaTime);
			}
			else
			{
				v.x = Mathf.SmoothDamp(v.x, this.MainPosX[1], ref num, this.smoothTime, float.PositiveInfinity, deltaTime);
			}
			component.anchoredPosition = v;
		}
		for (int j = 0; j < this.btSubCategory.Length; j++)
		{
			int num3 = j;
			num = 0f;
			RectTransform component = this.btSubCategory[num3].GetComponent<RectTransform>();
			v = component.anchoredPosition;
			if (this.bSubCategory[num3] || this.SubCategoryActive[num3])
			{
				v.x = Mathf.SmoothDamp(v.x, this.SubPosX[0], ref num, this.smoothTime, float.PositiveInfinity, deltaTime);
			}
			else
			{
				v.x = Mathf.SmoothDamp(v.x, this.SubPosX[1], ref num, this.smoothTime, float.PositiveInfinity, deltaTime);
			}
			component.anchoredPosition = v;
		}
	}

	// Token: 0x060052F2 RID: 21234 RVA: 0x0024417C File Offset: 0x0024257C
	public void Changebuttonactive(bool val)
	{
		if (this.canUse == val)
		{
			return;
		}
		this.canUse = val;
		for (int i = 0; i < this.btMainCategory.Length; i++)
		{
			if (!val)
			{
				this.bMainCategory[i] = val;
			}
			if (i <= 2 || Singleton<HSceneManager>.Instance.EventKind != HSceneManager.HEvent.GyakuYobai || Singleton<HSceneFlagCtrl>.Instance.initiative != 2)
			{
				this.btMainCategory[i].interactable = val;
			}
		}
		for (int j = 0; j < this.btSubCategory.Length; j++)
		{
			if (!val)
			{
				this.bSubCategory[j] = val;
			}
			this.btSubCategory[j].interactable = val;
		}
	}

	// Token: 0x060052F3 RID: 21235 RVA: 0x00244238 File Offset: 0x00242638
	public void AllForceClose(int mode = 0)
	{
		Vector3 zero = Vector3.zero;
		for (int i = 0; i < this.btMainCategory.Length; i++)
		{
			int num = i;
			RectTransform component = this.btMainCategory[num].GetComponent<RectTransform>();
			zero.x = this.MainPosX[1];
			component.anchoredPosition = zero;
		}
		for (int j = 0; j < this.btSubCategory.Length; j++)
		{
			int num2 = j;
			if (mode != 1 || num2 != 1)
			{
				RectTransform component = this.btSubCategory[num2].GetComponent<RectTransform>();
				zero.x = this.SubPosX[1];
				component.anchoredPosition = zero;
			}
		}
	}

	// Token: 0x04004D68 RID: 19816
	[SerializeField]
	private GameObject[] MainCategory;

	// Token: 0x04004D69 RID: 19817
	private Button[] btMainCategory;

	// Token: 0x04004D6A RID: 19818
	private bool[] bMainCategory;

	// Token: 0x04004D6B RID: 19819
	public bool[] MainCategoryActive;

	// Token: 0x04004D6C RID: 19820
	[SerializeField]
	private GameObject[] SubCategory;

	// Token: 0x04004D6D RID: 19821
	private Button[] btSubCategory;

	// Token: 0x04004D6E RID: 19822
	private bool[] bSubCategory;

	// Token: 0x04004D6F RID: 19823
	public bool[] SubCategoryActive;

	// Token: 0x04004D70 RID: 19824
	[SerializeField]
	[Space]
	private float[] MainPosX = new float[2];

	// Token: 0x04004D71 RID: 19825
	[SerializeField]
	private float[] SubPosX = new float[2];

	// Token: 0x04004D72 RID: 19826
	[SerializeField]
	[Space]
	private float smoothTime;

	// Token: 0x04004D73 RID: 19827
	private bool canUse = true;
}
