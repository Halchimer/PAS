#version 120

uniform sampler2D texture;
uniform float offset;

void main() {
    gl_FragColor = texture2D(texture, vec2(gl_TexCoord[0].x + offset, gl_TexCoord[0].y));
}
