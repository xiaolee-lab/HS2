using System;
using System.Collections.Generic;
using ReMotion;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000B14 RID: 2836
public class HsceneSpriteTaiiCategory : HSceneSpriteCategory
{
	// Token: 0x0600533E RID: 21310 RVA: 0x00247680 File Offset: 0x00245A80
	public void Init()
	{
		this.lstScrollNode = this.hSceneScroll.GetList();
		if (this.lstScrollNode.Count <= 1)
		{
			this.leftArrow.interactable = false;
			this.imgLeftArrow.color = this.arrowColor[1];
			this.rightArrow.interactable = false;
			this.imgRightArrow.color = this.arrowColor[1];
		}
		if (!this.leftArrow.interactable)
		{
			this.leftArrow.interactable = true;
			this.imgLeftArrow.color = this.arrowColor[0];
		}
		if (!this.rightArrow.interactable)
		{
			this.rightArrow.interactable = true;
			this.imgRightArrow.color = this.arrowColor[0];
		}
		this.leftArrow.onClick.AddListener(delegate()
		{
			this.btOld = new LinkedListNode<ScrollCylinderNode>(this.target.Value);
			LinkedListNode<ScrollCylinderNode> linkedListNode;
			if (this.lstScrollNode.Count >= 3)
			{
				linkedListNode = this.target.Previous;
			}
			else
			{
				RectTransform component = this.target.Value.GetComponent<RectTransform>();
				linkedListNode = (this.target.Previous ?? this.lstScrollNode.Last);
				RectTransform component2 = linkedListNode.Value.GetComponent<RectTransform>();
				component2.anchoredPosition = component.anchoredPosition + new Vector2((this.hSceneScroll.scrollMode != ScrollDir.Vertical) ? (-component.rect.width * 2f) : 0f, (this.hSceneScroll.scrollMode != ScrollDir.Horizontal) ? (component.rect.height * 2f) : 0f);
				if (linkedListNode == this.lstScrollNode.Last)
				{
					LinkedListNode<ScrollCylinderNode> node = new LinkedListNode<ScrollCylinderNode>(this.lstScrollNode.Last.Value);
					this.lstScrollNode.RemoveLast();
					this.lstScrollNode.AddFirst(node);
					linkedListNode = this.lstScrollNode.First;
				}
			}
			UnityEngine.Object value = this.btOld.Value;
			LinkedListNode<ScrollCylinderNode> next = linkedListNode.Next;
			if (value == ((next != null) ? next.Value : null))
			{
				this.ArrowChangeState[0] = 0;
				this.ArrowAnimTime[0] = 0f;
			}
			this.hSceneScroll.SetTarget(linkedListNode.Value);
		});
		this.rightArrow.onClick.AddListener(delegate()
		{
			this.btOld = new LinkedListNode<ScrollCylinderNode>(this.target.Value);
			LinkedListNode<ScrollCylinderNode> linkedListNode;
			if (this.lstScrollNode.Count >= 3)
			{
				linkedListNode = this.target.Next;
			}
			else
			{
				RectTransform component = this.target.Value.GetComponent<RectTransform>();
				linkedListNode = (this.target.Next ?? this.lstScrollNode.First);
				RectTransform component2 = linkedListNode.Value.GetComponent<RectTransform>();
				component2.anchoredPosition = component.anchoredPosition + new Vector2((this.hSceneScroll.scrollMode != ScrollDir.Vertical) ? (component.rect.width * 2f) : 0f, (this.hSceneScroll.scrollMode != ScrollDir.Horizontal) ? (-component.rect.height * 2f) : 0f);
				if (linkedListNode == this.lstScrollNode.First)
				{
					LinkedListNode<ScrollCylinderNode> node = new LinkedListNode<ScrollCylinderNode>(this.lstScrollNode.First.Value);
					this.lstScrollNode.RemoveFirst();
					this.lstScrollNode.AddLast(node);
					linkedListNode = this.lstScrollNode.Last;
				}
			}
			UnityEngine.Object value = this.btOld.Value;
			LinkedListNode<ScrollCylinderNode> previous = linkedListNode.Previous;
			if (value == ((previous != null) ? previous.Value : null))
			{
				this.ArrowChangeState[1] = 0;
				this.ArrowAnimTime[1] = 0f;
			}
			this.hSceneScroll.SetTarget(linkedListNode.Value);
		});
	}

