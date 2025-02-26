using System;
using UnityEngine;

// Token: 0x02001172 RID: 4466
public class MonoBehaviourMessage : MonoBehaviour
{
	// Token: 0x06009367 RID: 37735 RVA: 0x003D0B92 File Offset: 0x003CEF92
	public void Awake()
	{
		MonoBehaviour.print("Awake : " + base.name);
	}

	// Token: 0x06009368 RID: 37736 RVA: 0x003D0BA9 File Offset: 0x003CEFA9
	public void Start()
	{
		MonoBehaviour.print("Start : " + base.name);
	}

	// Token: 0x06009369 RID: 37737 RVA: 0x003D0BC0 File Offset: 0x003CEFC0
	public void OnEnable()
	{
		MonoBehaviour.print("OnEnable : " + base.name);
	}

	// Token: 0x0600936A RID: 37738 RVA: 0x003D0BD7 File Offset: 0x003CEFD7
	public void OnDisable()
	{
		MonoBehaviour.print("OnDisable : " + base.name);
	}

	// Token: 0x0600936B RID: 37739 RVA: 0x003D0BEE File Offset: 0x003CEFEE
	public void OnDestroy()
	{
		MonoBehaviour.print("OnDestroy : " + base.name);
	}

	// Token: 0x0600936C RID: 37740 RVA: 0x003D0C05 File Offset: 0x003CF005
	public void OnApplicationFocus(bool isFocus)
	{
		MonoBehaviour.print("OnApplicationFocus : " + base.name);
		MonoBehaviour.print("isFocus : " + isFocus);
	}

	// Token: 0x0600936D RID: 37741 RVA: 0x003D0C31 File Offset: 0x003CF031
	public void OnApplicationPause(bool isPause)
	{
		MonoBehaviour.print("OnApplicationPause : " + base.name);
		MonoBehaviour.print("isPause : " + isPause);
	}

	// Token: 0x0600936E RID: 37742 RVA: 0x003D0C5D File Offset: 0x003CF05D
	public void OnApplicationQuit()
	{
		MonoBehaviour.print("OnApplicationQuit : " + base.name);
	}

	// Token: 0x0600936F RID: 37743 RVA: 0x003D0C74 File Offset: 0x003CF074
	public void OnTransformChildrenChanged()
	{
		MonoBehaviour.print("OnTransformChildrenChanged : " + base.name);
	}

	// Token: 0x06009370 RID: 37744 RVA: 0x003D0C8B File Offset: 0x003CF08B
	public void OnTransformParentChanged()
	{
		MonoBehaviour.print("OnTransformParentChanged : " + base.name);
	}

	// Token: 0x06009371 RID: 37745 RVA: 0x003D0CA2 File Offset: 0x003CF0A2
	public void OnValidate()
	{
		MonoBehaviour.print("OnValidate : " + base.name);
	}

	// Token: 0x06009372 RID: 37746 RVA: 0x003D0CB9 File Offset: 0x003CF0B9
	public void Reset()
	{
		MonoBehaviour.print("Reset : " + base.name);
	}

	// Token: 0x06009373 RID: 37747 RVA: 0x003D0CD0 File Offset: 0x003CF0D0
	public void OnAnimatorIK()
	{
		MonoBehaviour.print("OnAnimatorIK : " + base.name);
	}

	// Token: 0x06009374 RID: 37748 RVA: 0x003D0CE7 File Offset: 0x003CF0E7
	public void OnAnimatorMove()
	{
		MonoBehaviour.print("OnAnimatorMove : " + base.name);
	}

	// Token: 0x06009375 RID: 37749 RVA: 0x003D0CFE File Offset: 0x003CF0FE
	public void OnAudioFilterRead(float[] data, int channels)
	{
		MonoBehaviour.print("OnAudioFilterRead : " + base.name);
	}

	// Token: 0x06009376 RID: 37750 RVA: 0x003D0D15 File Offset: 0x003CF115
	public void OnJointBreak()
	{
		MonoBehaviour.print("OnJointBreak : " + base.name);
	}

	// Token: 0x06009377 RID: 37751 RVA: 0x003D0D2C File Offset: 0x003CF12C
	public void OnParticleCollision()
	{
		MonoBehaviour.print("OnParticleCollision : " + base.name);
	}

	// Token: 0x06009378 RID: 37752 RVA: 0x003D0D43 File Offset: 0x003CF143
	public void FixedUpdate()
	{
		MonoBehaviour.print("FixedUpdate : " + base.name);
	}

	// Token: 0x06009379 RID: 37753 RVA: 0x003D0D5A File Offset: 0x003CF15A
	public void Update()
	{
		MonoBehaviour.print("Update : " + base.name);
	}

	// Token: 0x0600937A RID: 37754 RVA: 0x003D0D71 File Offset: 0x003CF171
	public void LateUpdate()
	{
		MonoBehaviour.print("LateUpdate : " + base.name);
	}

