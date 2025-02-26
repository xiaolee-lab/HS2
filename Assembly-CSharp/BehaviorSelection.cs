using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using BehaviorDesigner.Runtime;
using UnityEngine;

// Token: 0x020000B2 RID: 178
public class BehaviorSelection : MonoBehaviour
{
	// Token: 0x06000421 RID: 1057 RVA: 0x00018A6C File Offset: 0x00016E6C
	public void Start()
	{
		BehaviorTree[] components = this.mainBot.GetComponents<BehaviorTree>();
		for (int i = 0; i < components.Length; i++)
		{
			this.behaviorTreeGroup.Add(components[i].Group, components[i]);
		}
		components = Camera.main.GetComponents<BehaviorTree>();
		for (int j = 0; j < components.Length; j++)
		{
			this.behaviorTreeGroup.Add(components[j].Group, components[j]);
		}
		this.flockGroupPosition = new Vector3[this.flockGroup.transform.childCount];
		this.flockGroupRotation = new Quaternion[this.flockGroup.transform.childCount];
		for (int k = 0; k < this.flockGroup.transform.childCount; k++)
		{
			this.flockGroup.transform.GetChild(k).gameObject.SetActive(false);
			this.flockGroupPosition[k] = this.flockGroup.transform.GetChild(k).transform.position;
			this.flockGroupRotation[k] = this.flockGroup.transform.GetChild(k).transform.rotation;
		}
		this.followGroupPosition = new Vector3[this.followGroup.transform.childCount];
		this.followGroupRotation = new Quaternion[this.followGroup.transform.childCount];
		for (int l = 0; l < this.followGroup.transform.childCount; l++)
		{
			this.followGroup.transform.GetChild(l).gameObject.SetActive(false);
			this.followGroupPosition[l] = this.followGroup.transform.GetChild(l).transform.position;
			this.followGroupRotation[l] = this.followGroup.transform.GetChild(l).transform.rotation;
		}
		this.queueGroupPosition = new Vector3[this.queueGroup.transform.childCount];
		this.queueGroupRotation = new Quaternion[this.queueGroup.transform.childCount];
		for (int m = 0; m < this.queueGroup.transform.childCount; m++)
		{
			this.queueGroup.transform.GetChild(m).gameObject.SetActive(false);
			this.queueGroupPosition[m] = this.queueGroup.transform.GetChild(m).transform.position;
			this.queueGroupRotation[m] = this.queueGroup.transform.GetChild(m).transform.rotation;
		}
		this.SelectionChanged();
	}

	// Token: 0x06000422 RID: 1058 RVA: 0x00018D60 File Offset: 0x00017160
	public void OnGUI()
	{
		GUILayout.BeginVertical(new GUILayoutOption[]
		{
			GUILayout.Width(300f)
		});
		GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
		if (GUILayout.Button("<-", Array.Empty<GUILayoutOption>()))
		{
			this.prevSelectionType = this.selectionType;
			this.selectionType = (this.selectionType - 1) % BehaviorSelection.BehaviorSelectionType.Last;
			if (this.selectionType < BehaviorSelection.BehaviorSelectionType.MoveTowards)
			{
				this.selectionType = BehaviorSelection.BehaviorSelectionType.Queue;
			}
			this.SelectionChanged();
		}
		GUILayout.Box(BehaviorSelection.SplitCamelCase(this.selectionType.ToString()), new GUILayoutOption[]
		{
			GUILayout.Width(220f)
		});
		if (GUILayout.Button("->", Array.Empty<GUILayoutOption>()))
		{
			this.prevSelectionType = this.selectionType;
			this.selectionType = (this.selectionType + 1) % BehaviorSelection.BehaviorSelectionType.Last;
			this.SelectionChanged();
		}
		GUILayout.EndHorizontal();
		GUILayout.Box(this.Description(), this.descriptionGUISkin.box, Array.Empty<GUILayoutOption>());
		if (this.selectionType == BehaviorSelection.BehaviorSelectionType.CanHearObject && GUILayout.Button("Play Sound", Array.Empty<GUILayoutOption>()))
		{
			this.marker.GetComponent<AudioSource>().Play();
		}
		GUILayout.EndVertical();
	}

