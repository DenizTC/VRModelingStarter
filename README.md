# VRModelingStarter
VRModelingStarter is an example project that demonstrates how 3D models can be created in a virtual / augmented environment. This project uses the Leap Motion controller to create polygons by pinching in the world.

### Before you begin
VRModelingStarter requires the [Orion software update](https://developer.leapmotion.com/orion).

The [EasyLeapUI](https://github.com/DenizTC/EasyLeapUI) toolkit is used for a few of the user interactions. To toggle between drawing mode, and manipulating mode, turn both palms towards your face.

### Drawing Quads
![Draw Quad GIF](https://media.giphy.com/media/xT1XGyMJNdXVTZUcAE/giphy.gif)

Drawing four vertices renders a new quad face. Vertices must be drawn in a clockwise direction for the normals to point outwards. Similarly, drawing them anti-clockwise results in the normals facing inward.

### Moving vertices
![Move Verts GIF](https://media.giphy.com/media/3o6ozr4yYFGTbzNUti/giphy.gif)

While in manipulating mode, a vertex can be moved by double pinching inside the vertex sphere gizmo.

### Manipulating mesh
![Manipulating Mesh GIF](https://media.giphy.com/media/xT1XH06R7VObfD7qLu/giphy.gif)

The MeshManipulation script which allows rotating, translating and scaling of the mesh, is a modified version of the LeapRTS script found in the [Pinch Move](https://developer.leapmotion.com/gallery/pinch-move) add-on module.

### Examples
An example scene is available in the following directory: Scenes/Example.unity
