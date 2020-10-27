using Godot;
using System;

public class spikes : Area
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

	private void OnEntered(int body_id, object body, int body_shape, int area_shape) {
		GD.Print("Entered");
		Character player = (Character)body;
		
		if ( player.HasMethod("takeDamage") ) {
			player.takeDamage();
		}
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
