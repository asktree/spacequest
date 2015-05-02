#pragma strict
public var zoomspeed : float = 0.1;
public var movespeed : float = 0.05;

function Update() {
	//Horizontal/Vertical Camera is a custom input axis set in the project settings
	var move = Input.GetAxisRaw("Horizontal Camera")*Vector2.right;
	move += Input.GetAxisRaw("Vertical Camera")*Vector2.up;
	move = Vector3.Normalize(move);
	//camera moves faster when farther away
	var speed = movespeed*GetComponent.<Camera>().orthographicSize;
	transform.position += move*speed;
}


//I do this instead of using getaxis Mouse ScrollWheel because
//getaxis does not work for touchpads. unfortunately, getaxis is less jerky imo
//might want to find a way to default to getaxis. 
function OnGUI() {
	//apparently using just camera.orthographicSize is deprecated....
	if(Event.current.type == EventType.ScrollWheel)
		GetComponent.<Camera>().orthographicSize += 
        Event.current.delta.y*zoomspeed;
    if (GetComponent.<Camera>().orthographicSize < 1)
     	GetComponent.<Camera>().orthographicSize = 1;
 	else if (GetComponent.<Camera>().orthographicSize > 25)
 		GetComponent.<Camera>().orthographicSize = 25;
 		
}
