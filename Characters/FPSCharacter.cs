using Godot;
using System;

public class FPSCharacter : KinematicBody
{
    float gravity = -30f;
    float maxSpeed = 8f;
    float mouse_sensitivity = 0.002f; // radians/pixel
    float jumpSpeed = 12f;
    bool jump = false;
    Vector3 velocity = new Vector3();

    Camera  camera; 
    Spatial pivot;


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        camera = GetNode<Camera>("Pivot/Camera");
        pivot = GetNode<Spatial>("Pivot");
    }

    private Vector3 GetInput() {
        Vector3 inputDir = new Vector3();

        if(Input.IsActionJustPressed("jump")) {
            jump = true;
        }

        if (Input.IsActionPressed("move_forward")) {
			inputDir += -camera.GlobalTransform.basis.z;
		}
		if (Input.IsActionPressed("move_back")) {
			inputDir += camera.GlobalTransform.basis.z;
		}
		if (Input.IsActionPressed("move_right")) {
			inputDir += camera.GlobalTransform.basis.x;
		}
		if (Input.IsActionPressed("move_left")) {
			inputDir += -camera.GlobalTransform.basis.x;
		}
        inputDir = inputDir.Normalized();
        return inputDir;
    }

    public override void  _UnhandledInput(InputEvent inputEvent) {

        if(inputEvent is InputEventMouseMotion eventMouseMotion && Input.GetMouseMode() == Input.MouseMode.Captured) {
            RotateY(-eventMouseMotion.Relative.x * mouse_sensitivity);
            pivot.RotateX(-eventMouseMotion.Relative.y * mouse_sensitivity);

            // stop camera from going too far up or down
            float clamped = Mathf.Clamp(pivot.Rotation.x, -1.2f, 1.2f);
            Vector3 clampedRotation = new Vector3(clamped, pivot.Rotation.y,  pivot.Rotation.z);
            pivot.Rotation = clampedRotation;
        }

    }

    public override void _PhysicsProcess(float delta)
    {
        // gravity
        velocity.y += gravity * delta;

        // movement on the floor
        Vector3 desiredVelocity = GetInput() * maxSpeed;
        velocity.x = desiredVelocity.x;
        velocity.z = desiredVelocity.z;

        // check jump
        if( jump && IsOnFloor()) {
            velocity.y = jumpSpeed;
            jump = false;
        }

        // apply movement
        velocity = MoveAndSlide(velocity, Vector3.Up, true);

        
        
    }

}
