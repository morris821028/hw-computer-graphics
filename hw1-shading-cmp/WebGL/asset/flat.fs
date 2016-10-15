#extension GL_OES_standard_derivatives : enable

precision mediump float;

varying vec4 vFragcolor;
varying vec3 vLightDirection;
varying vec3 vNormalDirection;
varying vec3 vVertexPosition;

uniform float uMaterialShininess;

uniform vec3 uAmbientColor;
uniform vec3 uPointLightingSpecularColor;
uniform vec3 uPointLightingDiffuseColor;

void main(void) {
	vec3 dx = dFdx(vVertexPosition);
	vec3 dy = dFdy(vVertexPosition);
	vec3 tn = -normalize(cross(dx, dy));
	float lambert = max(dot(tn, vLightDirection), 0.0);

	float specularLightWeighting = 0.0;
    
    vec3 eyeDirection = normalize(vVertexPosition.xyz);
    vec3 reflectionDirection = reflect(-vLightDirection, tn);

    specularLightWeighting = pow(max(dot(reflectionDirection, eyeDirection), 0.0), uMaterialShininess);
    
    float diffuseLightWeighting = max(dot(tn, vLightDirection), 0.0);
    vec3 lightWeighting = uAmbientColor
        + uPointLightingSpecularColor * specularLightWeighting
        + uPointLightingDiffuseColor * diffuseLightWeighting;

	gl_FragColor = vec4(vFragcolor.rgb * lightWeighting, vFragcolor.a);
}