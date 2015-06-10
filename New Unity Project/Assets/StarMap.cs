using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StarMap : MonoBehaviour {
	public UnityEngine.Object BeaconPrefab;
	public UnityEngine.Object ConnectorPrefab;
	
	public class Beacon {
		public string name;
		public Vector2 position;
		public List<string> connections;
		public Color color;
		
		public Beacon(string n, string[] cons, Vector2 pos, Color col) {
			this.name = n;
			this.connections = new List<string>();
			this.connections.AddRange(cons);
			this.position = pos;
			this.color = col;
		}
	}
	private Dictionary<string,Beacon> beacons = new Dictionary<string,Beacon>(); //name, beacon

	// Use this for initialization
	void Start () {
		string[] wangcons = new string[]{"dick","chef"};
		string[] dickcons = new string[]{"wang"};
		string[] chefcons = new string[]{"wang"};

		beacons.Add("wang",new Beacon("wang", wangcons, new Vector2(4.5f,3.8f),new Color(.25F,.3F,.6F,1F)));
		beacons.Add("dick",new Beacon("dick", dickcons, new Vector2(200.0f,100.0f),new Color(1F,.3F,.2F,1F)));
		beacons.Add ("chef", new Beacon ("chef", chefcons, new Vector2 (-100f, 50f), new Color (0F, .7F, 0F, 1F)));
		
		HashSet<string> connectedTo = new HashSet<string>();
		foreach(Beacon i in beacons.Values)
		{
			var star = Instantiate(BeaconPrefab, i.position, Quaternion.identity) as GameObject;
			star.transform.SetParent(gameObject.transform,false);
			star.GetComponentInChildren<UnityEngine.UI.Text>().text = i.name;
			star.GetComponent<UnityEngine.UI.Image>().color = i.color;
			
			
			foreach(string j in i.connections)
			{
				if(beacons.ContainsKey(j) && !connectedTo.Contains(j+i.name))
				{
					connectedTo.Add(i.name+j);
					Beacon jitem = beacons[j];
					Vector2 dx = jitem.position-i.position;
					var connector = Instantiate(ConnectorPrefab, i.position+dx/2, Quaternion.identity) as GameObject;
					connector.transform.SetParent(gameObject.transform,false);
					print(connector.GetComponent<RectTransform>().sizeDelta);
					Vector2 endpt = jitem.position-dx.normalized*(dx.magnitude%20)/2;
					Vector3 endpt3 = new Vector3(endpt.x,endpt.y,0f);
					((UILineConnector)connector.GetComponent("UILineConnector")).targetPoint = endpt3;
				}
				
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
