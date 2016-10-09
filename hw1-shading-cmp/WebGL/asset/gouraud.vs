
attribute vec3 aVertexPosition;
attribute vec3 aVertexNormal;
attribute vec2 aTextureCoord;

uniform mat4 uMVMatrix;
uniform mat4 uPMatrix;
uniform mat3 uNMatrix;

varying vec4 fragcolor;

uniform float uMaterialShininess;

uniform vec3 uAmbientColor;
uniform vec3 uPointLightingLocation;
uniform vec3 uPointLightingSpecularColor;
uniform vec3 uPointLightingDiffuseColor;

uniform sampler2D uSampler;

void main(void) {
    //vPosition = uMVMatrix * vec4(aVertexPosition, 1.0);
    gl_Position = uPMatrix * uMVMatrix * vec4(aVertexPosition, 1.0);
    
    vec3 lightWeighting;
    vec3 lightDirection = normalize(uPointLightingLocation - (uMVMatrix * vec4(aVertexPosition, 1.0)).xyz);
    vec3 normal = normalize(uNMatrix * aVertexNormal);

    float specularLightWeighting = 0.0;
    
    vec3 eyeDirection = normalize(-(uMVMatrix * vec4(aVertexPosition, 1.0)).xyz);
    vec3 reflectionDirection = reflect(-lightDirection, normal);

    specularLightWeighting = pow(max(dot(reflectionDirection, eyeDirection), 0.0), uMaterialShininess);
    

    float diffuseLightWeighting = max(dot(normal, lightDirection), 0.0);
     lightWeighting = uAmbientColor
        + uPointLightingSpecularColor * specularLightWeighting
        + uPointLightingDiffuseColor * diffuseLightWeighting;       

    vec4 fragmentColor;
    fragmentColor = texture2D(uSampler, vec2(aTextureCoord.s, aTextureCoord.t));
    fragcolor = vec4(fragmentColor.rgb * lightWeighting, fragmentColor.a);
}