	// Token: 0x0600937B RID: 37755 RVA: 0x003D0D88 File Offset: 0x003CF188
	public void OnConnectedToServer()
	{
		MonoBehaviour.print("OnConnectedToServer : " + base.name);
	}

	// Token: 0x0600937C RID: 37756 RVA: 0x003D0D9F File Offset: 0x003CF19F
	public void OnDisconnectedFromServer()
	{
		MonoBehaviour.print("OnDisconnectedFromServer : " + base.name);
	}

	// Token: 0x0600937D RID: 37757 RVA: 0x003D0DB6 File Offset: 0x003CF1B6
	public void OnFailedToConnect()
	{
		MonoBehaviour.print("OnFailedToConnect : " + base.name);
	}

	// Token: 0x0600937E RID: 37758 RVA: 0x003D0DCD File Offset: 0x003CF1CD
	public void OnFailedToConnectToMasterServer()
	{
		MonoBehaviour.print("OnFailedToConnectToMasterServer : " + base.name);
	}

	// Token: 0x0600937F RID: 37759 RVA: 0x003D0DE4 File Offset: 0x003CF1E4
	public void OnMasterServerEvent()
	{
		MonoBehaviour.print("OnMasterServerEvent : " + base.name);
	}

	// Token: 0x06009380 RID: 37760 RVA: 0x003D0DFB File Offset: 0x003CF1FB
	public void OnPlayerConnected()
	{
		MonoBehaviour.print("OnPlayerConnected : " + base.name);
	}

	// Token: 0x06009381 RID: 37761 RVA: 0x003D0E12 File Offset: 0x003CF212
	public void OnPlayerDisconnected()
	{
		MonoBehaviour.print("OnPlayerDisconnected : " + base.name);
	}

	// Token: 0x06009382 RID: 37762 RVA: 0x003D0E29 File Offset: 0x003CF229
	public void OnServerInitialized()
	{
		MonoBehaviour.print("OnServerInitialized : " + base.name);
	}

	// Token: 0x06009383 RID: 37763 RVA: 0x003D0E40 File Offset: 0x003CF240
	public void OnMouseDown()
	{
		MonoBehaviour.print("OnMouseDown : " + base.name);
	}

	// Token: 0x06009384 RID: 37764 RVA: 0x003D0E57 File Offset: 0x003CF257
	public void OnMouseUp()
	{
		MonoBehaviour.print("OnMouseUp : " + base.name);
	}

	// Token: 0x06009385 RID: 37765 RVA: 0x003D0E6E File Offset: 0x003CF26E
	public void OnMouseUpAsButton()
	{
		MonoBehaviour.print("OnMouseUpAsButton : " + base.name);
	}

	// Token: 0x06009386 RID: 37766 RVA: 0x003D0E85 File Offset: 0x003CF285
	public void OnMouseDrag()
	{
		MonoBehaviour.print("OnMouseDrag : " + base.name);
	}

	// Token: 0x06009387 RID: 37767 RVA: 0x003D0E9C File Offset: 0x003CF29C
	public void OnMouseEnter()
	{
		MonoBehaviour.print("OnMouseEnter : " + base.name);
	}

	// Token: 0x06009388 RID: 37768 RVA: 0x003D0EB3 File Offset: 0x003CF2B3
	public void OnMouseExit()
	{
		MonoBehaviour.print("OnMouseExit : " + base.name);
	}

	// Token: 0x06009389 RID: 37769 RVA: 0x003D0ECA File Offset: 0x003CF2CA
	public void OnMouseOver()
	{
		MonoBehaviour.print("OnMouseOver : " + base.name);
	}

	// Token: 0x0600938A RID: 37770 RVA: 0x003D0EE1 File Offset: 0x003CF2E1
	public void OnControllerColliderHit(ControllerColliderHit hit)
	{
		MonoBehaviour.print("OnControllerColliderHit : " + hit);
	}

	// Token: 0x0600938B RID: 37771 RVA: 0x003D0EF3 File Offset: 0x003CF2F3
	public void OnTriggerEnter(Collider col)
	{
		MonoBehaviour.print("OnTriggerEnter : " + col);
	}

	// Token: 0x0600938C RID: 37772 RVA: 0x003D0F05 File Offset: 0x003CF305
	public void OnTriggerExit(Collider col)
	{
		MonoBehaviour.print("OnTriggerExit : " + col);
	}

	// Token: 0x0600938D RID: 37773 RVA: 0x003D0F17 File Offset: 0x003CF317
	public void OnTriggerStay(Collider col)
	{
		MonoBehaviour.print("OnTriggerStay : " + col);
	}

	// Token: 0x0600938E RID: 37774 RVA: 0x003D0F29 File Offset: 0x003CF329
	public void OnCollisionEnter(Collision col)
	{
		MonoBehaviour.print("OnCollisionEnter : " + col.gameObject);
	}

