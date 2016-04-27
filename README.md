# VRModelingStarter
A starter project for creating 3D models in VR/AR, using the Leap Motion.

### Drawing Quads
![Draw Quad GIF](http://im2.ezgif.com/tmp/ezgif-2516107953.gif)

Drawing four vertices renders a new quad face. Vertices must be drawn in a clockwise direction for the normals to point outwards. Similarly, drawing them anti-clockwise results in the normals facing inward.

### Moving vertices
![Move Verts GIF](http://im2.ezgif.com/tmp/ezgif-286637383.gif)

While in manipulating mode, a vertex can be moved by double pinching inside the vertex sphere gizmo.

### Manipulating mesh
![Manipulating Mesh GIF](http://im2.ezgif.com/tmp/ezgif-1079933731.gif)

The MeshManipulation script which allows rotating, translating and scaling of the mesh, is a modified version of the LeapRTS script found in the Pinch Move add-on module: https://developer.leapmotion.com/gallery/pinch-move
