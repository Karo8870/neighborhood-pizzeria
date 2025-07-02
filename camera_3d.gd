extends Camera3D

@export var mouse_sensitivity := 0.1
@export var move_speed := 5.0

# Camera node path (drag in from the editor or use $Camera3D)
@onready var cam = $"."

var yaw := 0.0
var pitch := 0.0

func _ready():
	Input.set_mouse_mode(Input.MOUSE_MODE_CAPTURED)

func _unhandled_input(event):
	if event is InputEventMouseMotion:
		yaw -= event.relative.x * mouse_sensitivity
		pitch -= event.relative.y * mouse_sensitivity
		pitch = clamp(pitch, -90, 90)

		# Rotate the camera up/down
		cam.rotation_degrees.x = pitch
		# Rotate the body left/right
		rotation_degrees.y = yaw

func _physics_process(delta):
	var direction = Vector3.ZERO

	if Input.is_action_pressed("move_forward"):
		direction -= transform.basis.z
	if Input.is_action_pressed("move_back"):
		direction += transform.basis.z
	if Input.is_action_pressed("move_left"):
		direction -= transform.basis.x
	if Input.is_action_pressed("move_right"):
		direction += transform.basis.x

	direction.y = 0
	#velocity = direction.normalized() * move_speed
	#move_and_slide()
