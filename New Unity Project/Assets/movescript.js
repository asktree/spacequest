#pragma strict

var speed : float = 1.0;
var rb : Rigidbody2D;
var handrange : float = 1.0;

function Start() {
	rb = GetComponent.<Rigidbody2D>();
	rb.fixedAngle = true;
}

function GetHandObj() {
	var hit : RaycastHit;
	var pt = Camera.main.ScreenToWorldPoint(Input.mousePosition);
	pt.z = 0;
	if (Vector2.Distance(pt, transform.position) > handrange){
		pt = transform.position + 
					 Vector3.Normalize(pt - transform.position) * handrange;
		}

	return(Physics2D.OverlapPoint(pt));
}

function Update () {
	var move = Input.GetAxisRaw("Horizontal")*Vector2.right;
	move += Input.GetAxisRaw("Vertical")*Vector2.up;
	move = Vector3.Normalize(move);
	move = Camera.main.transform.TransformDirection(move);
	rb.velocity= move*speed;

	if (Input.GetKeyDown ("e")) {
		print(GetHandObj());
	}
}