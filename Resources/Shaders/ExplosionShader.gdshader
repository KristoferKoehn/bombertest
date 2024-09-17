shader_type spatial;

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
	vec3 noiseData = texture(ExplosionNoise, UV + ExplosionDirection * TIME).xyz;
	
	//ALBEDO = ExplosionColor * noiseData;
	ALBEDO = lerp(ExplosionColor, vec3(1,1,1), pow(noiseData.r, ColorIntensity));
	//ALPHA = noiseData.r;
	// Called for every pixel the material is visible on.
}
