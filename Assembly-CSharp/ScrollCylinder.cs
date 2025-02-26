using System;
using System.Collections.Generic;
using Manager;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEx;

// Token: 0x02000965 RID: 2405
public class ScrollCylinder : MonoBehaviour
{
	// Token: 0x060042B1 RID: 17073 RVA: 0x001A31BC File Offset: 0x001A15BC
	public IObservable<int> OnValueChangeAsObservable()
	{
		Subject<int> result;
		if ((result = this._onValueChange) == null)
		{
			result = (this._onValueChange = new Subject<int>());
		}
		return result;
	}

	// Token: 0x060042B2 RID: 17074 RVA: 0x001A31E4 File Offset: 0x001A15E4
	private void Awake()
	{
		this.OldRect = new GameObject("Rect2");
		this.OldRect.AddComponent<RectTransform>();
		this.OldRect.transform.SetParent(base.transform, false);
		this.OldRect.transform.localPosition = Vector3.zero;
		this.OldRect.SetActive(false);
		GC.Collect();
	}

	// Token: 0x060042B3 RID: 17075 RVA: 0x001A324C File Offset: 0x001A164C
	private void Start()
	{
		this.input = Singleton<Manager.Input>.Instance;
		PointerEnterTrigger pointerEnterTrigger = this.Atari.gameObject.AddComponent<PointerEnterTrigger>();
		UITrigger.TriggerEvent triggerEvent = new UITrigger.TriggerEvent();
		pointerEnterTrigger.Triggers.Add(triggerEvent);
		triggerEvent.AddListener(delegate(BaseEventData x)
		{
			this.onEnter = true;
		});
		PointerExitTrigger pointerExitTrigger = this.Atari.gameObject.AddComponent<PointerExitTrigger>();
		triggerEvent = new UITrigger.TriggerEvent();
		pointerExitTrigger.Triggers.Add(triggerEvent);
		triggerEvent.AddListener(delegate(BaseEventData x)
		{
			this.onEnter = false;
		});
		if (this.InitList)
		{
			this.ListNodeSet(null);
		}
	}

	// Token: 0x060042B4 RID: 17076 RVA: 0x001A32E0 File Offset: 0x001A16E0
	public void BlankSet(ScrollCylinderNode Node, bool AddLast = false)
	{
		if (!AddLast)
		{
			this.lstBlankNodes.Clear();
		}
		if (this.blankObject == null)
		{
			return;
		}
		Vector2 sizeDelta = Node.GetComponent<RectTransform>().sizeDelta;
		RectTransform component = base.transform.GetComponent<RectTransform>();
		int[] array = new int[]
		{
			(int)(component.rect.height / sizeDelta.y),
			(int)(component.rect.width / sizeDelta.x)
		};
		int num = Mathf.Max(array[0], array[1]);
		if (this.lstBlankNodes.Count == ((num % 2 == 0) ? num : (num - 1)))
		{
			return;
		}
		array[0] /= 2;
		array[1] /= 2;
		for (int i = 0; i < array.Length; i++)
		{
			for (int j = 0; j < array[i]; j++)
			{
				ScrollCylinderNode scrollCylinderNode = UnityEngine.Object.Instantiate<ScrollCylinderNode>(this.blankObject);
				scrollCylinderNode.transform.SetParent(this.Contents.transform, false);
				scrollCylinderNode.gameObject.SetActive(true);
				this.lstBlankNodes.Add(scrollCylinderNode);
			}
		}
		if (!AddLast)
		{
			this.setLastBlank = false;
		}
		else
		{
			this.setLastBlank = true;
		}
	}

	// Token: 0x060042B5 RID: 17077 RVA: 0x001A3438 File Offset: 0x001A1838
	public void ClearBlank()
	{
		foreach (ScrollCylinderNode scrollCylinderNode in this.lstBlankNodes)
		{
			UnityEngine.Object.Destroy(scrollCylinderNode.gameObject);
		}
		this.lstBlankNodes.Clear();
	}

