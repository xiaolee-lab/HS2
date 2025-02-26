using System;
using System.Collections.Generic;
using AIChara;
using Manager;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000B11 RID: 2833
public class HSceneSpriteClothCondition : HSceneSpriteCategory
{
	// Token: 0x06005320 RID: 21280 RVA: 0x002464E8 File Offset: 0x002448E8
	public void Init()
	{
		this.hScene = Singleton<HSceneFlagCtrl>.Instance.GetComponent<HScene>();
		this.hSceneManager = Singleton<HSceneManager>.Instance;
		this.females = this.hScene.GetFemales();
		this.SetClothCharacter(true);
		this.hSceneSpriteChaChoice.SetAction(delegate
		{
			this.SetClothCharacter(false);
		});
	}

	// Token: 0x06005321 RID: 21281 RVA: 0x00246540 File Offset: 0x00244940
	public void SetClothCharacter(bool init = false)
	{
		if (!base.gameObject.activeSelf && !init)
		{
			return;
		}
		if (this.females[this.hSceneManager.numFemaleClothCustom].objBodyBone != null && this.females[this.hSceneManager.numFemaleClothCustom].visibleAll)
		{
			int num = -1;
			bool flag = true;
			for (int i = 0; i < 8; i++)
			{
				this.objs[i].SetButton((int)this.females[this.hSceneManager.numFemaleClothCustom].fileStatus.clothesState[i]);
				bool flag2 = this.females[this.hSceneManager.numFemaleClothCustom].IsClothesStateKind(i);
				this.SetActive(flag2, i);
				if (flag2 && flag)
				{
					if (num < 0)
					{
						num = (int)this.females[this.hSceneManager.numFemaleClothCustom].fileStatus.clothesState[i];
					}
					else
					{
						flag = (num == (int)this.females[this.hSceneManager.numFemaleClothCustom].fileStatus.clothesState[i]);
					}
				}
			}
			if (!flag)
			{
				this.allState[this.hSceneManager.numFemaleClothCustom] = 1;
			}
			else if (num >= 0)
			{
				this.allState[this.hSceneManager.numFemaleClothCustom] = num;
				this.allState[this.hSceneManager.numFemaleClothCustom] %= 3;
			}
			for (int j = 0; j < this.AllChange.Length; j++)
			{
				if (this.allState[this.hSceneManager.numFemaleClothCustom] != j)
				{
					this.AllChange[j].gameObject.SetActive(false);
				}
				else
				{
					this.AllChange[j].gameObject.SetActive(true);
				}
			}
		}
		if (this.hSceneSpriteChaChoice.Content.activeSelf)
		{
			this.hSceneSpriteChaChoice.Content.SetActive(false);
		}
	}

	// Token: 0x06005322 RID: 21282 RVA: 0x00246740 File Offset: 0x00244B40
	public void OnClickCloth(int _cloth)
	{
		if (Singleton<Scene>.Instance.IsNowLoading || Singleton<Scene>.Instance.IsNowLoadingFade || this.hSceneSprite.isFade || !this.hSceneManager.HSceneUISet.activeSelf)
		{
			return;
		}
		if (this.hSceneManager.Player.ChaControl == this.females[this.hSceneManager.numFemaleClothCustom] && !Config.HData.Cloth)
		{
			return;
		}
		this.females[this.hSceneManager.numFemaleClothCustom].SetClothesStateNext(_cloth);
		int num = -1;
		bool flag = true;
		for (int i = 0; i < 8; i++)
		{
			if (this.objs[i].gameObject.activeSelf)
			{
				this.objs[i].SetButton((int)this.females[this.hSceneManager.numFemaleClothCustom].fileStatus.clothesState[i]);
				if (flag)
				{
					if (num < 0)
					{
						num = (int)this.females[this.hSceneManager.numFemaleClothCustom].fileStatus.clothesState[i];
					}
					else
					{
						flag = (num == (int)this.females[this.hSceneManager.numFemaleClothCustom].fileStatus.clothesState[i]);
					}
				}
			}
		}
		if (!flag)
		{
			this.allState[this.hSceneManager.numFemaleClothCustom] = 1;
		}
		else if (num >= 0)
		{
			this.allState[this.hSceneManager.numFemaleClothCustom] = num;
			this.allState[this.hSceneManager.numFemaleClothCustom] %= 3;
		}
		for (int j = 0; j < this.AllChange.Length; j++)
		{
			if (this.allState[this.hSceneManager.numFemaleClothCustom] != j)
			{
				this.AllChange[j].gameObject.SetActive(false);
			}
			else
			{
				this.AllChange[j].gameObject.SetActive(true);
			}
		}
	}

	// Token: 0x06005323 RID: 21283 RVA: 0x0024694C File Offset: 0x00244D4C
	public void OnClickAllCloth()
	{
		if (Singleton<Scene>.Instance.IsNowLoading || Singleton<Scene>.Instance.IsNowLoadingFade || this.hSceneSprite.isFade || !this.hSceneManager.HSceneUISet.activeSelf)
		{
			return;
		}
		this.allState[this.hSceneManager.numFemaleClothCustom]++;
		this.allState[this.hSceneManager.numFemaleClothCustom] %= 3;
		this.females[this.hSceneManager.numFemaleClothCustom].SetClothesStateAll((byte)this.allState[this.hSceneManager.numFemaleClothCustom]);
		for (int i = 0; i < this.AllChange.Length; i++)
		{
			if (this.allState[this.hSceneManager.numFemaleClothCustom] != i)
			{
				this.AllChange[i].gameObject.SetActive(false);
			}
			else
			{
				this.AllChange[i].gameObject.SetActive(true);
			}
		}
		for (int j = 0; j < 8; j++)
		{
			this.objs[j].SetButton((int)this.females[this.hSceneManager.numFemaleClothCustom].fileStatus.clothesState[j]);
		}
	}

	// Token: 0x06005324 RID: 21284 RVA: 0x00246A9C File Offset: 0x00244E9C
	public override void SetActive(bool _active, int _array = -1)
	{
		if (_array < 0)
		{
			for (int i = 0; i < this.objs.Count; i++)
			{
				if (this.objs[i].gameObject.activeSelf != _active)
				{
					this.objs[i].gameObject.SetActive(_active);
				}
			}
		}
		else if (this.objs.Count > _array && this.objs[_array].gameObject.activeSelf != _active)
		{
			this.objs[_array].gameObject.SetActive(_active);
		}
	}

	// Token: 0x04004D9E RID: 19870
	public HSceneSpriteChaChoice hSceneSpriteChaChoice;

	// Token: 0x04004D9F RID: 19871
	public List<HSceneSpriteClothBtn> objs = new List<HSceneSpriteClothBtn>();

	// Token: 0x04004DA0 RID: 19872
	public Button[] AllChange;

	// Token: 0x04004DA1 RID: 19873
	private HScene hScene;

	// Token: 0x04004DA2 RID: 19874
	private ChaControl[] females;

	// Token: 0x04004DA3 RID: 19875
	private int[] allState = new int[2];

	// Token: 0x04004DA4 RID: 19876
	[SerializeField]
	private HSceneSprite hSceneSprite;

	// Token: 0x04004DA5 RID: 19877
	private HSceneManager hSceneManager;
}
