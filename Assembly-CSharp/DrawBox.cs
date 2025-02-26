using System;
using System.Collections;
using UnityEngine;
using Vectrosity;

// Token: 0x0200067E RID: 1662
public class DrawBox : MonoBehaviour
{
	// Token: 0x060026F7 RID: 9975 RVA: 0x000E19E0 File Offset: 0x000DFDE0
	private IEnumerator Start()
	{
		base.GetComponent<Renderer>().enabled = false;
		this.rigidbodies = (UnityEngine.Object.FindObjectsOfType(typeof(Rigidbody)) as Rigidbody[]);
		VectorLine.canvas.planeDistance = 0.5f;
		yield return null;
		VectorLine.SetCanvasCamera(this.vectorCam);
		yield break;
	}

	// Token: 0x060026F8 RID: 9976 RVA: 0x000E19FC File Offset: 0x000DFDFC
	private void Update()
	{
		Vector3 mousePosition = Input.mousePosition;
		mousePosition.z = 1f;
		Vector3 position = Camera.main.ScreenToWorldPoint(mousePosition);
		if (Input.GetMouseButtonDown(0) && this.canClick)
		{
			base.GetComponent<Renderer>().enabled = true;
			base.transform.position = position;
			this.mouseDown = true;
		}
		if (this.mouseDown)
		{
			base.transform.localScale = new Vector3(position.x - base.transform.position.x, position.y - base.transform.position.y, 1f);
		}
		if (Input.GetMouseButtonUp(0))
		{
			this.mouseDown = false;
			this.boxDrawn = true;
		}
		base.transform.Translate(-Vector3.up * Time.deltaTime * this.moveSpeed * Input.GetAxis("Vertical"));
		base.transform.Translate(Vector3.right * Time.deltaTime * this.moveSpeed * Input.GetAxis("Horizontal"));
	}

	// Token: 0x060026F9 RID: 9977 RVA: 0x000E1B38 File Offset: 0x000DFF38
	private void OnGUI()
	{
		GUI.Box(new Rect(20f, 20f, 320f, 38f), "Draw a box by clicking and dragging with the mouse\nMove the drawn box with the arrow keys");
		Rect position = new Rect(20f, 62f, 60f, 30f);
		this.canClick = !position.Contains(Event.current.mousePosition);
		if (this.boxDrawn && GUI.Button(position, "Boom!"))
		{
			foreach (Rigidbody rigidbody in this.rigidbodies)
			{
				rigidbody.AddExplosionForce(this.explodePower, new Vector3(0f, -6.5f, -1.5f), 20f, 0f, ForceMode.VelocityChange);
			}
		}
	}

	// Token: 0x04002737 RID: 10039
	public float moveSpeed = 1f;

	// Token: 0x04002738 RID: 10040
	public float explodePower = 20f;

	// Token: 0x04002739 RID: 10041
	public Camera vectorCam;

	// Token: 0x0400273A RID: 10042
	private bool mouseDown;

	// Token: 0x0400273B RID: 10043
	private Rigidbody[] rigidbodies;

	// Token: 0x0400273C RID: 10044
	private bool canClick = true;

	// Token: 0x0400273D RID: 10045
	private bool boxDrawn;
}