	// Token: 0x0600533F RID: 21311 RVA: 0x002477A8 File Offset: 0x00245BA8
	private void Update()
	{
		if (this.hSceneScroll.GetTarget().Item2 == null)
		{
			return;
		}
		this.tmp = new LinkedListNode<ScrollCylinderNode>((this.target == null) ? null : this.target.Value);
		this.target = this.lstScrollNode.Find(this.hSceneScroll.GetTarget().Item2);
		if (this.tmp.Value != null && this.target != null && this.target.Value != null)
		{
			this.targetNode = this.target.Value.GetComponent<HRotationScrollNode>();
			if (this.targetNode.id != this.tmp.Value.GetComponent<HRotationScrollNode>().id)
			{
				UnityEngine.Object value = this.tmp.Value;
				LinkedListNode<ScrollCylinderNode> previous = this.target.Previous;
				if (value == ((previous != null) ? previous.Value : null))
				{
					this.ArrowChangeState[1] = 0;
					this.ArrowAnimTime[1] = 0f;
				}
				else
				{
					UnityEngine.Object value2 = this.tmp.Value;
					LinkedListNode<ScrollCylinderNode> next = this.target.Next;
					if (value2 == ((next != null) ? next.Value : null))
					{
						this.ArrowChangeState[0] = 0;
						this.ArrowAnimTime[0] = 0f;
					}
				}
				this.hSprite.OnClickMotion(this.targetNode.id);
			}
		}
		float deltaTime = Time.deltaTime;
		if (this.ArrowChangeState[0] != -1)
		{
			this.ButtonReaction(this.imgLeftArrow, ref this.ArrowChangeState[0], ref this.ArrowAnimTime[0], deltaTime);
		}
		if (this.ArrowChangeState[1] != -1)
		{
			this.ButtonReaction(this.imgRightArrow, ref this.ArrowChangeState[1], ref this.ArrowAnimTime[1], deltaTime);
		}
		this.ButtonWaitReaction(deltaTime);
	}

	// Token: 0x06005340 RID: 21312 RVA: 0x002479A9 File Offset: 0x00245DA9
	public RotationScroll GetHScroll()
	{
		return this.hSceneScroll;
	}

	// Token: 0x06005341 RID: 21313 RVA: 0x002479B4 File Offset: 0x00245DB4
	private void ButtonReaction(Image _image, ref int ArrowChangeState, ref float ArrowAnimTime, float deltaTime)
	{
		if (ArrowChangeState == 0)
		{
			ArrowAnimTime += deltaTime / this.ArrowBigAnimTimeLimit;
			float num = Mathf.InverseLerp(0f, 1f, ArrowAnimTime);
			num = EasingFunctions.EaseOutQuint(num, 1f);
			_image.color = new Color(Mathf.Lerp(this.arrowColor[0].r, this.arrowColor[2].r, num), Mathf.Lerp(this.arrowColor[0].g, this.arrowColor[2].g, num), Mathf.Lerp(this.arrowColor[0].b, this.arrowColor[2].b, num), Mathf.Lerp(this.arrowColor[0].a, this.arrowColor[2].a, num));
			_image.transform.localScale = Vector3.Lerp(Vector3.one, Vector3.one * 1.3f, num);
			if (num == 1f)
			{
				ArrowAnimTime = 0f;
				ArrowChangeState = 1;
			}
		}
		else if (ArrowChangeState == 1)
		{
			ArrowAnimTime += deltaTime / this.ArrowWaitAnimTimeLimit;
			float num2 = Mathf.InverseLerp(0f, 1f, ArrowAnimTime);
			if (num2 == 1f)
			{
				ArrowAnimTime = 1f;
				ArrowChangeState = 2;
			}
		}
		else if (ArrowChangeState == 2)
		{
			ArrowAnimTime -= deltaTime / this.ArrowSmallAnimTimeLimit;
			float num3 = Mathf.InverseLerp(0f, 1f, ArrowAnimTime);
			num3 = EasingFunctions.EaseOutQuint(num3, 1f);
			_image.color = new Color(Mathf.Lerp(this.arrowColor[0].r, this.arrowColor[2].r, num3), Mathf.Lerp(this.arrowColor[0].g, this.arrowColor[2].g, num3), Mathf.Lerp(this.arrowColor[0].b, this.arrowColor[2].b, num3), Mathf.Lerp(this.arrowColor[0].a, this.arrowColor[2].a, num3));
			_image.transform.localScale = Vector3.Lerp(Vector3.one, Vector3.one * 1.3f, num3);
			if (num3 == 0f)
			{
				ArrowAnimTime = 0f;
				ArrowChangeState = -1;
			}
		}
	}