	// Token: 0x0600938F RID: 37775 RVA: 0x003D0F40 File Offset: 0x003CF340
	public void OnCollisionExit(Collision col)
	{
		MonoBehaviour.print("OnCollisionExit : " + col.gameObject);
	}

	// Token: 0x06009390 RID: 37776 RVA: 0x003D0F57 File Offset: 0x003CF357
	public void OnCollisionStay(Collision col)
	{
		MonoBehaviour.print("OnCollisionStay : " + col.gameObject);
	}

	// Token: 0x06009391 RID: 37777 RVA: 0x003D0F6E File Offset: 0x003CF36E
	public void OnTriggerEnter2D(Collider2D col)
	{
		MonoBehaviour.print("OnTriggerEnter2D : " + col);
	}

	// Token: 0x06009392 RID: 37778 RVA: 0x003D0F80 File Offset: 0x003CF380
	public void OnTriggerExit2D(Collider2D col)
	{
		MonoBehaviour.print("OnTriggerExit2D : " + col);
	}

	// Token: 0x06009393 RID: 37779 RVA: 0x003D0F92 File Offset: 0x003CF392
	public void OnTriggerStay2D(Collider2D col)
	{
		MonoBehaviour.print("OnTriggerStay2D : " + col);
	}

	// Token: 0x06009394 RID: 37780 RVA: 0x003D0FA4 File Offset: 0x003CF3A4
	public void OnCollisionEnter2D(Collision2D col)
	{
		MonoBehaviour.print("OnCollisionEnter2D : " + col.gameObject);
	}

	// Token: 0x06009395 RID: 37781 RVA: 0x003D0FBB File Offset: 0x003CF3BB
	public void OnCollisionExit2D(Collision2D col)
	{
		MonoBehaviour.print("OnCollisionExit2D : " + col.gameObject);
	}

	// Token: 0x06009396 RID: 37782 RVA: 0x003D0FD2 File Offset: 0x003CF3D2
	public void OnCollisionStay2D(Collision2D col)
	{
		MonoBehaviour.print("OnCollisionStay2D : " + col.gameObject);
	}

	// Token: 0x06009397 RID: 37783 RVA: 0x003D0FE9 File Offset: 0x003CF3E9
	public void OnPreCull()
	{
		MonoBehaviour.print("OnPreCull : " + base.name);
	}

	// Token: 0x06009398 RID: 37784 RVA: 0x003D1000 File Offset: 0x003CF400
	public void OnBecameVisible()
	{
		MonoBehaviour.print("OnBecameVisible : " + base.name);
	}

	// Token: 0x06009399 RID: 37785 RVA: 0x003D1017 File Offset: 0x003CF417
	public void OnBecameInvisible()
	{
		MonoBehaviour.print("OnBecameInvisible : " + base.name);
	}

	// Token: 0x0600939A RID: 37786 RVA: 0x003D102E File Offset: 0x003CF42E
	public void OnWillRenderObject()
	{
		MonoBehaviour.print("OnWillRenderObject : " + base.name);
	}

	// Token: 0x0600939B RID: 37787 RVA: 0x003D1045 File Offset: 0x003CF445
	public void OnPreRender()
	{
		MonoBehaviour.print("OnPreRender : " + base.name);
	}

	// Token: 0x0600939C RID: 37788 RVA: 0x003D105C File Offset: 0x003CF45C
	public void OnRenderObject()
	{
		MonoBehaviour.print("OnRenderObject : " + base.name);
	}

	// Token: 0x0600939D RID: 37789 RVA: 0x003D1073 File Offset: 0x003CF473
	public void OnPostRender()
	{
		MonoBehaviour.print("OnPostRender : " + base.name);
	}

	// Token: 0x0600939E RID: 37790 RVA: 0x003D108A File Offset: 0x003CF48A
	public void OnRenderImage(RenderTexture src, RenderTexture dest)
	{
		MonoBehaviour.print("OnRenderImage : " + base.name);
		MonoBehaviour.print("src : " + src);
		MonoBehaviour.print("dest : " + dest);
	}

	// Token: 0x0600939F RID: 37791 RVA: 0x003D10C1 File Offset: 0x003CF4C1
	public void OnGUI()
	{
		MonoBehaviour.print("OnGUI : " + base.name);
	}

	// Token: 0x060093A0 RID: 37792 RVA: 0x003D10D8 File Offset: 0x003CF4D8
	public void OnDrawGizmos()
	{
		MonoBehaviour.print("OnDrawGizmos : " + base.name);
	}

	// Token: 0x060093A1 RID: 37793 RVA: 0x003D10EF File Offset: 0x003CF4EF
	public void OnDrawGizmosSelected()
	{
		MonoBehaviour.print("OnDrawGizmosSelected : " + base.name);
	}
}
