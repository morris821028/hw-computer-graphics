// <script id="per-fragment-lighting-fs" type="x-shader/x-fragment">
precision mediump float;

varying vec3 vFragcolor;
varying vec3 vVertexPosition;
varying vec3 vNormalDirection;


void main(void) {
	// vec3 lightDirection = normalize(uPointLightingLocation - (uMVMatrix * vec4(aVertexPosition, 1.0)).xyz);
    gl_FragColor = vec4(vFragcolor.rgb, 1);
}
// </script>