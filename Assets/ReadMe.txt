https://github.com/DenizTC/VRModelingStarter 

VRModelingStarter: 

	VRModelingStarter is an example project that demonstrates how 3D models 
	can be created in a virtual / augmented environment. This project uses 
	the Leap Motion controller to create polygons by pinching in the world. 

Before you begin: 

	VRModelingStarter requires the Orion software update: 
	https://developer.leapmotion.com/orion 

	The EasyLeapUI toolkit (https://github.com/DenizTC/EasyLeapUI) is used 
	for a few of the user interactions. To toggle between drawing mode, and 
	manipulating mode, turn both palms towards your face. 

Drawing Quads: 

	Drawing four vertices renders a new quad face. Vertices must be drawn in 
	a clockwise direction for the normals to point outwards. Similarly, 
	drawing them anti-clockwise results in the normals facing inward. 

Moving vertices: 

	While in manipulating mode, a vertex can be moved by double pinching 
	inside the vertex sphere gizmo. 

Manipulating mesh: 

	The MeshManipulation script which allows rotating, translating and 
	scaling of the mesh, is a modified version of the LeapRTS script found 
	in the Pinch Move add-on module: 
	https://developer.leapmotion.com/gallery/pinch-move 

Examples: 

	An example scene is available in the following directory: 
	Scenes/Example.unity 

Contact: 
	You can email me if you have any 
	questions, come across any bugs, or have and future requests: 
	denizcetinalp@gmail.com https://github.com/DenizTC 