	// Token: 0x060042B6 RID: 17078 RVA: 0x001A34A4 File Offset: 0x001A18A4
	public void MoveBlankLast()
	{
		if (!this.setLastBlank)
		{
			return;
		}
		int num = this.lstBlankNodes.Count - 1;
		int num2 = this.Contents.transform.childCount - 1;
		for (int i = 0; i < this.lstBlankNodes.Count / 2; i++)
		{
			num -= i;
			ScrollCylinderNode scrollCylinderNode = this.lstBlankNodes[num];
			scrollCylinderNode.transform.SetSiblingIndex(num2 - i);
		}
	}

	// Token: 0x060042B7 RID: 17079 RVA: 0x001A351C File Offset: 0x001A191C
	public void ListNodeSet(List<ScrollCylinderNode> hSceneScrollNodes = null)
	{
		this.lstNodes.Clear();
		if (hSceneScrollNodes != null)
		{
			this.lstNodes.AddRange(hSceneScrollNodes);
		}
		else
		{
			this.lstNodes.AddRange(this.Contents.GetComponentsInChildren<ScrollCylinderNode>());
		}
		if (this.lstNodes.Count == 0)
		{
			return;
		}
		if (this.cursor != null)
		{
			this.cursor.SetActive(this.lstNodes.Count != 0);
		}
		this.BlankSet(this.lstNodes[0], true);
		this.MoveBlankLast();
		for (int i = 0; i < this.lstNodes.Count; i++)
		{
			int index = i;
			this.lstNodes[index].transform.localScale = new Vector3(1f, 1f, 1f);
		}
		for (int j = 0; j < this.lstBlankNodes.Count; j++)
		{
			int index2 = j;
			this.lstBlankNodes[index2].transform.localScale = new Vector3(1f, 1f, 1f);
		}
		RectTransform ContentsRt = this.Contents.GetComponent<RectTransform>();
		Vector3 ContentsPosition = ContentsRt.anchoredPosition;
		if (!this.OldRect.activeSelf)
		{
			this.OldRect.SetActive(true);
		}
		RectTransform rt2 = this.OldRect.GetComponent<RectTransform>();
		this.OldRectSet(ContentsRt, ref rt2);
		Vector3 position2 = ContentsPosition;
		ContentsPosition = this.LimitPos(ContentsRt, ContentsPosition);
		ContentsRt.anchoredPosition = ContentsPosition;
		UITrigger.TriggerEvent triggerEvent = new UITrigger.TriggerEvent();
		for (int k = 0; k < this.lstNodes.Count; k++)
		{
			int index3 = k;
			PointerEnterTrigger pointerEnterTrigger = this.lstNodes[index3].gameObject.GetComponent<PointerEnterTrigger>();
			this.lstNodes[index3].gameObject.SetActive(true);
			if (!(pointerEnterTrigger != null))
			{
				pointerEnterTrigger = this.lstNodes[index3].gameObject.AddComponent<PointerEnterTrigger>();
				triggerEvent = new UITrigger.TriggerEvent();
				pointerEnterTrigger.Triggers.Add(triggerEvent);
				triggerEvent.AddListener(delegate(BaseEventData x)
				{
					this.onEnter = true;
				});
			}
		}
		if (ContentsRt.rect.width == 0f || ContentsRt.rect.height == 0f)
		{
			Observable.NextFrame(FrameCountType.Update).Subscribe(delegate(Unit _)
			{
				this.InitTargrt(ContentsRt, ContentsPosition);
				this.OldRect.SetActive(false);
			});
		}
		else
		{
			Observable.NextFrame(FrameCountType.Update).Subscribe(delegate(Unit _)
			{
				rt2.sizeDelta = ContentsRt.sizeDelta;
				this.InitTargrt(rt2, position2);
				this.OldRect.SetActive(false);
			});
		}
	}

