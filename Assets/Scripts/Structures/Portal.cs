using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(MeshFilter))]
[RequireComponent (typeof(MeshRenderer))]
public class Portal : MonoBehaviour
{
    public Material material;

    private string objectiveName = "castle";

    // Start is called before the first frame update
    void Start()
    {
        CombineMeshes();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void CombineMeshes()
    {
        //// All our children (and us)
        //MeshFilter[] filters = GetComponentsInChildren<MeshFilter>(false);

        //// All the meshes in our children (just a big list)
        //List<Material> materials = new List<Material>();
        //MeshRenderer[] renderers = GetComponentsInChildren<MeshRenderer>(false); // <-- you can optimize this
        //foreach (MeshRenderer renderer in renderers)
        //{
        //    if (renderer.transform == transform)
        //        continue;
        //    Material[] localMats = renderer.sharedMaterials;
        //    foreach (Material localMat in localMats)
        //        if (!materials.Contains(localMat))
        //            materials.Add(localMat);
        //}

        //// Each material will have a mesh for it.
        //List<Mesh> submeshes = new List<Mesh>();
        //foreach (Material material in materials)
        //{
        //    // Make a combiner for each (sub)mesh that is mapped to the right material.
        //    List<CombineInstance> combiners = new List<CombineInstance>();
        //    foreach (MeshFilter filter in filters)
        //    {
        //        if (filter.transform == transform) continue;
        //        // The filter doesn't know what materials are involved, get the renderer.
        //        MeshRenderer renderer = filter.GetComponent<MeshRenderer>();  // <-- (Easy optimization is possible here, give it a try!)
        //        if (renderer == null)
        //        {
        //            Debug.LogError(filter.name + " has no MeshRenderer");
        //            continue;
        //        }

        //        // Let's see if their materials are the one we want right now.
        //        Material[] localMaterials = renderer.sharedMaterials;
        //        for (int materialIndex = 0; materialIndex < localMaterials.Length; materialIndex++)
        //        {
        //            if (localMaterials[materialIndex] != material)
        //                continue;
        //            // This submesh is the material we're looking for right now.
        //            CombineInstance ci = new CombineInstance();
        //            ci.mesh = filter.sharedMesh;
        //            ci.subMeshIndex = materialIndex;
        //            ci.transform = Matrix4x4.identity;
        //            combiners.Add(ci);
        //        }
        //    }
        //    // Flatten into a single mesh.
        //    Mesh mesh = new Mesh();
        //    mesh.CombineMeshes(combiners.ToArray(), true);
        //    submeshes.Add(mesh);
        //}

        //// The final mesh: combine all the material-specific meshes as independent submeshes.
        //List<CombineInstance> finalCombiners = new List<CombineInstance>();
        //foreach (Mesh mesh in submeshes)
        //{
        //    CombineInstance ci = new CombineInstance();
        //    ci.mesh = mesh;
        //    ci.subMeshIndex = 0;
        //    ci.transform = Matrix4x4.identity;
        //    finalCombiners.Add(ci);
        //}
        //Mesh finalMesh = new Mesh();
        //finalMesh.CombineMeshes(finalCombiners.ToArray(), false);
        //GetComponent<MeshFilter>().sharedMesh = finalMesh;

        Quaternion oldRot = transform.rotation;
        Vector3 oldPos = transform.position;

        transform.rotation = Quaternion.identity;
        transform.position = Vector3.zero;

        MeshFilter[] filters = GetComponentsInChildren<MeshFilter>();

        Mesh finalMesh = new Mesh();

        CombineInstance[] combiners = new CombineInstance[filters.Length];

        for (int i = 0; i < filters.Length; i++)
        {
            if (filters[i].transform == transform)
            {
                continue;
            }

            combiners[i].subMeshIndex = 0;
            combiners[i].mesh = filters[i].sharedMesh;
            combiners[i].transform = filters[i].transform.localToWorldMatrix;
        }

        finalMesh.CombineMeshes(combiners);

        GetComponent<MeshFilter>().sharedMesh = finalMesh;
        GetComponent<MeshCollider>().sharedMesh = finalMesh;

        transform.rotation = oldRot;
        transform.position = oldPos;
        transform.localScale = new Vector3(1, 1, 1);

        GetComponent<MeshRenderer>().material = material;

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}
