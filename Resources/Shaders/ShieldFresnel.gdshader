shader_type spatial;
render_mode unshaded, cull_disabled;

uniform sampler2D ShieldTexture;
uniform vec2 NoiseDirection;

vec3 fresnel_glow(float amount, float intensity, vec3 color, vec3 normal, vec3 view)
{
	return pow((1.0 - dot(normalize(normal), normalize(view))), amount) * color * intensity;
}

void fragment()
{
	vec3 base_color = texture(ShieldTexture, UV + TIME * NoiseDirection).xyz;
	
	//vec3 base_color = vec3(0.5, 0.2, 0.9);
	vec3 fresnel_color = vec3(0.0, 0.7, 0.9);
	vec3 fresnel = fresnel_glow(2.0, 1.0, fresnel_color, NORMAL, VIEW);
	ALBEDO = base_color + fresnel;
	ALPHA = clamp(1.0, 0, length(fresnel));
}

//void light() {
	// Called for every pixel for every light affecting the material.
	// Uncomment to replace the default light processing function with this one.
//}