	// Token: 0x060042B8 RID: 17080 RVA: 0x001A381D File Offset: 0x001A1C1D
	private void Update()
	{
		if (this.lstNodes.Count == 0)
		{
			return;
		}
		this.MoveContents();
		this.ChangeNode();
	}

	// Token: 0x060042B9 RID: 17081 RVA: 0x001A383C File Offset: 0x001A1C3C
	private void MoveContents()
	{
		RectTransform component = this.Contents.GetComponent<RectTransform>();
		Vector3 vector = component.anchoredPosition;
		if (this.enableInternalScroll)
		{
			float num = this.input.ScrollValue();
			if (this.onEnter)
			{
				if (num < 0f && this.targetNode.Item1 < this.lstNodes.Count - 1)
				{
					int num2 = this.targetNode.Item1 + 1;
					RectTransform component2 = this.lstNodes[num2].GetComponent<RectTransform>();
					Vector2 i;
					i.x = component.anchoredPosition.x - component.sizeDelta.x / 2f + component2.anchoredPosition.x;
					i.y = component.anchoredPosition.y + component.sizeDelta.y / 2f + component2.anchoredPosition.y;
					i.x = vector.x - i.x;
					i.y = vector.y - i.y;
					this.targetNode = new UnityEx.ValueTuple<int, Vector2, ScrollCylinderNode>(num2, i, this.lstNodes[num2]);
					if (this._onValueChange != null)
					{
						this._onValueChange.OnNext(num2);
					}
				}
				else if (num > 0f && this.targetNode.Item1 > 0)
				{
					int num3 = this.targetNode.Item1 - 1;
					RectTransform component3 = this.lstNodes[num3].GetComponent<RectTransform>();
					Vector2 i2;
					i2.x = component.anchoredPosition.x - component.sizeDelta.x / 2f + component3.anchoredPosition.x;
					i2.y = component.anchoredPosition.y + component.sizeDelta.y / 2f + component3.anchoredPosition.y;
					i2.x = vector.x - i2.x;
					i2.y = vector.y - i2.y;
					this.targetNode = new UnityEx.ValueTuple<int, Vector2, ScrollCylinderNode>(num3, i2, this.lstNodes[num3]);
					if (this._onValueChange != null)
					{
						this._onValueChange.OnNext(num3);
					}
				}
			}
		}
		Vector2 item = this.targetNode.Item2;
		float num4 = 0f;
		vector.x = Mathf.SmoothDamp(vector.x, item.x, ref num4, this.moveTime, float.PositiveInfinity, Time.deltaTime);
		num4 = 0f;
		vector.y = Mathf.SmoothDamp(vector.y, item.y, ref num4, this.moveTime, float.PositiveInfinity, Time.deltaTime);
		vector = this.LimitPos(component, vector);
		component.anchoredPosition = vector;
	}

	// Token: 0x060042BA RID: 17082 RVA: 0x001A3B50 File Offset: 0x001A1F50
	private void ChangeNode()
	{
		float deltaTime = Time.deltaTime;
		this.ChangeNodeColor();
		this.ChangeNodeScl();
		if (this.cursor != null)
		{
			this.CursorMove(deltaTime);
		}
	}

