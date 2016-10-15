attribute vec3 aVertexPosition;
attribute vec3 aVertexNormal;
attribute vec2 aTextureCoord;

uniform mat4 uMVMatrix;
uniform mat4 uPMatrix;
uniform mat3 uNMatrix;

varying vec4 vFragcolor;
varying vec3 vLightDirection;
varying vec3 vNormalDirection;
varying vec3 vVertexPosition;

uniform vec3 uPointLightingLocation;

uniform sampler2D uSampler;
void main(void) {
    gl_Position = uPMatrix * uMVMatrix * vec4(aVertexPosition, 1.0);

    vNormalDirection = normalize(uNMatrix * aVertexNormal);
    vVertexPosition = (uMVMatrix * vec4(aVertexPosition, 1.0)).xyz;
	vLightDirection = normalize(uPointLightingLocation-(uMVMatrix * vec4((aVertexPosition), 1.0)).xyz);

    vec4 fragmentColor = texture2D(uSampler, vec2(aTextureCoord.s, aTextureCoord.t));
    vFragcolor = fragmentColor;
}