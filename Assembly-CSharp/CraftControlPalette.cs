using System;
using UniRx;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000EF0 RID: 3824
public class CraftControlPalette : MonoBehaviour
{
	// Token: 0x06007CCA RID: 31946 RVA: 0x003566E4 File Offset: 0x00354AE4
	private void Start()
	{
		GameObject gameObject = this.paletteCorums[0];
		if (gameObject != null)
		{
			Button component = gameObject.GetComponent<Button>();
			if (component != null)
			{
				component.onClick.AddListener(new UnityAction(this.craftControler.Undo));
			}
		}
		GameObject gameObject2 = this.paletteCorums[1];
		if (gameObject2 != null)
		{
			Button component2 = gameObject2.GetComponent<Button>();
			if (component2 != null)
			{
				component2.onClick.AddListener(new UnityAction(this.craftControler.Redo));
			}
		}
		if (this.paletteCorums[4] != null)
		{
			Toggle component3 = this.paletteCorums[4].GetComponent<Toggle>();
			if (component3 != null)
			{
				component3.onValueChanged.AddListener(delegate(bool x)
				{
					this.ChangeCamLock(x);
				});
			}
		}
		GameObject gameObject3 = this.paletteCorums[6];
		if (gameObject3 != null)
		{
			Button component4 = gameObject3.GetComponent<Button>();
			if (component4 != null)
			{
				component4.onClick.AddListener(new UnityAction(this.SetEndPanel));
			}
		}
		(from x in this.nTargetFloorCnt
		where x != this.nPrevTargetFloorCnt
		select x).Subscribe(delegate(int x)
		{
			this.ChangeFloorCntTex();
		});
		if (this.floorUp != null)
		{
			this.floorUp.onClick.AddListener(new UnityAction(this.craftControler.OpelateFloorUp));
		}
		if (this.floorUp != null)
		{
			this.floorUp.onClick.AddListener(new UnityAction(this.ChangeFloorCntTex));
		}
		if (this.floorDown != null)
		{
			this.floorDown.onClick.AddListener(new UnityAction(this.craftControler.OpelateFloorUp));
		}
		if (this.floorDown != null)
		{
			this.floorDown.onClick.AddListener(new UnityAction(this.ChangeFloorCntTex));
		}
		if (this.craftEnd != null)
		{
			this.craftEnd.onClick.AddListener(delegate()
			{
				this.endPanel.SetActive(false);
			});
		}
		if (this.cancel != null)
		{
			this.cancel.onClick.AddListener(delegate()
			{
				this.endPanel.SetActive(false);
			});
		}
	}

	// Token: 0x06007CCB RID: 31947 RVA: 0x00356903 File Offset: 0x00354D03
	private void ChangeFloorCntTex()
	{
		this.nPrevTargetFloorCnt = this.nTargetFloorCnt.Value;
		this.floorCnt.text = string.Format("{0}", this.nTargetFloorCnt.Value + 1);
	}

	// Token: 0x06007CCC RID: 31948 RVA: 0x0035693D File Offset: 0x00354D3D
	private void ChangeCamLock(bool x)
	{
		this.Cam.bLock = x;
	}

	// Token: 0x06007CCD RID: 31949 RVA: 0x0035694B File Offset: 0x00354D4B
	private void SetEndPanel()
	{
		if (this.endPanel.activeSelf)
		{
			return;
		}
		this.endPanel.SetActive(true);
	}

	// Token: 0x06007CCE RID: 31950 RVA: 0x0035696A File Offset: 0x00354D6A
	private void Update()
	{
		this.nTargetFloorCnt.Value = Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt;
	}

	// Token: 0x040064C0 RID: 25792
	[SerializeField]
	private GameObject[] paletteCorums;

	// Token: 0x040064C1 RID: 25793
	[SerializeField]
	private GameObject endPanel;

	// Token: 0x040064C2 RID: 25794
	[SerializeField]
	private Button craftEnd;

	// Token: 0x040064C3 RID: 25795
	[SerializeField]
	private Button cancel;

	// Token: 0x040064C4 RID: 25796
	[SerializeField]
	private Text floorCnt;

	// Token: 0x040064C5 RID: 25797
	[SerializeField]
	private Button floorUp;

	// Token: 0x040064C6 RID: 25798
	[SerializeField]
	private Button floorDown;

	// Token: 0x040064C7 RID: 25799
	[SerializeField]
	private CraftControler craftControler;

	// Token: 0x040064C8 RID: 25800
	[SerializeField]
	private CraftCamera Cam;

	// Token: 0x040064C9 RID: 25801
	private IntReactiveProperty nTargetFloorCnt = new IntReactiveProperty(0);

	// Token: 0x040064CA RID: 25802
	private int nPrevTargetFloorCnt;
}