	// Token: 0x060042BB RID: 17083 RVA: 0x001A3B88 File Offset: 0x001A1F88
	private void ChangeNodeColor()
	{
		for (int i = 0; i < this.lstNodes.Count; i++)
		{
			int num = i;
			if (num == this.targetNode.Item1)
			{
				this.lstNodes[num].ChangeBGAlpha(0);
			}
			else if (num == this.targetNode.Item1 - 1 || num == this.targetNode.Item1 + 1)
			{
				this.lstNodes[num].ChangeBGAlpha(1);
			}
			else if (num == this.targetNode.Item1 - 2 || num == this.targetNode.Item1 + 2)
			{
				this.lstNodes[num].ChangeBGAlpha(2);
			}
			else
			{
				this.lstNodes[num].ChangeBGAlpha(3);
			}
		}
		int num2 = this.lstBlankNodes.Count / 2;
		for (int j = 0; j < this.lstBlankNodes.Count; j++)
		{
			if (this.lstNodes.Count == 1)
			{
				if (j == num2 - 2 || j == num2 + 1)
				{
					this.lstBlankNodes[j].ChangeBGAlpha(2);
				}
				else if (j == num2 - 1 || j == num2)
				{
					this.lstBlankNodes[j].ChangeBGAlpha(1);
				}
			}
			else if (this.targetNode.Item1 == 0)
			{
				if (j == num2 - 2)
				{
					this.lstBlankNodes[j].ChangeBGAlpha(2);
				}
				else if (j == num2 - 1)
				{
					this.lstBlankNodes[j].ChangeBGAlpha(1);
				}
				else if (j == num2 && this.targetNode.Item1 + 2 == this.lstNodes.Count)
				{
					this.lstBlankNodes[j].ChangeBGAlpha(2);
				}
				else
				{
					this.lstBlankNodes[j].ChangeBGAlpha(3);
				}
			}
			else if (this.targetNode.Item1 == this.lstNodes.Count - 1)
			{
				if (j == num2)
				{
					this.lstBlankNodes[j].ChangeBGAlpha(1);
				}
				else if (j == num2 + 1)
				{
					this.lstBlankNodes[j].ChangeBGAlpha(2);
				}
				else if (j == num2 - 1 && this.targetNode.Item1 - 2 == -1)
				{
					this.lstBlankNodes[j].ChangeBGAlpha(2);
				}
				else
				{
					this.lstBlankNodes[j].ChangeBGAlpha(3);
				}
			}
			else if (this.targetNode.Item1 == 1)
			{
				if (j == num2 - 1)
				{
					this.lstBlankNodes[j].ChangeBGAlpha(2);
				}
				else if (j == num2 && this.targetNode.Item1 + 2 == this.lstNodes.Count)
				{
					this.lstBlankNodes[j].ChangeBGAlpha(2);
				}
				else
				{
					this.lstBlankNodes[j].ChangeBGAlpha(3);
				}
			}
			else if (this.targetNode.Item1 == this.lstNodes.Count - 2)
			{
				if (j == num2)
				{
					this.lstBlankNodes[j].ChangeBGAlpha(2);
				}
				else if (j == num2 - 1 && this.targetNode.Item1 - 2 == -1)
				{
					this.lstBlankNodes[j].ChangeBGAlpha(2);
				}
				else
				{
					this.lstBlankNodes[j].ChangeBGAlpha(3);
				}
			}
			else
			{
				this.lstBlankNodes[j].ChangeBGAlpha(3);
			}
		}
	}

