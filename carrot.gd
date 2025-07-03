extends StaticBody3D


func on_click():
	get_parent().queue_free()
