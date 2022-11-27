using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;


/**
* Usage: Make an empty game object and attach this component script to it. Drag & drop CompositeCollider2D components
* into the sourceColliders field.
*
* This script isn't ideal, but it's simple and an okay jumping off point for more advanced changes (like caching).
*
* The main caveat is that it'll regenerate the child objects that comprise the shadow group every time you enter
* edit mode. For larger scenes this might be slow or annoying. It's a rough sketch I'm using for a jam game so I'm
* not too concerned about this, future travelers may way to dig into more optimal ways of not having to recreate the
* child object shadow casters.
*/
[ExecuteInEditMode]
[RequireComponent(typeof(UnityEngine.Rendering.Universal.CompositeShadowCaster2D))]
[DisallowMultipleComponent]
public class ShadowWrangler : MonoBehaviour
{
    public CompositeCollider2D[] sourceColliders = { };

    private static BindingFlags accessFlagsPrivate =
        BindingFlags.NonPublic | BindingFlags.Instance;

    private static FieldInfo shapePathField =
        typeof(UnityEngine.Rendering.Universal.ShadowCaster2D).GetField("m_ShapePath", accessFlagsPrivate);

    private static FieldInfo meshHashField =
        typeof(UnityEngine.Rendering.Universal.ShadowCaster2D).GetField("m_ShapePathHash", accessFlagsPrivate);


    // Start is called before the first frame update
    void OnEnable()
    {
        // only refresh this stuff in edit mode since the ShadowCaster components will serialize/persist their
        // shadow mesh on their own.
        // TODO: maybe only trigger the refresh when a difference is detected
        if (Application.isEditor) RefreshWorldShadows();
    }

    private void RefreshWorldShadows()
    {
        ClearChildShadows();
        foreach (var source in sourceColliders)
        {
            CreateGroupsForCollider(source);
        }
    }

    private void ClearChildShadows()
    {
        var doomed = new List<Transform>();
        for (int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i);
            if (child.GetComponent<UnityEngine.Rendering.Universal.ShadowCaster2D>() == null || !child.name.StartsWith("_caster_")) continue;
            doomed.Add(transform.GetChild(i));
        }

        foreach (var child in doomed)
        {
            DestroyImmediate(child.gameObject);
        }
    }

    /**
     * This is adapted from strategies in these two discussion threads:
     * - https://forum.unity.com/threads/shadow-caster-2d-not-working-on-tilemap.793803/
     * - https://forum.unity.com/threads/allow-programmatic-manipulation-for-shadowcaster2d-shape.829626/
     * Hopefully a day comes where we're allowed to programmatically mutate the shadow caster shape
     * programmatically.
     */
    private void CreateGroupsForCollider(CompositeCollider2D source)
    {
        for (int i = 0; i < source.pathCount; i++)
        {
            // get the path data
            Vector2[] pathVertices = new Vector2[source.GetPathPointCount(i)];
            source.GetPath(i, pathVertices);
            Vector3[] finalVerts = Array.ConvertAll<Vector2, Vector3>(pathVertices, input => input);

            // make a new child
            var shadowCaster = new GameObject("_caster_" + i + "_" + source.transform.name);
            shadowCaster.transform.parent = transform;

            // create & prime the shadow caster
            var shadow = shadowCaster.AddComponent<UnityEngine.Rendering.Universal.ShadowCaster2D>();
            shadow.selfShadows = true;
            shapePathField.SetValue(shadow, finalVerts);
            // invalidate the hash so it re-generates the shadow mesh on the next Update()
            meshHashField.SetValue(shadow, -1);
        }
    }
}