	// Token: 0x060042BC RID: 17084 RVA: 0x001A3F54 File Offset: 0x001A2354
	private void ChangeNodeScl()
	{
		for (int i = 0; i < this.lstNodes.Count; i++)
		{
			int num = i;
			if (num == this.targetNode.Item1)
			{
				this.lstNodes[num].ChangeScale(0);
			}
			else if (num == this.targetNode.Item1 - 1 || num == this.targetNode.Item1 + 1)
			{
				this.lstNodes[num].ChangeScale(1);
			}
			else if (num == this.targetNode.Item1 - 2 || num == this.targetNode.Item1 + 2)
			{
				this.lstNodes[num].ChangeScale(2);
			}
			else
			{
				this.lstNodes[num].ChangeScale(3);
			}
		}
		int num2 = this.lstBlankNodes.Count / 2;
		for (int j = 0; j < this.lstBlankNodes.Count; j++)
		{
			if (this.lstNodes.Count == 1)
			{
				if (j == num2 - 2 || j == num2 + 1)
				{
					this.lstBlankNodes[j].ChangeScale(2);
				}
				else if (j == num2 - 1 || j == num2)
				{
					this.lstBlankNodes[j].ChangeScale(1);
				}
			}
			else if (this.targetNode.Item1 == 0)
			{
				if (j == num2 - 2)
				{
					this.lstBlankNodes[j].ChangeScale(2);
				}
				else if (j == num2 - 1)
				{
					this.lstBlankNodes[j].ChangeScale(1);
				}
				else if (j == num2 && this.targetNode.Item1 + 2 == this.lstNodes.Count)
				{
					this.lstBlankNodes[j].ChangeScale(2);
				}
				else
				{
					this.lstBlankNodes[j].ChangeScale(3);
				}
			}
			else if (this.targetNode.Item1 == this.lstNodes.Count - 1)
			{
				if (j == num2)
				{
					this.lstBlankNodes[j].ChangeScale(1);
				}
				else if (j == num2 + 1)
				{
					this.lstBlankNodes[j].ChangeScale(2);
				}
				else if (j == num2 - 1 && this.targetNode.Item1 - 2 == -1)
				{
					this.lstBlankNodes[j].ChangeScale(2);
				}
				else
				{
					this.lstBlankNodes[j].ChangeScale(3);
				}
			}
			else if (this.targetNode.Item1 == 1)
			{
				if (j == num2 - 1)
				{
					this.lstBlankNodes[j].ChangeScale(2);
				}
				else if (j == num2 && this.targetNode.Item1 + 2 == this.lstNodes.Count)
				{
					this.lstBlankNodes[j].ChangeScale(2);
				}
				else
				{
					this.lstBlankNodes[j].ChangeScale(3);
				}
			}
			else if (this.targetNode.Item1 == this.lstNodes.Count - 2)
			{
				if (j == num2)
				{
					this.lstBlankNodes[j].ChangeScale(2);
				}
				else
				{
					this.lstBlankNodes[j].ChangeScale(3);
				}
			}
			else if (j == num2 - 1 && this.targetNode.Item1 - 2 == -1)
			{
				this.lstBlankNodes[j].ChangeScale(2);
			}
			else
			{
				this.lstBlankNodes[j].ChangeScale(3);
			}
		}
	}

	// Token: 0x060042BD RID: 17085 RVA: 0x001A4320 File Offset: 0x001A2720
	private void CursorMove(float deltaTime)
	{
		float num;
		if (this.cursorMovePtn == 0)
		{
			this.cursorTime += deltaTime / this.cursorFirstHurfAnimTimeLimit;
			num = Mathf.InverseLerp(0f, 1f, this.cursorTime);
			if (num == 1f)
			{
				this.cursorMovePtn = 1;
			}
		}
		else
		{
			this.cursorTime -= deltaTime / this.cursorLaterHurfAnimTimeLimit;
			num = Mathf.InverseLerp(0f, 1f, this.cursorTime);
			if (num == 0f)
			{
				this.cursorMovePtn = 0;
			}
		}
		Vector3 v = Vector3.zero;
		v = this.cursor.GetComponent<RectTransform>().anchoredPosition;
		v.x = Mathf.Lerp(this.cursorInitPos - this.cursorMoveRange / 2f, this.cursorInitPos + this.cursorMoveRange / 2f, num);
		this.cursor.GetComponent<RectTransform>().anchoredPosition = v;
	}

	// Token: 0x060042BE RID: 17086 RVA: 0x001A4422 File Offset: 0x001A2822
	public List<ScrollCylinderNode> GetList()
	{
		return this.lstNodes;
	}

	// Token: 0x060042BF RID: 17087 RVA: 0x001A442A File Offset: 0x001A282A
	public UnityEx.ValueTuple<int, Vector2, ScrollCylinderNode> GetTarget()
	{
		return this.targetNode;
	}

