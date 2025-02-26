using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000AF8 RID: 2808
public class MetaCollider : MonoBehaviour
{
	// Token: 0x060051DC RID: 20956 RVA: 0x0021CF14 File Offset: 0x0021B314
	private void Start()
	{
		this.judgeTags = new HashSet<string>(this.nameJudgeTags);
		if (this.judgeTags == null)
		{
			this.judgeTags = new HashSet<string>();
		}
		this.judgeWaitTags = new HashSet<string>(this.nameWaitTags);
		if (this.judgeWaitTags == null)
		{
			this.judgeWaitTags = new HashSet<string>();
		}
		this.rigid = base.GetComponent<Rigidbody>();
		this.metajoint = base.GetComponent<MetaballJoint>();
		this.joint = base.GetComponent<ConfigurableJoint>();
	}

	// Token: 0x060051DD RID: 20957 RVA: 0x0021CF93 File Offset: 0x0021B393
	private void FixedUpdate()
	{
		if (!this.isGroundHit && (this.objnowhit == MetaCollider.Hit.air || this.objnowhit == MetaCollider.Hit.exit) && this.nowhit == MetaCollider.Hit.air)
		{
			this.lerpDrag();
		}
	}

	// Token: 0x060051DE RID: 20958 RVA: 0x0021CFCC File Offset: 0x0021B3CC
	private void Update()
	{
		if (this.isConstraintNow && this.parentTransfrom != null)
		{
			base.transform.position = this.parentTransfrom.localToWorldMatrix.MultiplyPoint3x4(this.posConstLocal);
		}
		if (this.rigid && !this.rigid.isKinematic)
		{
			this.sleepForceTime += Time.deltaTime;
			if (this.sleepTime <= this.sleepForceTime)
			{
				this.rigid.isKinematic = true;
				this.rigid.Sleep();
			}
		}
	}

	// Token: 0x060051DF RID: 20959 RVA: 0x0021D074 File Offset: 0x0021B474
	private void LateUpdate()
	{
		if (!this.isGroundHit)
		{
			if (this.objhit == MetaCollider.Hit.hit)
			{
				if (this.objnowhit == MetaCollider.Hit.air || this.objnowhit == MetaCollider.Hit.exit)
				{
					this.objnowhit = MetaCollider.Hit.hit;
					this.rigid.drag = this.dragHit;
				}
				else if (this.objnowhit == MetaCollider.Hit.hit)
				{
					this.objnowhit = MetaCollider.Hit.stay;
				}
			}
			else if (this.objnowhit == MetaCollider.Hit.stay || this.objnowhit == MetaCollider.Hit.hit)
			{
				this.objnowhit = MetaCollider.Hit.exit;
				this.dragTempAir = this.dragHit;
				this.timeLerpDrag = 0f;
			}
			if ((this.objnowhit == MetaCollider.Hit.air || this.objnowhit == MetaCollider.Hit.exit) && this.nowhit == MetaCollider.Hit.air && this.isEndGravity && this.rigid && !this.rigid.useGravity)
			{
				this.rigid.useGravity = true;
			}
		}
		this.objhit = MetaCollider.Hit.air;
	}

