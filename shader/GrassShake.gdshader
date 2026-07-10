shader_type canvas_item;
uniform float speed : hint_range(0.0, 10.0) = 3.0;
uniform float strength : hint_range(0.0, 10.0) = 3.0;

void vertex() {
	float top_mask = 1.0 - UV.y;
	float wave = sin(TIME * speed + VERTEX.x * 0.1);
	float offest = round(wave * strength);
	
	VERTEX.x += offest * top_mask;
}

void fragment() {
	// Called for every pixel the material is visible on.
}

//void light() {
//	// Called for every pixel for every light affecting the CanvasItem.
//	// Uncomment to replace the default light processing function with this one.
//}
