using Godot;
using System;

public class CSGRoom : Spatial
{

    // Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Input.SetMouseMode(Input.MouseMode.Captured);
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


}