	// Token: 0x060051E0 RID: 20960 RVA: 0x0021D17C File Offset: 0x0021B57C
	private void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.layer == 13 || col.gameObject.layer == 17)
		{
			return;
		}
		if (this.isEndGravity && this.rigid && this.rigid.useGravity)
		{
			this.rigid.useGravity = false;
		}
		if (this.judgeTags.Contains(col.gameObject.tag))
		{
			this.isGroundHit = true;
			base.gameObject.layer = LayerMask.NameToLayer(this.nameChangeLayer);
			if (this.rigid)
			{
				this.rigid.mass = 1f;
				this.rigid.useGravity = true;
				this.rigid.drag = this.dragWait;
			}
			if (this.metajoint)
			{
				UnityEngine.Object.Destroy(this.metajoint);
			}
			if (this.joint)
			{
				UnityEngine.Object.Destroy(this.joint);
			}
		}
		if (this.judgeWaitTags.Contains(col.gameObject.tag))
		{
			this.nowhit = MetaCollider.Hit.hit;
			if (this.rigid)
			{
				this.rigid.drag = this.dragHit;
			}
			this.Constraint(col);
		}
		this.ChangeJoint();
		this.NextAddForce();
		if (col.gameObject.tag == this.nameBodytagName)
		{
			this.objhit = MetaCollider.Hit.hit;
		}
	}

	// Token: 0x060051E1 RID: 20961 RVA: 0x0021D30B File Offset: 0x0021B70B
	private void OnCollisionStay(Collision col)
	{
		if (this.judgeWaitTags.Contains(col.gameObject.tag))
		{
			this.Constraint(col);
		}
	}

	// Token: 0x060051E2 RID: 20962 RVA: 0x0021D330 File Offset: 0x0021B730
	private void OnCollisionExit(Collision col)
	{
		if (!this.judgeWaitTags.Contains(col.gameObject.tag))
		{
			return;
		}
		this.nowhit = MetaCollider.Hit.air;
		this.timeLerpDrag = 0f;
		this.dragTempAir = this.dragHit;
	}

	// Token: 0x060051E3 RID: 20963 RVA: 0x0021D36C File Offset: 0x0021B76C
	public bool ChangeJoint()
	{
		if (!this.metajoint || !this.joint)
		{
			return false;
		}
		if (!this.metajoint.isActiveAndEnabled)
		{
			return true;
		}
		this.metajoint.enabled = false;
		this.joint.xMotion = ConfigurableJointMotion.Limited;
		this.joint.yMotion = ConfigurableJointMotion.Limited;
		this.joint.zMotion = ConfigurableJointMotion.Limited;
		return true;
	}

	// Token: 0x060051E4 RID: 20964 RVA: 0x0021D3E0 File Offset: 0x0021B7E0
	private bool Constraint(Collision col)
	{
		if (!this.isConstraint)
		{
			return true;
		}
		if (this.isConstraintNow)
		{
			return true;
		}
		this.stayTime += Time.deltaTime;
		if (this.stayTime < this.constTime)
		{
			return true;
		}
		if (this.rigid)
		{
			this.rigid.isKinematic = true;
			this.rigid.Sleep();
		}
		this.isConstraintNow = true;
		return true;
	}

	// Token: 0x060051E5 RID: 20965 RVA: 0x0021D45C File Offset: 0x0021B85C
	private bool NextAddForce()
	{
		if (!this.nextRigidBody || this.isNextAddForce)
		{
			return true;
		}
		Vector3 vector = base.transform.position - this.nextRigidBody.transform.position;
		this.nextRigidBody.velocity = Vector3.zero;
		this.nextRigidBody.AddForce(vector.normalized * this.addNextForce, ForceMode.Impulse);
		this.isNextAddForce = true;
		return true;
	}

	// Token: 0x060051E6 RID: 20966 RVA: 0x0021D4E0 File Offset: 0x0021B8E0
	private bool lerpDrag()
	{
		this.timeLerpDrag += Time.deltaTime;
		this.timeLerpDrag = Mathf.Clamp(this.timeLerpDrag, 0f, this.timeDropDown);
		float t = Mathf.InverseLerp(0f, this.timeDropDown, this.timeLerpDrag);
		if (this.timeDropDown == 0f)
		{
			t = 1f;
		}
		if (this.rigid)
		{
			this.rigid.drag = Mathf.Lerp(this.dragTempAir, this.dragAir, t);
		}
		return true;
	}

	// Token: 0x04004C79 RID: 19577
	[Tooltip("こいつに当たったらLayerの名前を変更したりジョイントを切り離す dragWaitの値になる")]
	public string[] nameJudgeTags;

	// Token: 0x04004C7A RID: 19578
	[Tooltip("変更するLayerの名")]
	public string nameChangeLayer = string.Empty;

	// Token: 0x04004C7B RID: 19579
	[Tooltip("こいつに当たったらdragHitの値になる 拘束判定も含む")]
	public string[] nameWaitTags;

	// Token: 0x04004C7C RID: 19580
	[Tooltip("こいつに当たったらdragHitの値になる\n離れた瞬間dragAirが入る\nウエイトのついてる当たり判定？\nnameJudgeTagsより判定強い")]
	public string nameBodytagName;

	// Token: 0x04004C7D RID: 19581
	[Tooltip("nameJudgeTagsのColliderに当たった瞬間")]
	public float dragWait = 30f;

	// Token: 0x04004C7E RID: 19582
	[Tooltip("nameWaitTagsのColliderに当たった瞬間")]
	public float dragHit = 30f;

	// Token: 0x04004C7F RID: 19583
	[Tooltip("nameWaitTagsのColliderから離れた瞬間")]
	public float dragAir;

	// Token: 0x04004C80 RID: 19584
	[Tooltip("当たったコライダーと拘束する？")]
	public bool isConstraint = true;

	// Token: 0x04004C81 RID: 19585
	[Tooltip("当たったコライダーと拘束するまでの時間")]
	public float constTime = 0.5f;

	// Token: 0x04004C82 RID: 19586
	[Tooltip("強制的にスリープにするまでの時間")]
	public float sleepTime = 10f;

	// Token: 0x04004C83 RID: 19587
	[Tooltip("後ろのオブジェクトのRigidBody")]
	public Rigidbody nextRigidBody;

	// Token: 0x04004C84 RID: 19588
	[Tooltip("Air時に重力付けて,それ以外の時は切る")]
	public bool isEndGravity;

	// Token: 0x04004C85 RID: 19589
	[Tooltip("追加のちから")]
	public float addNextForce = 1f;

	// Token: 0x04004C86 RID: 19590
	[Header("確認用")]
	[Tooltip("Air中にこの時間でDragが変化する dragAirの値になる")]
	public float timeDropDown;

	// Token: 0x04004C87 RID: 19591
	[SerializeField]
	[Tooltip("確認用")]
	private float timeLerpDrag;

	// Token: 0x04004C88 RID: 19592
	[SerializeField]
	[Tooltip("確認用")]
	private float dragTempAir;

	// Token: 0x04004C89 RID: 19593
	[SerializeField]
	[Tooltip("確認用 拘束されている先")]
	private Transform parentTransfrom;

	// Token: 0x04004C8A RID: 19594
	private HashSet<string> judgeTags;

	// Token: 0x04004C8B RID: 19595
	private HashSet<string> judgeWaitTags;

	// Token: 0x04004C8C RID: 19596
	[SerializeField]
	[Tooltip("確認用表示 拘束計算するまで")]
	private float stayTime;

	// Token: 0x04004C8D RID: 19597
	[SerializeField]
	[Tooltip("確認用表示 強制的にスリープにするまで")]
	private float sleepForceTime;

	// Token: 0x04004C8E RID: 19598
	[SerializeField]
	[Tooltip("確認用表示 拘束計算が終わった？")]
	private bool isConstraintNow;

	// Token: 0x04004C8F RID: 19599
	private Vector3 posConstLocal = Vector3.zero;

	// Token: 0x04004C90 RID: 19600
	private MetaCollider.Hit objhit;

	// Token: 0x04004C91 RID: 19601
	[SerializeField]
	[Tooltip("確認用表示")]
	private MetaCollider.Hit objnowhit;

	// Token: 0x04004C92 RID: 19602
	[SerializeField]
	[Tooltip("確認用表示")]
	private MetaCollider.Hit nowhit;

	// Token: 0x04004C93 RID: 19603
	[SerializeField]
	[Tooltip("確認用表示")]
	private bool isNextAddForce;

	// Token: 0x04004C94 RID: 19604
	private Rigidbody rigid;

	// Token: 0x04004C95 RID: 19605
	private bool isGroundHit;

	// Token: 0x04004C96 RID: 19606
	private MetaballJoint metajoint;

	// Token: 0x04004C97 RID: 19607
	private ConfigurableJoint joint;

	// Token: 0x02000AF9 RID: 2809
	public enum Hit
	{
		// Token: 0x04004C99 RID: 19609
		air,
		// Token: 0x04004C9A RID: 19610
		hit,
		// Token: 0x04004C9B RID: 19611
		ground,
		// Token: 0x04004C9C RID: 19612
		stay,
		// Token: 0x04004C9D RID: 19613
		exit
	}
}
