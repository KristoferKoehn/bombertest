shader_type spatial;
render_mode shadows_disabled, unshaded;

uniform sampler2D ExplosionNoise;
uniform vec2 ExplosionDirection;
uniform vec3 ExplosionColor : source_color;
uniform float ColorIntensity;

vec3 lerp(vec3 col1, vec3 col2, float val) {
	vec3 ret = col1 + (col2 - col1) * val;
	return ret;
}

void vertex() {
	// Called for every vertex the material is visible on.
}

void fragment() {
	vec3 noiseData;
	if (UV.y < 0.25) {
		noiseData = texture(ExplosionNoise, UV + ExplosionDirection * TIME).xyz;
	} else if (UV.y < 0.5) {
		noiseData = texture(ExplosionNoise, UV - ExplosionDirection * TIME).xyz;
	} else {
		ALBEDO = vec3(1,1,1);
	}
	
	ALBEDO = lerp(ExplosionColor.xyz, vec3(1,1,1), pow(noiseData.r, ColorIntensity));
}
