#pragma strict

var speed : float = 1.0;
var rb : Rigidbody2D;

function Start() {
	rb = GetComponent.<Rigidbody2D>();
	rb.fixedAngle = true;
}


function Update () {
	var move = Input.GetAxisRaw("Horizontal")*Vector2.right;
	move += Input.GetAxisRaw("Vertical")*Vector2.up;
	move = Vector3.Normalize(move);
	rb.velocity= move*speed;
}