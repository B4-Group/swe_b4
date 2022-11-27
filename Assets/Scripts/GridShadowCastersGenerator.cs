using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class GridShadowCastersGenerator : MonoBehaviour {

    public string colliderTag = "GenerateShadowCasters";
    public GameObject shadowCasterPrefab;
    public Transform shadowCastersContainer;
    public bool removePreviouslyGenerated = true;

    bool[,] hits;
    GameObject[,] instances;

    public GameObject[] Generate() {
        Debug.Log("### Generating ShadowCasters ###");

        /* get the bounds of the area to check */

        // collect colliders specified by tag

        var colliders = new List<Collider2D>();
        var tagedGos = GameObject.FindGameObjectsWithTag(colliderTag);

        foreach (var go in tagedGos) {
            var goColliders = go.GetComponents<Collider2D>();

            foreach (var goc in goColliders) {
                colliders.Add(goc);
            }
        }

        if (colliders.Count == 0) {
            Debug.Log("No colliders found, aborting.");
            return new GameObject[0];
        }

        // get outer-most bound vertices, defining the area to check

        var bottomLeft = new Vector2(Mathf.Infinity, Mathf.Infinity);
        var topRight = new Vector2(-Mathf.Infinity, -Mathf.Infinity);

        foreach (var col in colliders) {
            bottomLeft.x = Mathf.Min(bottomLeft.x, Mathf.Floor(col.bounds.min.x));
            bottomLeft.y = Mathf.Min(bottomLeft.y, Mathf.Floor(col.bounds.min.y));
            topRight.x = Mathf.Max(topRight.x, Mathf.Ceil(col.bounds.max.x));
            topRight.y = Mathf.Max(topRight.y, Mathf.Ceil(col.bounds.max.y));
        }

        Debug.Log("Bounds: downLeft = (" + bottomLeft.x + ", " + bottomLeft.y + ")");
        Debug.Log("Bounds: topRight = (" + topRight.x + ", " + topRight.y + ")");

        /* check the area for collisions */

        var countX = Mathf.RoundToInt(topRight.x - bottomLeft.x);
        var countY = Mathf.RoundToInt(topRight.y - bottomLeft.y);

        hits = new bool[countX, countY];
        instances = new GameObject[countX, countY];

        for (int y = 0; y < countY; y++) {
            for (int x = 0; x < countX; x++) {
                hits[x, y] = IsHit(new Vector2(bottomLeft.x + x + 0.5f, bottomLeft.y + y + 0.5f));
            }
        }

        /* instantiate shadow casters, merging single tiles horizontaly */

        // removing old shadow casters! careful!

        if (removePreviouslyGenerated) {
            foreach (Transform shadowCaster in shadowCastersContainer) {
                DestroyImmediate(shadowCaster.gameObject);
            }
        }

        // create new ones

        for (int y = 0; y < countY; y++) {
            var previousWasHit = false;
            GameObject currentInstance = null;

            for (int x = 0; x < countX; x++) {
                if (hits[x, y]) {
                    if (!previousWasHit) {

                        // create new shadowCasterPrefab instance

                        currentInstance = (GameObject)PrefabUtility.InstantiatePrefab(shadowCasterPrefab, shadowCastersContainer);
                        currentInstance.transform.position = new Vector3(bottomLeft.x + x + 0.5f, bottomLeft.y + y + 0.5f, 0.0f);
                    } else {

                        // stretch prevois shadowCasterPrefab instance

                        currentInstance.transform.localScale = new Vector3(currentInstance.transform.localScale.x + 1.0f, 1.0f, 0.0f);
                        currentInstance.transform.Translate(new Vector3(0.5f, 0.0f, 0.0f));
                    }

                    instances[x, y] = currentInstance;
                    previousWasHit = true;
                } else {
                    previousWasHit = false;
                }
            }
        }

        /* merge vertically if they have the same dimensions */

        for (int y = 0; y < countY - 1; y++) { // -1 for skipping last row
            for (int x = 0; x < countX; x++) {
                var bottomInstance = instances[x, y];
                var topInstance = instances[x, y + 1];

                if (bottomInstance != null && topInstance != null) {
                    if (bottomInstance != topInstance && bottomInstance.transform.localScale.x == topInstance.transform.localScale.x) {
                        
                        //merge! enlarge bottom instance...

                        bottomInstance.transform.localScale = new Vector3(bottomInstance.transform.localScale.x, bottomInstance.transform.localScale.y + 1.0f, 0.0f);
                        bottomInstance.transform.Translate(new Vector3(0.0f, 0.5f, 0.0f));

                        // ...destroy top instance, save to instances array

                        for (var i = 0; i < Mathf.RoundToInt(topInstance.transform.localScale.x); i++) {
                            instances[x + i, y + 1] = instances[x + i, y];
                        }

                        DestroyImmediate(topInstance);
                    }
                }
            }
        }

        Debug.Log("ShadowCasters generated.");

        /* return shadow casters */

        var shadowCasterInstances = new List<GameObject>();

        for (int y = 0; y < countY; y++) {
            for (int x = 0; x < countX; x++) {
                if (instances[x, y] != null && !shadowCasterInstances.Contains(instances[x, y])) {
                    shadowCasterInstances.Add(instances[x, y]);
                }
            }
        }

        return shadowCasterInstances.ToArray();
    }

    bool IsHit(Vector2 pos) {
        var margin = .2f; // prevents overlapping

        // get tile bounds

        var bottomLeft = new Vector2(pos.x - 0.5f + margin, pos.y + 0.5f - margin);
        var topRight = new Vector2(pos.x + 0.5f - margin, pos.y - 0.5f + margin);

        //check for collisions

        Collider2D[] colliders = Physics2D.OverlapAreaAll(bottomLeft, topRight);

        foreach (var col in colliders) {
            if (col.CompareTag(colliderTag)) {
                return true;
            }
        }

        return false;
    }
}

[CustomEditor(typeof(GridShadowCastersGenerator))]
public class GridShadowCastersGeneratorEditor : Editor {

    public override void OnInspectorGUI() {
        DrawDefaultInspector();

        if (GUILayout.Button("Generate")) {
            var generator = (GridShadowCastersGenerator)target;

            Undo.RecordObject(generator.shadowCastersContainer, "GridShadowCastersGenerator.generate"); // this does not work :(

            var casters = generator.Generate();

            // as a hack to make the editor save the shadowcaster instances, we rename them now instead of when theyre generated.

            Undo.RecordObjects(casters, "GridShadowCastersGenerator name prefab instances");

            for (var i = 0; i < casters.Length; i++) {
                casters[i].name += "_" + i.ToString();
            }
        }
    }
}
