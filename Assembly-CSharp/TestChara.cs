using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using AIChara;
using IllusionUtility.GetUtility;
using RootMotion.FinalIK;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;
using UnityEx;

// Token: 0x02000E23 RID: 3619
public class TestChara : MonoBehaviour
{
	// Token: 0x1700157C RID: 5500
	// (get) Token: 0x0600705A RID: 28762 RVA: 0x002FFED6 File Offset: 0x002FE2D6
	// (set) Token: 0x0600705B RID: 28763 RVA: 0x002FFEDD File Offset: 0x002FE2DD
	public static SortedDictionary<int, TestChara> CharaTable { get; private set; } = new SortedDictionary<int, TestChara>();

	// Token: 0x1700157D RID: 5501
	// (get) Token: 0x0600705C RID: 28764 RVA: 0x002FFEE5 File Offset: 0x002FE2E5
	// (set) Token: 0x0600705D RID: 28765 RVA: 0x002FFEEC File Offset: 0x002FE2EC
	public static bool BareFootState { get; set; } = true;

	// Token: 0x0600705E RID: 28766 RVA: 0x002FFEF4 File Offset: 0x002FE2F4
	public static TestChara CreateFemale(GameObject parent, int id)
	{
		int num = TestChara.SearchUnusedNo();
		string ab = "actor/prefabs/00.unity3d";
		string manifest = "abdata";
		GameObject original = CommonLib.LoadAsset<GameObject>(ab, "TestFemale", false, manifest);
		if (!TestChara._charaAssetBundleList.Exists((UnityEx.ValueTuple<string, string> x) => x.Item1 == ab && x.Item2 == manifest))
		{
			TestChara._charaAssetBundleList.Add(new UnityEx.ValueTuple<string, string>(ab, manifest));
		}
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(original);
		gameObject.name = string.Format("chaF{0}", num.ToString("00"));
		if (parent)
		{
			gameObject.transform.SetParent(parent.transform, false);
		}
		TestChara component = gameObject.GetComponent<TestChara>();
		if (component)
		{
			component.InitializeFemale(gameObject, id, num);
		}
		TestChara.CharaTable.Add(num, component);
		return component;
	}

	// Token: 0x0600705F RID: 28767 RVA: 0x002FFFE0 File Offset: 0x002FE3E0
	public static TestChara CreateMale(GameObject parent, int id)
	{
		int num = TestChara.SearchUnusedNo();
		string ab = "actor/prefabs/00.unity3d";
		string manifest = "abdata";
		GameObject original = CommonLib.LoadAsset<GameObject>(ab, "TestMale", false, manifest);
		if (!TestChara._charaAssetBundleList.Exists((UnityEx.ValueTuple<string, string> x) => x.Item1 == ab && x.Item2 == manifest))
		{
			TestChara._charaAssetBundleList.Add(new UnityEx.ValueTuple<string, string>(ab, manifest));
		}
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(original);
		gameObject.name = string.Format("chaM{0}", num.ToString("00"));
		if (parent)
		{
			gameObject.transform.SetParent(parent.transform, false);
		}
		TestChara component = gameObject.GetComponent<TestChara>();
		if (component)
		{
			component.InitializeMale(gameObject, id, num);
		}
		TestChara.CharaTable.Add(num, component);
		return component;
	}

	// Token: 0x06007060 RID: 28768 RVA: 0x003000CC File Offset: 0x002FE4CC
	public static TestChara CreateMerchant(GameObject parent, int id)
	{
		int num = TestChara.SearchUnusedNo();
		string ab = "actor/prefabs/00.unity3d";
		string manifest = "abdata";
		GameObject original = CommonLib.LoadAsset<GameObject>(ab, "TestMerchant", false, manifest);
		if (!TestChara._charaAssetBundleList.Exists((UnityEx.ValueTuple<string, string> x) => x.Item1 == ab && x.Item2 == manifest))
		{
			TestChara._charaAssetBundleList.Add(new UnityEx.ValueTuple<string, string>(ab, manifest));
		}
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(original);
		gameObject.name = string.Format("chaF{0}", num.ToString("00"));
		if (parent)
		{
			gameObject.transform.SetParent(parent.transform, false);
		}
		TestChara component = gameObject.GetComponent<TestChara>();
		if (component)
		{
			component.InitializeFemale(gameObject, id, num);
		}
		TestChara.CharaTable.Add(num, component);
		return component;
	}

	// Token: 0x06007061 RID: 28769 RVA: 0x003001B8 File Offset: 0x002FE5B8
	public static bool Delete(TestChara chara)
	{
		foreach (KeyValuePair<int, TestChara> keyValuePair in TestChara.CharaTable)
		{
			if (keyValuePair.Value == chara)
			{
				if (keyValuePair.Value)
				{
					UnityEngine.Object.Destroy(keyValuePair.Value.gameObject);
				}
				TestChara.CharaTable.Remove(keyValuePair.Key);
				return true;
			}
		}
		return false;
	}

	// Token: 0x06007062 RID: 28770 RVA: 0x0030025C File Offset: 0x002FE65C
	public static void DeleteCharaAlll()
	{
		foreach (KeyValuePair<int, TestChara> keyValuePair in TestChara.CharaTable)
		{
			if (keyValuePair.Value)
			{
				UnityEngine.Object.Destroy(keyValuePair.Value.gameObject);
			}
		}
		TestChara.CharaTable.Clear();
	}

	// Token: 0x06007063 RID: 28771 RVA: 0x003002DC File Offset: 0x002FE6DC
	private static int SearchUnusedNo()
	{
		int num = 0;
		foreach (KeyValuePair<int, TestChara> keyValuePair in TestChara.CharaTable)
		{
			if (num != keyValuePair.Key)
			{
				break;
			}
			num++;
		}
		return num;
	}

	// Token: 0x1700157E RID: 5502
	// (get) Token: 0x06007064 RID: 28772 RVA: 0x0030034C File Offset: 0x002FE74C
	// (set) Token: 0x06007065 RID: 28773 RVA: 0x00300354 File Offset: 0x002FE754
	public bool ApplySelf
	{
		get
		{
			return this._applySelfAnimatorParameter;
		}
		set
		{
			this._applySelfAnimatorParameter = value;
		}
	}

	// Token: 0x1700157F RID: 5503
	// (get) Token: 0x06007066 RID: 28774 RVA: 0x0030035D File Offset: 0x002FE75D
	public Animator AnimBody
	{
		[CompilerGenerated]
		get
		{
			return this._animBody;
		}
	}

	// Token: 0x17001580 RID: 5504
	// (get) Token: 0x06007067 RID: 28775 RVA: 0x00300365 File Offset: 0x002FE765
	public RuntimeAnimatorController Rac
	{
		[CompilerGenerated]
		get
		{
			return this._rac;
		}
	}

	// Token: 0x17001581 RID: 5505
	// (get) Token: 0x06007068 RID: 28776 RVA: 0x0030036D File Offset: 0x002FE76D
	// (set) Token: 0x06007069 RID: 28777 RVA: 0x00300375 File Offset: 0x002FE775
	public int CharID { get; private set; }

	// Token: 0x17001582 RID: 5506
	// (get) Token: 0x0600706A RID: 28778 RVA: 0x0030037E File Offset: 0x002FE77E
	// (set) Token: 0x0600706B RID: 28779 RVA: 0x00300386 File Offset: 0x002FE786
	public int LoadNo { get; private set; }

	// Token: 0x17001583 RID: 5507
	// (get) Token: 0x0600706C RID: 28780 RVA: 0x0030038F File Offset: 0x002FE78F
	// (set) Token: 0x0600706D RID: 28781 RVA: 0x00300397 File Offset: 0x002FE797
	public GameObject ObjRoot { get; private set; }

	// Token: 0x17001584 RID: 5508
	// (get) Token: 0x0600706E RID: 28782 RVA: 0x003003A0 File Offset: 0x002FE7A0
	// (set) Token: 0x0600706F RID: 28783 RVA: 0x003003A8 File Offset: 0x002FE7A8
	public GameObject ObjAnim { get; private set; }

	// Token: 0x17001585 RID: 5509
	// (get) Token: 0x06007070 RID: 28784 RVA: 0x003003B1 File Offset: 0x002FE7B1
	// (set) Token: 0x06007071 RID: 28785 RVA: 0x003003B9 File Offset: 0x002FE7B9
	public GameObject ObjBodyBone { get; private set; }

	// Token: 0x17001586 RID: 5510
	// (get) Token: 0x06007072 RID: 28786 RVA: 0x003003C2 File Offset: 0x002FE7C2
	// (set) Token: 0x06007073 RID: 28787 RVA: 0x003003CA File Offset: 0x002FE7CA
	public GameObject ObjHeadBone { get; private set; }

	// Token: 0x17001587 RID: 5511
	// (get) Token: 0x06007074 RID: 28788 RVA: 0x003003D3 File Offset: 0x002FE7D3
	// (set) Token: 0x06007075 RID: 28789 RVA: 0x003003DB File Offset: 0x002FE7DB
	public GameObject ObjBody { get; private set; }

	// Token: 0x17001588 RID: 5512
	// (get) Token: 0x06007076 RID: 28790 RVA: 0x003003E4 File Offset: 0x002FE7E4
	// (set) Token: 0x06007077 RID: 28791 RVA: 0x003003EC File Offset: 0x002FE7EC
	public GameObject ObjHead { get; private set; }

	// Token: 0x17001589 RID: 5513
	// (get) Token: 0x06007078 RID: 28792 RVA: 0x003003F5 File Offset: 0x002FE7F5
	// (set) Token: 0x06007079 RID: 28793 RVA: 0x003003FD File Offset: 0x002FE7FD
	public GameObject ObjHairBack { get; private set; }

	// Token: 0x1700158A RID: 5514
	// (get) Token: 0x0600707A RID: 28794 RVA: 0x00300406 File Offset: 0x002FE806
	// (set) Token: 0x0600707B RID: 28795 RVA: 0x0030040E File Offset: 0x002FE80E
	public GameObject ObjHairFront { get; private set; }

	// Token: 0x1700158B RID: 5515
	// (get) Token: 0x0600707C RID: 28796 RVA: 0x00300417 File Offset: 0x002FE817
	// (set) Token: 0x0600707D RID: 28797 RVA: 0x0030041F File Offset: 0x002FE81F
	public GameObject ObjClothTop { get; private set; }

	// Token: 0x1700158C RID: 5516
	// (get) Token: 0x0600707E RID: 28798 RVA: 0x00300428 File Offset: 0x002FE828
	// (set) Token: 0x0600707F RID: 28799 RVA: 0x00300430 File Offset: 0x002FE830
	public GameObject ObjClothBot { get; private set; }

	// Token: 0x1700158D RID: 5517
	// (get) Token: 0x06007080 RID: 28800 RVA: 0x00300439 File Offset: 0x002FE839
	// (set) Token: 0x06007081 RID: 28801 RVA: 0x00300441 File Offset: 0x002FE841
	public GameObject ObjBra { get; private set; }

	// Token: 0x1700158E RID: 5518
	// (get) Token: 0x06007082 RID: 28802 RVA: 0x0030044A File Offset: 0x002FE84A
	// (set) Token: 0x06007083 RID: 28803 RVA: 0x00300452 File Offset: 0x002FE852
	public GameObject ObjShorts { get; private set; }

	// Token: 0x1700158F RID: 5519
	// (get) Token: 0x06007084 RID: 28804 RVA: 0x0030045B File Offset: 0x002FE85B
	// (set) Token: 0x06007085 RID: 28805 RVA: 0x00300463 File Offset: 0x002FE863
	public GameObject ObjGlove { get; private set; }

	// Token: 0x17001590 RID: 5520
	// (get) Token: 0x06007086 RID: 28806 RVA: 0x0030046C File Offset: 0x002FE86C
	// (set) Token: 0x06007087 RID: 28807 RVA: 0x00300474 File Offset: 0x002FE874
	public GameObject ObjSocks { get; private set; }

	// Token: 0x17001591 RID: 5521
	// (get) Token: 0x06007088 RID: 28808 RVA: 0x0030047D File Offset: 0x002FE87D
	// (set) Token: 0x06007089 RID: 28809 RVA: 0x00300485 File Offset: 0x002FE885
	public GameObject ObjShoes { get; private set; }

	// Token: 0x17001592 RID: 5522
	// (get) Token: 0x0600708A RID: 28810 RVA: 0x0030048E File Offset: 0x002FE88E
	// (set) Token: 0x0600708B RID: 28811 RVA: 0x00300496 File Offset: 0x002FE896
	public GameObject[] ObjAcsArray { get; private set; } = new GameObject[20];

	// Token: 0x17001593 RID: 5523
	// (get) Token: 0x0600708C RID: 28812 RVA: 0x0030049F File Offset: 0x002FE89F
	// (set) Token: 0x0600708D RID: 28813 RVA: 0x003004A7 File Offset: 0x002FE8A7
	public GameObject ObjHitBody { get; protected set; }

	// Token: 0x17001594 RID: 5524
	// (get) Token: 0x0600708E RID: 28814 RVA: 0x003004B0 File Offset: 0x002FE8B0
	// (set) Token: 0x0600708F RID: 28815 RVA: 0x003004B8 File Offset: 0x002FE8B8
	public GameObject ObjHitHead { get; protected set; }

	// Token: 0x17001595 RID: 5525
	// (get) Token: 0x06007090 RID: 28816 RVA: 0x003004C1 File Offset: 0x002FE8C1
	// (set) Token: 0x06007091 RID: 28817 RVA: 0x003004C9 File Offset: 0x002FE8C9
	public Dictionary<ChaControlDefine.DynamicBoneKind, DynamicBone_Ver02> DictDynamicBone { get; private set; } = new Dictionary<ChaControlDefine.DynamicBoneKind, DynamicBone_Ver02>();

	// Token: 0x17001596 RID: 5526
	// (get) Token: 0x06007092 RID: 28818 RVA: 0x003004D2 File Offset: 0x002FE8D2
	// (set) Token: 0x06007093 RID: 28819 RVA: 0x003004DA File Offset: 0x002FE8DA
	public NeckLookControllerVer2 NeckLookControl { get; private set; }

	// Token: 0x17001597 RID: 5527
	// (get) Token: 0x06007094 RID: 28820 RVA: 0x003004E3 File Offset: 0x002FE8E3
	// (set) Token: 0x06007095 RID: 28821 RVA: 0x003004EB File Offset: 0x002FE8EB
	public EyeLookController EyeLookControl { get; private set; }

	// Token: 0x17001598 RID: 5528
	// (get) Token: 0x06007096 RID: 28822 RVA: 0x003004F4 File Offset: 0x002FE8F4
	// (set) Token: 0x06007097 RID: 28823 RVA: 0x003004FC File Offset: 0x002FE8FC
	public FaceBlendShape FBSCtrl { get; private set; }

	// Token: 0x17001599 RID: 5529
	// (get) Token: 0x06007098 RID: 28824 RVA: 0x00300505 File Offset: 0x002FE905
	// (set) Token: 0x06007099 RID: 28825 RVA: 0x0030050D File Offset: 0x002FE90D
	public FBSCtrlEyebrow EyebrowCtrl { get; private set; }

	// Token: 0x1700159A RID: 5530
	// (get) Token: 0x0600709A RID: 28826 RVA: 0x00300516 File Offset: 0x002FE916
	// (set) Token: 0x0600709B RID: 28827 RVA: 0x0030051E File Offset: 0x002FE91E
	public FBSCtrlEyes EyesCtrl { get; private set; }

