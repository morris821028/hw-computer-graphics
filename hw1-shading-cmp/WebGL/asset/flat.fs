#extension GL_OES_standard_derivatives : enable

precision mediump float;

varying vec3 vFragcolor;
varying vec3 vLightDirection;
varying vec3 vNormalDirection;
varying vec3 vVertexPosition;

void main(void) {
	vec3 dx = dFdx(vVertexPosition);
	vec3 dy = dFdy(vVertexPosition);
	vec3 tn = normalize(cross(dx, dy));
	float lambert = max(-dot(tn, vLightDirection), 0.0);

	gl_FragColor = vec4(vFragcolor * lambert, 1.0);
}