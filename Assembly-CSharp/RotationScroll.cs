using System;
using System.Collections.Generic;
using AIProject;
using Manager;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEx;

// Token: 0x02000B1A RID: 2842
public class RotationScroll : MonoBehaviour
{
	// Token: 0x06005353 RID: 21331 RVA: 0x002488A4 File Offset: 0x00246CA4
	private void Start()
	{
		this.input = Singleton<Manager.Input>.Instance;
		PointerEnterTrigger pointerEnterTrigger = this.Atari.gameObject.AddComponent<PointerEnterTrigger>();
		UITrigger.TriggerEvent triggerEvent = new UITrigger.TriggerEvent();
		pointerEnterTrigger.Triggers.Add(triggerEvent);
		triggerEvent.AddListener(delegate(BaseEventData x)
		{
			if (Cursor.visible)
			{
				this.onEnter = true;
			}
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
			this.ListNodeSet(null, true);
		}
	}

	// Token: 0x06005354 RID: 21332 RVA: 0x0024893C File Offset: 0x00246D3C
	public void ListNodeSet(List<ScrollCylinderNode> ScrollNodes = null, bool targetInit = true)
	{
		this.lstNodes.Clear();
		if (ScrollNodes != null)
		{
			for (int i = 0; i < ScrollNodes.Count; i++)
			{
				RectTransform component = ScrollNodes[i].GetComponent<RectTransform>();
				if (ScrollNodes.Count < this.MaxView)
				{
					component.anchoredPosition = new Vector2(component.rect.width / 2f, -component.rect.height / 2f) + new Vector2((this.scrollMode != ScrollDir.Vertical) ? (component.rect.width * (float)(this.MaxView - 1)) : 0f, (this.scrollMode != ScrollDir.Horizontal) ? (-component.rect.height * (float)(this.MaxView - 1)) : 0f) * (float)i;
				}
				else
				{
					component.anchoredPosition = new Vector2(component.rect.width / 2f, -component.rect.height / 2f) + new Vector2((this.scrollMode != ScrollDir.Vertical) ? component.rect.width : 0f, (this.scrollMode != ScrollDir.Horizontal) ? (-component.rect.height) : 0f) * (float)i;
				}
				this.lstNodes.AddLast(ScrollNodes[i]);
			}
		}
		else
		{
			ScrollCylinderNode[] componentsInChildren = this.Contents.GetComponentsInChildren<ScrollCylinderNode>();
			for (int j = 0; j < componentsInChildren.Length; j++)
			{
				RectTransform component2 = componentsInChildren[j].GetComponent<RectTransform>();
				if (componentsInChildren.Length < this.MaxView)
				{
					component2.anchoredPosition = new Vector2(component2.rect.width / 2f, -component2.rect.height / 2f) + new Vector2((this.scrollMode != ScrollDir.Vertical) ? (component2.rect.width * (float)(this.MaxView - 1)) : 0f, (this.scrollMode != ScrollDir.Horizontal) ? (-component2.rect.height * (float)(this.MaxView - 1)) : 0f) * (float)j;
				}
				else
				{
					component2.anchoredPosition = new Vector2(component2.rect.width / 2f, -component2.rect.height / 2f) + new Vector2((this.scrollMode != ScrollDir.Vertical) ? component2.rect.width : 0f, (this.scrollMode != ScrollDir.Horizontal) ? (-component2.rect.height) : 0f) * (float)j;
				}
				this.lstNodes.AddLast(componentsInChildren[j]);
			}
		}
		if (this.lstNodes.Count == 0)
		{
			return;
		}
		RectTransform ContentsRt = this.Contents.GetComponent<RectTransform>();
		Vector3 ContentsPosition = ContentsRt.anchoredPosition;
		RectTransform rt2 = UnityEngine.Object.Instantiate<RectTransform>(ContentsRt);
		Vector3 position2 = ContentsPosition;
		ContentsPosition = this.LimitPos(ContentsRt, ContentsPosition, (this.scrollMode != ScrollDir.Vertical) ? 0 : 1);
		ContentsRt.anchoredPosition = ContentsPosition;
		UITrigger.TriggerEvent triggerEvent = new UITrigger.TriggerEvent();
		foreach (ScrollCylinderNode scrollCylinderNode in this.lstNodes)
		{
			PointerEnterTrigger pointerEnterTrigger = scrollCylinderNode.gameObject.GetComponent<PointerEnterTrigger>();
			scrollCylinderNode.gameObject.SetActive(true);
			if (!(pointerEnterTrigger != null))
			{
				pointerEnterTrigger = scrollCylinderNode.gameObject.AddComponent<PointerEnterTrigger>();
				triggerEvent = new UITrigger.TriggerEvent();
				pointerEnterTrigger.Triggers.Add(triggerEvent);
				triggerEvent.AddListener(delegate(BaseEventData x)
				{
					this.onEnter = true;
				});
			}
		}
		if (!targetInit)
		{
			if (rt2 != null && rt2.gameObject != null)
			{
				UnityEngine.Object.Destroy(rt2.gameObject);
			}
			return;
		}
		if (ContentsRt.rect.width == 0f || ContentsRt.rect.height == 0f)
		{
			Observable.NextFrame(FrameCountType.Update).Subscribe(delegate(Unit _)
			{
				this.InitTargrt(ContentsRt, ContentsPosition);
				UnityEngine.Object.Destroy(rt2.gameObject);
			});
		}
		else
		{
			Observable.NextFrame(FrameCountType.Update).Subscribe(delegate(Unit _)
			{
				rt2.sizeDelta = ContentsRt.sizeDelta;
				this.InitTargrt(rt2, position2);
				UnityEngine.Object.Destroy(rt2.gameObject);
			});
		}
	}

	// Token: 0x06005355 RID: 21333 RVA: 0x00248E88 File Offset: 0x00247288
	private void Update()
	{
		if (this.lstNodes.Count == 0)
		{
			return;
		}
		this.MoveContents();
		this.ChangeNode();
	}

	// Token: 0x06005356 RID: 21334 RVA: 0x00248EA8 File Offset: 0x002472A8
	private void MoveContents()
	{
		RectTransform component = this.Contents.GetComponent<RectTransform>();
		Vector3 vector = component.anchoredPosition;
		float num = this.input.ScrollValue();
		LinkedListNode<ScrollCylinderNode> linkedListNode = this.lstNodes.Find(this.targetNode.Item2);
		if (linkedListNode == null)
		{
			return;
		}
		if (this.lstNodes.Count == 1)
		{
			num = 0f;
		}
		if (this.onEnter)
		{
			if (num < 0f)
			{
				LinkedListNode<ScrollCylinderNode> linkedListNode2;
				if (this.lstNodes.Count >= this.MaxView)
				{
					linkedListNode2 = linkedListNode.Next;
				}
				else
				{
					linkedListNode2 = (linkedListNode.Next ?? this.lstNodes.First);
				}
				RectTransform component2 = linkedListNode2.Value.GetComponent<RectTransform>();
				if (this.lstNodes.Count < this.MaxView)
				{
					RectTransform component3 = linkedListNode.Value.GetComponent<RectTransform>();
					component2.anchoredPosition = component3.anchoredPosition + new Vector2((this.scrollMode != ScrollDir.Vertical) ? (component3.rect.width * (float)(this.MaxView - 1)) : 0f, (this.scrollMode != ScrollDir.Horizontal) ? (-component3.rect.height * (float)(this.MaxView - 1)) : 0f);
					if (linkedListNode2 == this.lstNodes.First)
					{
						LinkedListNode<ScrollCylinderNode> node = new LinkedListNode<ScrollCylinderNode>(this.lstNodes.First.Value);
						this.lstNodes.RemoveFirst();
						this.lstNodes.AddLast(node);
						linkedListNode2 = this.lstNodes.Last;
						component2 = linkedListNode2.Value.GetComponent<RectTransform>();
					}
				}
				Vector2 i;
				i.x = component.anchoredPosition.x - component.sizeDelta.x / 2f + component2.anchoredPosition.x;
				i.y = component.anchoredPosition.y + component.sizeDelta.y / 2f + component2.anchoredPosition.y;
				i.x = vector.x - i.x;
				i.y = vector.y - i.y;
				this.targetNode = new UnityEx.ValueTuple<Vector2, ScrollCylinderNode>(i, linkedListNode2.Value);
				linkedListNode = linkedListNode2;
				Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Select);
			}
			else if (num > 0f)
			{
				LinkedListNode<ScrollCylinderNode> linkedListNode3;
				if (this.lstNodes.Count >= this.MaxView)
				{
					linkedListNode3 = linkedListNode.Previous;
				}
				else
				{
					linkedListNode3 = (linkedListNode.Previous ?? this.lstNodes.Last);
				}
				RectTransform component4 = linkedListNode3.Value.GetComponent<RectTransform>();
				if (this.lstNodes.Count < this.MaxView)
				{
					RectTransform component5 = linkedListNode.Value.GetComponent<RectTransform>();
					component4.anchoredPosition = component5.anchoredPosition + new Vector2((this.scrollMode != ScrollDir.Vertical) ? (-component5.rect.width * (float)(this.MaxView - 1)) : 0f, (this.scrollMode != ScrollDir.Horizontal) ? (component5.rect.height * (float)(this.MaxView - 1)) : 0f);
					if (linkedListNode3 == this.lstNodes.Last)
					{
						LinkedListNode<ScrollCylinderNode> node2 = new LinkedListNode<ScrollCylinderNode>(this.lstNodes.Last.Value);
						this.lstNodes.RemoveLast();
						this.lstNodes.AddFirst(node2);
						linkedListNode3 = this.lstNodes.First;
						component4 = linkedListNode3.Value.GetComponent<RectTransform>();
					}
				}
				Vector2 i2;
				i2.x = component.anchoredPosition.x - component.sizeDelta.x / 2f + component4.anchoredPosition.x;
				i2.y = component.anchoredPosition.y + component.sizeDelta.y / 2f + component4.anchoredPosition.y;
				i2.x = vector.x - i2.x;
				i2.y = vector.y - i2.y;
				this.targetNode = new UnityEx.ValueTuple<Vector2, ScrollCylinderNode>(i2, linkedListNode3.Value);
				linkedListNode = linkedListNode3;
				Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Select);
			}
		}
		if (this.lstNodes.Count >= this.MaxView)
		{
			if (linkedListNode.Previous == null)
			{
				LinkedListNode<ScrollCylinderNode> linkedListNode4 = new LinkedListNode<ScrollCylinderNode>(this.lstNodes.Last.Value);
				this.lstNodes.RemoveLast();
				this.lstNodes.AddFirst(linkedListNode4);
				RectTransform[] array = new RectTransform[]
				{
					linkedListNode4.Value.GetComponent<RectTransform>(),
					linkedListNode.Value.GetComponent<RectTransform>()
				};
				Vector2 anchoredPosition = array[0].anchoredPosition;
				if (array[0].anchoredPosition.x == array[1].anchoredPosition.x)
				{
					anchoredPosition.y = array[1].anchoredPosition.y + array[1].rect.height;
				}
				if (array[0].anchoredPosition.y == array[1].anchoredPosition.y)
				{
					anchoredPosition.x = array[1].anchoredPosition.x - array[1].rect.width;
				}
				array[0].anchoredPosition = anchoredPosition;
			}
			if (linkedListNode.Next == null)
			{
				LinkedListNode<ScrollCylinderNode> linkedListNode4 = new LinkedListNode<ScrollCylinderNode>(this.lstNodes.First.Value);
				this.lstNodes.RemoveFirst();
				this.lstNodes.AddLast(linkedListNode4);
				RectTransform[] array2 = new RectTransform[]
				{
					linkedListNode4.Value.GetComponent<RectTransform>(),
					linkedListNode.Value.GetComponent<RectTransform>()
				};
				Vector2 anchoredPosition2 = array2[0].anchoredPosition;
				if (array2[0].anchoredPosition.x == array2[1].anchoredPosition.x)
				{
					anchoredPosition2.y = array2[1].anchoredPosition.y - array2[0].rect.height;
				}
				if (array2[0].anchoredPosition.y == array2[1].anchoredPosition.y)
				{
					anchoredPosition2.x = array2[1].anchoredPosition.x + array2[0].rect.width;
				}
				array2[0].anchoredPosition = anchoredPosition2;
			}
		}
		Vector2 item = this.targetNode.Item1;
		float num2 = 0f;
		vector.x = Mathf.SmoothDamp(vector.x, item.x, ref num2, this.moveTime, float.PositiveInfinity, Time.deltaTime);
		num2 = 0f;
		vector.y = Mathf.SmoothDamp(vector.y, item.y, ref num2, this.moveTime, float.PositiveInfinity, Time.deltaTime);
		vector = this.LimitPos(component, vector, (this.scrollMode != ScrollDir.Vertical) ? 0 : 1);
		component.anchoredPosition = vector;
	}

	// Token: 0x06005357 RID: 21335 RVA: 0x00249640 File Offset: 0x00247A40
	private void ChangeNode()
	{
		float deltaTime = Time.deltaTime;
		this.ChangeNodeColor();
		this.ChangeNodeScl();
	}

	// Token: 0x06005358 RID: 21336 RVA: 0x00249660 File Offset: 0x00247A60
	private void ChangeNodeColor()
	{
		LinkedListNode<ScrollCylinderNode> linkedListNode = this.lstNodes.Find(this.targetNode.Item2);
		if (linkedListNode == null)
		{
			return;
		}
		foreach (ScrollCylinderNode scrollCylinderNode in this.lstNodes)
		{
			if (scrollCylinderNode == linkedListNode.Value)
			{
				scrollCylinderNode.ChangeBGAlpha(0);
			}
			else
			{
				UnityEngine.Object x = scrollCylinderNode;
				LinkedListNode<ScrollCylinderNode> previous = linkedListNode.Previous;
				if (!(x == ((previous != null) ? previous.Value : null)))
				{
					UnityEngine.Object x2 = scrollCylinderNode;
					LinkedListNode<ScrollCylinderNode> next = linkedListNode.Next;
					if (!(x2 == ((next != null) ? next.Value : null)))
					{
						scrollCylinderNode.ChangeBGAlpha(3);
						continue;
					}
				}
				scrollCylinderNode.ChangeBGAlpha(1);
			}
		}
	}

	// Token: 0x06005359 RID: 21337 RVA: 0x00249744 File Offset: 0x00247B44
	private void ChangeNodeScl()
	{
		LinkedListNode<ScrollCylinderNode> linkedListNode = this.lstNodes.Find(this.targetNode.Item2);
		if (linkedListNode == null)
		{
			return;
		}
		foreach (ScrollCylinderNode scrollCylinderNode in this.lstNodes)
		{
			if (scrollCylinderNode == linkedListNode.Value)
			{
				scrollCylinderNode.ChangeScale(0);
			}
			else
			{
				UnityEngine.Object x = scrollCylinderNode;
				LinkedListNode<ScrollCylinderNode> previous = linkedListNode.Previous;
				if (!(x == ((previous != null) ? previous.Value : null)))
				{
					UnityEngine.Object x2 = scrollCylinderNode;
					LinkedListNode<ScrollCylinderNode> next = linkedListNode.Next;
					if (!(x2 == ((next != null) ? next.Value : null)))
					{
						scrollCylinderNode.ChangeScale(3);
						continue;
					}
				}
				scrollCylinderNode.ChangeScale(1);
			}
		}
	}

	// Token: 0x0600535A RID: 21338 RVA: 0x00249828 File Offset: 0x00247C28
	public LinkedList<ScrollCylinderNode> GetList()
	{
		return this.lstNodes;
	}

	// Token: 0x0600535B RID: 21339 RVA: 0x00249830 File Offset: 0x00247C30
	public UnityEx.ValueTuple<Vector2, ScrollCylinderNode> GetTarget()
	{
		return this.targetNode;
	}

	// Token: 0x0600535C RID: 21340 RVA: 0x00249838 File Offset: 0x00247C38
	public void SetTarget(ScrollCylinderNode target)
	{
		RectTransform component = this.Contents.GetComponent<RectTransform>();
		Vector3 vector = component.anchoredPosition;
		foreach (ScrollCylinderNode scrollCylinderNode in this.lstNodes)
		{
			if (!(scrollCylinderNode != target))
			{
				RectTransform component2 = scrollCylinderNode.GetComponent<RectTransform>();
				Vector2 i;
				i.x = component.anchoredPosition.x - component.rect.width / 2f + component2.anchoredPosition.x;
				i.y = component.anchoredPosition.y + component.rect.height / 2f + component2.anchoredPosition.y;
				i.x = vector.x - i.x;
				i.y = vector.y - i.y;
				this.targetNode = new UnityEx.ValueTuple<Vector2, ScrollCylinderNode>(i, target);
				break;
			}
		}
	}

	// Token: 0x0600535D RID: 21341 RVA: 0x00249978 File Offset: 0x00247D78
	public void SetTarget(int taii)
	{
		RectTransform component = this.Contents.GetComponent<RectTransform>();
		Vector3 vector = component.anchoredPosition;
		foreach (ScrollCylinderNode scrollCylinderNode in this.lstNodes)
		{
			int id = scrollCylinderNode.GetComponent<HRotationScrollNode>().id;
			if (id == taii)
			{
				RectTransform component2 = scrollCylinderNode.GetComponent<RectTransform>();
				Vector2 i;
				i.x = component.anchoredPosition.x - component.rect.width / 2f + component2.anchoredPosition.x;
				i.y = component.anchoredPosition.y + component.rect.height / 2f + component2.anchoredPosition.y;
				i.x = vector.x - i.x;
				i.y = vector.y - i.y;
				this.targetNode = new UnityEx.ValueTuple<Vector2, ScrollCylinderNode>(i, scrollCylinderNode);
				break;
			}
		}
	}

	// Token: 0x0600535E RID: 21342 RVA: 0x00249AC0 File Offset: 0x00247EC0
	private Vector3 LimitPos(RectTransform ContentsRt, Vector3 ContentsPosition, int LimitDir)
	{
		RectTransform component = base.transform.GetComponent<RectTransform>();
		if (LimitDir == 0)
		{
			float num = 0f;
			float num2 = 0f;
			if (component.sizeDelta.y / ContentsRt.rect.height % 2f != 0f)
			{
				num = ContentsPosition.y + ContentsRt.rect.height / 2f - component.sizeDelta.y / 2f;
				num2 = ContentsPosition.y - ContentsRt.rect.height / 2f - -component.sizeDelta.y / 2f;
			}
			else if (this.lstNodes.First.Value != null)
			{
				RectTransform component2 = this.lstNodes.First.Value.GetComponent<RectTransform>();
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
		}
		else
		{
			float num3 = 0f;
			float num4 = 0f;
			if (component.sizeDelta.x / ContentsRt.rect.width % 2f != 0f)
			{
				num3 = ContentsPosition.x - ContentsRt.rect.width / 2f - -component.sizeDelta.x / 2f;
				num4 = ContentsPosition.x + ContentsRt.rect.width / 2f - component.sizeDelta.x / 2f;
			}
			else if (this.lstNodes.First.Value != null)
			{
				RectTransform component3 = this.lstNodes.First.Value.GetComponent<RectTransform>();
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
		}
		return ContentsPosition;
	}

	// Token: 0x0600535F RID: 21343 RVA: 0x00249E58 File Offset: 0x00248258
	private void InitTargrt(RectTransform ContentsRt, Vector3 position)
	{
		float num = 0f;
		float num2 = 0f;
		RectTransform selectRect = this.SelectRect;
		if (selectRect.sizeDelta.x / ContentsRt.rect.width % 2f != 0f)
		{
			num = position.x - ContentsRt.rect.width / 2f - -selectRect.sizeDelta.x / 2f;
		}
		else if (this.lstNodes.First.Value != null)
		{
			RectTransform component = this.lstNodes.First.Value.GetComponent<RectTransform>();
			num = position.x - ContentsRt.rect.width / 2f - component.sizeDelta.x / 2f - -selectRect.sizeDelta.x / 2f;
		}
		if (selectRect.sizeDelta.y / ContentsRt.rect.height % 2f != 0f)
		{
			num2 = position.y + ContentsRt.rect.height / 2f - selectRect.sizeDelta.y / 2f;
		}
		else if (this.lstNodes.First.Value != null)
		{
			RectTransform component2 = this.lstNodes.First.Value.GetComponent<RectTransform>();
			num2 = position.y + ContentsRt.rect.height / 2f - component2.sizeDelta.y / 2f - selectRect.sizeDelta.y / 2f;
		}
		num = position.x - num;
		num2 = position.y - num2;
		ScrollCylinderNode i = null;
		foreach (HRotationScrollNode hrotationScrollNode in this.NodeList)
		{
			if (hrotationScrollNode.gameObject.activeSelf)
			{
				i = hrotationScrollNode;
				break;
			}
		}
		this.targetNode = new UnityEx.ValueTuple<Vector2, ScrollCylinderNode>(new Vector2(num, num2), i);
	}

	// Token: 0x06005360 RID: 21344 RVA: 0x0024A0B5 File Offset: 0x002484B5
	public void ListClear()
	{
		this.lstNodes.Clear();
	}

	// Token: 0x04004DE3 RID: 19939
	public GameObject Contents;

	// Token: 0x04004DE4 RID: 19940
	[Space]
	[SerializeField]
	public RectTransform SelectRect;

	// Token: 0x04004DE5 RID: 19941
	[Space]
	public RectTransform Atari;

	// Token: 0x04004DE6 RID: 19942
	[SerializeField]
	private float moveTime = 0.05f;

	// Token: 0x04004DE7 RID: 19943
	[SerializeField]
	private LinkedList<ScrollCylinderNode> lstNodes = new LinkedList<ScrollCylinderNode>();

	// Token: 0x04004DE8 RID: 19944
	[SerializeField]
	private UnityEx.ValueTuple<Vector2, ScrollCylinderNode> targetNode = default(UnityEx.ValueTuple<Vector2, ScrollCylinderNode>);

	// Token: 0x04004DE9 RID: 19945
	private Manager.Input input;

	// Token: 0x04004DEA RID: 19946
	[SerializeField]
	private bool InitList;

	// Token: 0x04004DEB RID: 19947
	[SerializeField]
	private int MaxView = 3;

	// Token: 0x04004DEC RID: 19948
	private bool onEnter;

	// Token: 0x04004DED RID: 19949
	public HRotationScrollNode[] NodeList;

	// Token: 0x04004DEE RID: 19950
	public ScrollDir scrollMode;
}