	// Token: 0x06005342 RID: 21314 RVA: 0x00247C44 File Offset: 0x00246044
	private void ButtonWaitReaction(float deltaTime)
	{
		float num;
		if (this.ArrowWaitingMovePtn == 0)
		{
			this.ArrowWaitingTime += deltaTime / this.ArrowFirstHurfWaitingAnimTimeLimit;
			num = Mathf.InverseLerp(0f, 1f, this.ArrowWaitingTime);
			if (num == 1f)
			{
				this.ArrowWaitingMovePtn = 1;
			}
		}
		else
		{
			this.ArrowWaitingTime -= deltaTime / this.ArrowLaterHurfWaitingAnimTimeLimit;
			num = Mathf.InverseLerp(0f, 1f, this.ArrowWaitingTime);
			if (num == 0f)
			{
				this.ArrowWaitingMovePtn = 0;
			}
		}
		Vector3 localPosition = Vector3.zero;
		if (this.ArrowChangeState[0] == -1 && this.leftArrow.interactable)
		{
			localPosition = this.imgLeftArrow.transform.localPosition;
			localPosition.x = Mathf.Lerp(this.ArrowInitPos[0], this.ArrowInitPos[0] - this.ArrowMoveVal, num);
			this.imgLeftArrow.transform.localPosition = localPosition;
		}
		else
		{
			localPosition = this.imgLeftArrow.transform.localPosition;
			localPosition.x = this.ArrowInitPos[0];
			this.imgLeftArrow.transform.localPosition = localPosition;
		}
		if (this.ArrowChangeState[1] == -1 && this.rightArrow.interactable)
		{
			localPosition = this.imgRightArrow.transform.localPosition;
			localPosition.x = Mathf.Lerp(this.ArrowInitPos[1], this.ArrowInitPos[1] + this.ArrowMoveVal, num);
			this.imgRightArrow.transform.localPosition = localPosition;
		}
		else
		{
			localPosition = this.imgRightArrow.transform.localPosition;
			localPosition.x = this.ArrowInitPos[1];
			this.imgRightArrow.transform.localPosition = localPosition;
		}
	}

	// Token: 0x06005343 RID: 21315 RVA: 0x00247E1C File Offset: 0x0024621C
	public override void SetActive(bool _active, int _array = -1)
	{
		if (_array < 0)
		{
			for (int i = 0; i < this.lstButton.Count; i++)
			{
				if (this.lstButton[i].gameObject.activeSelf != _active)
				{
					this.lstButton[i].gameObject.SetActive(_active);
				}
			}
		}
		else if (this.lstButton.Count > _array && this.lstButton[_array].gameObject.activeSelf != _active)
		{
			this.lstButton[_array].gameObject.SetActive(_active);
		}
	}

	// Token: 0x06005344 RID: 21316 RVA: 0x00247EC8 File Offset: 0x002462C8
	public void EndProc()
	{
		this.leftArrow.onClick.RemoveAllListeners();
		this.rightArrow.onClick.RemoveAllListeners();
	}

	// Token: 0x04004DBE RID: 19902
	[SerializeField]
	private HSceneSprite hSprite;

	// Token: 0x04004DBF RID: 19903
	[SerializeField]
	private RotationScroll hSceneScroll;

	// Token: 0x04004DC0 RID: 19904
	[SerializeField]
	private Button leftArrow;

	// Token: 0x04004DC1 RID: 19905
	[SerializeField]
	private Image imgLeftArrow;

	// Token: 0x04004DC2 RID: 19906
	[SerializeField]
	private Button rightArrow;

	// Token: 0x04004DC3 RID: 19907
	[SerializeField]
	private Image imgRightArrow;

	// Token: 0x04004DC4 RID: 19908
	[Space(2f)]
	[SerializeField]
	private float[] ArrowInitPos;

	// Token: 0x04004DC5 RID: 19909
	[SerializeField]
	private float ArrowMoveVal;

	// Token: 0x04004DC6 RID: 19910
	[Space(2f)]
	private Color[] arrowColor = new Color[]
	{
		Color.white,
		Color.gray,
		Color.green
	};

	// Token: 0x04004DC7 RID: 19911
	private float ArrowWaitingTime;

	// Token: 0x04004DC8 RID: 19912
	private int ArrowWaitingMovePtn;

	// Token: 0x04004DC9 RID: 19913
	[SerializeField]
	private float ArrowFirstHurfWaitingAnimTimeLimit;

	// Token: 0x04004DCA RID: 19914
	[SerializeField]
	private float ArrowLaterHurfWaitingAnimTimeLimit;

	// Token: 0x04004DCB RID: 19915
	private int[] ArrowChangeState = new int[]
	{
		-1,
		-1
	};

	// Token: 0x04004DCC RID: 19916
	private float[] ArrowAnimTime = new float[2];

	// Token: 0x04004DCD RID: 19917
	[SerializeField]
	private float ArrowBigAnimTimeLimit;

	// Token: 0x04004DCE RID: 19918
	[SerializeField]
	private float ArrowWaitAnimTimeLimit;

	// Token: 0x04004DCF RID: 19919
	[SerializeField]
	private float ArrowSmallAnimTimeLimit;

	// Token: 0x04004DD0 RID: 19920
	private LinkedListNode<ScrollCylinderNode> target;

	// Token: 0x04004DD1 RID: 19921
	private HRotationScrollNode targetNode;

	// Token: 0x04004DD2 RID: 19922
	private LinkedList<ScrollCylinderNode> lstScrollNode = new LinkedList<ScrollCylinderNode>();

	// Token: 0x04004DD3 RID: 19923
	private LinkedListNode<ScrollCylinderNode> tmp;

	// Token: 0x04004DD4 RID: 19924
	private LinkedListNode<ScrollCylinderNode> btOld;
}
