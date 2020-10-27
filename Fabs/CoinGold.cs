using Godot;
using System;

public class CoinGold : Area
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}


	private void _on_CoinGold_body_entered(object body)
	{
		QueueFree();
	}

}


