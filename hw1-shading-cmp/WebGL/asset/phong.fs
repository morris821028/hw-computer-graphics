// <script id="per-fragment-lighting-fs" type="x-shader/x-fragment">
precision mediump float;


varying vec2 vTextureCoord;

varying vec3 vLightDirection;
varying vec3 vNormalDirection;
varying vec3 vVertexPosition;

uniform float uMaterialShininess;

uniform vec3 uAmbientColor;
uniform vec3 uPointLightingSpecularColor;
uniform vec3 uPointLightingDiffuseColor;

uniform sampler2D uSampler;

void main(void) {
	vec3 tn = vNormalDirection;

	float specularLightWeighting = 0.0;
    
    vec3 eyeDirection = normalize(-vVertexPosition.xyz);
    vec3 reflectionDirection = reflect(-vLightDirection, tn);

    specularLightWeighting = pow(max(dot(reflectionDirection, eyeDirection), 0.0), uMaterialShininess);
    
    float diffuseLightWeighting = max(dot(tn, vLightDirection), 0.0);
    vec3 lightWeighting = uAmbientColor
        + uPointLightingSpecularColor * specularLightWeighting
        + uPointLightingDiffuseColor * diffuseLightWeighting;
	vec4 fragmentColor = texture2D(uSampler, vec2(vTextureCoord.s, vTextureCoord.t));
	gl_FragColor = vec4(fragmentColor.rgb * lightWeighting, fragmentColor.a);
}
// </script>