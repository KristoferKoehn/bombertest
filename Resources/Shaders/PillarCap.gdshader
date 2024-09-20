shader_type spatial;
render_mode unshaded, cull_disabled;

uniform sampler2D noise;
uniform sampler2D CutoffNoise;
uniform vec2 direction;
uniform float ColorIntensity;
uniform vec3 color : source_color;

vec3 lerp(vec3 col1, vec3 col2, float val) {
	vec3 ret = col1 + (col2 - col1) * val;
	return ret;
}

void vertex() {
	// Called for every vertex the material is visible on.
}

void fragment() {
	float cutoffModifier = texture(CutoffNoise, UV - TIME/3.0).r;
	
	if (UV.y < 0.5 + cutoffModifier/10.0) {
		vec3 noiseData = texture(noise, UV + TIME * direction).xyz;
		ALBEDO = lerp(vec3(1,1,1), color, pow(noiseData.r, ColorIntensity));
	} else {
		discard;
	}
}