	// Token: 0x060042C0 RID: 17088 RVA: 0x001A4434 File Offset: 0x001A2834
	public void SetTarget(ScrollCylinderNode target)
	{
		RectTransform component = this.Contents.GetComponent<RectTransform>();
		Vector3 vector = component.anchoredPosition;
		for (int i = 0; i < this.lstNodes.Count; i++)
		{
			int num = i;
			if (!(this.lstNodes[num] != target))
			{
				RectTransform component2 = this.lstNodes[num].GetComponent<RectTransform>();
				Vector2 i2;
				i2.x = component.anchoredPosition.x - component.rect.width / 2f + component2.anchoredPosition.x;
				i2.y = component.anchoredPosition.y + component.rect.height / 2f + component2.anchoredPosition.y;
				i2.x = vector.x - i2.x;
				i2.y = vector.y - i2.y;
				this.targetNode = new UnityEx.ValueTuple<int, Vector2, ScrollCylinderNode>(num, i2, target);
				if (this._onValueChange != null)
				{
					this._onValueChange.OnNext(num);
				}
				break;
			}
		}
	}

	// Token: 0x060042C1 RID: 17089 RVA: 0x001A457C File Offset: 0x001A297C
	private Vector3 LimitPos(RectTransform ContentsRt, Vector3 ContentsPosition)
	{
		RectTransform component = base.transform.GetComponent<RectTransform>();
		float num = 0f;
		float num2 = 0f;
		if (component.sizeDelta.y / ContentsRt.rect.height % 2f != 0f)
		{
			num = ContentsPosition.y + ContentsRt.rect.height / 2f - component.sizeDelta.y / 2f;
			num2 = ContentsPosition.y - ContentsRt.rect.height / 2f - -component.sizeDelta.y / 2f;
		}
		else if (this.lstNodes[0] != null)
		{
			RectTransform component2 = this.lstNodes[0].GetComponent<RectTransform>();
			num = ContentsPosition.y + ContentsRt.rect.height / 2f - component2.sizeDelta.y / 2f - component.sizeDelta.y / 2f;
			num2 = ContentsPosition.y - ContentsRt.rect.height / 2f + component2.sizeDelta.y / 2f - -component.sizeDelta.y / 2f;
		}
		if (num <= 0f)
		{
			ContentsPosition.y += Mathf.Abs(num);
		}
		else if (num2 >= 0f)
		{
			ContentsPosition.y -= Mathf.Abs(num2);
		}
		float num3 = 0f;
		float num4 = 0f;
		if (component.sizeDelta.x / ContentsRt.rect.width % 2f != 0f)
		{
			num3 = ContentsPosition.x - ContentsRt.rect.width / 2f - -component.sizeDelta.x / 2f;
			num4 = ContentsPosition.x + ContentsRt.rect.width / 2f - component.sizeDelta.x / 2f;
		}
		else if (this.lstNodes[0] != null)
		{
			RectTransform component3 = this.lstNodes[0].GetComponent<RectTransform>();
			num3 = ContentsPosition.x + ContentsRt.rect.width / 2f - component3.sizeDelta.x / 2f - component.sizeDelta.x / 2f;
			num4 = ContentsPosition.x - ContentsRt.rect.width / 2f + component3.sizeDelta.x / 2f - -component.sizeDelta.x / 2f;
		}
		if (num3 >= 0f)
		{
			ContentsPosition.x -= Mathf.Abs(num3);
		}
		else if (num4 <= 0f)
		{
			ContentsPosition.x += Mathf.Abs(num4);
		}
		return ContentsPosition;
	}

