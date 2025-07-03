extends Camera3D

@onready var ray = $RayCast3D

var is_coliding := false

func _physics_process(delta: float) -> void:
	if ray.is_colliding():
		is_coliding = true
		var obj = ray.get_collider() as Node3D
		
		if not obj:
			return
		
		if obj.has_meta("hint"):
			%Label.text = obj.get_meta("hint")
		if Input.is_action_just_pressed("ui_accept"):
			if obj.has_method("on_click"):
				obj.on_click() 
	else:
		is_coliding = false
		%Label.text = ""
