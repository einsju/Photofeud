using System.Collections;
using UnityEngine;

namespace Photofeud.Effects
{
    public class ExampleEffect : MonoBehaviour
    {
        [SerializeField] Material[] answerButtonMaterials;

        //IEnumerator Start()
        //{
        //    while(true)
        //    {
        //        foreach (var material in answerButtonMaterials)
        //        {
        //            material.color = MaterialColorAlpha(material.color, 0.2f);
        //            yield return new WaitForSeconds(0.5f);
        //            material.color = MaterialColorAlpha(material.color, 0.1f);
        //        }
        //    }
        //}

        //Color MaterialColorAlpha(Color color, float alpha)
        //{
        //    var newColor = color;

        //    newColor.a = alpha;
        //    return color;
        //}
    }
}
