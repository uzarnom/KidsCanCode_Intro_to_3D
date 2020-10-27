using Godot;
using System;

public class Character : KinematicBody
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";
	Vector3 gravity = Vector3.Down * 9.8f;
	float speed = 4f;
	float jumpSpeed = 6f;
	float spin = 0.1f;
	bool jump = false;
	bool canMove = true;
	
	Vector3 velocity = new Vector3();

	RayCast rayCast;
	
	PackedScene bullet = (PackedScene)ResourceLoader.Load("res://Fabs/Bullet.tscn");

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		rayCast = GetNode<RayCast>("RayCast");
	}
	
	public override void _PhysicsProcess(float delta) {
		velocity += gravity * delta;
		getInput();
		velocity = MoveAndSlide(velocity, Vector3.Up);
		if (jump && IsOnFloor()) {
			velocity.y = jumpSpeed;
		}
	}

	private void getInput() {
		// velocity.x = 0;
		// velocity.z = 0;

		if(!canMove) {
			return;
		}

		float curY = velocity.y;
		velocity = new Vector3();
		
		if (Input.IsActionPressed("move_forward")) {
			if( IsOnFloor()  && !rayCast.IsColliding()) {

			} else {
				velocity += -Transform.basis.z * speed;
			}
			//velocity.z -= speed;
			
		}
		if (Input.IsActionPressed("move_back")) {
			// velocity.z += speed;
			velocity += Transform.basis.z * speed;
		}
		if (Input.IsActionPressed("move_right")) {
			// velocity.x += speed;
			velocity += Transform.basis.x * speed;
		}
		if (Input.IsActionPressed("move_left")) {
			// velocity.x -= speed;
			velocity += -Transform.basis.x * speed;
			//GD.Print(Transform.GetType());
		}
		
		
		// reapply gravity
		velocity.y = curY;
		jump = false;
		
		if (Input.IsActionPressed("jump")) {
			jump = true;
		}
		
	}
	
	public override void _UnhandledInput(InputEvent inputEvent) {
		if (inputEvent is InputEventMouseMotion eventMouseMotion && Input.GetMouseMode() == Input.MouseMode.Captured) {
			RotateY(-Mathf.Lerp(0, spin, eventMouseMotion.Relative.x/10));
		}
		
		if (Input.IsActionPressed("shoot")) {
			
			// spawn position
			Position3D spawnPos = GetNode<Position3D>("Muz");

			// bullet instance
			Bullet b = (Bullet)bullet.Instance();
			b.start(spawnPos.GlobalTransform);
			
			GetParent().AddChild(b);
		}
	}

	async public void takeDamage() {
		velocity *= -1;
		velocity.y = jumpSpeed;
		canMove = false;
		SceneTreeTimer newTime = GetTree().CreateTimer(1);
		await ToSignal(newTime, "timeout");
		canMove = true;
	}

}
