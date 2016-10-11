
attribute vec3 aVertexPosition;
attribute vec3 aVertexNormal;
attribute vec2 aTextureCoord;

uniform mat4 uMVMatrix;
uniform mat4 uPMatrix;
uniform mat3 uNMatrix;

varying vec3 vFragcolor;
varying vec3 vVertexPosition;
varying vec3 vNormalDirection;

uniform float uMaterialShininess;

uniform vec3 uAmbientColor;
uniform vec3 uPointLightingLocation;
uniform vec3 uPointLightingSpecularColor;
uniform vec3 uPointLightingDiffuseColor;

uniform sampler2D uSampler;

void main(void) {
    gl_Position = uPMatrix * uMVMatrix * vec4(aVertexPosition, 1.0);

    vec4 fragmentColor = texture2D(uSampler, vec2(aTextureCoord.s, aTextureCoord.t));
    vFragcolor = fragmentColor.rgb;
    vVertexPosition = aVertexPosition;
    vNormalDirection = normalize(uNMatrix * aVertexNormal);
}