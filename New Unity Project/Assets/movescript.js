#pragma strict
public var movingto = Vector2(0,0);
public var domove = false;
function Start () {
	domove = false;
}

function Update () {
	var tilesize : float = 1;
	if(Input.GetMouseButton(0) && domove == false) {

		var tilex = Mathf.Floor(Camera.main.ScreenToWorldPoint(Input.mousePosition).x / tilesize);
		var tiley = Mathf.Floor(Camera.main.ScreenToWorldPoint(Input.mousePosition).y / tilesize);
		
		var playertilex = Mathf.Floor(transform.position.x / tilesize);
		var playertiley = Mathf.Floor(transform.position.y / tilesize);
		
		if(Mathf.Abs(playertilex - tilex)+Mathf.Abs(playertiley-tiley) == 1)
		{
			movingto = Vector2(tilex,tiley);
			domove = true;
		}
		
		Debug.Log(tilex + " " + playertilex);
	}
	
	var movespeed : float = 2.0;
	
	if(!domove){
	}
	else if(Vector2.Distance((movingto*tilesize)+Vector2(tilesize/2,tilesize/2),Vector2(transform.position.x,transform.position.y)) < movespeed*Time.deltaTime) {
		Debug.Log(transform.position.x);
		transform.position = ((movingto*tilesize)+Vector2(tilesize/2,tilesize/2));
		Debug.Log(transform.position.x);
		domove = false;
	}
	else {
		Debug.Log((movingto*tilesize)+Vector2(tilesize/2,tilesize/2));
		var directionvector : Vector2 = (movingto*tilesize)+Vector2(tilesize/2,tilesize/2)-Vector2(transform.position.x,transform.position.y);
		directionvector.Normalize();
		transform.Translate(movespeed*directionvector*Time.deltaTime);
	}
}