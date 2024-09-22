// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class RandomCubeMovement1 : MonoBehaviour
// {
//     public GameObject cubePrefab;  // Drag the cube prefab here
//     public Material[] materials;   // Array of materials to assign to cubes
//     public int numberOfCubes = 20;  // Number of cubes to spawn
//     public float planeSize = 250f;  // Size of the plane to spawn cubes on
//     public float moveSpeed = 3f;   // Speed at which the cubes move
//     public float directionChangeInterval = 2f;  // Time interval for changing direction
//     public float maxScale = 5f;   // Maximum scale size for the cubes

//     private List<GameObject> cubes = new List<GameObject>();

//     void Start()
// {
//     for (int i = 0; i < numberOfCubes; i++)
//     {
//         Vector3 randomPosition = new Vector3(
//             Random.Range(-planeSize / 2, planeSize / 2),
//             5f,
//             Random.Range(-planeSize / 2, planeSize / 2)
//         );
//         GameObject cube = Instantiate(cubePrefab, randomPosition, Quaternion.identity);

//         // Add Rigidbody component and configure it
//         Rigidbody rb = cube.AddComponent<Rigidbody>();
//         rb.isKinematic = false; // Set to false to use physics
//         rb.collisionDetectionMode = CollisionDetectionMode.Continuous; // Set collision detection mode to Continuous
//         rb.interpolation = RigidbodyInterpolation.Interpolate; // Optional for smoother movement

//         // Get random scale with more cubes in range 1 to 2 and fewer in 3 to 5
//         float randomScale = GetWeightedRandomScale();
//         cube.transform.localScale = new Vector3(randomScale, randomScale, randomScale);

//         // Assign a random material to the cube
//         Material randomMaterial = materials[Random.Range(0, materials.Length)];
//         cube.GetComponent<Renderer>().material = randomMaterial;

//         // Add the DirectionMovement component to handle movement
//         cube.AddComponent<DirectionMovement>().Initialize(moveSpeed, directionChangeInterval, planeSize);
//         // Set the tag for the cube
//         cube.tag = "Cube";
//     }
// }
//     // Function to get weighted random scale
//     private float GetWeightedRandomScale()
//     {
//         float randomValue = Random.value;

//         // More likely to get values between 1 and 2
//         if (randomValue < 0.85f)  // 70% chance to scale between 1 and 2
//         {
//             return Random.Range(1f, 2f);
//         }
//         else  // 30% chance to scale between 3 and 5
//         {
//             return Random.Range(3f, 5f);
//         }
//     }
// }