	// Token: 0x06000423 RID: 1059 RVA: 0x00018E98 File Offset: 0x00017298
	private string Description()
	{
		string result = string.Empty;
		switch (this.selectionType)
		{
		case BehaviorSelection.BehaviorSelectionType.MoveTowards:
			result = "The Move Towards task will move the agent towards the target (without pathfinding). In this example the green agent is moving towards the red dot.";
			break;
		case BehaviorSelection.BehaviorSelectionType.RotateTowards:
			result = "The Rotate Towards task rotate the agent towards the target. In this example the green agent is rotating towards the red dot.";
			break;
		case BehaviorSelection.BehaviorSelectionType.Seek:
			result = "The Seek task will move the agent towards the target with pathfinding. In this example the green agent is seeking the red dot (which moves).";
			break;
		case BehaviorSelection.BehaviorSelectionType.Flee:
			result = "The Flee task will move the agent away from the target with pathfinding. In this example the green agent is fleeing from red dot (which moves).";
			break;
		case BehaviorSelection.BehaviorSelectionType.Pursue:
			result = "The Pursue task is similar to the Seek task except the Pursue task predicts where the target is going to be in the future. This allows the agent to arrive at the target earlier than it would have with the Seek task.";
			break;
		case BehaviorSelection.BehaviorSelectionType.Evade:
			result = "The Evade task is similar to the Flee task except the Evade task predicts where the target is going to be in the future. This allows the agent to flee from the target earlier than it would have with the Flee task.";
			break;
		case BehaviorSelection.BehaviorSelectionType.Follow:
			result = "The Follow task will allow the agent to continuously follow a GameObject. In this example the green agent is following the red dot.";
			break;
		case BehaviorSelection.BehaviorSelectionType.Patrol:
			result = "The Patrol task moves from waypoint to waypint. In this example the green agent is patrolling with four different waypoints (the white dots).";
			break;
		case BehaviorSelection.BehaviorSelectionType.Cover:
			result = "The Cover task will move the agent into cover from its current position. In this example the agent sees cover in front of it so takes cover behind the wall.";
			break;
		case BehaviorSelection.BehaviorSelectionType.Wander:
			result = "The Wander task moves the agent randomly throughout the map with pathfinding.";
			break;
		case BehaviorSelection.BehaviorSelectionType.Search:
			result = "The Search task will search the map by wandering until it finds the target. It can find the target by seeing or hearing the target. In this example the Search task is looking for the red dot.";
			break;
		case BehaviorSelection.BehaviorSelectionType.WithinDistance:
			result = "The Within Distance task is a conditional task that returns success when another object comes within distance of the current agent. In this example the Within Distance task is paired with the Seek task so the agent will move towards the target as soon as the target within distance.";
			break;
		case BehaviorSelection.BehaviorSelectionType.CanSeeObject:
			result = "The Can See Object task is a conditional task that returns success when it sees an object in front of the current agent. In this example the Can See Object task is paired with the Seek task so the agent will move towards the target as soon as the target is seen.";
			break;
		case BehaviorSelection.BehaviorSelectionType.CanHearObject:
			result = "The Can Hear Object task is a conditional task that returns success when it hears another object. Press the \"Play\" button to emit a sound from the red dot. If the red dot is within range of the agent when the sound is played then the agent will move towards the red dot with the Seek task.";
			break;
		case BehaviorSelection.BehaviorSelectionType.Flock:
			result = "The Flock task moves a group of objects together in a pattern (which can be adjusted). In this example the Flock task is controlling all 30 objects. There are no colliders on the objects - the Flock task is completing managing the position of all of the objects";
			break;
		case BehaviorSelection.BehaviorSelectionType.LeaderFollow:
			result = "The Leader Follow task moves a group of objects behind a leader object. There are two behavior trees running in this example - one for the leader (who is patrolling the area) and one for the group of objects. Again, there is are no colliders on the objects.";
			break;
		case BehaviorSelection.BehaviorSelectionType.Queue:
			result = "The Queue task will move a group of objects through a small space in an organized way. In this example the Queue task is controlling all of the objects. Another way to move all of the objects through the doorway is with the seek task, however with this approach the objects would group up at the doorway.";
			break;
		}
		return result;
	}

	// Token: 0x06000424 RID: 1060 RVA: 0x00018FC0 File Offset: 0x000173C0
	private static string SplitCamelCase(string s)
	{
		Regex regex = new Regex("(?<=[A-Z])(?=[A-Z][a-z])|(?<=[^A-Z])(?=[A-Z])|(?<=[A-Za-z])(?=[^A-Za-z])", RegexOptions.IgnorePatternWhitespace);
		s = regex.Replace(s, " ");
		return (char.ToUpper(s[0]) + s.Substring(1)).Trim();
	}

