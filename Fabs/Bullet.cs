using Godot;
using System;

public class Bullet : Area
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";
	
	float Speed = 15f;
	Vector3 velocity = new Vector3();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}
	
	public void start(Transform xform) {
		this.Transform = xform;
		velocity = -Transform.basis.z * Speed;
	}
	

	public override void _Process(float delta) {
		var newOrigin = Transform.origin + velocity * delta;
		
		Transform = new Transform(Transform.basis, newOrigin);	

	}
	
	private void _on_Timer_timeout()
	{
		QueueFree();
	}
	
	private void _on_Spatial_body_entered(object body)
	{
		if( body is StaticBody) {
			QueueFree();
		}
		
	}

}






