using System;
using UnityEngine;

// Token: 0x020002F7 RID: 759
public class GameController : MonoBehaviour
{
	// Token: 0x06000D28 RID: 3368 RVA: 0x0003763C File Offset: 0x00035A3C
	private void Update()
	{
		this.m_Player.transform.Rotate(new Vector3(0f, Input.GetAxis("Horizontal") * Time.deltaTime * 200f, 0f));
		this.m_Player.transform.Translate(base.transform.forward * Input.GetAxis("Vertical") * Time.deltaTime * 4f);
	}

	// Token: 0x06000D29 RID: 3369 RVA: 0x000376BC File Offset: 0x00035ABC
	private void OnGUI()
	{
		GUI.Label(new Rect(50f, 50f, 200f, 20f), "Press arrow key to move");
		Animation componentInChildren = this.m_Player.GetComponentInChildren<Animation>();
		componentInChildren.enabled = GUI.Toggle(new Rect(50f, 70f, 200f, 20f), componentInChildren.enabled, "Play Animation");
		DynamicBone[] components = this.m_Player.GetComponents<DynamicBone>();
		GUI.Label(new Rect(50f, 100f, 200f, 20f), "Choose dynamic bone:");
		Behaviour behaviour = components[0];
		bool enabled = GUI.Toggle(new Rect(50f, 120f, 100f, 20f), components[0].enabled, "Breasts");
		components[1].enabled = enabled;
		behaviour.enabled = enabled;
		components[2].enabled = GUI.Toggle(new Rect(50f, 140f, 100f, 20f), components[2].enabled, "Tail");
	}

	// Token: 0x04000C48 RID: 3144
	public GameObject m_Player;
}