	// Token: 0x06000425 RID: 1061 RVA: 0x0001900C File Offset: 0x0001740C
	private void SelectionChanged()
	{
		this.DisableAll();
		switch (this.selectionType)
		{
		case BehaviorSelection.BehaviorSelectionType.MoveTowards:
			this.marker.transform.position = new Vector3(20f, 1f, -20f);
			this.marker.SetActive(true);
			this.mainBot.transform.position = new Vector3(-20f, 1f, -20f);
			this.mainBot.transform.eulerAngles = new Vector3(0f, 180f, 0f);
			this.mainBot.SetActive(true);
			break;
		case BehaviorSelection.BehaviorSelectionType.RotateTowards:
			this.marker.transform.position = new Vector3(20f, 1f, 10f);
			this.marker.SetActive(true);
			this.mainBot.transform.position = new Vector3(0f, 1f, -20f);
			this.mainBot.transform.eulerAngles = new Vector3(0f, 180f, 0f);
			this.mainBot.SetActive(true);
			break;
		case BehaviorSelection.BehaviorSelectionType.Seek:
			this.marker.transform.position = new Vector3(20f, 1f, 20f);
			this.marker.SetActive(true);
			this.marker.GetComponent<Animation>()["MarkerSeek"].time = 0f;
			this.marker.GetComponent<Animation>()["MarkerSeek"].speed = 1f;
			this.marker.GetComponent<Animation>().Play("MarkerSeek");
			this.mainBot.transform.position = new Vector3(-20f, 1f, -20f);
			this.mainBot.transform.eulerAngles = new Vector3(0f, 180f, 0f);
			this.mainBot.SetActive(true);
			break;
		case BehaviorSelection.BehaviorSelectionType.Flee:
			this.marker.transform.position = new Vector3(20f, 1f, 20f);
			this.marker.SetActive(true);
			this.marker.GetComponent<Animation>()["MarkerFlee"].time = 0f;
			this.marker.GetComponent<Animation>()["MarkerFlee"].speed = 1f;
			this.marker.GetComponent<Animation>().Play("MarkerFlee");
			this.mainBot.transform.position = new Vector3(10f, 1f, 18f);
			this.mainBot.transform.eulerAngles = new Vector3(0f, 180f, 0f);
			this.mainBot.SetActive(true);
			break;
		case BehaviorSelection.BehaviorSelectionType.Pursue:
			this.marker.transform.position = new Vector3(20f, 1f, 20f);
			this.marker.SetActive(true);
			this.marker.GetComponent<Animation>()["MarkerPersue"].time = 0f;
			this.marker.GetComponent<Animation>()["MarkerPersue"].speed = 1f;
			this.marker.GetComponent<Animation>().Play("MarkerPersue");
			this.mainBot.transform.position = new Vector3(-20f, 1f, 0f);
			this.mainBot.transform.eulerAngles = new Vector3(0f, 90f, 0f);
			this.mainBot.SetActive(true);
			break;
		case BehaviorSelection.BehaviorSelectionType.Evade:
			this.marker.transform.position = new Vector3(20f, 1f, 20f);
			this.marker.SetActive(true);
			this.marker.GetComponent<Animation>()["MarkerEvade"].time = 0f;
			this.marker.GetComponent<Animation>()["MarkerEvade"].speed = 1f;
			this.marker.GetComponent<Animation>().Play("MarkerEvade");
			this.mainBot.transform.position = new Vector3(0f, 1f, 18f);
			this.mainBot.transform.eulerAngles = new Vector3(0f, 180f, 0f);
			this.mainBot.SetActive(true);
			break;
		case BehaviorSelection.BehaviorSelectionType.Follow:
			this.marker.transform.position = new Vector3(20f, 1f, 20f);
			this.marker.SetActive(true);
			this.marker.GetComponent<Animation>()["MarkerFollow"].time = 0f;
			this.marker.GetComponent<Animation>()["MarkerFollow"].speed = 1f;
			this.marker.GetComponent<Animation>().Play("MarkerFollow");
			this.mainBot.transform.position = new Vector3(20f, 1f, 15f);
			this.mainBot.transform.eulerAngles = new Vector3(0f, 0f, 0f);
			this.mainBot.SetActive(true);
			break;
		case BehaviorSelection.BehaviorSelectionType.Patrol:
			for (int i = 0; i < this.waypoints.Length; i++)
			{
				this.waypoints[i].SetActive(true);
			}
			this.mainBot.transform.position = new Vector3(-20f, 1f, 20f);
			this.mainBot.transform.eulerAngles = new Vector3(0f, 180f, 0f);
			this.mainBot.SetActive(true);
			break;
		case BehaviorSelection.BehaviorSelectionType.Cover:
			this.mainBot.transform.position = new Vector3(-5f, 1f, -10f);
			this.mainBot.transform.eulerAngles = new Vector3(0f, 0f, 0f);
			this.mainBot.SetActive(true);
			break;
		case BehaviorSelection.BehaviorSelectionType.Wander:
			this.mainBot.transform.position = new Vector3(-20f, 1f, -20f);
			this.mainBot.transform.eulerAngles = new Vector3(0f, 0f, 0f);
			this.mainBot.SetActive(true);
			break;
		case BehaviorSelection.BehaviorSelectionType.Search:
			this.marker.transform.position = new Vector3(20f, 1f, 20f);
			this.marker.SetActive(true);
			this.mainBot.transform.position = new Vector3(-20f, 1f, -20f);
			this.mainBot.transform.eulerAngles = new Vector3(0f, 0f, 0f);
			this.mainBot.SetActive(true);
			break;
		case BehaviorSelection.BehaviorSelectionType.WithinDistance:
			this.marker.transform.position = new Vector3(20f, 1f, 20f);
			this.marker.GetComponent<Animation>()["MarkerPersue"].time = 0f;
			this.marker.GetComponent<Animation>()["MarkerPersue"].speed = 1f;
			this.marker.GetComponent<Animation>().Play("MarkerPersue");
			this.marker.SetActive(true);
			this.mainBot.transform.position = new Vector3(-15f, 1f, 2f);
			this.mainBot.transform.eulerAngles = new Vector3(0f, 0f, 0f);
			this.mainBot.SetActive(true);
			break;
		case BehaviorSelection.BehaviorSelectionType.CanSeeObject:
			this.marker.transform.position = new Vector3(20f, 1f, 20f);
			this.marker.GetComponent<Animation>()["MarkerPersue"].time = 0f;
			this.marker.GetComponent<Animation>()["MarkerPersue"].speed = 1f;
			this.marker.GetComponent<Animation>().Play("MarkerPersue");
			this.marker.SetActive(true);
			this.mainBot.transform.position = new Vector3(-15f, 1f, -10f);
			this.mainBot.transform.eulerAngles = new Vector3(0f, 0f, 0f);
			this.mainBot.SetActive(true);
			break;
		case BehaviorSelection.BehaviorSelectionType.CanHearObject:
			this.marker.transform.position = new Vector3(20f, 1f, 20f);
			this.marker.GetComponent<Animation>()["MarkerPersue"].time = 0f;
			this.marker.GetComponent<Animation>()["MarkerPersue"].speed = 1f;
			this.marker.GetComponent<Animation>().Play("MarkerPersue");
			this.marker.SetActive(true);
			this.mainBot.transform.position = new Vector3(-15f, 1f, -10f);
			this.mainBot.transform.eulerAngles = new Vector3(0f, 0f, 0f);
			this.mainBot.SetActive(true);
			break;
		case BehaviorSelection.BehaviorSelectionType.Flock:
			Camera.main.transform.position = new Vector3(0f, 90f, -150f);
			for (int j = 0; j < this.flockGroup.transform.childCount; j++)
			{
				this.flockGroup.transform.GetChild(j).gameObject.SetActive(true);
			}
			break;
		case BehaviorSelection.BehaviorSelectionType.LeaderFollow:
			for (int k = 0; k < this.waypointsA.Length; k++)
			{
				this.waypointsA[k].SetActive(true);
			}
			this.mainBot.transform.position = new Vector3(0f, 1f, -130f);
			this.mainBot.SetActive(true);
			Camera.main.transform.position = new Vector3(0f, 90f, -150f);
			for (int l = 0; l < this.followGroup.transform.childCount; l++)
			{
				this.followGroup.transform.GetChild(l).gameObject.SetActive(true);
			}
			break;
		case BehaviorSelection.BehaviorSelectionType.Queue:
			this.marker.transform.position = new Vector3(45f, 1f, 0f);
			for (int m = 0; m < this.queueGroup.transform.childCount; m++)
			{
				this.queueGroup.transform.GetChild(m).gameObject.SetActive(true);
			}
			break;
		}
		base.StartCoroutine("EnableBehavior");
	}

