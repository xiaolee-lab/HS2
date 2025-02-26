using System;
using System.Collections.Generic;
using AIChara;
using UnityEngine;

// Token: 0x02000AF6 RID: 2806
[Serializable]
public class MetaballShoot
{
	// Token: 0x17000EEF RID: 3823
	// (get) Token: 0x060051D2 RID: 20946 RVA: 0x0021C9F3 File Offset: 0x0021ADF3
	// (set) Token: 0x060051D3 RID: 20947 RVA: 0x0021C9FB File Offset: 0x0021ADFB
	public bool isConstMetaMesh { get; private set; }

	// Token: 0x060051D4 RID: 20948 RVA: 0x0021CA04 File Offset: 0x0021AE04
	public bool IsCreate()
	{
		for (int i = 0; i < this.lstMeta.Count; i++)
		{
			for (int j = 0; j < this.lstMeta[i].objMeta.aRigid.Length; j++)
			{
				if (!this.lstMeta[i].objMeta.aRigid[j].isKinematic && !this.lstMeta[i].objMeta.aRigid[j].IsSleeping())
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x060051D5 RID: 20949 RVA: 0x0021CAA2 File Offset: 0x0021AEA2
	private void OnValidate()
	{
	}

	// Token: 0x060051D6 RID: 20950 RVA: 0x0021CAA4 File Offset: 0x0021AEA4
	public bool ShootMetaBallStart()
	{
		this.numInterval = -1;
		this.timeInterval = 0f;
		if (this.parentTransform)
		{
			this.isConstMetaMesh = true;
		}
		return true;
	}

	// Token: 0x060051D7 RID: 20951 RVA: 0x0021CAD0 File Offset: 0x0021AED0
	public void MetaBallClear()
	{
		for (int i = 0; i < this.lstMeta.Count; i++)
		{
			UnityEngine.Object.Destroy(this.lstMeta[i].objMeta.gameObject);
		}
		this.lstMeta.Clear();
		this.isConstMetaMesh = false;
	}

	// Token: 0x060051D8 RID: 20952 RVA: 0x0021CB28 File Offset: 0x0021AF28
	public bool ShootMetaBall()
	{
		if (this.timeInterval == 9999999f)
		{
			return false;
		}
		if (this.numInterval != -1)
		{
			this.timeInterval += Time.deltaTime;
			if (this.timeInterval < this.aInterval[this.numInterval])
			{
				return false;
			}
		}
		this.MetaBallInstantiate();
		this.numInterval++;
		this.timeInterval = 0f;
		if (this.aInterval.Length <= this.numInterval)
		{
			this.timeInterval = 9999999f;
		}
		return true;
	}

	// Token: 0x060051D9 RID: 20953 RVA: 0x0021CBC0 File Offset: 0x0021AFC0
	private bool MetaBallInstantiate()
	{
		if (!this.ShootAxis || !this.ShootObj || !this.objSourceRoot)
		{
			return false;
		}
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.ShootObj);
		if (!gameObject)
		{
			return false;
		}
		gameObject.name = this.ShootObj.name;
		gameObject.transform.SetParent(this.objSourceRoot.transform, false);
		gameObject.transform.position = this.ShootAxis.transform.position;
		gameObject.transform.rotation = this.ShootAxis.transform.rotation;
		metaballinfo component = gameObject.GetComponent<metaballinfo>();
		if (!component)
		{
			UnityEngine.Object.Destroy(gameObject);
			return false;
		}
		if (component.rigidBeginning)
		{
			component.rigidBeginning.drag = this.drag;
			float num = 0.5f;
			if (this.chaCustom != null)
			{
				num = this.chaCustom.GetShapeBodyValue(0);
			}
			Vector3 vector = component.rigidBeginning.transform.InverseTransformDirection(this.ShootAxis.transform.forward);
			float t = (num < 0.5f) ? Mathf.InverseLerp(0f, 0.5f, num) : Mathf.InverseLerp(0.5f, 1f, num);
			Vector2 vector2 = (num < 0.5f) ? Vector2.Lerp(this.randomRotationS, this.randomRotationM, t) : Vector2.Lerp(this.randomRotationM, this.randomRotationL, t);
			Quaternion rotation = Quaternion.Euler(UnityEngine.Random.Range(-vector2.x, vector2.x), UnityEngine.Random.Range(-vector2.y, vector2.y), 0f);
			vector = rotation * vector;
			float min = (num < 0.5f) ? Mathf.Lerp(this.speedSMin, this.speedMMin, t) : Mathf.Lerp(this.speedMMin, this.speedLMin, t);
			float max = (num < 0.5f) ? Mathf.Lerp(this.speedSMax, this.speedMMax, t) : Mathf.Lerp(this.speedMMax, this.speedLMax, t);
			component.rigidBeginning.AddRelativeForce(vector * UnityEngine.Random.Range(min, max), this.shootForce);
		}
		MetaballShoot.MetaInfo metaInfo = new MetaballShoot.MetaInfo();
		metaInfo.objMeta = component;
		metaInfo.nowDrag = this.drag;
		metaInfo.timeLerpDragRand = this.timeDropDown;
		this.lstMeta.Add(metaInfo);
		if (this.lstMeta.Count > this.maxMetaball)
		{
			UnityEngine.Object.Destroy(this.lstMeta[0].objMeta.gameObject);
			this.lstMeta.RemoveAt(0);
		}
		return true;
	}

	// Token: 0x04004C5A RID: 19546
	public string comment = string.Empty;

	// Token: 0x04004C5B RID: 19547
	public bool isEnable = true;

	// Token: 0x04004C5C RID: 19548
	[Tooltip("発射軸")]
	public GameObject ShootAxis;

	// Token: 0x04004C5D RID: 19549
	[Tooltip("生成する弾")]
	public GameObject ShootObj;

	// Token: 0x04004C5E RID: 19550
	[Tooltip("SourceRoot")]
	public GameObject objSourceRoot;

	// Token: 0x04004C5F RID: 19551
	[Tooltip("止まった時に親子付する場所")]
	public Transform parentTransform;

	// Token: 0x04004C60 RID: 19552
	[Header("PARAM")]
	public float drag = 1f;

	// Token: 0x04004C61 RID: 19553
	public float dragDropDown = 1f;

	// Token: 0x04004C62 RID: 19554
	public int maxMetaball = 30;

	// Token: 0x04004C63 RID: 19555
	public float timeDropDown = 1f;

	// Token: 0x04004C64 RID: 19556
	public float speedSMin = 1f;

	// Token: 0x04004C65 RID: 19557
	public float speedSMax = 1f;

	// Token: 0x04004C66 RID: 19558
	public float speedMMin = 1f;

	// Token: 0x04004C67 RID: 19559
	public float speedMMax = 1f;

	// Token: 0x04004C68 RID: 19560
	public float speedLMin = 1f;

	// Token: 0x04004C69 RID: 19561
	public float speedLMax = 1f;

	// Token: 0x04004C6A RID: 19562
	public float[] aInterval;

	// Token: 0x04004C6B RID: 19563
	public Vector2 randomRotationS = Vector2.zero;

	// Token: 0x04004C6C RID: 19564
	public Vector2 randomRotationM = Vector2.zero;

	// Token: 0x04004C6D RID: 19565
	public Vector2 randomRotationL = Vector2.zero;

	// Token: 0x04004C6E RID: 19566
	public ForceMode shootForce;

	// Token: 0x04004C70 RID: 19568
	public ChaControl chaCustom;

	// Token: 0x04004C71 RID: 19569
	private const float timeConstMax = 9999999f;

	// Token: 0x04004C72 RID: 19570
	[SerializeField]
	private List<MetaballShoot.MetaInfo> lstMeta = new List<MetaballShoot.MetaInfo>();

	// Token: 0x04004C73 RID: 19571
	private float timeInterval = 9999999f;

	// Token: 0x04004C74 RID: 19572
	private int numInterval;

	// Token: 0x02000AF7 RID: 2807
	[Serializable]
	public class MetaInfo
	{
		// Token: 0x04004C75 RID: 19573
		public metaballinfo objMeta;

		// Token: 0x04004C76 RID: 19574
		public float timeLerpDrag;

		// Token: 0x04004C77 RID: 19575
		public float timeLerpDragRand;

		// Token: 0x04004C78 RID: 19576
		public float nowDrag;
	}
}
