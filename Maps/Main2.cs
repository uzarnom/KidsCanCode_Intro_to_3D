using Godot;
using System;

public class Main2 : Spatial
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Input.SetMouseMode(Input.MouseMode.Captured);
	}
	
	public override void _Process(float delta) {
		//$CameraHub.rotate_y(0.01);
		// GetNode<Spatial>("CameraHub").RotateY(0.01f);
	}

    public override void _Input(InputEvent @event)
    {
        if (Input.IsActionPressed("ui_cancel")) {
			Input.SetMouseMode(Input.MouseMode.Visible);
		}

		if(Input.IsActionPressed("shoot")) {
			if( Input.GetMouseMode() == Input.MouseMode.Visible) {
				Input.SetMouseMode(Input.MouseMode.Captured);
				GetTree().SetInputAsHandled();
			}
		}
    }
    // https://www.youtube.com/watch?v=ickZ_Genr7A&t=85s


    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    
		// https://www.youtube.com/watch?v=ickZ_Genr7A&t=85s
		
		
//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