	// Token: 0x1700159B RID: 5531
	// (get) Token: 0x0600709C RID: 28828 RVA: 0x00300527 File Offset: 0x002FE927
	// (set) Token: 0x0600709D RID: 28829 RVA: 0x0030052F File Offset: 0x002FE92F
	public FBSCtrlMouth MouthCtrl { get; private set; }

	// Token: 0x1700159C RID: 5532
	// (get) Token: 0x0600709E RID: 28830 RVA: 0x00300538 File Offset: 0x002FE938
	// (set) Token: 0x0600709F RID: 28831 RVA: 0x00300540 File Offset: 0x002FE940
	public byte[] SiruNewLv { get; private set; }

	// Token: 0x1700159D RID: 5533
	// (get) Token: 0x060070A0 RID: 28832 RVA: 0x00300549 File Offset: 0x002FE949
	// (set) Token: 0x060070A1 RID: 28833 RVA: 0x00300551 File Offset: 0x002FE951
	public Renderer FaceRenderer { get; private set; }

	// Token: 0x1700159E RID: 5534
	// (get) Token: 0x060070A2 RID: 28834 RVA: 0x0030055A File Offset: 0x002FE95A
	// (set) Token: 0x060070A3 RID: 28835 RVA: 0x00300562 File Offset: 0x002FE962
	public Renderer EyeRendererL { get; private set; }

	// Token: 0x1700159F RID: 5535
	// (get) Token: 0x060070A4 RID: 28836 RVA: 0x0030056B File Offset: 0x002FE96B
	// (set) Token: 0x060070A5 RID: 28837 RVA: 0x00300573 File Offset: 0x002FE973
	public Renderer EyeRendererR { get; private set; }

	// Token: 0x170015A0 RID: 5536
	// (get) Token: 0x060070A6 RID: 28838 RVA: 0x0030057C File Offset: 0x002FE97C
	// (set) Token: 0x060070A7 RID: 28839 RVA: 0x00300584 File Offset: 0x002FE984
	public Renderer TearRenderer { get; private set; }

	// Token: 0x170015A1 RID: 5537
	// (get) Token: 0x060070A8 RID: 28840 RVA: 0x0030058D File Offset: 0x002FE98D
	// (set) Token: 0x060070A9 RID: 28841 RVA: 0x00300595 File Offset: 0x002FE995
	public Material FaceMaterial { get; private set; }

	// Token: 0x170015A2 RID: 5538
	// (get) Token: 0x060070AA RID: 28842 RVA: 0x0030059E File Offset: 0x002FE99E
	// (set) Token: 0x060070AB RID: 28843 RVA: 0x003005A6 File Offset: 0x002FE9A6
	public Material EyeLMaterial { get; private set; }

	// Token: 0x170015A3 RID: 5539
	// (get) Token: 0x060070AC RID: 28844 RVA: 0x003005AF File Offset: 0x002FE9AF
	// (set) Token: 0x060070AD RID: 28845 RVA: 0x003005B7 File Offset: 0x002FE9B7
	public Material EyeRMaterial { get; private set; }

	// Token: 0x170015A4 RID: 5540
	// (get) Token: 0x060070AE RID: 28846 RVA: 0x003005C0 File Offset: 0x002FE9C0
	// (set) Token: 0x060070AF RID: 28847 RVA: 0x003005C8 File Offset: 0x002FE9C8
	public Material TearMaterial { get; private set; }

	// Token: 0x170015A5 RID: 5541
	// (get) Token: 0x060070B0 RID: 28848 RVA: 0x003005D1 File Offset: 0x002FE9D1
	// (set) Token: 0x060070B1 RID: 28849 RVA: 0x003005D9 File Offset: 0x002FE9D9
	public bool ResetupDynamicBone { get; set; }

	// Token: 0x170015A6 RID: 5542
	// (get) Token: 0x060070B2 RID: 28850 RVA: 0x003005E2 File Offset: 0x002FE9E2
	// (set) Token: 0x060070B3 RID: 28851 RVA: 0x003005EA File Offset: 0x002FE9EA
	public bool ResetupDynamicBoneBust { get; set; }

	// Token: 0x170015A7 RID: 5543
	// (get) Token: 0x060070B4 RID: 28852 RVA: 0x003005F3 File Offset: 0x002FE9F3
	// (set) Token: 0x060070B5 RID: 28853 RVA: 0x003005FB File Offset: 0x002FE9FB
	public bool UpdateBustSize { get; set; }

	// Token: 0x170015A8 RID: 5544
	// (get) Token: 0x060070B6 RID: 28854 RVA: 0x00300604 File Offset: 0x002FEA04
	// (set) Token: 0x060070B7 RID: 28855 RVA: 0x0030060C File Offset: 0x002FEA0C
	public GameObject ObjTop { get; private set; }

	// Token: 0x170015A9 RID: 5545
	// (get) Token: 0x060070B8 RID: 28856 RVA: 0x00300615 File Offset: 0x002FEA15
	// (set) Token: 0x060070B9 RID: 28857 RVA: 0x00300620 File Offset: 0x002FEA20
	public bool VisibleMozaiz
	{
		get
		{
			return this._visibleMozaic;
		}
		set
		{
			if (this._visibleMozaic == value)
			{
				return;
			}
			this._visibleMozaic = value;
			foreach (GameObject gameObject in this._objMozaicList)
			{
				gameObject.SetActive(value);
			}
		}
	}

	// Token: 0x170015AA RID: 5546
	// (get) Token: 0x060070BA RID: 28858 RVA: 0x00300690 File Offset: 0x002FEA90
	// (set) Token: 0x060070BB RID: 28859 RVA: 0x00300698 File Offset: 0x002FEA98
	public bool LoadEnd { get; private set; }

	// Token: 0x170015AB RID: 5547
	// (get) Token: 0x060070BC RID: 28860 RVA: 0x003006A1 File Offset: 0x002FEAA1
	// (set) Token: 0x060070BD RID: 28861 RVA: 0x003006A9 File Offset: 0x002FEAA9
	public byte Sex
	{
		get
		{
			return this._sex;
		}
		set
		{
			this._sex = value;
		}
	}

	// Token: 0x170015AC RID: 5548
	// (get) Token: 0x060070BE RID: 28862 RVA: 0x003006B2 File Offset: 0x002FEAB2
	// (set) Token: 0x060070BF RID: 28863 RVA: 0x003006BA File Offset: 0x002FEABA
	public bool VisibleAll { get; set; }

	// Token: 0x170015AD RID: 5549
	// (get) Token: 0x060070C0 RID: 28864 RVA: 0x003006C3 File Offset: 0x002FEAC3
	// (set) Token: 0x060070C1 RID: 28865 RVA: 0x003006CB File Offset: 0x002FEACB
	public bool HideEyesHighlight { get; set; }

	// Token: 0x170015AE RID: 5550
	// (get) Token: 0x060070C2 RID: 28866 RVA: 0x003006D4 File Offset: 0x002FEAD4
	// (set) Token: 0x060070C3 RID: 28867 RVA: 0x003006DC File Offset: 0x002FEADC
	public float TearsRate
	{
		get
		{
			return this._tearsRate;
		}
		set
		{
			this.ChangeTearsRate(value);
		}
	}

	// Token: 0x170015AF RID: 5551
	// (get) Token: 0x060070C4 RID: 28868 RVA: 0x003006E5 File Offset: 0x002FEAE5
	// (set) Token: 0x060070C5 RID: 28869 RVA: 0x003006ED File Offset: 0x002FEAED
	public float HohoAkaRate
	{
		get
		{
			return this._hohoAkaRate;
		}
		set
		{
			this.ChangeHohoAkaRate(value);
		}
	}

	// Token: 0x170015B0 RID: 5552
	// (get) Token: 0x060070C6 RID: 28870 RVA: 0x003006F6 File Offset: 0x002FEAF6
	// (set) Token: 0x060070C7 RID: 28871 RVA: 0x003006FE File Offset: 0x002FEAFE
	public int EyebrowPtn { get; set; }

	// Token: 0x170015B1 RID: 5553
	// (get) Token: 0x060070C8 RID: 28872 RVA: 0x00300707 File Offset: 0x002FEB07
	// (set) Token: 0x060070C9 RID: 28873 RVA: 0x0030070F File Offset: 0x002FEB0F
	public int EyesPtn { get; set; }

	// Token: 0x170015B2 RID: 5554
	// (get) Token: 0x060070CA RID: 28874 RVA: 0x00300718 File Offset: 0x002FEB18
	// (set) Token: 0x060070CB RID: 28875 RVA: 0x00300720 File Offset: 0x002FEB20
	public int MouthPtn { get; set; }

	// Token: 0x170015B3 RID: 5555
	// (get) Token: 0x060070CC RID: 28876 RVA: 0x00300729 File Offset: 0x002FEB29
	// (set) Token: 0x060070CD RID: 28877 RVA: 0x00300731 File Offset: 0x002FEB31
	public float EyesOpen { get; set; }

	// Token: 0x170015B4 RID: 5556
	// (get) Token: 0x060070CE RID: 28878 RVA: 0x0030073A File Offset: 0x002FEB3A
	// (set) Token: 0x060070CF RID: 28879 RVA: 0x00300742 File Offset: 0x002FEB42
	public float MouthOpen { get; set; }

	// Token: 0x170015B5 RID: 5557
	// (get) Token: 0x060070D0 RID: 28880 RVA: 0x0030074B File Offset: 0x002FEB4B
	// (set) Token: 0x060070D1 RID: 28881 RVA: 0x00300753 File Offset: 0x002FEB53
	public int EyesLookPtn { get; set; }

	// Token: 0x170015B6 RID: 5558
	// (get) Token: 0x060070D2 RID: 28882 RVA: 0x0030075C File Offset: 0x002FEB5C
	// (set) Token: 0x060070D3 RID: 28883 RVA: 0x00300764 File Offset: 0x002FEB64
	public int NeckLookPtn { get; set; }

	// Token: 0x170015B7 RID: 5559
	// (get) Token: 0x060070D4 RID: 28884 RVA: 0x0030076D File Offset: 0x002FEB6D
	// (set) Token: 0x060070D5 RID: 28885 RVA: 0x00300775 File Offset: 0x002FEB75
	public bool EyesBlink { get; set; }

	// Token: 0x060070D6 RID: 28886 RVA: 0x00300780 File Offset: 0x002FEB80
	private void Start()
	{
		if (this._loadOnStart)
		{
			int key = TestChara.SearchUnusedNo();
			TestChara.CharaTable[key] = this;
			this.Load();
		}
	}

	// Token: 0x060070D7 RID: 28887 RVA: 0x003007B0 File Offset: 0x002FEBB0
	public void InitializeFemale(GameObject objRoot, int id, int no)
	{
		this.VisibleAll = true;
		this.ObjRoot = objRoot;
		this.CharID = id;
		this.LoadNo = no;
		this.Sex = 1;
		this.deleteBodyBoneName = "cf_J_Root";
		this.deleteHeadBoneName = "cf_J_FaceRoot";
		this.SiruNewLv = new byte[Enum.GetValues(typeof(ChaFileDefine.SiruParts)).Length];
		for (int i = 0; i < this.SiruNewLv.Length; i++)
		{
			this.SiruNewLv[i] = 0;
		}
		this.FaceMaterial = null;
		this.LoadEnd = false;
	}

	// Token: 0x060070D8 RID: 28888 RVA: 0x00300848 File Offset: 0x002FEC48
	public void InitializeMale(GameObject objRoot, int id, int no)
	{
		this.VisibleAll = true;
		this.ObjRoot = objRoot;
		this.CharID = id;
		this.LoadNo = no;
		this.Sex = 0;
		this.deleteBodyBoneName = "cf_J_Root";
		this.deleteHeadBoneName = "cf_J_FaceRoot";
		this.LoadEnd = false;
	}

	// Token: 0x060070D9 RID: 28889 RVA: 0x00300895 File Offset: 0x002FEC95
	public void SetActiveTop(bool active)
	{
		if (this.ObjTop)
		{
			this.ObjTop.SetActive(active);
		}
	}

	// Token: 0x060070DA RID: 28890 RVA: 0x003008B3 File Offset: 0x002FECB3
	public bool GetActiveTop()
	{
		return this.ObjTop && this.ObjTop.activeSelf;
	}

