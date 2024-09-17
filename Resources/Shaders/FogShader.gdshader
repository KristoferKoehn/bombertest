shader_type fog;

uniform sampler2D FogTexture;

void fog() {
	ALBEDO = vec3(1.0,1.0,1.0);
	DENSITY = pow(texture(FogTexture, WORLD_POSITION.xz/40.0).r, 2.0);
	// Called once for every froxel that is touched by an axis-aligned bounding box
	// of the associated FogVolume. This means that froxels that just barely touch
	// a given FogVolume will still be used.
}