	// Token: 0x06000426 RID: 1062 RVA: 0x00019BC8 File Offset: 0x00017FC8
	private void DisableAll()
	{
		base.StopCoroutine("EnableBehavior");
		this.behaviorTreeGroup[(int)this.prevSelectionType].DisableBehavior();
		if (this.prevSelectionType == BehaviorSelection.BehaviorSelectionType.LeaderFollow)
		{
			this.behaviorTreeGroup[17].DisableBehavior();
		}
		this.marker.GetComponent<Animation>().Stop();
		this.marker.SetActive(false);
		this.mainBot.SetActive(false);
		Camera.main.transform.position = new Vector3(0f, 90f, 0f);
		for (int i = 0; i < this.flockGroup.transform.childCount; i++)
		{
			this.flockGroup.transform.GetChild(i).gameObject.SetActive(false);
			this.flockGroup.transform.GetChild(i).transform.position = this.flockGroupPosition[i];
			this.flockGroup.transform.GetChild(i).transform.rotation = this.flockGroupRotation[i];
		}
		for (int j = 0; j < this.followGroup.transform.childCount; j++)
		{
			this.followGroup.transform.GetChild(j).gameObject.SetActive(false);
			this.followGroup.transform.GetChild(j).transform.position = this.followGroupPosition[j];
			this.followGroup.transform.GetChild(j).transform.rotation = this.followGroupRotation[j];
		}
		for (int k = 0; k < this.queueGroup.transform.childCount; k++)
		{
			this.queueGroup.transform.GetChild(k).gameObject.SetActive(false);
			this.queueGroup.transform.GetChild(k).transform.position = this.queueGroupPosition[k];
			this.queueGroup.transform.GetChild(k).transform.rotation = this.queueGroupRotation[k];
		}
		for (int l = 0; l < this.waypoints.Length; l++)
		{
			this.waypoints[l].SetActive(false);
		}
		for (int m = 0; m < this.waypointsA.Length; m++)
		{
			this.waypointsA[m].SetActive(false);
		}
	}

