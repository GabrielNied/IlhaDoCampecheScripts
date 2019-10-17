using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrocaCor : MonoBehaviour
{
    public bool trocaCor = false;

    public List<GameObject> trocaLuz;

    public List<Material> mats;

    public Material[] skyBox;

    void Update()
    {
        if (trocaCor)
        {
            RenderSettings.skybox = (Material) skyBox[0];
            RenderSettings.ambientLight = new Color(96/255f, 0, 3/255f);
            RenderSettings.fogColor = new Color(41 / 255f, 12/255f, 15 / 255f);
            trocaLuz[0].GetComponent<MeshRenderer>().material = mats[0];
            trocaLuz[1].GetComponent<ParticleSystemRenderer>().material = mats[1];
            trocaLuz[1].GetComponent<ParticleSystemRenderer>().trailMaterial = mats[1];
            trocaLuz[2].GetComponent<Light>().color = new Color(253 / 255f, 168 / 255f, 175 / 255f);
        }
        else
        {
            RenderSettings.skybox = (Material)skyBox[1];
            RenderSettings.ambientLight = new Color(0, 21/255f, 96 / 255f);
            RenderSettings.fogColor = new Color(12 / 255f, 13/255f, 41 / 255f);
            trocaLuz[0].GetComponent<MeshRenderer>().material = mats[2];
            trocaLuz[1].GetComponent<ParticleSystemRenderer>().material = mats[3];
            trocaLuz[1].GetComponent<ParticleSystemRenderer>().trailMaterial = mats[3];
            trocaLuz[2].GetComponent<Light>().color = new Color(168 / 255f, 240 / 255f, 253 / 255f);
        }
    }
}