	// Token: 0x060070DB RID: 28891 RVA: 0x003008D4 File Offset: 0x002FECD4
	public void Load()
	{
		if (this._shapeFace == null)
		{
			return;
		}
		if (!TestChara.CharaTable.ContainsValue(this))
		{
			int key = TestChara.SearchUnusedNo();
			TestChara.CharaTable.Add(key, this);
		}
		this.ObjTop = new GameObject("BodyTop");
		this.ObjTop.transform.SetParent(base.transform, false);
		this.ObjTop.transform.localPosition = Vector3.zero;
		this.ObjTop.transform.localRotation = Quaternion.identity;
		if (!this._boneBody)
		{
			return;
		}
		this.ObjAnim = UnityEngine.Object.Instantiate<GameObject>(this._boneBody, this.ObjTop.transform);
		this.ObjAnim.transform.localPosition = Vector3.zero;
		this.ObjAnim.transform.localRotation = Quaternion.identity;
		this._animBody = this.ObjAnim.GetComponent<Animator>();
		DynamicBone_Ver02[] componentsInChildren = this.ObjAnim.GetComponentsInChildren<DynamicBone_Ver02>(true);
		foreach (DynamicBone_Ver02 dynamicBone_Ver in componentsInChildren)
		{
			string comment = dynamicBone_Ver.Comment;
			if (comment != null)
			{
				if (!(comment == "Mune_L"))
				{
					if (!(comment == "Mune_R"))
					{
						if (!(comment == "Siri_L"))
						{
							if (comment == "Siri_R")
							{
								this.DictDynamicBone[ChaControlDefine.DynamicBoneKind.HipR] = dynamicBone_Ver;
							}
						}
						else
						{
							this.DictDynamicBone[ChaControlDefine.DynamicBoneKind.HipL] = dynamicBone_Ver;
						}
					}
					else
					{
						this.DictDynamicBone[ChaControlDefine.DynamicBoneKind.BreastR] = dynamicBone_Ver;
					}
				}
				else
				{
					this.DictDynamicBone[ChaControlDefine.DynamicBoneKind.BreastL] = dynamicBone_Ver;
				}
			}
		}
		this.ObjBodyBone = this.ObjAnim.transform.FindLoop("cf_J_Root");
		if (this.ObjBodyBone)
		{
			this._aaWeightsBody.CreateBoneListMultiple(this.ObjBodyBone, TestChara._boneNames);
			this.CreateReferenceInfo(1UL, this.ObjBodyBone);
			NeckLookControllerVer2[] componentsInChildren2 = this.ObjBodyBone.GetComponentsInChildren<NeckLookControllerVer2>(true);
			if (componentsInChildren2.Length != 0)
			{
				this.NeckLookControl = componentsInChildren2[0];
			}
			string anmKeyInfoName = "cf_anmShapeBody";
			string cateInfoName = "cf_custombody";
			Transform transform = this.ObjBodyBone.transform;
			this._shapeBody.InitShapeInfo(string.Empty, "list/customshape.unity3d", "list/customshape.unity3d", anmKeyInfoName, cateInfoName, transform);
			int num = ChaFileDefine.cf_bodyshapename.Length - 1;
			for (int j = 0; j < num; j++)
			{
				this._shapeBody.ChangeValue(j, 0.5f);
			}
			this._shapeBody.ChangeValue(0, 1f);
			this._shapeBody.ChangeValue(num, this._nipStand);
		}
		this.ObjAnim.SetActive(true);
		if (this._boneHead)
		{
			GameObject referenceInfo = this.GetReferenceInfo(CharReference.RefObjKey.HeadParent);
			this.ObjHeadBone = UnityEngine.Object.Instantiate<GameObject>(this._boneHead, referenceInfo.transform);
			this.ObjHeadBone.transform.localPosition = Vector3.zero;
			this.ObjHeadBone.transform.localRotation = Quaternion.identity;
			EyeLookController[] componentsInChildren3 = this.ObjHeadBone.GetComponentsInChildren<EyeLookController>(true);
			if (componentsInChildren3.Length != 0)
			{
				this.EyeLookControl = componentsInChildren3[0];
			}
			if (this.EyeLookControl)
			{
				EyeLookCalc component = this.EyeLookControl.GetComponent<EyeLookCalc>();
				if (component)
				{
					component.Init();
				}
			}
			this._aaWeightsHead.CreateBoneList(this.ObjHeadBone, "cf_J");
			this.ObjHeadBone.SetActive(true);
			if (this._body)
			{
				this.ObjBody = UnityEngine.Object.Instantiate<GameObject>(this._body, this.ObjTop.transform);
				this.ObjBody.transform.localPosition = Vector3.zero;
				this.ObjBody.transform.localRotation = Quaternion.identity;
				GameObject referenceInfo2 = this.GetReferenceInfo(CharReference.RefObjKey.A_ROOTBONE);
				Transform rootBone = (!referenceInfo2) ? null : referenceInfo2.transform;
				this._aaWeightsBody.AssignedWeights(this.ObjBody, "cf_J_Root", rootBone);
				Animator component2 = this.ObjBody.GetComponent<Animator>();
				UnityEngine.Object.Destroy(component2);
				FullBodyBipedIK component3 = this.ObjBody.GetComponent<FullBodyBipedIK>();
				UnityEngine.Object.Destroy(component3);
				DynamicBone[] components = this.ObjBody.GetComponents<DynamicBone>();
				foreach (DynamicBone obj in components)
				{
					UnityEngine.Object.Destroy(obj);
				}
				DynamicBone_Ver02[] components2 = this.ObjBody.GetComponents<DynamicBone_Ver02>();
				foreach (DynamicBone_Ver02 obj2 in components2)
				{
					UnityEngine.Object.Destroy(obj2);
				}
				DynamicBoneCollider[] componentsInChildren4 = this.ObjBody.GetComponentsInChildren<DynamicBoneCollider>();
				foreach (DynamicBoneCollider obj3 in componentsInChildren4)
				{
					UnityEngine.Object.Destroy(obj3);
				}
				this.ObjBody.SetActive(true);
			}
			string[] array5 = (this._sex != 0) ? TestChara._f_mozaicparts : TestChara._m_mozaicparts;
			for (int n = 0; n < array5.Length; n++)
			{
				GameObject gameObject = this.ObjBody.transform.FindLoop(array5[n]);
				if (!(gameObject == null))
				{
					this._objMozaicList.Add(gameObject);
				}
			}
			foreach (GameObject gameObject2 in this._objMozaicList)
			{
				if (gameObject2 != null)
				{
					gameObject2.SetActive(false);
				}
			}
			if (this._head)
			{
				this.ObjHead = UnityEngine.Object.Instantiate<GameObject>(this._head, Vector3.zero, Quaternion.identity, this.ObjHeadBone.transform);
				this.ObjHead.transform.SetParent(this.ObjHeadBone.transform, false);
				this.ObjHead.transform.localPosition = Vector3.zero;
				this.ObjHead.transform.localRotation = Quaternion.identity;
				GameObject gameObject3 = this.ObjHead.transform.FindLoop("o_head");
				if (gameObject3)
				{
					this.FaceRenderer = gameObject3.GetComponent<Renderer>();
					Renderer faceRenderer = this.FaceRenderer;
					this.FaceMaterial = ((faceRenderer != null) ? faceRenderer.material : null);
				}
				GameObject gameObject4 = this.ObjHead.transform.FindLoop("o_eyebase_L");
				if (gameObject4)
				{
					this.EyeRendererL = gameObject4.GetComponent<Renderer>();
					Renderer eyeRendererL = this.EyeRendererL;
					this.EyeLMaterial = ((eyeRendererL != null) ? eyeRendererL.material : null);
				}
				GameObject gameObject5 = this.ObjHead.transform.FindLoop("o_eyebase_R");
				if (gameObject5)
				{
					this.EyeRendererR = gameObject5.GetComponent<Renderer>();
					Renderer eyeRendererR = this.EyeRendererR;
					this.EyeRMaterial = ((eyeRendererR != null) ? eyeRendererR.material : null);
				}
				GameObject gameObject6 = this.ObjHead.transform.FindLoop("o_namida");
				if (gameObject6)
				{
					this.TearRenderer = gameObject6.GetComponent<Renderer>();
					Renderer tearRenderer = this.TearRenderer;
					this.TearMaterial = ((tearRenderer != null) ? tearRenderer.material : null);
				}
				EyeLookController[] componentsInChildren5 = this.ObjHead.GetComponentsInChildren<EyeLookController>();
				if (!componentsInChildren5.IsNullOrEmpty<EyeLookController>())
				{
					foreach (EyeLookController eyeLookController in componentsInChildren5)
					{
						EyeLookCalc component4 = eyeLookController.GetComponent<EyeLookCalc>();
						if (component4)
						{
							UnityEngine.Object.Destroy(component4);
						}
						UnityEngine.Object.Destroy(eyeLookController);
					}
				}
				CommonLib.CopySameNameTransform(this.ObjHeadBone.transform, this.ObjHead.transform);
				this.ObjHead.transform.SetParent(this.ObjHeadBone.transform, false);
				this._aaWeightsHead.AssignedWeights(this.ObjHead, "cf_J_FaceRoot", null);
				this.FBSCtrl = this.ObjHead.GetComponent<FaceBlendShape>();
				if (this.FBSCtrl != null)
				{
					this.EyebrowCtrl = this.FBSCtrl.EyebrowCtrl;
					this.EyesCtrl = this.FBSCtrl.EyesCtrl;
					this.MouthCtrl = this.FBSCtrl.MouthCtrl;
					this.ChangeEyebrowPtn(0, true);
					this.ChangeEyesPtn(0, true);
					this.ChangeMouthPtn(0, true);
				}
				this.CreateReferenceInfo(3UL, this.ObjHeadBone);
				this.CreateTagInfo(3UL, this.ObjHeadBone);
				string shapeFaceName = this._shapeFaceName;
				string cateInfoName2 = "cf_customhead";
				this._shapeFace.InitShapeInfo(string.Empty, "chara/cf_head_00.unity3d", "list/customshape.unity3d", shapeFaceName, cateInfoName2, this.ObjHeadBone.transform);
				for (int num3 = 0; num3 < ChaFileDefine.cf_headshapename.Length; num3++)
				{
					this._shapeFace.ChangeValue(num3, 0.5f);
				}
				this.ObjHead.SetActive(true);
			}
			DynamicBoneCollider[] componentsInChildren6 = this.ObjAnim.GetComponentsInChildren<DynamicBoneCollider>(true);
			GameObject referenceInfo3 = this.GetReferenceInfo(CharReference.RefObjKey.HairParent);
			try
			{
				if (this._hairBack)
				{
					this.ObjHairBack = UnityEngine.Object.Instantiate<GameObject>(this._hairBack, referenceInfo3.transform);
					this.ObjHairBack.transform.localPosition = Vector3.zero;
					this.ObjHairBack.transform.localRotation = Quaternion.identity;
					Dictionary<string, GameObject> dictBone = this._aaWeightsBody.dictBone;
					DynamicBone[] componentsInChildren7 = this.ObjHairBack.GetComponentsInChildren<DynamicBone>(true);
					foreach (DynamicBone dynamicBone in componentsInChildren7)
					{
						if (dynamicBone.m_Colliders != null)
						{
							dynamicBone.m_Colliders.Clear();
							for (int num5 = 0; num5 < componentsInChildren6.Length; num5++)
							{
								dynamicBone.m_Colliders.Add(componentsInChildren6[num5]);
							}
						}
					}
					DynamicBone_Ver01[] componentsInChildren8 = this.ObjHairBack.GetComponentsInChildren<DynamicBone_Ver01>(true);
					foreach (DynamicBone_Ver01 dynamicBone_Ver2 in componentsInChildren8)
					{
						if (dynamicBone_Ver2.m_Colliders != null)
						{
							dynamicBone_Ver2.m_Colliders.Clear();
							for (int num7 = 0; num7 < componentsInChildren6.Length; num7++)
							{
								dynamicBone_Ver2.m_Colliders.Add(componentsInChildren6[num7]);
							}
						}
					}
				}
				if (this._hairFront)
				{
					this.ObjHairFront = UnityEngine.Object.Instantiate<GameObject>(this._hairFront, referenceInfo3.transform);
					this.ObjHairFront.transform.localPosition = Vector3.zero;
					this.ObjHairFront.transform.localRotation = Quaternion.identity;
					Dictionary<string, GameObject> dictBone2 = this._aaWeightsBody.dictBone;
					DynamicBone[] componentsInChildren9 = this.ObjHairFront.GetComponentsInChildren<DynamicBone>(true);
					foreach (DynamicBone dynamicBone2 in componentsInChildren9)
					{
						if (dynamicBone2.m_Colliders != null)
						{
							dynamicBone2.m_Colliders.Clear();
							for (int num9 = 0; num9 < componentsInChildren6.Length; num9++)
							{
								dynamicBone2.m_Colliders.Add(componentsInChildren6[num9]);
							}
						}
					}
					DynamicBone_Ver01[] componentsInChildren10 = this.ObjHairFront.GetComponentsInChildren<DynamicBone_Ver01>(true);
					foreach (DynamicBone_Ver01 dynamicBone_Ver3 in componentsInChildren10)
					{
						if (dynamicBone_Ver3.m_Colliders != null)
						{
							dynamicBone_Ver3.m_Colliders.Clear();
							for (int num11 = 0; num11 < componentsInChildren6.Length; num11++)
							{
								dynamicBone_Ver3.m_Colliders.Add(componentsInChildren6[num11]);
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
			}
			if (this._clothTop)
			{
				this.ObjClothTop = UnityEngine.Object.Instantiate<GameObject>(this._clothTop, this.ObjTop.transform);
				this.ObjClothTop.transform.localPosition = Vector3.zero;
				this.ObjClothTop.transform.localRotation = Quaternion.identity;
				GameObject referenceInfo4 = this.GetReferenceInfo(CharReference.RefObjKey.A_ROOTBONE);
				Transform rootBone2 = (!referenceInfo4) ? null : referenceInfo4.transform;
				this._aaWeightsBody.AssignedWeights(this.ObjClothTop, "cf_J_Root", rootBone2);
				Dictionary<string, GameObject> dictBone3 = this._aaWeightsBody.dictBone;
				DynamicBone[] componentsInChildren11 = this.ObjClothTop.GetComponentsInChildren<DynamicBone>(true);
				foreach (DynamicBone dynamicBone3 in componentsInChildren11)
				{
					if (dynamicBone3.m_Root)
					{
						foreach (KeyValuePair<string, GameObject> keyValuePair in dictBone3)
						{
							if (!(keyValuePair.Key != dynamicBone3.m_Root.name))
							{
								dynamicBone3.m_Root = keyValuePair.Value.transform;
								break;
							}
						}
					}
					if (dynamicBone3.m_Exclusions != null && dynamicBone3.m_Exclusions.Count != 0)
					{
						for (int num13 = 0; num13 < dynamicBone3.m_Exclusions.Count; num13++)
						{
							if (!(dynamicBone3.m_Exclusions[num13] == null))
							{
								foreach (KeyValuePair<string, GameObject> keyValuePair2 in dictBone3)
								{
									if (!(keyValuePair2.Key != dynamicBone3.m_Exclusions[num13].name))
									{
										dynamicBone3.m_Exclusions[num13] = keyValuePair2.Value.transform;
										break;
									}
								}
							}
						}
					}
					if (dynamicBone3.m_notRolls != null && dynamicBone3.m_notRolls.Count != 0)
					{
						for (int num14 = 0; num14 < dynamicBone3.m_notRolls.Count; num14++)
						{
							if (!(dynamicBone3.m_notRolls[num14] == null))
							{
								foreach (KeyValuePair<string, GameObject> keyValuePair3 in dictBone3)
								{
									if (!(keyValuePair3.Key != dynamicBone3.m_notRolls[num14].name))
									{
										dynamicBone3.m_notRolls[num14] = keyValuePair3.Value.transform;
										break;
									}
								}
							}
						}
					}
					if (dynamicBone3.m_Colliders != null)
					{
						dynamicBone3.m_Colliders.Clear();
						for (int num15 = 0; num15 < componentsInChildren6.Length; num15++)
						{
							dynamicBone3.m_Colliders.Add(componentsInChildren6[num15]);
						}
					}
				}
				this.ObjClothTop.SetActive(true);
			}
			if (this._clothBot)
			{
				this.ObjClothBot = UnityEngine.Object.Instantiate<GameObject>(this._clothBot, this.ObjTop.transform);
				this.ObjClothBot.transform.localPosition = Vector3.zero;
				this.ObjClothBot.transform.localRotation = Quaternion.identity;
				GameObject referenceInfo5 = this.GetReferenceInfo(CharReference.RefObjKey.A_ROOTBONE);
				Transform rootBone3 = (!referenceInfo5) ? null : referenceInfo5.transform;
				this._aaWeightsBody.AssignedWeights(this.ObjClothBot, "cf_J_Root", rootBone3);
				Dictionary<string, GameObject> dictBone4 = this._aaWeightsBody.dictBone;
				DynamicBone[] componentsInChildren12 = this.ObjClothBot.GetComponentsInChildren<DynamicBone>(true);
				foreach (DynamicBone dynamicBone4 in componentsInChildren12)
				{
					if (dynamicBone4.m_Root)
					{
						foreach (KeyValuePair<string, GameObject> keyValuePair4 in dictBone4)
						{
							if (!(keyValuePair4.Key != dynamicBone4.m_Root.name))
							{
								dynamicBone4.m_Root = keyValuePair4.Value.transform;
								break;
							}
						}
					}
					if (dynamicBone4.m_Exclusions != null && dynamicBone4.m_Exclusions.Count != 0)
					{
						for (int num17 = 0; num17 < dynamicBone4.m_Exclusions.Count; num17++)
						{
							if (!(dynamicBone4.m_Exclusions[num17] == null))
							{
								foreach (KeyValuePair<string, GameObject> keyValuePair5 in dictBone4)
								{
									if (!(keyValuePair5.Key != dynamicBone4.m_Exclusions[num17].name))
									{
										dynamicBone4.m_Exclusions[num17] = keyValuePair5.Value.transform;
										break;
									}
								}
							}
						}
					}
					if (dynamicBone4.m_notRolls != null && dynamicBone4.m_notRolls.Count != 0)
					{
						for (int num18 = 0; num18 < dynamicBone4.m_notRolls.Count; num18++)
						{
							if (!(dynamicBone4.m_notRolls[num18] == null))
							{
								foreach (KeyValuePair<string, GameObject> keyValuePair6 in dictBone4)
								{
									if (!(keyValuePair6.Key != dynamicBone4.m_notRolls[num18].name))
									{
										dynamicBone4.m_notRolls[num18] = keyValuePair6.Value.transform;
										break;
									}
								}
							}
						}
					}
					if (dynamicBone4.m_Colliders != null)
					{
						dynamicBone4.m_Colliders.Clear();
						for (int num19 = 0; num19 < componentsInChildren6.Length; num19++)
						{
							dynamicBone4.m_Colliders.Add(componentsInChildren6[num19]);
						}
					}
				}
				this.ObjClothBot.SetActive(true);
			}
			if (this._bra)
			{
				this.ObjBra = UnityEngine.Object.Instantiate<GameObject>(this._bra, this.ObjTop.transform);
				this.ObjBra.transform.localPosition = Vector3.zero;
				this.ObjBra.transform.localRotation = Quaternion.identity;
				GameObject referenceInfo6 = this.GetReferenceInfo(CharReference.RefObjKey.A_ROOTBONE);
				Transform rootBone4 = (!referenceInfo6) ? null : referenceInfo6.transform;
				this._aaWeightsBody.AssignedWeights(this.ObjBra, "cf_J_Root", rootBone4);
			}
			if (this._shorts)
			{
				this.ObjShorts = UnityEngine.Object.Instantiate<GameObject>(this._shorts, this.ObjTop.transform);
				this.ObjShorts.transform.localPosition = Vector3.zero;
				this.ObjShorts.transform.localRotation = Quaternion.identity;
				GameObject referenceInfo7 = this.GetReferenceInfo(CharReference.RefObjKey.A_ROOTBONE);
				Transform rootBone5 = (!referenceInfo7) ? null : referenceInfo7.transform;
				this._aaWeightsBody.AssignedWeights(this.ObjShorts, "cf_J_Root", rootBone5);
				this.ObjShorts.SetActive(true);
			}
			if (this._glove)
			{
				this.ObjGlove = UnityEngine.Object.Instantiate<GameObject>(this._glove, this.ObjTop.transform);
				this.ObjGlove.transform.localPosition = Vector3.zero;
				this.ObjGlove.transform.localRotation = Quaternion.identity;
				GameObject referenceInfo8 = this.GetReferenceInfo(CharReference.RefObjKey.A_ROOTBONE);
				Transform rootBone6 = (!referenceInfo8) ? null : referenceInfo8.transform;
				this._aaWeightsBody.AssignedWeights(this.ObjGlove, "cf_J_Root", rootBone6);
			}
			if (this._socks)
			{
				this.ObjSocks = UnityEngine.Object.Instantiate<GameObject>(this._socks, this.ObjTop.transform);
				this.ObjSocks.transform.localPosition = Vector3.zero;
				this.ObjSocks.transform.localRotation = Quaternion.identity;
				GameObject referenceInfo9 = this.GetReferenceInfo(CharReference.RefObjKey.A_ROOTBONE);
				Transform rootBone7 = (!referenceInfo9) ? null : referenceInfo9.transform;
				this._aaWeightsBody.AssignedWeights(this.ObjSocks, "cf_J_Root", rootBone7);
			}
			if (this._shoes)
			{
				this.ObjShoes = UnityEngine.Object.Instantiate<GameObject>(this._shoes, this.ObjTop.transform);
				this.ObjShoes.transform.localPosition = Vector3.zero;
				this.ObjShoes.transform.localRotation = Quaternion.identity;
				GameObject referenceInfo10 = this.GetReferenceInfo(CharReference.RefObjKey.A_ROOTBONE);
				Transform rootBone8 = (!referenceInfo10) ? null : referenceInfo10.transform;
				this._aaWeightsBody.AssignedWeights(this.ObjShoes, "cf_J_Root", rootBone8);
			}
			for (int num20 = 0; num20 < this._acsArray.Length; num20++)
			{
				TestChara.AcsGenerateInfo acsGenerateInfo = this._acsArray[num20];
				if (!(acsGenerateInfo.ACSObj == null))
				{
					GameObject referenceInfo_New = this.GetReferenceInfo_New(acsGenerateInfo.Key);
					if (!(referenceInfo_New == null))
					{
						GameObject gameObject7 = this.ObjAcsArray[num20] = UnityEngine.Object.Instantiate<GameObject>(acsGenerateInfo.ACSObj, referenceInfo_New.transform);
						gameObject7.transform.localPosition = Vector3.zero;
						gameObject7.transform.localRotation = Quaternion.identity;
					}
				}
			}
			Resources.UnloadUnusedAssets();
			GC.Collect();
			this.LoadEnd = true;
			for (int num21 = 0; num21 < this._shapeValuesFace.Length; num21++)
			{
				this._shapeValuesFace[num21] = 0.5f;
			}
			for (int num22 = 0; num22 < this._shapeValues.Length; num22++)
			{
				this._shapeValues[num22] = 0.5f;
			}
			this._shapeValues[0] = 1f;
			this._animBody.runtimeAnimatorController = this._rac;
			if (this._animBody.runtimeAnimatorController != null)
			{
				RuntimeAnimatorController rac = this._animBody.runtimeAnimatorController;
				this._animBody.runtimeAnimatorController = null;
				Observable.NextFrame(FrameCountType.Update).Subscribe(delegate(Unit _)
				{
					this._animBody.runtimeAnimatorController = rac;
				});
			}
			this.NeckLookControl.ptnNo = 3;
			this.VisibleAll = true;
			return;
		}
	}

	// Token: 0x060070DC RID: 28892 RVA: 0x003020A0 File Offset: 0x003004A0
	private void CreateReferenceInfo(ulong flags, GameObject objRef)
	{
		if (objRef == null)
		{
			return;
		}
		FindAssist findAssist = new FindAssist();
		findAssist.Initialize(objRef.transform);
		if (flags != 3UL)
		{
			if (flags != 1UL)
			{
				if (flags == 8UL)
				{
					this._dictRefObj[35] = findAssist.GetObjectFromName("N_tang");
					this._dictRefObj[36] = findAssist.GetObjectFromName("N_mnpb");
					this._dictRefObj[37] = findAssist.GetObjectFromName("N_tikubi");
				}
			}
			else
			{
				this._dictRefObj[8] = findAssist.GetObjectFromName("N_Neck");
				this._dictRefObj[9] = findAssist.GetObjectFromName("N_Chest");
				this._dictRefObj[10] = findAssist.GetObjectFromName("N_Wrist_L");
				this._dictRefObj[11] = findAssist.GetObjectFromName("N_Wrist_R");
				this._dictRefObj[12] = findAssist.GetObjectFromName("N_Arm_L");
				this._dictRefObj[13] = findAssist.GetObjectFromName("N_Arm_R");
				this._dictRefObj[14] = findAssist.GetObjectFromName("N_Index_L");
				this._dictRefObj[15] = findAssist.GetObjectFromName("N_Index_R");
				this._dictRefObj[16] = findAssist.GetObjectFromName("N_Middle_L");
				this._dictRefObj[17] = findAssist.GetObjectFromName("N_Middle_R");
				this._dictRefObj[18] = findAssist.GetObjectFromName("N_Ring_L");
				this._dictRefObj[19] = findAssist.GetObjectFromName("N_Ring_R");
				this._dictRefObj[20] = findAssist.GetObjectFromName("N_Leg_L");
				this._dictRefObj[21] = findAssist.GetObjectFromName("N_Leg_R");
				this._dictRefObj[22] = findAssist.GetObjectFromName("N_Ankle_L");
				this._dictRefObj[23] = findAssist.GetObjectFromName("N_Ankle_R");
				this._dictRefObj[24] = findAssist.GetObjectFromName("N_Tikubi_L");
				this._dictRefObj[25] = findAssist.GetObjectFromName("N_Tikubi_R");
				this._dictRefObj[26] = findAssist.GetObjectFromName("N_Waist");
				this._dictRefObj[27] = findAssist.GetObjectFromName("N_Shoulder_L");
				this._dictRefObj[28] = findAssist.GetObjectFromName("N_Shoulder_R");
				this._dictRefObj[29] = findAssist.GetObjectFromName("N_Hand_L");
				this._dictRefObj[30] = findAssist.GetObjectFromName("N_Hand_R");
				this._dictRefObj[42] = findAssist.GetObjectFromName("cf_J_Hips");
				this._dictRefObj[0] = findAssist.GetObjectFromName("cf_J_Head_s");
				this._dictRefObj[32] = findAssist.GetObjectFromName("cf_J_Kokan_dam");
				this._dictRefObjNew[15] = findAssist.GetObjectFromName("N_Neck");
				this._dictRefObjNew[16] = findAssist.GetObjectFromName("N_Chest_f");
				this._dictRefObjNew[17] = findAssist.GetObjectFromName("N_Chest");
				this._dictRefObjNew[18] = findAssist.GetObjectFromName("N_Tikubi_L");
				this._dictRefObjNew[19] = findAssist.GetObjectFromName("N_Tikubi_R");
				this._dictRefObjNew[20] = findAssist.GetObjectFromName("N_Back");
				this._dictRefObjNew[21] = findAssist.GetObjectFromName("N_Back_L");
				this._dictRefObjNew[22] = findAssist.GetObjectFromName("N_Back_R");
				this._dictRefObjNew[23] = findAssist.GetObjectFromName("N_Waist");
				this._dictRefObjNew[24] = findAssist.GetObjectFromName("N_Waist_f");
				this._dictRefObjNew[25] = findAssist.GetObjectFromName("N_Waist_b");
				this._dictRefObjNew[26] = findAssist.GetObjectFromName("N_Waist_L");
				this._dictRefObjNew[27] = findAssist.GetObjectFromName("N_Waist_R");
				this._dictRefObjNew[28] = findAssist.GetObjectFromName("N_Leg_L");
				this._dictRefObjNew[29] = findAssist.GetObjectFromName("N_Leg_R");
				this._dictRefObjNew[30] = findAssist.GetObjectFromName("N_Knee_L");
				this._dictRefObjNew[31] = findAssist.GetObjectFromName("N_Knee_R");
				this._dictRefObjNew[32] = findAssist.GetObjectFromName("N_Ankle_L");
				this._dictRefObjNew[33] = findAssist.GetObjectFromName("N_Ankle_R");
				this._dictRefObjNew[34] = findAssist.GetObjectFromName("N_Foot_L");
				this._dictRefObjNew[35] = findAssist.GetObjectFromName("N_Foot_R");
				this._dictRefObjNew[36] = findAssist.GetObjectFromName("N_Shoulder_L");
				this._dictRefObjNew[37] = findAssist.GetObjectFromName("N_Shoulder_R");
				this._dictRefObjNew[38] = findAssist.GetObjectFromName("N_Elbo_L");
				this._dictRefObjNew[39] = findAssist.GetObjectFromName("N_Elbo_R");
				this._dictRefObjNew[40] = findAssist.GetObjectFromName("N_Arm_L");
				this._dictRefObjNew[41] = findAssist.GetObjectFromName("N_Arm_R");
				this._dictRefObjNew[42] = findAssist.GetObjectFromName("N_Wrist_L");
				this._dictRefObjNew[43] = findAssist.GetObjectFromName("N_Wrist_R");
				this._dictRefObjNew[44] = findAssist.GetObjectFromName("N_Hand_L");
				this._dictRefObjNew[45] = findAssist.GetObjectFromName("N_Hand_R");
				this._dictRefObjNew[46] = findAssist.GetObjectFromName("N_Index_L");
				this._dictRefObjNew[47] = findAssist.GetObjectFromName("N_Index_R");
				this._dictRefObjNew[48] = findAssist.GetObjectFromName("N_Middle_L");
				this._dictRefObjNew[49] = findAssist.GetObjectFromName("N_Middle_R");
				this._dictRefObjNew[50] = findAssist.GetObjectFromName("N_Ring_L");
				this._dictRefObjNew[51] = findAssist.GetObjectFromName("N_Ring_R");
				this._dictRefObjNew[52] = findAssist.GetObjectFromName("N_Dan");
				this._dictRefObjNew[53] = findAssist.GetObjectFromName("N_Kokan");
				this._dictRefObjNew[54] = findAssist.GetObjectFromName("N_Ana");
				this._dictRefObjNew[0] = findAssist.GetObjectFromName("cf_J_Head_s");
			}
		}
		else
		{
			this._dictRefObj[2] = findAssist.GetObjectFromName("N_Head");
			this._dictRefObj[3] = findAssist.GetObjectFromName("N_Megane");
			this._dictRefObj[4] = findAssist.GetObjectFromName("N_Earring_L");
			this._dictRefObj[5] = findAssist.GetObjectFromName("N_Earring_R");
			this._dictRefObj[6] = findAssist.GetObjectFromName("N_Mouth");
			this._dictRefObj[7] = findAssist.GetObjectFromName("N_Nose");
			this._dictRefObj[41] = findAssist.GetObjectFromName("cf_J_MouthMove");
			this._dictRefObj[1] = findAssist.GetObjectFromName("N_hair_Root");
			this._dictRefObj[34] = findAssist.GetObjectFromName("cf_O_sita");
			this._dictRefObj[38] = findAssist.GetObjectFromName("cf_O_namida01");
			this._dictRefObj[39] = findAssist.GetObjectFromName("cf_O_namida02");
			this._dictRefObj[40] = findAssist.GetObjectFromName("cf_O_namida03");
			this._dictRefObjNew[7] = findAssist.GetObjectFromName("N_Head");
			this._dictRefObjNew[10] = findAssist.GetObjectFromName("N_Megane");
			this._dictRefObjNew[11] = findAssist.GetObjectFromName("N_Earring_L");
			this._dictRefObjNew[12] = findAssist.GetObjectFromName("N_Earring_R");
			this._dictRefObjNew[14] = findAssist.GetObjectFromName("N_Mouth");
			this._dictRefObjNew[13] = findAssist.GetObjectFromName("N_Nose");
			this._dictRefObjNew[1] = findAssist.GetObjectFromName("N_hair_Root");
			this._dictRefObjNew[2] = findAssist.GetObjectFromName("N_Hair_twin_L");
			this._dictRefObjNew[3] = findAssist.GetObjectFromName("N_Hair_twin_R");
			this._dictRefObjNew[4] = findAssist.GetObjectFromName("N_Hair_pin_L");
			this._dictRefObjNew[5] = findAssist.GetObjectFromName("N_Hair_pin_R");
			this._dictRefObjNew[6] = findAssist.GetObjectFromName("N_Head_top");
			this._dictRefObjNew[8] = findAssist.GetObjectFromName("N_Hitai");
			this._dictRefObjNew[9] = findAssist.GetObjectFromName("N_Face");
		}
	}

	// Token: 0x060070DD RID: 28893 RVA: 0x003029E8 File Offset: 0x00300DE8
	public GameObject GetReferenceInfo(CharReference.RefObjKey key)
	{
		GameObject result;
		this._dictRefObj.TryGetValue((int)key, out result);
		return result;
	}

	// Token: 0x060070DE RID: 28894 RVA: 0x00302A08 File Offset: 0x00300E08
	public GameObject GetReferenceInfo_New(CharReference.RefObjKey_New key)
	{
		GameObject result;
		this._dictRefObjNew.TryGetValue((int)key, out result);
		return result;
	}

	// Token: 0x060070DF RID: 28895 RVA: 0x00302A28 File Offset: 0x00300E28
	public float GetShapeFaceValue(int index)
	{
		int num = ChaFileDefine.cf_headshapename.Length;
		if (index >= num)
		{
			return 0f;
		}
		return this._shapeValuesFace[index];
	}

	// Token: 0x060070E0 RID: 28896 RVA: 0x00302A54 File Offset: 0x00300E54
	public bool SetShapeFaceValue(int index, float value)
	{
		int num = ChaFileDefine.cf_headshapename.Length;
		if (index >= num)
		{
			return false;
		}
		this._shapeValuesFace[index] = Mathf.Clamp(value, 0f, 1f);
		if (this._shapeFace != null && this._shapeFace.InitEnd)
		{
			this._shapeFace.ChangeValue(index, value);
		}
		this._updateShapeFace = true;
		return true;
	}

	// Token: 0x060070E1 RID: 28897 RVA: 0x00302ABC File Offset: 0x00300EBC
	public void UpdateShapeFace()
	{
		if (this._shapeFace == null || !this._shapeFace.InitEnd)
		{
			return;
		}
		int[] array = new int[]
		{
			55,
			56,
			57,
			58
		};
		if (this._disableShapeMouth)
		{
			for (int i = 0; i < array.Length; i++)
			{
				this._shapeFace.ChangeValue(array[i], 0.5f);
			}
		}
		else
		{
			for (int j = 0; j < array.Length; j++)
			{
				this._shapeFace.ChangeValue(array[j], this._shapeValuesFace[array[j]]);
			}
		}
		this._shapeFace.Update();
	}

	// Token: 0x060070E2 RID: 28898 RVA: 0x00302B68 File Offset: 0x00300F68
	public void UpdateShapeBody()
	{
		if (this._shapeBody == null || !this._shapeBody.InitEnd)
		{
			return;
		}
		this._shapeBody.updateMask = 31;
		this._shapeBody.Update();
		if (this._disableShapeNipL)
		{
			this._shapeBody.ChangeValue(ChaFileDefine.cf_bodyshapename.Length - 1, 0f);
			this._shapeBody.updateMask = 4;
			this._shapeBody.Update();
		}
		if (this._disableShapeNipR)
		{
			this._shapeBody.ChangeValue(ChaFileDefine.cf_bodyshapename.Length - 1, 0f);
			this._shapeBody.updateMask = 8;
			this._shapeBody.Update();
		}
		for (int i = 0; i < this._disableShapeBustLAry.Length; i++)
		{
			int num = ChaFileDefine.cf_BustShapeMaskID[i];
			float value = (!this._disableShapeBustLAry[i]) ? this._shapeValues[num] : this._defaultBustValue[i];
			this._shapeBody.ChangeValue(num, value);
		}
		this._shapeBody.updateMask = 1;
		this._shapeBody.Update();
		for (int j = 0; j < this._disableShapeBustRAry.Length; j++)
		{
			int num2 = ChaFileDefine.cf_BustShapeMaskID[j];
			float value2 = (!this._disableShapeBustRAry[j]) ? this._shapeValues[num2] : this._defaultBustValue[j];
			this._shapeBody.ChangeValue(num2, value2);
		}
		this._shapeBody.updateMask = 2;
		this._shapeBody.Update();
	}

	// Token: 0x060070E3 RID: 28899 RVA: 0x00302CF8 File Offset: 0x003010F8
	public float GetShapeBodyValue(int index)
	{
		int num = ChaFileDefine.cf_bodyshapename.Length;
		if (index >= num)
		{
			return 0f;
		}
		if (index == num - 1)
		{
			return this._nipStand;
		}
		return this._shapeValues[index];
	}

	// Token: 0x060070E4 RID: 28900 RVA: 0x00302D34 File Offset: 0x00301134
	public bool SetShapeBodyValue(int index, float value)
	{
		int num = ChaFileDefine.cf_bodyshapename.Length;
		if (index >= num)
		{
			return false;
		}
		if (index == num - 1)
		{
			this._nipStand = value;
		}
		else
		{
			this._shapeValues[index] = Mathf.Clamp(value, 0f, 1f);
		}
		if (this._shapeBody != null && this._shapeBody.InitEnd)
		{
			this._shapeBody.ChangeValue(index, value);
		}
		this._updateShapeBody = true;
		return true;
	}

	// Token: 0x060070E5 RID: 28901 RVA: 0x00302DB0 File Offset: 0x003011B0
	public void ForceUpdate()
	{
		this.UpdateVisible();
	}

	// Token: 0x060070E6 RID: 28902 RVA: 0x00302DB8 File Offset: 0x003011B8
	public void Release()
	{
		if (this.ObjTop)
		{
			UnityEngine.Object.Destroy(this.ObjTop);
		}
		this.ReleaseSub();
		this.ReleaseTagAll();
		this.NeckLookControl = null;
		this.EyeLookControl = null;
		this.FBSCtrl = null;
		this.EyesCtrl = null;
		this.MouthCtrl = null;
		this.ObjTop = null;
		this.ObjAnim = null;
		this.ObjBodyBone = null;
		this.ObjBody = null;
		this.ObjHeadBone = null;
		this.ObjHead = null;
	}

	// Token: 0x060070E7 RID: 28903 RVA: 0x00302E3C File Offset: 0x0030123C
	private void ReleaseSub()
	{
		this.DictDynamicBone = null;
		for (int i = 0; i < this.SiruNewLv.Length; i++)
		{
			this.SiruNewLv[i] = 0;
		}
		UnityEngine.Object.Destroy(this.FaceMaterial);
		this.FaceMaterial = null;
	}

	// Token: 0x060070E8 RID: 28904 RVA: 0x00302E84 File Offset: 0x00301284
	private void UpdateVisible()
	{
		bool flag = false;
		if (YS_Assist.SetActiveControl(this.ObjBody, this.VisibleAll))
		{
			flag = true;
		}
		if (YS_Assist.SetActiveControl(this.ObjHead, this.VisibleAll))
		{
			flag = true;
		}
		if (YS_Assist.SetActiveControl(this.ObjHairBack, this.VisibleAll))
		{
			flag = true;
		}
		if (YS_Assist.SetActiveControl(this.ObjHairFront, this.VisibleAll))
		{
			flag = true;
		}
		if (YS_Assist.SetActiveControl(this.ObjClothTop, this.VisibleAll))
		{
			flag = true;
		}
		if (YS_Assist.SetActiveControl(this.ObjClothBot, this.VisibleAll))
		{
			flag = true;
		}
		if (YS_Assist.SetActiveControl(this.ObjBra, this.VisibleAll))
		{
			flag = true;
		}
		if (YS_Assist.SetActiveControl(this.ObjShorts, this.VisibleAll))
		{
			flag = true;
		}
		if (YS_Assist.SetActiveControl(this.ObjGlove, this.VisibleAll))
		{
			flag = true;
		}
		if (YS_Assist.SetActiveControl(this.ObjSocks, this.VisibleAll))
		{
			flag = true;
		}
		if (YS_Assist.SetActiveControl(this.ObjShoes, this.VisibleAll))
		{
			flag = true;
		}
		foreach (GameObject obj in this.ObjAcsArray)
		{
			if (YS_Assist.SetActiveControl(obj, this.VisibleAll))
			{
				flag = true;
			}
		}
		if (flag)
		{
		}
	}

	// Token: 0x060070E9 RID: 28905 RVA: 0x00302FD4 File Offset: 0x003013D4
	private void Update()
	{
		this.ForceUpdate();
		if (!this._applySelfAnimatorParameter)
		{
			return;
		}
		if (this._animBody == null)
		{
			return;
		}
		if (this._animBody.runtimeAnimatorController == null)
		{
			return;
		}
		float shapeBodyValue = this.GetShapeBodyValue(0);
		float shapeBodyValue2 = this.GetShapeBodyValue(1);
		foreach (AnimatorControllerParameter animatorControllerParameter in this._animBody.parameters)
		{
			string text = animatorControllerParameter.name.ToLower();
			if (text != null)
			{
				if (!(text == "height"))
				{
					if (text == "breast")
					{
						if (animatorControllerParameter.type == AnimatorControllerParameterType.Float)
						{
							this._animBody.SetFloat(animatorControllerParameter.name, shapeBodyValue2);
						}
					}
				}
				else if (animatorControllerParameter.type == AnimatorControllerParameterType.Float)
				{
					this._animBody.SetFloat(animatorControllerParameter.name, shapeBodyValue);
				}
			}
		}
	}

	// Token: 0x060070EA RID: 28906 RVA: 0x003030D6 File Offset: 0x003014D6
	private void UpdateBustSoftnessAndGravity()
	{
	}

	// Token: 0x060070EB RID: 28907 RVA: 0x003030D8 File Offset: 0x003014D8
	private void ForceLateUpdate()
	{
		if (!this.LoadEnd)
		{
			return;
		}
		if (this._updateShapeFace)
		{
			this.UpdateShapeFace();
			this._updateShapeFace = false;
		}
		if (this._updateShapeBody)
		{
			this.UpdateShapeBody();
			this._updateShapeBody = false;
		}
		this.LateUpdateSub();
		if (this.ResetupDynamicBone)
		{
			this.ResetDynamicBone();
			this.ResetupDynamicBone = false;
		}
	}

	// Token: 0x060070EC RID: 28908 RVA: 0x0030313F File Offset: 0x0030153F
	private void LateUpdateSub()
	{
		if (this.ResetupDynamicBoneBust)
		{
			this.ResetupDynamicBones();
			this.UpdateBustSoftnessAndGravity();
			this.ResetupDynamicBoneBust = false;
		}
		if (this.UpdateBustSize)
		{
			this.UpdateBustSize = false;
		}
	}

	// Token: 0x060070ED RID: 28909 RVA: 0x00303174 File Offset: 0x00301574
	private void LateUpdate()
	{
		if (this.ObjAnim == null || this.ObjHeadBone == null || this.ObjBody == null || this.ObjHead == null)
		{
			return;
		}
		this.ForceLateUpdate();
	}

	// Token: 0x060070EE RID: 28910 RVA: 0x003031CC File Offset: 0x003015CC
	public void AnimPlay(string stateName)
	{
		if (this.AnimBody == null)
		{
			return;
		}
		this.AnimBody.Play(stateName);
	}

	// Token: 0x060070EF RID: 28911 RVA: 0x003031EC File Offset: 0x003015EC
	public AnimatorStateInfo GetAnimatorStateInfo(int layer)
	{
		if (this.AnimBody == null || this.AnimBody.runtimeAnimatorController == null)
		{
			return default(AnimatorStateInfo);
		}
		return this.AnimBody.GetCurrentAnimatorStateInfo(layer);
	}

	// Token: 0x060070F0 RID: 28912 RVA: 0x00303238 File Offset: 0x00301638
	public bool PlaySync(AnimatorStateInfo syncState, int layer)
	{
		if (this.AnimBody == null || this.AnimBody.runtimeAnimatorController == null)
		{
			return false;
		}
		this.AnimBody.Play(syncState.shortNameHash, layer, syncState.normalizedTime);
		return true;
	}

	// Token: 0x060070F1 RID: 28913 RVA: 0x00303289 File Offset: 0x00301689
	public bool PlaySync(int nameHash, int layer, float normalizedTime)
	{
		if (this.AnimBody == null || this.AnimBody.runtimeAnimatorController == null)
		{
			return false;
		}
		this.AnimBody.Play(nameHash, layer, normalizedTime);
		return true;
	}

	// Token: 0x060070F2 RID: 28914 RVA: 0x003032C3 File Offset: 0x003016C3
	public bool PlaySync(string stateName, int layer, float normalizedTime)
	{
		if (this.AnimBody == null || this.AnimBody.runtimeAnimatorController == null)
		{
			return false;
		}
		this.AnimBody.Play(stateName, layer, normalizedTime);
		return true;
	}

	// Token: 0x060070F3 RID: 28915 RVA: 0x003032FD File Offset: 0x003016FD
	public bool SetLayerWeight(float weight, int layer)
	{
		if (this.AnimBody == null || this.AnimBody.runtimeAnimatorController == null)
		{
			return false;
		}
		this.AnimBody.SetLayerWeight(layer, weight);
		return true;
	}

	// Token: 0x060070F4 RID: 28916 RVA: 0x00303338 File Offset: 0x00301738
	public bool SetAllLayerWeight(float weight)
	{
		if (this.AnimBody == null || this.AnimBody.runtimeAnimatorController == null)
		{
			return false;
		}
		for (int i = 1; i < this.AnimBody.layerCount; i++)
		{
			this.AnimBody.SetLayerWeight(i, weight);
		}
		return true;
	}

	// Token: 0x060070F5 RID: 28917 RVA: 0x00303398 File Offset: 0x00301798
	public float GetLayerWeight(int layer)
	{
		if (this.AnimBody == null || this.AnimBody.runtimeAnimatorController == null)
		{
			return 0f;
		}
		return this.AnimBody.GetLayerWeight(layer);
	}

	// Token: 0x060070F6 RID: 28918 RVA: 0x003033D4 File Offset: 0x003017D4
	public bool InitDynamicBone()
	{
		foreach (KeyValuePair<ChaControlDefine.DynamicBoneKind, DynamicBone_Ver02> keyValuePair in this.DictDynamicBone)
		{
			if (keyValuePair.Value != null)
			{
				keyValuePair.Value.ResetParticlesPosition();
			}
		}
		return true;
	}

	// Token: 0x060070F7 RID: 28919 RVA: 0x00303448 File Offset: 0x00301848
	public void ResetDynamicBone()
	{
		DynamicBone[] componentsInChildren = this.ObjTop.GetComponentsInChildren<DynamicBone>(true);
		foreach (DynamicBone dynamicBone in componentsInChildren)
		{
			dynamicBone.ResetParticlesPosition();
		}
	}

	// Token: 0x060070F8 RID: 28920 RVA: 0x00303484 File Offset: 0x00301884
	private bool ResetupDynamicBones()
	{
		DynamicBone_Ver02 dynamicBone_Ver = null;
		if (this.DictDynamicBone.TryGetValue(ChaControlDefine.DynamicBoneKind.BreastL, out dynamicBone_Ver))
		{
			dynamicBone_Ver.ResetPosition();
		}
		if (this.DictDynamicBone.TryGetValue(ChaControlDefine.DynamicBoneKind.BreastR, out dynamicBone_Ver))
		{
			dynamicBone_Ver.ResetPosition();
		}
		if (this.DictDynamicBone.TryGetValue(ChaControlDefine.DynamicBoneKind.HipL, out dynamicBone_Ver))
		{
			dynamicBone_Ver.ResetPosition();
		}
		if (this.DictDynamicBone.TryGetValue(ChaControlDefine.DynamicBoneKind.HipR, out dynamicBone_Ver))
		{
			dynamicBone_Ver.ResetPosition();
		}
		return true;
	}

	// Token: 0x060070F9 RID: 28921 RVA: 0x003034F8 File Offset: 0x003018F8
	public bool PlayYure(int area, bool play)
	{
		DynamicBone_Ver02 dynamicBone_Ver = null;
		if (area == 0)
		{
			if (this.DictDynamicBone.TryGetValue(ChaControlDefine.DynamicBoneKind.BreastL, out dynamicBone_Ver))
			{
				dynamicBone_Ver.enabled = play;
			}
			if (this.DictDynamicBone.TryGetValue(ChaControlDefine.DynamicBoneKind.BreastR, out dynamicBone_Ver))
			{
				dynamicBone_Ver.enabled = play;
			}
		}
		else
		{
			if (this.DictDynamicBone.TryGetValue(ChaControlDefine.DynamicBoneKind.HipL, out dynamicBone_Ver))
			{
				dynamicBone_Ver.enabled = play;
			}
			if (this.DictDynamicBone.TryGetValue(ChaControlDefine.DynamicBoneKind.HipR, out dynamicBone_Ver))
			{
				dynamicBone_Ver.enabled = play;
			}
		}
		return true;
	}

	// Token: 0x060070FA RID: 28922 RVA: 0x0030357C File Offset: 0x0030197C
	public bool PlayYure(ChaControlDefine.DynamicBoneKind area, bool play)
	{
		DynamicBone_Ver02 dynamicBone_Ver = null;
		if (this.DictDynamicBone.TryGetValue(area, out dynamicBone_Ver))
		{
			dynamicBone_Ver.enabled = play;
		}
		return true;
	}

	// Token: 0x060070FB RID: 28923 RVA: 0x003035A8 File Offset: 0x003019A8
	public DynamicBone_Ver02 GetDynamicBone(ChaControlDefine.DynamicBoneKind area)
	{
		DynamicBone_Ver02 result = null;
		this.DictDynamicBone.TryGetValue(area, out result);
		return result;
	}

	// Token: 0x060070FC RID: 28924 RVA: 0x003035C7 File Offset: 0x003019C7
	public bool IsBlend(int layer)
	{
		return !(this.AnimBody == null) && !(this.AnimBody.runtimeAnimatorController == null) && this.AnimBody.IsInTransition(layer);
	}

	// Token: 0x060070FD RID: 28925 RVA: 0x003035FE File Offset: 0x003019FE
	public Vector3 GetPosition()
	{
		return (!(this.ObjTop == null)) ? this.ObjTop.transform.localPosition : Vector3.zero;
	}

	// Token: 0x060070FE RID: 28926 RVA: 0x0030362B File Offset: 0x00301A2B
	public void SetPosition(float x, float y, float z)
	{
		this.ObjTop.transform.localPosition = new Vector3(x, y, z);
	}

	// Token: 0x060070FF RID: 28927 RVA: 0x00303645 File Offset: 0x00301A45
	public void SetPosition(Vector3 pos)
	{
		this.ObjTop.transform.localPosition = pos;
	}

	// Token: 0x06007100 RID: 28928 RVA: 0x00303658 File Offset: 0x00301A58
	public void SetRotation(float x, float y, float z)
	{
		if (this.ObjTop == null)
		{
			return;
		}
		this.ObjTop.transform.localEulerAngles = new Vector3(x, y, z);
	}

	// Token: 0x06007101 RID: 28929 RVA: 0x00303684 File Offset: 0x00301A84
	public void SetRotation(Vector3 rot)
	{
		if (this.ObjTop == null)
		{
			return;
		}
		this.ObjTop.transform.localEulerAngles = rot;
	}

	// Token: 0x06007102 RID: 28930 RVA: 0x003036A9 File Offset: 0x00301AA9
	public Vector3 GetRotation()
	{
		return (!(this.ObjTop == null)) ? this.ObjTop.transform.localEulerAngles : Vector3.zero;
	}

	// Token: 0x06007103 RID: 28931 RVA: 0x003036D8 File Offset: 0x00301AD8
	public void ChangeEyebrowPtn(int ptn, bool blend = true)
	{
		this.EyebrowPtn = ptn;
		FBSCtrlEyebrow eyebrowCtrl = this.EyebrowCtrl;
		if (eyebrowCtrl == null)
		{
			return;
		}
		eyebrowCtrl.ChangePtn(ptn, blend);
	}

	// Token: 0x06007104 RID: 28932 RVA: 0x00303704 File Offset: 0x00301B04
	public void ChangeEyebrowOpen(float value, bool fixedValue = false)
	{
		FBSCtrlEyebrow eyebrowCtrl = this.EyebrowCtrl;
		if (eyebrowCtrl == null)
		{
			return;
		}
		float num = Mathf.Clamp(value, 0f, 1f);
		if (fixedValue)
		{
			eyebrowCtrl.FixedRate = num;
		}
		else
		{
			eyebrowCtrl.FixedRate = -1f;
			eyebrowCtrl.OpenMax = num;
		}
	}

	// Token: 0x06007105 RID: 28933 RVA: 0x00303754 File Offset: 0x00301B54
	public void ChangeEyebrowOpenMin(float value)
	{
		if (this.EyebrowCtrl == null)
		{
			return;
		}
		this.EyebrowCtrl.OpenMin = value;
	}

	// Token: 0x06007106 RID: 28934 RVA: 0x0030377C File Offset: 0x00301B7C
	public void ChangeEyebrowOpenMax(float value)
	{
		FBSCtrlEyebrow eyebrowCtrl = this.EyebrowCtrl;
		if (eyebrowCtrl == null)
		{
			return;
		}
		eyebrowCtrl.OpenMax = value;
	}

	// Token: 0x06007107 RID: 28935 RVA: 0x003037A0 File Offset: 0x00301BA0
	public void HideEyeHighlight(bool hide)
	{
		this.HideEyesHighlight = hide;
		float value = (!hide) ? 1f : 0f;
		Material eyeLMaterial = this.EyeLMaterial;
		Material eyeRMaterial = this.EyeRMaterial;
		if (eyeLMaterial == null || eyeRMaterial == null)
		{
			return;
		}
		if (eyeLMaterial != null)
		{
			eyeLMaterial.SetFloat(TestChara._Smoothness, value);
		}
		if (eyeRMaterial != null)
		{
			eyeRMaterial.SetFloat(TestChara._Smoothness, value);
		}
	}

	// Token: 0x06007108 RID: 28936 RVA: 0x0030382C File Offset: 0x00301C2C
	public void ChangeEyesPtn(int ptn, bool blend = true)
	{
		this.EyesPtn = ptn;
		FBSCtrlEyes eyesCtrl = this.EyesCtrl;
		if (eyesCtrl == null)
		{
			return;
		}
		eyesCtrl.ChangePtn(ptn, blend);
	}

	// Token: 0x06007109 RID: 28937 RVA: 0x00303858 File Offset: 0x00301C58
	public void ChangeEyesOpen(float value, bool fixedValue = false)
	{
		FBSCtrlEyes eyesCtrl = this.EyesCtrl;
		if (eyesCtrl == null)
		{
			return;
		}
		float num = Mathf.Clamp(value, 0f, 1f);
		if (fixedValue)
		{
			eyesCtrl.FixedRate = num;
		}
		else
		{
			eyesCtrl.FixedRate = -1f;
			eyesCtrl.OpenMax = num;
		}
	}

	// Token: 0x0600710A RID: 28938 RVA: 0x003038A8 File Offset: 0x00301CA8
	public void ChangeEyesOpenMin(float value)
	{
		if (this.EyesCtrl == null)
		{
			return;
		}
		this.EyesCtrl.OpenMin = value;
	}

	// Token: 0x0600710B RID: 28939 RVA: 0x003038D0 File Offset: 0x00301CD0
	public void ChangeEyesOpenMax(float value)
	{
		FBSCtrlEyes eyesCtrl = this.EyesCtrl;
		if (eyesCtrl == null)
		{
			return;
		}
		eyesCtrl.OpenMax = value;
	}

	// Token: 0x0600710C RID: 28940 RVA: 0x003038F4 File Offset: 0x00301CF4
	public void ChangeBlinkFlag(bool blink)
	{
		this.EyesBlink = blink;
		FaceBlendShape fbsctrl = this.FBSCtrl;
		if (fbsctrl == null)
		{
			return;
		}
		if (fbsctrl.BlinkCtrl == null)
		{
			return;
		}
		this.EyesBlink = blink;
		fbsctrl.BlinkCtrl.SetFixedFlags((!blink) ? 1 : 0);
	}

	// Token: 0x0600710D RID: 28941 RVA: 0x00303948 File Offset: 0x00301D48
	public bool GetBlinkFlag()
	{
		return this.EyesBlink;
	}

	// Token: 0x0600710E RID: 28942 RVA: 0x00303950 File Offset: 0x00301D50
	public void ChangeMouthPtn(int ptn, bool blend = true)
	{
		this.MouthPtn = ptn;
		if (this.MouthCtrl == null)
		{
			return;
		}
		this.MouthCtrl.ChangePtn(ptn, blend);
		if (this.Sex == 1)
		{
			this.ChangeMouthPtnSubFemale(ptn, blend);
		}
		else
		{
			this.ChangeMouthPtnSubMale(ptn, blend);
		}
	}

	// Token: 0x0600710F RID: 28943 RVA: 0x003039A0 File Offset: 0x00301DA0
	public void ChangeMouthOpen(float value, bool fixedValue = false)
	{
		FBSCtrlMouth mouthCtrl = this.MouthCtrl;
		if (mouthCtrl == null)
		{
			return;
		}
		float num = Mathf.Clamp(value, 0f, 1f);
		if (fixedValue)
		{
			mouthCtrl.FixedRate = num;
		}
		else
		{
			mouthCtrl.OpenMax = num;
			mouthCtrl.FixedRate = -1f;
		}
	}

	// Token: 0x06007110 RID: 28944 RVA: 0x003039F0 File Offset: 0x00301DF0
	public void ChangeMouthOpenMin(float value)
	{
		FBSCtrlMouth mouthCtrl = this.MouthCtrl;
		if (mouthCtrl == null)
		{
			return;
		}
		mouthCtrl.OpenMin = value;
	}

	// Token: 0x06007111 RID: 28945 RVA: 0x00303A14 File Offset: 0x00301E14
	public void ChangeMouthOpenMax(float value)
	{
		FBSCtrlMouth mouthCtrl = this.MouthCtrl;
		if (mouthCtrl == null)
		{
			return;
		}
		mouthCtrl.OpenMax = value;
	}

	// Token: 0x06007112 RID: 28946 RVA: 0x00303A36 File Offset: 0x00301E36
	private void ChangeMouthPtnSubFemale(int ptn, bool blend)
	{
		if (ptn == 35 || ptn == 36)
		{
			this.ChangeTongueState(1);
		}
		else
		{
			this.ChangeTongueState(0);
		}
	}

	// Token: 0x06007113 RID: 28947 RVA: 0x00303A5B File Offset: 0x00301E5B
	private void ChangeMouthPtnSubMale(int ptn, bool blend)
	{
		if (ptn == 10)
		{
			this.ChangeTongueState(1);
		}
		else
		{
			this.ChangeTongueState(0);
		}
	}

	// Token: 0x06007114 RID: 28948 RVA: 0x00303A78 File Offset: 0x00301E78
	public void ChangeTongueState(byte state)
	{
	}

	// Token: 0x06007115 RID: 28949 RVA: 0x00303A7A File Offset: 0x00301E7A
	public void SetNipStand(float value)
	{
		this.SetShapeBodyValue(ChaFileDefine.cf_bodyshapename.Length - 1, value);
	}

	// Token: 0x06007116 RID: 28950 RVA: 0x00303A8D File Offset: 0x00301E8D
	public byte GetSiruFlags(ChaFileDefine.SiruParts parts)
	{
		return this.SiruNewLv[(int)parts];
	}

	// Token: 0x06007117 RID: 28951 RVA: 0x00303A97 File Offset: 0x00301E97
	public void SetSiruFlags(ChaFileDefine.SiruParts parts, byte lv)
	{
		this.SiruNewLv[(int)parts] = lv;
	}

	// Token: 0x06007118 RID: 28952 RVA: 0x00303AA2 File Offset: 0x00301EA2
	public void SetTuyaRate(float rate)
	{
	}

	// Token: 0x06007119 RID: 28953 RVA: 0x00303AA4 File Offset: 0x00301EA4
	public bool SetPlay(string stateName, int layer)
	{
		if (this.AnimBody == null || this.AnimBody.runtimeAnimatorController == null)
		{
			return false;
		}
		this.AnimBody.Play(stateName, layer);
		return true;
	}

	// Token: 0x0600711A RID: 28954 RVA: 0x00303ADD File Offset: 0x00301EDD
	public void SetAnimatorParamBool(string strAnmName, bool flag)
	{
		if (this.AnimBody == null || this.AnimBody.runtimeAnimatorController)
		{
			return;
		}
		this.AnimBody.SetBool(strAnmName, flag);
	}

	// Token: 0x0600711B RID: 28955 RVA: 0x00303B13 File Offset: 0x00301F13
	public bool GetAnimatorParamBool(string strAnmName)
	{
		return !(this.AnimBody == null) && !this.AnimBody.runtimeAnimatorController && this.AnimBody.GetBool(strAnmName);
	}

	// Token: 0x0600711C RID: 28956 RVA: 0x00303B49 File Offset: 0x00301F49
	public void SetAnimatorParamFloat(string strAnmName, float value)
	{
		if (this.AnimBody == null || this.AnimBody.runtimeAnimatorController == null)
		{
			return;
		}
		this.AnimBody.SetFloat(strAnmName, value);
	}

	// Token: 0x0600711D RID: 28957 RVA: 0x00303B80 File Offset: 0x00301F80
	public bool IsParameterInAnimator(string strParameter)
	{
		return !(this.AnimBody == null) && !(this.AnimBody.runtimeAnimatorController == null) && Array.FindIndex<AnimatorControllerParameter>(this.AnimBody.parameters, (AnimatorControllerParameter p) => p.name == strParameter) != -1;
	}

	// Token: 0x0600711E RID: 28958 RVA: 0x00303BE8 File Offset: 0x00301FE8
	public void ChangeLookEyesTarget(int targetno, Transform trfTarg = null, float rate = 1f)
	{
		if (this.EyeLookControl == null)
		{
			return;
		}
		this.EyeLookControl.target = null;
		if (targetno == 0)
		{
			if (Camera.main)
			{
				this.EyeLookControl.target = Camera.main.transform;
			}
		}
		else if (trfTarg)
		{
			this.EyeLookControl.target = trfTarg;
		}
	}

	// Token: 0x0600711F RID: 28959 RVA: 0x00303C59 File Offset: 0x00302059
	public void ChangeLookEyesPtn(int ptn)
	{
		this.EyesLookPtn = ptn;
		if (this.EyeLookControl == null)
		{
			return;
		}
		this.EyeLookControl.ptnNo = ptn;
	}

	// Token: 0x06007120 RID: 28960 RVA: 0x00303C80 File Offset: 0x00302080
	public void ChangeLookNeckTarget(int targetNo, Transform trfTarg = null, float rate = 1f)
	{
		if (this.NeckLookControl == null)
		{
			return;
		}
		this.NeckLookControl.target = null;
		if (targetNo == 0)
		{
			if (Camera.main)
			{
				this.NeckLookControl.target = Camera.main.transform;
			}
		}
		else if (trfTarg)
		{
			this.NeckLookControl.target = trfTarg;
		}
	}

	// Token: 0x06007121 RID: 28961 RVA: 0x00303CF1 File Offset: 0x003020F1
	public void ChangeLookNeckPtn(int ptn)
	{
		this.NeckLookPtn = ptn;
		if (this.NeckLookControl == null)
		{
			return;
		}
		this.NeckLookControl.ptnNo = ptn;
	}

	// Token: 0x06007122 RID: 28962 RVA: 0x00303D18 File Offset: 0x00302118
	public bool LookAt(Transform lookat, bool position)
	{
		GameObject referenceInfo = this.GetReferenceInfo(CharReference.RefObjKey.H_Kokan);
		GameObject referenceInfo2 = this.GetReferenceInfo(CharReference.RefObjKey.H_DanDir);
		if (referenceInfo == null)
		{
			return false;
		}
		if (lookat == null)
		{
			return false;
		}
		referenceInfo.transform.LookAt(lookat);
		if (position && referenceInfo2 != null)
		{
			referenceInfo2.transform.position = lookat.position;
		}
		return true;
	}

	// Token: 0x06007123 RID: 28963 RVA: 0x00303D84 File Offset: 0x00302184
	public void LoadHitObject()
	{
		this.ReleaseHitObject();
		this.ObjHitBody = UnityEngine.Object.Instantiate<GameObject>(this._objHitBody);
		if (this.ObjHitBody)
		{
			this.ObjHitBody.transform.SetParent(this.ObjTop.transform, false);
			this._aaWeightsBody.AssignedWeights(this.ObjHitBody, this.deleteBodyBoneName, null);
			SkinnedCollisionHelper[] componentsInChildren = this.ObjHitBody.GetComponentsInChildren<SkinnedCollisionHelper>(true);
			foreach (SkinnedCollisionHelper skinnedCollisionHelper in componentsInChildren)
			{
				skinnedCollisionHelper.Init();
			}
		}
		if (this.Sex != 0 && this._objHitHead)
		{
			this.ObjHitHead = UnityEngine.Object.Instantiate<GameObject>(this._objHitHead);
			if (this.ObjHitHead)
			{
				this.ObjHitHead.transform.SetParent(this.ObjTop.transform, false);
				this._aaWeightsHead.AssignedWeights(this.ObjHitHead, this.deleteHeadBoneName, null);
				SkinnedCollisionHelper[] componentsInChildren2 = this.ObjHitHead.GetComponentsInChildren<SkinnedCollisionHelper>(true);
				foreach (SkinnedCollisionHelper skinnedCollisionHelper2 in componentsInChildren2)
				{
					skinnedCollisionHelper2.Init();
				}
			}
		}
	}

	// Token: 0x06007124 RID: 28964 RVA: 0x00303EC4 File Offset: 0x003022C4
	public void ReleaseHitObject()
	{
		if (this.ObjHitBody)
		{
			SkinnedCollisionHelper[] componentsInChildren = this.ObjHitBody.GetComponentsInChildren<SkinnedCollisionHelper>(true);
			foreach (SkinnedCollisionHelper skinnedCollisionHelper in componentsInChildren)
			{
				skinnedCollisionHelper.Release();
			}
			UnityEngine.Object.Destroy(this.ObjHitBody);
			this.ObjHitBody = null;
		}
		if (this.ObjHitHead)
		{
			SkinnedCollisionHelper[] componentsInChildren2 = this.ObjHitHead.GetComponentsInChildren<SkinnedCollisionHelper>(true);
			foreach (SkinnedCollisionHelper skinnedCollisionHelper2 in componentsInChildren2)
			{
				skinnedCollisionHelper2.Release();
			}
			UnityEngine.Object.Destroy(this.ObjHitHead);
			this.ObjHitHead = null;
		}
	}

	// Token: 0x06007125 RID: 28965 RVA: 0x00303F80 File Offset: 0x00302380
	public RuntimeAnimatorController LoadAnimation(string assetBundleName, string assetName)
	{
		if (this.AnimBody == null)
		{
			return null;
		}
		RuntimeAnimatorController runtimeAnimatorController = CommonLib.LoadAsset<RuntimeAnimatorController>(assetBundleName, assetName, false, string.Empty);
		if (runtimeAnimatorController == null)
		{
			return null;
		}
		this.AnimBody.runtimeAnimatorController = runtimeAnimatorController;
		AssetBundleManager.UnloadAssetBundle(assetBundleName, true, null, false);
		return runtimeAnimatorController;
	}

	// Token: 0x06007126 RID: 28966 RVA: 0x00303FD4 File Offset: 0x003023D4
	public void ChangeTearsRate(float value)
	{
		this._tearsRate = value;
		if (this.TearMaterial)
		{
			float value2 = this.TearMaterial.GetFloat(TestChara._NamidaScale);
			value2 = Mathf.Lerp(0f, 1f, value);
			this.TearMaterial.SetFloat(TestChara._NamidaScale, value2);
		}
	}

	// Token: 0x06007127 RID: 28967 RVA: 0x0030402C File Offset: 0x0030242C
	public void ChangeHohoAkaRate(float value)
	{
		this._hohoAkaRate = value;
		if (this.FaceMaterial)
		{
			float value2 = this.FaceMaterial.GetFloat(TestChara._Texture4Scale);
			value2 = Mathf.Lerp(0f, 1f, value);
			this.FaceMaterial.SetFloat(TestChara._Texture4Scale, value2);
		}
	}

	// Token: 0x06007128 RID: 28968 RVA: 0x00304083 File Offset: 0x00302483
	public void DisableShapeMouth()
	{
		this._updateShapeFace = true;
		this._disableShapeMouth = true;
	}

	// Token: 0x06007129 RID: 28969 RVA: 0x00304093 File Offset: 0x00302493
	public void EnableShapeMouth()
	{
		this._updateShapeFace = true;
		this._disableShapeMouth = false;
	}

	// Token: 0x0600712A RID: 28970 RVA: 0x003040A4 File Offset: 0x003024A4
	public void DisableShapeBust(int lr, int idx)
	{
		if (this._shapeBody != null && this._shapeBody.InitEnd)
		{
			for (int i = 0; i < TestChara._idx.Length; i++)
			{
				this._shapeBody.ChangeValue(TestChara._idx[i], this._shapeValues[TestChara._idx[i]]);
			}
			this._shapeBody.ChangeValue(ChaFileDefine.cf_bodyshapename.Length - 1, this._nipStand);
			this._updateShapeBody = true;
			if (lr != 0)
			{
				if (lr != 1)
				{
					this._disableShapeBustLAry[idx] = true;
					this._disableShapeBustRAry[idx] = true;
				}
				else
				{
					this._disableShapeBustRAry[idx] = true;
				}
			}
			else
			{
				this._disableShapeBustLAry[idx] = true;
			}
			this.ResetupDynamicBoneBust = true;
		}
	}

	// Token: 0x0600712B RID: 28971 RVA: 0x00304174 File Offset: 0x00302574
	public void EnableShapeBust(int lr, int idx)
	{
		if (this._shapeBody != null && this._shapeBody.InitEnd)
		{
			for (int i = 0; i < TestChara._idx.Length; i++)
			{
				this._shapeBody.ChangeValue(TestChara._idx[i], this._shapeValues[TestChara._idx[i]]);
			}
			this._shapeBody.ChangeValue(ChaFileDefine.cf_bodyshapename.Length - 1, this._nipStand);
			this._updateShapeBody = true;
			if (lr != 0)
			{
				if (lr != 1)
				{
					this._disableShapeBustLAry[idx] = false;
					this._disableShapeBustRAry[idx] = false;
				}
				else
				{
					this._disableShapeBustRAry[idx] = false;
				}
			}
			else
			{
				this._disableShapeBustLAry[idx] = false;
			}
			this.ResetupDynamicBoneBust = true;
		}
	}

	// Token: 0x0600712C RID: 28972 RVA: 0x00304244 File Offset: 0x00302644
	public void DisableShapeNip(int lr)
	{
		if (this._shapeBody != null && this._shapeBody.InitEnd)
		{
			for (int i = 0; i < TestChara._idx.Length; i++)
			{
				this._shapeBody.ChangeValue(TestChara._idx[i], this._shapeValues[TestChara._idx[i]]);
			}
			this._shapeBody.ChangeValue(ChaFileDefine.cf_bodyshapename.Length, this._nipStand);
			this._updateShapeBody = true;
			if (lr != 0)
			{
				if (lr != 1)
				{
					this._disableShapeNipL = true;
					this._disableShapeNipR = true;
				}
				else
				{
					this._disableShapeNipR = true;
				}
			}
			else
			{
				this._disableShapeNipL = true;
			}
			this.ResetupDynamicBoneBust = true;
		}
	}

	// Token: 0x0600712D RID: 28973 RVA: 0x0030430C File Offset: 0x0030270C
	public void EnableShapeNip(int lr)
	{
		if (this._shapeBody != null && this._shapeBody.InitEnd)
		{
			for (int i = 0; i < TestChara._idx.Length; i++)
			{
				this._shapeBody.ChangeValue(TestChara._idx[i], this._shapeValues[TestChara._idx[i]]);
			}
			this._shapeBody.ChangeValue(ChaFileDefine.cf_bodyshapename.Length - 1, this._nipStand);
			this._updateShapeBody = true;
			if (lr != 0)
			{
				if (lr != 1)
				{
					this._disableShapeNipL = false;
					this._disableShapeNipR = false;
				}
				else
				{
					this._disableShapeNipR = false;
				}
			}
			else
			{
				this._disableShapeNipL = false;
			}
			this.ResetupDynamicBoneBust = true;
		}
	}

	// Token: 0x0600712E RID: 28974 RVA: 0x003043D4 File Offset: 0x003027D4
	private void AddListToTag(CharReference.TagObjKey key, List<GameObject> add)
	{
		if (add == null)
		{
			return;
		}
		List<GameObject> list = null;
		if (this._dictTagObj.TryGetValue((int)key, out list))
		{
			list.AddRange(add);
		}
		else
		{
			this._dictTagObj[(int)key] = add;
		}
	}

	// Token: 0x0600712F RID: 28975 RVA: 0x00304418 File Offset: 0x00302818
	public void CreateTagInfo(ulong flags, GameObject objTag)
	{
		if (null == objTag)
		{
			return;
		}
		FindAssist findAssist = new FindAssist();
		findAssist.Initialize(objTag.transform);
		if (flags >= 3UL && flags <= 30UL)
		{
			switch ((int)(flags - 3UL))
			{
			case 0:
				this.AddListToTag(CharReference.TagObjKey.ObjSkinFace, findAssist.GetObjectFromTag("ObjSkinFace"));
				this.AddListToTag(CharReference.TagObjKey.ObjEyebrow, findAssist.GetObjectFromTag("ObjEyebrow"));
				this.AddListToTag(CharReference.TagObjKey.ObjEyeL, findAssist.GetObjectFromTag("ObjEyeL"));
				this.AddListToTag(CharReference.TagObjKey.ObjEyeR, findAssist.GetObjectFromTag("ObjEyeR"));
				this.AddListToTag(CharReference.TagObjKey.ObjEyeW, findAssist.GetObjectFromTag("ObjEyeW"));
				if (this.Sex != 0)
				{
					this.AddListToTag(CharReference.TagObjKey.ObjEyeHi, findAssist.GetObjectFromTag("ObjEyeHi"));
					this.AddListToTag(CharReference.TagObjKey.ObjEyelashes, findAssist.GetObjectFromTag("ObjEyelashes"));
				}
				break;
			case 1:
				this.AddListToTag(CharReference.TagObjKey.ObjHairB, findAssist.GetObjectFromTag("ObjHair"));
				this.AddListToTag(CharReference.TagObjKey.ObjHairAcsB, findAssist.GetObjectFromTag("ObjHairAcs"));
				break;
			case 2:
				this.AddListToTag(CharReference.TagObjKey.ObjHairF, findAssist.GetObjectFromTag("ObjHair"));
				this.AddListToTag(CharReference.TagObjKey.ObjHairAcsF, findAssist.GetObjectFromTag("ObjHairAcs"));
				break;
			case 3:
				this.AddListToTag(CharReference.TagObjKey.ObjHairS, findAssist.GetObjectFromTag("ObjHair"));
				this.AddListToTag(CharReference.TagObjKey.ObjHairAcsS, findAssist.GetObjectFromTag("ObjHairAcs"));
				break;
			case 4:
				this.AddListToTag(CharReference.TagObjKey.ObjHairO, findAssist.GetObjectFromTag("ObjHair"));
				this.AddListToTag(CharReference.TagObjKey.ObjHairAcsO, findAssist.GetObjectFromTag("ObjHairAcs"));
				break;
			case 5:
				if (this.Sex != 0)
				{
					this.AddListToTag(CharReference.TagObjKey.ObjNip, findAssist.GetObjectFromTag("ObjNip"));
					this.AddListToTag(CharReference.TagObjKey.ObjNail, findAssist.GetObjectFromTag("ObjNail"));
					this.AddListToTag(CharReference.TagObjKey.ObjUnderHair, findAssist.GetObjectFromTag("ObjUnderHair"));
				}
				break;
			case 6:
				this.AddListToTag(CharReference.TagObjKey.ObjSkinBody, findAssist.GetObjectFromTag("ObjSkinBody"));
				this.AddListToTag(CharReference.TagObjKey.ObjCTop, findAssist.GetObjectFromTag("ObjColor"));
				break;
			case 7:
				this.AddListToTag(CharReference.TagObjKey.ObjCBot, findAssist.GetObjectFromTag("ObjColor"));
				break;
			case 8:
				this.AddListToTag(CharReference.TagObjKey.ObjSwim, findAssist.GetObjectFromTag("ObjColor"));
				break;
			case 9:
				this.AddListToTag(CharReference.TagObjKey.ObjSTop, findAssist.GetObjectFromTag("ObjColor"));
				break;
			case 10:
				this.AddListToTag(CharReference.TagObjKey.ObjSBot, findAssist.GetObjectFromTag("ObjColor"));
				break;
			case 11:
				this.AddListToTag(CharReference.TagObjKey.ObjBra, findAssist.GetObjectFromTag("ObjColor"));
				break;
			case 12:
				this.AddListToTag(CharReference.TagObjKey.ObjShorts, findAssist.GetObjectFromTag("ObjColor"));
				break;
			case 13:
				this.AddListToTag(CharReference.TagObjKey.ObjPanst, findAssist.GetObjectFromTag("ObjColor"));
				break;
			case 14:
				this.AddListToTag(CharReference.TagObjKey.ObjGloves, findAssist.GetObjectFromTag("ObjColor"));
				break;
			case 15:
				this.AddListToTag(CharReference.TagObjKey.ObjSocks, findAssist.GetObjectFromTag("ObjColor"));
				break;
			case 16:
				this.AddListToTag(CharReference.TagObjKey.ObjShoes, findAssist.GetObjectFromTag("ObjColor"));
				break;
			case 17:
				this.AddListToTag(CharReference.TagObjKey.ObjASlot01, findAssist.GetObjectFromTag("ObjColor"));
				break;
			case 18:
				this.AddListToTag(CharReference.TagObjKey.ObjASlot02, findAssist.GetObjectFromTag("ObjColor"));
				break;
			case 19:
				this.AddListToTag(CharReference.TagObjKey.ObjASlot03, findAssist.GetObjectFromTag("ObjColor"));
				break;
			case 20:
				this.AddListToTag(CharReference.TagObjKey.ObjASlot04, findAssist.GetObjectFromTag("ObjColor"));
				break;
			case 21:
				this.AddListToTag(CharReference.TagObjKey.ObjASlot05, findAssist.GetObjectFromTag("ObjColor"));
				break;
			case 22:
				this.AddListToTag(CharReference.TagObjKey.ObjASlot06, findAssist.GetObjectFromTag("ObjColor"));
				break;
			case 23:
				this.AddListToTag(CharReference.TagObjKey.ObjASlot07, findAssist.GetObjectFromTag("ObjColor"));
				break;
			case 24:
				this.AddListToTag(CharReference.TagObjKey.ObjASlot08, findAssist.GetObjectFromTag("ObjColor"));
				break;
			case 25:
				this.AddListToTag(CharReference.TagObjKey.ObjASlot09, findAssist.GetObjectFromTag("ObjColor"));
				break;
			case 26:
				this.AddListToTag(CharReference.TagObjKey.ObjASlot10, findAssist.GetObjectFromTag("ObjColor"));
				break;
			case 27:
				this.AddListToTag(CharReference.TagObjKey.ObjBeard, findAssist.GetObjectFromTag("ObjBeard"));
				break;
			}
		}
	}

	// Token: 0x06007130 RID: 28976 RVA: 0x0030487C File Offset: 0x00302C7C
	public void ReleaseTagObject(ulong flags)
	{
		if (flags >= 3UL && flags <= 30UL)
		{
			switch ((int)(flags - 3UL))
			{
			case 0:
				this._dictTagObj[8].Clear();
				this._dictTagObj[9].Clear();
				this._dictTagObj[10].Clear();
				this._dictTagObj[11].Clear();
				this._dictTagObj[12].Clear();
				this._dictTagObj[13].Clear();
				this._dictTagObj[15].Clear();
				break;
			case 1:
				this._dictTagObj[0].Clear();
				this._dictTagObj[4].Clear();
				break;
			case 2:
				this._dictTagObj[1].Clear();
				this._dictTagObj[5].Clear();
				break;
			case 3:
				this._dictTagObj[2].Clear();
				this._dictTagObj[6].Clear();
				break;
			case 4:
				this._dictTagObj[3].Clear();
				this._dictTagObj[7].Clear();
				break;
			case 5:
				this._dictTagObj[17].Clear();
				this._dictTagObj[16].Clear();
				this._dictTagObj[18].Clear();
				break;
			case 6:
				this._dictTagObj[19].Clear();
				this._dictTagObj[20].Clear();
				break;
			case 7:
				this._dictTagObj[21].Clear();
				break;
			case 8:
				this._dictTagObj[28].Clear();
				break;
			case 9:
				this._dictTagObj[29].Clear();
				break;
			case 10:
				this._dictTagObj[30].Clear();
				break;
			case 11:
				this._dictTagObj[22].Clear();
				break;
			case 12:
				this._dictTagObj[23].Clear();
				break;
			case 13:
				this._dictTagObj[25].Clear();
				break;
			case 14:
				this._dictTagObj[24].Clear();
				break;
			case 15:
				this._dictTagObj[26].Clear();
				break;
			case 16:
				this._dictTagObj[27].Clear();
				break;
			case 17:
				this._dictTagObj[31].Clear();
				break;
			case 18:
				this._dictTagObj[32].Clear();
				break;
			case 19:
				this._dictTagObj[33].Clear();
				break;
			case 20:
				this._dictTagObj[34].Clear();
				break;
			case 21:
				this._dictTagObj[35].Clear();
				break;
			case 22:
				this._dictTagObj[36].Clear();
				break;
			case 23:
				this._dictTagObj[37].Clear();
				break;
			case 24:
				this._dictTagObj[38].Clear();
				break;
			case 25:
				this._dictTagObj[39].Clear();
				break;
			case 26:
				this._dictTagObj[40].Clear();
				break;
			case 27:
				this._dictTagObj[14].Clear();
				break;
			}
		}
	}

	// Token: 0x06007131 RID: 28977 RVA: 0x00304C80 File Offset: 0x00303080
	public void ReleaseTagAll()
	{
		IEnumerator enumerator = Enum.GetValues(typeof(CharReference.TagObjKey)).GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				CharReference.TagObjKey key = (CharReference.TagObjKey)obj;
				this._dictTagObj[(int)key].Clear();
			}
		}
		finally
		{
			IDisposable disposable;
			if ((disposable = (enumerator as IDisposable)) != null)
			{
				disposable.Dispose();
			}
		}
	}

	// Token: 0x06007132 RID: 28978 RVA: 0x00304CF8 File Offset: 0x003030F8
	public List<GameObject> GetTagInfo(CharReference.TagObjKey key)
	{
		List<GameObject> collection = null;
		if (this._dictTagObj.TryGetValue((int)key, out collection))
		{
			return new List<GameObject>(collection);
		}
		return null;
	}

	// Token: 0x04005C5B RID: 23643
	private static List<UnityEx.ValueTuple<string, string>> _charaAssetBundleList = new List<UnityEx.ValueTuple<string, string>>();

	// Token: 0x04005C5C RID: 23644
	[SerializeField]
	[Label("初期化時にキャラを作成するか")]
	private bool _loadOnStart;

	// Token: 0x04005C5D RID: 23645
	[SerializeField]
	private bool _applySelfAnimatorParameter;

	// Token: 0x04005C5E RID: 23646
	[Header("Bone")]
	[SerializeField]
	private GameObject _boneBody;

	// Token: 0x04005C5F RID: 23647
	[SerializeField]
	private GameObject _boneHead;

	// Token: 0x04005C60 RID: 23648
	[SerializeField]
	private GameObject _body;

	// Token: 0x04005C61 RID: 23649
	[SerializeField]
	private GameObject _head;

	// Token: 0x04005C62 RID: 23650
	[SerializeField]
	private GameObject _hairBack;

	// Token: 0x04005C63 RID: 23651
	[SerializeField]
	private GameObject _hairFront;

	// Token: 0x04005C64 RID: 23652
	[SerializeField]
	private GameObject _clothTop;

	// Token: 0x04005C65 RID: 23653
	[SerializeField]
	private GameObject _clothBot;

	// Token: 0x04005C66 RID: 23654
	[SerializeField]
	private GameObject _bra;

	// Token: 0x04005C67 RID: 23655
	[SerializeField]
	private GameObject _shorts;

	// Token: 0x04005C68 RID: 23656
	[SerializeField]
	private GameObject _glove;

	// Token: 0x04005C69 RID: 23657
	[SerializeField]
	private GameObject _socks;

	// Token: 0x04005C6A RID: 23658
	[SerializeField]
	private GameObject _shoes;

	// Token: 0x04005C6B RID: 23659
	[SerializeField]
	[ListDrawerSettings(DraggableItems = false, HideAddButton = true, HideRemoveButton = true)]
	private TestChara.AcsGenerateInfo[] _acsArray = new TestChara.AcsGenerateInfo[20];

	// Token: 0x04005C6C RID: 23660
	[SerializeField]
	private Animator _animBody;

	// Token: 0x04005C6D RID: 23661
	[SerializeField]
	private RuntimeAnimatorController _rac;

	// Token: 0x04005C6E RID: 23662
	[SerializeField]
	private GameObject _objHitBody;

	// Token: 0x04005C6F RID: 23663
	[SerializeField]
	private GameObject _objHitHead;

	// Token: 0x04005C70 RID: 23664
	[SerializeField]
	private string _shapeFaceName = string.Empty;

	// Token: 0x04005C85 RID: 23685
	private ShapeHeadInfoFemale _shapeFace = new ShapeHeadInfoFemale();

	// Token: 0x04005C86 RID: 23686
	private ShapeBodyInfoFemale _shapeBody = new ShapeBodyInfoFemale();

	// Token: 0x04005C88 RID: 23688
	protected AssignedAnotherWeights _aaWeightsBody = new AssignedAnotherWeights();

	// Token: 0x04005C89 RID: 23689
	protected AssignedAnotherWeights _aaWeightsHead = new AssignedAnotherWeights();

	// Token: 0x04005C90 RID: 23696
	private float _nipStand;

	// Token: 0x04005C91 RID: 23697
	private Dictionary<int, GameObject> _dictRefObj = new Dictionary<int, GameObject>();

	// Token: 0x04005C92 RID: 23698
	private Dictionary<int, GameObject> _dictRefObjNew = new Dictionary<int, GameObject>();

	// Token: 0x04005C93 RID: 23699
	private float[] _shapeValuesFace = new float[ChaFileDefine.cf_headshapename.Length];

	// Token: 0x04005C94 RID: 23700
	private float[] _shapeValues = new float[ChaFileDefine.cf_bodyshapename.Length - 1];

	// Token: 0x04005C95 RID: 23701
	private bool _updateShapeFace;

	// Token: 0x04005C96 RID: 23702
	private bool _updateShapeBody;

	// Token: 0x04005C97 RID: 23703
	private readonly int[] _bustChangeSettingPtn = new int[1];

	// Token: 0x04005CA5 RID: 23717
	private bool _visibleMozaic;

	// Token: 0x04005CA6 RID: 23718
	private List<GameObject> _objMozaicList = new List<GameObject>();

	// Token: 0x04005CA7 RID: 23719
	private AnimationKeyInfo _anmKeyInfo = new AnimationKeyInfo();

	// Token: 0x04005CA9 RID: 23721
	[SerializeField]
	private byte _sex = 1;

	// Token: 0x04005CAC RID: 23724
	private float _tearsRate;

	// Token: 0x04005CAD RID: 23725
	private float _hohoAkaRate;

	// Token: 0x04005CB6 RID: 23734
	private string deleteBodyBoneName = "cf_J_Root";

	// Token: 0x04005CB7 RID: 23735
	private string deleteHeadBoneName = "cf_J_FaceRoot";

	// Token: 0x04005CB8 RID: 23736
	private static string[] _boneNames = new string[]
	{
		"cf_J",
		"cm_J"
	};

	// Token: 0x04005CB9 RID: 23737
	private static string[] _m_mozaicparts = new string[]
	{
		"cm_o_dan00",
		"cm_o_dan_f"
	};

	// Token: 0x04005CBA RID: 23738
	private static string[] _f_mozaicparts = new string[]
	{
		"o_mnpa",
		"o_mnpb"
	};

	// Token: 0x04005CBB RID: 23739
	private float[] _workValue = new float[ChaFileDefine.cf_BustShapeMaskID.Length];

	// Token: 0x04005CBC RID: 23740
	private float[] _defaultBustValue = new float[]
	{
		0.5f,
		0.5f,
		0.5f,
		0.5f,
		0.5f,
		0.5f,
		0.5f
	};

	// Token: 0x04005CBD RID: 23741
	private static readonly int _Smoothness = Shader.PropertyToID("_Smoothness");

	// Token: 0x04005CBE RID: 23742
	private static readonly int[] _idx = new int[]
	{
		2,
		3,
		4,
		5,
		6,
		7,
		8
	};

	// Token: 0x04005CBF RID: 23743
	private bool _disableShapeMouth;

	// Token: 0x04005CC0 RID: 23744
	private bool[] _disableShapeBustLAry = new bool[7];

	// Token: 0x04005CC1 RID: 23745
	private bool[] _disableShapeBustRAry = new bool[7];

	// Token: 0x04005CC2 RID: 23746
	private bool _disableShapeNipL;

	// Token: 0x04005CC3 RID: 23747
	private bool _disableShapeNipR;

	// Token: 0x04005CC4 RID: 23748
	private static readonly int _NamidaScale = Shader.PropertyToID("_NamidaScale");

	// Token: 0x04005CC5 RID: 23749
	private static readonly int _Texture4Scale = Shader.PropertyToID("_Texture4Scale");

	// Token: 0x04005CC6 RID: 23750
	public const ulong FbxTypeBone = 1UL;

	// Token: 0x04005CC7 RID: 23751
	public const ulong FbxTypeBody = 2UL;

	// Token: 0x04005CC8 RID: 23752
	public const ulong FbxTypeHead = 3UL;

	// Token: 0x04005CC9 RID: 23753
	public const ulong FbxTypeHairB = 4UL;

	// Token: 0x04005CCA RID: 23754
	public const ulong FbxTypeHairF = 5UL;

	// Token: 0x04005CCB RID: 23755
	public const ulong FbxTypeHairS = 6UL;

	// Token: 0x04005CCC RID: 23756
	public const ulong FbxTypeHairO = 7UL;

	// Token: 0x04005CCD RID: 23757
	public const ulong FbxTypeBase = 8UL;

	// Token: 0x04005CCE RID: 23758
	public const ulong FbxTypeCTop = 9UL;

	// Token: 0x04005CCF RID: 23759
	public const ulong FbxTypeCBot = 10UL;

	// Token: 0x04005CD0 RID: 23760
	public const ulong FbxTypeSwim = 11UL;

	// Token: 0x04005CD1 RID: 23761
	public const ulong FbxTypeSTop = 12UL;

	// Token: 0x04005CD2 RID: 23762
	public const ulong FbxTypeSBot = 13UL;

	// Token: 0x04005CD3 RID: 23763
	public const ulong FbxTypeBra = 14UL;

	// Token: 0x04005CD4 RID: 23764
	public const ulong FbxTypeShorts = 15UL;

	// Token: 0x04005CD5 RID: 23765
	public const ulong FbxTypePanst = 16UL;

	// Token: 0x04005CD6 RID: 23766
	public const ulong FbxTypeGloves = 17UL;

	// Token: 0x04005CD7 RID: 23767
	public const ulong FbxTypeSocks = 18UL;

	// Token: 0x04005CD8 RID: 23768
	public const ulong FbxTypeShoes = 19UL;

	// Token: 0x04005CD9 RID: 23769
	public const ulong FbxTypeAcs01 = 20UL;

	// Token: 0x04005CDA RID: 23770
	public const ulong FbxTypeAcs02 = 21UL;

	// Token: 0x04005CDB RID: 23771
	public const ulong FbxTypeAcs03 = 22UL;

	// Token: 0x04005CDC RID: 23772
	public const ulong FbxTypeAcs04 = 23UL;

	// Token: 0x04005CDD RID: 23773
	public const ulong FbxTypeAcs05 = 24UL;

	// Token: 0x04005CDE RID: 23774
	public const ulong FbxTypeAcs06 = 25UL;

	// Token: 0x04005CDF RID: 23775
	public const ulong FbxTypeAcs07 = 26UL;

	// Token: 0x04005CE0 RID: 23776
	public const ulong FbxTypeAcs08 = 27UL;

	// Token: 0x04005CE1 RID: 23777
	public const ulong FbxTypeAcs09 = 28UL;

	// Token: 0x04005CE2 RID: 23778
	public const ulong FbxTypeAcs10 = 29UL;

	// Token: 0x04005CE3 RID: 23779
	public const ulong FbxTypeBeard = 30UL;

	// Token: 0x04005CE4 RID: 23780
	public const ulong FbxTypeSiruTop = 31UL;

	// Token: 0x04005CE5 RID: 23781
	public const ulong FbxTypeSiruBot = 32UL;

	// Token: 0x04005CE6 RID: 23782
	public const ulong FbxTypeSiruBra = 33UL;

	// Token: 0x04005CE7 RID: 23783
	public const ulong FbxTypeSiruShorts = 34UL;

	// Token: 0x04005CE8 RID: 23784
	public const ulong FbxTypeSiruSwim = 35UL;

	// Token: 0x04005CE9 RID: 23785
	public const ulong FbxTypeSiruHairB = 36UL;

	// Token: 0x04005CEA RID: 23786
	public const ulong FbxTypeSiruHairF = 37UL;

	// Token: 0x04005CEB RID: 23787
	private Dictionary<int, List<GameObject>> _dictTagObj = new Dictionary<int, List<GameObject>>();

	// Token: 0x02000E24 RID: 3620
	[Serializable]
	public class AcsGenerateInfo
	{
		// Token: 0x170015B8 RID: 5560
		// (get) Token: 0x06007135 RID: 28981 RVA: 0x00304DE7 File Offset: 0x003031E7
		public CharReference.RefObjKey_New Key
		{
			[CompilerGenerated]
			get
			{
				return this._key;
			}
		}

		// Token: 0x170015B9 RID: 5561
		// (get) Token: 0x06007136 RID: 28982 RVA: 0x00304DEF File Offset: 0x003031EF
		public CharReference.RefObjKey ParentKey
		{
			[CompilerGenerated]
			get
			{
				return this._parentKey;
			}
		}

		// Token: 0x170015BA RID: 5562
		// (get) Token: 0x06007137 RID: 28983 RVA: 0x00304DF7 File Offset: 0x003031F7
		public GameObject ACSObj
		{
			[CompilerGenerated]
			get
			{
				return this._acsObj;
			}
		}

		// Token: 0x04005CEC RID: 23788
		[SerializeField]
		private CharReference.RefObjKey_New _key;

		// Token: 0x04005CED RID: 23789
		[SerializeField]
		[HideInInspector]
		private CharReference.RefObjKey _parentKey;

		// Token: 0x04005CEE RID: 23790
		[SerializeField]
		private GameObject _acsObj;
	}
}