	// Token: 0x06000427 RID: 1063 RVA: 0x00019E74 File Offset: 0x00018274
	private IEnumerator EnableBehavior()
	{
		yield return new WaitForSeconds(0.5f);
		this.behaviorTreeGroup[(int)this.selectionType].EnableBehavior();
		if (this.selectionType == BehaviorSelection.BehaviorSelectionType.LeaderFollow)
		{
			this.behaviorTreeGroup[17].EnableBehavior();
		}
		yield break;
	}

	// Token: 0x0400034A RID: 842
	public GameObject marker;

	// Token: 0x0400034B RID: 843
	public GameObject mainBot;

	// Token: 0x0400034C RID: 844
	public GameObject flockGroup;

	// Token: 0x0400034D RID: 845
	public GameObject followGroup;

	// Token: 0x0400034E RID: 846
	public GameObject queueGroup;

	// Token: 0x0400034F RID: 847
	public GameObject[] waypoints;

	// Token: 0x04000350 RID: 848
	public GameObject[] waypointsA;

	// Token: 0x04000351 RID: 849
	public GUISkin descriptionGUISkin;

	// Token: 0x04000352 RID: 850
	private Vector3[] flockGroupPosition;

	// Token: 0x04000353 RID: 851
	private Vector3[] followGroupPosition;

	// Token: 0x04000354 RID: 852
	private Vector3[] queueGroupPosition;

	// Token: 0x04000355 RID: 853
	private Quaternion[] flockGroupRotation;

	// Token: 0x04000356 RID: 854
	private Quaternion[] followGroupRotation;

	// Token: 0x04000357 RID: 855
	private Quaternion[] queueGroupRotation;

	// Token: 0x04000358 RID: 856
	private Dictionary<int, BehaviorTree> behaviorTreeGroup = new Dictionary<int, BehaviorTree>();

	// Token: 0x04000359 RID: 857
	private BehaviorSelection.BehaviorSelectionType selectionType;

	// Token: 0x0400035A RID: 858
	private BehaviorSelection.BehaviorSelectionType prevSelectionType;

	// Token: 0x020000B3 RID: 179
	private enum BehaviorSelectionType
	{
		// Token: 0x0400035C RID: 860
		MoveTowards,
		// Token: 0x0400035D RID: 861
		RotateTowards,
		// Token: 0x0400035E RID: 862
		Seek,
		// Token: 0x0400035F RID: 863
		Flee,
		// Token: 0x04000360 RID: 864
		Pursue,
		// Token: 0x04000361 RID: 865
		Evade,
		// Token: 0x04000362 RID: 866
		Follow,
		// Token: 0x04000363 RID: 867
		Patrol,
		// Token: 0x04000364 RID: 868
		Cover,
		// Token: 0x04000365 RID: 869
		Wander,
		// Token: 0x04000366 RID: 870
		Search,
		// Token: 0x04000367 RID: 871
		WithinDistance,
		// Token: 0x04000368 RID: 872
		CanSeeObject,
		// Token: 0x04000369 RID: 873
		CanHearObject,
		// Token: 0x0400036A RID: 874
		Flock,
		// Token: 0x0400036B RID: 875
		LeaderFollow,
		// Token: 0x0400036C RID: 876
		Queue,
		// Token: 0x0400036D RID: 877
		Last
	}
}