	// Token: 0x060042C2 RID: 17090 RVA: 0x001A48FC File Offset: 0x001A2CFC
	private void InitTargrt(RectTransform ContentsRt, Vector3 position)
	{
		float num = 0f;
		float num2 = 0f;
		RectTransform component = base.transform.GetComponent<RectTransform>();
		if (component.sizeDelta.x / ContentsRt.rect.width % 2f != 0f)
		{
			num = position.x - ContentsRt.rect.width / 2f - -component.sizeDelta.x / 2f;
		}
		else if (this.lstNodes[0] != null)
		{
			RectTransform component2 = this.lstNodes[0].GetComponent<RectTransform>();
			num = position.x - ContentsRt.rect.width / 2f - component2.sizeDelta.x / 2f - -component.sizeDelta.x / 2f;
		}
		if (component.sizeDelta.y / ContentsRt.rect.height % 2f != 0f)
		{
			num2 = position.y + ContentsRt.rect.height / 2f - component.sizeDelta.y / 2f;
		}
		else if (this.lstNodes[0] != null)
		{
			RectTransform component3 = this.lstNodes[0].GetComponent<RectTransform>();
			num2 = position.y + ContentsRt.rect.height / 2f - component3.sizeDelta.y / 2f - component.sizeDelta.y / 2f;
		}
		num = position.x - num;
		num2 = position.y - num2;
		this.targetNode = new UnityEx.ValueTuple<int, Vector2, ScrollCylinderNode>(0, new Vector2(num, num2), this.lstNodes[0]);
		if (this._onValueChange != null)
		{
			this._onValueChange.OnNext(0);
		}
	}

	// Token: 0x060042C3 RID: 17091 RVA: 0x001A4B28 File Offset: 0x001A2F28
	private void OldRectSet(RectTransform src, ref RectTransform newRect)
	{
		newRect.anchoredPosition = src.anchoredPosition;
		newRect.anchoredPosition3D = src.anchoredPosition3D;
		newRect.anchorMax = src.anchorMax;
		newRect.anchorMin = src.anchorMin;
		newRect.offsetMax = src.offsetMax;
		newRect.offsetMin = src.offsetMin;
		newRect.pivot = src.pivot;
		newRect.sizeDelta = src.sizeDelta;
	}

	// Token: 0x04003F91 RID: 16273
	public GameObject Contents;

	// Token: 0x04003F92 RID: 16274
	[Space]
	public RectTransform Atari;

	// Token: 0x04003F93 RID: 16275
	[Space]
	public ScrollCylinderNode blankObject;

	// Token: 0x04003F94 RID: 16276
	[Space]
	[SerializeField]
	private GameObject cursor;

	// Token: 0x04003F95 RID: 16277
	[SerializeField]
	private float cursorInitPos;

	// Token: 0x04003F96 RID: 16278
	[SerializeField]
	private float cursorMoveRange;

	// Token: 0x04003F97 RID: 16279
	private float cursorTime;

	// Token: 0x04003F98 RID: 16280
	private int cursorMovePtn;

	// Token: 0x04003F99 RID: 16281
	[SerializeField]
	private float cursorFirstHurfAnimTimeLimit;

	// Token: 0x04003F9A RID: 16282
	[SerializeField]
	private float cursorLaterHurfAnimTimeLimit;

	// Token: 0x04003F9B RID: 16283
	[Space]
	public float moveVal = 0.05f;

	// Token: 0x04003F9C RID: 16284
	[SerializeField]
	private float moveTime = 0.05f;

	// Token: 0x04003F9D RID: 16285
	[SerializeField]
	private List<ScrollCylinderNode> lstNodes = new List<ScrollCylinderNode>();

	// Token: 0x04003F9E RID: 16286
	[SerializeField]
	private List<ScrollCylinderNode> lstBlankNodes = new List<ScrollCylinderNode>();

	// Token: 0x04003F9F RID: 16287
	[SerializeField]
	private UnityEx.ValueTuple<int, Vector2, ScrollCylinderNode> targetNode = default(UnityEx.ValueTuple<int, Vector2, ScrollCylinderNode>);

	// Token: 0x04003FA0 RID: 16288
	private Manager.Input input;

	// Token: 0x04003FA1 RID: 16289
	private Subject<int> _onValueChange;

	// Token: 0x04003FA2 RID: 16290
	[SerializeField]
	private bool InitList;

	// Token: 0x04003FA3 RID: 16291
	private bool onEnter;

	// Token: 0x04003FA4 RID: 16292
	private GameObject OldRect;

	// Token: 0x04003FA5 RID: 16293
	public bool enableInternalScroll = true;

	// Token: 0x04003FA6 RID: 16294
	private bool setLastBlank;
}
